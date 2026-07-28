# H.Extensions.Behvaiors 开发文档

**适用项目：** `H.Extensions.Behvaiors`  
**核心依赖：** `Microsoft.Xaml.Behaviors.Wpf`  
**相关能力：** Behavior、Trigger、Action、附加属性、拖放、TreeView 选择、DataGrid、PasswordBox、圆角裁剪、Adorner、文本与滚动行为

本文介绍 `H.Extensions.Behvaiors` 的结构、XAML 使用方式、常见行为和二次开发约定。

> 程序集和物理目录的实际拼写是 `H.Extensions.Behvaiors`，其中 `Behvaiors` 为历史拼写。引用程序集、项目路径和 CLR 命名空间时必须使用该实际名称；文档中“Behavior”泛指 WPF 行为模式。

---

## 1. 模块定位

`H.Extensions.Behvaiors` 基于 `Microsoft.Xaml.Behaviors.Wpf`，把原本需要写在 View 代码后置中的交互逻辑封装为可声明、可复用的 XAML 行为。

```text
UIElement
    ↓
Interaction.Behaviors / Interaction.Triggers
    ↓
Behavior / Trigger / Action
    ↓
处理 UI 事件、状态同步或视觉交互
```

适用场景：

- 将控件事件转换为命令或方法调用。
- 绑定 `PasswordBox.Password`。
- 同步 TreeView、ListBox 或 DataGrid 的选择和状态。
- 在 TextBox 获得焦点时全选内容。
- 对元素实现拖放、圆角裁剪、鼠标悬停或 Adorner 效果。
- 在不创建派生控件的前提下添加局部交互。

不适合：

- 领域业务规则、数据库访问、网络请求和权限判断。
- 需要跨多个页面维护复杂状态的流程。
- 用行为替换所有 ViewModel 命令。

业务逻辑应位于 Service 或 ViewModel；Behavior 只负责 UI 事件、状态和视觉层连接。

---

## 2. 项目结构

```text
H.Extensions.Behvaiors/
├── Adorners/          Adorner 和命中测试相关行为
├── ContextMenus/      ContextMenu 显示和切换行为
├── DataGrids/         自动列与拖放行为
├── FrameworkElements/ 元素拖放、拖拽状态、圆角裁剪
├── ItemsControls/     集合增删、过滤、搜索、选择绑定
├── PasswordBoxs/      PasswordBox 绑定
├── ScrollViewers/     滚动行为
├── TextBlocks/        文本计数、索引行为
├── TextBoxs/          焦点全选、双击编辑、键盘更新源
├── TreeViews/         选择项和多选绑定
├── Triggers/          鼠标触发器和 Action
├── BorderClipCornerRadiusBehavior.cs
└── Cattach.Behaviors.cs
```

主要类型按功能分类：

| 分类 | 示例 |
|---|---|
| Trigger / Action | `MouseTrigger`、`MousePressTrigger`、`MousePressClickTrigger`、`CallMethodActionEx`。 |
| 密码输入 | `PasswordBindingBehavior`。 |
| 文本框 | `TextBoxSelectAllOnFocusBebavior`、`TextBoxEditOnDoubleClickBebavior`、`TextBoxUpdateSourceOnKeyDownBebavior`。 |
| TreeView | `TreeViewSelectedItemBindableBehavior`、`TreeViewSelectedItemsBindableBehavior`、`TreeViewItemSetSelectedOnMouseDownBehavior`。 |
| ItemsControl | Add/Insert/Remove/Clear Item 按钮行为、搜索和过滤行为、`Behavior.ListBox.BindingSelectedItems`。 |
| DataGrid | `DataGridAutoColumnBehavior`、`DataGridDropBehavior`、`DataGridColumnAttribute`。 |
| 拖放 | `FrameworkElementDragDropBehavior`、`ElementDragStateBehavior`。 |
| 视觉效果 | `FrameworkElementClipCornerRadiusBehavior`、`BorderClipCornerRadiusBehavior`、Adorner 行为。 |
| 菜单与滚动 | `ContextMenuDisplayBehavior`、`MouseOverContextMenuBehavior`、`ToggleButtonContextMenuBehavior`、`ScrollViewerBebavior`。 |

部分类型名称中的 `Bebavior`、`Behvaiors` 是现有公开 API 的历史拼写；引用时必须保持一致。

---

## 3. 引用和 XAML 命名空间

项目引用：

