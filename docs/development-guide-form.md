# Form 表单二次开发文档
# Form 表单二次开发文档

**适用项目：** `H.Controls.Form`、`H.Controls.Form.PropertyItem`、`H.Modules.Messages.Form`  
**核心类型：** `Form`、`IFormOption`、`ITabFormOption`、`IPropertyItem`、`PropertyItemAttribute`  
**相关能力：** 对象属性自动表单、属性编辑器、属性特性配置、分组/过滤/排序、表单消息弹窗

本文介绍 WPF-Control 中 `Form` 表单控件的详细使用方式，包括如何根据对象自动生成属性编辑界面、如何选择和扩展 `PropertyItem`，以及如何通过特性控制属性显示、编辑器、分组、数据源和交互行为。

---

## 1. Form 表单定位

`H.Controls.Form.Form` 是一个基于反射的对象属性编辑控件。它继承 `ItemsControl`，通过 `SelectObject` 指定目标对象，然后扫描对象属性并为每个属性创建对应的 `IPropertyItem`。

核心用途：

- 设置页自动生成配置表单。
- 快速编辑业务对象。
- 查看对象详情。
- 配合 `IocMessage.Form` 弹出编辑窗口。
- 通过特性控制不同属性的编辑器和展示方式。

典型流程：

```text
SelectObject
    ↓
反射读取属性 PropertyInfo
    ↓
根据 Form 选项过滤属性
    ↓
根据属性类型或 [PropertyItem] 创建 IPropertyItem
    ↓
ItemsControl 展示属性编辑项
```

---

## 2. 基础使用

### 2.1 XAML 中直接使用

```xaml
<h:Form
    SelectObject="{Binding CurrentOptions}"
    UseGroup="True"
    TitleWidth="120"
    MessageWidth="20" />
```

常用命名空间：

```xaml
xmlns:h="https://github.com/HeBianGu"
```

### 2.2 绑定对象示例

```csharp
public class UserOptions
{
    [Display(Name = "用户名", GroupName = "基础", Order = 1)]
    public string UserName { get; set; }

    [Display(Name = "启用", GroupName = "基础", Order = 2)]
    public bool IsEnabled { get; set; }

    [Display(Name = "创建时间", GroupName = "高级", Order = 3)]
    public DateTime CreateTime { get; set; }
}
```

绑定：

```csharp
public UserOptions CurrentOptions { get; } = new UserOptions();
```

`Form` 会自动为：

- `string` 创建 `TextPropertyItem`。
- `bool` 创建 `BoolPropertyItem`。
- `DateTime` 创建 `DateTimePropertyItem`。

---

## 3. 使用 `IocMessage.Form` 弹出表单

如果已注册 `H.Modules.Messages.Form`：

```csharp
services.AddFormMessageService();
```

可以通过 `IocMessage.Form` 显示对象编辑或查看窗口。

### 3.1 编辑对象

```csharp
var model = new UserOptions();
bool? result = await IocMessage.Form.ShowEdit(model, dialog =>
{
    dialog.Title = "编辑用户";
    dialog.MinWidth = 600;
});

if (result == true)
{
    // 用户点击确定
}
```

### 3.2 查看对象

```csharp
await IocMessage.Form.ShowView(model, dialog =>
{
    dialog.Title = "查看详情";
});
```

### 3.3 Tab 表单编辑

```csharp
await IocMessage.Form.ShowTabEdit(model, dialog =>
{
    dialog.Title = "高级设置";
}, option =>
{
    option.UseTabAttribute = true;
    option.TabStripPlacement = Dock.Left;
});
```

---

## 4. `Form` 属性筛选和显示选项

`Form` 同时实现 `IFormOption`，常见配置如下。

