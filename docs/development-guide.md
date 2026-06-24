# WPF-Control 开发文档
# WPF-Control 开发文档

本文面向 WPF-Control 的二次开发者、维护者和模块贡献者，说明解决方案结构、技术栈、应用启动、依赖注入、常用模块、核心概念、扩展方式和开发规范。

## 目录

- 主要特性
- 技术栈
- 文档导读
- 目录与解决方案结构
- 环境要求
- 快速开始
- 常用模块说明
- 应用启动、依赖注入与配置
- 核心概念与主要模块
- 详细开发示例
- 测试建议
- 重构路线建议
- 开发建议
- 许可

## 主要特性

- 基于 WPF 的控件、主题、样式、服务和业务模块集合。
- 面向桌面应用提供 MVVM、依赖注入、消息服务、主题切换、项目文件、流程图、表单、权限、升级、打印等能力。
- 采用多程序集拆分方式，将基础能力、扩展能力、控件、模块、服务、主题和示例测试分层维护。
- 支持通过 `Microsoft.Extensions.DependencyInjection` 管理服务和模块依赖。
- 支持通过 `ResourceDictionary`、`MarkupExtension` 和 Pack URI 组织主题资源。
- 支持通过 `DataTemplate`、Presenter 和数据模型构建数据驱动 UI。
- 支持 Diagram 节点、节点分组、流程图保存、项目文件和可视化交互场景。

## 技术栈

| 类型 | 技术 |
|---|---|
| 平台 | `.NET 8` / Windows Desktop |
| UI | WPF、XAML、`ResourceDictionary`、`ControlTemplate`、`DataTemplate` |
| 架构 | MVVM、模块化程序集、服务抽象、Presenter 模式 |
| DI | `Microsoft.Extensions.DependencyInjection` |
| MVVM | `H.Mvvm`、`H.Extensions.Mvvm`、`CommunityToolkit.Mvvm` |
| 行为 | `Microsoft.Xaml.Behaviors.Wpf` |
| 序列化 | `System.Text.Json`、`Newtonsoft.Json` |
| 数据库 | SQLite、SQL Server、Repository 扩展 |
| 图形 | Diagram、Chart2D、Geometry、Adorner、ZoomBox、ROIBox |
| 主题 | `H.Theme`、`H.Style`、`H.Styles.*`、`H.Themes.Colors.*` |

## 文档导读

建议按角色阅读：

- 使用者：先读环境要求、快速开始、常用模块说明，再运行 `Source/Tests` 下对应示例。
- 二次开发者：重点阅读应用启动、依赖注入、核心概念和详细开发示例。
- 维护者：重点阅读目录结构、测试建议、重构路线、开发建议和兼容性说明。

## 目录与解决方案结构

| 目录 | 定位 | 说明 |
|---|---|---|
| `Source/Base` | 基础层 | 附加属性、MarkupExtension、MVVM、ValueConverter、全球化基础能力 |
| `Source/Common` | 公共层 | 通用接口、常量、基础公共能力 |
| `Source/Services` | 服务抽象层 | 消息、路径、项目、设置、日志、身份、序列化等服务抽象或默认入口 |
| `Source/Extensions` | 扩展实现层 | 应用启动、MVVM、数据库、Json、动画、行为、路径等可复用扩展 |
| `Source/Controls` | 控件层 | Form、Dock、Diagram、Chart2D、PDF、QRCode、TreeListView 等控件 |
| `Source/Presenters` | Presenter 层 | 数据驱动 UI 组件和通用 Presenter |
| `Source/Modules` | 业务模块层 | Theme、Project、Login、Identity、Setting、License、Guide、Messages 等模块 |
| `Source/Themes` | 主题层 | `H.Theme` 基础主题与颜色主题扩展 |
| `Source/Styles` | 样式层 | 控件样式和风格资源 |
| `Source/ApplicationBases` | 应用组合层 | 默认应用、主题应用、模块应用、身份应用等应用基类 |
| `Source/Windows` | 窗口层 | Dialog、Ribbon、Dock、MainWindow 相关能力 |
| `Source/Templates` | 模板层 | 项目模板、控件模板、默认模板 |
| `Source/Tests` | 测试与示例 | 每个控件或模块的最小示例项目 |
| `Source/App` | 应用示例 | 完整应用或集成示例 |

推荐依赖方向：

```text
App / Tests
  -> ApplicationBases
  -> Modules
  -> Windows / Presenters / Controls
  -> Services / Extensions
  -> Base / Common
```

