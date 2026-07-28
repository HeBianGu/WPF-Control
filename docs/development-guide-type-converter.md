# H.Extensions.TypeConverter 开发文档

**适用项目：** `H.Extensions.TypeConverter`  
**核心类型：** `System.ComponentModel.TypeConverter`、`DisplayEnumConverter`、`DisplayNameConverter`、`NullableDateTimeConverter`、`SizeToDisplayTypeConverter`  
**相关能力：** 属性编辑、PropertyGrid、XAML 文本解析、字符串序列化、枚举显示、可空值、几何类型和集合转换

本文介绍项目中 `H.Extensions.TypeConverter` 的 `TypeConverter` 用法，并说明它与 WPF `IValueConverter`、`MarkupExtension` 的差异。

---

## 1. 什么是 TypeConverter

`System.ComponentModel.TypeConverter` 是 .NET 组件模型的类型转换机制。它将对象与其他表示形式相互转换，最常见的是：

```text
对象  ?  string
对象  ?  基础类型
对象  ?  PropertyDescriptor/PropertyGrid 编辑值
```

它常被以下场景使用：

- `PropertyGrid`、Form 和设计器编辑属性。
- XAML 将字符串属性值解析为目标类型。
- 配置文件或文本格式与对象之间转换。
- `TypeDescriptor`、属性浏览器和序列化工具。
- 枚举、可空数值、矩形、集合等对象的文本显示与解析。

项目结构：

```text
H.Extensions.TypeConverter/
├── DisplayEnumConverter.cs
├── DisplayNameConverter.cs
├── FileSizeDisplayTypeConverter.cs
├── NullableBooleanConverter.cs
├── NullableByteConverter.cs
├── NullableInt16Converter.cs
├── NullableInt32Converter.cs
├── NuallableInt64Converter.cs
├── NullableFloatConverter.cs
├── NullableDoubleConverter.cs
├── NullableDateTimeConverter.cs
├── ObservableCollectionTypeConverter.cs
├── StringObservableCollectionConverter.cs
├── IntRectConverter.cs
├── Round2RectConverter.cs
└── DelegateConverter.cs
```

部分类型名称保留历史拼写，例如 `NuallableInt64Converter`。引用现有公开类型时必须使用实际名称。

---

## 2. TypeConverter 与 ValueConverter 的区别

| 对比项 | `TypeConverter` | `IValueConverter` |
|---|---|---|
| 命名空间 | `System.ComponentModel` | `System.Windows.Data` |
| 主要调用方 | TypeDescriptor、PropertyGrid、设计器、XAML 属性解析、序列化。 | WPF Binding 引擎。 |
| 转换方法 | `ConvertFrom` / `ConvertTo`。 | `Convert` / `ConvertBack`。 |
| 上下文 | `ITypeDescriptorContext`、`CultureInfo`、目标类型。 | 源值、目标类型、参数、区域信息。 |
| 是否自动监听绑定 | 否。 | 是，随 Binding 源值变化再次调用。 |
| 典型用途 | 文本编辑、配置、属性面板、类型字符串化。 | 运行时 UI 显示、可见性、画刷、MultiBinding。 |

示例：

```text
"2025-01-01" → DateTime?         使用 NullableDateTimeConverter
bool → Visibility                 使用 IValueConverter
long → "1.25MB"                  两者都可，但属性编辑场景优先 TypeConverter
```

WPF Binding 转换请阅读 [`development-guide-value-converter.md`](development-guide-value-converter.md)。

---

## 3. 注册 TypeConverter

### 3.1 在类型上注册

```csharp
[TypeConverter(typeof(SizeToDisplayTypeConverter))]
public class FileSizeValue
{
    public long Value { get; set; }
}
```

当整个类型都以统一文本形式转换时，使用类型级特性。

### 3.2 在属性上注册

```csharp
public class ExportOptions
{
    [TypeConverter(typeof(NullableDateTimeConverter))]
    public DateTime? ExpireAt { get; set; }
}
```

当只有特定属性需要特殊编辑或序列化方式时，使用属性级特性。

### 3.3 在枚举上注册