```xml
<ItemGroup>
  <ProjectReference Include="..\..\Extensions\H.Extensions.Behvaiors\H.Extensions.Behvaiors.csproj" />
</ItemGroup>
```

XAML 命名空间：

```xaml
xmlns:b="http://schemas.microsoft.com/xaml/behaviors"
xmlns:h="https://github.com/HeBianGu"
```

- `b`：`Microsoft.Xaml.Behaviors` 的 `Interaction.Behaviors` 和 `Interaction.Triggers`。
- `h`：框架公开 XAML 类型、标记扩展和行为类型。

基本结构：

```xaml
<TextBox>
    <b:Interaction.Behaviors>
        <!-- Behavior 实例 -->
    </b:Interaction.Behaviors>

    <b:Interaction.Triggers>
        <!-- Trigger 和 Action -->
    </b:Interaction.Triggers>
</TextBox>
```

---

## 4. Behavior、Trigger 和 Action 的区别

| 类型 | 基类模式 | 用途 | 生命周期 |
|---|---|---|---|
| `Behavior<T>` | 关联一个目标元素 | 订阅控件事件、同步依赖属性、维护 UI 状态。 | `OnAttached()` / `OnDetaching()`。 |
| `TriggerBase<T>` | 监听事件或条件 | 在满足交互条件时触发一组 Action。 | 附加后注册事件。 |
| `TriggerAction<T>` | 被 Trigger 调用 | 执行单次行为，例如调用方法。 | `Invoke(parameter)`。 |
| 附加属性 | `DependencyProperty` | 通过属性变更附加较轻量的行为。 | 属性变更回调。 |

选择建议：

```text
需要持续订阅与清理事件      → Behavior<T>
需要“事件发生后执行动作”    → Trigger + Action
只需要单一属性开关          → 附加属性
需要复杂/可复用控件视觉树   → 自定义控件或 UserControl
```

---

## 5. 生命周期与内存安全

正确的 `Behavior<T>` 必须成对订阅和解除订阅：

```csharp
public class SelectAllOnFocusBehavior : Behavior<TextBox>
{
    protected override void OnAttached()
    {
        base.OnAttached();
        this.AssociatedObject.GotKeyboardFocus += OnGotKeyboardFocus;
    }

    protected override void OnDetaching()
    {
        this.AssociatedObject.GotKeyboardFocus -= OnGotKeyboardFocus;
        base.OnDetaching();
    }

    private void OnGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
    {
        this.AssociatedObject.SelectAll();
    }
}
```

规则：

- 在 `OnAttached()` 订阅 `AssociatedObject` 事件。
- 在 `OnDetaching()` 解除所有订阅。
- 不要让静态事件、计时器、全局消息总线长期持有 View 引用。
- 行为中的异步回调在元素卸载后不得继续更新 UI。
- 必要时检查 `AssociatedObject.IsLoaded`、`IsVisible` 或取消令牌。

---

## 6. 调用方法 `CallMethodActionEx`

`CallMethodActionEx` 用于从 XAML Trigger 调用目标对象方法。

常见用法：

```xaml
xmlns:b="http://schemas.microsoft.com/xaml/behaviors"
xmlns:h="https://github.com/HeBianGu"

<h:Zoombox x:Name="Zoom">
    <b:Interaction.Triggers>
        <b:EventTrigger EventName="Loaded">
            <h:CallMethodActionEx
                MethodName="FitToBounds"
                TargetObject="{Binding ElementName=Zoom}" />
        </b:EventTrigger>
    </b:Interaction.Triggers>
</h:Zoombox>
```

适用：

- 调用无参数 UI 方法。
- 将 `Loaded`、`SizeChanged`、`TargetUpdated` 等 View 事件连接至控件方法。
- 复用第三方控件公开方法。

限制：

- 方法名通过反射解析，重构时需要额外验证。
- 不要用它调用业务 Service、持久化操作或安全敏感操作。
- 目标方法应快速完成，避免阻塞 UI 线程。

ZoomBox 的自动适应场景见 [`development-guide-zoombox.md`](development-guide-zoombox.md)。

---

## 7. 鼠标触发器

相关类型：

```text
MouseTrigger
MousePressTrigger
MousePressClickTrigger
```

`MouseTrigger` 支持按鼠标按下或释放触发，并可限制：

