# Identify 身份认证与权限管理二次开发文档

**适用项目：** `H.Modules.Identity`、`H.Services.Identity`、`H.ApplicationBases.Identify`、`H.Modules.Login`  
**核心类型：** `ILoginService`、`IRegisterService`、`IUser`、`IRole`、`IAuthor`、`IocLogin`、`IdentifyDataContext`  
**相关能力：** 登录、退出、注册、密码重置、用户管理、角色管理、权限管理、操作日志、SQLite 持久化

本文介绍框架中 Identify 身份认证模块的注册、启动流程、用户/角色/权限模型、登录与注册服务，以及业务功能中的授权检查方式。

> 仓库中同时存在 `Identity`、`Identify`、`Indentify`、`Registor` 等历史命名。本文说明概念时使用“身份认证”，代码示例必须使用框架中的实际 API 名称。

---

## 1. 模块定位

框架身份模块包含两个相关但不同的过程：

```text
Authentication（认证）
验证账号和密码，确认当前用户是谁

Authorization（授权）
根据用户角色和权限编码，判断当前用户能否执行某项操作
```

整体关系：

```text
User 用户
    ↓ 属于
Role 角色
    ↓ 拥有多个
Author 权限
```

运行流程：

```text
ApplicationBase.OnStartup
    ↓
SplashScreen 初始化设置和数据库
    ↓
ApplicationBase.OnLogin
    ↓
ILoginViewPresenter 显示登录页面
    ↓
ILoginService.Login(account, password)
    ↓
设置 ILoginService.User
    ↓
主窗口显示
    ↓
业务通过 IocLogin.User.IsValid(authorCode) 检查权限
```

---

## 2. 相关项目职责

| 项目 | 职责 |
|---|---|
| `H.Services.Identity` | 定义登录、注册、用户、角色、权限和 Presenter 公共接口。 |
| `H.Modules.Identity` | EF Core 数据模型、SQLite 数据上下文、登录与注册实现、用户/角色/权限管理页面。 |
| `H.Modules.Login` | 登录、注册和登录后加载页面及其 Presenter。 |
| `H.ApplicationBases.Identify` | 将数据库、身份模块、登录页面和操作日志组合成默认应用基座。 |
| `H.Modules.Operation` | 记录登录、退出、注册和重置密码等操作日志。 |
| `H.Test.Identify` | 手动注册身份模块的测试示例。 |

---

## 3. 快速开始：使用默认身份应用基座

最简方式是让应用继承：

```csharp
IdentifyApplicationBase
```

示例：

```csharp
public partial class App : IdentifyApplicationBase
{
    protected override Window CreateMainWindow(StartupEventArgs e)
    {
        return new MainWindow();
    }
}
```

`IdentifyApplicationBase` 默认执行：

```csharp
protected override void ConfigureServices(IServiceCollection services)
{
    services.AddIdentifyDefaultServices();
}

protected override void Configure(IApplicationBuilder app)
{
    base.Configure(app);
    app.UseIdentifyDefaultOptions();
}
```

`AddIdentifyDefaultServices()` 会组合注册：

- `IdentifyDataContext`
- 用户、角色、权限 Repository
- 用户、角色、权限管理 Presenter
- 操作日志数据库和 Presenter
- 背景登录/注册 Presenter
- `IdentityLoginService`
- `IdentityRegisterService`

适合：

- 采用框架默认 SQLite 存储。
- 采用默认登录与注册页面。
- 需要完整用户、角色和权限管理。

---

## 4. 默认服务注册

默认组合扩展：

```csharp
services.AddIdentifyDefaultServices();
```

也可传入配置：

```csharp
services.AddIdentifyDefaultServices(options =>
{
    options.UseLoginOptions(login =>
    {
        // 登录页面配置
    });

    options.UseRegistorOptions(register =>
    {
        register.UseMail = false;
    });

    options.UseIdentifyOptions(identity =>
    {
        identity.UseAdiminCheckOnRegister = true;
        identity.UseUserLicenseDeadTime = true;
        identity.UserLicenseDefaultTryTime = TimeSpan.FromDays(30);
    });

    options.UseSqliteSettable(sqlite =>
    {
        // SQLite 配置
    });
});
```

对应应用配置：