| 属性 | 默认值 | 说明 |
|---|---:|---|
| `SelectObject` | `null` | 当前要编辑/查看的对象。 |
| `UseDisplayOnly` | `true` | 只显示带 `[Display]` 的属性。 |
| `UseDeclaredOnly` | `false` | 只显示当前类型声明的属性，不包含基类属性。 |
| `UsePropertyNames` | `null` | 只显示指定属性，多个名称用空格或逗号分隔。 |
| `ExceptPropertyNames` | `null` | 排除指定属性，多个名称用空格或逗号分隔。 |
| `UseGroup` | `false` | 是否按 `GroupName` 分组显示。 |
| `UseGroupNames` | `null` | 只显示指定分组。 |
| `UseTabNames` | `null` | 只显示指定 Tab。 |
| `UseOrder` | `true` | 按 `DisplayAttribute.Order` 排序。 |
| `UseOrderByName` | `false` | 按属性显示名称排序。 |
| `UseOrderByType` | `false` | 按属性项类型排序。 |
| `UseAsync` | `false` | 异步逐项添加属性项。 |
| `UsePropertyView` | `false` | 使用只读视图属性项。 |
| `SearchText` | `null` | 搜索过滤文本。 |
| `TitleWidth` | `NaN` | 属性标题列宽度。 |
| `MessageWidth` | `15` | 属性提示消息宽度。 |

类型过滤选项：

| 属性 | 说明 |
|---|---|
| `UseEnum` | 是否显示枚举属性。 |
| `UseDateTime` | 是否显示 `DateTime` 属性。 |
| `UseClass` | 是否显示类属性，`string` 单独由 `UseString` 控制。 |
| `UseArray` | 是否显示数组属性。 |
| `UseInterface` | 是否显示接口属性。 |
| `UsePrimitive` | 是否显示基元类型。 |
| `UseString` | 是否显示字符串属性。 |
| `UseBoolen` | 是否显示布尔属性。 |
| `UseCommand` | 是否显示 `ICommand` 属性。 |
| `UseCommandOnly` | 是否只显示命令属性。 |
| `UseEnumerator` | 是否显示 `IEnumerable` / `IEnumerator` 属性。 |
| `UseNull` | 是否显示值为 `null` 的属性。 |
| `UseTypeConverter` | 是否启用 `TypeConverter` 属性编辑。 |
| `UseTypeConverterOnly` | 是否只显示带 `TypeConverter` 的属性。 |

---

## 5. 属性项创建规则

默认创建逻辑位于 `PropertyInfoExtention.Create(...)`。

优先级：

1. 如果属性标注 `[PropertyItem(typeof(...))]`，优先创建指定 `PropertyItem`。
2. `ICommand` → `CommandPropertyItem`。
3. `DateTime` → `DateTimePropertyItem`。
4. `bool` → `BoolPropertyItem`。
5. `bool?` → `BoolNullablePropertyItem`。
6. 枚举 → `EnumPropertyItem`。
7. 带 `TypeConverter` 或实现 `IConvertible` → `TextPropertyItem`。
8. 基元类型、`string`、`Nullable<T>` → `TextPropertyItem`。
9. 其他对象 → `ObjectPropertyItem<object>`。

只读视图模式 `UsePropertyView=True` 时：

- 优先使用 `[PropertyViewItem(typeof(...))]`。
- 其次尝试复用 `[PropertyItem(typeof(...))]`。
- 类属性使用 `ObjectPropertyItem<object>`。
- 其他属性使用 `TextPropertyViewItem`。

---

## 6. 内置基础 PropertyItem

### 6.1 `TextPropertyItem`

适用于：

- `string`
- 数值类型
- `Nullable<T>`
- 带 `TypeConverter` 的类型
- 实现 `IConvertible` 的类型

示例：

```csharp
[Display(Name = "名称")]
public string Name { get; set; }

[Display(Name = "数量")]
public int Count { get; set; }
```

### 6.2 `TextPropertyViewItem`

只读文本视图项，通常在 `UsePropertyView=True` 时使用。

### 6.3 `BoolPropertyItem`

用于 `bool` 属性，通常显示为勾选框或开关。

```csharp
[Display(Name = "启用")]
public bool IsEnabled { get; set; }
```

### 6.4 `BoolNullablePropertyItem`

用于 `bool?` 属性，支持三态布尔值。

