# AppPaths 系统路径二次开发文档
# AppPaths 系统路径二次开发文档

**适用服务：** `H.Services.AppPath`、`H.Extensions.AppPath`  
**核心类型：** `IAppPathServce`、`AppPathServce`、`AppPaths`、`AppDomianPaths`  
**框架：** `.NET 8.0-windows` + WPF

本文说明 WPF-Control 中系统路径服务的设计、配置和二次开发方式，重点介绍 `IAppPathServce` 的目录规划，以及 `AppDomianPaths` 在应用程序目录下的模板、插件、模块等静态路径约定。

> 注意：当前源码类型名为 `IAppPathServce`、`AppPathServce`、`AppDomianPaths`，保留了项目中的现有拼写。二次开发时应按源码类型名引用。

---

## 1. 模块定位

系统路径服务用于统一管理应用运行过程中产生或依赖的目录，包括：

- 应用级配置目录。
- 应用级数据目录。
- 应用级设置目录。
- 应用级日志目录。
- 应用级项目目录。
- 应用级模板目录。
- 应用级缓存目录。
- 登录用户级数据、设置、项目、模板、缓存目录。
- 应用安装目录下的默认模板、模块、组件、插件、版本目录。

它解决的问题是：业务模块不直接拼接硬编码路径，而是通过统一服务获取目录，从而便于切换公司名、应用名、版本号、用户目录和默认资源目录。

---

## 2. 相关项目和类型

| 项目 | 说明 |
|---|---|
| `H.Services.AppPath` | 定义路径服务接口、静态入口和扩展方法。 |
| `H.Extensions.AppPath` | 提供默认路径服务实现和清理缓存命令。 |

| 类型 | 说明 |
|---|---|
| `IAppPathServce` | 系统路径服务接口，定义所有应用级和用户级目录。 |
| `AppPathServce` | 默认实现，基于 `MyDocuments/Company/AppName` 生成目录结构。 |
| `AppPaths` | 静态访问入口，通过 `AppPaths.Instance` 获取当前路径服务。 |
| `AppDomianPaths` | 应用程序运行目录下的静态资源目录约定。 |
| `AppDomianPathExtensions` | 提供默认模板路径转换扩展。 |
| `AppPathExtension` | 提供 `ClearCache`、`ClearSetting` 等路径清理扩展。 |
| `ClearCacheDataCommand` | 清空缓存命令，内部调用 `AppPaths.Instance.ClearCache(...)`。 |

---

## 3. `IAppPathServce` 路径接口

`IAppPathServce` 定义系统路径服务的完整协议：

```csharp
public interface IAppPathServce
{
    string AppName { get; }
    string AppPath { get; }
    string Cache { get; }
    string Company { get; set; }
    string Config { get; }
    string Data { get; }
    string Default { get; }
    string Document { get; set; }
    string License { get; }
    string Log { get; }
    string Project { get; }
    string RegistryPath { get; }
    string Setting { get; }
    string Template { get; }
    string UserCache { get; }
    string UserData { get; }
    string UserLicense { get; }
    string UserLog { get; }
    string UserPath { get; }
    string UserProject { get; }
    string UserSetting { get; }
    string UserTemplate { get; }
    string Version { get; }
}
```

接口将路径分为两类：

1. **应用级路径**：与当前应用相关，不区分登录用户。
2. **用户级路径**：与当前登录用户相关，通常用于保存用户私有数据。

---

## 4. 默认实现 `AppPathServce`

`AppPathServce` 位于 `H.Extensions.AppPath`，是 `IAppPathServce` 的默认实现。

### 4.1 基础规则

默认属性：

```csharp
public virtual string Company { get; set; } = "HeBianGu";
public virtual string Document { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
public virtual string AppName => Assembly.GetEntryAssembly()?.GetName()?.Name;
public virtual string AppPath => Path.Combine(this.Document, this.Company, this.AppName);
```

默认根目录格式：

```text
{MyDocuments}\{Company}\{AppName}
```

例如：

```text
C:\Users\User\Documents\HeBianGu\H.Test.Login
```

### 4.2 版本目录规则

`Default` 目录会根据 `Version` 决定是否添加版本层级。

未设置版本：

```text
{Document}\{Company}\{AppName}\Default
```

设置版本：

```text
{Document}\{Company}\{AppName}\{Version}\Default
```

