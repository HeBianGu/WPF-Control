# WPF-Control 二次开发文档 如何配置登录页面的示例
# 登录页面二次开发文档

**适用模块：** `H.Modules.Login`  
**参考示例：** `Source/Tests/H.Test.Login`  
**框架：** `.NET 8.0-windows` + WPF

本文说明如何在应用中启用、配置和扩展登录页面。登录页面由 `H.Modules.Login` 提供，通常配合 `ApplicationBase`、设置模块、消息模块和主题样式使用。

---

## 1. 模块概览

`H.Modules.Login` 提供以下能力：

- 登录页面展示与登录流程控制。
- 注册页面展示与注册流程控制。
- 登录、注册、当前用户、登录按钮等 Presenter。
- 登录配置项持久化与设置页集成。
- 默认测试登录服务，便于快速验证登录流程。
- 与 `ApplicationBase` 的 `OnLogin`、`Login`、`Logout` 流程配合。

常用类型：

| 类型 | 说明 |
|---|---|
| `ILoginService` | 登录服务接口，负责登录、退出和当前用户状态。 |
| `IRegisterService` | 注册服务接口，负责注册逻辑。 |
| `ILoginOptions` | 登录页面配置接口。 |
| `IRegistorOptions` | 注册页面配置接口。 |
| `LoginOptions` | 登录页面配置实现，继承 `IocOptionInstance<LoginOptions>`。 |
| `RegistorOptions` | 注册页面配置实现，继承 `IocOptionInstance<RegistorOptions>`。 |
| `ILoginViewPresenter` | 登录页面 Presenter 接口。 |
| `ILoginButtonViewPresenter` | 登录按钮 Presenter，可嵌入主窗口。 |
| `ICurrentUserViewPresenter` | 当前用户信息 Presenter。 |
| `LoginCommand` | 显示登录页面命令。 |

---

## 2. 项目引用

参考 `H.Test.Login.csproj`，应用至少需要引用登录模块、应用基类、消息模块、设置模块、主题和样式。

```xml
<ItemGroup>
  <ProjectReference Include="..\..\Extensions\H.Extensions.ApplicationBase\H.Extensions.ApplicationBase.csproj" />
  <ProjectReference Include="..\..\Modules\H.Modules.Login\H.Modules.Login.csproj" />
  <ProjectReference Include="..\..\Modules\H.Modules.Messages.Dialog\H.Modules.Messages.Dialog.csproj" />
  <ProjectReference Include="..\..\Modules\H.Modules.Setting\H.Modules.Setting.csproj" />
  <ProjectReference Include="..\..\Styles\H.Style\H.Style.csproj" />
  <ProjectReference Include="..\..\Themes\H.Theme\H.Theme.csproj" />
</ItemGroup>
```

如果启用邮箱注册，还需要邮件扩展：

```xml
<ProjectReference Include="..\..\Extensions\H.Extensions.Mail\H.Extensions.Mail.csproj" />
```

---

## 3. 在 `App.xaml` 中合并主题和样式

`H.Test.Login` 使用 `ApplicationBase` 作为应用入口，并在资源字典中合并主题和样式。

```xaml
<h:ApplicationBase
    x:Class="H.Test.Login.App"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:h="https://github.com/HeBianGu">
    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <FontSizeTheme Type="Default" />
                <LayoutTheme Type="Default" />
                <ColorTheme Type="Dark" />
                <ConciseStyle />
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Application.Resources>
</h:ApplicationBase>
```

说明：

- `FontSizeTheme`：配置默认字号资源。
- `LayoutTheme`：配置默认布局尺寸资源。
- `ColorTheme`：配置颜色主题，示例中使用 `Dark`。
- `ConciseStyle`：加载默认控件样式。

---

## 4. 注册登录相关服务

在 `App.xaml.cs` 中继承 `ApplicationBase`，并在 `ConfigureServices` 注册登录页面、登录服务、注册服务、消息和设置。

