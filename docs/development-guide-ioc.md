# IOC 依赖注入二次开发文档
# IOC 依赖注入二次开发文档

**适用项目：** `H.Iocable`、`H.Extensions.ApplicationBase`、`H.Services.*`  
**核心类型：** `Ioc`、`Ioc<T>`、`IocExtension`、`IocBindable`、`DbIoc`  
**底层容器：** `Microsoft.Extensions.DependencyInjection`

本文说明 WPF-Control 中 IOC 依赖注入的注册、解析、XAML 使用、服务定位器封装、数据库独立容器，以及解决方案中常见 `IService` 接口的职责分布。

---

## 1. IOC 模块定位

`H.Iocable` 是对 `Microsoft.Extensions.DependencyInjection` 的轻量封装，主要作用：

- 保存全局 `IServiceProvider`。
- 提供静态服务定位器 `Ioc`。
- 提供强类型访问器 `Ioc<T>` / `Ioc<T, Interface>`。
- 提供 XAML 标记扩展 `IocExtension`。
- 提供可绑定基类 `IocBindable`。
- 支持按接口类型枚举已注册服务，例如启动加载、退出释放、保存配置等扩展点。

容器构建通常由 `ApplicationBase` 完成：

```csharp
ServiceCollection sc = new ServiceCollection();
this.ConfigureServices(sc);
Ioc.Build(sc);
```

因此，应用二次开发时，服务应优先在派生 `ApplicationBase` 的 `ConfigureServices` 中注册。

---

## 2. IOC 初始化流程

`ApplicationBase` 构造阶段会调用 `InitServiceCollection()`：

```csharp
protected void InitServiceCollection()
{
    ServiceCollection sc = new ServiceCollection();
    this.ConfigureServices(sc);
    Ioc.Build(sc);
    Ioc.GetService<ILoadGlobalizationOptionsService>(false)?.Load(out string message);
}
```

执行顺序：

1. 创建 `ServiceCollection`。
2. 调用派生类 `ConfigureServices(IServiceCollection services)`。
3. 调用 `Ioc.Build(sc)` 构建全局 `IServiceProvider`。
4. 尝试加载全球化配置。

最小注册示例：

```csharp
public partial class App : ApplicationBase
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IMyService, MyService>();
    }

    protected override Window CreateMainWindow(StartupEventArgs e)
    {
        return new MainWindow();
    }
}
```

---

## 3. `Ioc` 静态服务定位器

核心实现位于 `Source/Providers/H.Iocable/Ioc.cs`。

### 3.1 `Build`

```csharp
public static void Build(IServiceCollection serviceCollection)
{
    _services = serviceCollection.BuildServiceProvider();
    _serviceCollection = serviceCollection;
}
```

作用：

- 构建 `IServiceProvider`。
- 保存原始 `IServiceCollection`，供 `FindAll`、`GetAssignableFromServices` 查询。

### 3.2 `Services`

```csharp
public static IServiceProvider Services => _services;
```

用于直接访问底层容器：

```csharp
var service = Ioc.Services.GetService<IMyService>();
```

通常更推荐使用 `Ioc.GetService<T>()`，因为它提供统一的缺失服务提示。

### 3.3 `GetService<T>`

```csharp
public static T GetService<T>(bool throwIfNone = true)
```

默认 `throwIfNone = true`，服务不存在时抛出异常：

```text
此接口为依赖注入接口，请先在ApplicationBase中注册<T>服务
```

使用示例：

```csharp
var logger = Ioc.GetService<ILogService>();
```

可选获取：

```csharp
var logger = Ioc.GetService<ILogService>(false);
if (logger != null)
{
    logger.Info("message");
}
```

### 3.4 `GetService<T>(Type type)`

```csharp
public static T GetService<T>(Type type, bool throwIfNone = true)
```

用于动态类型解析，`IocExtension` 就是通过该方法实现：

```csharp
return Ioc.GetService<object>(this.Type);
```

### 3.5 `Exist<T>`

```csharp
public static bool Exist<T>()
```

用于判断服务是否存在：

```csharp
if (Ioc.Exist<ILoginService>())
{
    // 已启用登录模块
}
```

### 3.6 `FindAll`

```csharp
public static IEnumerable<ServiceDescriptor> FindAll(Func<ServiceDescriptor, bool> predicate = null)
```

用于查看已注册的服务描述符：

```csharp
var allSingletons = Ioc.FindAll(x => x.Lifetime == ServiceLifetime.Singleton);
```