| 属性 | 作用 |
|---|---|
| `MouseButton` | 指定左键、右键、中键或扩展键。 |
| `ClickCount` | 指定单击、双击等点击次数。 |
| `FiredOn` | 在鼠标按下或释放阶段触发。 |
| `Handled` | 触发后设置事件已处理状态。 |
| `UseHandle` | 是否在处理已处理事件时仍接收事件。 |

双击调用方法：

```xaml
<b:Interaction.Triggers>
    <h:MouseTrigger
        ClickCount="2"
        MouseButton="Left"
        UseHandle="False">
        <h:CallMethodActionEx
            MethodName="Open"
            TargetObject="{Binding}" />
    </h:MouseTrigger>
</b:Interaction.Triggers>
```

对于命令优先的 ViewModel，可把 Trigger 与 InvokeCommandAction 或项目已有命令 Action 组合。不要用 MouseTrigger 替代控件本身已提供的 `Command` 属性。

---

## 8. PasswordBox 双向绑定

WPF `PasswordBox.Password` 不是依赖属性。`PasswordBindingBehavior` 提供行为绑定：

```xaml
<PasswordBox>
    <b:Interaction.Behaviors>
        <h:PasswordBindingBehavior
            Password="{Binding Password, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}" />
    </b:Interaction.Behaviors>
</PasswordBox>
```

行为逻辑：

1. `OnAttached()` 将 Behavior 的 `Password` 写入 `PasswordBox.Password`。
2. 订阅 `PasswordChanged`。
3. 密码改变后，将 `Password` 更新回绑定源。
4. `OnDetaching()` 解除事件订阅。

安全注意事项：

- 绑定源是 `string`，不能在内存中安全清零。
- 不要将密码写入日志、Options、配置文件或异常消息。
- 不要把密码长期保存在共享 ViewModel。
- 认证、哈希和授权必须由可信身份 Service 处理。

登录注册场景见 [`development-guide-login.md`](development-guide-login.md) 与 [`development-guide-identity.md`](development-guide-identity.md)。

---

## 9. TextBox 行为

常用类型：

| 类型 | 用途 |
|---|---|
| `TextBoxSelectAllOnFocusBebavior` | TextBox 获得焦点后全选文本。 |
| `TextBoxEditOnDoubleClickBebavior` | 双击时切换或进入编辑状态。 |
| `TextBoxUpdateSourceOnKeyDownBebavior` | 在指定键盘事件时更新绑定源。 |

全选示例：

```xaml
<TextBox Text="{Binding Name}">
    <b:Interaction.Behaviors>
        <h:TextBoxSelectAllOnFocusBebavior />
    </b:Interaction.Behaviors>
</TextBox>
```

使用这些行为的目的在于统一输入体验。对于复杂校验应使用绑定验证、ViewModel 或 Service，不要把业务验证塞入按键事件。

---

## 10. TreeView 选择绑定

WPF TreeView 的 `SelectedItem` 不是可直接双向绑定的依赖属性。模块提供：

```text
TreeViewSelectedItemBindableBehavior
TreeViewSelectedItemsBindableBehavior
TreeViewItemContainSelectItemBehavior
TreeViewItemSetSelectedOnMouseDownBehavior
TreeViewSelectNoneOnMouseDownBehavior
```

单选绑定示意：

```xaml
<TreeView ItemsSource="{Binding Nodes}">
    <b:Interaction.Behaviors>
        <h:TreeViewSelectedItemBindableBehavior
            SelectedItem="{Binding SelectedNode, Mode=TwoWay}" />
    </b:Interaction.Behaviors>
</TreeView>
```

多选场景使用 `TreeViewSelectedItemsBindableBehavior`，绑定源应为可变集合，并在 ViewModel 中处理集合变更。

注意：

- TreeView 存在虚拟化和容器延迟生成时，选中项同步可能晚于数据绑定。
- 节点替换或异步加载后应验证选中项是否仍在树中。
- 不要同时使用多个会竞争设置选中状态的行为。

---

## 11. ItemsControl、ListBox 与集合操作

模块为 ItemsControl 提供集合操作和筛选行为：

```text
Behavior.Button.AddItem
Behavior.Button.InsertItem
Behavior.Button.RemoveItem
Behavior.Button.RemoveCheckedItem
Behavior.Button.ClearItem
Behavior.ItemsControl.Filter
Behavior.ItemsControl.SearchText
Behavior.ListBox.BindingSelectedItems
ListBoxBringIntoSelectedItemBehavior
```

适用场景：