```csharp
app.UseIdentifyDefaultOptions(options =>
{
    options.UseRegistorOptions(register =>
    {
        register.UseMail = false;
    });
});
```

`DefaultIndentifyOptions` 用于暂存并传递多个模块的配置：

```csharp
UseLoginOptions(Action<ILoginOptions>)
UseIdentifyOptions(Action<IIdentifyOptions>)
UseRegistorOptions(Action<IRegistorOptions>)
UseSqliteSettable(Action<ISqliteSettable>)
```

---

## 5. 手动注册身份模块

需要自定义应用基座或数据库上下文时，可以逐项注册。

```csharp
protected override void ConfigureServices(IServiceCollection services)
{
    services.AddSetting();
    services.AddWindowMessage();
    services.AddAdornerDialogMessage();
    services.AddFormMessageService();

    services.AddDbContextBySetting<IdentifyDataContext>();

    services.AddSingleton<IStringRepository<hi_dd_user>,
        DbContextRepository<IdentifyDataContext, hi_dd_user>>();
    services.AddUserViewPresenter();

    services.AddSingleton<IStringRepository<hi_dd_role>,
        DbContextRepository<IdentifyDataContext, hi_dd_role>>();
    services.AddRoleViewPresenter();

    services.AddSingleton<IStringRepository<hi_dd_author>,
        DbContextRepository<IdentifyDataContext, hi_dd_author>>();
    services.AddAuthorityViewPresenter();

    services.AddBackgroundRigisterLoginViewPresenter();
    services.AddIdentityLoginService();
    services.AddIdentityRegisterService();
}
```

应用配置：

```csharp
protected override void Configure(IApplicationBuilder app)
{
    base.Configure(app);
    app.UseLoginOptions();
    app.UseRegistorOptions(options => options.UseMail = false);
    app.UseIdentifyOptions();
    app.UseSqlite();
}
```

### 5.1 使用自定义 DbContext

默认组合扩展支持泛型上下文：

```csharp
services.AddIdentifyDefaultServices<MyIdentityDataContext>();
```

上下文必须继承 `DbContext`，并提供身份实体所需映射。

---

## 6. 登录在应用启动中的位置

`ApplicationBase.OnStartup()` 先执行启动页，再执行登录：

```csharp
this.OnSplashScreen(e);
this.OnLogin();
this.MainWindow.Show();
```

`OnLogin()` 获取：

```csharp
ILoginViewPresenter presenter =
    Ioc.Services.GetService<ILoginViewPresenter>();
```

未注册 `ILoginViewPresenter` 时：

```csharp
presenter == null
```

框架会跳过登录页面，直接继续启动。

已注册时，登录窗口配置为：

```csharp
x.MinWidth = 400;
x.DialogButton = DialogButton.None;
x.Title = Assembly.GetEntryAssembly().GetName().Version.ToString();
```

登录返回 `false` 时：

```csharp
this.Shutdown();
```

因此默认身份应用必须在登录成功后才显示主窗口。

---

## 7. `ILoginService`

接口：

```csharp
public interface ILoginService
{
    IUser User { get; }
    bool Login(string name, string password, out string message);
    bool Logout(out string message);
}
```

| 成员 | 说明 |
|---|---|
| `User` | 当前已认证用户；未登录或退出后为 `null`。 |
| `Login` | 验证账号、密码和用户状态。 |
| `Logout` | 清除当前用户。 |

注入使用：

```csharp
public class AccountViewModel
{
    private readonly ILoginService _loginService;

    public AccountViewModel(ILoginService loginService)
    {
        _loginService = loginService;
    }

    public bool Login(string account, string password, out string message)
    {
        return _loginService.Login(account, password, out message);
    }
}
```

全局访问：

```csharp
IUser currentUser = IocLogin.User;
```

`IocLogin` 定义：

```csharp
public class IocLogin : Ioc<ILoginService>
{
    public static IUser User => Instance?.User;
}
```

业务服务优先构造函数注入 `ILoginService`；XAML、命令或框架扩展中可使用 `IocLogin.User`。

---

## 8. 默认登录流程

`IdentityLoginService.Login()` 依次执行：

```text
1. 检查账号非空
2. 检查密码非空
3. 检查内置 AdminUser
4. 从用户 Repository 按账号查询
5. 比较密码
6. 检查 Enable
7. 创建 User 包装对象
8. 设置当前 User
9. 写入操作日志
```

