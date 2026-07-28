# H.Modules.Guide 开发文档

**适用项目：** `H.Modules.Guide`、`H.Attach`、`H.Services.Common.Guide`  
**核心类型：** `IGuideService`、`GuideService`、`GuideOptions`、`GuideBoxAdorner`、`GuideTree`  
**相关能力：** 新手引导、控件高亮、步骤导航、版本新增功能、启动自动展示、引导树

本文介绍 `H.Modules.Guide` 模块的服务注册、附加属性、Adorner 覆盖层、版本筛选、命令和二次开发约定。

---

## 1. 模块定位

`H.Modules.Guide` 用于在现有 WPF 窗口上叠加新手引导。开发者通过 `Cattach` 附加属性标记需要说明的 `UIElement`，然后由 `IGuideService` 查找、排序并显示引导覆盖层。

基本流程：

```text
XAML 为目标控件设置 Cattach.UseGuide 和说明数据
    ↓
IGuideService.Show(...)
    ↓
查找主窗口（或指定 Owner）中的可引导元素
    ↓
GuideBoxAdorner 加入 AdornerLayer
    ↓
依次高亮目标元素并显示标题、说明和操作
    ↓
完成或关闭后移除 Adorner
```

典型场景：

- 首次使用功能的分步引导。
- 版本升级后的新增功能介绍。
- 复杂设置页、编辑器、Diagram 工具栏的使用说明。
- 用户主动从“帮助”或“新增功能”菜单打开的功能导览。

不适合：

- 作为权限提示或安全确认机制。
- 替代表单校验、错误提示或完整帮助中心。
- 覆盖尚未加载、不可见或虚拟化未生成的元素。

---

## 2. 相关项目职责

| 项目 | 职责 |
|---|---|
| `H.Modules.Guide` | 引导服务、Adorner、引导树、命令、Options 和模板。 |
| `H.Attach` | `Cattach.UseGuide`、标题、说明、版本等附加属性。 |
| `H.Services.Common.Guide` | `IGuideService` 服务契约。 |
| `H.Controls.Adorner` | `GuideBoxAdorner` 使用的底层 Adorner 基类。 |
| `H.Test.Guide` | 最小运行示例与命令用法参考。 |

---

## 3. 注册模块

在应用服务注册阶段添加 Guide：

```csharp
protected override void ConfigureServices(IServiceCollection services)
{
    services.AddGuide();
}
```

在应用配置阶段注册设置项：

```csharp
protected override void Configure(IApplicationBuilder app)
{
    base.Configure(app);

    app.UseGuideOptions(options =>
    {
        options.UseOnLoad = true;
    });
}
```

`AddGuide()` 注册默认 `IGuideService -> GuideService` 和 Guide 相关 Options；`UseGuideOptions()` 将 `GuideOptions.Instance` 加入设置系统。

应用启动、主窗口加载后，`IGuideService` 作为 `IMainWindowLoadedLoadable` 参与加载流程。`GuideService.Load()` 会根据 `GuideOptions.UseOnLoad` 决定是否自动显示版本引导。

---

## 4. 最小 XAML 示例

```xaml
<Window
    x:Class="MyApp.MainWindow"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:h="https://github.com/HeBianGu">
    <Grid>
        <Button
            Width="120"
            Height="36"
            h:Cattach.GuideData="点击此处创建一个新项目。"
            h:Cattach.GuideTitle="新建项目"
            h:Cattach.UseGuide="True"
            Content="新建项目" />
    </Grid>
</Window>
```

代码中显示当前窗口内全部符合条件的引导项：

```csharp
await Ioc<IGuideService>.Instance.Show();
```

或通过服务接口注入：

```csharp
public class HelpService
{
    private readonly IGuideService _guideService;

    public HelpService(IGuideService guideService)
    {
        _guideService = guideService;
    }

    public Task ShowFeatureGuideAsync()
    {
        return _guideService.Show();
    }
}
```

---

## 5. `Cattach` 引导附加属性

引导元数据定义于 `H.Attach/Cattach.Guide.cs`。

