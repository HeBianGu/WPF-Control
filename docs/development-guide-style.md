# H.Style 开发文档

**适用项目：** `H.Style`  
**核心类型：** `ConciseStyleExtension`、`IStyleResource`、`ConciseStyleResource`、`NoneStyleResource`  
**相关能力：** WPF 标准控件默认样式、资源字典、主题资源绑定、FontIcon 控件样式、样式切换

本文介绍 `H.Style` 的资源组织、样式加载、标准控件模板、自定义控件样式和二次开发规范。

---

## 1. 项目定位

`H.Style` 是 WPF-Control 的标准 WPF 控件样式库。它为 Button、TextBox、ListBox、DataGrid、Menu、TreeView 等 WPF 原生控件以及 FontIcon 相关框架控件提供统一模板和视觉风格。

```text
H.Theme / H.Themes.Colors.*
        ↓ 提供颜色、画刷、字号、布局资源
H.Style
        ↓ 提供标准控件 Style 与 ControlTemplate
业务窗口 / 控件模块
        ↓ 使用默认样式或显式样式键
应用 UI
```

职责边界：

- `H.Theme` 定义主题语义和资源键。
- `H.Themes.Colors.*` 提供具体浅色/深色配色。
- `H.Style` 消费主题资源并定义控件外观。
- `H.Styles.Bootstrap` 等项目提供可选的替代风格。
- 业务模块只定义模块专属控件的样式，不应修改全局标准控件样式。

主题资源完整说明见 [`development-guide-theme.md`](development-guide-theme.md)。

---

## 2. 项目结构

```text
H.Style/
├── Controls/
│   ├── Button.xaml
│   ├── TextBox.xaml
│   ├── ComboBox.xaml
│   ├── DataGrid.xaml
│   ├── ListBox.xaml
│   ├── TreeView.xaml
│   ├── Menu.xaml
│   ├── ScrollViewer.xaml
│   ├── FontIconButton.xaml
│   └── ...
├── Themes/
│   └── Generic.xaml
├── StyleResources/
│   ├── IStyleResource.cs
│   ├── ConciseStyleResource.cs
│   └── NoneStyleResource.cs
├── ConciseControls.xaml
├── NoneControls.xaml
├── ConciseStyleExtension.cs
├── Share.xaml
└── H.Style.csproj
```

### 2.1 关键资源

| 文件 | 职责 |
|---|---|
| `ConciseControls.xaml` | 推荐的简洁默认样式入口，合并各 `Controls/*.xaml`。 |
| `NoneControls.xaml` | 空资源字典，用于恢复或表示不应用此套标准样式。 |
| `Themes/Generic.xaml` | 程序集主题资源入口。 |
| `Controls/*.xaml` | 具体控件的 Style、ControlTemplate、资源键和转换器。 |
| `Share.xaml` | 跨控件共享样式和资源。 |
| `ConciseStyleExtension` | 在 XAML 中返回 `ConciseControls.xaml` 资源字典。 |
| `IStyleResource` | 用于模块化选择和提供样式资源字典的接口。 |

---

## 3. 样式加载

### 3.1 推荐方式：`ConciseStyle`

`ConciseStyleExtension` 返回：

```text
pack://application:,,,/H.Style;component/ConciseControls.xaml
```

在 `App.xaml` 合并：

```xaml
<Application
    x:Class="MyApp.App"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:h="https://github.com/HeBianGu">
    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <FontSizeTheme Type="Default" />
                <LayoutTheme Type="Default" />
                <ColorTheme Type="Default" />
                <ConciseStyle />
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Application.Resources>
</Application>
```

`ConciseControls.xaml` 会合并标准控件资源字典，例如：

```text
Border
Button
Calendar
CheckBox
ComboBox
ContextMenu
DataGrid
DatePicker
DocumentViewer
Expander
Frame
GroupBox
GridSplitter
Label
ListBox
ListView
Menu
MenuItem
PasswordBox
ProgressBar
RadioButton
RepeatButton
ScrollBar
ScrollViewer
Separator
Slider
StatusBar
TabControl
TextBlock
TextBox
Thumb
ToggleButton
ToolBar
ToolTip
TreeView
ResizeGrip
Path
FontIconButton
FontIconTextBlock
FontIconToggleButton
GroupItem
Hyperlink
LineGrid
```

### 3.2 直接合并资源字典

需要控制样式组合时可直接引用：

```xaml
<ResourceDictionary.MergedDictionaries>
    <ResourceDictionary Source="pack://application:,,,/H.Style;component/Controls/Button.xaml" />
    <ResourceDictionary Source="pack://application:,,,/H.Style;component/Controls/TextBox.xaml" />
    <ResourceDictionary Source="pack://application:,,,/H.Style;component/Controls/DataGrid.xaml" />
</ResourceDictionary.MergedDictionaries>
```

