# MarkupExtension 开发与使用文档

**参考项目：** `H.MarkupExtension`  
**核心类型：** `System.Windows.Markup.MarkupExtension`、`GetValueExtensionBase`、`GetValueToTypeExtensionBase`、`GetTypeExtensionBase`、`InvokeMethodExtensionBase`  
**相关能力：** XAML 值生成、枚举和范围数据源、类型构造、日期时间、文件路径、可见性、随机值

本文结合项目 `H.MarkupExtension` 中的实际实现，介绍 WPF `MarkupExtension` 的使用方式、开发模式、限制和技巧。

---

## 1. 什么是 MarkupExtension

`MarkupExtension` 是 WPF XAML 的值提供机制。它在 XAML 解析/对象构造阶段执行 `ProvideValue(IServiceProvider)`，返回赋给目标属性的对象。

```text
XAML 属性
    ↓
{h:GetRange Start=1, Count=5}
    ↓
MarkupExtension.ProvideValue(...)
    ↓
返回 List<int>
    ↓
设置到目标属性
```

示例：

```xaml
<ItemsControl ItemsSource="{h:GetRange Start=1, Count=5}" />
```

`GetRangeExtension` 的实现：

```csharp
public class GetRangeExtension : MarkupExtension
{
    public int Start { get; set; }
    public int Count { get; set; }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return Enumerable.Range(Start, Count).ToList();
    }
}
```

XAML 中类名的 `Extension` 后缀可省略：

```xaml
{h:GetRange Start=1, Count=5}
```

等价于：

```xaml
{h:GetRangeExtension Start=1, Count=5}
```

---

## 2. 适用场景与边界

MarkupExtension 适合在 XAML 加载时产生值：

- 根据枚举类型生成 ItemsSource。
- 生成整数范围、`DateTime`、`TimeSpan`、`Visibility`、文件夹路径等常量值。
- 构造泛型 `Type`。
- 返回无状态对象、转换器或轻量配置对象。
- 基于 XAML 参数简化重复声明。

不适合：

- 长时间网络、数据库或文件 I/O。
- 需要持续监听 ViewModel 属性变化的动态业务状态。
- 依赖异步结果的值生成。
- 持有 Window、Page、Control 的长期引用。
- 在 `ProvideValue` 中执行安全敏感或不可预测操作。

对持续动态值优先使用：

```text
Binding / MultiBinding
DynamicResource
DependencyProperty
Behavior
ViewModel
```

`ProvideValue` 不是 ViewModel 生命周期回调，也不是业务初始化入口。

---

## 3. 项目中的扩展一览

`H.MarkupExtension` 提供：

