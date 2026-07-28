# 自定义控件开发文档

**参考项目：** `H.Controls.ProgressButton`  
**参考类型：** `ProgressButton : Button`  
**相关能力：** 自定义控件、依赖属性、资源键、默认样式、`ControlTemplate`、主题资源、命令状态绑定

本文以 `H.Controls.ProgressButton.ProgressButton` 为实际范例，说明如何在 WPF-Control 框架中创建可主题化、可复用、可扩展的自定义控件。

---

## 1. 自定义控件与 UserControl

开始开发前先选择正确类型。

| 类型 | 适用场景 | 特点 |
|---|---|---|
| `UserControl` | 固定界面组合、业务页面、局部视图。 | 视觉树固定，开发快，复用模板能力有限。 |
| 自定义控件 `Control` | 需要可换主题、可换模板、可扩展公共 API 的通用控件。 | 逻辑和样式分离，使用 `Generic.xaml` 与 `ControlTemplate`。 |
| 继承现有控件 | 在 `Button`、`ItemsControl`、`TextBox` 等基础能力上增加功能。 | 复用原控件交互和命令能力。 |
| Attached Property / Behavior | 仅为已有控件附加状态或交互。 | 不创建新控件类型。 |

`ProgressButton` 继承 `Button`：

```csharp
public class ProgressButton : Button
{
}
```

因为它仍然是按钮，保留 `Command`、`CommandParameter`、`Click`、键盘激活、焦点和无障碍等 Button 基础能力，只额外增加进度状态。

---

## 2. ProgressButton 设计分析

`ProgressButton` 的目标：

```text
正常状态：显示 Content，允许点击
忙碌状态：显示 Message 与 ProgressBar，禁用按钮
命令状态：可从 Command 的 IsBusy / Percent 等状态自动绑定
```

公开 API：

| 属性 | 类型 | 默认值 | 作用 |
|---|---|---:|---|
| `IsBusy` | `bool` | `false` | 是否显示进度并禁用按钮。 |
| `IsIndeterminate` | `bool` | `false` | 是否显示不确定进度动画。 |
| `Percent` | `double` | `0` | 进度值；默认模板范围为 `0～1`。 |
| `Message` | `string` | `null` | 忙碌时显示的提示文字。 |
| `ProgressOpacity` | `double` | `0.5` | 进度层透明度。 |
| `DefaultKey` | `ComponentResourceKey` | - | 默认样式资源键。 |
| `CommandKey` | `ComponentResourceKey` | - | 命令状态绑定样式资源键。 |

资源键：

```csharp
public static ComponentResourceKey DefaultKey =>
    new ComponentResourceKey(typeof(ProgressButton), "S.ProgressButton.Default");

public static ComponentResourceKey CommandKey =>
    new ComponentResourceKey(typeof(ProgressButton), "S.ProgressButton.Command");
```

设计要点：

- `Content` 仍表示按钮的普通文本或内容。
- `Message` 用于忙碌状态，避免覆盖业务绑定的 `Content`。
- `Percent` 语义必须明确为 `0～1`，不是 `0～100`。
- `IsBusy` 是状态属性；模板决定状态变化时的具体视觉效果。
- 使用两个显式资源键提供“默认模式”和“命令状态模式”。

---

## 3. 控件项目结构

推荐结构：

```text
H.Controls.MyControl/
├── MyControl.cs                 控件逻辑、依赖属性、命令、事件
├── MyControl.xaml               控件 Style 与 ControlTemplate
├── Themes/
│   └── Generic.xaml             程序集主题资源入口
├── MyControlKeys.cs             可选：统一资源键
├── Provider/                    转换器、事件参数、帮助类型
├── Resources/                   图片、光标、本地化资源
├── AssemblyInfo.cs
└── H.Controls.MyControl.csproj
```

`ProgressButton` 使用：

```text
H.Controls.ProgressButton/
├── ProgressButton.xaml.cs
├── ProgressButton.xaml
├── Themes/Generic.xaml
└── H.Controls.ProgressButton.csproj
```

逻辑代码和默认模板分离是自定义控件的核心：