适合：

- 只需要部分标准控件样式。
- 使用第三方控件库且需要避免全局样式冲突。
- 需要逐步替换现有应用样式。

### 3.3 使用空样式集

`NoneControls.xaml` 是空资源字典，对应：

```csharp
new NoneStyleResource().Resource
```

它用于样式选择框架中的“默认样式”选项，或作为不加载 H.Style 标准控件模板的显式资源。

---

## 4. 主题与样式的关系

控件模板不应硬编码业务颜色、字号和常用尺寸。模板通过主题资源读取值：

```xaml
<Border
    Background="{DynamicResource {x:Static h:BrushKeys.CaptionBackground}}"
    BorderBrush="{DynamicResource {x:Static h:BrushKeys.BorderBrush}}"
    CornerRadius="{DynamicResource {x:Static h:LayoutKeys.CornerRadius}}">
    <TextBlock
        FontSize="{DynamicResource {x:Static h:FontSizeKeys.Default}}"
        Foreground="{DynamicResource {x:Static h:BrushKeys.Foreground}}" />
</Border>
```

使用原则：

| 需求 | 使用资源 |
|---|---|
| 文字、背景、边框、强调色 | `BrushKeys`。 |
| 字号 | `FontSizeKeys`。 |
| Padding、Margin、圆角、控件高度 | `LayoutKeys`。 |
| 支持运行时主题切换 | `DynamicResource`。 |
| 固定、不会随主题变化的本地资源 | `StaticResource`。 |

不要在通用 `H.Style` 模板中写死 `#FFFFFF`、`#000000` 等颜色。若确有无法归类的视觉需求，应在主题项目定义具有明确语义的资源键。

---

## 5. 默认样式与显式样式

### 5.1 默认样式

没有 `x:Key`、指定 `TargetType` 的 Style 会影响作用域内同类控件：

```xaml
<Style TargetType="{x:Type Button}">
    <!-- 默认 Button 样式 -->
</Style>
```

`ConciseControls.xaml` 合并后，应用中的普通 Button 等控件会使用对应默认样式。

### 5.2 显式样式

带 `x:Key` 的样式仅在显式引用时生效：

```xaml
<Style x:Key="DangerButton" TargetType="{x:Type Button}">
    <Setter Property="Background" Value="{DynamicResource {x:Static h:BrushKeys.Danger}}" />
</Style>

<Button
    Style="{StaticResource DangerButton}"
    Content="删除" />
```

### 5.3 基于现有样式扩展

```xaml
<Style
    x:Key="LargeButton"
    BasedOn="{StaticResource {x:Type Button}}"
    TargetType="{x:Type Button}">
    <Setter Property="MinWidth" Value="160" />
    <Setter Property="FontSize" Value="{DynamicResource {x:Static h:FontSizeKeys.Header}}" />
</Style>
```

使用 `BasedOn` 时需确认基础样式已在同一资源作用域内解析。跨资源字典引用顺序不正确会导致 XAML 加载失败。

---

## 6. 控件样式文件约定

`Controls/*.xaml` 通常包含：

```text
局部资源
    ↓
公开样式键（可选）
    ↓
默认 Style
    ↓
ControlTemplate
    ↓
Triggers / VisualState
```

推荐结构：

```xaml
<ResourceDictionary
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:h="https://github.com/HeBianGu">

    <Style TargetType="{x:Type Button}">
        <Setter Property="Background" Value="{DynamicResource {x:Static h:BrushKeys.CaptionBackground}}" />
        <Setter Property="BorderBrush" Value="{DynamicResource {x:Static h:BrushKeys.BorderBrush}}" />
        <Setter Property="Foreground" Value="{DynamicResource {x:Static h:BrushKeys.Foreground}}" />
        <Setter Property="Template">
            <Setter.Value>
                <ControlTemplate TargetType="{x:Type Button}">
                    <Border
                        Background="{TemplateBinding Background}"
                        BorderBrush="{TemplateBinding BorderBrush}"
                        BorderThickness="{TemplateBinding BorderThickness}"
                        CornerRadius="{DynamicResource {x:Static h:LayoutKeys.CornerRadius}}">
                        <ContentPresenter
                            Margin="{TemplateBinding Padding}"
                            HorizontalAlignment="{TemplateBinding HorizontalContentAlignment}"
                            VerticalAlignment="{TemplateBinding VerticalContentAlignment}" />
                    </Border>
                </ControlTemplate>
            </Setter.Value>
        </Setter>
    </Style>
</ResourceDictionary>
```

实际模板应从当前 `H.Style` 同类控件文件复制并按需修改，避免遗漏键盘焦点、禁用、鼠标悬停、按下、验证错误、滚动条或自动化语义。

---

## 7. 模板绑定规则

控件模板应优先使用 `TemplateBinding` 传递控件可配置属性：