适用场景：

- 不同版本之间隔离配置、模板、缓存。
- 升级后保留旧版本数据。
- 多版本并行运行。

### 4.3 应用级目录

| 属性 | 目录规则 | 说明 |
|---|---|---|
| `AppPath` | `{Document}\{Company}\{AppName}` | 应用根目录。 |
| `Default` | `{AppPath}\Default` 或 `{AppPath}\{Version}\Default` | 应用默认数据根目录。 |
| `Config` | `{Default}\Config` | 配置目录。 |
| `Data` | `{Default}\Data` | 数据目录。 |
| `Setting` | `{Default}\Setting` | 设置目录。 |
| `License` | `{Default}\License` | 许可证目录。 |
| `Log` | `{Default}\Log` | 日志目录。 |
| `Project` | `{Default}\Project` | 项目文件目录。 |
| `Template` | `{Default}\Template` | 模板目录。 |
| `Cache` | `{Default}\Cache` | 缓存目录。 |
| `RegistryPath` | `SOFTWARE\{AppName}` | 注册表路径。 |

`AppPathServce` 构造时会创建主要应用级目录：`AppPath`、`Default`、`Config`、`Data`、`Setting`、`License`、`Log`、`Project`、`Template`、`Cache`。

### 4.4 用户级目录

`UserPath` 会尝试读取当前登录用户：

```csharp
private string GetUserName()
{
    return Ioc<ILoginService>.Instance?.User?.Account;
}
```

如果当前没有登录用户，则返回 `Default`。

未设置版本时：

```text
{AppPath}\{UserName}
```

设置版本时：

```text
{AppPath}\{UserName}\{Version}
```

用户级目录：

| 属性 | 目录规则 | 说明 |
|---|---|---|
| `UserPath` | `{AppPath}\{UserName}` 或 `{AppPath}\{UserName}\{Version}` | 当前登录用户根目录。未登录时回退到 `Default`。 |
| `UserData` | `{UserPath}\Data` | 用户数据目录。 |
| `UserSetting` | `{UserPath}\Setting` | 用户设置目录。 |
| `UserProject` | `{UserPath}\Project` | 用户项目目录。 |
| `UserTemplate` | `{UserPath}\Template` | 用户模板目录。 |
| `UserCache` | `{UserPath}\Cache` | 用户缓存目录。 |
| `UserLicense` | `{Default}\License` | 当前实现回到应用级许可证目录。 |
| `UserLog` | `{Default}\Log` | 当前实现回到应用级日志目录。 |

> 默认构造函数只主动创建应用级目录。用户级目录通常在业务写入前自行调用 `Directory.CreateDirectory(...)` 或通过自定义服务统一创建。

---

## 5. 注册和使用 `AppPaths`

`AppPaths` 是路径服务的静态入口。如果访问前未注册，会抛出 `AppPathServce is not registered.`。

### 5.1 最小注册示例

```csharp
using H.Extensions.AppPath;
using H.Services.AppPath;
using Microsoft.Extensions.DependencyInjection;

protected override void ConfigureServices(IServiceCollection services)
{
    var appPathServce = new AppPathServce();
    AppPaths.Register(appPathServce);

    services.AddSingleton<IAppPathServce>(appPathServce);
}
```

### 5.2 使用路径服务

业务代码中优先通过 `IAppPathServce` 注入使用：

```csharp
public class ProjectFileService
{
    private readonly IAppPathServce _appPath;

    public ProjectFileService(IAppPathServce appPath)
    {
        _appPath = appPath;
    }

    public string GetProjectFile(string fileName)
    {
        Directory.CreateDirectory(_appPath.UserProject);
        return Path.Combine(_appPath.UserProject, fileName);
    }
}
```

也可以通过静态入口快速访问：

```csharp
var logFolder = AppPaths.Instance.Log;
var userSettingFolder = AppPaths.Instance.UserSetting;
```

建议：

- 服务类、业务类优先使用构造函数注入 `IAppPathServce`。
- 命令、静态辅助类或旧代码可使用 `AppPaths.Instance`。
- 写入用户级目录前确认目录存在。

---

## 6. 配置公司名、文档根目录和版本号

`AppPathServce` 的 `Company`、`Document`、`Version` 是影响路径结构的关键配置。

### 6.1 通过实例配置