### 3.7 `GetAssignableFromServices<T>`

```csharp
public static IEnumerable<T> GetAssignableFromServices<T>(Func<T, bool> predicate = null)
```

用于扫描所有注册项，找到可赋值给 `T` 的服务实例。`ApplicationBase` 使用它加载扩展点，例如：

- `ISplashLoadable`
- `ILoginedSplashLoadable`
- `IMainWindowLoadedLoadable`
- `IAppExitService`

示例：

```csharp
var loads = Ioc.GetAssignableFromServices<ISplashLoadable>().Distinct();
foreach (var load in loads)
{
    load.Load(out string message);
}
```

---

## 4. 强类型访问器

### 4.1 `Ioc<Interface>`

```csharp
public abstract class Ioc<Interface>
{
    public static Interface Instance => Ioc.GetService<Interface>(false);
}
```

服务不存在时返回 `default`，不会抛异常。

使用示例：

```csharp
Ioc<ILoginService>.Instance?.Logout(out string message);
```

### 4.2 `Ioc<T, Interface>`

```csharp
public abstract class Ioc<T, Interface> where T : class, Interface
{
    public static T Instance => Ioc.GetService<Interface>() as T;
}
```

适合在具体服务类中定义强类型单例入口。

示例：

```csharp
public interface IThemeService
{
}

public class ThemeService : IThemeService
{
}

public abstract class IocThemeService : Ioc<ThemeService, IThemeService>
{
}
```

使用：

```csharp
var themeService = IocThemeService.Instance;
```

### 4.3 `IocThrowIfNone<T>` / `ThrowIfNoneIoc<T>`

两个类型都提供强制获取服务的访问方式：

```csharp
public static Interface Instance => Ioc.GetService<Interface>();
```

适合服务必须存在的场景。如果服务未注册，会立即抛出异常。

---

## 5. XAML 中使用 `IocExtension`

`IocExtension` 继承 `MarkupExtension`，允许在 XAML 中从 IOC 容器解析对象。

源码：

```csharp
public class IocExtension : MarkupExtension
{
    public Type Type { get; set; }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return Ioc.GetService<object>(this.Type);
    }
}
```

使用示例：

```xaml
<ContentControl Content="{Ioc Type={x:Type ILoginButtonViewPresenter}}" />
```

常见用途：

- 在主窗口标题栏显示登录按钮。
- 在界面中嵌入模块 Presenter。
- 从 IOC 中获取单例 ViewModel 或服务型 Presenter。

注意事项：

- XAML 加载时对应服务必须已经注册。
- 如果服务未注册，默认会抛出异常。
- 适合 UI 组合，不建议在复杂业务逻辑中大量使用服务定位器。

---

## 6. `IocBindable`

`IocBindable<T, Interface>` 继承 `Ioc<T, Interface>` 并实现 `INotifyPropertyChanged`：

```csharp
public abstract class IocBindable<T, Interface> : Ioc<T, Interface>, INotifyPropertyChanged
    where T : class, Interface, new()
{
    public event PropertyChangedEventHandler PropertyChanged;

    public virtual void RaisePropertyChanged(string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
```

适合需要：

- IOC 强类型单例访问。
- 简单属性变更通知。
- 设置项、服务状态、全局可绑定对象。

---

## 7. `DbIoc` 数据库独立容器

`DbIoc` 位于 `H.Services.Common`，用于数据库相关服务的独立容器。

```csharp
public static class DbIoc
{
    public static IServiceProvider Services => _services;

    public static void ConfigureServices(Action<IServiceCollection> action)
    {
        _sc = new ServiceCollection();
        action?.Invoke(_sc);
        _services = _sc.BuildServiceProvider();
    }

    public static void Rebuild()
    {
        _services = _sc.BuildServiceProvider();
    }

    public static T GetService<T>(bool throwIfNone = true)
    {
        T r = (T)_services?.GetService(typeof(T));
        return r == null && throwIfNone ? Ioc.GetService<T>(throwIfNone) : r;
    }
}
```

特点：

- 与主 `Ioc` 容器隔离。
- 可通过 `Rebuild()` 重建数据库服务容器。
- 获取不到服务时可回退到主 IOC。

适用场景：

- 数据库连接字符串运行时切换。
- 不同项目或用户使用不同 DbContext。
- 数据库相关服务需要单独重建，不影响主应用容器。

---

