# H.ValueConverter 开发文档

**适用项目：** `H.ValueConverter`  
**核心类型：** `Converter`、`MarkupValueConverterBase`、`MarkupMultiValueConverterBase`、`DependencyConverterBase`  
**相关能力：** Binding 转换、MultiBinding、静态转换器入口、布尔/可见性、颜色、集合、字符串、文件、图像和类型转换

本文介绍 WPF-Control 中 `H.ValueConverter` 的使用方式、现有转换器分类和自定义转换器开发规范。

---

## 1. 模块定位

`H.ValueConverter` 将绑定源值转换为目标属性需要的值。它位于 View 与 ViewModel 之间：

```text
ViewModel 属性
    ↓ Binding
IValueConverter.Convert(...)
    ↓
控件目标属性
```

典型转换：

```text
bool              → Visibility
bool              → Brush / 文本 / 任意状态值
double            → 角度 / 百分比 / 尺寸
Color             → Brush
文件大小          → 可读文本
文件路径          → ImageSource
枚举/类型/集合     → 显示数据或状态
多个绑定值         → 一个显示值
```

ValueConverter 应是：

```text
同步
轻量
无副作用
可预测
尽量无状态
```

不应在 `Convert` 中执行数据库访问、网络调用、长时间文件扫描、认证授权或修改业务状态。

---

## 2. 使用前提

引用项目：

```xml
<ItemGroup>
  <ProjectReference Include="..\..\Base\H.ValueConverter\H.ValueConverter.csproj" />
</ItemGroup>
```

XAML 使用统一命名空间：

```xaml
xmlns:h="https://github.com/HeBianGu"
```

转换器可以通过三种主要方式使用：

| 方式 | 用途 |
|---|---|
| 静态 `Converter` 属性 | 使用框架已提供的通用转换器。 |
| `MarkupValueConverterBase` | 将转换器内联写入 Binding。 |
| 资源字典实例 | 共享带配置的转换器，或使用不支持 MarkupExtension 的转换器。 |

---

## 3. 静态 `Converter` 入口

`H.ValueConverter.Converter` 是 `static partial class`，公开大量 `IValueConverter` 属性。

例如：

```xaml
<TextBlock
    Visibility="{Binding IsBusy, Converter={x:Static h:Converter.GetTrueToVisible}}" />
```

```xaml
<TextBlock
    Text="{Binding FilePath, Converter={x:Static h:Converter.GetFileName}}" />
```

```xaml
<Border
    Background="{Binding Color, Converter={x:Static h:Converter.GetColorToBrush}}" />
```

静态入口适合无状态通用转换。引用语法：

```xaml
Converter="{x:Static h:Converter.属性名}"
```

不要在 ViewModel 中手动调用 UI 转换器来生成展示值；ViewModel 应尽量暴露领域状态，View 层选择转换器完成显示转换。

---

## 4. 常用转换器分类

`H.ValueConverter` 包含多个目录和静态入口。

| 分类 | 示例 | 用途 |
|---|---|---|
| 布尔值 | `GetTrueToVisible`、`GetTrueToCollapsed`、`GetBoolToValueConverter` | 布尔转可见性、文字、对象或状态。 |
| 可见性 | `GetEqualsToVisibilityStringConverter`、`GetItemInListToVisibilityConverter` | 条件显示/隐藏。 |
| 画刷与颜色 | `GetColorToBrushConverter`、`GetBackgroundToForegroundConverter`、`GetLevelColorConverter` | 颜色、画刷和对比前景。 |
| 数值与尺寸 | `GetPercentToAngleConverter`、`GetCornerRadiusToDoubleConverter`、`GetThicknessToDoubleConverter` | 进度、几何、布局显示。 |
| 字符串 | `GetStringReplaceConverter`、`GetStringSplitIndexConverter` | 分割、替换、格式化。 |
| 集合 | `GetAnyConverter`、`GetConcatConverter`、`GetTrueForAllConverter` | 集合判断、连接和组合。 |
| 项容器 | `GetIsFirstItemInItemsControlConverter`、`GetIsLastItemInItemsControlConverter` | 列表项视觉状态。 |
| 文件 | `GetByteToSizeDisplayConverter` | 文件大小和文件属性显示。 |
| 图像 | `GetImageSourceFromBase64Converter`、`GetImageSourceFromFilePathConverter` | 图片数据和路径转换。 |
| 类型 | `GetHasFlagConverter`、`GetTypeAttributeConverter` | 枚举标记和反射元数据。 |
| 基础静态转换 | `GetMath*`、`GetString*`、`GetIEnumerable*`、`GetFile*` | 数学、字符串、集合、文件路径等。 |

