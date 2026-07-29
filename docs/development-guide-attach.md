# H.Attach 与 Cattach 开发文档

**适用项目：** `H.Attach`  
**核心类型：** `Cattach`  
**相关能力：** 附加属性、样式属性继承、控件状态、标题/搜索/选择、ItemsControl、引导、动画、Busy 状态

本文介绍 `H.Attach` 中的 `Cattach` 附加属性系统、常用分类、使用边界和自定义方式。

---

## 1. 模块定位

`H.Attach` 是 WPF-Control 的附加属性基础项目。核心类型为：

```csharp
public static partial class Cattach
{
}
```

`Cattach` 按功能拆分到多个 `Cattach.*.cs` 文件中，通过 WPF 附加依赖属性为标准控件和框架控件提供统一的样式、状态和交互元数据。

```text
XAML 控件
    ↓
Cattach 附加属性
    ↓
控件模板 / 样式 / Behavior / 模块服务
    ↓
统一外观、状态或交互
```

它主要解决：

- 不派生控件也能提供附加样式信息。
- 父容器向子元素传递统一的布局与颜色元数据。
- 为模板、行为和模块提供稳定的绑定入口。
- 通过状态属性驱动模板 Trigger。
- 为 Guide、Search、ItemsControl 等跨控件功能提供标记。

`Cattach` 不是业务状态容器。订单状态、权限判断、网络请求和数据库操作应保留在 ViewModel 或 Service 中。

---

## 2. 引用和 XAML 命名空间

项目引用：

```xml
<ItemGroup>
  <ProjectReference Include="..\..\Base\H.Attach\H.Attach.csproj" />
</ItemGroup>
```

XAML：

```xaml
xmlns:h="https://github.com/HeBianGu"
```

使用格式：

```xaml
<TextBox
    h:Cattach.UseWatermark="True"
    h:Cattach.Watermark="请输入名称" />
```

附加属性的常规访问形式：

```xaml
h:Cattach.属性名="值"
```

C# 中读取或设置：

```csharp
Cattach.SetUseWatermark(textBox, true);
string watermark = Cattach.GetWatermark(textBox);
```

---

## 3. 属性分类总览

| 文件 | 属性类别 | 主要用途 |
|---|---|---|
| `Cattach.cs` | 基础 | 水印、密码、忙状态、路径、多选项。 |
| `Cattach.Attach.cs` | 继承型样式 | 父容器向子元素传递画刷、尺寸、间距、圆角、模板。 |
| `Cattach.Animation.cs` | 动画参数 | 过渡数值、背景色等动画元数据。 |
| `Cattach.Caption.cs` | 标题栏 | 标题区域样式、尺寸和左右中模板。 |
| `Cattach.Check.cs` | 勾选状态 | 选中/未选中内容、文本、画刷、边框和透明度。 |
| `Cattach.Dock.cs` | Dock 布局 | 上下左右中区域模板。 |
| `Cattach.Except.cs` | 例外标记 | 控制自身或子元素的排除逻辑。 |
| `Cattach.Focus.cs` | 焦点 | 焦点边框厚度。 |
| `Cattach.Guide.cs` | 新手引导 | 标题、正文、模板、版本和 Adorner 宿主。 |
| `Cattach.NewGuide.cs` | 新版引导 | 新引导模式元数据。 |
| `Cattach.Icon.cs` | 图标 | 图标颜色和透明度。 |
| `Cattach.Item.cs` | 项容器 | 项背景、尺寸、间距、Padding 和状态模板。 |
| `Cattach.ItemsControl.cs` | 集合控件 | 内部数据源、工具区、首页/末页工具和选中项。 |
| `Cattach.MouseOver.cs` | 悬停状态 | 悬停边框和特效。 |
| `Cattach.Press.cs` | 按下状态 | 按下边框厚度。 |
| `Cattach.Search.cs` | 搜索 | 搜索开关、尺寸、位置、历史与文本。 |
| `Cattach.Select.cs` | 选择状态 | 选中边框、文字、透明度和特效。 |
| `Cattach.Title.cs` | 标题 | 标题开关、列背景、标题宽度、圆角和字号。 |