```csharp
var appPathServce = new AppPathServce
{
    Company = "Contoso",
    Document = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
    Version = "1.3.0"
};

AppPaths.Register(appPathServce);
services.AddSingleton<IAppPathServce>(appPathServce);
```

> `AppPathServce` 构造函数会创建目录。如果需要在目录创建前配置 `Company`、`Document`、`Version`，建议使用自定义派生类覆盖默认值，或创建后主动确认目录存在。

### 6.2 通过派生类配置

推荐在正式应用中派生自己的路径服务：

```csharp
using H.Extensions.AppPath;

public class MyAppPathServce : AppPathServce
{
    public override string Company { get; set; } = "Contoso";
    public override string Document { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
    public override string Version { get; set; } = "1.3.0";
}
```

注册：

```csharp
var appPathServce = new MyAppPathServce();
AppPaths.Register(appPathServce);
services.AddSingleton<IAppPathServce>(appPathServce);
```

生成目录示例：

```text
C:\Users\User\AppData\Local\Contoso\MyApp\1.3.0\Default\Setting
C:\Users\User\AppData\Local\Contoso\MyApp\1.3.0\Default\Cache
```

---

## 7. `AppDomianPaths` 说明

`AppDomianPaths` 定义的是应用程序运行目录下的静态资源目录约定。它与 `AppPathServce` 的用户文档目录不同。

源码：

```csharp
public static class AppDomianPaths
{
    public const string Assets = nameof(Assets);
    public static string DefaultTemplates => Path.Combine(Assets, "DefaultTemplates");
    public static string DefaultProjects => Path.Combine(DefaultTemplates, "Project");
    public static string DefaultSettings => Path.Combine(DefaultTemplates, "Setting");
    public static string Modules => "Modules";
    public static string Components => "Components";
    public static string Plugin => "Plugins";
    public static string Versions => "Versions";
}
```

路径含义：

| 属性 | 相对路径 | 典型用途 |
|---|---|---|
| `Assets` | `Assets` | 应用内置静态资源根目录。 |
| `DefaultTemplates` | `Assets\DefaultTemplates` | 默认模板根目录。 |
| `DefaultProjects` | `Assets\DefaultTemplates\Project` | 内置默认项目模板。 |
| `DefaultSettings` | `Assets\DefaultTemplates\Setting` | 内置默认设置模板。 |
| `Modules` | `Modules` | 模块目录。 |
| `Components` | `Components` | 组件目录。 |
| `Plugin` | `Plugins` | 插件目录。 |
| `Versions` | `Versions` | 版本文件或升级包目录。 |

获取绝对路径时通常与 `AppDomain.CurrentDomain.BaseDirectory` 拼接：

```csharp
var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
var defaultProjectPath = Path.Combine(baseDirectory, AppDomianPaths.DefaultProjects);
var pluginPath = Path.Combine(baseDirectory, AppDomianPaths.Plugin);
```

示例结果：

```text
{AppBase}\Assets\DefaultTemplates\Project
{AppBase}\Plugins
{AppBase}\Versions
```

---

## 8. `ToDefaultTemplatePath` 扩展方法

`AppDomianPathExtensions` 提供默认模板路径转换：

```csharp
public static string ToDefaultTemplatePath(this string path, string relativePath)
{
    var rpath = Path.GetRelativePath(relativePath, path);
    var folderName = Path.GetFileNameWithoutExtension(relativePath);
    return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppDomianPaths.DefaultTemplates, folderName, rpath);
}
```

它的作用是：把一个源路径映射到应用运行目录下的默认模板目录。

例如：

```csharp
var sourceFile = @"D:\Work\Templates\ProjectA\config.json";
var relativeRoot = @"D:\Work\Templates\ProjectA";
var targetFile = sourceFile.ToDefaultTemplatePath(relativeRoot);
```

生成目标路径：

```text
{AppBase}\Assets\DefaultTemplates\ProjectA\config.json
```

适用场景：

- 将工程模板复制到应用内置模板目录。
- 构建默认设置模板。
- 生成打包时使用的模板目标路径。

---

## 9. 清理缓存和设置

`AppPathExtension` 提供两个常用清理扩展：

```csharp
public static bool ClearCache(this IAppPathServce servce, out string message)
public static bool ClearSetting(this IAppPathServce servce, out string message)
```

### 9.1 清空缓存