| 属性 | 类型 | 说明 |
|---|---|---|
| `Cattach.UseGuide` | `bool` | 是否将元素作为引导节点。默认 `false`。 |
| `Cattach.GuideTitle` | `object` | 引导标题。可使用字符串或绑定对象。 |
| `Cattach.GuideParentTitle` | `string` | 引导所属父级标题，用于组织说明。 |
| `Cattach.GuideData` | `object` | 引导正文、业务对象或模板数据。 |
| `Cattach.GuideDataTemplate` | `DataTemplate` | 自定义呈现 `GuideData` 的模板。 |
| `Cattach.GuideUseClick` | `bool` | 是否让引导流程使用目标元素的点击交互。 |
| `Cattach.GuideIcon` | `string` | 引导图标标识。 |
| `Cattach.GuideAssemblyVersion` | `Version` | 此引导项所属功能版本，用于筛选新增功能。 |
| `Cattach.IsGuideAdonerElement` | `bool` | 指定承载 Adorner 的根元素。 |
| `Cattach.IsGuide` | `bool` | 已废弃旧属性，不建议新代码使用。 |

### 5.1 标记目标元素

```xaml
<Button
    h:Cattach.UseGuide="True"
    h:Cattach.GuideTitle="导出"
    h:Cattach.GuideData="选择格式后，将当前数据导出为文件。"
    Content="导出" />
```

建议仅为用户真正需要了解的入口设置 `UseGuide=True`。避免为每个普通控件添加引导，过长的步骤会降低完成率。

### 5.2 使用绑定

```xaml
<Button
    h:Cattach.UseGuide="True"
    h:Cattach.GuideTitle="{Binding ExportGuideTitle}"
    h:Cattach.GuideData="{Binding ExportGuideDescription}"
    Content="导出" />
```

附加属性是依赖属性，支持普通绑定和动态资源。引导展示时应确保绑定值已准备完成。

### 5.3 使用自定义内容模板

```xaml
<Window.Resources>
    <DataTemplate x:Key="ExportGuideTemplate">
        <StackPanel MaxWidth="320">
            <TextBlock FontWeight="Bold" Text="{Binding Name}" />
            <TextBlock
                Margin="0,8,0,0"
                TextWrapping="Wrap"
                Text="{Binding Description}" />
        </StackPanel>
    </DataTemplate>
</Window.Resources>

<Button
    h:Cattach.UseGuide="True"
    h:Cattach.GuideTitle="导出"
    h:Cattach.GuideData="{Binding ExportGuideInfo}"
    h:Cattach.GuideDataTemplate="{StaticResource ExportGuideTemplate}"
    Content="导出" />
```

`GuideDataTemplate` 适合展示图文、快捷键、状态或业务对象；简单文字说明使用 `GuideData` 即可。

---

## 6. 引导宿主与 AdornerLayer

Guide 模块依赖 WPF `AdornerLayer`。默认逻辑：

1. 在 `Application.Current.MainWindow` 内查找第一个 `Cattach.IsGuideAdonerElement=True` 的元素。
2. 如果找不到，则使用主窗口的 `Content`。
3. 获取该元素的 `AdornerLayer`。
4. 将 `GuideBoxAdorner` 添加至该层。

### 6.1 指定引导宿主

复杂主窗口应显式标记内容根：

```xaml
<Grid h:Cattach.IsGuideAdonerElement="True">
    <!-- 应用内容 -->
</Grid>
```

引导层应覆盖需要引导的全部区域。不要把该属性放在尺寸为 `0`、被裁剪或不包含目标控件的元素上。

### 6.2 自定义 Owner

`IGuideService.Show()` 支持指定 Owner：

```csharp
Task Show(Predicate<UIElement> predicate = null, UIElement owner = null);
```

示例：

```csharp
await _guideService.Show(
    predicate: element => Cattach.GetGuideParentTitle(element) == "项目设置",
    owner: SettingsPanel);
```

Owner 必须处于可视树中，并且能够取得 `AdornerLayer`。否则默认服务无法显示覆盖层。

### 6.3 常见宿主问题

如果调用后没有显示引导，检查：

1. 目标窗口或 UserControl 是否已 `Loaded`。
2. Owner/主窗口内容是否存在 `AdornerLayer`。
3. Owner 是否包含标记的目标元素。
4. 是否已经显示同一个 `GuideBoxAdorner`。
5. 筛选谓词是否过滤掉了所有元素。

---

## 7. 显示、筛选与完成

`IGuideService`：

```csharp
public interface IGuideService : IMainWindowLoadedLoadable
{
    Task Show(Predicate<UIElement> predicate = null, UIElement owner = null);
}
```