并非所有附加属性都会直接改变目标元素。许多属性是供控件模板、样式 Trigger 或模块服务读取的元数据；单独设置后是否生效取决于对应模板是否绑定该属性。

---

## 4. 基础属性 `Cattach.cs`

主要属性：

| 属性 | 用途 |
|---|---|
| `UseWatermark` | 启用控件水印显示。 |
| `Watermark` | 水印文字或内容。 |
| `Password` | `PasswordBox` 的密码绑定入口。 |
| `IsUpdating` | 表示控件或数据处于更新状态。 |
| `SelectedItems` | 绑定控件的多选项目集合。 |
| `IsBuzy` | 忙状态开关。名称为现有 API 拼写 `Buzy`。 |
| `BuzyText` | 忙状态显示文本。 |
| `Path` | 通用路径元数据。 |

### 4.1 水印

```xaml
<TextBox
    h:Cattach.UseWatermark="True"
    h:Cattach.Watermark="请输入用户名"
    Text="{Binding UserName, UpdateSourceTrigger=PropertyChanged}" />
```

水印依赖控件库对应样式模板。普通 WPF 默认 `TextBox` 模板不会因为设置该属性自动显示水印。

### 4.2 Busy 状态

```xaml
<Grid
    h:Cattach.BuzyText="正在加载..."
    h:Cattach.IsBuzy="{Binding IsLoading}">
    <!-- 内容 -->
</Grid>
```

`IsBuzy` 只表达 UI 状态，不自动取消请求、不处理异常，也不阻止所有后台访问。长任务应由 ViewModel/Service 提供取消、超时、重试和错误反馈。

### 4.3 多选绑定

```xaml
<ListBox
    SelectionMode="Multiple"
    h:Cattach.SelectedItems="{Binding SelectedItems, Mode=TwoWay}"
    ItemsSource="{Binding Items}" />
```

多选同步依赖支持该属性的控件实现或行为。集合应由 ViewModel 管理，并避免在集合变化回调中再次无条件重置选择状态。

### 4.4 密码属性

```xaml
<PasswordBox h:Cattach.Password="{Binding Password, Mode=TwoWay}" />
```

密码字符串只应短暂存在于 UI 状态，不能写入日志、配置或长期 Options。需要更复杂绑定时可使用 `H.Extensions.Behvaiors` 的 `PasswordBindingBehavior`；详见 [`development-guide-behvaiors.md`](development-guide-behvaiors.md)。

---

## 5. 继承型样式属性 `Cattach.Attach`

`Attach*` 是 `Cattach` 最重要的一组属性。多数字段使用：

```csharp
FrameworkPropertyMetadataOptions.Inherits
```

因此，父元素设置后，逻辑/视觉树中的子元素可继承相同值，前提是子元素不覆盖该值且模板实际读取该属性。

| 属性 | 类型 | 用途 |
|---|---|---|
| `Attach` | `object` | 通用附加数据。 |
| `AttachControlTemplate` | `ControlTemplate` | 附加控件模板。 |
| `AttachTemplate` | `DataTemplate` | 附加数据模板。 |
| `AttachForeground` | `Brush` | 可继承前景画刷。 |
| `AttachBackground` | `Brush` | 可继承背景画刷。 |
| `AttachBorderBrush` | `Brush` | 可继承边框画刷。 |
| `AttachWidth` | `double` | 可继承宽度。 |
| `AttachHeight` | `double` | 可继承高度。 |
| `AttachBorderThickness` | `Thickness` | 可继承边框厚度。 |
| `AttachHorizontalAlignment` | `HorizontalAlignment` | 可继承水平对齐。 |
| `AttachVerticalAlignment` | `VerticalAlignment` | 可继承垂直对齐。 |
| `AttachCornerRaius` | `CornerRadius` | 可继承圆角。当前 API 拼写为 `Raius`。 |
| `AttachMargin` | `Thickness` | 可继承外边距。 |
| `AttachDock` | `Dock` | 可继承 Dock 停靠位置。 |