```csharp
using H.Extensions.ApplicationBase;
using H.Extensions.Mail;
using H.Modules.Login;
using H.Modules.Setting;
using H.Services.Common;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace H.Test.Login;

public partial class App : ApplicationBase
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddSetting();
        services.AddWindowMessage();
        services.AddAdornerDialogMessage();

        // 登录页面 Presenter，按需选择一种。
        // services.AddLoginViewPresenter();
        // services.AddRegisterLoginViewPresenter();
        services.AddBackgroundRigisterLoginViewPresenter();

        // 示例登录/注册服务，真实项目建议替换为自己的实现。
        services.AddTestLoginService();
        services.AddTestRegistorService();

        // 启用邮箱注册时需要注册邮件服务。
        services.AddMail();
    }

    protected override Window CreateMainWindow(StartupEventArgs e)
    {
        return new MainWindow();
    }

    protected override void Configure(IApplicationBuilder app)
    {
        base.Configure(app);
        app.UseLoginOptions();
        app.UseRegistorOptions();
        app.UseMailOptions();
    }
}
```

---

## 5. 登录页面 Presenter 选择

`H.Modules.Login` 提供多种登录页面注册方法：

| 方法 | 说明 |
|---|---|
| `AddLoginViewPresenter()` | 标准登录页面。 |
| `AddBackgroundLoginViewPresenter()` | 带背景图的登录页面。 |
| `AddRegisterLoginViewPresenter()` | 标准登录 + 注册页面。 |
| `AddBackgroundRigisterLoginViewPresenter()` | 带背景图的登录 + 注册页面，`H.Test.Login` 使用该方式。 |

示例：

```csharp
protected override void ConfigureServices(IServiceCollection services)
{
    services.AddSetting();
    services.AddWindowMessage();
    services.AddAdornerDialogMessage();

    services.AddBackgroundRigisterLoginViewPresenter();
    services.AddTestLoginService();
    services.AddTestRegistorService();
}
```

> 注意：`Rigister` / `Registor` 是当前项目中已有类型和方法的拼写，使用时应保持与 API 名称一致。

---

## 6. 配置登录页面参数

`LoginOptions` 是登录页面配置入口。可通过两种方式配置：

1. 在服务注册时传入 `setupAction`。
2. 在 `Configure` 中调用 `UseLoginOptions` 配置运行时选项。

推荐在 `UseLoginOptions` 中配置最终显示参数：

```csharp
protected override void Configure(IApplicationBuilder app)
{
    base.Configure(app);

    app.UseLoginOptions(option =>
    {
        option.Product = "WPF-Control 示例系统";
        option.ProductFontSize = 42;
        option.AdminName = "admin";
        option.AdminPassword = "123456";
        option.LastUserName = "admin";
        option.LastPassword = "123456";
        option.Remember = true;
        option.UseVisitor = false;
        option.UseLogoutRestart = false;
        option.Background = "pack://application:,,,/H.Modules.Login;component/Assets/background.jpg";
    });

    app.UseRegistorOptions();
}
```

常用配置项：

| 配置项 | 默认值 | 说明 |
|---|---|---|
| `Product` | `ApplicationProvider.Product` | 登录页标题。 |
| `ProductFontSize` | `50` | 登录页标题字体大小。 |
| `AdminName` | `admin` | 默认管理员账号。 |
| `AdminPassword` | `123456` | 默认管理员密码。 |
| `LastUserName` | `admin` | 上次登录账号。 |
| `LastPassword` | `123456` | 上次登录密码。 |
| `Remember` | `true` | 是否记住密码。 |
| `UseVisitor` | `false` | 访客模式配置项；当前默认登录流程尚未使用该选项。 |
| `UseLogoutRestart` | `false` | 退出登录后是否重启应用。 |
| `Background` | 登录模块内置背景图 | 背景图资源地址。 |

---

## 7. 配置注册页面参数

如果使用 `AddRegisterLoginViewPresenter()` 或 `AddBackgroundRigisterLoginViewPresenter()`，建议同时启用注册配置：

```csharp
protected override void Configure(IApplicationBuilder app)
{
    base.Configure(app);

    app.UseRegistorOptions(option =>
    {
        option.UseMail = false;
        option.ServiceAgreementUri = "https://hebiangu.github.io/WPF-Control/ServiceAgreement.html";
        option.PrivacypolicyUri = "https://hebiangu.github.io/WPF-Control/PrivacyPolicy.html";
        option.MailAccount = "example@163.com";
        option.Image = "pack://application:,,,/H.Style;component/Assets/Logo.ico";
    });
}
```

常用配置项：