转换器名称是公开 API。现有名称中可能存在历史拼写，不应仅为更正拼写而修改已发布类型或静态属性。

---

## 5. 布尔与可见性转换

最常见用法：

```xaml
<Grid
    Visibility="{Binding IsLoading, Converter={x:Static h:Converter.GetTrueToVisible}}">
    <TextBlock Text="正在加载..." />
</Grid>
```

反向隐藏：

```xaml
<TextBlock
    Visibility="{Binding IsLoading, Converter={x:Static h:Converter.GetTrueToCollapsed}}"
    Text="加载完成" />
```

条件相等显示：

```xaml
<TextBlock
    Visibility="{Binding Status,
        Converter={x:Static h:Converter.GetEqualsToVisibilityString},
        ConverterParameter=Completed}"
    Text="已完成" />
```

注意：`Visibility` 的“反向”语义可能是 `Collapsed` 或 `Hidden`。框架常用转换器通常使用 `Collapsed`，它不占布局空间；需要保留布局位置时应明确使用 `Hidden` 或自定义转换器。

---

## 6. 静态基础转换器

`Converter` 中包含许多由泛型 `ConverterBase` 组合的转换器。

### 6.1 数学

```xaml
<TextBlock
    Text="{Binding Value,
        Converter={x:Static h:Converter.GetMathRound},
        ConverterParameter=2}" />
```

```xaml
<ProgressBar
    Value="{Binding Ratio, Converter={x:Static h:Converter.GetMathMultiplication}, ConverterParameter=100}" />
```

可用成员包括：

```text
GetMathAbs
GetMathPow
GetMathSqrt
GetMathFloor
GetMathExp
GetMathLog
GetMathSin / GetMathCos / GetMathTan
GetMathMin / GetMathMax
GetMathRound
GetMathAddition
GetMathMultiplication
GetGreaterThan / GetLessThan
```

参数类型必须符合转换器期待的类型。XAML 的 `ConverterParameter` 通常是字符串；涉及 `double`、`int`、`Type` 等参数时，确认 WPF 类型转换能否正确完成，复杂参数优先使用 `MultiBinding` 或自定义转换器属性。

### 6.2 字符串

```xaml
<TextBlock
    Text="{Binding Name, Converter={x:Static h:Converter.GetStringToUpper}}" />
```

```xaml
<TextBlock
    Text="{Binding FilePath, Converter={x:Static h:Converter.GetFileNameWithoutExtension}}" />
```

常用成员：

```text
GetStringTrim
GetStringToUpper / GetStringToLower
GetStringIsNullOrEmpty
GetStringContains / GetStringStartsWith / GetStringEndsWith
GetStringPadLeft / GetStringPadRight
GetStringSplit / GetStringSubstring / GetStringFormat / GetStringJoin
GetDirectoryName / GetExtension / GetFileName / GetFileNameWithoutExtension
GetFullPath / GetPathRoot / GetChangeExtension / GetHasExtension
```

对 `null`、空文本、无效路径或索引越界应有明确行为。不要在用户每次输入时使用高成本字符串转换链。

### 6.3 时间与集合

```xaml
<TextBlock
    Text="{Binding Elapsed, Converter={x:Static h:Converter.GetTimeSpanStr}}" />
```

```xaml
<TextBlock
    Text="{Binding Items, Converter={x:Static h:Converter.GetIEnumerableCount}}" />
```

