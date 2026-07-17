# MVVM 二次开发文档

**适用项目：** `H.Mvvm`、`H.Extensions.Mvvm`、业务 Presenter、ViewModel、设置项、项目项  
**核心类型：** `BindableBase`、`Bindable`、`CommandsBindableBase`、`DisplayBindableBase`、`RelayCommand`、`DisplayCommand`、`IDisplayCommand`  
**相关能力：** 属性通知、命令绑定、命令可用性、显示元数据、图标、命令自动发现、默认值、序列化忽略

本文介绍框架中 MVVM 基础设施的使用方式。重点包括 `BindableBase`、`Bindable`、`DisplayBindableBase`、`RelayCommand` 和 `DisplayCommand` 的职责、继承关系、XAML 绑定方式及命令自动收集机制。

> 用户提到的 `RealyCommand` 在框架中的实际类型名为 `RelayCommand`。

---

## 1. MVVM 分层

框架中的 MVVM 对象通常分为：

```text
View（XAML）
    ↓ Binding / Command
ViewModel / Presenter（BindableBase、Bindable、DisplayBindableBase）
    ↓ Service / Repository / Project / Ioc
业务数据、配置、持久化模型
```

职责建议：

| 层 | 主要职责 |
|---|---|
| View | 控件、布局、样式、DataTemplate、绑定表达式。 |
| ViewModel / Presenter | UI 状态、属性通知、命令、显示元数据、调用服务。 |
| Service | 业务操作、文件、数据库、网络、项目管理。 |
| Model / DTO | 业务数据和可序列化持久化数据。 |

避免在 ViewModel 中直接操作大量控件对象，例如 `TextBox`、`Window`、`DataGrid`。ViewModel 应暴露可绑定属性和命令，由 XAML 负责显示。

---

## 2. 继承关系

框架核心基类关系：

```text
BindableBase
    ↓
Bindable
    ↓
CommandsBindableBase
    ↓
DisplayBindableBase
    ↓
ResxDisplayBindableBase / GroupDisplayBindableBase / 业务 Presenter
```

| 类型 | 适用场景 |
|---|---|
| `BindableBase` | 只需要 `INotifyPropertyChanged` 的简单对象。 |
| `Bindable` | 需要加载状态、通用 `RelayCommand`、反射调用方法等基础 Presenter。 |
| `CommandsBindableBase` | 需要声明和自动收集 `DisplayCommand` 的 Presenter。 |
| `DisplayBindableBase` | 需要名称、图标、分组、描述、排序、默认值等显示元数据的 Presenter。 |

---

## 3. `BindableBase`

`BindableBase` 是最基础的可绑定对象：

```csharp
public abstract class BindableBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    public virtual void RaisePropertyChanged(
        [CallerMemberName] string propertyName = "")
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    }
}
```

它实现了 WPF 绑定更新所需的 `INotifyPropertyChanged`。

### 3.1 基础用法

```csharp
public class UserViewModel : BindableBase
{
    private string _name;

    public string Name
    {
        get => _name;
        set
        {
            if (_name == value)
                return;

            _name = value;
            RaisePropertyChanged();
        }
    }
}
```

XAML：

```xaml
<TextBox Text="{Binding Name, UpdateSourceTrigger=PropertyChanged}" />
<TextBlock Text="{Binding Name}" />
```

当 `Name` 改变时，`RaisePropertyChanged()` 使用调用方成员名自动通知 `Name` 属性，无需显式写：

```csharp
RaisePropertyChanged(nameof(Name));
```

### 3.2 什么时候继承 `BindableBase`

适用于：

- 简单 ViewModel。
- 列表项状态对象。
- UI 临时状态。
- 不需要图标、显示名称、命令集合的业务 Presenter。

不适用于：

- 需要被框架自动发现命令、生成菜单或工具栏的对象。
- 需要 `DisplayAttribute`、`IconAttribute`、默认值机制的设置项或 Presenter。

---

## 4. `Bindable`

`Bindable` 继承 `BindableBase`，提供通用加载与命令能力：

