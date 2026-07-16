# ApplicationBase 二次开发文档
# ApplicationBase 二次开发文档

**适用项目：** `H.Extensions.ApplicationBase`  
**核心类型：** `ApplicationBase`、`ApplicationBuilder`、`IConfigureableApplication`、`ILoginableApplication`  
**框架：** `.NET 8.0-windows` + WPF

本文说明 `H.Extensions.ApplicationBase.ApplicationBase` 的定位、启动流程、服务注册、配置管线、启动页、登录页、异常处理和常见扩展方式。`ApplicationBase` 是 WPF-Control 应用启动和模块组合的基础类，建议所有完整应用或测试应用都基于它派生。

---

## 1. 类定位

`ApplicationBase` 继承 WPF `Application`，并实现：

- `IConfigureableApplication`
- `ILoginableApplication`

它承担以下职责：

- 创建并注册系统路径服务 `IAppPathServce`。
- 初始化全局异常处理。
- 创建 `ServiceCollection` 并构建 IOC 容器。
- 在启动时执行应用配置管线。
- 限制应用单实例运行。
- 创建主窗口。
- 加载启动页和启动任务。
- 显示登录页面并加载登录后任务。
- 恢复主窗口状态。
- 启动计划任务服务。
- 应用退出时释放服务并记录日志。

典型派生类只需要重写：

```csharp
protected override void ConfigureServices(IServiceCollection services)
protected override void Configure(IApplicationBuilder app)
protected override Window CreateMainWindow(StartupEventArgs e)
```

---

## 2. 最小使用示例

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
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace MyApp;

public partial class App : ApplicationBase
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        // 注册服务、模块、窗口、消息、设置等。
    }

    protected override void Configure(IApplicationBuilder app)
    {
        base.Configure(app);
        // 启用模块配置，例如 app.UseLoginOptions()、app.UseApplicationOptions()。
    }

    protected override Window CreateMainWindow(StartupEventArgs e)
    {
        return new MainWindow();
    }
}
```

---

## 3. 构造阶段流程

`ApplicationBase` 构造函数：

```csharp
public ApplicationBase()
{
    this.ShutdownMode = ShutdownMode.OnMainWindowClose;
    AppPaths.Register(this.CreateAppPathServce());
    this.InitExcetion();
    this.InitServiceCollection();
}
```

执行顺序：

1. 设置 `ShutdownMode = OnMainWindowClose`。
2. 调用 `CreateAppPathServce()` 创建路径服务，并注册到 `AppPaths`。
3. 调用 `InitExcetion()` 注册全局异常处理。
4. 调用 `InitServiceCollection()` 初始化 IOC 容器。

注意：构造阶段早于 `OnStartup`，因此 `ConfigureServices` 中注册的服务在启动流程开始前已经可通过 `Ioc` 获取。

---

## 4. 系统路径服务创建

默认路径服务由 `CreateAppPathServce()` 创建：

```csharp
protected virtual IAppPathServce CreateAppPathServce()
{
    return new AppPathServce()
    {
        Version = ApplicationProvider.Version,
        Company = ApplicationProvider.Company ?? "HeBianGu",
    };
}
```

默认实现会注册到：

```csharp
AppPaths.Register(this.CreateAppPathServce());
```

如果需要自定义公司名、版本号、根目录或路径结构，可以重写该方法：

```csharp
protected override IAppPathServce CreateAppPathServce()
{
    return new MyAppPathServce
    {
        Company = "Contoso",
        Version = ApplicationProvider.Version
    };
}
```

适用场景：

- 使用 `LocalApplicationData` 替代 `MyDocuments`。
- 按应用版本隔离配置目录。
- 改写用户目录或注册表路径。
- 企业产品中统一公司名和目录结构。

---

## 5. 服务注册阶段

`InitServiceCollection()` 负责创建服务集合并构建 IOC：

```csharp
protected void InitServiceCollection()
{
    ServiceCollection sc = new ServiceCollection();
    this.ConfigureServices(sc);
    Ioc.Build(sc);
    Ioc.GetService<ILoadGlobalizationOptionsService>(false)?.Load(out string message);
}
```

执行内容：

1. 创建 `ServiceCollection`。
2. 调用派生类 `ConfigureServices(IServiceCollection services)`。
3. 调用 `Ioc.Build(sc)` 构建全局 IOC 容器。
4. 尝试加载全球化配置 `ILoadGlobalizationOptionsService`。

### 5.1 注册服务示例

```csharp
protected override void ConfigureServices(IServiceCollection services)
{
    services.AddSetting();
    services.AddWindowMessage();
    services.AddAdornerDialogMessage();
    services.AddLoginViewPresenter();
    services.AddTestLoginService();
}
```

建议：

- 所有模块服务在 `ConfigureServices` 中注册。
- 需要被启动页加载的服务，注册为 `ISplashLoadable`。
- 需要登录后加载的服务，注册为 `ILoginedSplashLoadable`。
- 需要退出释放资源的服务，注册为 `IAppExitService`。

---

## 6. 配置管线阶段

`ApplicationBase` 提供两个配置入口：

```csharp
protected virtual void Configure(IApplicationBuilder app)
{
}