| 类型 | XAML 用法 | 返回值/用途 |
|---|---|---|
| `GetRangeExtension` | `{h:GetRange Start=1, Count=10}` | `List<int>`。 |
| `GetEnumSourceExtension` | `{h:GetEnumSource EnumType={x:Type local:Mode}}` | 枚举值序列。 |
| `GetEnumGroupSourceExtension` | `{h:GetEnumGroupSource EnumType=..., GroupName=...}` | 按 `DisplayAttribute.GroupName` 筛选的枚举序列。 |
| `GetEnumExtension` | `{h:GetEnum Type={x:Type local:Mode}, Value=Edit}` | 指定枚举成员。 |
| `GenericTypeExtension` | `{h:GenericType GenericType={x:Type local:Repository`1}, TypeArgument={x:Type local:Order}}` | 构造后的泛型 `Type`。 |
| `GetDateTimeExtension` | `{h:GetDateTime Value=2025-01-01}` | `DateTime`。 |
| `TimeSpanParseExtension` | `{h:TimeSpanParse Value=00:00:05}` | `TimeSpan`。 |
| `TimeSpanFromMethodExtension` | 见项目实现参数 | 通过时间单位/方法构造 `TimeSpan`。 |
| `GetVisibilityExtension` | `{h:GetVisibility Value=Collapsed}` | `Visibility`。 |
| `SpecialFolderExtension` | `{h:SpecialFolder SpecialFolder=MyDocuments}` | 特殊目录路径字符串。 |
| `GetInstanceExtension` | `{h:GetInstance Type={x:Type local:MyOptions}}` | 通过默认构造函数创建对象。 |
| `GetTypeConverterExtension` | `{h:GetTypeConverter Type={x:Type sys:Int32}}` | 指定类型的 `TypeConverter`。 |
| `GetIConvertibleExtension` | `{h:GetIConvertible Type={x:Type sys:Int32}, Value=10}` | 转换后的基础类型值。 |
| `IntRandowmExtension` | `{h:IntRandowm ...}` | 随机整数。 |
| `DoubleRandowmExtension` | `{h:DoubleRandowm ...}` | 随机双精度值。 |
| `PointExtension` / `PointRandomExtension` | `{h:Point ...}` | `Point`。 |
| `RectMarkupExtension` / `RectRandomExtension` | `{h:RectMarkup ...}` | `Rect`。 |

> `IntRandowmExtension` 和 `DoubleRandowmExtension` 的拼写为现有公开 API。引用时必须使用实际类型名，不应为了纠正拼写修改已发布 API。

---

## 4. XAML 命名空间

项目统一 XAML 命名空间：

```xaml
xmlns:h="https://github.com/HeBianGu"
```

也可以使用 CLR 命名空间：

```xaml
xmlns:markup="clr-namespace:H.MarkupExtension;assembly=H.MarkupExtension"
```

推荐使用统一命名空间：

```xaml
<ComboBox ItemsSource="{h:GetEnumSource EnumType={x:Type local:ExportFormat}}" />
```

使用 `{x:Type}` 传入类型：

```xaml
{h:GetEnumSource EnumType={x:Type local:ExportFormat}}
```

使用位置参数时，扩展必须提供对应构造函数或构造函数参数名：

```xaml
{h:GetRange 1, 10}
```

如果不确定构造函数和参数顺序，使用具名参数，代码可读性更好：

```xaml
{h:GetRange Start=1, Count=10}
```

---

## 5. `ProvideValue` 的执行时机

`ProvideValue` 一般在 XAML 加载、资源字典解析或模板构造期间执行。其返回值会被赋给目标属性。

```csharp
public override object ProvideValue(IServiceProvider serviceProvider)
{
    return DateTime.Parse(Value);
}
```

关键特征：

- 通常是一次性值提供，不会自动跟随数据变化。
- 可能在设计器、资源字典、Style、`ControlTemplate` 或运行时控件树中执行。
- 同一 XAML 结构可能多次创建扩展实例或多次调用 `ProvideValue`。
- 不应假设目标对象已加载、已连接到 Window 或拥有 DataContext。
- 异常可能导致整个 XAML 资源加载失败。

因此 `ProvideValue` 应保持：

```text
快速
无副作用
可预测
可重复
可失败诊断
```

---

## 6. `IServiceProvider` 与 XAML 服务

`ProvideValue(IServiceProvider serviceProvider)` 的参数可用于获取 XAML 解析上下文。常见服务：

| 服务类型 | 用途 |
|---|---|
| `IProvideValueTarget` | 获取目标对象和目标属性。 |
| `IRootObjectProvider` | 获取 XAML 根对象。 |
| `IXamlSchemaContextProvider` | 获取 XAML Schema 上下文。 |
| `IAmbientProvider` | 查询环境属性和资源上下文。 |
| `IXamlTypeResolver` | 解析 XAML 类型名称。 |

安全读取目标对象：

```csharp
public override object ProvideValue(IServiceProvider serviceProvider)
{
    var target = serviceProvider?.GetService(
        typeof(IProvideValueTarget)) as IProvideValueTarget;

    object targetObject = target?.TargetObject;
    object targetProperty = target?.TargetProperty;

    // 返回值，不依赖 targetObject 必然为 FrameworkElement。
    return Value;
}
```

注意：在 Style、Setter、Template、SharedDp、资源字典或设计器场景中，`TargetObject` 可能不是最终控件实例，也可能是内部共享对象。不能假定它一定是 `FrameworkElement`。

---

## 7. 在 Style 和 Template 中返回自身

当扩展作为 Style Setter、Template 或共享资源的一部分时，目标尚未完全确定。此时若扩展需要后续绑定上下文，通常应返回扩展自身：

```csharp
public override object ProvideValue(IServiceProvider serviceProvider)
{
    var target = serviceProvider?.GetService(
        typeof(IProvideValueTarget)) as IProvideValueTarget;

    if (target?.TargetObject is Setter)
        return this;

    return CreateValue();
}
```

常见场景：

```xaml
<Style TargetType="{x:Type TextBlock}">
    <Setter Property="Text" Value="{local:MyExtension}" />