| 配置项 | 默认值 | 说明 |
|---|---|---|
| `UseMail` | `false` | 是否启用邮箱注册。 |
| `ServiceAgreementUri` | 官方服务协议地址 | 应用许可/服务协议链接。 |
| `PrivacypolicyUri` | 官方隐私策略地址 | 隐私策略链接。 |
| `MailAccount` | 默认邮箱账号 | 发送验证码的邮箱账号。 |
| `Image` | `H.Style` 内置 Logo | 注册页左侧图片。 |

启用邮箱注册时，还需要配置邮件服务：

```csharp
protected override void ConfigureServices(IServiceCollection services)
{
    services.AddMail();
}

protected override void Configure(IApplicationBuilder app)
{
    base.Configure(app);
    app.UseMailOptions(option =>
    {
        // 按邮件扩展的配置项填写 SMTP、账号、密码等。
    });
}
```

---

## 8. 在主窗口中显示登录入口和退出登录

`H.Test.Login` 的 `MainWindow.xaml` 展示了三个常见入口：

- `ShowSettingCommand`：打开设置页面，可查看登录配置项。
- `LogoutCommand`：退出登录。
- `ILoginButtonViewPresenter`：从 IOC 中解析登录按钮 Presenter。

```xaml
<Window
    x:Class="H.Test.Login.MainWindow"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:h="https://github.com/HeBianGu"
    Title="MainWindow"
    Width="800"
    Height="450"
    Style="{StaticResource {x:Static WindowKeys.Default}}">
    <UniformGrid>
        <FontIconButton
            Command="{ShowSettingCommand}"
            Style="{DynamicResource {x:Static FontIconButtonKeys.Command}}" />

        <Button
            Command="{LogoutCommand}"
            Content="退出登录" />

        <ContentControl Content="{Ioc Type={x:Type ILoginButtonViewPresenter}}" />
    </UniformGrid>
</Window>
```

使用 `ContentControl Content="{Ioc Type={x:Type ILoginButtonViewPresenter}}"` 可以直接把登录按钮嵌入标题栏、工具栏或主界面。

---

## 9. 替换为真实登录服务

`AddTestLoginService()` 注册的是示例登录服务，仅用于测试。真实项目应实现自己的 `ILoginService` 并注册到 IOC。

示例：

```csharp
using H.Modules.Login;
using H.Modules.Login.Base;

public class MyLoginService : BindableBase, ILoginService
{
    private IUser _user;

    public IUser User
    {
        get => _user;
        private set
        {
            _user = value;
            RaisePropertyChanged();
        }
    }

    public bool Login(string name, string password, out string message)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            message = "账号不能为空";
            return false;
        }

        if (string.IsNullOrWhiteSpace(password))
        {
            message = "密码不能为空";
            return false;
        }

        // TODO: 替换为数据库、Web API、LDAP 或本地配置校验。
        if (name == "admin" && password == "123456")
        {
            User = new TestUser(name, password, "管理员");
            message = "登录成功";
            return true;
        }

        message = "账号或密码错误";
        return false;
    }

    public bool Logout(out string message)
    {
        User = null;
        message = "已退出登录";
        return true;
    }
}
```

注册自定义服务：

```csharp
protected override void ConfigureServices(IServiceCollection services)
{
    services.AddBackgroundRigisterLoginViewPresenter();
    services.AddSingleton<ILoginService, MyLoginService>();
}
```

如果需要替换注册逻辑，实现 `IRegisterService`，并注册：

```csharp
services.AddSingleton<IRegisterService, MyRegisterService>();
```

---

## 10. 控制启动时登录流程

`ApplicationBase` 提供登录生命周期入口，常见扩展点：

```csharp
protected override void OnSplashScreen(StartupEventArgs e)
{
    base.OnSplashScreen(e);
}

protected override void OnLogin()
{
    base.OnLogin();
    // 登录前后需要执行的逻辑可放在这里。
}
```

主窗口内也可以通过命令触发登录/退出：

```xaml
<Button Command="{LogoutCommand}" Content="退出登录" />
<ContentControl Content="{Ioc Type={x:Type ILoginButtonViewPresenter}}" />
```

---

## 11. 完整最小配置示例

### `App.xaml`