```csharp
[TypeConverter(typeof(DisplayEnumConverter))]
public enum ExportFormat
{
    [Display(Name = "PDF 文档")]
    Pdf,

    [Display(Name = "Excel 工作簿")]
    Excel,

    [Display(Name = "逗号分隔文本")]
    Csv
}
```

`DisplayEnumConverter` 继承 `EnumConverter`，向字符串转换时读取枚举字段的 `DisplayAttribute.Name`；未标记时回退到成员名称。

---

## 4. `ConvertFrom` 与 `ConvertTo`

典型 TypeConverter：

```csharp
public class SizeToDisplayTypeConverter : TypeConverter
{
    public override bool CanConvertFrom(
        ITypeDescriptorContext context,
        Type sourceType)
    {
        return sourceType == typeof(string) ||
               base.CanConvertFrom(context, sourceType);
    }

    public override object ConvertFrom(
        ITypeDescriptorContext context,
        CultureInfo culture,
        object value)
    {
        if (value is string text)
            return Parse(text);

        return base.ConvertFrom(context, culture, value);
    }

    public override bool CanConvertTo(
        ITypeDescriptorContext context,
        Type destinationType)
    {
        return destinationType == typeof(string) ||
               base.CanConvertTo(context, destinationType);
    }

    public override object ConvertTo(
        ITypeDescriptorContext context,
        CultureInfo culture,
        object value,
        Type destinationType)
    {
        if (destinationType == typeof(string))
            return Format(value, culture);

        return base.ConvertTo(context, culture, value, destinationType);
    }
}
```

规则：

- 在 `CanConvertFrom` / `CanConvertTo` 中声明支持的转换。
- `ConvertFrom` 处理输入表示，最常见是字符串。
- `ConvertTo` 处理目标表示，最常见是字符串。
- 不支持的类型调用 `base`，不要返回错误类型。
- 解析失败应抛出清晰的 `FormatException` 或回退到基类，不要静默生成误导性业务值。

---

## 5. 枚举显示 `DisplayEnumConverter`

项目实现：

```csharp
public class DisplayEnumConverter : EnumConverter
{
    public DisplayEnumConverter(Type type) : base(type)
    {
    }

    public override object ConvertTo(
        ITypeDescriptorContext context,
        CultureInfo culture,
        object value,
        Type destinationType)
    {
        if (destinationType == typeof(string) && value != null)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            DisplayAttribute display = field?
                .GetCustomAttribute<DisplayAttribute>();

            return display?.Name ?? value.ToString();
        }

        return base.ConvertTo(context, culture, value, destinationType);
    }
}
```

用途：

- PropertyGrid 中展示本地化或业务友好的枚举名称。
- Form 自动生成界面时显示 `DisplayAttribute.Name`。
- 日志或配置 UI 中获得可读枚举文本。

注意：当前实现主要自定义“枚举值 → 字符串”显示。字符串反向转换继续交由 `EnumConverter`，通常使用枚举成员名而不是 `DisplayAttribute.Name`。如果需要从显示名反向转换，必须实现明确映射并处理重复名称和本地化。

---

## 6. 显示名称 `DisplayNameConverter`

`DisplayNameConverter` 读取类型上的 `DisplayNameAttribute`：

```csharp
[DisplayName("数据库备份")]
public class BackupTask
{
}
```

```csharp
var converter = new DisplayNameConverter();
string name = converter.ConvertToString(new BackupTask());
// 数据库备份
```

当前实现中，未找到 `DisplayNameAttribute` 或输入为 `null` 时返回空字符串。

适用：

- 类型列表、插件列表和工具箱显示名称。
- PropertyGrid 分类或对象描述。
- 反射式 UI 的友好名称。

如果项目统一使用 `DisplayAttribute`，不要混用两种元数据而不定义优先级。框架中的枚举展示使用 `DisplayAttribute`，类型显示需要依据实际模块约定选择 `DisplayNameAttribute` 或 `DisplayAttribute`。

---

## 7. 可空值转换器

项目提供可空基础类型转换器：