```text
控件类：公开状态和交互契约
XAML 模板：定义视觉结构和状态表现
主题资源：提供颜色、字号、布局参数
```

---

## 4. 创建控件类

最小控件：

```csharp
using System.Windows;
using System.Windows.Controls;

namespace H.Controls.MyControl;

public class StatusButton : Button
{
    static StatusButton()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(StatusButton),
            new FrameworkPropertyMetadata(typeof(StatusButton)));
    }
}
```

`DefaultStyleKeyProperty.OverrideMetadata` 的作用是告诉 WPF：

```text
StatusButton 的默认样式键是 typeof(StatusButton)
```

WPF 会在程序集主题资源中查找目标类型为 `StatusButton` 的默认样式。

`ProgressButton` 使用相同模式：

```csharp
static ProgressButton()
{
    DefaultStyleKeyProperty.OverrideMetadata(
        typeof(ProgressButton),
        new FrameworkPropertyMetadata(typeof(ProgressButton)));
}
```

没有此元数据覆盖时，控件可能继承 Button 默认外观，而不会自动使用自定义 `ControlTemplate`。

---

## 5. 选择合适的基类

| 需求 | 建议基类 |
|---|---|
| 可点击、支持命令 | `Button` / `ButtonBase`。 |
| 可选择项或切换状态 | `ToggleButton`、`Selector`、`ListBoxItem`。 |
| 单一内容区域 | `ContentControl`。 |
| 多项目集合 | `ItemsControl`、`Selector`。 |
| 文本输入 | `TextBox`、`Control` 加 `Text` 依赖属性。 |
| 可拖动范围值 | `RangeBase`。 |
| 完全自定义绘制 | `Control` 或 `FrameworkElement`。 |

优先继承最接近的 WPF 基类。不要为了新增一个状态属性而重新实现 Button、Selector 或 ItemsControl 的所有交互。

---

## 6. 定义依赖属性

可绑定、可设样式、可动画和可触发模板状态的公共属性应定义为依赖属性。

`ProgressButton.IsBusy`：

```csharp
public bool IsBusy
{
    get => (bool)GetValue(IsBusyProperty);
    set => SetValue(IsBusyProperty, value);
}

public static readonly DependencyProperty IsBusyProperty =
    DependencyProperty.Register(
        nameof(IsBusy),
        typeof(bool),
        typeof(ProgressButton),
        new FrameworkPropertyMetadata(false));
```

`Percent` 示例：

```csharp
public double Percent
{
    get => (double)GetValue(PercentProperty);
    set => SetValue(PercentProperty, value);
}

public static readonly DependencyProperty PercentProperty =
    DependencyProperty.Register(
        nameof(Percent),
        typeof(double),
        typeof(ProgressButton),
        new FrameworkPropertyMetadata(
            0d,
            null,
            CoercePercent));

private static object CoercePercent(DependencyObject d, object value)
{
    var percent = (double)value;
    return Math.Clamp(percent, 0d, 1d);
}
```

当前 `ProgressButton.Percent` 未强制限制范围；新控件如果存在明确范围，建议通过验证或强制回调避免无效状态。

### 6.1 属性注册选项

| 需求 | 使用方式 |
|---|---|
| 默认值 | `new FrameworkPropertyMetadata(defaultValue)`。 |
| 属性变化通知 | 在 Metadata 中提供 `PropertyChangedCallback`。 |
| 限制有效值 | `ValidateValueCallback` 或 `CoerceValueCallback`。 |
| 影响布局 | `FrameworkPropertyMetadataOptions.AffectsMeasure` / `AffectsArrange`。 |
| 影响渲染 | `AffectsRender`。 |
| 双向绑定默认 | `BindsTwoWayByDefault`。 |
| 父级属性继承 | `Inherits`，仅用于跨元素树传递的通用状态。 |

不要为每个属性都添加空的变化回调。只有确实需要同步模板部件、重新计算状态或执行轻量 UI 更新时才添加回调。

### 6.2 属性命名规则

- 使用清晰的正向语义：`IsBusy`、`IsIndeterminate`。
- 布尔属性以 `Is`、`Can`、`Has`、`Use` 开头。
- 范围值明确单位和范围：`Percent` 是 `0～1` 还是 `0～100` 必须写入文档。
- 公开 API 一旦发布，应避免重命名。
- 避免在公共 API 中重复已有 WPF 属性语义。