对延迟枚举、数据库查询或大集合使用 `Count()`、`Distinct()`、`Reverse()` 等转换可能反复枚举数据。大数据场景应在 ViewModel 中预先计算、缓存或分页。

---

## 7. 图片、文件和画刷转换

### 7.1 颜色转画刷

```xaml
<Border
    Background="{Binding ThemeColor, Converter={x:Static h:Converter.GetColorToBrush}}" />
```

主题相关颜色应优先直接使用 `BrushKeys`：

```xaml
Background="{DynamicResource {x:Static h:BrushKeys.CaptionBackground}}"
```

只有画刷真正来自绑定数据时才使用 Color→Brush 转换器。

### 7.2 文件大小

```xaml
<TextBlock
    Text="{Binding Length, Converter={x:Static h:Converter.GetByteToSizeDisplay}}" />
```

文件大小格式化是显示转换，不能替代文件存在性、权限和读取异常处理。

### 7.3 路径转图片

```xaml
<Image
    Source="{Binding ImagePath, Converter={x:Static h:Converter.GetImageSourceFromFilePath}}" />
```

图片路径转换可能涉及图片解码。对于大图、频繁更新路径或大量列表项：

- 在 ViewModel/Service 中异步加载缩略图。
- 缓存可复用 `ImageSource`。
- 避免在每次布局/绑定刷新中重新读取文件。
- 处理文件不存在、损坏和访问失败。

需要大图缩放和预览时见 [`development-guide-zoombox.md`](development-guide-zoombox.md)。

---

## 8. `MarkupValueConverterBase`

`MarkupValueConverterBase` 同时实现：

```text
MarkupExtension
IValueConverter
INotifyPropertyChanged
```

核心实现：

```csharp
[MarkupExtensionReturnType(typeof(IValueConverter))]
public abstract class MarkupValueConverterBase : MarkupExtension, IValueConverter
{
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return this;
    }

    public abstract object Convert(
        object value,
        Type targetType,
        object parameter,
        CultureInfo culture);
}
```

因此转换器可直接写在 Binding 中：

```xaml
<TextBlock
    Visibility="{Binding IsBusy, Converter={local:BooleanToVisibilityConverter}}" />
```

无需先声明资源：

```xaml
<local:BooleanToVisibilityConverter x:Key="BooleanToVisibility" />
```

然后使用：

```xaml
Converter="{StaticResource BooleanToVisibility}"
```

更详细的 MarkupExtension 生命周期、`ProvideValue` 和刷新限制见 [`development-guide-markup-extension.md`](development-guide-markup-extension.md)。

### 8.1 自定义单值转换器

```csharp
using System.Globalization;
using System.Windows;
using H.ValueConverter;

public class BooleanToVisibilityConverter : MarkupValueConverterBase
{
    public override object Convert(
        object value,
        Type targetType,
        object parameter,
        CultureInfo culture)
    {
        return value is true
            ? Visibility.Visible
            : Visibility.Collapsed;
    }

    public override object ConvertBack(
        object value,
        Type targetType,
        object parameter,
        CultureInfo culture)
    {
        return Binding.DoNothing;
    }
}
```

仅支持 OneWay 时，`ConvertBack` 返回 `Binding.DoNothing` 或 `DependencyProperty.UnsetValue`，不要让基类默认 `NotImplementedException` 被 TwoWay Binding 触发。

### 8.2 返回值约定

| 返回值 | 含义 |
|---|---|
| `DependencyProperty.UnsetValue` | 无法转换，绑定引擎使用 FallbackValue 或默认行为。 |
| `Binding.DoNothing` | 不更新源或目标。 |
| `null` | 合法空值，目标属性必须允许。 |
| 正常目标类型值 | 转换成功。 |

`MarkupValueConverterBase` 提供 `DefaultValue` 与 `DefaultBackValue`，默认均为 `DependencyProperty.UnsetValue`。自定义转换器应清楚约定无效输入的返回值。