### 7.1 显示全部引导

```csharp
await Ioc<IGuideService>.Instance.Show();
```

服务会构建当前可引导元素的树，按视觉层级依次显示。

### 7.2 按功能筛选

```csharp
await Ioc<IGuideService>.Instance.Show(element =>
{
    return Cattach.GetGuideParentTitle(element) == "数据管理";
});
```

### 7.3 按版本筛选

```csharp
Version version = new Version(1, 2, 0, 0);

await Ioc<IGuideService>.Instance.Show(element =>
{
    return Cattach.GetGuideAssemblyVersion(element) == version;
});
```

### 7.4 等待关闭

`Show()` 返回 `Task`，在引导完成、关闭或无法展示时完成：

```csharp
await _guideService.Show();

// 此处继续执行后续逻辑。
```

同一宿主已有 Guide Adorner 时，默认服务不会重复叠加。调用方不应在没有等待上次任务完成的情况下重复调用 `Show()`。

---

## 8. 引导树和步骤顺序

`GuideExtension.GetGuideTree(...)` 会遍历可视树，筛选目标元素并构建：

```text
GuideTree
└── GuideTreeNode
    ├── Element
    ├── Parent
    └── Chidren
```

> `Chidren` 是当前公开 API 的历史拼写。新代码读取该属性时应使用实际名称，不要假设存在 `Children`。

步骤规则：

1. 当前节点若有子节点，下一项是第一个子节点。
2. 当前节点没有子节点时，查找右侧兄弟节点。
3. 当前根节点完成后，切换到下一个根节点。
4. 全部节点完成后，当前节点为 `null`，引导结束。

因此，引导顺序主要由元素在 WPF 可视树中的位置决定，而不是由 `GuideTitle` 或 XAML 行号单独决定。

### 8.1 控制顺序的建议

- 在同一视觉容器中按用户操作顺序声明控件。
- 不要依赖复杂模板、虚拟化面板或运行时重排后的视觉树顺序。
- 对需要严格步骤的功能，将引导元素放入稳定的容器结构。
- 对无关区域使用 `predicate` 分批显示，而不是把多个页面引导混在同一次调用中。

---

## 9. 版本新增功能引导

`GuideAssemblyVersion` 记录某项功能对应的程序集版本：

```xaml
<Button
    h:Cattach.UseGuide="True"
    h:Cattach.GuideTitle="批量导出"
    h:Cattach.GuideData="支持按模板批量导出。"
    h:Cattach.GuideAssemblyVersion="1.3.0.0"
    Content="批量导出" />
```

`GuideOptions` 保存已处理版本，`GuideService.Load()` 在主窗口加载后：

1. 读取入口程序集版本。
2. 读取设置中已保存的 `GuideOptions.Version`。
3. 筛选 `GuideAssemblyVersion` 高于已保存版本的元素。
4. 显示符合条件的引导。
5. 将当前入口程序集版本写回 `GuideOptions.Version` 并保存。

这使应用能够在升级后自动展示“本版本新增功能”。

### 9.1 版本策略

- 为用户可见的新功能设置真实发布版本。
- 已存在功能不要反复提高版本，否则旧用户会重复看到。
- 发布前保证入口程序集版本与引导版本策略一致。
- 将 `GuideAssemblyVersion` 视为产品发布元数据，不是程序集兼容性判断工具。

### 9.2 开发调试

调试阶段如希望始终显示引导：

```csharp
app.UseGuideOptions(options =>
{
    options.UseOnLoad = true;
    options.Version = null;
});
```

也可直接调用：

```csharp
await Ioc<IGuideService>.Instance.Show();
```

不要把清空版本的调试逻辑带入生产配置。

---

## 10. `GuideOptions`

`GuideOptions` 是可持久化设置项，主要属性：

| 属性 | 默认值 | 说明 |
|---|---:|---|
| `UseOnLoad` | 由默认 Options 决定 | 主窗口加载后是否自动检查和显示引导。 |
| `Version` | 空或已保存版本 | 已处理的入口程序集版本。 |
| `TextMaxWidth` | `300` | 引导详情最大宽度，范围 `100～800`。 |
| `CoverColor` | `Black` | 覆盖层背景颜色。 |
| `CoverOpacity` | `0.8` | 覆盖层透明度，范围 `0～1`。 |
| `Stroke` | `Orange` | 高亮边框画刷。 |
| `StrokeThickness` | `1` | 高亮边框厚度。 |
| `StrokeDashArray` | `1,1` | 高亮边框虚线样式。 |
| `AnimationDuration` | 由默认 Options 决定 | 引导动画时长。 |