</Style>
```

如果扩展直接返回依赖最终控件的值，Style 共享与模板延迟应用可能失败。

项目中简单的 `GetRangeExtension`、`GetDateTimeExtension` 等返回纯值，无需依赖目标对象，因此可以直接返回值。

---

## 8. 项目基础抽象类

`H.MarkupExtension` 提供多个基类以减少重复属性声明：

```text
GetValueToTypeExtensionBase
GetValueExtensionBase
GetTypeExtensionBase
InvokeMethodExtensionBase
```

用途概览：

| 基类 | 典型参数 | 适用扩展 |
|---|---|---|
| `GetValueExtensionBase` | `Value` | 字符串解析为 `DateTime`、`Visibility` 等。 |
| `GetValueToTypeExtensionBase` | `Type`、`Value` | 枚举解析、TypeConverter、基础类型转换。 |
| `GetTypeExtensionBase` | `Type` | 仅根据类型生成值。 |
| `InvokeMethodExtensionBase` | 类型和方法相关参数 | 调用静态/实例方法生成值。 |

例如 `GetDateTimeExtension`：

```csharp
[MarkupExtensionReturnType(typeof(DateTime))]
public class GetDateTimeExtension : GetValueExtensionBase
{
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return DateTime.TryParse(Value, out var result)
            ? result
            : DateTime.MinValue;
    }
}
```

使用基类时应避免让参数名含义不清。`Value` 适合文本输入；需要多个参数时明确使用 `Start`、`Count`、`EnumType`、`GroupName` 等名称。

---

## 9. 枚举扩展

### 9.1 枚举数据源 `GetEnumSource`

```xaml
<ComboBox
    ItemsSource="{h:GetEnumSource EnumType={x:Type local:ExportFormat}}"
    SelectedItem="{Binding Format}" />
```

定义枚举：

```csharp
public enum ExportFormat
{
    Pdf,
    Excel,
    Csv
}
```

扩展会验证目标类型是否为枚举；非枚举类型应视为 XAML 配置错误。

### 9.2 按分组筛选 `GetEnumGroupSource`

```csharp
public enum ReportType
{
    [Display(GroupName = "Basic")]
    Summary,

    [Display(GroupName = "Basic,Advanced")]
    Detail,

    [Display(GroupName = "Advanced")]
    Diagnostic
}
```

```xaml
<ComboBox
    ItemsSource="{h:GetEnumGroupSource
        EnumType={x:Type local:ReportType},
        GroupName=Basic}" />
```

`GetEnumGroupSourceExtension` 按 `DisplayAttribute.GroupName` 的逗号分隔值筛选枚举成员。

### 9.3 单个枚举值 `GetEnum`

```xaml
<local:ReportOptions
    Type="{h:GetEnum Type={x:Type local:ReportType}, Value=Summary}" />
```

注意枚举解析区分名称和底层值的合法性。对于外部配置或用户输入，不要在 XAML 扩展中假设输入一定可信。

---

## 10. 范围与集合扩展

`GetRangeExtension`：

```xaml
<ComboBox
    ItemsSource="{h:GetRange Start=1, Count=12}"
    SelectedItem="{Binding Month}" />
```

返回：

```csharp
Enumerable.Range(Start, Count).ToList()
```

使用场景：

- 月份、小时、页码等固定整数范围。
- 简单测试数据。
- 无需动态刷新的小型选择列表。

不适合：

- 依赖运行时业务数据的集合。
- 大范围数据。
- 需要分页、过滤、异步加载或国际化显示的选项。

这些场景应使用绑定到 ViewModel 的 `ObservableCollection<T>` 或 `IEnumerable`。

---

## 11. 基础类型、日期与时间

### 11.1 日期时间

```xaml
<local:ScheduleOptions
    StartDate="{h:GetDateTime Value=2025-01-01}" />