```csharp
public abstract class Bindable : BindableBase
{
    public RelayCommand RelayCommand { get; set; }
    public RelayCommand LoadedCommand => new RelayCommand(Loaded);
    public RelayCommand CallMethodCommand { get; set; }
}
```

这些命令均标记为：

```csharp
[JsonIgnore]
[XmlIgnore]
[Browsable(false)]
```

因此运行时命令不会被作为配置或项目数据保存。

### 4.1 `RelayCommand`

构造 `Bindable` 时会创建：

```csharp
this.RelayCommand = new RelayCommand(RelayMethod);
this.CallMethodCommand = new RelayCommand(CallMethod);
RelayMethod("init");
```

可重写：

```csharp
protected override void RelayMethod(object obj)
{
    if (Equals(obj, "init"))
    {
        // 初始化逻辑
        return;
    }

    if (Equals(obj, "refresh"))
    {
        Refresh();
    }
}
```

XAML：

```xaml
<Button
    Command="{Binding RelayCommand}"
    CommandParameter="refresh"
    Content="刷新" />
```

适合参数驱动、较简单的通用命令入口。

### 4.2 `LoadedCommand`

`LoadedCommand` 将 `Loaded` 事件转换为命令：

```csharp
protected virtual void Loaded(object obj)
{
    if (obj is RoutedEventArgs args)
        this._targetElement = args.Source;
    this.IsLoaded = true;
}
```

XAML：

```xaml
<UserControl Loaded="{Binding LoadedCommand}">
</UserControl>
```

在标准 WPF 中，`Loaded` 是事件，通常应通过 Behaviors 把事件转换为命令：

```xaml
xmlns:b="http://schemas.microsoft.com/xaml/behaviors"

<b:Interaction.Triggers>
    <b:EventTrigger EventName="Loaded">
        <b:InvokeCommandAction Command="{Binding LoadedCommand}" PassEventArgsToCommand="True" />
    </b:EventTrigger>
</b:Interaction.Triggers>
```

重写加载逻辑：

```csharp
protected override void Loaded(object obj)
{
    base.Loaded(obj);
    LoadData();
}
```

`IsLoaded` 可用于避免在视图尚未加载时执行依赖 UI 生命周期的操作。

### 4.3 `CallMethodCommand`

`CallMethodCommand` 根据参数反射调用 ViewModel 的公开方法：

```csharp
protected virtual void CallMethod(object obj)
{
    string methodName = obj?.ToString();
    MethodInfo method = GetType().GetMethod(methodName);
    object[] parameters = method.GetParameters()
        .Select(l => l.RawDefaultValue is DBNull ? null : l.RawDefaultValue)
        .ToArray();
    method.Invoke(this, parameters);
}
```

示例：

```csharp
public class ToolViewModel : Bindable
{
    public void Refresh()
    {
        // 刷新
    }
}
```

```xaml
<Button
    Command="{Binding CallMethodCommand}"
    CommandParameter="Refresh"
    Content="刷新" />
```

注意：反射调用缺少编译期重构保护，也不适合复杂参数或高频操作。常规业务命令优先使用显式 `RelayCommand` 或 `DisplayCommand`。

---

## 5. `RelayCommand`

`RelayCommand` 是框架的基础 `ICommand` 实现：

```csharp
public class RelayCommand : CommandBase
{
    protected Action<object> _action;
    protected readonly Predicate<object> _canExecute;

    public RelayCommand(Action<object> action);
    public RelayCommand(Action<object> execute, Predicate<object> canExecute);
}
```

执行逻辑：

```csharp
public override void Execute(object parameter)
{
    _action?.Invoke(parameter);
    CommandManager.InvalidateRequerySuggested();
}

public override bool CanExecute(object parameter)
{
    return _canExecute == null ? true : _canExecute.Invoke(parameter);
}
```

### 5.1 基础命令

```csharp
public class CounterViewModel : BindableBase
{
    private int _count;

    public int Count
    {
        get => _count;
        set
        {
            _count = value;
            RaisePropertyChanged();
        }
    }

    public RelayCommand IncreaseCommand => new RelayCommand(_ =>
    {
        Count++;
    });
}
```

XAML：