### 6.5 `DateTimePropertyItem`

用于 `DateTime` 属性。

```csharp
[Display(Name = "创建时间")]
public DateTime CreateTime { get; set; }
```

### 6.6 `EnumPropertyItem`

用于枚举属性。

```csharp
[Display(Name = "方向")]
public HorizontalAlignment HorizontalAlignment { get; set; }
```

### 6.7 `CommandPropertyItem`

用于 `ICommand` 属性，生成命令按钮。

```csharp
[Display(Name = "执行")]
public ICommand RunCommand { get; set; }
```

### 6.8 `ObjectPropertyItem<T>`

用于复杂对象属性，通常以嵌套对象方式展示。

### 6.9 `FormPropertyItem`

将复杂对象以内嵌 `Form` 的方式展示。

```csharp
[PropertyItem(typeof(FormPropertyItem))]
[Display(Name = "详细参数")]
public DetailOptions Detail { get; set; } = new DetailOptions();
```

### 6.10 `ExpanderFormPropertyItem`

将复杂对象放入可展开区域中，适合高级设置。

```csharp
[PropertyItem(typeof(ExpanderFormPropertyItem))]
[Display(Name = "高级参数")]
public AdvancedOptions Advanced { get; set; } = new AdvancedOptions();
```

### 6.11 `PresenterPropertyItem`

用于把复杂对象作为 Presenter 展示。`Form.CreatePropertyItem` 中当类属性标注 `[UsePropertyPresenter]` 时，会创建 `PresenterPropertyItem`。

```csharp
[UsePropertyPresenter]
[Display(Name = "预览")]
public object Presenter { get; set; }
```

---

## 7. `H.Controls.Form.PropertyItem` 扩展 PropertyItem

### 7.1 文本命令类

| PropertyItem | 作用 |
|---|---|
| `PasswordTextPropertyItem` | 密码输入，隐藏明文。 |
| `OpenFileDialogPropertyItem` | 文本框 + 文件选择按钮。 |
| `OpenSystemPathTextPropertyItem` | 文本框 + 打开系统路径按钮。 |
| `DeleteSystemPathTextPropertyItem` | 文本框 + 删除系统路径按钮。 |
| `OpenDeleteSystemPathTextPropertyItem` | 同时支持打开和删除路径。 |
| `DeleteTextPropertyItem` | 带清空/删除文本能力。 |
| `HyperlinkPropertyItem` | 将字符串作为超链接展示或打开。 |
| `SliderTextPropertyItem` | 滑块 + 文本数值编辑。 |
| `UnitTextPropertyItem` | 文本编辑并显示单位。 |
| `ComboBoxTextPropertyItemItem` | 文本与下拉选择结合。 |

示例：

```csharp
[PropertyItem(typeof(OpenFileDialogPropertyItem))]
[Display(Name = "文件路径")]
public string FilePath { get; set; }

[PropertyItem(typeof(PasswordTextPropertyItem))]
[Display(Name = "密码")]
public string Password { get; set; }

[Unit("ms")]
[PropertyItem(typeof(UnitTextPropertyItem))]
[Display(Name = "延迟")]
public int Delay { get; set; }
```

### 7.2 下拉选择类

| PropertyItem | 作用 |
|---|---|
| `ComboBoxPropertyItem` | 普通下拉选择。 |
| `EnumComboBoxPropertyItem` | 枚举下拉选择。 |
| `PresenterComboBoxPropertyItem` | 下拉项以 Presenter 方式展示。 |
| `FormComboBoxPropertyItem` | 下拉项以 Form 方式展示。 |
| `GroupStyleComboBoxPropertyItem` | 带分组样式的下拉。 |
| `SelectedIndexComboBoxPropertyItem` | 使用选中索引的下拉。 |

示例：

```csharp
public string[] SelectSource { get; set; } = { "A", "B", "C" };

[GetPropertyNameSource(nameof(SelectSource))]
[PropertyItem(typeof(ComboBoxPropertyItem))]
[Display(Name = "选项")]
public string SelectedItem { get; set; }
```