不推荐：`Base` 依赖 `Controls`，通用控件依赖具体业务模块，多个控件程序集互相循环引用。

## 环境要求

- Windows 10 或 Windows 11。
- Visual Studio 2022。
- `.NET 8 SDK`，并安装 Windows Desktop workload。
- NuGet 可访问外部包源。
- 推荐启用 XAML 设计器、WPF UI Debugging Tools。

## 快速开始

```powershell
git clone https://github.com/HeBianGu/WPF-Control.git
cd WPF-Control
dotnet restore
```

构建单个项目：

```powershell
dotnet build Source/Themes/H.Theme/H.Theme.csproj
dotnet build Source/Controls/H.Controls.Diagram/H.Controls.Diagram.csproj
```

建议优先运行 `Source/Tests` 下示例：

- `H.Test.Theme`：主题相关示例。
- `H.Test.Diagram`：流程图相关示例。
- `H.Test.Form`：表单相关示例。
- `H.Test.PropertyGrid`：属性网格示例。
- `H.Test.Dock`：Dock 布局示例。

## 常用模块说明

| 模块 | 用途 | 场景 |
|---|---|---|
| `H.Theme` | 基础主题资源 | 颜色、画刷、字体、布局、背景、主题切换 |
| `H.Style` / `H.Styles.*` | 控件样式 | 完整控件风格、主题风格 |
| `H.Mvvm` | MVVM 基础 | 命令、绑定基类、基础 ViewModel |
| `H.Extensions.ApplicationBase` | 应用启动 | DI、配置、应用生命周期 |
| `H.Services.Message` | 消息服务 | 对话框、通知、确认、Snack、Notice |
| `H.Services.AppPath` / `H.Extensions.AppPath` | 路径服务 | 配置目录、缓存目录、日志目录、数据目录 |
| `H.Services.Project` / `H.Modules.Project` | 项目系统 | 项目文件、最近项目、加载保存 |
| `H.Extensions.NewtonsoftJson` | JSON 序列化 | 项目文件、复杂类型、自定义转换器 |
| `H.Controls.Form` | 表单控件 | 设置页面、参数编辑、对象属性编辑 |
| `H.Controls.Diagram` | 流程图控件 | 节点、连线、交互、缩放、编辑 |
| `H.Controls.Diagram.Presenter` | 数据驱动流程图 | NodeData、NodeDataGroup、DataTemplate、Presenter |
| `H.Controls.Adorner` | 装饰层 | 选中框、拖拽辅助、标注 |
| `H.Controls.ZoomBox` | 缩放视图 | 大画布、图像、流程图、设计器 |
| `H.Controls.Dock` | Dock 布局 | IDE 类布局、多文档窗口、工具窗口 |

## 应用启动、依赖注入与配置

应用启动相关代码主要位于 `Source/Extensions/H.Extensions.ApplicationBase` 和 `Source/ApplicationBases`。

典型启动流程：

1. `App` 继承某个 `ApplicationBase`。
2. 创建 `IServiceCollection`。
3. 调用 `ConfigureServices` 注册服务。
4. 构建服务容器。
5. 调用 `Configure` 应用配置。
6. 加载主题、设置、模块和主窗口。

### 应用基类示例

```csharp
using H.ApplicationBases.Themes;
using H.Extensions.ApplicationBase;
using Microsoft.Extensions.DependencyInjection;

public partial class App : ThemesApplicationBase
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);
        services.AddMyApplicationServices();
    }

    protected override void Configure(IApplicationBuilder app)
    {
        base.Configure(app);
        app.UseMyApplicationOptions();
    }
}
```

### 服务注册示例

```csharp
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

public interface IExportService
{
    Task ExportAsync(string filePath, CancellationToken cancellationToken = default);
}

public class ExportService : IExportService
{
    public async Task ExportAsync(string filePath, CancellationToken cancellationToken = default)
    {
        await File.WriteAllTextAsync(filePath, "export data", cancellationToken);
    }
}

public static class ExportServiceExtensions
{
    public static IServiceCollection AddExportServices(this IServiceCollection services)
    {
        services.TryAddSingleton<IExportService, ExportService>();
        return services;
    }
}
```

### 应用配置示例

```csharp
using H.Extensions.ApplicationBase;

public static class ExportApplicationBuilderExtensions
{
    public static IApplicationBuilder UseExportOptions(this IApplicationBuilder app)
    {
        // 加载配置、初始化默认目录、应用模块选项。
        return app;
    }
}
```

## 核心概念与主要模块