### 5.1 父容器统一设置

```xaml
<StackPanel
    h:Cattach.AttachBackground="{DynamicResource {x:Static h:BrushKeys.CaptionBackground}}"
    h:Cattach.AttachCornerRaius="4"
    h:Cattach.AttachMargin="0,0,0,8">
    <Button Content="保存" />
    <Button Content="取消" />
</StackPanel>
```

该写法只传递附加属性值。子 `Button` 是否将其用于自身 `Background`、`Margin` 或圆角，取决于 Button 样式是否绑定：

```xaml
Background="{Binding RelativeSource={RelativeSource Self}, Path=(h:Cattach.AttachBackground)}"
```

或通过 Trigger、TemplateBinding 等方式读取。

### 5.2 覆盖继承值

子元素可显式覆盖：

```xaml
<StackPanel h:Cattach.AttachForeground="Gray">
    <TextBlock Text="普通说明" />
    <TextBlock
        h:Cattach.AttachForeground="Red"
        Text="错误说明" />
</StackPanel>
```

### 5.3 使用边界

- 不要把 `AttachWidth`、`AttachHeight` 当作普通 `Width`、`Height` 的全局替代品。
- 继承值会影响深层子元素，复杂页面应限制设置作用域。
- 需要主题切换时使用 `DynamicResource`。
- API 拼写 `AttachCornerRaius` 是兼容契约，新代码必须按实际名称引用。

---

## 6. Item 容器属性 `Cattach.Item`

`Item*` 属性用于 ItemsControl 的项目容器、卡片、列表项或菜单项模板。

| 属性组 | 包含属性 | 用途 |
|---|---|---|
| 外观 | `ItemCornerRadius`、`ItemBackground`、`ItemForeground`、`ItemBorderBrush`、`ItemBorderThickness` | 项默认外观。 |
| 尺寸 | `ItemHeight`、`ItemMinHeight`、`ItemMinWidth`、`ItemsContianerWidth` | 项和容器尺寸。 |
| 布局 | `ItemMargin`、`ItemPadding`、`ItemHorizontalAlignment`、`ItemVerticalAlignment`、`ItemHorizontalContentAlignment`、`ItemVerticalContentAlignment` | 项内容布局。 |
| 状态模板 | `ItemOverTemplate`、`SelectedItemTemplate` | 悬停和选中状态的模板。 |

示例：

```xaml
<ListBox
    h:Cattach.ItemHeight="36"
    h:Cattach.ItemMargin="4,2"
    h:Cattach.ItemPadding="12,0"
    h:Cattach.ItemCornerRadius="4"
    ItemsSource="{Binding MenuItems}" />
```

这些属性通常由框架 `ListBoxItem`、菜单或特定控件模板消费。为通用 WPF `ListBox` 创建自定义模板时，需明确绑定相应附加属性。

> `ItemsContianerWidth` 是当前 API 拼写；不要自行改为 `ItemsContainerWidth`。

---

## 7. 标题、Caption 和 Dock 模板

### 7.1 Caption 属性

`Cattach.Caption` 用于提供标题栏或卡片标题区元数据：

```text
CaptionCornerRadius
CaptionBackground
CaptionForeground
CaptionBorderBrush
CaptionHeight
CaptionBorderThickness
CaptionHorizontalAlignment
CaptionVerticalAlignment
CaptionMargin
CaptionFontSize
CaptionLeftTemplate
CaptionRightTemplate
CaptionCenterTemplate
```

示例：

```xaml
<Border
    h:Cattach.CaptionBackground="{DynamicResource {x:Static h:BrushKeys.CaptionBackground}}"
    h:Cattach.CaptionForeground="{DynamicResource {x:Static h:BrushKeys.CaptionForeground}}"
    h:Cattach.CaptionHeight="40">
    <!-- 需要对应 Caption 模板读取属性。 -->
</Border>
```