protected void Configure()
{
    ApplicationBuilder bulder = new ApplicationBuilder();
    this.Configure(bulder);
}
```

`OnStartup` 开始时会调用：

```csharp
this.Configure();
```

因此派生类通常重写 `Configure(IApplicationBuilder app)`：

```csharp
protected override void Configure(IApplicationBuilder app)
{
    base.Configure(app);
    app.UseLoginOptions();
    app.UseRegistorOptions();
    app.UseApplicationOptions();
}
```

`ApplicationBuilder` 当前是一个空实现类，主要作为各模块 `UseXxxOptions` 扩展方法的承载对象。例如：

```csharp
public static IApplicationBuilder UseLoginOptions(this IApplicationBuilder builder, Action<ILoginOptions> option = null)
{
    IocSetting.Instance.Add(LoginOptions.Instance);
    option?.Invoke(LoginOptions.Instance);
    return builder;
}
```

---

## 7. 启动阶段流程

`OnStartup` 是应用启动主流程：

```csharp
protected override void OnStartup(StartupEventArgs e)
{
    this.Configure();
    this.OnSingleton(e);
    base.OnStartup(e);

    Window window = this.CreateMainWindow(e);
    window.Loaded += ...;

    this.OnSplashScreen(e);
    this.OnLogin();
    Ioc<IMainWindowSavableService>.Instance?.Load(window);
    this.MainWindow.Show();
    this.ILogger?.Info("系统启动");
    Ioc<IScheduledTaskService>.Instance?.Start();
}
```

完整顺序：

1. 执行配置管线 `Configure()`。
2. 执行单实例检查 `OnSingleton(e)`。
3. 调用 WPF 基类 `base.OnStartup(e)`。
4. 调用 `CreateMainWindow(e)` 创建主窗口。
5. 绑定主窗口 `Loaded` 后加载 `IMainWindowLoadedLoadable` 服务。
6. 调用 `OnSplashScreen(e)` 显示启动页并加载启动任务。
7. 调用 `OnLogin()` 显示登录页并加载登录后任务。
8. 调用 `IMainWindowSavableService.Load(window)` 恢复主窗口状态。
9. 显示主窗口。
10. 记录启动日志。
11. 启动 `IScheduledTaskService`。

---

## 8. 创建主窗口

`CreateMainWindow` 是唯一必须实现的抽象方法：

```csharp
protected abstract Window CreateMainWindow(StartupEventArgs e);
```

示例：

```csharp
protected override Window CreateMainWindow(StartupEventArgs e)
{
    return new MainWindow();
}
```

`base.OnStartup(e)` 会创建 WPF 主窗口上下文，随后 `ApplicationBase` 使用该窗口完成启动页、登录页、窗口状态恢复和显示。

---

## 9. 单实例检查

默认 `OnSingleton` 使用当前进程名创建 `Mutex`：

```csharp
public virtual void OnSingleton(StartupEventArgs e)
{
    Process thisProc = Process.GetCurrentProcess();
    bool createdNew;
    mutex = new Mutex(true, thisProc.ProcessName, out createdNew);
    if (!createdNew)
    {
        this.ShowMessage(Resources.Message_Singleton);
        this.Shutdown();
    }
}
```

如果应用允许多开，可以重写为空：

```csharp
public override void OnSingleton(StartupEventArgs e)
{
    // 允许多实例运行。
}
```

如果需要按用户、工作区或启动参数控制单实例，也可以重写该方法使用自定义 `Mutex` 名称。

---

## 10. 启动页加载流程 `OnSplashScreen`

`OnSplashScreen` 负责启动页显示和启动任务加载。

主要流程：

1. 从 IOC 获取 `ISplashScreenViewPresenter`。
2. 先加载主题配置 `ILoadThemeOptionsService`。
3. 加载设置数据 `IocSetting.Instance.Load(...)`。
4. 执行所有 `IDefaultTemplateable.LoadDefaultTemplate()`。
5. 执行所有 `ISplashLoadable.Load(out message)`。
6. 如果加载失败则关闭应用。

### 10.1 注册启动加载服务

实现 `ISplashLoadable`：

```csharp
using H.Common.Interfaces;