### AppPaths：应用目录与文件路径管理

`AppPaths` 位于 `H.Services.AppPath`，用于提供全局应用路径服务入口。实际路径计算和目录初始化通常由 `H.Extensions.AppPath` 中的服务实现完成。

不推荐在业务代码中散落路径拼接：

```csharp
string file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config", "app.json");
```

推荐统一通过路径服务：

```csharp
public class MyConfigStore
{
    public string GetConfigFile()
    {
        string root = AppPaths.Instance.UserDataPath;
        return Path.Combine(root, "Config", "app.json");
    }
}
```

路径建议：配置文件放配置目录，缓存放缓存目录，日志放日志目录，项目文件保存相对路径。

### Project：项目与工程文件

项目模块由 `H.Services.Project` 和 `H.Modules.Project` 共同组成。

核心概念：

- `ProjectSaveMode.OnAppExit`：应用退出时保存项目。
- `ProjectSaveMode.OnProjectChanged`：项目变化时保存项目。
- `ProjectItemBase`：项目项基础类型。
- `ProjectServiceBase`：项目加载、保存、切换流程。

项目模型示例：

```csharp
public class MyProject
{
    public string Name { get; set; } = "未命名项目";
    public string Version { get; set; } = "1.0";
    public ObservableCollection<MyProjectItem> Items { get; set; } = new();
}

public class MyProjectItem
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string DisplayName { get; set; } = string.Empty;
    public string RelativePath { get; set; } = string.Empty;
}
```

项目文件建议：保存版本号、保存必要状态、不保存 UI 控件、路径优先相对路径、新增字段提供默认值。

### Newtonsoft.Json：项目序列化与反序列化

`H.Extensions.NewtonsoftJson` 提供基于 `Newtonsoft.Json` 的 `IJsonSerializerService` 实现。

```csharp
using H.Services.Serializable;

public class ProjectStore
{
    private readonly IJsonSerializerService _serializer;

    public ProjectStore(IJsonSerializerService serializer)
    {
        _serializer = serializer;
    }

    public void Save(string file, MyProject project)
    {
        string json = _serializer.Serialize(project);
        File.WriteAllText(file, json);
    }

    public MyProject Load(string file)
    {
        string json = File.ReadAllText(file);
        return _serializer.Deserialize<MyProject>(json);
    }
}
```

自定义转换器示例：

```csharp
public class MyNodeConverter : Newtonsoft.Json.JsonConverter<MyNode>
{
    public override MyNode ReadJson(JsonReader reader, Type objectType, MyNode existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        JObject obj = JObject.Load(reader);
        return new MyNode
        {
            Id = obj.Value<string>("id") ?? Guid.NewGuid().ToString(),
            Name = obj.Value<string>("name") ?? string.Empty
        };
    }

    public override void WriteJson(JsonWriter writer, MyNode value, JsonSerializer serializer)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("id");
        writer.WriteValue(value.Id);
        writer.WritePropertyName("name");
        writer.WriteValue(value.Name);
        writer.WriteEndObject();
    }
}
```

### Diagram：视觉流程图

流程图体系主要位于：`H.Controls.Diagram`、`H.Controls.Diagram.Extension`、`H.Controls.Diagram.Presenter`、`H.Controls.Diagram.Presenters.Workflow`。

推荐三层理解：

1. 控件层：节点、连线、选择、拖拽、缩放、布局。
2. 数据层：节点数据、连线数据、图数据、保存加载。
3. Presenter 层：NodeData、NodeDataGroup、DataTemplate、工具箱、工作流展示。

### NodeData：功能节点

`NodeData` 用于描述一个可拖拽、可显示、可保存的流程图节点。

```csharp
public class DelayNodeData : NodeData
{
    public int Milliseconds { get; set; } = 1000;

    public DelayNodeData()
    {
        this.Name = "延时";
        this.Description = "等待指定毫秒数后继续执行";
    }
}
```

建议节点数据包含 `Id`、`Name`、`GroupName`、`Icon`、`Description`、输入输出端口和可序列化参数。

### NodeDataGroup：节点资源分组

`NodeDataGroup` 位于 `H.Controls.Diagram.Presenter.Provider`，用于组织一组可用节点资源。典型用途包括流程图左侧工具箱、按业务领域分组节点、按算法或动作类型分组节点。

扩展方式：继承 `NodeDataGroupBase`，重写 `CreateNodeDatas()` 返回节点集合。

### 绘图、标注与交互模块