```xaml
<h:ApplicationBase
    x:Class="MyApp.App"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:h="https://github.com/HeBianGu">
    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <FontSizeTheme Type="Default" />
                <LayoutTheme Type="Default" />
                <ColorTheme Type="Dark" />
                <ConciseStyle />
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Application.Resources>
</h:ApplicationBase>
```

### `App.xaml.cs`

```csharp
using H.Extensions.ApplicationBase;
using H.Modules.Login;
using H.Modules.Setting;
using H.Services.Common;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace MyApp;

public partial class App : ApplicationBase
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddSetting();
        services.AddWindowMessage();
        services.AddAdornerDialogMessage();
        services.AddBackgroundRigisterLoginViewPresenter();
        services.AddTestLoginService();
        services.AddTestRegistorService();
    }

    protected override void Configure(IApplicationBuilder app)
    {
        base.Configure(app);
        app.UseLoginOptions(option =>
        {
            option.Product = "MyApp";
            option.AdminName = "admin";
            option.AdminPassword = "123456";
            option.Remember = true;
        });
        app.UseRegistorOptions();
    }

    protected override Window CreateMainWindow(StartupEventArgs e)
    {
        return new MainWindow();
    }
}
```

### `MainWindow.xaml`

```xaml
<Window
    x:Class="MyApp.MainWindow"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:h="https://github.com/HeBianGu"
    Width="800"
    Height="450"
    Style="{StaticResource {x:Static WindowKeys.Default}}">
    <StackPanel>
        <ContentControl Content="{Ioc Type={x:Type ILoginButtonViewPresenter}}" />
        <Button Command="{LogoutCommand}" Content="退出登录" />
    </StackPanel>
</Window>
```

---

## 12. 常见问题

### 登录页面不显示

检查：

1. `App` 是否继承 `ApplicationBase`。
2. 是否注册了 `AddLoginViewPresenter()`、`AddRegisterLoginViewPresenter()` 或 `AddBackgroundRigisterLoginViewPresenter()`。
3. 是否注册了 `ILoginService`，例如 `AddTestLoginService()` 或自定义服务。
4. 是否调用了 `base.Configure(app)`。

### 登录按钮无法显示

检查是否注册了登录 Presenter。`AddLoginViewPresenter()`、`AddRegisterLoginViewPresenter()` 等方法内部会注册 `ILoginButtonViewPresenter`。

### 设置页看不到登录配置

检查是否注册并启用了设置：

```csharp
services.AddSetting();
app.UseLoginOptions();
app.UseRegistorOptions();
```

### 注册页面邮箱功能不可用

检查：

1. 是否使用 `AddRegisterLoginViewPresenter()` 或 `AddBackgroundRigisterLoginViewPresenter()`。
2. 是否设置 `RegistorOptions.UseMail = true`。
3. 是否注册 `services.AddMail()` 并调用 `app.UseMailOptions()`。
4. 邮件服务的 SMTP、账号和授权码是否配置正确。

### 退出登录后行为不符合预期

检查 `LoginOptions.UseLogoutRestart`：

- `false`：退出登录后只清空当前用户。
- `true`：退出登录后重启应用。

---

## 13. Login 与 Identity 的职责边界

`H.Modules.Login` 主要负责 UI 和交互编排：

- 登录、注册、邮箱验证和忘记密码页面。
- 表单状态、校验消息和 Busy 状态。
- 调用登录与注册服务。
- 登录、退出和切换用户命令。
- 登录成功后的用户数据加载页面。

真正的认证和持久化由接口实现负责：

```text
H.Modules.Login
登录/注册 Presenter 和 DataTemplate
        ↓
ILoginService / IRegisterService
        ↓
H.Modules.Identity、自定义数据库、Web API、LDAP 或 OAuth
```

因此注册登录 Presenter 后还必须注册 `ILoginService`。使用注册或忘记密码功能时，还必须注册 `IRegisterService`。

生产项目可以使用：

```csharp
services.AddIdentifyDefaultServices();
```

也可以仅注册 Login UI，然后提供自己的服务：

```csharp
services.AddBackgroundRigisterLoginViewPresenter();
services.AddSingleton<ILoginService, ApiLoginService>();
services.AddSingleton<IRegisterService, ApiRegisterService>();
```

---

## 14. 登录 Presenter 的状态与执行流程

`LoginViewPresenter` 继承 `DisplayBindableBase`，并实现：