---

## 7. 定义资源键

框架控件通常为可选样式提供 `ComponentResourceKey`：

```csharp
public static ComponentResourceKey DefaultKey =>
    new(typeof(StatusButton), "S.StatusButton.Default");

public static ComponentResourceKey CompactKey =>
    new(typeof(StatusButton), "S.StatusButton.Compact");
```

对应 XAML：

```xaml
<Style
    x:Key="{ComponentResourceKey
        ResourceId=S.StatusButton.Default,
        TypeInTargetAssembly={x:Type local:StatusButton}}"
    TargetType="{x:Type local:StatusButton}">
    <!-- 默认样式 -->
</Style>
```

使用：

```xaml
<local:StatusButton
    Style="{StaticResource {x:Static local:StatusButton.DefaultKey}}" />
```

`ProgressButton` 的 `CommandKey` 样式基于 `DefaultKey`，并将控件状态绑定到 Command 状态。

资源键适合：

- 默认、紧凑、危险、工具栏、命令等多种稳定外观。
- 允许业务应用在不复制整个模板的情况下选择风格。
- 避免字符串资源键冲突。

---

## 8. 编写默认 `ControlTemplate`

`ProgressButton` 的核心模板结构：

```text
Border
└── Grid
    ├── ContentPresenter    正常内容或忙碌消息
    └── ProgressBar         忙碌时覆盖显示
```

精简示例：

```xaml
<Style
    x:Key="{ComponentResourceKey
        ResourceId=S.StatusButton.Default,
        TypeInTargetAssembly={x:Type local:StatusButton}}"
    TargetType="{x:Type local:StatusButton}">
    <Setter Property="Foreground" Value="{DynamicResource {x:Static h:BrushKeys.CaptionForeground}}" />
    <Setter Property="Background" Value="{DynamicResource {x:Static h:BrushKeys.CaptionBackground}}" />
    <Setter Property="Padding" Value="10,6" />
    <Setter Property="Template">
        <Setter.Value>
            <ControlTemplate TargetType="{x:Type local:StatusButton}">
                <Border
                    Background="{TemplateBinding Background}"
                    BorderBrush="{TemplateBinding BorderBrush}"
                    BorderThickness="{TemplateBinding BorderThickness}">
                    <Grid>
                        <ContentPresenter
                            x:Name="ContentPresenter"
                            Margin="{TemplateBinding Padding}"
                            HorizontalAlignment="{TemplateBinding HorizontalContentAlignment}"
                            VerticalAlignment="{TemplateBinding VerticalContentAlignment}" />
                        <ProgressBar
                            x:Name="Progress"
                            Maximum="1"
                            Minimum="0"
                            Value="{Binding Percent, RelativeSource={RelativeSource TemplatedParent}}"
                            Visibility="Collapsed" />
                    </Grid>
                </Border>
                <ControlTemplate.Triggers>
                    <Trigger Property="IsBusy" Value="True">
                        <Setter TargetName="Progress" Property="Visibility" Value="Visible" />
                        <Setter Property="IsEnabled" Value="False" />
                    </Trigger>
                </ControlTemplate.Triggers>
            </ControlTemplate>
        </Setter.Value>
    </Setter>
</Style>
```

### 8.1 模板绑定

`ProgressButton` 使用：

```xaml
Background="{TemplateBinding Background}"
BorderBrush="{TemplateBinding BorderBrush}"
BorderThickness="{TemplateBinding BorderThickness}"
Margin="{TemplateBinding Padding}"
```

简单单向属性使用 `TemplateBinding`；需要复杂路径、转换器或附加属性时使用：

```xaml
{Binding
    RelativeSource={RelativeSource TemplatedParent},
    Path=IsBusy}
```

### 8.2 内容呈现

按钮、ContentControl 等控件模板应包含 `ContentPresenter`，以保留：

```text
Content
ContentTemplate
ContentTemplateSelector
ContentStringFormat
AccessKey
```