配置：

```csharp
app.UseGuideOptions(options =>
{
    options.UseOnLoad = true;
    options.TextMaxWidth = 360;
    options.CoverOpacity = 0.65;
    options.Stroke = Brushes.DeepSkyBlue;
    options.StrokeThickness = 2;
});
```

颜色和画刷应优先考虑当前主题。若 Options 需要随主题切换更新，使用主题服务或在运行时从资源字典解析画刷，而不是固化具体颜色。

---

## 11. 引导命令

模块提供标记命令，可直接绑定到按钮、菜单和工具栏。

| 命令 | 用途 |
|---|---|
| `ShowGuideCommand` | 显示符合默认匹配规则的引导。 |
| `ShowGuideTreeCommand` | 显示引导树或列表入口。 |
| `ShowNewGuideCommand` | 显示当前入口程序集版本对应的新功能引导。 |
| `ShowNewGuideTreeCommand` | 显示当前版本新增功能的引导树。 |
| `ShowVersionGuideCommand` | 显示指定 `Version` 对应的引导。 |

基础用法：

```xaml
<Button
    Command="{h:ShowGuideCommand}"
    Content="功能引导" />
```

显示当前版本新增功能：

```xaml
<Button
    Command="{h:ShowNewGuideCommand}"
    Content="本版本新增功能" />
```

显示指定版本：

```xaml
<Button
    Command="{h:ShowVersionGuideCommand Version=1.3.0.0}"
    Content="查看 1.3 新功能" />
```

命令依赖 `IGuideService` 已注册。对需要精确筛选、指定 Owner 或等待完成的业务流程，优先直接调用服务。

---

## 12. 用户点击与 `GuideUseClick`

部分引导需要用户实际点击被高亮元素后再继续：

```xaml
<Button
    h:Cattach.GuideData="点击此按钮创建项目。"
    h:Cattach.GuideTitle="新建项目"
    h:Cattach.GuideUseClick="True"
    h:Cattach.UseGuide="True"
    Content="新建项目" />
```

使用建议：

- 仅对安全、可重复、不会立即离开当前窗口的操作启用。
- 会删除数据、提交表单、关闭窗口或打开模态对话框的控件不应作为强制点击步骤。
- 当目标操作会改变视觉树时，需要确保下一步元素仍存在且已加载。

---

## 13. 自定义 `IGuideService`

默认 `GuideService` 使用主窗口视觉树和 `GuideBoxAdorner`。以下情况可替换实现：

- 多窗口应用需要按活动窗口展示。
- Tab、Dock 或导航页面需要在页面切换后展示。
- 需要显式步骤列表而非视觉树遍历。
- 需要记录用户是否跳过某一项而非仅记录版本。
- 需要远程配置、A/B 实验或按角色显示引导。

接口：

```csharp
public interface IGuideService : IMainWindowLoadedLoadable
{
    Task Show(Predicate<UIElement> predicate = null, UIElement owner = null);
}
```

替换：

```csharp
services.Replace(
    ServiceDescriptor.Singleton<IGuideService, MyGuideService>());
```

自定义实现应：

- 确保 UI 操作在 Dispatcher 线程执行。
- 避免同一 Owner 重复添加 Adorner。
- 始终完成返回的 `Task`。
- 正确处理 Owner 卸载、窗口关闭和取消。
- 不在后台线程直接遍历视觉树。

---

## 14. 自定义引导内容和视觉效果

默认 Adorner 使用 `GuideOptions` 的遮罩和高亮配置。扩展视觉效果时：

1. 优先通过 `GuideDataTemplate` 自定义步骤内容。
2. 通过 `GuideOptions` 调整覆盖层、描边和动画。
3. 如需修改高亮几何、箭头定位或导航按钮，扩展 `GuideBoxAdorner` 或替换服务。

不要直接在业务窗口中叠加固定 `Popup` 模拟引导。这样会绕过 Adorner 层、窗口裁剪、统一关闭和版本筛选机制。

---

## 15. 完整示例