- 编辑 `ObservableCollection<T>`。
- 根据搜索文本过滤项目。
- 同步 ListBox 多选项到 ViewModel。
- 选中项变化后自动滚动到可见区域。

原则：

- 集合必须由拥有它的 ViewModel 或 Presenter 管理。
- 行为可以触发增删请求，但不能绕过业务规则、权限或确认流程。
- 大集合使用 `CollectionView`、虚拟化和节流，避免每次输入全量遍历。
- 删除项时要处理当前选中项、空集合和撤销需求。

---

## 12. DataGrid 行为

相关类型：

```text
DataGridAutoColumnBehavior
DataGridDropBehavior
DataGridColumnAttribute
IDataGridColumn
```

### 12.1 自动列

`DataGridAutoColumnBehavior` 用于通过类型元数据和 `DataGridColumnAttribute` 调整自动生成列。

建议：

- 对展示模型添加明确显示名、顺序和可见性元数据。
- 避免把领域实体所有属性直接暴露给 DataGrid。
- 对敏感属性、导航属性和大对象显式隐藏。
- 需要复杂编辑器时定义 `DataTemplateColumn` 或专属 ViewModel。

### 12.2 拖放

`DataGridDropBehavior` 处理表格拖放。使用前需要明确：

- 拖动数据类型。
- 可接受目标。
- 复制还是移动。
- 拖放完成后的集合更新与持久化。
- 无权限、重复项和异常的反馈。

拖放行为只处理 UI 层操作；最终业务更新必须由 Service 验证。

---

## 13. FrameworkElement 拖放与状态

相关类型：

```text
FrameworkElementDragDropBehavior
ElementDragStateBehavior
FrameworkElementClipCornerRadiusBehavior
BorderClipCornerRadiusBehavior
```

### 13.1 拖放

`FrameworkElementDragDropBehavior` 用于向普通元素附加拖放交互。适合卡片、设计器节点、文件入口和自定义面板。

不要在拖放回调中执行长时间 I/O；应把文件读取、网络上传和数据库更新交给异步 Service，并在 UI 上显示等待或进度。

### 13.2 圆角裁剪

WPF `Border.CornerRadius` 不会自动裁剪子内容。圆角裁剪行为适合：

```xaml
<Border CornerRadius="12">
    <b:Interaction.Behaviors>
        <h:BorderClipCornerRadiusBehavior />
    </b:Interaction.Behaviors>
    <Image Source="{Binding CoverImage}" Stretch="UniformToFill" />
</Border>
```

需要在尺寸变化时更新裁剪区域。对大量高频变化元素使用裁剪行为会增加布局和渲染成本。

---

## 14. ContextMenu 与滚动

ContextMenu 行为：

```text
ContextMenuDisplayBehavior
MouseOverContextMenuBehavior
ToggleButtonContextMenuBehavior
```

适合：

- 鼠标悬停展示菜单。
- ToggleButton 控制菜单显示状态。
- 根据元素状态显示上下文操作。

注意：`ContextMenu` 与其 PlacementTarget 的 DataContext、焦点和关闭时机不同于普通视觉树。行为中应避免保存已卸载控件的引用。

滚动行为：

```text
ScrollViewerBebavior
```

用于控制滚动定位、自动滚动或内容变更后的可见性。聊天、日志和虚拟化列表使用自动滚动时，应只在用户已位于底部时继续自动滚动，避免打断阅读。

---

## 15. 文本与 Adorner 行为

文本相关：

```text
TextBlockCountBehavior
TextBlockIndexOfBebavior
```

这些行为用于显示计数、索引或基于文本状态的视觉辅助信息。

Adorner 相关：

```text
Behavior.Adorner.Base
Behavior.Adorner
Behavior.Adorner.Loaded
Behavior.Adorner.MouseOver
Behavior.Adorner.HitTest
Behavior.Adorner.HitTest.DragOver
Behavior.Adorner.HitTest.MouseOver
Behavior.Adorner.HitTest.Selected
```

Adorner 行为适合实现：

- 鼠标悬停高亮。
- 选中边框。
- 拖入反馈。
- 设计器/编辑器命中测试。

Adorner 依赖 `AdornerLayer`。需要确保目标元素已加载并处于包含 AdornerLayer 的视觉树内；窗口关闭或元素卸载时必须移除 Adorner。

---

## 16. `Cattach.Behaviors`