核心调用：

```csharp
bool success = loginService.Login(account, password, out string message);
```

成功后：

```csharp
loginService.User != null
```

失败消息包括：

- 用户名不能为空。
- 密码不能为空。
- 用户名不正确。
- 密码不正确。
- 用户未激活。

退出：

```csharp
bool success = loginService.Logout(out string message);
```

退出会清除：

```csharp
User = null;
```

并写入操作日志。

---

## 9. 当前用户：`IUser`

接口：

```csharp
public interface IUser
{
    string ID { get; }
    string Account { get; set; }
    string Password { get; set; }
    string Name { get; set; }
    IRole Role { get; }
    bool IsValid(string authorId);
}
```

常用访问：

```csharp
IUser user = IocLogin.User;

string id = user?.ID;
string account = user?.Account;
string name = user?.Name;
IRole role = user?.Role;
```

检查是否登录：

```csharp
bool isLogined = IocLogin.User != null;
```

建议封装业务服务：

```csharp
public interface ICurrentUserService
{
    bool IsAuthenticated { get; }
    IUser User { get; }
}
```

这样业务代码不必直接依赖全局 IOC。

---

## 10. 角色：`IRole`

接口：

```csharp
public interface IRole
{
    string ID { get; }
    string Name { get; set; }
    IReadOnlyCollection<IAuthor> Authors { get; }
    bool IsValid(string authorId);
}
```

角色包含多个权限：

```text
管理员
├── 用户管理
├── 角色管理
├── 项目删除
└── 系统设置
```

角色检查：

```csharp
bool isAdminRole = IocLogin.User?.IsInRole(AdminRoleId) == true;
```

扩展方法：

```csharp
public static bool IsInRole(this IUser user, string roleId)
{
    if (user?.Role == null)
        return false;
    return user.Role.ID == roleId;
}
```

除少量固定角色场景外，推荐按权限授权，不要在业务代码中大量硬编码角色判断。

---

## 11. 权限：`IAuthor`

权限实体契约：

```csharp
public interface IAuthor
{
    string ID { get; }
    string Name { get; }
}
```

数据库实体 `hi_dd_author` 还包含：

```csharp
public string AuthorCode { get; set; }
public virtual ICollection<hi_dd_role> Roles { get; set; }
```

| 字段 | 建议用途 |
|---|---|
| `ID` | 数据库实体身份和关联键。 |
| `Name` | 权限显示名称。 |
| `AuthorCode` | 业务授权代码，应保持稳定。 |

示例权限代码：

```text
user.view
user.edit
user.delete
role.manage
project.open
project.delete
setting.edit
```

权限代码不应使用易变化的界面文字，例如“删除用户”。

---

## 12. 权限检查

默认 `User.IsValid()` 委托给角色：

```csharp
public virtual bool IsValid(string authorId)
{
    if (this.Role == null)
        return false;
    return this.Role.IsValid(authorId);
}
```

默认 `Role.IsValid()` 实际比较：

```csharp
return this.Model.Authors.Any(x => x.AuthorCode == authorId);
```

因此常规检查应传入 **`AuthorCode`**：

```csharp
public static class AuthorityCodes
{
    public const string UserView = "user.view";
    public const string UserEdit = "user.edit";
    public const string UserDelete = "user.delete";
}

bool canDelete =
    IocLogin.User?.IsValid(AuthorityCodes.UserDelete) == true;
```

### 12.1 `IsInAuthor()` 与 `IsValid()` 的差别

框架扩展：

```csharp
public static bool IsInAuthor(this IUser user, string authorId)
{
    return user.Role.Authors.Any(x => x.ID == authorId);
}
```

它比较 `IAuthor.ID`。

而 `Role.IsValid()` 比较 `hi_dd_author.AuthorCode`。

因此：

```csharp
user.IsInAuthor(databasePermissionId); // 传数据库 ID
user.IsValid(stablePermissionCode);    // 传 AuthorCode
```

业务授权建议统一使用 `IsValid(AuthorCode)`，避免数据库重新建库、迁移或种子变化后 ID 不一致。

---

## 13. 在命令中检查权限

```csharp
public DisplayCommand DeleteUserCommand => new DisplayCommand(
    _ => DeleteUser(),
    _ => IocLogin.User?.IsValid(AuthorityCodes.UserDelete) == true)
{
    Name = "删除用户",
    Icon = FontIcons.Delete,
    Description = "删除当前选中的用户"
};
```