## 8. 服务注册规范

### 8.1 生命周期建议

| 生命周期 | 用法 | 示例 |
|---|---|---|
| `AddSingleton` | 全局服务、模块 Presenter、设置服务、状态服务。 | `ILoginService`、`ILogService` |
| `AddTransient` | 轻量无状态对象，每次使用新实例。 | 临时任务、短生命周期处理器 |
| `AddScoped` | WPF 桌面应用较少使用，除非自行创建 Scope。 | Web 风格业务单元 |
| `TryAdd` / `TryAddSingleton` | 模块默认实现，允许应用覆盖。 | 模块扩展方法内部 |

模块扩展方法中建议使用 `TryAdd`：

```csharp
services.TryAdd(ServiceDescriptor.Singleton<ILoginService, LoginService>());
```

应用层需要覆盖时，先注册自定义实现或避免调用默认注册方法。

### 8.2 扩展方法注册

推荐为模块提供 `AddXxx` 扩展方法：

```csharp
public static IServiceCollection AddMyService(this IServiceCollection services)
{
    services.TryAdd(ServiceDescriptor.Singleton<IMyService, MyService>());
    return services;
}
```

在应用中使用：

```csharp
protected override void ConfigureServices(IServiceCollection services)
{
    services.AddMyService();
}
```

### 8.3 Options 配置模式

项目中常见模式：

```csharp
public static IServiceCollection AddMyModule(this IServiceCollection services, Action<IMyOptions> setupAction = null)
{
    services.AddOptions();
    services.TryAdd(ServiceDescriptor.Singleton<IMyService, MyService>());
    if (setupAction != null)
        services.Configure(new Action<MyOptions>(setupAction));
    return services;
}
```

配置管线中再通过 `UseXxxOptions` 把配置项加入设置系统：

```csharp
public static IApplicationBuilder UseMyOptions(this IApplicationBuilder builder, Action<IMyOptions> option = null)
{
    IocSetting.Instance.Add(MyOptions.Instance);
    option?.Invoke(MyOptions.Instance);
    return builder;
}
```

---

## 9. 基础使用示例

参考 `Source/Tests/H.Test.Ioc`：

```csharp
public interface ITest
{
}

public class MyTest : ITest
{
}

public partial class App : ApplicationBase
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<ITest, MyTest>();
    }
}
```

在代码中解析：

```csharp
var test = Ioc.Services.GetService<ITest>();
```

或：

```csharp
var test = Ioc.GetService<ITest>();
```

---

## 10. 解决方案中的 `IService` 接口清单

以下清单按目录归类，列出当前解决方案中以 `I...Service` 或历史拼写 `I...Servce` 命名的主要服务接口。

### 10.1 `Source/Services` 服务层

#### `H.Services.AppPath`

| 接口 | 说明 |
|---|---|
| `IAppPathServce` | 系统路径服务，提供应用级、用户级目录。 |

#### `H.Services.Common`

| 接口 | 说明 |
|---|---|
| `ICryptService` | 加解密服务。 |
| `IDbConnectService` | 数据库连接服务。 |
| `IDbDisconnectService` | 数据库断开服务。 |
| `IExcelService` | Excel 导入导出或处理服务。 |
| `IGuideService` | 向导服务抽象。 |
| `ILoadGlobalizationOptionsService` | 全球化配置加载服务。 |
| `IMainWindowSavableService` | 主窗口状态加载和保存服务。 |
| `IScheduledTaskService` | 计划任务服务。 |
| `IMetaSettingService` | 元数据设置服务。 |
| `IAppExitService` | 应用退出释放服务。 |
| `IAppLoadService` | 应用启动加载服务。 |
| `IAppSaveService` | 应用保存服务。 |
| `ILicenseLoadService` | 许可证启动加载服务。 |
| `ILoadThemeOptionsService` | 主题配置加载服务。 |
| `IUpgradeService` | 升级服务。 |

#### `H.Services.Identity`

| 接口 | 说明 |
|---|---|
| `ILoginService` | 登录、退出和当前用户服务。 |
| `IRegisterService` | 注册服务。 |

#### `H.Services.Logger`

| 接口 | 说明 |
|---|---|
| `ILogService` | 日志写入服务。 |
| `IAppLogService` | 应用日志服务。 |

#### `H.Services.Mail`

| 接口 | 说明 |
|---|---|
| `IMailService` | 邮件发送服务。 |
| `ILogMailService` | 日志邮件服务。 |