```text
NullableBooleanConverter
NullableByteConverter
NullableInt16Converter
NullableInt32Converter
NuallableInt64Converter
NullableFloatConverter
NullableDoubleConverter
NullableDateTimeConverter
```

以 `NullableDateTimeConverter` 为例：

```csharp
public class NullableDateTimeConverter : DateTimeConverter
{
    public override bool CanConvertFrom(
        ITypeDescriptorContext context,
        Type sourceType)
    {
        return sourceType == typeof(string) ||
               base.CanConvertFrom(context, sourceType);
    }

    public override object ConvertFrom(
        ITypeDescriptorContext context,
        CultureInfo culture,
        object value)
    {
        if (value is null)
            return null;

        if (value is string text && string.IsNullOrWhiteSpace(text))
            return null;

        return base.ConvertFrom(context, culture, value);
    }
}
```

行为：

```text
null / 空白字符串 → null
有效文本          → 基础类型转换结果
无效文本          → 基础转换器抛出格式异常
```

属性示例：

```csharp
public class ScheduleOptions
{
    [TypeConverter(typeof(NullableDateTimeConverter))]
    public DateTime? StartAt { get; set; }
}
```

可空转换器尤其适合 PropertyGrid 或 Form 中“空文本表示未设置”的输入体验。

---

## 8. 文件大小 `SizeToDisplayTypeConverter`

`SizeToDisplayTypeConverter` 将字节数显示为：

```text
Byte
KB
MB
GB
TB
```

使用：

```csharp
[TypeConverter(typeof(SizeToDisplayTypeConverter))]
public long FileSize { get; set; }
```

示例：

```text
1024       → 1KB
1048576    → 1MB
```

反向转换接受 `Byte`、`KB`、`MB`、`GB`、`TB` 结尾文本并返回字节数。

注意当前实现：

- 格式化以 1024 为单位。
- 显示最多保留两位小数。
- 反向解析主要使用整数解析，`1.5MB` 不一定能按预期解析。
- 无法解析时回退为 `0`。
- 大于 TB 的数值和溢出需要额外验证。

如果配置输入需要精确小数、国际化单位或严格错误提示，应提供项目专属转换器，而不是依赖此通用实现的 `0` 回退。

---

## 9. 几何类型：`Rect` 与 IntRect

项目提供：

```text
IntRectConverter
Round2RectConverter
```

`Round2RectConverter` 用于将 `Rect` 序列化为：

```text
X,Y,Width,Height
```

并在字符串输出时将数值舍入到两位小数：

```text
12.345,20,100.999,80 → 12.35,20,101,80
```

适用：

- ROI 区域配置。
- 图像裁剪、选区和设计器布局保存。
- PropertyGrid 中的矩形编辑。

使用前确定坐标体系：

```text
像素坐标
DIP（设备无关像素）
逻辑画布坐标
图像原始像素坐标
```

TypeConverter 只负责文本与对象转换，不能自动完成 DPI、缩放比例和坐标系转换。

---

## 10. 集合转换器

### 10.1 `ObservableCollectionTypeConverter<Item, ItemConverter>`

泛型集合转换器可将：

```text
ObservableCollection<Item>  ?  空格分隔字符串
```

实现约定：

```text
ConvertTo   每个元素调用 ItemConverter.ConvertToString，使用空格连接
ConvertFrom 按空格拆分，逐项调用 ItemConverter.ConvertFrom
```

示例：

```csharp
public class IntListConverter :
    ObservableCollectionTypeConverter<int, Int32Converter>
{
}

public class FilterOptions
{
    [TypeConverter(typeof(IntListConverter))]
    public ObservableCollection<int> SelectedIds { get; set; } = new();
}
```

```text
1 2 10 20
```

限制：

- 元素文本不能包含空格。
- 分隔符未做转义。
- 空白字符串转换为空集合。
- 配置格式需要逗号、JSON、引号或复杂对象时，应使用专用序列化格式。

### 10.2 `StringObservableCollectionConverter`

该转换器用于字符串集合的文本表示。使用前检查实际分隔符与反向解析规则，避免内容本身包含分隔符导致数据损坏。