### 7.3 ListBox 和多选类

| PropertyItem | 作用 |
|---|---|
| `ListBoxPropertyItem` | 单选列表。 |
| `MultiListBoxPropertyItem` | 多选列表。 |

示例：

```csharp
public string[] SelectSource { get; set; } = { "A", "B", "C" };

[GetPropertyNameSource(nameof(SelectSource))]
[PropertyItem(typeof(ListBoxPropertyItem))]
[Display(Name = "单选")]
public string SelectedItem { get; set; }

[GetPropertyNameSource(nameof(SelectSource))]
[PropertyItem(typeof(MultiListBoxPropertyItem))]
[Display(Name = "多选")]
public IList<string> SelectedItems { get; set; }
```

### 7.4 集合类

| PropertyItem | 作用 |
|---|---|
| `EnumerablePropertyItem` | 展示或编辑 `IEnumerable` 集合。 |
| `ObservableCollectionPropertyItem` | 编辑 `ObservableCollection<T>`。 |
| `PrimitiveListPropertyItem` | 编辑基础类型列表。 |
| `PrimitiveArrayPropertyItem` | 编辑基础类型数组。 |
| `PrimitivesPropertyItemBase` | 基础类型集合编辑基类。 |

示例：

```csharp
[PropertyItem(typeof(PrimitiveListPropertyItem))]
[Display(Name = "阈值列表")]
public List<double> Thresholds { get; set; } = new List<double>();
```

### 7.5 颜色和画刷类

| PropertyItem | 作用 |
|---|---|
| `ColorPropertyItem` | 编辑 `Color`。 |
| `BrushPropertyItem` | 编辑 `Brush`。 |

示例：

```csharp
[PropertyItem(typeof(ColorPropertyItem))]
[Display(Name = "颜色")]
public Color Color { get; set; }

[PropertyItem(typeof(BrushPropertyItem))]
[Display(Name = "画刷")]
public Brush Brush { get; set; }
```

### 7.6 仓储/数据源类

| PropertyItem | 作用 |
|---|---|
| `ComboboxRepositoryPropertyItem` | 从仓储或数据源中选择对象。 |

实际使用时通常需要结合项目中的仓储服务或数据源特性。

---

## 8. Form 基础特性配置

### 8.1 `[Display]`

控制名称、说明、分组和排序。

```csharp
[Display(Name = "服务地址", Description = "接口服务地址", GroupName = "网络", Order = 1)]
public string ServiceUrl { get; set; }
```

`Form` 中常用字段：

- `Name`：属性标题。
- `Description`：提示说明。
- `GroupName`：属性分组。
- `Order`：排序。

### 8.2 `[Browsable(false)]`

隐藏属性。

```csharp
[Browsable(false)]
public string RuntimeValue { get; set; }
```

`Form.RefreshObjectinternal` 会跳过 `Browsable=false` 的属性。

### 8.3 `[ReadOnly(true)]`

让属性只读展示。

```csharp
[ReadOnly(true)]
[Display(Name = "产品名称")]
public string ProductName { get; set; }
```

### 8.4 `[DefaultValue]`

声明默认值，常用于设置系统恢复默认，也能作为文档化配置。

```csharp
[DefaultValue(30)]
[Display(Name = "超时时间")]
public int TimeoutSeconds { get; set; } = 30;
```

### 8.5 校验特性

常用标准校验：

```csharp
[Required]
[Display(Name = "用户名")]
public string UserName { get; set; }

[Range(1, 300)]
[Display(Name = "超时时间")]
public int TimeoutSeconds { get; set; }
```

---

## 9. Form 专用特性

### 9.1 `[PropertyItem]`

指定编辑器类型。

```csharp
[PropertyItem(typeof(PasswordTextPropertyItem))]
[Display(Name = "密码")]
public string Password { get; set; }
```

要求：指定类型必须实现 `IPropertyItem`，并提供 `(PropertyInfo, object)` 构造函数。

### 9.2 `[PropertyViewItem]`

指定只读视图模式下的展示项。