```csharp
if (!AppPaths.Instance.ClearCache(out string message))
{
    // 显示错误信息
}
```

`ClearCache` 会删除 `IAppPathServce.Cache` 目录。

### 9.2 清空设置

```csharp
if (!AppPaths.Instance.ClearSetting(out string message))
{
    // 显示错误信息
}
```

`ClearSetting` 会删除：

- `Setting`
- `UserSetting`

### 9.3 清空缓存命令

`H.Extensions.AppPath` 中提供 `ClearCacheDataCommand`，可用于设置页面、菜单项或工具按钮。

---

## 10. 二次开发建议

### 10.1 统一从路径服务获取目录

不建议：

```csharp
var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MyApp", "Data");
```

建议：

```csharp
var path = AppPaths.Instance.Data;
```

或：

```csharp
public MyService(IAppPathServce appPath)
{
    _appPath = appPath;
}
```

### 10.2 写文件前确保目录存在

```csharp
Directory.CreateDirectory(AppPaths.Instance.UserData);
var file = Path.Combine(AppPaths.Instance.UserData, "data.json");
File.WriteAllText(file, json);
```

### 10.3 区分应用级和用户级数据

| 数据类型 | 推荐目录 |
|---|---|
| 全局配置 | `Config` |
| 全局设置 | `Setting` |
| 全局缓存 | `Cache` |
| 全局日志 | `Log` |
| 默认项目 | `Project` |
| 默认模板 | `Template` |
| 用户私有数据 | `UserData` |
| 用户私有设置 | `UserSetting` |
| 用户项目 | `UserProject` |
| 用户模板 | `UserTemplate` |
| 用户缓存 | `UserCache` |

### 10.4 登录前后的路径差异

由于 `UserPath` 依赖 `ILoginService.User.Account`：

- 未登录时：`UserPath == Default`。
- 登录后：`UserPath == {AppPath}\{UserName}` 或 `{AppPath}\{UserName}\{Version}`。

因此：

- 需要与用户绑定的数据，应在登录成功后再读取。
- 登录前初始化的数据，应放在应用级目录。
- 用户切换后，应重新加载用户级配置或项目列表。

---

## 11. 完整示例：自定义系统路径服务

```csharp
using H.Extensions.AppPath;
using H.Services.AppPath;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

public class MyAppPathServce : AppPathServce
{
    public override string Company { get; set; } = "Contoso";
    public override string Document { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
    public override string Version { get; set; } = "1.3.0";

    public override string RegistryPath => Path.Combine("SOFTWARE", Company, AppName);
}

public partial class App : ApplicationBase
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        var appPathServce = new MyAppPathServce();
        AppPaths.Register(appPathServce);
        services.AddSingleton<IAppPathServce>(appPathServce);

        // 其他服务注册...
    }

    protected override Window CreateMainWindow(StartupEventArgs e)
    {
        return new MainWindow();
    }
}
```

使用：

```csharp
Directory.CreateDirectory(AppPaths.Instance.UserProject);
var projectFile = Path.Combine(AppPaths.Instance.UserProject, "default.project");
```

---

## 12. 常见问题

### `AppPathServce is not registered.`

原因：访问 `AppPaths.Instance` 前未调用：

```csharp
AppPaths.Register(appPathServce);
```

解决：在应用启动阶段注册 `IAppPathServce`，并确保早于其他服务使用路径。

### 修改 `Company` 后目录没有变化

`AppPathServce` 构造函数会创建目录。如果创建后再修改 `Company`，后续属性会返回新路径，但旧目录已经被创建。正式应用建议通过派生类覆盖默认值，确保构造时就是正确配置。

### 用户目录仍然指向 `Default`

原因：当前未登录，或 `ILoginService.User` 为空。

解决：确认登录服务已注册且登录成功后 `User.Account` 有值。

### 清空缓存失败

常见原因：

- 缓存文件正在被占用。
- 当前进程没有目录删除权限。
- `Cache` 目录不存在或路径异常。

处理建议：关闭占用文件的流，必要时在下次启动时清理。

### `AppDomianPaths` 和 `AppPaths` 应该怎么选

- 使用 `AppDomianPaths`：读取应用安装目录下随程序发布的静态资源，如默认模板、插件、模块、版本包。
- 使用 `AppPaths.Instance`：读写运行时产生的数据，如设置、日志、项目、缓存、用户数据。
