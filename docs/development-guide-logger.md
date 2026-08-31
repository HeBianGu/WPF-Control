**适用项目：** `H.Services.Logger`、`H.Extensions.Log4net`、`H.Modules.Logger`  
**核心类型：** `ILogService`、`IocLog`、`Log4netService`、`Log4netOptions`、`IAppLogService`、`AppLogService`、`LoggerOptions`  
**相关能力：** 文件日志、应用内日志列表、异常记录、日志级别控制、日志目录打开

本文介绍框架日志体系的组成、注册方式、日志调用、文件输出、应用内展示和自定义扩展方式。

---

## 1. 日志体系定位

框架将日志能力分为三层：

| 项目 | 职责 |
|---|---|
| `H.Services.Logger` | 定义 `ILogService`、`IAppLogService`、`LogType`、`IocLog` 和日志命令。 |
| `H.Extensions.Log4net` | 使用 log4net 实现 `ILogService`，将日志写入文件。 |
| `H.Modules.Logger` | 实现 `IAppLogService`，在应用内存中维护最近的日志并提供展示界面。 |

典型调用链：

```text
业务代码
    ↓
IocLog.Info / Warn / Error ...
    ├── AppLogger → IAppLogService → 应用内日志列表
    └── ILogService → Log4netService → 日志文件
```

应用可以只注册文件日志、只注册应用内日志，也可以同时注册两者。通常建议同时注册：文件日志用于问题追踪，应用内日志用于向用户或运维人员展示最近事件。

---

## 2. 项目引用

完整日志能力需要引用：

```xml
<ItemGroup>
  <ProjectReference Include="..\..\Services\H.Services.Logger\H.Services.Logger.csproj" />
  <ProjectReference Include="..\..\Extensions\H.Extensions.Log4net\H.Extensions.Log4net.csproj" />
  <ProjectReference Include="..\..\Modules\H.Modules.Logger\H.Modules.Logger.csproj" />
</ItemGroup>
```

如果应用只需要日志接口和自定义实现，可仅引用 `H.Services.Logger`。

`H.Extensions.Log4net` 当前使用 `log4net` 包实现文件输出。应用不应绕过 `ILogService` 在业务层直接依赖 log4net，除非确实需要框架接口未提供的高级能力。

---

## 3. 完整注册

在基于 `ApplicationBase` 的应用中注册文件日志和应用内日志：

```csharp
using H.Extensions.ApplicationBase;
using H.Extensions.Log4net;
using H.Modules.Logger;
using H.Services.AppPath;
using H.Services.Logger;
using Microsoft.Extensions.DependencyInjection;

public partial class App : ApplicationBase
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);

        services.AddLog4net(options =>
        {
            options.LogPath = AppPaths.Instance.Log;
            options.tempPath = AppPaths.Instance.Cache;
        });

        services.AddAppLog(options =>
        {
            options.Capacity = 200;
            options.LogType = LogType.Info;
        });
    }

    protected override void Configure(IApplicationBuilder app)
    {
        base.Configure(app);

        app.UseLog4netOptions(options =>
        {
            options.LogPath = AppPaths.Instance.Log;
            options.tempPath = AppPaths.Instance.Cache;
        });
        Log4netOptions.Instance.LogType = LogType.Info;

        app.UseAppLogOptions(options =>
        {
            options.Capacity = 200;
            options.LogType = LogType.Info;
        });
    }
}
```

相关扩展方法：

| 方法 | 作用 |
|---|---|
| `AddLog4net()` | 将 `Log4netService` 注册为默认 `ILogService`。 |
| `UseLog4netOptions()` | 配置并注册 `Log4netOptions.Instance` 到设置系统。 |
| `AddAppLog()` | 注册 `IAppLogService` 和 `IAppLogPresenter`。 |
| `UseAppLogOptions()` | 配置并注册 `LoggerOptions.Instance` 到设置系统。 |

`ApplicationBase` 会创建并注册默认 `AppPaths`，因此基于该基类的应用可使用 `AppPaths.Instance.Log`。自定义启动架构必须先注册 `IAppPathServce` 和 `AppPaths`，详细说明见 [`development-guide-apppaths.md`](development-guide-apppaths.md)。

使用 `DefaultApplicationBase` 或调用 `AddApplicationServices()`、`UseApplicationOptions()` 的应用，默认已经注册文件日志和应用内日志，不应无意重复注册。

---

## 4. 日志级别

`LogType` 定义：

```csharp
public enum LogType
{
    Debug = 0,
    Info,
    Error,
    Warn,
    Fatal
}
```

级别含义：