```csharp
ILoginViewPresenter
IWindowInitable
```

主要状态：

```csharp
string UserName
string Password
bool ShowPassword
string Error
InvokeCommand LoginCommand
```

构造时从 `LoginOptions` 恢复上次登录信息：

```csharp
this.UserName = options.Value.LastUserName;
this.Password = options.Value.LastPassword;
```

登录流程：

```text
点击登录
    ↓
ProgressButton 进入 Busy 状态
    ↓
后台调用 ILoginService.Login
    ↓
失败：显示服务返回的 message
    ↓
成功：根据 Remember 保存登录信息
    ↓
保存 LoginOptions
    ↓
调用 IDialog.Sumit()
    ↓
关闭登录窗口并继续启动
```

核心服务调用：

```csharp
bool result = Ioc<ILoginService>.Instance.Login(
    this.UserName,
    this.Password,
    out string message);
```

登录服务可以来自本地数据库、远程 API 或企业身份系统，Presenter 不依赖具体存储实现。

### 14.1 `IDialog` 命令参数

默认模板把当前对话框传给命令：

```xaml
<ProgressButton
    Command="{Binding LoginCommand}"
    CommandParameter="{Binding RelativeSource={RelativeSource AncestorType={x:Type IDialog}}}"
    Content="登录" />
```

`LoginCommand` 只有在参数是 `IDialog` 时，才会在成功后调用：

```csharp
dialog.Sumit();
```

自定义模板如果遗漏该参数，可能出现登录服务已经成功，但窗口没有关闭的问题。

### 14.2 密码绑定

WPF `PasswordBox.Password` 不是普通依赖属性，框架使用行为实现双向绑定：

```xaml
<PasswordBox>
    <b:Interaction.Behaviors>
        <PasswordBindingBehavior Password="{Binding Password, Mode=TwoWay}" />
    </b:Interaction.Behaviors>
</PasswordBox>
```

自定义模板应继续使用该行为，或自行实现安全的密码输入传递方式。

---

## 15. 登录注册组合页的页面状态

`RigisterLoginViewPresenter` 继承 `LoginViewPresenter`，增加：

```csharp
int SelectedIndex
MailVerify MailVerify
Registor Registor
Forget Forget
```

默认模板通过 `SelectedIndex` 切换页面：

| Index | 页面 |
|---|---|
| `0` | 用户登录。 |
| `1` | 邮箱验证。 |
| `2` | 用户注册。 |
| `3` | 修改密码。 |

注册流程：

```text
登录页
    ↓ 立即注册
UseMail = true  → 邮箱验证 → 注册页
UseMail = false → 注册页
    ↓
IRegisterService.Register
    ↓
成功后回填账号和密码并返回登录页
```

忘记密码流程：

```text
登录页
    ↓ 忘记密码
邮箱验证/修改密码
    ↓
IRegisterService.ResetPassword
    ↓
成功后回填新密码并返回登录页
```

`Clear()` 会清理验证码、协议勾选状态以及注册和重置密码输入。

---

## 16. 注册和重置密码数据校验

### 16.1 `Registor`

`Registor.Valid()` 检查：

- 账号不能为空。
- 密码不能为空。
- 两次输入的密码必须一致。

注册调用：

```csharp
Ioc<IRegisterService>.Instance.Register(
    this.MailVerify.Mail,
    this.Registor.UserName,
    this.Registor.Password,
    out string message);
```

### 16.2 `Forget`

`Forget.Valid()` 检查：

- 新密码不能为空。
- 两次输入的新密码必须一致。

重置调用：

```csharp
Ioc<IRegisterService>.Instance.ResetPassword(
    this.MailVerify.Mail,
    this.UserName,
    this.Forget.Password,
    out string message);
```

Presenter 校验只用于改善交互。账号格式、密码强度、重复账号、验证码和身份凭证必须由可信服务再次校验。

---

## 17. 邮箱验证流程和当前限制

`MailVerify` 实现 `IDataErrorInfo`，包含：

```csharp
string Mail
string VerifyCode
string InputCode
bool Agree
string Message
```

它检查：

1. 邮箱格式正确。
2. 输入验证码与保存的验证码一致。
3. 验证码非空。
4. 用户已勾选服务协议和隐私政策。

启用邮件功能需要：