`ProgressButton` 在 `IsBusy=True` 时将 `ContentPresenter.Content` 切换为 `Message`，正常时保留 `Content`。

---

## 9. 使用 Template Trigger 表达状态

`ProgressButton` 的模板状态：

```xaml
<Trigger Property="IsBusy" Value="true">
    <Setter
        TargetName="cp"
        Property="Content"
        Value="{Binding Path=Message, RelativeSource={RelativeSource TemplatedParent}}" />
    <Setter Property="IsEnabled" Value="False" />
</Trigger>
```

常见状态：

| 状态 | 属性/机制 |
|---|---|
| 忙碌 | `IsBusy`。 |
| 不确定进度 | `IsIndeterminate`。 |
| 禁用 | `IsEnabled`。 |
| 鼠标悬停 | `IsMouseOver`。 |
| 按下 | `IsPressed`。 |
| 默认按钮 | `IsDefault`。 |
| 焦点 | `IsKeyboardFocusWithin`。 |
| 验证错误 | `Validation.HasError`。 |

样式级 Trigger 示例：

```xaml
<Style.Triggers>
    <Trigger Property="IsEnabled" Value="False">
        <Setter Property="Opacity" Value="0.6" />
    </Trigger>
    <Trigger Property="IsMouseOver" Value="True">
        <Setter
            Property="Background"
            Value="{DynamicResource {x:Static h:BrushKeys.Accent}}" />
    </Trigger>
</Style.Triggers>
```

不要在状态 Trigger 中覆盖业务绑定的状态属性，除非该属性确实只由控件模板管理。

---

## 10. 命令状态绑定模式

`ProgressButton` 提供 `CommandKey` 样式：

```xaml
<Style
    x:Key="{ComponentResourceKey
        ResourceId=S.ProgressButton.Command,
        TypeInTargetAssembly={x:Type local:ProgressButton}}"
    BasedOn="{StaticResource {x:Static h:ProgressButton.DefaultKey}}"
    TargetType="{x:Type local:ProgressButton}">
    <Setter Property="IsBusy" Value="{Binding RelativeSource={RelativeSource Self}, Path=Command.IsBusy}" />
    <Setter Property="Percent" Value="{Binding RelativeSource={RelativeSource Self}, Path=Command.Percent}" />
    <Setter Property="IsIndeterminate" Value="{Binding RelativeSource={RelativeSource Self}, Path=Command.IsIndeterminate}" />
    <Setter Property="Message" Value="{Binding RelativeSource={RelativeSource Self}, Path=Command.Message}" />
    <Setter Property="IsEnabled" Value="{Binding RelativeSource={RelativeSource Self}, Path=Command.IsEnabled}" />
</Style>
```

使用：

```xaml
<h:ProgressButton
    Command="{Binding ExportCommand}"
    Content="导出" />
```

默认样式基于 `CommandKey`，因此 Command 对象若提供：

```text
IsBusy
Percent
IsIndeterminate
Message
IsEnabled
IsVisible
```

模板可自动反映命令执行状态。

注意：WPF 标准 `ICommand` 只定义 `CanExecute`、`Execute` 和 `CanExecuteChanged`，并不定义上述进度属性。只有框架特定的命令实现或带这些公开属性的命令对象才能使用此绑定模式。

### 10.1 开发自定义命令状态控件

- 对普通 `ICommand` 保持可用，不能强制要求进度属性。
- 为富命令状态提供独立显式 Style Key。
- 绑定路径错误不应导致控件不可用；需要在测试中检查输出窗口 Binding Error。
- 长任务必须提供取消、异常和重复点击策略。

---

## 11. 使用主题与附加属性

`ProgressButton` 模板使用：

```xaml
{DynamicResource {x:Static BrushKeys.CaptionForeground}}
{DynamicResource {x:Static BrushKeys.CaptionBackground}}
{DynamicResource {x:Static BrushKeys.BorderBrushTitle}}
{DynamicResource {x:Static ColorKeys.Selected}}
{DynamicResource {x:Static LayoutKeys.ItemHeight}}
{DynamicResource {x:Static LayoutKeys.CornerRadius}}
```

并读取：

```xaml
(Cattach.CornerRadius)
```