相关程序集：`H.Controls.Adorner`、`H.Controls.ROIBox`、`H.Controls.ZoomBox`、`H.Extensions.Geometry`、`H.Controls.Diagram`。

建议：高频交互避免在 `MouseMove` 中创建大量对象；可冻结的 `Brush`、`Pen`、`Geometry` 优先 `Freeze()`；大图形考虑虚拟化、分层渲染和延迟加载。

### DataTemplate 与 Presenter

数据驱动 UI 的核心思想是：数据对象描述状态，`DataTemplate` 决定显示方式，Presenter 组织数据和命令。

```csharp
public class UserCardPresenter
{
    public string Name { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public ICommand OpenCommand { get; set; }
}
```

```xml
<DataTemplate DataType="{x:Type local:UserCardPresenter}">
    <Border Padding="8" CornerRadius="4" Background="{DynamicResource S.Brush.Card}">
        <StackPanel>
            <TextBlock Text="{Binding Name}" FontWeight="Bold" />
            <TextBlock Text="{Binding Role}" Opacity="0.7" />
            <Button Content="打开" Command="{Binding OpenCommand}" />
        </StackPanel>
    </Border>
</DataTemplate>
```

### Form 表单组件

`H.Controls.Form` 和 `H.Controls.Form.PropertyItem` 适合设置页、参数编辑、对象属性编辑和 Diagram 节点属性面板。

```csharp
public class ExportOptions
{
    [Display(Name = "文件名")]
    public string FileName { get; set; } = "output.png";

    [Display(Name = "宽度")]
    [Range(100, 10000)]
    public int Width { get; set; } = 1920;

    [Display(Name = "高度")]
    [Range(100, 10000)]
    public int Height { get; set; } = 1080;
}
```

```xml
<!-- 具体控件属性以 H.Controls.Form 公开 API 为准 -->
<h:Form DataContext="{Binding ExportOptions}" />
```

开发建议：

- 简单属性优先使用 `DisplayAttribute`、`RangeAttribute` 等数据注解。
- 复杂编辑器拆成独立 PropertyItem。
- 表单控件只负责编辑，不负责保存业务。
- 保存逻辑放到 ViewModel 或服务中。

### IocMessage 消息服务

`IocMessage` 用于让 ViewModel 发起消息、确认框和对话框，而不直接创建窗口。

```csharp
public class SaveViewModel
{
    public async Task SaveAsync()
    {
        bool success = await SaveCoreAsync();
        if (success)
        {
            await IocMessage.ShowSnackInfo("保存成功");
        }
    }

    private Task<bool> SaveCoreAsync()
    {
        return Task.FromResult(true);
    }
}
```

```csharp
public class OpenSettingsViewModel
{
    public async Task OpenAsync()
    {
        object view = Ioc.Services.GetService(typeof(SettingsView));
        await IocMessage.ShowDialog(view, x =>
        {
            x.Title = "系统设置";
            x.DialogButton = DialogButton.Sumit;
        });
    }
}
```

建议：ViewModel 不直接 new Window；对话框内容注册到 DI 容器；消息展示统一走消息服务。

## 详细开发示例

此节提供更贴近二次开发的示例，示例代码用于说明推荐写法。

### 新增自定义控件

控件类：

```csharp
[TemplatePart(Name = PART_Content, Type = typeof(ContentPresenter))]
public class Badge : ContentControl
{
    private const string PART_Content = "PART_Content";

    static Badge()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Badge), new FrameworkPropertyMetadata(typeof(Badge)));
    }

    public static readonly DependencyProperty CountProperty = DependencyProperty.Register(
        nameof(Count),
        typeof(int),
        typeof(Badge),
        new FrameworkPropertyMetadata(0));

    public int Count
    {
        get => (int)GetValue(CountProperty);
        set => SetValue(CountProperty, value);
    }
}
```

默认样式：

```xml
<Style TargetType="{x:Type local:Badge}">
    <Setter Property="Template">
        <Setter.Value>
            <ControlTemplate TargetType="{x:Type local:Badge}">
                <Grid>
                    <ContentPresenter x:Name="PART_Content" Content="{TemplateBinding Content}" />
                    <Border HorizontalAlignment="Right" VerticalAlignment="Top" CornerRadius="8" Background="Red">
                        <TextBlock Margin="4,0" Foreground="White" Text="{TemplateBinding Count}" />
                    </Border>
                </Grid>
            </ControlTemplate>
        </Setter.Value>
    </Setter>
</Style>
```

### 主题资源使用

引入基础主题：