```xaml
<StackPanel>
    <TextBlock Text="{Binding Count}" />
    <Button Command="{Binding IncreaseCommand}" Content="增加" />
</StackPanel>
```

### 5.2 带 `CanExecute` 的命令

```csharp
public RelayCommand DeleteCommand => new RelayCommand(
    _ => DeleteSelected(),
    _ => SelectedItem != null);
```

```xaml
<Button Command="{Binding DeleteCommand}" Content="删除" />
```

当 `CanExecute` 返回 `false` 时，WPF 默认会禁用按钮。

`RelayCommand.Execute()` 后调用：

```csharp
CommandManager.InvalidateRequerySuggested();
```

这会请求 WPF 重新查询命令状态。状态依赖属性更新后，通常可由 WPF 输入循环触发重查；必要时可手动调用：

```csharp
CommandManager.InvalidateRequerySuggested();
```

### 5.3 不要在属性 Getter 中反复创建复杂命令

框架大量示例使用表达式属性：

```csharp
public RelayCommand RefreshCommand => new RelayCommand(_ => Refresh());
```

这种写法简洁，但每次读取属性可能创建新命令对象。对于频繁绑定、状态复杂或需要保持命令实例的场景，建议缓存：

```csharp
private readonly RelayCommand _refreshCommand;

public MyViewModel()
{
    _refreshCommand = new RelayCommand(_ => Refresh());
}

public RelayCommand RefreshCommand => _refreshCommand;
```

---

## 6. `CommandsBindableBase`

`CommandsBindableBase` 继承 `Bindable`，用于自动收集展示命令：

```csharp
public abstract class CommandsBindableBase : Bindable, ICommandsBindable
{
    public ObservableCollection<ICommand> Commands { get; }
}
```

构造时调用：

```csharp
this.UpdateCommands();
```

命令集合不参与序列化：

```csharp
[JsonIgnore]
[XmlIgnore]
public ObservableCollection<ICommand> Commands { get; }
```

### 6.1 命令自动发现规则

默认扫描当前类型的：

```csharp
BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public
```

并筛选：

```csharp
typeof(IDisplayCommand).IsAssignableFrom(property.PropertyType)
```

因此，满足以下条件的公开属性会加入 `Commands`：

1. 是公开属性。
2. 属性类型实现 `IDisplayCommand`。
3. 可读取。
4. 没有标记 `[Browsable(false)]`。

扫描后会按 `Order` 排序：

```csharp
this.CreateCommands()
    .Where(x => x != null)
    .OrderBy(x => x.Order)
```

### 6.2 命令集合绑定

```xaml
<ItemsControl ItemsSource="{Binding Commands}">
    <ItemsControl.ItemTemplate>
        <DataTemplate>
            <Button
                Command="{Binding}"
                Content="{Binding Name}"
                ToolTip="{Binding Description}" />
        </DataTemplate>
    </ItemsControl.ItemTemplate>
</ItemsControl>
```

框架中的菜单、工具栏、属性面板等可以利用 `Name`、`Icon`、`GroupName`、`Order` 自动生成命令入口。

---

## 7. `DisplayBindableBase`

`DisplayBindableBase` 是框架中最常用的 Presenter / 配置对象基类：

```csharp
public abstract class DisplayBindableBase : CommandsBindableBase, IDable, IDisplayBindable
{
}
```

它提供显示元数据：

| 属性 | 说明 |
|---|---|
| `ID` | 唯一标识。 |
| `Name` | 显示名称。 |
| `Icon` | 字体图标字符串。 |
| `ShortName` | 简称。 |
| `Prompt` | 提示文本。 |
| `GroupName` | 分组名称。 |
| `Description` | 描述、工具提示。 |
| `Order` | 显示排序。 |
| `LoadDefaultCommand` | 恢复默认值命令。 |

### 7.1 使用特性初始化显示元数据

构造函数会读取类型特性：

```csharp
DisplayAttribute display = type.GetCustomAttribute<DisplayAttribute>(true);
IconAttribute icon = type.GetCustomAttribute<IconAttribute>(true);
IDAttribute id = type.GetCustomAttribute<IDAttribute>(true);
```

示例：