```xaml
Background="{TemplateBinding Background}"
BorderBrush="{TemplateBinding BorderBrush}"
BorderThickness="{TemplateBinding BorderThickness}"
Padding="{TemplateBinding Padding}"
HorizontalAlignment="{TemplateBinding HorizontalContentAlignment}"
VerticalAlignment="{TemplateBinding VerticalContentAlignment}"
```

当需要转换器、双向绑定或附加属性路径时，使用普通绑定：

```xaml
<Border
    Background="{Binding
        RelativeSource={RelativeSource TemplatedParent},
        Path=(h:Cattach.AttachBackground)}" />
```

规则：

- 简单一向模板属性使用 `TemplateBinding`。
- 需要更多 Binding 功能时使用 `RelativeSource TemplatedParent`。
- 内容区使用 `ContentPresenter`、`ItemsPresenter` 或正确的模板部件。
- 不要通过 `ElementName` 绑定外部页面元素，模板应保持可复用。

---

## 8. 标准控件样式注意事项

### 8.1 Button、ToggleButton、CheckBox、RadioButton

需要正确处理：

```text
IsEnabled
IsMouseOver
IsPressed
IsChecked
IsKeyboardFocusWithin
FocusVisualStyle
Content
Command / CommandParameter
```

不要为了修改颜色而覆盖整个 Button 模板。优先创建基于默认 Button Style 的局部样式。

### 8.2 TextBox、PasswordBox、ComboBox、DatePicker

输入控件模板通常包含多个命名部件和验证视觉状态。自定义时应验证：

- 键盘输入和焦点移动。
- 只读、禁用、验证错误。
- 水印、清除按钮和密码绑定等框架扩展。
- 深色/浅色主题下文字可读性。

### 8.3 ListBox、ListView、TreeView、DataGrid

集合控件需要同时考虑：

- `ItemsPanel` 与虚拟化。
- `ItemContainerStyle`。
- 选中、悬停和键盘焦点状态。
- ScrollViewer 模板和滚动条资源。
- 空数据、分组、排序与大数据性能。

不要在 `ItemsControl.ItemTemplate` 内嵌套不必要的 ScrollViewer，这会破坏虚拟化。

### 8.4 Menu、ContextMenu、ToolTip

这些控件经常位于 Popup 中，资源查找和 DataContext 与普通视觉树不同。修改模板后需测试：

- 菜单打开、关闭和键盘导航。
- 子菜单定位。
- 右键上下文菜单。
- 深色主题对比度。
- Popup 内资源是否可解析。

---

## 9. FontIcon 样式

`H.Style` 包含：

```text
FontIconButton.xaml
FontIconTextBlock.xaml
FontIconToggleButton.xaml
```

这些样式用于框架字体图标控件。使用时优先使用框架公开的样式键，例如：

```xaml
<h:FontIconButton
    Content="{x:Static h:FontIcons.Save}"
    ToolTip="保存" />
```

图标按钮模板应保留：

- 图标与文字内容呈现。
- `IsEnabled`、悬停、按下、选中状态。
- 可访问性文本或 `ToolTip`。
- 主题前景与强调色。

不要把图标字符硬编码到多个业务页面；优先复用 `FontIcons` 中的稳定定义。

---

## 10. 样式资源提供者

`H.Style` 定义：

```csharp
public interface IStyleResource
{
    string Name { get; }
    ResourceDictionary Resource { get; }
}
```

默认实现：

| 类型 | `Name` | 资源 |
|---|---|---|
| `ConciseStyleResource` | `简洁样式（推荐）` | `ConciseControls.xaml`。 |
| `NoneStyleResource` | `默认样式` | `NoneControls.xaml`。 |

可用于主题或设置模块中的样式选择器。自定义样式集可实现相同接口：

```csharp
public class MyStyleResource : IStyleResource
{
    public string Name => "MyApp 样式";

    public ResourceDictionary Resource => new()
    {
        Source = new Uri(
            "pack://application:,,,/MyApp;component/Themes/MyControls.xaml")
    };
}
```

不要缓存并跨应用共享同一个可变 `ResourceDictionary` 实例。样式切换时应创建或加载正确的资源字典实例。

---

## 11. 自定义样式集

创建业务应用样式集的推荐方式：

```text
MyApp.Styles/
├── Themes/MyControls.xaml
├── Controls/OrderCard.xaml
├── Controls/ProjectTree.xaml
└── MyStyleResource.cs
```

`MyControls.xaml`：

```xaml
<ResourceDictionary
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">
    <ResourceDictionary.MergedDictionaries>
        <ResourceDictionary Source="Controls/OrderCard.xaml" />
        <ResourceDictionary Source="Controls/ProjectTree.xaml" />
    </ResourceDictionary.MergedDictionaries>
</ResourceDictionary>
```

原则：

