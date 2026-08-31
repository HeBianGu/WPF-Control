**适用项目：** `H.Modules.Plugin`  
**核心类型：** `PluginAttribute`、`PluginManager`、`IPluginService`、`PluginOptions`、`PluginManagerPresenter`  
**相关能力：** 插件程序集发现、插件服务注册、插件配置初始化、插件信息展示

本文介绍 `H.Modules.Plugin` 的插件约定、宿主注册、插件项目开发和部署方式。

---

## 1. 模块定位

`H.Modules.Plugin` 采用基于目录和程序集特性的插件发现方式：

1. 扫描应用运行目录下的 `Plugins` 文件夹及其所有子目录。
2. 使用 `Assembly.LoadFrom()` 加载扫描到的 `.dll` 文件。
3. 只将带有程序集级 `PluginAttribute` 的程序集识别为插件。
4. 查找插件程序集中的指定接口实现，并通过无参构造函数创建实例。
5. 宿主注册服务时调用插件的 `IPluginService.AddPluginService()`。
6. 宿主配置应用时调用插件的 `IPluginService.UsePluginOptions()`。

默认插件目录由以下常量定义：

```csharp
AppDomianPaths.Plugin
```

当前值为：

```text
Plugins
```

它是相对于应用运行目录的路径。典型部署结构如下：

```text
MyApp.exe
Plugins/
└── ReportPlugin/
    ├── MyApp.Plugin.Report.dll
    └── 插件私有依赖.dll
```

---

## 2. 宿主项目引用与注册

宿主项目引用：

```xml
<ItemGroup>
  <ProjectReference Include="..\..\Modules\H.Modules.Plugin\H.Modules.Plugin.csproj" />
</ItemGroup>
```

在基于 `ApplicationBase` 的应用中注册插件模块：

```csharp
using H.Extensions.ApplicationBase;
using H.Modules.Plugin;
using Microsoft.Extensions.DependencyInjection;

public partial class App : ApplicationBase
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);
        services.AddPlugin();
    }

    protected override void Configure(IApplicationBuilder app)
    {
        base.Configure(app);
        app.UsePluginOptions();
    }
}
```

调用顺序非常重要：

- `AddPlugin()` 必须在依赖注入容器构建前执行，插件才能向 `IServiceCollection` 注册服务。
- `UsePluginOptions()` 在应用配置阶段执行，用于调用各插件的配置入口，并将 `PluginOptions.Instance` 加入设置系统。
- 插件 DLL 必须在调用 `AddPlugin()` 前已经位于 `Plugins` 目录中。

`IPluginOptions` 当前没有可配置成员；`PluginOptions.PluginPath` 是只读展示属性，返回默认插件目录。

---

## 3. 创建插件项目

建议为每个插件创建独立的 .NET 8 WPF 类库，并引用 `H.Modules.Plugin`。插件和宿主应使用兼容的目标框架与公共依赖版本。

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net8.0-windows</TargetFramework>
    <UseWPF>true</UseWPF>
    <Nullable>disable</Nullable>
  </PropertyGroup>

  <ItemGroup>
    <ProjectReference Include="..\..\Modules\H.Modules.Plugin\H.Modules.Plugin.csproj" />
  </ItemGroup>
</Project>
```

插件可以只提供可发现的业务类型，也可以实现 `IPluginService` 参与宿主的服务注册和应用配置。

---

## 4. 标记插件程序集

每个插件程序集必须添加 `PluginAttribute`。通常在插件项目的 `AssemblyInfo.cs` 中声明：

```csharp
using H.Modules.Plugin.Base;

[assembly: Plugin(
    name: "报表插件",
    groupName: "业务插件",
    decription: "提供报表生成和导出功能")]