```csharp
[PropertyViewItem(typeof(TextPropertyViewItem))]
[Display(Name = "只读文本")]
public string Text { get; set; }
```

当 `Form.UsePropertyView=True` 时优先使用。

### 9.3 `[PropertyStyle]`

控制属性项样式，目前常用 `UseTitle`。

```csharp
[PropertyStyle(UseTitle = false)]
[PropertyItem(typeof(ExpanderFormPropertyItem))]
[Display(Name = "高级配置")]
public AdvancedOptions Advanced { get; set; }
```

### 9.4 `[PropertyTextBoxStyle]`

控制文本框样式。

```csharp
[PropertyTextBoxStyle(TextWrapping = TextWrapping.Wrap, UseClear = true)]
[Display(Name = "备注")]
public string Remark { get; set; }
```

### 9.5 `[Unit]`

指定单位文本。

```csharp
[Unit("秒")]
[PropertyItem(typeof(UnitTextPropertyItem))]
[Display(Name = "超时时间")]
public int TimeoutSeconds { get; set; }
```

### 9.6 `[Tab]`

声明属性所属 Tab。

```csharp
[Tab("高级")]
[Display(Name = "缓存大小")]
public int CacheSize { get; set; }
```

配合 `ShowTabEdit` 或 `ITabFormOption.UseTabAttribute` 使用。

### 9.7 `[UsePropertyPresenter]`

复杂对象属性使用 Presenter 展示。

```csharp
[UsePropertyPresenter]
[Display(Name = "预览")]
public object PreviewPresenter { get; set; }
```

### 9.8 `[TextValueConverter]`

指定文本值转换器。

```csharp
[TextValueConverter(typeof(MyValueConverter))]
[Display(Name = "显示值")]
public double Value { get; set; }
```

适合需要自定义文本显示和回写逻辑的属性。

---

## 10. 可见性、刷新和通知特性

### 10.1 `[BindingVisibleablePropertyName]`

根据另一个属性值控制当前属性可见性。

```csharp
[Display(Name = "启用高级")]
public bool UseAdvanced { get; set; }

[BindingVisibleablePropertyName(nameof(UseAdvanced))]
[Display(Name = "高级参数")]
public string AdvancedValue { get; set; }
```

当 `UseAdvanced` 为 `false` 时，当前属性项可被过滤隐藏。

### 10.2 `[BindingVisiblableMethodName]`

通过方法返回值控制可见性。

```csharp
[BindingVisiblableMethodName(nameof(CanShowAdvanced))]
[Display(Name = "高级参数")]
public string AdvancedValue { get; set; }

public bool CanShowAdvanced()
{
    return UseAdvanced;
}
```

### 10.3 `[RefreshFilterOnValueChanged]`

属性值变化后刷新表单过滤，可用于可见性联动。

```csharp
[RefreshFilterOnValueChanged]
[Display(Name = "启用高级")]
public bool UseAdvanced { get; set; }
```

### 10.4 `[RefreshSourceOnValueChanged]`

属性值变化后刷新指定属性的数据源。

```csharp
[RefreshSourceOnValueChanged(nameof(City))]
[Display(Name = "省份")]
public string Province { get; set; }

[PropertyItem(typeof(ComboBoxPropertyItem))]
[Display(Name = "城市")]
public string City { get; set; }
```

### 10.5 `[NotifyMethodName]`

属性值变化后调用指定方法。

```csharp
[NotifyMethodName(nameof(OnServiceUrlChanged))]
[Display(Name = "服务地址")]
public string ServiceUrl { get; set; }

public void OnServiceUrlChanged()
{
    // 刷新数据源或校验状态
}
```

### 10.6 `[Binding]`

声明属性绑定路径，用于属性项之间的数据关联。

```csharp
[Binding(nameof(SourceProperty))]
[Display(Name = "绑定属性")]
public string TargetProperty { get; set; }
```

---

## 11. 数据源特性

数据源特性位于 `H.Controls.Form.PropertyItem.Attribute`，常配合下拉、列表、多选等 `PropertyItem` 使用。