### 7.2 Title 属性

`Cattach.Title` 用于属性标题、表单标题或标题列：

```text
UseTitle
BackgroundColumn
TitleMinWidth
TitleCornerRaius
TitleFontSize
```

```xaml
<ContentControl
    h:Cattach.UseTitle="True"
    h:Cattach.TitleMinWidth="120"
    h:Cattach.TitleFontSize="{DynamicResource {x:Static h:FontSizeKeys.Default}}" />
```

### 7.3 Dock 模板

`Cattach.Dock` 提供：

```text
TopTemplate
BottomTemplate
LeftTemplate
RightTemplate
CenterTemplate
```

适用于能够读取这些模板的 Dock、布局容器或窗口控件：

```xaml
<ContentControl h:Cattach.LeftTemplate="{StaticResource NavigationTemplate}" />
```

这些属性仅保存模板引用，不会自动创建 DockPanel 布局。

---

## 8. 选择、勾选、悬停和按下状态

### 8.1 选择状态 `Cattach.Select`

| 属性 | 说明 |
|---|---|
| `IsSelected` | 元素是否选中。 |
| `SelectBorderBrush` / `SelectBorderThickness` | 选中边框。 |
| `SelectEffect` | 选中特效。 |
| `SelectedText` / `NoneSelectedText` | 选中与未选中显示文本。 |
| `SelectedOpacity` / `UnSelectedOpacity` | 不同选择状态透明度。 |

```xaml
<ToggleButton
    h:Cattach.IsSelected="{Binding IsActive}"
    h:Cattach.SelectedText="已启用"
    h:Cattach.NoneSelectedText="未启用" />
```

### 8.2 勾选状态 `Cattach.Check`

该组属性支持不同勾选状态的文本、图形、颜色、背景、边框、厚度与透明度：

```text
IsChecked
CheckedGeometry / UnCheckedGeometry
CheckedContent / UnCheckedContent
CheckedText / UncheckedText
CheckedForeground / UncheckForeground
CheckedBackground / UncheckedBackground
CheckedBorderBrush / UnCheckedBorderBrush
CheckedBorderThickness / UnCheckedBorderThickness
CheckedOpacity / UncheckedOpacity
```

适合 ToggleButton、CheckBox 或自定义状态控件模板。

### 8.3 鼠标悬停与按下

```text
MouseOverBorderBrush
MouseOverBorderThickness
MouseOverEffect
PressBorderThickness
FocusBorderThickness
```

这些属性一般在模板 Trigger 中使用：

```xaml
<Trigger Property="IsMouseOver" Value="True">
    <Setter
        Property="BorderBrush"
        Value="{Binding RelativeSource={RelativeSource Self}, Path=(h:Cattach.MouseOverBorderBrush)}" />
</Trigger>
```

附加属性不代替 WPF 内置 `IsMouseOver`、`IsPressed` 和 `IsKeyboardFocusWithin`。它们用于传递样式值或状态元数据。

---

## 9. 搜索属性 `Cattach.Search`

`Cattach.Search` 适用于框架支持搜索区的控件：

| 属性 | 用途 |
|---|---|
| `UseSearch` | 是否显示或启用搜索。 |
| `SearchText` | 当前搜索文本。 |
| `SearchWidth` / `SearchHeight` | 搜索控件尺寸。 |
| `SearchDock` | 搜索区域 Dock 位置。 |
| `SearchMargin` | 搜索区域外边距。 |
| `SearchVerticalAlignment` / `SearchHorizontalAlignment` | 搜索对齐方式。 |
| `SearchUseHistory` | 是否启用搜索历史。 |

```xaml
<ItemsControl
    h:Cattach.SearchText="{Binding SearchText, UpdateSourceTrigger=PropertyChanged}"
    h:Cattach.SearchUseHistory="True"
    h:Cattach.SearchWidth="240"
    h:Cattach.UseSearch="True" />
```