---

## 9. `MarkupMultiValueConverterBase` 与 MultiBinding

多个绑定源合成一个目标值时，使用 `MarkupMultiValueConverterBase`：

```csharp
public class AllTrueToVisibilityConverter : MarkupMultiValueConverterBase
{
    public override object Convert(
        object[] values,
        Type targetType,
        object parameter,
        CultureInfo culture)
    {
        return values.All(value => value is true)
            ? Visibility.Visible
            : Visibility.Collapsed;
    }

    public override object[] ConvertBack(
        object value,
        Type[] targetTypes,
        object parameter,
        CultureInfo culture)
    {
        return targetTypes.Select(_ => Binding.DoNothing).ToArray();
    }
}
```

XAML：

```xaml
<TextBlock>
    <TextBlock.Visibility>
        <MultiBinding Converter="{local:AllTrueToVisibilityConverter}">
            <Binding Path="IsLoaded" />
            <Binding Path="HasPermission" />
        </MultiBinding>
    </TextBlock.Visibility>
</TextBlock>
```

转换器必须处理：

```text
null
DependencyProperty.UnsetValue
Binding.DoNothing
values 数量不足
参数类型不匹配
```

MultiBinding 不应用于大量高频刷新值；它会在任一源变化时重新执行转换。

---

## 10. `DependencyConverterBase`

`DependencyConverterBase`：

```csharp
public abstract class DependencyConverterBase : DependencyObject, IValueConverter
{
    public abstract object Convert(...);
    public abstract object ConvertBack(...);
}
```

当转换器本身需要依赖属性、样式 Setter、动画或绑定配置时可继承该类。

示例：

```csharp
public class ThresholdConverter : DependencyConverterBase
{
    public double Threshold
    {
        get => (double)GetValue(ThresholdProperty);
        set => SetValue(ThresholdProperty, value);
    }

    public static readonly DependencyProperty ThresholdProperty =
        DependencyProperty.Register(
            nameof(Threshold),
            typeof(double),
            typeof(ThresholdConverter),
            new PropertyMetadata(0d));

    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value is double number && number >= Threshold;
    }

    public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return Binding.DoNothing;
    }
}
```

在资源字典声明：

```xaml
<local:ThresholdConverter x:Key="ThresholdConverter" Threshold="80" />
```

然后使用：

```xaml
<TextBlock
    Visibility="{Binding CpuUsage,
        Converter={StaticResource ThresholdConverter},
        ConverterParameter=80}" />
```

如果转换器需要随其自身属性变化更新绑定目标，不要假设 `INotifyPropertyChanged` 自动让所有 Binding 重跑；优先使阈值成为绑定源，使用 MultiBinding 或显式更新 `BindingExpression`。

---

## 11. ConverterParameter 使用技巧

`ConverterParameter` 是一个对象，但 XAML 中常被解析成字符串：

```xaml
<TextBlock
    Text="{Binding Value,
        Converter={x:Static h:Converter.GetMathRound},
        ConverterParameter=2}" />
```

适合：

- 固定字符串。
- 简单数值、枚举或可由 XAML TypeConverter 解析的值。
- 不变化的显示配置。

不适合：

- 需要绑定的动态参数。
- 复杂对象图。
- 用户每次操作都会变化的阈值。

动态参数使用 `MultiBinding`：

```xaml
<TextBlock>
    <TextBlock.Text>
        <MultiBinding Converter="{local:FormatValueConverter}">
            <Binding Path="Value" />
            <Binding Path="Decimals" />
        </MultiBinding>
    </TextBlock.Text>
</TextBlock>
```

---

## 12. ConvertBack 与双向绑定

`ConvertBack` 仅在目标属性回写源属性时执行：

```xaml
<TextBox
    Text="{Binding Amount,
        Mode=TwoWay,
        UpdateSourceTrigger=PropertyChanged,
        Converter={local:CurrencyConverter}}" />
```

规则：