```csharp
services.AddMail();
app.UseMailOptions();
app.UseRegistorOptions(options => options.UseMail = true);
```

### 17.1 当前验证码代码需要修正

当前 `GetVerifyCodeCommand` 生成：

```csharp
_code = Random.Shared.Next(100000, 999999);
```

但没有把 `_code` 赋给：

```csharp
MailVerify.VerifyCode
```

同时当前邮件设置为：

```csharp
mailMessageItem.From = this.MailVerify.Mail;
mailMessageItem.To = new[] { RegistorOptions.Instance.MailAccount };
```

这与通常“系统邮箱向用户邮箱发送验证码”的方向相反。生产使用前应修正为类似：

```csharp
string code = RandomNumberGenerator
    .GetInt32(100000, 1000000)
    .ToString();

this.MailVerify.VerifyCode = code;

var mail = new MailMessageItem
{
    From = RegistorOptions.Instance.MailAccount,
    To = new[] { this.MailVerify.Mail },
    Subject = "注册验证码",
    Body = code
};
```

更可靠的生产方案应在服务端生成和验证验证码，并实现：

- 验证码有效期。
- 发送频率限制。
- 验证尝试次数限制。
- 一次性使用。
- 邮箱、账号和操作类型绑定。
- 验证码哈希存储。

---

## 18. 记住密码的安全边界

登录成功后默认逻辑：

```csharp
if (LoginOptions.Instance.Remember)
{
    LoginOptions.Instance.LastUserName = this.UserName;
    LoginOptions.Instance.LastPassword = this.Password;
}
else
{
    LoginOptions.Instance.LastUserName = null;
    LoginOptions.Instance.LastPassword = null;
}

LoginOptions.Instance.Save(out message);
```

`LastPassword` 当前没有 JSON/XML 忽略或加密处理，因此可能被写入本地设置文件。

生产环境建议：

- 默认关闭 `Remember`。
- 不保存原始密码。
- 保存短期刷新令牌或凭据引用。
- 使用 Windows Credential Manager、DPAPI 或企业凭据服务。
- 为令牌设置有效期、撤销和设备绑定策略。

`AddTestLoginService()` 使用固定的 `admin / 123456`，`AddTestRegistorService()` 直接返回成功；二者都不能用于生产。

---

## 19. `UseVisitor` 当前未接入默认流程

`LoginOptions.UseVisitor` 的描述表示登录失败时允许进入主窗口，但当前：

- `LoginViewPresenter.LoginCommand` 未读取该属性。
- `ApplicationBase.OnLogin()` 未读取该属性。
- 登录失败不会自动提交登录对话框。

因此仅设置：

```csharp
options.UseVisitor = true;
```

不会启用访客模式。需要自定义 Presenter 或登录流程，并为访客创建权限受限的 `IUser`，不能简单绕过所有授权检查。

---

## 20. 登录后加载用户数据

注册登录 Presenter 时会自动注册：

```text
ILoginedSplashViewPresenter → LoginedSplashViewPresenter
```

登录成功后，`ApplicationBase.OnLogin()` 会：

1. 加载登录用户相关设置。
2. 显示登录后加载 Presenter。
3. 执行所有 `ILoginedSplashLoadable`。
4. 成功后继续显示主窗口。

开发用户级加载服务：

```csharp
public class UserProfileLoadService : ILoginedSplashLoadable
{
    private readonly ILoginService _loginService;
    private readonly IUserProfileService _profileService;

    public string Name => "用户配置";

    public bool Load(out string message)
    {
        if (_loginService.User == null)
        {
            message = "当前用户为空";
            return false;
        }

        return _profileService.Load(
            _loginService.User.ID,
            out message);
    }
}
```

注册：

```csharp
services.AddSingleton<ILoginedSplashLoadable, UserProfileLoadService>();
```

适合加载用户设置、工作区、权限缓存、最近项目和个性化菜单。

---

## 21. 登录、退出与切换用户命令

### `LoginCommand`

当前用户为空时可执行，并通过 `ILoginableApplication.Login()` 再次打开登录流程。

### `LogoutCommand`

当前用户非空时可执行：

```csharp
this.Service.Logout(out string message);

if (LoginOptions.Instance.UseLogoutRestart)
    Application.Current.Restart();
```

### `SwitchUserCommand`