搜索属性只提供 UI 元数据。真正的过滤应由 `CollectionView`、ViewModel 或模块服务实现，并处理大集合的节流、取消和空状态。

---

## 10. ItemsControl 属性

`Cattach.ItemsControl` 提供集合控件扩展元数据：

| 属性 | 用途 |
|---|---|
| `InnerSource` | 内部数据源。 |
| `Tools` | 工具区内容或模板。 |
| `HomeTool` | 首页/前置工具。 |
| `EndTool` | 尾部工具。 |
| `SelectedItems` | 多选项集合。 |

```xaml
<ListBox
    h:Cattach.EndTool="{StaticResource AddButtonTemplate}"
    h:Cattach.HomeTool="{StaticResource FilterTemplate}"
    h:Cattach.SelectedItems="{Binding SelectedItems, Mode=TwoWay}"
    ItemsSource="{Binding Items}" />
```

实际工具区位置和显示方式由控件模板决定。

---

## 11. Guide 与 NewGuide 属性

`Cattach.Guide` 与 `Cattach.NewGuide` 为 `H.Modules.Guide` 提供标记信息。

常用 Guide 属性：

```text
UseGuide
GuideTitle
GuideParentTitle
GuideData
GuideDataTemplate
GuideUseClick
GuideIcon
GuideAssemblyVersion
IsGuideAdonerElement
```

示例：

```xaml
<Button
    h:Cattach.GuideAssemblyVersion="1.3.0.0"
    h:Cattach.GuideData="从模板创建一个项目。"
    h:Cattach.GuideTitle="新建项目"
    h:Cattach.UseGuide="True"
    Content="新建项目" />
```

`Cattach.IsGuide` 已标记 `[Obsolete]`，新代码使用 `UseGuide`。

Guide 的服务注册、Adorner、版本筛选和命令详见 [`development-guide-guide.md`](development-guide-guide.md)。

`Cattach.NewGuide` 包含：

```text
UseNewGuide
NewGuideTitle
NewGuideParentTitle
NewGuideData
NewGuideDataTemplate
IsNewGuide
```

它们用于新引导模式的元数据。使用前应确认目标模块模板或服务是否读取该组属性。

---

## 12. 图标、动画和例外标记

### 12.1 图标

```text
IconForeground
IconOpacity
```

```xaml
<ContentControl
    h:Cattach.IconForeground="{DynamicResource {x:Static h:BrushKeys.Accent}}"
    h:Cattach.IconOpacity="0.8" />
```

### 12.2 动画参数

```text
FromDouble
ToDouble
ToBackgroundColor
```

这些值为动画或模板提供起始、结束数值与背景色元数据。动画启动、Storyboard 创建和时长由使用它们的控件/模板决定。

### 12.3 例外标记

```text
IsExceptSelf
IsExcepChildren
```

该组用于在框架遍历、批量处理或样式逻辑中标记自身/子元素应被排除。`IsExcepChildren` 是当前 API 实际拼写。

不要将例外标记当作安全或权限机制；它只影响读取该属性的 UI 逻辑。

---

## 13. 自定义附加属性

需要新能力时先判断：

1. WPF 原生属性是否已满足。
2. 现有 `Cattach` 是否已有语义相近的属性。
3. 能否在模块自己的静态类中定义属性，避免扩张全局 `Cattach`。

业务模块专属属性应优先使用独立类型：

```csharp
public static class ReportAttach
{
    public static readonly DependencyProperty IsExportingProperty =
        DependencyProperty.RegisterAttached(
            "IsExporting",
            typeof(bool),
            typeof(ReportAttach),
            new PropertyMetadata(false, OnIsExportingChanged));

    public static bool GetIsExporting(DependencyObject obj)
    {
        return (bool)obj.GetValue(IsExportingProperty);
    }

    public static void SetIsExporting(DependencyObject obj, bool value)
    {
        obj.SetValue(IsExportingProperty, value);
    }

    private static void OnIsExportingChanged(
        DependencyObject d,
        DependencyPropertyChangedEventArgs e)
    {
        // 仅处理 UI 层状态。
    }
}
```