- 转换失败时返回 `DependencyProperty.UnsetValue`，阻止源更新。
- 不应回写时返回 `Binding.DoNothing`。
- `ConvertBack` 必须返回源属性可接受的类型。
- 解析用户输入时处理空字符串、区域性、小数点、范围和异常。
- 复杂验证应通过 `ValidationRule`、数据注解或 ViewModel 验证完成。

不要在 `ConvertBack` 中直接保存数据库、弹出对话框或修改无关状态。

---

## 13. 主题、资源与转换器

转换器只用于数据到显示值的计算。主题资源应优先使用：

```xaml
Background="{DynamicResource {x:Static h:BrushKeys.CaptionBackground}}"
```

而不是：

```xaml
Background="{Binding CurrentTheme, Converter={local:ThemeToBrushConverter}}"
```

除非颜色确实来自运行时业务数据。

主题切换、资源键和 `DynamicResource` 见 [`development-guide-theme.md`](development-guide-theme.md)。样式中的转换器使用见 [`development-guide-style.md`](development-guide-style.md)。

---

## 14. 性能与可靠性

- `Convert` 可能在布局、源属性变化、虚拟化和模板重建时频繁调用。
- 不要在 `Convert` 中访问网络、数据库、磁盘或大量反射。
- 避免在 `Convert` 中创建大集合、大图片或大量 Brush 实例。
- 大集合计算应在 ViewModel 中缓存，不应在绑定转换时反复 `Count()`、`Distinct()`、`Reverse()`。
- 图片路径转换对大量项使用异步缩略图和缓存。
- 转换器不应修改输入对象、绑定源或其他 UI 元素。
- 对无效输入返回合适的 WPF 特殊值，不要吞掉所有异常后返回错误类型。
- 不要把转换器作为业务状态机或 Service Locator。

---

## 15. 测试清单

- [ ] 正常值、`null`、空字符串和 `UnsetValue`。
- [ ] `ConverterParameter` 缺失、类型错误和边界值。
- [ ] 目标类型与返回类型是否兼容。
- [ ] OneWay 与 TwoWay 绑定下的 `ConvertBack` 行为。
- [ ] 深色/浅色主题下画刷和可见性结果是否可读。
- [ ] 大列表和频繁更新时的性能。
- [ ] 图片、文件与路径不存在时的失败处理。
- [ ] XAML 设计器、资源字典、Style 和 Template 中的加载结果。

---

## 16. 常见问题

### Converter 没有执行

检查 Binding 是否成功、源属性是否通知变化、目标属性是否接受转换器返回类型，以及 Output 窗口是否存在 Binding Error。

### ConverterParameter 类型不正确

XAML 参数通常是字符串。将动态值改为 MultiBinding，或使用资源对象/显式转换器属性。

### TwoWay 绑定抛出 `NotImplementedException`

派生自 `MarkupValueConverterBase` 时，未覆盖 `ConvertBack` 会使用默认实现。对于不支持回写的转换器，返回 `Binding.DoNothing`。

### 切换主题后颜色没有刷新

主题颜色应使用 `DynamicResource`，不要依赖转换器从固定值计算主题画刷。

### 列表滚动时卡顿

转换器可能在大量项上重复执行了昂贵计算。移动计算到 ViewModel、缓存结果，并保持 ItemTemplate 轻量。

---

## 17. 二次开发建议

- 优先复用 `Converter` 静态入口，避免重复实现通用转换。
- 无状态单值转换器继承 `MarkupValueConverterBase`。
- 多值组合继承 `MarkupMultiValueConverterBase`。
- 需要依赖属性配置时继承 `DependencyConverterBase` 并在资源字典使用。
- 返回 `UnsetValue`、`DoNothing`、`null` 或具体值时保持语义明确。
- 业务规则、I/O、权限和持久化不放入转换器。
- 动态参数使用 MultiBinding，不滥用 `ConverterParameter`。
- 主题资源使用 `DynamicResource`，不通过转换器模拟主题系统。
- 参考 MarkupExtension 文档理解内联转换器的生命周期和刷新限制。