```

`GetDateTimeExtension` 使用 `DateTime.TryParse`。解析失败时返回 `DateTime.MinValue`。

注意：`DateTime.MinValue` 可能是有效业务外值，也可能掩盖 XAML 配置错误。对关键配置建议创建严格扩展，在失败时抛出带上下文的 `InvalidOperationException`。

### 11.2 时间间隔

```xaml
<local:RetryOptions
    Delay="{h:TimeSpanParse Value=00:00:05}" />
```

`TimeSpanParseExtension` 用于解析固定 `TimeSpan`。推荐使用明确格式：

```text
hh:mm:ss
```

避免依赖区域性格式差异。

### 11.3 Visibility

```xaml
<Border Visibility="{h:GetVisibility Value=Collapsed}" />
```

`GetVisibilityExtension` 尝试将文本解析为 `Visibility`；无法解析时返回 `Visibility.Visible`。

对动态可见性使用 Binding + Converter：

```xaml
Visibility="{Binding IsBusy, Converter={x:Static h:Converter.GetTrueToVisible}}"
```

MarkupExtension 返回的是初始化值，不会订阅 `IsBusy` 的变化。

---

## 12. 类型与对象构造

### 12.1 泛型类型 `GenericTypeExtension`

`GenericTypeExtension` 返回构造后的泛型 `Type`：

```xaml
<local:ViewRegistration
    ViewModelType="{h:GenericType
        GenericType={x:Type local:Repository`1},
        TypeArgument={x:Type local:Order}}" />
```

核心实现：

```csharp
return GenericType.MakeGenericType(TypeArgument);
```

也支持 `TypeArguments`，用于多个泛型参数。

使用前检查：

- `GenericType` 不为 `null`。
- 类型确实是泛型类型定义。
- 泛型参数数量和约束匹配。
- 不要把动态、不可信类型名交给此扩展。

### 12.2 创建对象 `GetInstanceExtension`

```xaml
<ContentControl Content="{h:GetInstance Type={x:Type local:SampleOptions}}" />
```

`GetInstanceExtension` 调用：

```csharp
Activator.CreateInstance(Type)
```

因此目标类型必须具有可访问的无参构造函数。

不适合 IOC 服务：

```text
GetInstance → 创建新对象
Ioc 扩展    → 解析已注册服务及其生命周期
```

需要依赖注入、单例或可替换服务时，使用框架 `Ioc` 标记扩展，不要用 `GetInstanceExtension` 绕过容器。

---

## 13. 特殊目录和文件路径

`SpecialFolderExtension`：

```xaml
<local:ExportOptions
    DefaultDirectory="{h:SpecialFolder SpecialFolder=MyDocuments}" />
```

内部调用：

```csharp
Environment.GetFolderPath(SpecialFolder)
```

适合设置默认路径，不保证该目录一定可写。实际文件操作前仍应：

- 验证目录存在。
- 处理权限和磁盘异常。
- 允许用户修改路径。
- 不把用户输入拼接为不安全的文件路径。

---

## 14. 随机、Point 和 Rect 扩展

项目包含：

```text
IntRandowmExtension
DoubleRandowmExtension
PointExtension
PointRandomExtension
RectMarkupExtension
RectRandomExtension
```

适合：

- 演示项目、设计时预览和测试数据。
- 初始化简单几何数据。
- 可视化示例。

不适合：

- 安全随机数、令牌、密码或验证码。
- 需要可重复的业务随机算法。
- 对象状态每次加载都必须一致的生产配置。

随机扩展可能在 XAML 重建时重新生成值。若需要稳定随机序列，应使用可设置种子的业务服务或 ViewModel。

---

## 15. 编写自定义 MarkupExtension

### 15.1 最小纯值扩展

```csharp
using System;
using System.Windows.Markup;

namespace MyApp.Markup;

[MarkupExtensionReturnType(typeof(string))]
public class JoinPathExtension : MarkupExtension
{
    public string Left { get; set; }
    public string Right { get; set; }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        if (string.IsNullOrWhiteSpace(Left))
            return Right;

        if (string.IsNullOrWhiteSpace(Right))
            return Left;

        return System.IO.Path.Combine(Left, Right);
    }
}
```

XAML：

```xaml
<local:ExportOptions
    OutputPath="{local:JoinPath Left={Binding BaseDirectory}, Right=report.json}" />
```

注意：Binding 不能总是作为普通字符串参数直接按预期求值。若扩展需要动态绑定，应返回 `Binding` 或使用其他绑定结构，而不是把绑定对象当作最终字符串处理。

### 15.2 带位置参数的扩展

```csharp
[MarkupExtensionReturnType(typeof(Uri))]
public class UriExtension : MarkupExtension
{
    public UriExtension()
    {
    }

    public UriExtension(string value)
    {
        Value = value;
    }

    public string Value { get; set; }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return new Uri(Value, UriKind.RelativeOrAbsolute);
    }
}
```

使用：

```xaml
Source="{local:Uri Assets/logo.png}"
```

位置参数应少且语义明确；多个复杂参数优先使用具名属性。

### 15.3 返回 Binding

MarkupExtension 可以创建并返回 `BindingBase`：

```csharp
public class PropertyBindingExtension : MarkupExtension
{
    public string Path { get; set; }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        var binding = new Binding(Path)
        {
            Mode = BindingMode.OneWay
        };

        return binding.ProvideValue(serviceProvider);
    }
}
```

这种模式适合封装重复 Binding 配置。必须调用：

```csharp
binding.ProvideValue(serviceProvider)
```

以便 WPF 正确连接绑定目标。

不要在扩展中自行订阅 `PropertyChanged` 来模拟 Binding；应让 WPF 绑定引擎管理生命周期。

---

## 16. 参数、验证与错误处理

MarkupExtension 的参数来自 XAML，错误通常在资源加载时暴露。推荐策略：

| 情况 | 建议 |
|---|---|
| 缺少必填类型 | 抛出 `InvalidOperationException`，包含属性名。 |
| 类型不匹配 | 抛出 `ArgumentException`。 |
| 可选值为空 | 返回清晰默认值或 `DependencyProperty.UnsetValue`。 |
| 格式可恢复 | 使用 `TryParse` 并记录明确规则。 |
| 关键配置格式错误 | 不要静默吞掉，尽早失败。 |

示例：

```csharp
public override object ProvideValue(IServiceProvider serviceProvider)
{
    if (EnumType == null)
        throw new InvalidOperationException(
            $"{nameof(EnumType)} must be specified.");

    Type actualType = Nullable.GetUnderlyingType(EnumType) ?? EnumType;
    if (!actualType.IsEnum)
        throw new ArgumentException("Type must be an enum.", nameof(EnumType));

    return Enum.GetValues(actualType);
}
```

项目中的 `GetEnumGroupSourceExtension` 使用相同原则验证 `EnumType`。

---

## 17. 设计器与资源字典兼容性

MarkupExtension 需要兼容：

```text
Visual Studio XAML Designer
资源字典加载
Style / Setter
ControlTemplate
DataTemplate
运行时窗口
```

建议：

- 不在 `ProvideValue` 中依赖 `Application.Current.MainWindow`。
- 不依赖 DataContext、Loaded 事件或最终布局尺寸。
- `serviceProvider` 允许为 `null`，设计器或测试场景下应安全处理。
- 返回类型使用 `[MarkupExtensionReturnType]` 提升工具提示和设计器支持。
- 对 Style/Template 目标场景按需返回 `this`。
- 不将 `TargetObject` 缓存到静态字段。

---

## 18. 性能与线程安全

- `ProvideValue` 应是同步且快速的。
- 不在其中执行网络、数据库、磁盘扫描或大量反射。
- 不修改全局状态、注册事件或启动后台任务。
- 不缓存与目标控件绑定的可变对象到静态字段。
- 若返回可变集合，明确它是否会在多个控件间共享。
- 枚举、范围和纯值扩展适合返回新对象，避免调用方互相影响。
- 对需要多线程或异步初始化的状态，使用 ViewModel/Service，而不是 MarkupExtension。

---

## 19. MarkupExtension、Converter、Binding 与 Resource 的选择

| 需求 | 推荐方案 |
|---|---|
| XAML 加载时产生一次性值 | `MarkupExtension`。 |
| 根据绑定值转换显示值 | `IValueConverter`。 |
| 持续反映 ViewModel 属性变化 | `Binding` / `MultiBinding`。 |
| 随主题或资源字典切换 | `DynamicResource`。 |
| 共享固定对象、模板、画刷 | `StaticResource`。 |
| 控件交互事件 | Behavior / Trigger / Command。 |
| 全局服务实例 | IOC 标记扩展或构造函数注入。 |

常见错误：将 `MarkupExtension` 误用为实时数据绑定。`ProvideValue` 返回普通对象后，后续 ViewModel 改变通常不会自动更新该对象。

---

## 20. 常见问题

### XAML 找不到扩展

检查项目是否引用 `H.MarkupExtension`，并使用：

```xaml
xmlns:h="https://github.com/HeBianGu"
```

或：

```xaml
xmlns:markup="clr-namespace:H.MarkupExtension;assembly=H.MarkupExtension"
```

### `ProvideValue` 中目标对象为 null 或不是控件

Style、Setter、Template 和资源字典环境中这是正常现象。不要直接转换为 `FrameworkElement`，应判断 `IProvideValueTarget` 和目标类型。

### 绑定没有更新

普通 MarkupExtension 返回的是一次性值。需要动态更新时返回 `Binding.ProvideValue(serviceProvider)`，或在 XAML 中直接使用 Binding。

### 使用泛型类型时抛出异常

检查 `GenericType` 是否为泛型类型定义、参数数量是否匹配、类型约束是否满足。

### 枚举数据源为空或加载失败

检查 `EnumType` 是否指定、是否真的是枚举、分组筛选时成员是否使用 `DisplayAttribute.GroupName`。

### 资源字典加载时应用崩溃

检查扩展是否抛出参数/解析异常。对可选值可使用安全默认值；对必填配置应提供包含属性名的异常信息，便于定位。

---

## 21. 二次开发建议

- 只为“XAML 加载时的值构造”创建 MarkupExtension。
- 保持 `ProvideValue` 快速、无副作用并且不依赖最终视觉树。
- 优先复用 `H.MarkupExtension` 的现有扩展和基类。
- 参数使用清晰、稳定的属性名，必填参数要验证。
- 使用 `[MarkupExtensionReturnType]` 标注主要返回类型。
- 在 Style、Template 和资源字典中使用前，验证 `IProvideValueTarget` 场景。
- 动态数据用 Binding，主题资源用 DynamicResource，业务对象用 IOC/Service。
- 不修正已公开扩展类的历史拼写，以免破坏现有 XAML。
- 为新扩展提供最小 XAML 示例，并在测试项目中验证设计器和运行时加载。

---

## 22. `MarkupCommandBase`：将命令作为 MarkupExtension

`H.Common.Commands.MarkupCommandBase` 同时实现 `MarkupExtension`、`ICommand` 和 `INotifyPropertyChanged`：

```csharp
[MarkupExtensionReturnType(typeof(ICommand))]
public abstract class MarkupCommandBase : MarkupExtension, ICommand, INotifyPropertyChanged
{
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return this;
    }
}
```

这使派生命令可以直接在 XAML 中创建并赋给 `Command`，无需先在资源字典声明实例：

```xaml
<Button
    Command="{h:ShowGuideCommand}"
    Content="功能引导" />
```

与普通 ViewModel 命令的区别：

| 方式 | 命令来源 | 适用场景 |
|---|---|---|
| `Command="{Binding SaveCommand}"` | ViewModel 实例 | 依赖页面状态、可测试业务命令。 |
| `Command="{h:MyCommand}"` | MarkupExtension 创建的命令对象 | 框架级、通用、可在 XAML 独立使用的命令。 |
| `Command="{StaticResource MyCommand}"` | 资源字典实例 | 共享参数化命令或应用级命令。 |

### 22.1 基础命令实现

```csharp
public class ClearTextBoxCommand : MarkupCommandBase
{
    public override bool CanExecute(object parameter)
    {
        return parameter is TextBox textBox && !string.IsNullOrEmpty(textBox.Text);
    }

    public override void Execute(object parameter)
    {
        if (parameter is TextBox textBox)
            textBox.Clear();
    }
}
```

```xaml
<TextBox x:Name="Editor" />
<Button
    Command="{local:ClearTextBoxCommand}"
    CommandParameter="{Binding ElementName=Editor}"
    Content="清空" />
```

`MarkupCommandBase.CanExecute()` 默认返回 `true`，派生类可根据命令参数或轻量 UI 状态覆盖它。基类的 `CanExecuteChanged` 转发至 `CommandManager.RequerySuggested`，因此 WPF 重新查询命令状态时会调用 `CanExecute()`。

### 22.2 `ProvideValue` 与目标元素

基类在 `ProvideValue()` 中保存 `IServiceProvider` 并返回自身，因此一个命令实例同时是：

```text
XAML 扩展对象
    +
ICommand 命令对象
    +
INotifyPropertyChanged 状态对象
```

基类通过 `Target` 暴露 `IProvideValueTarget`，可用于获取 XAML 目标对象和属性；`GetTargetElement(parameter)` 优先把 `CommandParameter` 作为目标 `UIElement`。

实践中应优先显式传递 `CommandParameter`：

```xaml
CommandParameter="{Binding ElementName=Editor}"
```

不要依赖 `Target.TargetObject` 一定是最终控件。命令位于 Style、Setter、Template 或资源字典时，目标可能是共享/内部对象，而不是实际 `UIElement`。

### 22.3 异步命令 `AsyncMarkupCommandBase`

`AsyncMarkupCommandBase` 继承 `MarkupCommandBase`，将 `Execute()` 包装为异步执行：

```csharp
public abstract class AsyncMarkupCommandBase : MarkupCommandBase
{
    public override async void Execute(object parameter)
    {
        _isExecuting = true;
        try
        {
            await ExecuteAsync(parameter);
            CommandManager.InvalidateRequerySuggested();
        }
        finally
        {
            _isExecuting = false;
        }
    }

    public override bool CanExecute(object parameter) => !_isExecuting;

    public virtual Task ExecuteAsync(object parameter) => Task.CompletedTask;
}
```

实现示例：

```csharp
public class RefreshCommand : AsyncMarkupCommandBase
{
    public override async Task ExecuteAsync(object parameter)
    {
        await _service.RefreshAsync();
    }
}
```

异步基类在执行期间令 `CanExecute()` 返回 `false`，用于避免重复点击。注意当前实现的 `Execute()` 是 `async void`，异常会回到 WPF 同步上下文；派生类应在 `ExecuteAsync()` 中处理可预期异常并向用户显示反馈，避免让异常直接终止 UI 流程。

### 22.4 `DisplayMarkupCommandBase`

`DisplayMarkupCommandBase` 继承 `AsyncMarkupCommandBase`，并实现图标、名称和说明相关接口。框架中 `ShowGuideCommand`、`ShowImageFileCommand`、`ShowDataGridCommand` 等通用展示命令均采用这一路径。

适用：

- 可直接写在 Button、MenuItem、工具栏或模板中的框架命令。
- 命令本身具备稳定显示元数据（名称、描述、图标）。
- 命令只需命令参数或 IOC 服务，不依赖页面专属 ViewModel 状态。

不适用：

- 与单个页面状态强绑定的保存、编辑、选择命令。
- 需要构造函数注入和独立单元测试的复杂业务命令。
- 需要取消、并发队列或精细错误恢复的长任务。

此类场景应使用 ViewModel `ICommand` 或专门的异步命令服务。

---

## 23. `MarkupValueConverterBase`：将转换器作为 MarkupExtension

`H.ValueConverter.MarkupValueConverterBase` 同时实现 `MarkupExtension`、`IValueConverter` 和 `INotifyPropertyChanged`：

```csharp
[MarkupExtensionReturnType(typeof(IValueConverter))]
public abstract class MarkupValueConverterBase : MarkupExtension, IValueConverter, INotifyPropertyChanged
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

派生转换器可以直接内联到 Binding：

```xaml
<TextBlock
    Visibility="{Binding IsBusy, Converter={local:BooleanToVisibilityConverter}}" />
```

这避免了先在资源字典中声明：

```xaml
<local:BooleanToVisibilityConverter x:Key="BooleanToVisibility" />
```

再通过：

```xaml
Converter="{StaticResource BooleanToVisibility}"
```

来引用转换器。

### 23.1 基础转换器实现

```csharp
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
}
```

如果绑定不支持反向更新，应明确返回：

```csharp
public override object ConvertBack(
    object value,
    Type targetType,
    object parameter,
    CultureInfo culture)
{
    return Binding.DoNothing;
}
```

不要沿用基类默认的 `NotImplementedException` 到会发生 TwoWay 回写的绑定中。

### 23.2 默认值与无效值

基类提供：

```csharp
DefaultValue      // 默认 DependencyProperty.UnsetValue
DefaultBackValue  // 默认 DependencyProperty.UnsetValue
```

转换器应在无法处理输入时明确选择返回值：

| 返回值 | 含义 |
|---|---|
| `DependencyProperty.UnsetValue` | 转换失败，要求绑定引擎使用 FallbackValue 或默认行为。 |
| `Binding.DoNothing` | 保持目标/源当前值，不执行更新。 |
| `null` | 合法空值；目标属性必须允许。 |
| 具体默认值 | 业务允许的回退显示值。 |

示例：

```csharp
public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
{
    if (value is not decimal amount)
        return DefaultValue;

    return amount.ToString("N2", culture);
}
```

### 23.3 多值转换器

`MarkupMultiValueConverterBase` 同时实现 `MarkupExtension` 与 `IMultiValueConverter`：

```csharp
public abstract class MarkupMultiValueConverterBase : MarkupExtension, IMultiValueConverter
{
    public override object ProvideValue(IServiceProvider serviceProvider) => this;