```

`PluginAttribute` 的属性如下：

| 属性 | 说明 |
|---|---|
| `Name` | 插件显示名称。 |
| `GroupName` | 插件分组名称。 |
| `Description` | 插件说明。 |

构造函数第三个参数的当前 API 名称是 `decription`，使用命名参数时必须采用该拼写；赋值后的公开属性名称仍为 `Description`。

未添加 `PluginAttribute` 的程序集即使位于 `Plugins` 目录中，也不会出现在 `PluginManager.GetPluginAssemblies()` 的结果中。

---

## 5. 注册插件服务

需要参与宿主初始化的插件应提供一个公开、非抽象且带无参构造函数的 `IPluginService` 实现：

```csharp
using H.Modules.Plugin.Base;
using Microsoft.Extensions.DependencyInjection;

namespace MyApp.Plugin.Report;

public sealed class ReportPluginService : IPluginService
{
    public IServiceCollection AddPluginService(IServiceCollection services)
    {
        services.AddSingleton<IReportService, ReportService>();
        return services;
    }

    public IApplicationBuilder UsePluginOptions(IApplicationBuilder builder)
    {
        return builder;
    }
}
```

两个入口的职责：

| 方法 | 调用阶段 | 适合执行的操作 |
|---|---|---|
| `AddPluginService(IServiceCollection)` | 宿主服务注册阶段 | 注册服务、ViewModel、Presenter 和业务接口实现。 |
| `UsePluginOptions(IApplicationBuilder)` | 宿主应用配置阶段 | 注册插件设置项、应用插件配置。 |

`PluginManager` 使用 `Activator.CreateInstance()` 创建 `IPluginService`，因此：

- 实现类型必须是公开的具体类。
- 必须提供可访问的无参构造函数。
- 构造函数不能依赖宿主的依赖注入服务。
- 两个初始化阶段会重新扫描并创建实例，不应依赖同一个 `IPluginService` 对象保存阶段间状态。

需要共享状态时，应在 `AddPluginService()` 中注册单例服务，再由正常业务对象通过依赖注入使用。

---

## 6. 定义和发现插件扩展点

除 `IPluginService` 外，宿主也可以定义自己的公共扩展接口。该接口应位于宿主和插件共同引用的契约程序集内。

契约：

```csharp
public interface IReportProvider
{
    string Name { get; }
    void Export(string filePath);
}
```

插件实现：

```csharp
public sealed class ExcelReportProvider : IReportProvider
{
    public string Name => "Excel";

    public void Export(string filePath)
    {
        // 生成报表。
    }
}
```

宿主发现实例：

```csharp
IEnumerable<IReportProvider> providers =
    PluginManager.GetPluginInstances<IReportProvider>();
```

发现规则与 `IPluginService` 相同：实现类型必须是非抽象类，并且可通过无参构造函数创建。

如果扩展对象需要依赖注入，优先让 `IPluginService.AddPluginService()` 将实现注册到容器中，而不是直接使用 `GetPluginInstances<T>()` 创建业务对象。

---

## 7. 插件配置

插件可以定义自己的 Options，并在配置阶段加入框架设置系统。例如：

```csharp
[Display(Name = "报表插件设置")]
public sealed class ReportPluginOptions :
    IocOptionInstance<ReportPluginOptions>
{
    public string ExportDirectory { get; set; } = "Reports";
}
```

插件扩展方法：

```csharp
public static class ReportPluginExtensions
{
    public static IApplicationBuilder UseReportPluginOptions(
        this IApplicationBuilder builder)
    {
        IocSetting.Instance.Add(ReportPluginOptions.Instance);
        return builder;
    }
}
```

在插件入口中调用：

```csharp
public IApplicationBuilder UsePluginOptions(IApplicationBuilder builder)
{
    return builder.UseReportPluginOptions();
}
```

插件设置应遵循 [`development-guide-setting.md`](development-guide-setting.md) 中的持久化约定。

---

## 8. 插件管理界面

`AddPlugin()` 会注册：

```text
IPluginManagerPresenter -> PluginManagerPresenter
```

可以通过 `ShowPluginManagerCommand` 打开插件管理界面：

```xaml
<Button Command="{h:ShowPluginManagerCommand}" Content="插件管理" />
```

管理界面通过 `PluginManager.GetPluginAssemblies()` 读取插件，并展示：

- 名称、分组和说明。
- DLL 相对路径。
- 程序集全名。
- 文件访问时间。
- 文件大小。

该界面用于查看当前扫描到的插件，不负责安装、卸载、启用、禁用或热更新插件。

使用命令前，宿主还应按应用基类约定注册相应的消息和对话框能力。详细说明见 [`development-guide-message.md`](development-guide-message.md)。

---

## 9. 插件部署

构建插件后，将插件主程序集及其私有依赖复制到宿主输出目录的 `Plugins` 文件夹。扫描会递归处理子目录，因此建议每个插件使用独立目录：

```text
Plugins/
├── ReportPlugin/
│   ├── MyApp.Plugin.Report.dll
│   └── ReportEngine.dll
└── DevicePlugin/
    ├── MyApp.Plugin.Device.dll
    └── DeviceSdk.dll