对于用户可编辑的复杂集合，优先提供明确的列表编辑 UI，不要依赖一行分隔文本。

---

## 11. 与 XAML 的关系

WPF XAML 在某些属性赋值时会使用 `TypeConverter` 将字符串文本转换为目标类型：

```xaml
<local:ScheduleOptions StartAt="2025-01-01" />
```

如果 `StartAt` 的类型或属性上注册了适当 `TypeConverter`，XAML 解析器可以完成转换。

但 TypeConverter 不是 WPF Binding 转换器：

```xaml
<!-- 这是 Binding 转换器，不会自动寻找 TypeConverter。 -->
<TextBlock Text="{Binding StartAt}" />
```

需要格式化 Binding 时：

```xaml
<TextBlock Text="{Binding StartAt, StringFormat={}{0:yyyy-MM-dd}}" />
```

或使用 `IValueConverter`：

```xaml
<TextBlock Text="{Binding StartAt, Converter={local:DateToTextConverter}}" />
```

---

## 12. 与 PropertyGrid 和 Form 的关系

`TypeConverter` 常被 PropertyGrid、Form 和 TypeDescriptor 驱动的编辑器用于：

- 判断属性是否可从文本编辑。
- 将属性值显示为字符串。
- 把用户输入文本转换回属性类型。
- 提供标准值列表或自定义属性描述。

示例：

```csharp
public class ConnectionOptions
{
    [Display(Name = "超时时间")]
    [TypeConverter(typeof(NullableInt32Converter))]
    public int? TimeoutSeconds { get; set; }

    [Display(Name = "过期时间")]
    [TypeConverter(typeof(NullableDateTimeConverter))]
    public DateTime? ExpiresAt { get; set; }
}
```

`DisplayAttribute` 控制显示元数据，`TypeConverter` 控制值与文本的互转；两者职责不同，可以组合使用。

复杂编辑器（文件选择、颜色、集合、树、图像）通常需要 `UITypeEditor`、PropertyGrid 编辑器模板或框架 Form 属性编辑器，而不只是 TypeConverter。

---

## 13. 自定义 TypeConverter 示例

下面实现一个将 `Size` 转换为 `宽,高` 文本的转换器：

```csharp
using System.ComponentModel;
using System.Globalization;
using System.Windows;

public class SizeTextConverter : TypeConverter
{
    public override bool CanConvertFrom(
        ITypeDescriptorContext context,
        Type sourceType)
    {
        return sourceType == typeof(string) ||
               base.CanConvertFrom(context, sourceType);
    }

    public override object ConvertFrom(
        ITypeDescriptorContext context,
        CultureInfo culture,
        object value)
    {
        if (value is not string text)
            return base.ConvertFrom(context, culture, value);

        string[] values = text.Split(',');
        if (values.Length != 2 ||
            !double.TryParse(values[0], NumberStyles.Float, culture, out double width) ||
            !double.TryParse(values[1], NumberStyles.Float, culture, out double height))
        {
            throw new FormatException("Size 必须是“宽,高”格式。");
        }

        return new Size(width, height);
    }

    public override bool CanConvertTo(
        ITypeDescriptorContext context,
        Type destinationType)
    {
        return destinationType == typeof(string) ||
               base.CanConvertTo(context, destinationType);
    }

    public override object ConvertTo(
        ITypeDescriptorContext context,
        CultureInfo culture,
        object value,
        Type destinationType)
    {
        if (destinationType == typeof(string) && value is Size size)
        {
            return string.Format(culture, "{0},{1}", size.Width, size.Height);
        }

        return base.ConvertTo(context, culture, value, destinationType);
    }
}
```

注册：

```csharp
[TypeConverter(typeof(SizeTextConverter))]
public Size PreviewSize { get; set; }
```

设计原则：

- 文本格式必须稳定且有文档说明。
- `ConvertFrom` 与 `ConvertTo` 尽量互逆。
- 解析使用传入的 `CultureInfo`，除非格式明确规定为不变区域性。
- 无效输入提供可定位的异常信息。
- 不在转换器中访问文件、数据库、IOC 或 UI 元素。