| 级别 | 使用场景 |
|---|---|
| `Debug` | 调试细节、参数值和开发期诊断信息。 |
| `Info` | 正常业务流程、启动、停止和关键状态变化。 |
| `Error` | 已发生但应用仍可继续运行的错误。 |
| `Warn` | 潜在问题、降级行为或需要关注的异常状态。 |
| `Fatal` | 导致核心功能不可用或应用即将退出的严重错误。 |

当前枚举顺序是 `Error` 在 `Warn` 之前，应按源码实际顺序理解阈值，不要假定它与其他日志框架的数值顺序完全一致。

`Log4netService` 将 `Log4netOptions.LogType` 作为最低记录阈值。例如设置为 `Info` 时不记录 `Debug`，但记录 `Info`、`Error`、`Warn` 和 `Fatal`。

`Trace()` 没有独立的 `LogType.Trace`：

- 写入文件时映射为 log4net `Debug`。
- 写入应用内日志时当前映射为 `Info`。

---

## 5. 使用 `IocLog` 记录日志

静态入口适用于命令、事件处理器和框架扩展：

```csharp
using H.Services.Logger;

IocLog.Debug("开始加载项目配置");
IocLog.Info("项目加载完成");
IocLog.Warn("未找到用户模板，已使用默认模板");
IocLog.Error("数据保存失败");
IocLog.Fatal("数据库初始化失败");
```

一次记录多条消息：

```csharp
IocLog.Info(
    "开始导入数据",
    "正在校验文件",
    "数据导入完成");
```

记录异常：

```csharp
try
{
    SaveProject();
}
catch (Exception ex)
{
    IocLog.Error(ex);
}
```

`Error(Exception[])` 和 `Fatal(Exception[])` 会把异常传给文件日志实现。`AppLogger` 的默认异常转发只保留 `Exception.Message`，应用内日志列表不会保留完整堆栈。

使用 `IocLog` 时，消息会先尝试写入应用内日志，再写入 `ILogService`。

> 当前 `IocLog.Debug()` 直接调用 `ILogService.Debug()`，没有空值保护。调用 Debug 日志前应确保已注册 `ILogService`。其他级别在没有文件日志实现时会跳过文件写入。

---

## 6. 通过依赖注入记录日志

业务服务优先注入 `ILogService`，便于测试和替换实现：

```csharp
using H.Services.Logger;

public sealed class ProjectService
{
    private readonly ILogService _logger;

    public ProjectService(ILogService logger)
    {
        _logger = logger;
    }

    public void Save()
    {
        try
        {
            // 保存项目。
            _logger.Info("项目保存完成");
        }
        catch (Exception ex)
        {
            _logger.Error(ex);
            throw;
        }
    }
}
```

直接调用注入的 `ILogService` 只写入文件日志实现，不会自动进入 `IAppLogService` 的应用内列表。需要同时输出到两个目标时，可使用 `IocLog`，或实现自己的组合日志服务。

推荐选择：

- 可测试的领域服务：注入 `ILogService`。
- 需要同时进入文件和应用日志列表的 UI 事件：使用 `IocLog`。
- 基础设施层需要完全控制输出：实现并注入自定义 `ILogService`。

---

## 7. 文件日志 `Log4netService`

`Log4netService` 在第一次从 IOC 解析时初始化日志器：

1. 使用当前进程名创建 log4net Logger。
2. 确保 `LogPath` 目录存在。
3. 创建 `RollingFileAppender`。
4. 按日期目录和小时文件输出日志。
5. 使用 `MinimalLock` 降低文件长期占用。

默认布局：

```text
%date [%thread] %-5level - %message%newline
```

典型目录结构：

```text
{LogPath}/
└── 2025-01-01/
    ├── 2025-01-01_09.log
    └── 2025-01-01_10.log
```

实际文件名由 `RollingFileAppender` 在运行时生成。

默认配置：

```csharp
Log4netOptions.Instance.LogPath
Log4netOptions.Instance.tempPath
Log4netOptions.Instance.LogType
```

其中 `tempPath` 的当前 API 名称以小写字母开头，调用时必须使用实际拼写。

`ILog4netOptions` 当前只公开 `LogPath` 和 `tempPath`，没有公开 `LogType`。需要在代码中设置级别时，可在容器构建后使用：

```csharp
Log4netOptions.Instance.LogType = LogType.Debug;
```

或者使用完整类型配置标准 Options：

```csharp
services.Configure<Log4netOptions>(options =>
{
    options.LogType = LogType.Debug;
});
```

---

## 8. `log4net.config`

当前 `Log4netService` 会尝试读取应用工作目录中的：

```text
log4net.config
```

如果文件包含占位符：

```text
#LOG_PATH#
```