#### `H.Services.Message`

| 接口 | 说明 |
|---|---|
| `IDialogMessageService` | 对话框消息服务。 |
| `IAdornerDialogMessageService` | Adorner 形式对话框消息服务。 |
| `IWindowDialogMessageService` | Window 形式对话框消息服务。 |
| `IFormMessageService` | 表单消息服务。 |
| `IIOFileDialogService` | 文件选择对话框服务。 |
| `IIOFolderDialogService` | 文件夹选择对话框服务。 |
| `INoticeMessageService` | 通知消息服务。 |
| `ISnackMessageService` | Snack 消息服务。 |

#### `H.Services.Operation`

| 接口 | 说明 |
|---|---|
| `IOperationService` | 操作日志或操作记录服务。 |

#### `H.Services.Project`

| 接口 | 说明 |
|---|---|
| `IProjectService` | 项目管理服务。 |
| `IProjectDialogService` | 项目相关对话框服务。 |
| `IProjectSplashSaveService` | 项目启动/退出保存服务。 |

#### `H.Services.Revertible`

| 接口 | 说明 |
|---|---|
| `IRevertibleService` | 撤销/恢复服务。包含非泛型和泛型版本。 |

#### `H.Services.Serializable`

| 接口 | 说明 |
|---|---|
| `ICloneService` | 克隆服务。 |
| `ISerializerService` | 序列化服务基础接口。 |
| `IJsonSerializerService` | JSON 序列化服务。 |
| `IXmlSerializerService` | XML 序列化服务。 |
| `IWebJsonSerializerService` | Web JSON 序列化服务。 |
| `IWebXmlSerializerService` | Web XML 序列化服务。 |

#### `H.Services.Setting`

| 接口 | 说明 |
|---|---|
| `ISettingDataService` | 设置数据加载、保存服务。 |

### 10.2 `Source/Modules` 模块层

| 接口 | 所在模块 | 说明 |
|---|---|---|
| `IFeedBackMailService` | `H.Modules.Feedback` | 反馈邮件服务。 |
| `IContactService` | `H.Modules.Help` | 联系方式服务。 |
| `IReleaseVersionsService` | `H.Modules.Help` | 发布版本信息服务。 |
| `IShowHelpService` | `H.Modules.Help` | 帮助展示服务。 |
| `ISupportService` | `H.Modules.Help` | 技术支持服务。 |
| `IWebsiteService` | `H.Modules.Help` | 网站跳转或官网服务。 |
| `ILicenseService` | `H.Modules.License` | 许可证业务服务。 |

### 10.3 `Source/Controls` 控件层

| 接口 | 所在项目 | 说明 |
|---|---|---|
| `IColorBoxService` | `H.Controls.ColorBox` | 颜色选择服务。 |
| `IFavoriteService` | `H.Controls.FavoriteBox` | 收藏服务。 |
| `ITagService` | `H.Controls.TagBox` | 标签服务，包含非泛型和泛型版本。 |
| `IService` | `H.Controls.Chart2D` | 图表服务基础接口。 |
| `IService` | `H.Controls.Chart2D.Presenter` | 图表 Presenter 服务基础接口。 |
| `IService` | `H.Controls.Vlc` | VLC 播放相关服务基础接口。 |

### 10.4 `Source/Extensions` 扩展层

| 接口 | 所在项目 | 说明 |
|---|---|---|
| `IFFMpegService` | `H.Extensions.FFMpeg` | FFMpeg 服务。 |
| `IHttpService` | `H.Extensions.Http` | HTTP 请求服务。 |
| `ITorrentService` | `H.Extensions.Torrent` | Torrent 服务。 |
| `ITypeLicenseService` | `H.Extensions.TypeLicense` | 类型许可证服务。 |
| `IZipSaveFileDialogService` | `H.Extensions.Zip` | Zip 保存文件对话框服务。 |

### 10.5 `Source/Windows` 窗口层

| 接口 | 所在项目 | 说明 |
|---|---|---|
| `IService` | `H.Windows.Ribbon` | Ribbon 窗口服务基础接口。 |

---

## 11. 按功能分类的服务速查

### 启动、退出和保存

- `IAppLoadService`
- `ISplashLoadable`
- `ILoginedSplashLoadable`
- `IAppSaveService`
- `ISplashSave`
- `IAppExitService`
- `IMainWindowSavableService`
- `IScheduledTaskService`

### 设置、主题和全球化