如果还依赖选择状态：

```csharp
public DisplayCommand DeleteUserCommand => new DisplayCommand(
    _ => DeleteUser(),
    _ => SelectedUser != null &&
         IocLogin.User?.IsValid(AuthorityCodes.UserDelete) == true);
```

用户或权限变化后刷新命令状态：

```csharp
CommandManager.InvalidateRequerySuggested();
```

注意：禁用按钮只是用户体验，真正的数据修改服务中仍必须再次验证权限。

---

## 14. 在 ViewModel 和 Service 中授权

推荐建立集中授权服务：

```csharp
public interface IAuthorizationService
{
    bool IsGranted(string authorityCode);
    void EnsureGranted(string authorityCode);
}

public class AuthorizationService : IAuthorizationService
{
    private readonly ILoginService _loginService;

    public AuthorizationService(ILoginService loginService)
    {
        _loginService = loginService;
    }

    public bool IsGranted(string authorityCode)
    {
        return _loginService.User?.IsValid(authorityCode) == true;
    }

    public void EnsureGranted(string authorityCode)
    {
        if (!IsGranted(authorityCode))
            throw new UnauthorizedAccessException($"缺少权限：{authorityCode}");
    }
}
```

业务服务：

```csharp
public class UserManagementService
{
    private readonly IAuthorizationService _authorization;
    private readonly IStringRepository<hi_dd_user> _repository;

    public void Delete(string userId)
    {
        _authorization.EnsureGranted(AuthorityCodes.UserDelete);
        _repository.Delete(userId, true);
    }
}
```

这种方式比只在 XAML 中隐藏按钮更可靠，也更容易单元测试。

---

## 15. 在 XAML 中控制显示

ViewModel 暴露权限状态：

```csharp
public bool CanManageUsers =>
    IocLogin.User?.IsValid(AuthorityCodes.UserEdit) == true;
```

XAML：

```xaml
<Button
    Command="{Binding EditUserCommand}"
    Content="编辑用户"
    Visibility="{Binding CanManageUsers, Converter={x:Static Converter.GetTrueToVisible}}" />
```

或仅通过命令 `CanExecute` 禁用：

```xaml
<Button Command="{Binding EditUserCommand}" Content="编辑用户" />
```

选择建议：

- 用户应知道功能存在但当前不可用：禁用。
- 功能不应向无权限用户显示：隐藏。
- 无论隐藏还是禁用，Service 层都必须再次授权。

---

## 16. 用户、角色与权限数据库模型

`IdentifyDataContext` 包含：

```csharp
public DbSet<hi_dd_user> hi_dd_users { get; set; }
public DbSet<hi_dd_role> hi_dd_roles { get; set; }
public DbSet<hi_dd_author> hi_dd_authors { get; set; }
```

关系：

```text
hi_dd_user
    RoleID ─────→ hi_dd_role

hi_dd_role
    Authors ←──→ hi_dd_author
    Users   ←──→ hi_dd_user
```

用户主要字段：

- `Name`
- `Account`
- `Password`
- `DisplayName`
- `Enable`
- `Mail`
- `LastLoginTime`
- `LicenseDeadline`
- `RoleID`
- `Role`

角色主要字段：

- `Name`
- `Code`
- `Authors`
- `Users`

权限主要字段：

- `Name`
- `AuthorCode`
- `Roles`

---

## 17. 数据库和种子数据

`IdentifyDataContext.OnModelCreating()` 调用：

```csharp
modelBuilder.BuildIdentifySeed();
```

默认种子包含：

- “用户管理”权限。
- “角色管理”权限。
- “管理员”角色。
- “普通用户”角色。
- `admin` 用户。
- `user` 用户。

当前种子密码为：

```text
123456
```

首次开发测试可以使用种子账号，但生产环境必须在部署前移除、禁用或强制修改默认凭据。

### 17.1 EF Core 迁移

模块包含 `Migrations`。数据库模型改变后应创建并应用迁移，而不是直接修改已有生产数据库。

典型命令：

```powershell
dotnet ef migrations add AddIdentityField --project <包含上下文的项目>
dotnet ef database update --project <包含上下文的项目>
```