初始化时会将其替换为 `Log4netOptions.tempPath`。

当前主要文件 Appender 仍由 `Log4netService.InitLogPath()` 通过代码创建，因此基础滚动文件日志不依赖外部配置文件。没有自定义配置需求时可以不提供 `log4net.config`。

如果应用提供该文件，应确保：

```xml
<ItemGroup>
  <None Update="log4net.config">
    <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
  </None>
</ItemGroup>
```

不要在只读安装目录中依赖运行时修改配置文件。需要复杂 Appender、数据库日志或集中日志时，建议派生或替换 `ILogService`，明确管理配置文件加载和输出失败策略。

---

## 9. 应用内日志 `AppLogService`

`AppLogService` 将日志插入 `IAppLogPresenter.Messages` 的开头，最新消息显示在最前面。

日志消息包含：

```csharp
public interface IAppLogMessage
{
    LogType Level { get; set; }
    string Message { get; set; }
    DateTime Time { get; set; }
}
```

容量由以下配置控制：

```csharp
LoggerOptions.Instance.Capacity
```

默认值为 `100`。超过容量后会删除旧消息。建议将容量设置为大于零的合理值：

```csharp
app.UseAppLogOptions(options =>
{
    options.Capacity = 500;
});
```

应用内日志仅存在于当前进程内存中，退出应用后不会保留。需要持久化追踪时必须同时注册文件日志或其他持久化实现。

`LoggerOptions.LogType` 当前已作为设置项公开，但默认 `AppLogService` 尚未使用它过滤消息。应用内列表目前接收 `IocLog` 转发的所有级别；文件日志阈值由 `Log4netOptions.LogType` 单独控制。

`AppLogService` 直接修改 `ObservableCollection`。从后台线程记录并同时绑定 UI 时，应切换到 WPF Dispatcher，或提供线程安全的自定义 `IAppLogService`，避免跨线程集合访问异常。

---

## 10. 打开应用日志界面

`AddAppLog()` 注册：

```text
IAppLogService -> AppLogService
IAppLogPresenter -> AppLogPresenter
```

通过框架命令打开应用日志列表：

```xaml
<Button
    Command="{h:ShowAppLogerCommmand}"
    Content="应用日志" />
```

当前类型名为 `ShowAppLogerCommmand`，保留了源码中的 `Loger` 和 `Commmand` 拼写，XAML 必须使用实际名称。

默认界面使用只读 `DataGrid` 展示：

- 序号。
- 时间。
- 日志类型。
- 日志消息。
- 不同级别对应的颜色和图标。

使用前应调用 `services.AddAppLog()`，并注册框架消息和对话框能力。消息系统说明见 [`development-guide-message.md`](development-guide-message.md)。

---

## 11. 打开日志目录

`ShowLog4netPathCommand` 使用系统文件管理器打开 `Log4netOptions.Instance.LogPath`：

```xaml
<Button
    Command="{h:ShowLog4netPathCommand}"
    Content="查看日志文件" />
```

调用前应确保：

1. 已注册 `AddLog4net()`。
2. 已调用 `UseLog4netOptions()`。
3. `LogPath` 已设置且目录存在。
4. 当前用户有权访问该目录。

代码中也可以直接打开：

```csharp
Process.Start(new ProcessStartInfo(Log4netOptions.Instance.LogPath)
{
    UseShellExecute = true
});
```

---

## 12. XAML 日志命令

`LogCommand` 可在 XAML 中记录固定消息：

```xaml
<Button
    Command="{h:LogCommand Message=用户点击了导出按钮, Type=Info}"
    Content="导出" />
```

支持 `Debug`、`Info`、`Error`、`Warn` 和 `Fatal`。

该命令适合记录简单的 UI 操作。包含业务上下文、异常对象或动态结构化数据时，应在 ViewModel 命令中调用 `IocLog` 或注入 `ILogService`。

---

## 13. 自定义文件日志实现

实现 `ILogService` 可以替换 log4net，例如输出到 Serilog、数据库、远程日志平台或测试收集器：

```csharp
using H.Services.Logger;

public sealed class CustomLogService : ILogService
{
    public void Debug(params string[] messages) => Write(LogType.Debug, messages);
    public void Info(params string[] messages) => Write(LogType.Info, messages);
    public void Warn(params string[] messages) => Write(LogType.Warn, messages);
    public void Error(params string[] messages) => Write(LogType.Error, messages);
    public void Fatal(params string[] messages) => Write(LogType.Fatal, messages);
    public void Trace(params string[] messages) => Write(LogType.Debug, messages);

    public void Error(params Exception[] exceptions)
    {
        foreach (Exception exception in exceptions)
            WriteException(LogType.Error, exception);
    }

    public void Fatal(params Exception[] exceptions)
    {
        foreach (Exception exception in exceptions)
            WriteException(LogType.Fatal, exception);
    }

    private void Write(LogType level, IEnumerable<string> messages)
    {
        // 写入自定义目标。
    }

    private void WriteException(LogType level, Exception exception)
    {
        // 保留异常类型、消息、内部异常和堆栈。
    }
}
```