当前用户非空时可执行，再次调用应用登录流程。当前命令本身没有先调用 `Logout()`。

如果应用包含用户级缓存、数据库上下文、订阅或临时文件，应在退出或切换用户时明确清理旧用户资源。简单方案是启用：

```csharp
LoginOptions.Instance.UseLogoutRestart = true;
```

复杂应用应实现专门的会话生命周期服务，而不是仅依赖重启。

---

## 22. 自定义登录模板注意事项

Login 使用 Presenter 与隐式 `DataTemplate`。只修改外观时，优先覆盖模板，无需复制 Presenter。

自定义模板至少应保留：

- `UserName` 绑定。
- `PasswordBindingBehavior`。
- `LoginCommand`。
- 当前 `IDialog` 命令参数。
- `Error` 或 Busy 消息显示。

示例：

```xaml
<DataTemplate DataType="{x:Type login:LoginViewPresenter}">
    <Border Width="420" Padding="32" Background="#FF16243A">
        <StackPanel>
            <TextBlock
                HorizontalAlignment="Center"
                FontSize="32"
                Foreground="White"
                Text="{Binding Source={x:Static login:LoginOptions.Instance}, Path=Product}" />

            <TextBox
                Margin="0,24,0,8"
                Text="{Binding UserName, UpdateSourceTrigger=PropertyChanged}" />

            <PasswordBox>
                <b:Interaction.Behaviors>
                    <h:PasswordBindingBehavior Password="{Binding Password, Mode=TwoWay}" />
                </b:Interaction.Behaviors>
            </PasswordBox>

            <TextBlock
                Margin="0,8,0,0"
                Foreground="OrangeRed"
                Text="{Binding Error}" />

            <h:ProgressButton
                Margin="0,20,0,0"
                Command="{Binding LoginCommand}"
                CommandParameter="{Binding RelativeSource={RelativeSource AncestorType={x:Type h:IDialog}}}"
                Content="登录" />
        </StackPanel>
    </Border>
</DataTemplate>
```

---

## 23. 线程与异步注意事项

默认 Presenter 在 `Task.Run` 中调用同步服务接口：

```csharp
ILoginService.Login(...)
IRegisterService.Register(...)
IRegisterService.ResetPassword(...)
```

服务实现应注意：

- 不要从后台线程访问 WPF 控件或 `DispatcherObject`。
- EF Core `DbContext` 的生命周期和线程使用必须安全。
- 不要用 `.Result` 等待依赖 UI 上下文的异步方法。
- 远程请求应设置超时、取消和错误映射。
- 默认代码中的 `Thread.Sleep` 主要用于展示 Busy 状态，生产环境可缩短或移除。

如果使用真正的异步远程认证，建议定义异步身份服务并自定义 Presenter，避免用同步接口包装复杂异步调用。

---

## 24. 安全开发清单

- 不使用 `AddTestLoginService()` 和 `AddTestRegistorService()` 部署生产环境。
- 不持久化明文密码。
- 密码使用 PBKDF2、bcrypt、scrypt 或 Argon2 等成熟方案哈希。
- 登录失败消息避免暴露账号是否存在。
- 增加失败次数限制、延迟和锁定策略。
- 邮箱验证码使用加密安全随机数并设置有效期。
- 密码重置必须验证一次性令牌、验证码、原密码或管理员审批。
- 客户端只负责交互，远程敏感操作必须由服务端再次认证和授权。
- 切换用户时清理旧用户缓存、订阅和资源。
- 日志中不记录密码、验证码或完整令牌。

---

## 25. 补充排查项

### 登录成功但窗口不关闭

检查自定义模板是否把当前 `IDialog` 传给 `LoginCommand`。

### 设置 `UseVisitor=true` 仍无法进入

当前选项未接入默认流程，需要自定义实现。

### 邮箱格式正确但验证码始终失败

检查生成验证码是否写入 `MailVerify.VerifyCode`，以及邮件的 `From`、`To` 是否设置正确。

### 切换用户后仍显示旧数据

清理用户级缓存，重新执行用户加载服务，或采用退出后重启策略。

### 登录后加载失败导致应用退出

检查 `ILoginedSplashLoadable.Load()` 返回值和消息。非关键任务应采用降级策略，不应返回 `false` 阻止应用启动。