public class MyStartupLoadService : ISplashLoadable
{
    public string Name => "初始化业务数据";

    public bool Load(out string message)
    {
        message = null;
        // 初始化缓存、数据库、配置等。
        return true;
    }
}
```

注册：

```csharp
services.AddSingleton<ISplashLoadable, MyStartupLoadService>();
```

### 10.2 注册默认模板加载服务

如果服务实现 `IDefaultTemplateable`，启动页会调用：

```csharp
item.LoadDefaultTemplate();
```

适合用于：

- 复制默认项目模板。
- 初始化默认配置模板。
- 初始化内置样例数据。

---

## 11. 登录流程 `OnLogin`

`OnLogin` 分两段：

1. 如果注册了 `ILoginViewPresenter`，显示登录页面。
2. 登录成功后，如果注册了 `ILoginedSplashViewPresenter`，显示登录后加载页，并加载用户级数据。

### 11.1 登录页面显示

```csharp
ILoginViewPresenter presenter = Ioc.Services.GetService<ILoginViewPresenter>();
if (presenter == null)
    return;

bool? r = IocMessage.Window.Show(presenter, x =>
{
    x.MinWidth = 400;
    x.DialogButton = DialogButton.None;
    x.Title = Assembly.GetEntryAssembly().GetName().Version.ToString();
}).Result;
```

如果返回 `false`，应用会退出：

```csharp
IocLog.Info("登录失败程序退出");
this.Shutdown();
```

### 11.2 登录后加载

登录后加载内容包括：

- `IocSetting.Instance.LoadLoginedLoad(...)`
- `ILoginedSplashLoadable`
- `IDefaultTemplateable`

实现登录后加载服务：

```csharp
using H.Common.Interfaces;

public class MyUserDataLoadService : ILoginedSplashLoadable
{
    public string Name => "加载用户数据";

    public bool Load(out string message)
    {
        message = null;
        // 读取 AppPaths.Instance.UserData / UserSetting 等用户级数据。
        return true;
    }
}
```

注册：

```csharp
services.AddSingleton<ISplashLoadable, MyUserDataLoadService>();
```

注意：源码通过 `Ioc.GetAssignableFromServices<ISplashLoadable>().OfType<ILoginedSplashLoadable>()` 查找登录后任务，因此服务需要能从 `ISplashLoadable` 集合中被解析。

---

## 12. 主窗口 Loaded 扩展点

主窗口 `Loaded` 后会执行所有 `IMainWindowLoadedLoadable`：

```csharp
window.Loaded += (s, e) =>
{
    var loads = Ioc.GetAssignableFromServices<IMainWindowLoadedLoadable>().Distinct();
    foreach (var item in loads)
        item.Load(out string message);
};
```

适合做：

- 依赖主窗口句柄的初始化。
- 主窗口可视化元素加载后执行的逻辑。
- 延迟加载 UI 数据。
- 启动后自动打开面板或工作区。

示例：

```csharp
public class MyMainWindowLoadedService : IMainWindowLoadedLoadable
{
    public bool Load(out string message)
    {
        message = null;
        // 主窗口 Loaded 后执行。
        return true;
    }
}
```

注册：

```csharp
services.AddSingleton<IMainWindowLoadedLoadable, MyMainWindowLoadedService>();
```

---

## 13. 主窗口状态恢复

`OnStartup` 中会调用：

```csharp
Ioc<IMainWindowSavableService>.Instance?.Load(window);
```

`IMainWindowSavableService` 用于加载和保存主窗口状态：

```csharp
public interface IMainWindowSavableService : ISplashSave
{
    void Load(Window window);
}
```

适合保存：

- 主窗口大小。
- 主窗口位置。
- 最大化状态。
- Dock 布局状态。

如果应用需要窗口状态持久化，需要注册具体实现，例如窗口模块中的实现服务。

---

## 14. 异常处理机制

`InitExcetion()` 注册三类异常处理：

```csharp
DispatcherUnhandledException += App_DispatcherUnhandledException;
AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
```

| 处理器 | 场景 |
|---|---|
| `App_DispatcherUnhandledException` | UI 线程未处理异常。 |
| `CurrentDomain_UnhandledException` | AppDomain 未处理异常。 |
| `TaskScheduler_UnobservedTaskException` | 未观察的 Task 异常。 |

默认行为：

- 弹出消息提示。
- 通过 `ILogService` 记录错误或致命日志。
- UI 线程异常会设置 `e.Handled = true`。

如需自定义异常处理，可重写：

```csharp
protected override void InitExcetion()
{
    base.InitExcetion();
    // 注册额外异常上报，例如 Sentry、Web API、文件日志等。
}
```

---

## 15. 退出流程

`OnExit` 中会：

1. 查找所有 `IAppExitService`。
2. 逐个调用 `Dispose()`。
3. 记录系统退出日志。
4. 记录操作日志。
5. 调用 `base.OnExit(e)`。

```csharp
protected override void OnExit(ExitEventArgs e)
{
    try
    {
        var disposes = Ioc.GetAssignableFromServices<IAppExitService>().Distinct();
        foreach (var item in disposes)
            item.Dispose();
        this.ILogger?.Info("系统退出");
        Ioc<IOperationService>.Instance?.Log<ApplicationBase>("系统推出");
    }
    catch (Exception ex)
    {
        this.ILogger?.Error(ex);
    }
    finally
    {
        base.OnExit(e);
    }
}
```

实现退出服务：

```csharp
public class MyAppExitService : IAppExitService
{
    public void Dispose()
    {
        // 释放连接、关闭文件、停止后台线程。
    }
}
```

注册：

```csharp
services.AddSingleton<IAppExitService, MyAppExitService>();
```

---

## 16. 保存所有配置命令

`IocSaveAllCommand` 用于保存应用中所有需要保存的数据。

它会：

1. 获取所有 `ISplashSave`。
2. 逐个调用 `Save(out message)`。
3. 调用 `ISettingDataService.Save(out message)` 保存系统配置。

适合放在设置页或菜单按钮：

```xaml
<MenuItem Command="{IocSaveAllCommand}" />
```

或在命令集合中直接使用对应命令类型。

实现可保存服务：

```csharp
public class MySaveService : ISplashSave
{
    public string Name => "保存业务数据";