```csharp
[ID("system.settings")]
[Icon(FontIcons.Setting)]
[Display(
    Name = "系统设置",
    GroupName = "系统",
    Description = "配置系统运行参数",
    Order = 10,
    ShortName = "设置")]
public class SystemSettingsPresenter : DisplayBindableBase
{
}
```

构造完成后：

```csharp
Name        = "系统设置"
GroupName   = "系统"
Description = "配置系统运行参数"
Order       = 10
ShortName   = "设置"
Icon        = FontIcons.Setting
ID          = "system.settings"
```

如果未标记 `[ID]`，框架会自动生成：

```csharp
Guid.NewGuid().ToString()
```

对于需要持久化或跨会话识别的对象，建议显式设置稳定 ID，不要依赖随机 GUID。

### 7.2 默认值：`[DefaultValue]`

`DisplayBindableBase` 构造末尾调用：

```csharp
LoadDefault();
```

它会扫描属性上的 `[DefaultValue]`：

```csharp
public virtual void LoadDefault(object obj)
{
    PropertyInfo[] ps = obj.GetType().GetProperties();
    foreach (PropertyInfo p in ps)
    {
        DefaultValueAttribute d = p.GetCustomAttribute<DefaultValueAttribute>();
        if (d == null)
            continue;
        p.SetValue(obj, value);
    }
}
```

示例：

```csharp
[Display(Name = "显示配置")]
public class DisplayOptions : DisplayBindableBase
{
    private int _fontSize;

    [DefaultValue(14)]
    [Display(Name = "字号")]
    public int FontSize
    {
        get => _fontSize;
        set
        {
            _fontSize = value;
            RaisePropertyChanged();
        }
    }

    private bool _showGrid;

    [DefaultValue(true)]
    [Display(Name = "显示网格")]
    public bool ShowGrid
    {
        get => _showGrid;
        set
        {
            _showGrid = value;
            RaisePropertyChanged();
        }
    }
}
```

恢复默认值：

```xaml
<Button Command="{Binding LoadDefaultCommand}" Content="恢复默认" />
```

### 7.3 序列化行为

以下显示元数据通常不保存：

```csharp
[JsonIgnore]
[XmlIgnore]
public virtual string ShortName { get; set; }

[JsonIgnore]
[XmlIgnore]
public virtual string Prompt { get; set; }

[JsonIgnore]
[XmlIgnore]
public virtual string GroupName { get; set; }

[JsonIgnore]
[XmlIgnore]
public virtual string Description { get; set; }

[JsonIgnore]
[XmlIgnore]
public virtual int Order { get; set; }
```

原因：它们通常来自代码特性，是 UI 元数据而非用户运行时配置。`ID`、`Name`、`Icon` 是否保存取决于对象实际业务需求和属性标记。

---

## 8. `DisplayCommand`

`DisplayCommand` 在 `RelayCommand` 基础上增加可展示元数据：

```csharp
public class DisplayCommand : RelayCommand, IDisplayCommand, INotifyPropertyChanged
{
    public string Name { get; set; }
    public string Icon { get; set; }
    public string Description { get; set; }
    public string GroupName { get; set; }
    public int Order { get; set; }
}
```

接口：

```csharp
public interface IDisplayCommand : ICommand
{
    string Name { get; set; }
    string Icon { get; set; }
    string Description { get; set; }
    string GroupName { get; set; }
    int Order { get; set; }
}
```

适用场景：

- 工具栏按钮。
- 菜单命令。
- 右键菜单。
- 自动生成操作按钮。
- 根据分组和排序组织命令。

### 8.1 基础 `DisplayCommand`

```csharp
public class ProductPresenter : DisplayBindableBase
{
    private Product _selectedItem;

    public Product SelectedItem
    {
        get => _selectedItem;
        set
        {
            _selectedItem = value;
            RaisePropertyChanged();
        }
    }

    public DisplayCommand RefreshCommand => new DisplayCommand(_ => Refresh())
    {
        Name = "刷新",
        Icon = FontIcons.Refresh,
        Description = "重新加载数据",
        GroupName = "工具栏",
        Order = 10
    };

    public DisplayCommand DeleteCommand => new DisplayCommand(
        _ => Delete(SelectedItem),
        _ => SelectedItem != null)
    {
        Name = "删除",
        Icon = FontIcons.Delete,
        Description = "删除选中数据",
        GroupName = "工具栏",
        Order = 20
    };

    private void Refresh()
    {
    }

    private void Delete(Product product)
    {
    }
}
```