实际启动项目、上下文工厂和连接设置应按解决方案结构指定。

---

## 18. 用户管理 Presenter

注册：

```csharp
services.AddUserViewPresenter();
```

它注册：

```text
IObservableSourceRepositoryBindable<hi_dd_user>
IUserViewPresenter → UserViewPresenter
```

显示：

```csharp
IocMessage.Dialog.Show(Ioc.GetService<IUserViewPresenter>());
```

框架命令 `ShowUserViewCommand` 可打开用户管理页面。

用户管理支持：

- 查看用户。
- 新增和编辑用户。
- 设置用户角色。
- 启用或停用用户。
- 配置邮箱和许可截止日期。

用户实体的 `Role` 属性配置了下拉属性编辑器，角色数据来自角色 Repository。

---

## 19. 角色管理 Presenter

注册：

```csharp
services.AddRoleViewPresenter();
```

它注册：

```text
IObservableSourceRepositoryBindable<hi_dd_role>
IRoleViewPresenter → RoleViewPresenter
```

角色管理负责：

- 角色名称和编码。
- 角色拥有的权限集合。
- 角色关联的用户集合。

角色权限更新后，已登录用户对象是否立即反映变化取决于当前 EF 实体和 Presenter 的生命周期。生产场景建议权限变更后重新加载当前用户，或要求重新登录。

---

## 20. 权限管理 Presenter

注册：

```csharp
services.AddAuthorityViewPresenter();
```

它注册：

```text
IObservableSourceRepositoryBindable<hi_dd_author>
IAuthorityViewPresenter → AuthorityViewPresenter
```

权限管理命令：

```csharp
ShowAuthorityViewCommand
```

权限项至少应维护：

```text
Name       用户可读名称
AuthorCode 稳定业务编码
```

推荐在代码中集中声明权限代码，在数据库种子或初始化服务中同步权限定义。

---

## 21. 注册服务：`IRegisterService`

默认 `IdentityRegisterService` 支持：

```csharp
bool Register(string mail, string account, string password, out string message);
bool ResetPassword(string mail, string account, string password, out string message);
```

注册流程：

```text
检查账号和密码非空
    ↓
检查邮箱是否已注册
    ↓
检查账号是否已存在
    ↓
验证账号格式
    ↓
验证密码格式
    ↓
根据 UseAdiminCheckOnRegister 设置 Enable
    ↓
设置 LicenseDeadline
    ↓
保存用户
    ↓
记录操作日志
```

注册配置：

```csharp
services.AddIdentityRegisterService(options =>
{
    options.UseAdiminCheckOnRegister = true;
    options.UserLicenseDefaultTryTime = TimeSpan.FromDays(30);
});
```

---

## 22. `IdentifyOptions`

主要配置：

| 属性 | 默认值 | 说明 |
|---|---|---|
| `UseAdiminCheckOnRegister` | `false` | 注册后是否需要管理员启用。 |
| `UseUserLicenseDeadTime` | `false` | 是否启用用户许可截止日期。 |
| `UserLicenseDefaultTryTime` | 30 天 | 注册用户默认许可时长。 |

使用：

```csharp
app.UseIdentifyOptions(options =>
{
    options.UseAdiminCheckOnRegister = true;
    options.UseUserLicenseDeadTime = true;
    options.UserLicenseDefaultTryTime = TimeSpan.FromDays(15);
});
```

### 22.1 当前实现限制

`IdentityRegisterService` 会设置：

```csharp
LicenseDeadline = DateTime.Now.Add(UserLicenseDefaultTryTime)
```

但当前 `IdentityLoginService.Login()` 没有检查：

```csharp
UseUserLicenseDeadTime
LicenseDeadline
```

因此仅把 `UseUserLicenseDeadTime` 设置为 `true`，当前并不会自动阻止过期用户登录。

若业务需要许可截止日期，应自定义 `ILoginService` 或扩展登录逻辑：

```csharp
if (options.UseUserLicenseDeadTime &&
    user.LicenseDeadline < DateTime.Now)
{
    message = "用户许可已过期";
    return false;
}
```

---

## 23. 自定义登录服务

实现接口：