XAML：

```xaml
<Grid local:ReportAttach.IsExporting="{Binding IsExporting}" />
```

### 13.1 需要加入 `Cattach` 的情况

仅当属性同时满足以下条件时考虑加入 `Cattach`：

- 跨多个控件、模块和应用使用。
- 语义通用、稳定，不属于具体业务领域。
- 已有控件模板或样式需要统一消费该属性。
- 名称不会与现有 WPF 或框架属性产生混淆。

### 13.2 设计规则

- 使用 `RegisterAttached` 并提供 `GetXxx` / `SetXxx`。
- 默认值必须安全且不会意外改变现有控件行为。
- 只有确实需要父级传递时添加 `FrameworkPropertyMetadataOptions.Inherits`。
- 属性变更回调中不要执行长时间业务操作。
- 订阅事件后必须在适当时机解除，避免泄漏。
- 公共属性需要 XML 文档和最小 XAML 示例。

---

## 14. 主题与资源

附加属性经常承载画刷、字号、间距和模板。可随主题变化的值使用：

```xaml
<Border
    h:Cattach.AttachBackground="{DynamicResource {x:Static h:BrushKeys.CaptionBackground}}"
    h:Cattach.AttachBorderBrush="{DynamicResource {x:Static h:BrushKeys.BorderBrush}}"
    h:Cattach.AttachCornerRaius="{DynamicResource {x:Static h:LayoutKeys.CornerRadius}}" />
```

规则：

- 颜色、背景、前景和边框使用 `BrushKeys`。
- 字号使用 `FontSizeKeys`。
- 间距、圆角和高度使用 `LayoutKeys`。
- 需要运行时刷新时使用 `DynamicResource`。
- 模板中读取附加属性时使用 `(h:Cattach.PropertyName)` 附加属性路径语法。

主题资源的完整说明见 [`development-guide-theme.md`](development-guide-theme.md)。

---

## 15. 常见问题

### 设置了附加属性但界面没有变化

附加属性可能只是元数据。检查当前控件模板或样式是否读取该属性；WPF 标准控件不会自动理解 `Cattach` 的所有属性。

### `Attach*` 值没有被子元素使用

确认：

1. 父子关系是否处于可继承的元素树中。
2. 子元素是否覆盖了同名附加属性。
3. 属性是否配置了 `Inherits`。
4. 子元素模板是否绑定该属性。

### 水印或 Busy 状态不显示

确认应用已合并支持这些属性的控件样式，并且使用的是框架样式覆盖的控件。

### 多选集合不同步

确认控件支持 `SelectedItems`，绑定模式正确，集合可更新，并避免在选择变化时循环重置绑定源。

### 主题切换后颜色未更新

附加属性值应使用：

```xaml
{DynamicResource {x:Static h:BrushKeys.Foreground}}
```

不要为需要切换的主题值使用 `StaticResource`。

### 属性名称看起来拼写错误

项目存在历史公开 API，例如：

```text
IsBuzy
AttachCornerRaius
ItemsContianerWidth
TitleCornerRaius
IsExcepChildren
```

新代码引用时必须使用真实 API 名称。不要修改现有公开名称，否则会破坏 XAML、样式和外部应用兼容性。

---

## 16. 二次开发建议

- 把 `Cattach` 视为 UI 元数据和样式桥接，不要承载领域逻辑。
- 优先复用现有语义属性，避免属性数量无限增长。
- 业务专属能力使用模块自己的附加属性类。
- 使用继承型属性时控制作用域，避免无意影响深层子元素。
- 自定义模板必须显式读取相应附加属性。
- 主题值使用 `DynamicResource` 和稳定资源键。
- 对带事件订阅的附加属性严格管理附加与清理生命周期。
- 修改公共 `Cattach` API 前检查所有 XAML、模板和控件库引用。
- 在关联控件或模块的测试项目中验证附加属性是否真正被消费。