### 8.2 使用特性补充命令元数据

`CommandsBindableBase` 会读取命令属性上的 `[Display]` 与 `[Icon]`：

```csharp
[Icon(FontIcons.Refresh)]
[Display(
    Name = "刷新",
    Description = "重新加载数据",
    GroupName = "工具栏,右键菜单",
    Order = 10)]
public DisplayCommand RefreshCommand => new DisplayCommand(_ => Refresh());
```

自动发现时会把特性信息填充到命令：

```csharp
displayCommand.Name = displayCommand.Name ?? attr.Name;
displayCommand.Description = displayCommand.Description ?? attr.Description;
displayCommand.GroupName = displayCommand.GroupName ?? attr.GroupName;
if (displayCommand.Order <= 0)
    displayCommand.Order = attr.GetOrder() ?? 0;
displayCommand.Icon = displayCommand.Icon ?? icon.Icon;
```

推荐在框架型 Presenter 中使用特性，使定义更紧凑：

```csharp
[Icon(FontIcons.Save)]
[Display(Name = "保存", Description = "保存当前数据", GroupName = "菜单栏,工具栏", Order = 20)]
public DisplayCommand SaveCommand => new DisplayCommand(
    _ => Save(),
    _ => CanSave());
```

### 8.3 命令图标按钮

```xaml
<FontIconButton
    Command="{Binding SaveCommand}"
    Style="{DynamicResource {x:Static h:FontIconButtonKeys.Command}}" />
```

`FontIconButtonKeys.Command` 会绑定：

```xaml
<Setter Property="Content" Value="{Binding RelativeSource={RelativeSource Self}, Path=Command.Icon}" />
<Setter Property="ToolTip" Value="{Binding RelativeSource={RelativeSource Self}, Path=Command.Name}" />
```

因此命令可自动显示图标和名称提示。

### 8.4 命令集合的菜单呈现

```xaml
<MenuItem Header="操作" ItemsSource="{Binding Commands}">
    <MenuItem.ItemContainerStyle>
        <Style TargetType="MenuItem">
            <Setter Property="Command" Value="{Binding}" />
            <Setter Property="Header" Value="{Binding Name}" />
        </Style>
    </MenuItem.ItemContainerStyle>
</MenuItem>
```

实际菜单样式可进一步绑定 `Icon`、`Description`、`GroupName` 和 `Order`。

---

## 9. `RelayCommand` 与 `DisplayCommand` 的选择

| 对比项 | `RelayCommand` | `DisplayCommand` |
|---|---|---|
| 实现 `ICommand` | 是 | 是，继承 `RelayCommand`。 |
| 执行与可用性 | 支持 | 支持。 |
| 命令名称 | 无 | `Name`。 |
| 图标 | 无 | `Icon`。 |
| 描述 / ToolTip | 无 | `Description`。 |
| 分组 / 排序 | 无 | `GroupName`、`Order`。 |
| 自动加入 `Commands` | 否 | 是，前提是公开属性且继承 `CommandsBindableBase`。 |
| 适用场景 | 普通按钮、局部交互、加载命令。 | 菜单、工具栏、操作面板、自动命令呈现。 |

简单选择：

- 只需要执行行为：使用 `RelayCommand`。
- 需要自动生成菜单、工具栏、图标或分组：使用 `DisplayCommand`。

---

## 10. 完整 Presenter 示例