- 用业务样式程序集扩展，而不是直接修改 `H.Style`。
- 只对确有业务差异的控件覆盖默认样式。
- 保持主题语义资源不变，避免在业务样式中复制整套颜色。
- 将样式集作为独立 `IStyleResource` 暴露给设置模块。

---

## 12. 与 `H.Attach` 的协作

`H.Style` 中的模板可读取 `Cattach` 附加属性，以支持跨控件样式元数据：

```xaml
<Border
    Background="{Binding
        RelativeSource={RelativeSource TemplatedParent},
        Path=(h:Cattach.ItemBackground)}"
    CornerRadius="{Binding
        RelativeSource={RelativeSource TemplatedParent},
        Path=(h:Cattach.ItemCornerRadius)}" />
```

常见用途：

- `Attach*`：父容器继承式样式元数据。
- `Item*`：列表项尺寸、间距、颜色和状态模板。
- `Select*` / `Check*`：选择和勾选状态样式。
- `MouseOver*` / `Press*`：交互状态视觉。
- `Caption*` / `Title*`：标题区域布局。

附加属性详见 [`development-guide-attach.md`](development-guide-attach.md)。

---

## 13. 样式切换与资源顺序

资源字典顺序会影响覆盖关系。一般顺序：

```text
主题基础资源
    ↓
颜色主题资源
    ↓
H.Style 标准控件样式
    ↓
模块控件样式
    ↓
应用局部覆盖样式
```

示例：

```xaml
<ResourceDictionary.MergedDictionaries>
    <FontSizeTheme Type="Default" />
    <LayoutTheme Type="Default" />
    <ColorTheme Type="Default" />
    <ConciseStyle />
    <ResourceDictionary Source="/MyApp;component/Themes/Overrides.xaml" />
</ResourceDictionary.MergedDictionaries>
```

后合并的资源会覆盖前面同键资源。覆盖默认 Style 时应确认是否希望影响全局、窗口级还是局部容器级控件。

运行时切换主题时，控件模板中的主题资源必须使用 `DynamicResource` 才能刷新。

---

## 14. 性能与可访问性

### 14.1 性能

- 不要在每个项目容器中创建大量复杂动画和 VisualBrush。
- 列表和 DataGrid 模板保持轻量，保留虚拟化。
- 避免 Trigger 内频繁改变会触发布局的大量属性。
- 可复用 Brush、Converter 和 Geometry 资源。
- 不要在模板绑定中使用昂贵的多层 `RelativeSource` 搜索。

### 14.2 可访问性

- 焦点状态不能仅依赖颜色，应保留可见焦点提示。
- 禁用状态应具备足够对比度。
- 图标按钮提供 `ToolTip`、AutomationProperties 或文字说明。
- 不要删除键盘导航和默认命令行为。
- 文本、背景和错误状态在深色/浅色主题下均需可读。

---

## 15. 常见问题

### 标准样式没有生效

检查：

1. 是否引用 `H.Style`。
2. `App.xaml` 是否合并 `<ConciseStyle />` 或具体资源字典。
3. 合并顺序中是否有后续资源覆盖了同类型默认 Style。
4. 控件是否显式设置了 `Style="{x:Null}"`。

### 切换主题后控件颜色不更新

检查模板是否使用：

```xaml
{DynamicResource {x:Static h:BrushKeys.Foreground}}
```

不要对可切换主题资源使用 `StaticResource`。

### 自定义模板后控件功能丢失

完整替换 `ControlTemplate` 可能遗漏模板部件、命令、验证状态或 ItemsPresenter。应基于当前版本的原始模板修改，并测试键盘、鼠标、禁用、选择、滚动与验证。

### 资源字典加载失败

检查 Pack URI、项目程序集名称、文件生成操作和资源合并顺序。跨程序集资源一般使用：

```text
pack://application:,,,/程序集名;component/路径/文件.xaml
```

### DataGrid 或 ListBox 滚动卡顿

检查是否禁用了虚拟化、是否在每项模板中创建复杂控件、是否嵌套 ScrollViewer，以及是否在选择/滚动 Trigger 中执行大量逻辑。

---

## 16. 二次开发建议

- 全局标准控件外观改动放在 `H.Style` 或独立样式程序集，不放入业务页面。
- 新模板优先使用主题语义键和 `DynamicResource`。
- 只在需要时覆盖默认模板；轻量修改使用局部 `Style` 和 `BasedOn`。
- 控件模板必须保留 WPF 基础功能、键盘导航和可访问性。
- 模块专属视觉资源使用模块自己的 `ComponentResourceKey`。
- 不修改现有资源键含义，避免影响所有引用方。
- 使用 `H.Test.Theme` 或关联测试项目验证浅色/深色主题和标准控件状态。
- 为新样式集提供 `IStyleResource` 实现，便于统一选择和切换。