    public abstract object Convert(
        object[] values,
        Type targetType,
        object parameter,
        CultureInfo culture);
}
```

使用：

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

多值转换器应处理：

```text
DependencyProperty.UnsetValue
Binding.DoNothing
null
values 数量不足
目标类型不匹配
```

### 23.4 刷新限制

`MarkupValueConverterBase` 实现 `INotifyPropertyChanged` 并提供 `RaisePropertyChanged()` 与 `Refresh()`。当前 `Refresh()` 的实现通过调用 `ConvertBack(null, ...)` 尝试触发刷新，代码中也标注该机制“目前作用不大”。

因此不要依赖：

```csharp
converter.RaisePropertyChanged();
```

来让所有使用该转换器的 Binding 自动重新执行 `Convert()`。

需要刷新显示时，优先：

- 让绑定源属性触发 `PropertyChanged`。
- 将可变状态放入 ViewModel 并绑定它。
- 使用 `DynamicResource` 响应主题资源更换。
- 在必要时调用 `BindingExpression.UpdateTarget()`。

转换器应尽可能无状态。若转换器实例保存可变状态，内联 MarkupExtension 和资源字典实例的共享范围不同，容易产生不可预测结果。

---

## 24. 命令和转换器的设计边界

`MarkupCommandBase` 与 `MarkupValueConverterBase` 都通过 `ProvideValue()` 返回 `this`，但职责不同：

| 基类 | 返回对象 | WPF 使用位置 | 推荐职责 |
|---|---|---|---|
| `MarkupCommandBase` | `ICommand` | `Command` 属性 | UI 操作入口和轻量参数路由。 |
| `AsyncMarkupCommandBase` | 异步 `ICommand` | `Command` 属性 | 防止重复执行的短期 UI 异步操作。 |
| `MarkupValueConverterBase` | `IValueConverter` | `Binding.Converter` | 无状态的单值显示/编辑转换。 |
| `MarkupMultiValueConverterBase` | `IMultiValueConverter` | `MultiBinding.Converter` | 多个绑定值的纯组合转换。 |

共同约束：

- 不在 `ProvideValue()` 执行耗时逻辑。
- 不缓存最终控件或 ViewModel 的强引用。
- 不将共享实例作为页面业务状态容器。
- 不把异常、认证、授权和持久化细节隐藏在转换器中。
- 在 Style、Template、资源字典和设计器环境中验证 XAML 加载。

优先顺序：

```text
复杂业务和页面状态      → ViewModel + Service
用户操作                → ViewModel ICommand
框架级、通用 XAML 操作   → MarkupCommandBase
显示值转换              → MarkupValueConverterBase
动态主题资源            → DynamicResource
一次性 XAML 构造值      → 普通 MarkupExtension
```