```xml
<ResourceDictionary.MergedDictionaries>
    <ResourceDictionary Source="pack://application:,,,/H.Theme;component/ColorKeys.xaml" />
    <ResourceDictionary Source="pack://application:,,,/H.Theme;component/BrushKeys.xaml" />
    <ResourceDictionary Source="pack://application:,,,/H.Theme;component/FontSizeKeys.xaml" />
    <ResourceDictionary Source="pack://application:,,,/H.Theme;component/LayoutKeys.xaml" />
</ResourceDictionary.MergedDictionaries>
```

动态主题切换：

```csharp
FontSizeThemeType.Large.ChangeFontSizeThemeType();
LayoutThemeType.Small.ChangeLayoutThemeType();
ThemeTypeExtension.RefreshBrushResourceDictionary();
```

资源使用示例：

```xml
<Border
    Background="{DynamicResource S.Brush.Background}" 
    CornerRadius="{DynamicResource S.CornerRadius.Default}">
    <TextBlock
        Foreground="{DynamicResource S.Brush.Foreground}"
        FontSize="{DynamicResource S.FontSize.Default}"
        Text="主题化文本" />
</Border>
```

建议：

- 支持主题切换的资源使用 `DynamicResource`。
- 不随主题变化的资源使用 `StaticResource`。
- 公共资源 Key 不要随意改名。
- 废弃资源时优先保留旧 Key，并在文档中标记为废弃。

### 程序集 README 模板

后续每个程序集建议增加 `README.md`，模板如下：

```markdown
# H.Xxx

## 简介
说明程序集职责和适用场景。

## 依赖关系
| 依赖 | 用途 |
|---|---|
| H.Mvvm | MVVM 基础能力 |

## 快速开始
给出最小 XAML 或 C# 示例。

## 核心类型
| 类型 | 说明 |
|---|---|
| XxxService | Xxx 服务 |

## 服务注册
说明 `AddXxxServices()` 和 `UseXxxOptions()`。

## 主题资源
说明资源字典、Pack URI 和资源 Key。

## 扩展点
说明接口、基类、模板部件、DataTemplate 和 Presenter。

## 测试建议
说明应覆盖的测试和示例项目。

## 兼容性说明
说明公开 API、资源 Key、XAML 命名空间的兼容要求。
```

## 测试建议

### 控件测试

- 控件可实例化。
- 默认样式可加载。
- `OnApplyTemplate` 不抛异常。
- 依赖属性默认值正确。
- 依赖属性变更回调正确。

### 主题测试

- 所有 Pack URI 可加载。
- 资源字典无重复 Key。
- 深色和浅色主题 Key 集合一致。
- 字体尺寸、布局尺寸枚举值都有对应 XAML。

### 服务测试

- 服务可注册。
- 服务可从 DI 容器解析。
- 默认配置可用。
- 异常场景有明确错误信息。

### 项目文件测试

- 新项目可创建。
- 项目可保存。
- 项目可加载。
- 旧版本项目文件可兼容。
- 相对路径可正确解析。

## 重构路线建议

1. 补齐每个程序集 `README.md`。
2. 梳理程序集依赖方向，消除循环依赖。
3. 整理主题资源，稳定公共资源 Key。
4. 为核心控件补充最小测试项目。
5. 为项目文件和序列化补充兼容性测试。
6. 按使用频率逐个优化控件模板和性能。

## 开发建议

- 保持程序集职责单一，避免基础层反向依赖控件层或模块层。
- 公开 API、资源 Key、XAML 命名空间和 Pack URI 需要保持兼容。
- 控件模板中的可主题化资源优先使用 `DynamicResource`。
- 大型 `ResourceDictionary` 应按控件或主题拆分。
- ViewModel 不直接创建窗口，优先使用 `IocMessage` 或对话框服务。
- 项目文件和配置文件应保持向后兼容。
- 新增程序集建议同步增加 `README.md`。
- 新增模块建议提供最小可运行测试项目。

## 兼容性清单

修改以下内容前需要评估兼容性：

- 公开类名、接口名、方法名。
- XAML 命名空间。
- Pack URI 路径。
- 资源 Key。
- 枚举成员名称。
- 项目文件 JSON 字段。
- DI 注册服务类型。
- 默认样式 TargetType。

推荐做法：新增优先于删除，废弃优先于直接移除，保留旧资源 Key 并转发到新资源，为项目文件增加版本迁移逻辑。

## 许可

项目源码文件头部声明使用 MIT License。二次开发、分发或引用第三方组件时，应同时遵守本项目许可和对应第三方库许可。