### 11.1 `[GetPropertyNameSource]`

从当前对象的某个属性获取数据源。

```csharp
public string[] SelectSource { get; set; } = { "Item1", "Item2" };

[GetPropertyNameSource(nameof(SelectSource))]
[PropertyItem(typeof(ComboBoxPropertyItem))]
[Display(Name = "选择项")]
public string SelectItem { get; set; }
```

### 11.2 `[GetMethodNameSource]`

从当前对象的方法获取数据源。

```csharp
public IEnumerable<DemoModelItem> GetItems()
{
    return Items;
}

[GetMethodNameSource(nameof(GetItems))]
[PropertyItem(typeof(PresenterComboBoxPropertyItem))]
[Display(Name = "对象选择")]
public DemoModelItem SelectedItem { get; set; }
```

### 11.3 `[GetEnumSource]`

从枚举类型生成数据源。

```csharp
[GetEnumSource]
[PropertyItem(typeof(ListBoxPropertyItem))]
[Display(Name = "水平对齐")]
public HorizontalAlignment HorizontalAlignment { get; set; }
```

### 11.4 `[GetFilesSource]`

从文件夹读取文件列表作为数据源。

```csharp
[GetFilesSource("Assets", SearchPattern = "*.png", UseFullPath = true)]
[PropertyItem(typeof(ComboBoxPropertyItem))]
[Display(Name = "图片文件")]
public string SelectedFilePath { get; set; }
```

### 11.5 `[DisplayMemberPath]`

指定对象数据源的显示属性。

```csharp
[DisplayMemberPath("Name")]
[GetMethodNameSource(nameof(GetItems))]
[PropertyItem(typeof(PresenterComboBoxPropertyItem))]
[Display(Name = "对象选择")]
public DemoModelItem SelectedItem { get; set; }
```

---

## 12. `ValueChanged` 事件

`Form` 提供冒泡事件 `ValueChanged`，当属性项值变化时触发。

```xaml
<h:Form
    SelectObject="{Binding Options}"
    ValueChanged="OnFormValueChanged" />
```

```csharp
private void OnFormValueChanged(object sender, RoutedEventArgs e)
{
    if (e.Source is Tuple<IPropertyItem, object> tuple)
    {
        IPropertyItem item = tuple.Item1;
        object value = tuple.Item2;
    }
}
```

内部会在值变化后自动处理：

- `IRefreshFilterOnValueChanged`：刷新过滤。
- `IRefreshSourceOnValueChanged`：刷新关联数据源。
- 重新触发 `ValueChanged` 事件。

---

## 13. 自定义 PropertyItem

自定义属性项需要实现 `IPropertyItem`，通常继承现有基类更简单。

基本要求：

- 实现或继承 `IPropertyItem`。
- 提供构造函数 `(PropertyInfo propertyInfo, object obj)`。
- 需要值变化通知时实现 `IValueChangeable`。

示例：

```csharp
public class MyTextPropertyItem : TextPropertyItem
{
    public MyTextPropertyItem(PropertyInfo propertyInfo, object obj)
        : base(propertyInfo, obj)
    {
    }
}
```

使用：

```csharp
[PropertyItem(typeof(MyTextPropertyItem))]
[Display(Name = "自定义文本")]
public string Text { get; set; }
```

建议：

- 优先继承现有 `TextPropertyItem`、`ObjectPropertyItemBase`、`ItemsSourcePropertyItem`。
- 数据源型属性项实现数据源刷新能力。
- 修改属性值后触发 `ValueChanged`，确保 Form 能联动刷新。

---

## 14. 完整示例