除了 `Microsoft.Xaml.Behaviors` 的实例行为，项目还包含 `Cattach.Behaviors.cs` 中的附加行为入口。

附加行为适合通过单个属性开关启用：

```xaml
<Control h:Cattach.SomeBehavior="True" />
```

设计附加行为时：

1. 使用依赖属性注册开关或配置值。
2. 在属性变化回调中附加/解除事件。
3. 保存每个目标元素的状态，避免静态共享可变状态。
4. 关闭时清理事件、Adorner、Timer 和拖放状态。
5. 不要在回调中直接执行领域操作。

当行为需要多个可绑定配置属性或明确生命周期时，优先使用 `Behavior<T>`。

---

## 17. 自定义 Behavior 示例

以下示例在元素加载后调用 ViewModel 命令。它仅负责 UI 生命周期到命令的桥接：

```csharp
public class LoadedCommandBehavior : Behavior<FrameworkElement>
{
    public static readonly DependencyProperty CommandProperty =
        DependencyProperty.Register(
            nameof(Command),
            typeof(ICommand),
            typeof(LoadedCommandBehavior));

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    protected override void OnAttached()
    {
        base.OnAttached();
        AssociatedObject.Loaded += OnLoaded;
    }

    protected override void OnDetaching()
    {
        AssociatedObject.Loaded -= OnLoaded;
        base.OnDetaching();
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        if (Command?.CanExecute(null) == true)
            Command.Execute(null);
    }
}
```

XAML：

```xaml
<Grid>
    <b:Interaction.Behaviors>
        <local:LoadedCommandBehavior Command="{Binding LoadedCommand}" />
    </b:Interaction.Behaviors>
</Grid>
```

如果框架已有 `Cattach.LoadedCommand` 或既有行为，应优先复用，不要重复创建相同能力。

---

## 18. 性能与可靠性

- Behavior 附加到大量虚拟化项容器时会显著增加事件订阅和内存开销。
- 高触发率事件（`MouseMove`、`SizeChanged`、`ScrollChanged`）需要节流或最小化工作量。
- 拖放、Adorner 和裁剪应在元素卸载时清理。
- 不要通过行为频繁遍历整个视觉树。
- 异步操作结束前检查 `AssociatedObject` 是否仍可用。
- 对可重复附加的行为避免重复订阅同一事件。
- 对用户输入、文件路径和拖放数据执行格式、权限和异常检查。

---

## 19. 常见问题

### XAML 找不到行为类型

检查项目是否引用 `H.Extensions.Behvaiors`，并使用：

```xaml
xmlns:h="https://github.com/HeBianGu"
xmlns:b="http://schemas.microsoft.com/xaml/behaviors"
```

注意程序集实际拼写为 `Behvaiors`。

### Behavior 没有触发

检查：

1. 是否放在正确元素的 `Interaction.Behaviors` 或 `Interaction.Triggers` 中。
2. 元素是否已加载。
3. 事件是否被其他控件处理。
4. Trigger 的鼠标按钮、点击次数和 `FiredOn` 是否匹配。
5. 是否存在绑定错误或目标方法不存在。

### 界面关闭后仍有事件回调

通常是遗漏 `OnDetaching()` 中的解除订阅。检查静态事件、Dispatcher Timer、全局服务和 Adorner。

### PasswordBox 绑定后密码未更新

确认 `PasswordBindingBehavior` 位于 `Interaction.Behaviors`，并使用 `Mode=TwoWay`。不要同时从多个行为或代码后置写入同一个密码属性。

### TreeView 选择不同步

检查节点容器是否已生成、绑定集合是否可变，以及是否有其他行为重复设置选择状态。

### 圆角 Border 内图片没有被裁剪

`CornerRadius` 仅影响 Border 自身。为 Border 或内容添加圆角裁剪行为，并确认元素已获得有效尺寸。

---

## 20. 二次开发建议

- 先确认现有行为是否已经满足需求，避免重复实现。
- 行为聚焦 View 层交互，不包含领域业务。
- 订阅和解除订阅必须成对出现。
- 对复杂交互优先提供独立 Behavior，而不是堆叠大量附加属性。
- 保持命名空间与现有类型一致；不要修正历史 API 拼写而破坏兼容性。
- 在 `H.Test.XamlBehaviors` 增加或维护可运行示例。
- 大量列表项中使用 Behavior 前评估虚拟化、事件频率和内存开销。
- 主题相关视觉效果使用 `DynamicResource` 引用框架资源键。