```

部署时注意：

1. 不要把调试符号、文档文件或非托管 DLL 误认为插件入口；扫描器会尝试加载目录中的所有 `.dll`。
2. 插件依赖必须能够从应用目录、插件目录或默认程序集加载上下文中解析。
3. 宿主、契约程序集和插件使用的公共依赖应保持版本兼容。
4. 插件在应用启动阶段加载，更新 DLL 前应先退出应用。
5. 插件目录不存在时扫描结果为空，不会自动创建目录。

---

## 10. 加载流程

宿主启动时的主要流程如下：

```text
ConfigureServices
    ↓
services.AddPlugin()
    ↓
递归扫描 Plugins/*.dll
    ↓
Assembly.LoadFrom()
    ↓
筛选带 PluginAttribute 的程序集
    ↓
创建 IPluginService 实例
    ↓
AddPluginService(services)
    ↓
构建依赖注入容器
    ↓
Configure
    ↓
app.UsePluginOptions()
    ↓
重新扫描并创建 IPluginService 实例
    ↓
UsePluginOptions(builder)
```

`PluginManager.GetPluginAssemblies()` 会捕获读取插件特性时的异常并写入 `IocLog.Error()`；程序集文件加载和类型创建仍可能抛出异常。生产插件应在发布前验证依赖完整性和类型构造逻辑。

---

## 11. 当前实现边界

当前插件机制是轻量级启动期扩展，不是隔离式插件运行时：

- 使用默认加载上下文中的 `Assembly.LoadFrom()`。
- 不创建独立 `AssemblyLoadContext`。
- 不支持卸载程序集。
- 不支持运行时热加载或热更新。
- 不提供插件版本冲突隔离。
- 不提供插件启用、禁用或依赖关系排序。
- 扫描和实例化顺序不应作为业务顺序依赖。
- 插件代码与宿主进程拥有相同权限，不应加载不受信任的程序集。

需要隔离、卸载或安全边界时，应在现有模块之外增加自定义 `AssemblyLoadContext`、进程隔离或明确的插件清单与版本策略。

---

## 12. 常见问题

### 插件没有被发现

检查：

1. DLL 是否位于应用运行目录下的 `Plugins` 或其子目录。
2. 插件程序集是否声明了 `[assembly: Plugin(...)]`。
3. 插件及其依赖是否与宿主目标框架和版本兼容。
4. 是否在应用启动早期调用了 `services.AddPlugin()`。
5. 日志中是否存在程序集加载或特性读取异常。

### `IPluginService` 没有执行

检查实现类型是否：

- 实现了 `IPluginService`。
- 是非抽象类。
- 具有可访问的无参构造函数。
- 位于带 `PluginAttribute` 的程序集内。

### 插件服务无法从容器解析

确认服务是在 `AddPluginService()` 中注册，而不是在 `UsePluginOptions()` 中注册。后者执行时容器通常已经构建完成。

### 插件依赖加载失败

将插件私有依赖一并部署，并避免在不同插件中携带互不兼容的同名公共程序集。当前实现没有独立加载上下文，无法隔离依赖版本冲突。

### 替换插件 DLL 后仍使用旧版本

程序集加载后不能由当前机制卸载。退出宿主进程、替换文件后再重新启动应用。