- `ISettingDataService`
- `ILoadThemeOptionsService`
- `ILoadGlobalizationOptionsService`
- `IMetaSettingService`

### 用户和权限

- `ILoginService`
- `IRegisterService`

### 消息和对话框

- `IDialogMessageService`
- `IAdornerDialogMessageService`
- `IWindowDialogMessageService`
- `IFormMessageService`
- `INoticeMessageService`
- `ISnackMessageService`
- `IIOFileDialogService`
- `IIOFolderDialogService`

### 文件、序列化和数据

- `IAppPathServce`
- `ISerializerService`
- `IJsonSerializerService`
- `IXmlSerializerService`
- `ICloneService`
- `IExcelService`
- `IDbConnectService`
- `IDbDisconnectService`

### 日志、操作和通知

- `ILogService`
- `IAppLogService`
- `IOperationService`
- `ILogMailService`

### 项目和业务模块

- `IProjectService`
- `IProjectDialogService`
- `IProjectSplashSaveService`
- `ILicenseService`
- `IUpgradeService`
- `IGuideService`
- `IContactService`
- `IWebsiteService`
- `ISupportService`

---

## 12. 二次开发建议

### 12.1 优先构造函数注入

推荐：

```csharp
public class MyViewModel
{
    private readonly IProjectService _projectService;

    public MyViewModel(IProjectService projectService)
    {
        _projectService = projectService;
    }
}
```

仅在命令、静态入口、XAML 组合或历史代码中使用 `Ioc.GetService<T>()`。

### 12.2 模块默认实现使用 `TryAdd`

模块库不要强行覆盖应用层服务：

```csharp
services.TryAdd(ServiceDescriptor.Singleton<IMyService, MyService>());
```

应用如果需要替换实现，可注册自己的服务：

```csharp
services.AddSingleton<IMyService, MyCustomService>();
```

### 12.3 不要过早访问 IOC

避免在以下位置访问 `Ioc.Services`：

- 静态字段初始化。
- `App` 构造前的静态构造函数。
- `ConfigureServices` 注册服务之前。

安全位置：

- `Configure` 之后。
- 主窗口构造或加载后。
- 服务方法执行时。
- `OnSplashScreen` / `OnLogin` 扩展流程中。

### 12.4 注册多实现服务时注意查询方式

如果服务作为扩展点，需要按接口集合枚举，注册时使用扩展点接口：

```csharp
services.AddSingleton<ISplashLoadable, MyStartupLoadService>();
```

如果实现了 `ILoginedSplashLoadable`，但希望登录后流程能扫描到，建议注册到 `ISplashLoadable` 集合中：

```csharp
services.AddSingleton<ISplashLoadable, MyUserDataLoadService>();
```

### 12.5 XAML 中使用服务要保证命名空间和注册顺序

```xaml
<ContentControl Content="{Ioc Type={x:Type ILoginButtonViewPresenter}}" />
```

需要：

- XAML 文件能识别服务接口类型。
- 服务已在 `ConfigureServices` 中注册。
- IOC 已由 `ApplicationBase` 构建。

---

## 13. 常见问题

### `请先初始化ApplicationBase.ConfigureServices后再获取Ioc接口`

原因：访问 `Ioc` 时容器尚未构建。

解决：确保应用继承 `ApplicationBase`，并避免在静态初始化阶段访问 `Ioc`。

### `此接口为依赖注入接口，请先在ApplicationBase中注册<T>服务`

原因：服务未注册，且 `throwIfNone = true`。

解决：在 `ConfigureServices` 中注册服务，或使用可选获取：

```csharp
var service = Ioc.GetService<IMyService>(false);
```

### XAML 中 `{Ioc ...}` 报错

检查：

1. 服务接口类型是否正确引入命名空间。
2. 服务是否已注册。
3. 是否在应用启动资源加载过早的位置使用。

### 多个实现只获取到一个

`GetService<T>()` 获取单个服务。多个实现应使用：

```csharp
Ioc.Services.GetServices<T>();
```

或使用项目封装：

```csharp
Ioc.GetAssignableFromServices<T>();
```

### `DbIoc` 获取不到服务

`DbIoc.GetService<T>()` 找不到服务时会回退到主 `Ioc`。如果数据库服务需要独立容器，请确认已调用：

```csharp
DbIoc.ConfigureServices(services =>
{
    services.AddSingleton<IMyDbService, MyDbService>();
});
```