---

## 14. `TypeDescriptionProvider` 与运行时注册

某些情况下不能修改源类型的特性，可以通过 `TypeDescriptor` 注册转换器：

```csharp
TypeDescriptor.AddAttributes(
    typeof(ExternalOptions),
    new TypeConverterAttribute(typeof(ExternalOptionsConverter)));
```

注意：

- 应在应用启动阶段统一注册。
- 注册影响全局 TypeDescriptor 行为。
- 第三方类型或插件类型的运行时扩展要避免重复注册。
- 测试环境需清楚注册的生命周期和隔离方式。

除非必须扩展第三方类型，优先在自己的类型或属性上使用静态 `[TypeConverter]` 特性。

---

## 15. 区域性、本地化与序列化

`ConvertFrom` / `ConvertTo` 都接收 `CultureInfo`。

| 用途 | 建议 |
|---|---|
| 面向用户的 PropertyGrid 输入 | 使用传入 `culture`。 |
| 稳定配置文件 | 使用明确、不变的格式和 `CultureInfo.InvariantCulture`。 |
| 可本地化枚举显示 | 使用资源或 `DisplayAttribute` 的本地化能力。 |
| 数值/日期文本 | 不要混淆显示格式和机器存储格式。 |

例如 `1,5` 在某些区域性中表示 `1.5`，在另一些区域性中可能表示千位分隔。配置持久化应定义固定格式，UI 输入则遵循用户区域性。

---

## 16. 安全与可靠性

- TypeConverter 的输入可能来自 XAML、配置、剪贴板、文件或用户编辑器；不能假定可信。
- 限制字符串长度、数值范围、集合数量和递归深度。
- 不在转换器中执行反射实例化、脚本、委托解析或动态类型加载，除非有严格白名单。
- 不使用 TypeConverter 加密或保存密码、令牌等敏感数据。
- 项目中的部分历史转换器/加密相关代码处于注释状态，不应视为可用生产方案。
- 转换失败时不要静默返回可能造成数据丢失的默认业务值；如文件大小解析失败返回 `0` 的场景，应由调用方评估是否可接受。

---

## 17. 常见问题

### PropertyGrid 中仍显示类型名称

检查类型/属性是否注册了 `[TypeConverter]`，转换器是否支持 `ConvertTo(..., typeof(string))`，以及编辑器是否使用 `TypeDescriptor`。

### XAML 无法把文本转换成目标类型

检查转换器是否正确注册、`CanConvertFrom(typeof(string))` 是否返回 `true`，以及 `ConvertFrom` 是否接受当前文本格式。

### Binding 没有使用 TypeConverter

这是正常行为。Binding 使用 `IValueConverter`，不是 `System.ComponentModel.TypeConverter`。请使用 `StringFormat` 或 `H.ValueConverter`。

### 空字符串无法设置为 null

使用对应的 `Nullable*Converter`，并确认属性类型为可空类型，例如 `int?`、`DateTime?`。

### 枚举显示名称能显示但不能从显示名解析

`DisplayEnumConverter` 主要处理输出显示。若需要显示名反向解析，创建专用映射转换器并明确重复显示名和本地化策略。

### 集合文本解析后数据异常

确认元素文本不含当前分隔符。复杂内容使用 JSON 或明确的集合编辑器，而不是空格分隔字符串。

---

## 18. 二次开发建议

- 文本/配置/PropertyGrid 编辑使用 `TypeConverter`。
- Binding 显示转换使用 `IValueConverter`。
- XAML 一次性值构造使用 `MarkupExtension`。
- 为可空属性提供空文本到 `null` 的明确规则。
- 枚举显示优先使用 `DisplayAttribute` 与 `DisplayEnumConverter`。
- 新转换器实现 `CanConvertFrom`、`CanConvertTo`、`ConvertFrom`、`ConvertTo` 的一致契约。
- 明确区域性与序列化格式，避免用户显示格式污染机器配置。
- 不在转换器中执行 I/O、网络、授权、加密或业务副作用。
- 在 PropertyGrid、Form、XAML 解析和配置读写场景分别测试。