```csharp
public class SecureIdentityLoginService : BindableBase, ILoginService
{
    private readonly IStringRepository<hi_dd_user> _users;
    private readonly IPasswordHasher _passwordHasher;

    public SecureIdentityLoginService(
        IStringRepository<hi_dd_user> users,
        IPasswordHasher passwordHasher)
    {
        _users = users;
        _passwordHasher = passwordHasher;
    }

    public IUser User { get; private set; }

    public bool Login(string name, string password, out string message)
    {
        var entity = _users.GetList(x => x.Account == name).FirstOrDefault();
        if (entity == null || !_passwordHasher.Verify(password, entity.Password))
        {
            message = "账号或密码错误";
            return false;
        }

        if (!entity.Enable)
        {
            message = "用户未启用";
            return false;
        }

        if (IdentifyOptions.Instance.UseUserLicenseDeadTime &&
            entity.LicenseDeadline < DateTime.Now)
        {
            message = "用户许可已过期";
            return false;
        }

        User = new User(entity);
        message = "登录成功";
        return true;
    }

    public bool Logout(out string message)
    {
        User = null;
        message = null;
        return true;
    }
}
```

注册时不要同时让默认实现覆盖自定义实现：

```csharp
services.AddSingleton<ILoginService, SecureIdentityLoginService>();
```

`AddIdentityLoginService()` 使用普通 `AddSingleton`，会新增默认注册。应选择一种实现，或在注册完成后使用 `Replace`：

```csharp
services.Replace(
    ServiceDescriptor.Singleton<ILoginService, SecureIdentityLoginService>());
```

---

## 24. 自定义用户源

身份契约与数据库实现分离，因此可以替换为：

- 企业 LDAP / Active Directory。
- OAuth / OpenID Connect 后端。
- Web API 身份服务。
- 本地加密文件。
- 自定义数据库。

只需实现：

```csharp
ILoginService
IRegisterService // 如果支持注册
IUser
IRole
IAuthor
```

登录页面依赖接口，不要求必须使用 `IdentifyDataContext`。

对于远程认证，建议：

- 服务端完成密码验证和授权。
- 客户端只保存短期令牌和必要用户信息。
- 不在 WPF 客户端数据库保存远程账号明文密码。
- 服务端再次验证每个敏感操作，不能只依赖客户端按钮状态。

---

## 25. 操作日志

默认身份服务在以下操作写日志：

- 登录。
- 退出登录。
- 用户注册。
- 重置密码。

调用方式：

```csharp
Ioc<IOperationService>.Instance?.Log<hi_dd_user>(
    "登录",
    "登录成功");
```

若未注册 `IOperationService`，空条件访问不会中断身份流程。

敏感信息日志规则：

- 不记录明文密码。
- 不记录完整令牌。
- 邮箱和账号根据业务合规要求脱敏。
- 记录用户 ID、操作、时间、结果和必要上下文。

---

## 26. 安全注意事项

### 26.1 当前密码为明文存储

当前实体直接保存：

```csharp
public string Password { get; set; }
```

默认登录直接比较字符串，注册和重置密码直接写入数据库。这不适合生产环境。

生产环境必须改为：

- 使用成熟密码哈希算法，如 PBKDF2、bcrypt、scrypt 或 Argon2。
- 每个密码使用随机 Salt。
- 使用恒定时间验证函数。
- 绝不解密或恢复原密码。

不要使用普通 SHA256/MD5 直接哈希密码。

### 26.2 默认管理员凭据

`AdminUser` 内置：

```text
Account: admin
Password: 123456
```

数据库种子也包含默认账号。生产部署前必须：

- 移除内置管理员快捷登录；或
- 首次启动强制修改密码；或
- 从安全配置/密钥服务读取初始化凭据；并
- 防止默认凭据长期存在。

### 26.3 默认密码比较忽略大小写

当前实现：

```csharp
user.Password.ToLower() != password.ToLower()
```

密码通常应区分大小写。自定义安全登录服务必须使用密码哈希验证，不能把密码转小写。

### 26.4 客户端授权不是安全边界

WPF 是客户端应用，用户可能修改本地文件、数据库或程序。涉及远程数据和高价值操作时，服务端必须重新认证和授权。

### 26.5 密码重置验证较弱

当前重置逻辑按邮箱和账号查找后直接设置新密码，不包含：

- 邮箱验证码。
- 原密码验证。
- 管理员审批。
- 一次性重置令牌。

生产环境应实现安全的密码重置流程。

---