`AddLog4net()` 使用 `TryAdd` 注册默认实现。若仍需调用它注册 Options，但希望替换 Logger，应先注册自定义服务：

```csharp
services.AddSingleton<ILogService, CustomLogService>();
services.AddLog4net();
```

也可以完全不调用 `AddLog4net()`，只注册自定义 `ILogService`。

---

## 14. 自定义应用内日志

实现 `IAppLogService` 可以增加过滤、线程切换或持久化：

```csharp
public sealed class DispatcherAppLogService : IAppLogService
{
    private readonly IAppLogPresenter _presenter;

    public DispatcherAppLogService(IAppLogPresenter presenter)
    {
        _presenter = presenter;
    }

    public void Log(string message, LogType level = LogType.Info)
    {
        Application.Current.Dispatcher.Invoke(() =>
        {
            _presenter.Messages.Insert(0,
                new AppLogMessage(message, level));
        });
    }
}
```

`AddAppLog()` 同样使用 `TryAdd`，因此自定义实现应在其之前注册：

```csharp
services.AddSingleton<IAppLogService, DispatcherAppLogService>();
services.AddAppLog();
```

自定义展示界面时，可以替换 `IAppLogPresenter`，但需要保持命令和 DataTemplate 所需的契约一致。

---

## 15. 日志内容建议

推荐记录：

- 应用启动、退出和版本信息。
- 登录、权限校验和关键用户操作结果。
- 项目打开、保存、导入和导出结果。
- 外部设备、数据库和网络连接状态。
- 降级处理和重试结果。
- 完整异常对象。

避免记录：

- 密码、令牌、私钥和连接字符串密码。
- 用户身份证件、完整手机号等不必要的个人信息。
- 大型二进制内容或完整文件内容。
- 高频循环中没有采样或级别控制的调试日志。

建议让消息包含必要上下文：

```csharp
IocLog.Info($"项目保存完成：ProjectId={project.Id}, Path={project.FilePath}");
```

记录异常时优先传递异常对象：

```csharp
IocLog.Error(ex);
```

而不是只记录：

```csharp
IocLog.Error(ex.Message);
```

前者能让文件日志实现保留更多诊断信息。

---

## 16. 常见问题

### 调用日志后没有生成文件

检查：

1. 是否调用 `services.AddLog4net()`。
2. `ILogService` 是否能从 IOC 中解析。
3. `Log4netOptions.LogPath` 是否为有效目录。
4. 当前用户是否具有目录创建和写入权限。
5. 当前日志级别是否低于 `Log4netOptions.LogType` 阈值。
6. 是否查看了日期子目录和小时日志文件。

### `LogPath` 为空或初始化失败

确保路径服务已注册，并显式配置：

```csharp
app.UseLog4netOptions(options =>
{
    options.LogPath = AppPaths.Instance.Log;
    options.tempPath = AppPaths.Instance.Cache;
});
```

日志服务可能在第一次解析时初始化，因此应在首次记录日志前完成路径配置。

### 应用日志页面为空，但文件中有日志

直接调用注入的 `ILogService` 只写文件。需要同步进入应用日志列表时使用：

```csharp
IocLog.Info("消息");
```

并确认已调用 `services.AddAppLog()`。

### 文件没有 Debug 日志

将文件日志阈值设置为 `Debug`：

```csharp
Log4netOptions.Instance.LogType = LogType.Debug;
```

同时确保已注册 `ILogService`。当前 `IocLog.Debug()` 在未注册文件日志时可能出现空引用异常。

### 后台任务记录日志时出现集合线程异常

文件日志可以从后台线程调用，但默认应用内日志会直接修改 UI 绑定的 `ObservableCollection`。切换到 Dispatcher，或替换为线程安全的 `IAppLogService`。

### 应用日志数量超过预期

检查 `LoggerOptions.Instance.Capacity` 是否为正数，并确认是否存在自定义 `IAppLogService`。默认实现会保留最新的指定数量消息。

### 修改日志级别后没有影响应用日志列表

当前 `LoggerOptions.LogType` 尚未用于 `AppLogService` 过滤。它与 `Log4netOptions.LogType` 是不同设置；文件过滤应修改后者，应用内过滤需要自定义 `IAppLogService`。