这体现框架控件模板约定：

```text
颜色、画刷     → BrushKeys / ColorKeys
字号           → FontSizeKeys
高度、间距、圆角 → LayoutKeys
可选 UI 元数据  → Cattach 附加属性
```

可随主题切换的资源使用 `DynamicResource`。附加属性与样式协作方式见 [`development-guide-attach.md`](development-guide-attach.md)，标准样式组织见 [`development-guide-style.md`](development-guide-style.md)。

---

## 12. 模板部件与 `OnApplyTemplate`

控件需要从模板获取命名元素时，使用 `TemplatePart`：

```csharp
[TemplatePart(Name = PartProgress, Type = typeof(ProgressBar))]
public class StatusButton : Button
{
    private const string PartProgress = "PART_Progress";
    private ProgressBar _progress;

    static StatusButton()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(StatusButton),
            new FrameworkPropertyMetadata(typeof(StatusButton)));
    }

    public override void OnApplyTemplate()
    {
        if (_progress != null)
        {
            // 解除旧模板部件事件。
        }

        base.OnApplyTemplate();
        _progress = GetTemplateChild(PartProgress) as ProgressBar;

        if (_progress != null)
        {
            // 订阅新模板部件事件。
        }
    }
}
```

`ProgressButton` 仅通过绑定和 Trigger 即可完成当前功能，不需要访问 `ProgressBar` 实例，因此没有 `OnApplyTemplate()`。

规则：

- 模板部件名称使用 `PART_` 前缀。
- 标记 `TemplatePart`，使模板契约可见。
- 每次 `OnApplyTemplate()` 都要处理旧部件解除订阅。
- 模板部件可以不存在；代码必须可空处理。
- 不要把模板内部普通元素暴露为控件长期 API。

---

## 13. 路由事件与命令

控件需要对外通知交互或状态变化时，优先考虑：

| 需求 | 方案 |
|---|---|
| 属性状态可绑定 | 依赖属性。 |
| 父元素需要响应控件事件 | 路由事件。 |
| 用户执行操作 | `ICommand` / RoutedCommand。 |
| 需要取消或修改操作 | 可取消事件参数或命令 CanExecute。 |

路由事件示例：

```csharp
public static readonly RoutedEvent CompletedEvent =
    EventManager.RegisterRoutedEvent(
        nameof(Completed),
        RoutingStrategy.Bubble,
        typeof(RoutedEventHandler),
        typeof(StatusButton));

public event RoutedEventHandler Completed
{
    add => AddHandler(CompletedEvent, value);
    remove => RemoveHandler(CompletedEvent, value);
}

protected virtual void OnCompleted()
{
    RaiseEvent(new RoutedEventArgs(CompletedEvent));
}
```

不要为可由 `IsBusy=False`、`Percent=1` 等状态绑定表达的信息额外创建冗余事件。

---

## 14. `Generic.xaml` 与资源发现

自定义控件的默认样式必须在程序集可发现的主题资源中。通常：

```text
Themes/Generic.xaml
```

`Generic.xaml` 合并控件样式资源：

```xaml
<ResourceDictionary
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">
    <ResourceDictionary.MergedDictionaries>
        <ResourceDictionary Source="/H.Controls.MyControl;component/StatusButton.xaml" />
    </ResourceDictionary.MergedDictionaries>
</ResourceDictionary>
```

控件项目需要正确的主题信息。模板无法自动发现时检查：

1. `DefaultStyleKeyProperty` 是否覆写为控件类型。
2. `Generic.xaml` 是否位于 `Themes`。
3. 项目主题信息和资源生成方式是否正确。
4. Pack URI 中程序集名和路径是否正确。
5. Style 的 `TargetType` 是否为实际控件类型。

---

## 15. 创建新控件的完整流程

```text
确认使用自定义控件而不是 UserControl/Behavior
    ↓
选择最接近的 WPF 基类
    ↓
定义最小公共 API（依赖属性、命令、事件）
    ↓
覆盖 DefaultStyleKeyProperty
    ↓
定义 ComponentResourceKey（有多个稳定样式时）
    ↓
编写默认 Style 与 ControlTemplate
    ↓
在 Generic.xaml 合并资源
    ↓
接入 BrushKeys、FontSizeKeys、LayoutKeys、Cattach
    ↓
添加 H.Test.<Name> 示例
    ↓
验证主题、键盘、鼠标、禁用、模板替换和绑定错误
    ↓
编写开发文档
```