## 27. 单元测试建议

登录成功：

```csharp
[Fact]
public void Login_SetsCurrentUser_WhenCredentialsAreValid()
{
    var service = CreateLoginServiceWithUser("tester", "hashed-password");

    bool result = service.Login("tester", "password", out string message);

    Assert.True(result);
    Assert.NotNull(service.User);
}
```

禁用用户：

```csharp
[Fact]
public void Login_Fails_WhenUserIsDisabled()
{
    var service = CreateLoginServiceWithDisabledUser();

    bool result = service.Login("tester", "password", out string message);

    Assert.False(result);
    Assert.Null(service.User);
}
```

权限检查：

```csharp
[Fact]
public void IsValid_ReturnsTrue_WhenRoleContainsAuthorityCode()
{
    IUser user = CreateUserWithAuthority("user.delete");

    bool result = user.IsValid("user.delete");

    Assert.True(result);
}
```

服务授权：

```csharp
[Fact]
public void Delete_Throws_WhenCurrentUserHasNoPermission()
{
    var service = CreateUserManagementServiceWithoutPermission();

    Assert.Throws<UnauthorizedAccessException>(() => service.Delete("user-id"));
}
```

应覆盖：

- 空账号和空密码。
- 不存在账号。
- 错误密码。
- 禁用用户。
- 许可过期。
- 登录与退出状态。
- 有权限和无权限。
- 管理员策略。
- 注册重复邮箱和账号。
- 密码重置验证。

---

## 28. 常见问题

### 应用启动时没有登录页面

检查：

1. 是否注册 `ILoginViewPresenter`。
2. 是否调用 `AddBackgroundRigisterLoginViewPresenter()` 或其他登录 Presenter 注册。
3. 应用是否继承 `ApplicationBase` / `IdentifyApplicationBase`。
4. 是否存在登录 Presenter 对应的 `DataTemplate`。

### 登录成功后 `IocLogin.User` 仍为空

检查：

- 登录页面是否调用了 IOC 中同一个 `ILoginService`。
- 是否重复注册多个 `ILoginService`。
- 是否在登录后立即调用了 `Logout()`。
- 自定义服务是否正确设置 `User`。

### 普通用户始终没有权限

检查：

1. 用户是否关联角色。
2. 角色是否加载 `Authors` 集合。
3. 权限是否设置了 `AuthorCode`。
4. `IsValid()` 是否传入 `AuthorCode`，而不是数据库 ID。
5. EF 导航属性是否正确加载。

### `IsInAuthor()` 和 `IsValid()` 结果不同

这是因为：

- `IsInAuthor()` 比较 `IAuthor.ID`。
- `Role.IsValid()` 比较 `hi_dd_author.AuthorCode`。

业务权限建议统一使用稳定 `AuthorCode` 和 `IsValid()`。

### 开启许可截止日期后仍能登录

当前默认 `IdentityLoginService` 未执行截止日期检查。需要替换或扩展 `ILoginService`。

### 用户修改角色后权限没有立即变化

当前登录用户可能仍持有旧的角色实体。重新加载当前用户、刷新导航属性或要求重新登录。

### 数据库创建后没有种子数据

检查：

- 上下文是否继承 `IdentifyDataContext<TContext>`，或是否调用 `BuildIdentifySeed()`。
- EF Core 迁移是否已应用。
- 当前连接字符串是否指向预期数据库。

---

## 29. 二次开发建议

- 简单桌面应用可使用 `IdentifyApplicationBase` 快速集成。
- 复杂应用使用手动注册或自定义 `ILoginService`。
- 权限判断统一使用稳定 `AuthorCode`，不要依赖数据库 ID。
- UI 通过 `CanExecute` 或 `Visibility` 提供权限反馈，Service 层再次强制授权。
- 生产环境必须替换明文密码存储和默认管理员凭据。
- `UseUserLicenseDeadTime` 当前不会自动拦截登录，必须补充登录检查。
- 密码重置必须增加可靠身份验证，不要只依赖账号和邮箱文本。
- 角色或权限改变后刷新当前用户，或要求重新登录。
- 远程业务必须在服务端再次认证和授权。
- 身份服务、授权服务和 Presenter 分离，便于单元测试及替换用户源。
- 使用操作日志记录结果和上下文，但绝不记录密码或完整令牌。