```csharp
public class DeviceOptions
{
    public string[] Ports { get; set; } = { "COM1", "COM2", "COM3" };

    [Display(Name = "设备名称", GroupName = "基础", Order = 1)]
    public string Name { get; set; }

    [RefreshFilterOnValueChanged]
    [Display(Name = "启用高级", GroupName = "基础", Order = 2)]
    public bool UseAdvanced { get; set; }

    [GetPropertyNameSource(nameof(Ports))]
    [PropertyItem(typeof(ComboBoxPropertyItem))]
    [Display(Name = "串口", GroupName = "连接", Order = 3)]
    public string Port { get; set; }

    [Unit("ms")]
    [PropertyItem(typeof(UnitTextPropertyItem))]
    [Display(Name = "超时时间", GroupName = "连接", Order = 4)]
    public int Timeout { get; set; } = 1000;

    [BindingVisibleablePropertyName(nameof(UseAdvanced))]
    [PropertyItem(typeof(PasswordTextPropertyItem))]
    [Display(Name = "高级密钥", GroupName = "高级", Order = 5)]
    public string Secret { get; set; }

    [Tab("对象")]
    [PropertyItem(typeof(ExpanderFormPropertyItem))]
    [Display(Name = "详细配置", GroupName = "高级", Order = 6)]
    public DetailOptions Detail { get; set; } = new DetailOptions();
}

public class DetailOptions
{
    [Display(Name = "重试次数")]
    public int RetryCount { get; set; } = 3;
}
```

XAML：

```xaml
<h:Form
    SelectObject="{Binding DeviceOptions}"
    UseGroup="True"
    TitleWidth="120"
    UseDisplayOnly="True" />
```

弹窗编辑：

```csharp
await IocMessage.Form.ShowEdit(DeviceOptions, dialog =>
{
    dialog.Title = "设备配置";
    dialog.MinWidth = 700;
});
```

---

## 15. 二次开发建议

- 可编辑属性建议都加 `[Display]`，因为 `UseDisplayOnly` 默认是 `true`。
- 用 `Display.GroupName` 组织分组，用 `Display.Order` 控制排序。
- 不希望显示的属性使用 `[Browsable(false)]`。
- 特殊编辑器使用 `[PropertyItem(typeof(...))]` 显式指定。
- 下拉和列表类属性项配合 `GetPropertyNameSource` 或 `GetMethodNameSource`。
- 复杂对象优先用 `FormPropertyItem` 或 `ExpanderFormPropertyItem`。
- 密码、路径、颜色、集合等场景优先复用 `H.Controls.Form.PropertyItem` 中的现有属性项。
- 需要联动显示时配合 `BindingVisibleablePropertyName` 和 `RefreshFilterOnValueChanged`。
- 需要刷新下拉数据源时使用 `RefreshSourceOnValueChanged`。
- 大对象属性较多时可启用 `UseAsync=True` 优化加载体验。

---

## 16. 常见问题

### 属性没有显示

检查：

1. 属性是否为实例属性。
2. 属性是否可读。
3. 是否加了 `[Browsable(false)]`。
4. `UseDisplayOnly=True` 时是否加了 `[Display]`。
5. 是否被 `UsePropertyNames` / `ExceptPropertyNames` / `UseGroupNames` / `UseTabNames` 过滤。
6. 类型过滤开关是否关闭了该类型，例如 `UseString=False`。

### 下拉没有数据

检查：

1. 是否配置了 `GetPropertyNameSource` 或 `GetMethodNameSource`。
2. 数据源属性或方法是否返回 `IEnumerable`。
3. 数据源属性名或方法名是否拼写正确。
4. 是否需要 `RefreshSourceOnValueChanged` 刷新数据源。

### 自定义 PropertyItem 没有生效

检查：

1. 是否标注 `[PropertyItem(typeof(MyPropertyItem))]`。
2. 类型是否实现 `IPropertyItem`。
3. 是否有 `(PropertyInfo, object)` 构造函数。
4. 对应程序集是否被项目引用。

### 复杂对象显示太占空间

使用：

```csharp
[PropertyItem(typeof(ExpanderFormPropertyItem))]
public AdvancedOptions Advanced { get; set; }
```

或通过 `[PropertyStyle(UseTitle = false)]` 调整标题显示。

### 只想查看不想编辑

设置：

```xaml
<h:Form SelectObject="{Binding Model}" UsePropertyView="True" />
```

或使用：

```csharp
await IocMessage.Form.ShowView(model);
```