```csharp
[Icon(FontIcons.People)]
[Display(Name = "用户管理", GroupName = "系统", Description = "管理系统用户", Order = 10)]
public class UserManagerPresenter : DisplayBindableBase
{
    private ObservableCollection<User> _users = new();

    public ObservableCollection<User> Users
    {
        get => _users;
        set
        {
            _users = value;
            RaisePropertyChanged();
        }
    }

    private User _selectedUser;

    public User SelectedUser
    {
        get => _selectedUser;
        set
        {
            _selectedUser = value;
            RaisePropertyChanged();
            CommandManager.InvalidateRequerySuggested();
        }
    }

    [Icon(FontIcons.Add)]
    [Display(Name = "新增", GroupName = "工具栏", Description = "新增用户", Order = 10)]
    public DisplayCommand AddCommand => new DisplayCommand(_ =>
    {
        Users.Add(new User { Name = "新用户" });
    });

    [Icon(FontIcons.Delete)]
    [Display(Name = "删除", GroupName = "工具栏", Description = "删除选中用户", Order = 20)]
    public DisplayCommand DeleteCommand => new DisplayCommand(
        _ => Users.Remove(SelectedUser),
        _ => SelectedUser != null);

    [Icon(FontIcons.Refresh)]
    [Display(Name = "刷新", GroupName = "工具栏", Description = "刷新用户列表", Order = 30)]
    public DisplayCommand RefreshCommand => new DisplayCommand(_ => LoadUsers());

    private void LoadUsers()
    {
        // 从服务加载数据
    }
}

public class User : BindableBase
{
    private string _name;

    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            RaisePropertyChanged();
        }
    }
}
```

XAML：

```xaml
<Grid>
    <Grid.RowDefinitions>
        <RowDefinition Height="Auto" />
        <RowDefinition />
    </Grid.RowDefinitions>

    <ItemsControl ItemsSource="{Binding Commands}">
        <ItemsControl.ItemsPanel>
            <ItemsPanelTemplate>
                <StackPanel Orientation="Horizontal" />
            </ItemsPanelTemplate>
        </ItemsControl.ItemsPanel>
        <ItemsControl.ItemTemplate>
            <DataTemplate>
                <FontIconButton
                    Command="{Binding}"
                    Style="{DynamicResource {x:Static h:FontIconButtonKeys.Command}}" />
            </DataTemplate>
        </ItemsControl.ItemTemplate>
    </ItemsControl>

    <DataGrid
        Grid.Row="1"
        ItemsSource="{Binding Users}"
        SelectedItem="{Binding SelectedUser}" />
</Grid>
```

---

## 11. 属性变更与命令可用性

当 `CanExecute` 依赖某个属性时，属性变化后应触发命令重新查询。

```csharp
public Product SelectedItem
{
    get => _selectedItem;
    set
    {
        _selectedItem = value;
        RaisePropertyChanged();
        CommandManager.InvalidateRequerySuggested();
    }
}
```

例如：

```csharp
public RelayCommand EditCommand => new RelayCommand(
    _ => Edit(SelectedItem),
    _ => SelectedItem != null);
```

`SelectedItem` 改变后，按钮应更新启用状态。

对于复杂场景，建议将可执行条件封装为方法：

```csharp
private bool CanDelete() => SelectedItem != null && !SelectedItem.IsReadOnly;

public DisplayCommand DeleteCommand => new DisplayCommand(
    _ => Delete(SelectedItem),
    _ => CanDelete());
```

---

## 12. 与 `DataTemplate` 的结合

MVVM 对象可作为 `ContentPresenter.Content`，通过 `DataTemplate` 呈现：

```xaml
<ContentPresenter Content="{Binding CurrentPresenter}" />
```

```xaml
<DataTemplate DataType="{x:Type local:UserManagerPresenter}">
    <local:UserManagerView />
</DataTemplate>
```

也可以直接用模板构建 UI：

```xaml
<DataTemplate DataType="{x:Type local:TextBoxPresenter}">
    <TextBox Text="{Binding Text}" />
</DataTemplate>
```

这种方式让：

- ViewModel / Presenter 负责状态和命令。
- `DataTemplate` 负责具体 UI。
- 外部应用可通过资源字典覆盖显示。
- 数据对象更易序列化、保存和单元测试。

---

## 13. 序列化建议

ViewModel 或 Presenter 可能同时包含数据、命令、服务、缓存和 UI 状态。保存时应只保留业务数据。