### `App.xaml.cs`

```csharp
public partial class App : ApplicationBase
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddGuide();
    }

    protected override void Configure(IApplicationBuilder app)
    {
        base.Configure(app);

        app.UseGuideOptions(options =>
        {
            options.UseOnLoad = true;
            options.TextMaxWidth = 320;
            options.CoverOpacity = 0.7;
        });
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
    Title="MyApp">
    <Grid h:Cattach.IsGuideAdonerElement="True">
        <Grid.RowDefinitions>
            <RowDefinition Height="Auto" />
            <RowDefinition Height="*" />
        </Grid.RowDefinitions>

        <ToolBar>
            <Button
                Command="{h:ShowGuideCommand}"
                Content="功能引导" />
            <Button
                Command="{h:ShowNewGuideCommand}"
                Content="新增功能" />
        </ToolBar>

        <StackPanel Grid.Row="1" Margin="24">
            <Button
                h:Cattach.GuideAssemblyVersion="1.3.0.0"
                h:Cattach.GuideData="从模板创建项目，并自动生成基础目录。"
                h:Cattach.GuideTitle="新建项目"
                h:Cattach.UseGuide="True"
                Content="新建项目" />
            <Button
                Margin="0,12,0,0"
                h:Cattach.GuideAssemblyVersion="1.3.0.0"
                h:Cattach.GuideData="查看和管理当前项目的设置。"
                h:Cattach.GuideTitle="项目设置"
                h:Cattach.UseGuide="True"
                Content="项目设置" />
        </StackPanel>
    </Grid>
</Window>
```

---

## 16. 性能与可靠性

- 引导仅在需要时显示，不要在每次页面刷新时自动调用 `Show()`。
- 复杂窗口的视觉树遍历可能较重，使用 `predicate` 缩小范围。
- 虚拟化列表中不可见项没有实际 `UIElement`，无法被默认引导服务高亮。
- 在 Tab、Dock 或异步页面中，等待目标元素 `Loaded` 后再显示。
- 将版本记录写入设置前确认引导已正常发起；对于“必须完成才记录”的需求应使用自定义服务。
- 避免在同一个 Owner 上并发调用多个 `Show()`。

---

## 17. 常见问题

### 调用后没有显示任何引导

检查：

1. 是否已调用 `services.AddGuide()`。
2. 是否至少一个元素设置了 `Cattach.UseGuide="True"`。
3. 目标元素是否已经加载且可见。
4. 是否能找到 `AdornerLayer`。
5. `predicate` 或版本条件是否过滤掉全部元素。

### 自动引导没有在启动时执行

检查：

1. 是否调用 `app.UseGuideOptions()`。
2. `GuideOptions.UseOnLoad` 是否为 `true`。
3. 当前入口程序集版本是否高于已保存的 `GuideOptions.Version`。
4. 主窗口加载流程是否已执行。

### 版本新增功能显示不符合预期

检查 `GuideAssemblyVersion`、入口程序集版本和已保存版本。引导项筛选按 `Version` 比较，确保版本号格式一致，例如 `1.3.0.0`。

### 覆盖层没有覆盖整个页面

为包含全部引导目标的内容根设置：

```xaml
h:Cattach.IsGuideAdonerElement="True"
```

或者调用 `Show(..., owner)` 显式指定正确 Owner。

### 单击目标控件后引导中断

目标控件可能打开了新窗口、切换页面或销毁了视觉树。不要为此类操作启用 `GuideUseClick`，或在新页面加载后重新开始下一段引导。

### 引导顺序不符合预期

默认顺序来自视觉树层级。调整稳定容器结构，拆分不同区域的引导，并通过 `predicate` 分批展示。

---

## 18. 二次开发建议

- 把 Guide 当作“发现功能”的辅助机制，不要替代正式文档和错误提示。
- 只标记高价值、首次使用或版本新增的功能。
- 标题短、说明具体，每步只说明一个动作。
- 使用 `GuideDataTemplate` 呈现图文、快捷键或动态业务说明。
- 将版本字段作为发布流程的一部分维护。
- Tab、Dock、虚拟化和多窗口场景优先考虑自定义 `IGuideService`。
- 将遮罩和高亮颜色与应用主题协调。
- 在 `H.Test.Guide` 中验证引导层、关闭、版本筛选和多语言说明。