### 15.1 最小骨架

```csharp
public class StatusButton : Button
{
    public static ComponentResourceKey DefaultKey =>
        new(typeof(StatusButton), "S.StatusButton.Default");

    static StatusButton()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(StatusButton),
            new FrameworkPropertyMetadata(typeof(StatusButton)));
    }

    public bool IsBusy
    {
        get => (bool)GetValue(IsBusyProperty);
        set => SetValue(IsBusyProperty, value);
    }

    public static readonly DependencyProperty IsBusyProperty =
        DependencyProperty.Register(
            nameof(IsBusy),
            typeof(bool),
            typeof(StatusButton),
            new FrameworkPropertyMetadata(false));
}
```

---

## 16. 测试清单

### 功能

- [ ] 默认模板可自动加载。
- [ ] 依赖属性支持 XAML、Binding、Style Setter 和代码设置。
- [ ] 默认值、非法值和边界值行为明确。
- [ ] `Command`、`Click`、键盘激活仍正常工作。
- [ ] 状态 Trigger 不会留下错误的视觉状态。
- [ ] 替换自定义模板后控件不会因缺失可选部件崩溃。

### 主题与样式

- [ ] 浅色和深色主题下前景、背景、边框可读。
- [ ] 主题值使用 `DynamicResource`。
- [ ] 默认样式和显式资源键可解析。
- [ ] 禁用、悬停、按下、焦点状态可用。
- [ ] 应用覆盖样式时不会破坏基础功能。

### 性能与可访问性

- [ ] 不在属性变化回调中执行长时间操作。
- [ ] 不重复订阅模板部件事件。
- [ ] 大量控件实例时模板保持轻量。
- [ ] 可使用键盘获得焦点、激活和导航。
- [ ] 图标或仅视觉内容具有可访问文字/提示。

---

## 17. 常见问题

### 控件显示为普通 Button

检查 `DefaultStyleKeyProperty.OverrideMetadata`、`Generic.xaml`、Style `TargetType` 和资源字典合并。

### 设置了依赖属性但模板没有变化

依赖属性只保存状态。模板需要通过 `TemplateBinding`、普通 Binding 或 Trigger 读取该属性。

### ProgressBar 显示的进度不正确

`ProgressButton` 默认 `ProgressBar` 的 `Minimum=0`、`Maximum=1`。因此：

```csharp
Percent = 0.5; // 50%
```

不是：

```csharp
Percent = 50; // 会超出默认范围
```

新控件应在 API 文档中固定并验证进度单位。

### 忙碌时按钮仍可点击

确认模板 Trigger 或样式 Trigger 在 `IsBusy=True` 时设置：

```xaml
<Setter Property="IsEnabled" Value="False" />
```

同时确认业务命令不允许重复并发执行。

### Command 状态绑定出现错误

普通 `ICommand` 不包含 `IsBusy`、`Percent` 等属性。使用 `CommandKey` 或依赖该样式时，确保绑定的命令实现公开了这些属性；否则创建不依赖富命令状态的默认样式。

### 自定义模板后点击或键盘失效

确认仍继承正确基类、没有覆盖基础 Button 行为，并保留 `ContentPresenter`、焦点视觉与命令路径。

---

## 18. 二次开发建议

- 先定义控件契约，再设计视觉模板。
- 继承最接近的 WPF 基类，避免重复实现已有能力。
- 控件逻辑不直接依赖业务 Service、数据库或具体 ViewModel。
- 公共状态使用依赖属性，交互使用命令或路由事件。
- 默认模板使用主题资源和 `DynamicResource`。
- 多种稳定外观使用 `ComponentResourceKey`，不要用魔法字符串。
- 模板部件是契约，使用 `TemplatePart` 并处理重新应用模板。
- 在独立 `H.Test.*` 项目中验证所有控件状态与主题。
- 公共 API 发布后保持命名和语义兼容。