```csharp
public class ProjectPresenter : DisplayBindableBase
{
    public string Title { get; set; }

    [JsonIgnore]
    public IProjectService ProjectService { get; set; }

    [JsonIgnore]
    public RelayCommand SaveCommand => new RelayCommand(_ => Save());
}
```

框架的 NewtonsoftJson `CustomContractResolver` 会自动忽略 `ICommand` 属性；服务引用、缓存、事件和 UI 对象仍建议显式标记 `[JsonIgnore]` / `[XmlIgnore]`。

不要序列化：

- `Window`、`UserControl`、`FrameworkElement`。
- `DispatcherObject` 的运行时引用。
- 数据库连接、文件流、网络连接。
- 命令闭包、事件订阅、服务实例。

---

## 14. 单元测试建议

因为 Presenter 只包含状态和命令，可脱离 WPF 控件测试：

```csharp
[Fact]
public void DeleteCommand_RemovesSelectedUser()
{
    var presenter = new UserManagerPresenter();
    var user = new User { Name = "张三" };
    presenter.Users.Add(user);
    presenter.SelectedUser = user;

    presenter.DeleteCommand.Execute(null);

    Assert.Empty(presenter.Users);
}
```

测试命令可用性：

```csharp
[Fact]
public void DeleteCommand_IsDisabledWhenNoSelection()
{
    var presenter = new UserManagerPresenter();

    bool canExecute = presenter.DeleteCommand.CanExecute(null);

    Assert.False(canExecute);
}
```

业务规则应尽量放在 Presenter / Service 中，不应只能通过按钮 Click 事件测试。

---

## 15. 常见问题

### 修改属性后 UI 不更新

检查：

1. 对象是否继承 `BindableBase` 或实现 `INotifyPropertyChanged`。
2. Setter 中是否调用 `RaisePropertyChanged()`。
3. 绑定路径是否正确。
4. 是否修改了绑定对象实例，但没有通知对应属性。

### 命令按钮始终禁用

检查：

1. `CanExecute` 是否正确返回 `true`。
2. 依赖属性变化后是否调用 `CommandManager.InvalidateRequerySuggested()`。
3. 按钮是否绑定到正确命令属性。
4. 是否误将命令属性标记为 `[Browsable(false)]`，导致自动命令集合未包含它。

### `Commands` 中没有命令

检查：

1. ViewModel 是否继承 `CommandsBindableBase` 或 `DisplayBindableBase`。
2. 命令属性是否为 `public`。
3. 属性类型是否为 `DisplayCommand` 或其他 `IDisplayCommand` 实现。
4. 是否标记了 `[Browsable(false)]`。
5. 命令是否在 `UpdateCommands()` 执行时返回 `null`。

### 命令图标不显示

检查：

1. 命令是否设置 `Icon`，或命令属性是否标记 `[Icon(...)]`。
2. 是否使用 `FontIconButtonKeys.Command` 样式。
3. 当前主题的 `SystemKeys.FontFamilyIcon` 是否为可用字体。

### `DisplayAttribute` 没有效果

检查：

1. 类型是否继承 `DisplayBindableBase`。
2. 命令是否继承 `CommandsBindableBase` 并被自动扫描。
3. `[Display]` 是否标记在正确的类型或命令属性上。
4. 命令是否已经手动设置同名元数据；自动填充不会覆盖已有值。

---

## 16. 二次开发建议

- 简单状态对象优先继承 `BindableBase`。
- 需要通用加载、反射调用命令时使用 `Bindable`。
- 需要自动命令集合时使用 `CommandsBindableBase`。
- 需要图标、名称、分组、排序、默认值和设置页展示时使用 `DisplayBindableBase`。
- 普通局部操作使用 `RelayCommand`。
- 菜单、工具栏、右键菜单等可视化操作使用 `DisplayCommand`。
- 命令属性应保持公开且不返回 `null`。
- `CanExecute` 依赖属性变化后触发 `CommandManager.InvalidateRequerySuggested()`。
- 运行时服务、控件和命令不作为业务状态序列化。
- 复杂业务逻辑放入 Service；Presenter 负责协调状态和命令。
- 使用 `DataTemplate` 呈现 Presenter，避免在 Presenter 中创建 WPF 控件。