    public bool Save(out string message)
    {
        message = null;
        return true;
    }
}
```

注册：

```csharp
services.AddSingleton<ISplashSave, MySaveService>();
```

---

## 17. 推荐派生模板

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

        services.AddLoginViewPresenter();
        services.AddTestLoginService();

        services.AddSingleton<ISplashLoadable, MyStartupLoadService>();
        services.AddSingleton<ISplashSave, MySaveService>();
        services.AddSingleton<IAppExitService, MyAppExitService>();
    }

    protected override void Configure(IApplicationBuilder app)
    {
        base.Configure(app);
        app.UseLoginOptions(option =>
        {
            option.Product = "MyApp";
            option.Remember = true;
        });
    }

    protected override Window CreateMainWindow(StartupEventArgs e)
    {
        return new MainWindow();
    }
}
```

---

## 18. 常见问题

### `Ioc.Services` 为空

通常是访问时机过早。`Ioc.Build(sc)` 在 `ApplicationBase` 构造阶段完成。避免在 `App` 字段初始化、静态构造函数或模块静态字段中访问 IOC。

### 主窗口不显示

检查：

1. `CreateMainWindow` 是否返回了有效窗口。
2. `OnSplashScreen` 是否加载失败并调用 `Shutdown()`。
3. `OnLogin` 是否登录失败并调用 `Shutdown()`。
4. 单实例检查是否检测到已有进程。

### 启动页不显示

需要注册 `ISplashScreenViewPresenter`。如果未注册，`OnSplashScreen` 会直接执行加载函数，不显示启动窗口。

### 登录页不显示

需要注册 `ILoginViewPresenter`。如果未注册，`OnLogin` 会直接返回，不显示登录页面。

### 登录后加载任务没有执行

源码从 `ISplashLoadable` 服务集合中筛选 `ILoginedSplashLoadable`。注册时建议：

```csharp
services.AddSingleton<ISplashLoadable, MyUserDataLoadService>();
```

其中 `MyUserDataLoadService` 实现 `ILoginedSplashLoadable`。

### 应用无法多开

默认 `OnSingleton` 会按进程名限制单实例。如果需要多实例，重写 `OnSingleton` 并留空。

### 如何自定义系统路径

重写 `CreateAppPathServce()`，返回自己的 `IAppPathServce` 实现或 `AppPathServce` 派生类。
