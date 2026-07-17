# ContentPresenter 与 DataTemplate 呈现模式二次开发文档

**适用项目：** WPF-Control 的 Presenter、Dialog、Project、Theme 等模块  
**核心类型：** `ContentPresenter`、`ContentControl`、`DataTemplate`、`ResourceDictionary`、`Ioc`、Presenter/ViewModel  
**核心理念：** 容器负责占位和生命周期，数据对象负责状态，`DataTemplate` 负责把数据呈现为 UI。

本文说明框架中以 `ContentPresenter` 负责占位、以 `DataTemplate` 负责呈现的设计模式；介绍如何通过资源字典定义继承和覆盖模板，以及这种方式在外部控制显示、数据驱动 UI、序列化保存和单元测试方面的优势。

---

## 1. 模式概览

框架中大量功能不直接把具体 `UserControl` 写入窗口、对话框或容器，而是把一个 Presenter / ViewModel / 业务对象设置为内容：

```text
宿主容器
    ↓
ContentPresenter / ContentControl 占位
    ↓
Content = Presenter 或 ViewModel 数据对象
    ↓
WPF 根据对象类型查找 DataTemplate
    ↓
DataTemplate 创建实际控件树并绑定数据
```

例如，对话框宿主只需要承载一个对象：

```csharp
IocMessage.Dialog.Show(new TextBoxPresenter());
```

实际显示的 `TextBox` 不由 `TextBoxPresenter` 创建，而是由该类型对应的 `DataTemplate` 创建。

这种分离关系如下：

| 层 | 职责 |
|---|---|
| 宿主容器 | 控制位置、大小、边距、窗口按钮、遮罩、对话框生命周期。 |
| Presenter / ViewModel | 保存状态、业务行为、命令和可序列化数据。 |
| `ContentPresenter` | 负责内容占位和调用 WPF 模板选择机制。 |
| `DataTemplate` | 定义某种数据类型实际呈现的 XAML 控件树。 |
| `ResourceDictionary` | 注册、组合、继承和覆盖 `DataTemplate`。 |

---

## 2. 框架中的实际示例

### 2.1 `DialogWindow` 只负责承载内容

`H.Windows.Dialog/DialogWindow.xaml` 的默认模板中使用：

```xaml
<AdornerDecorator Grid.Row="1" Grid.ColumnSpan="3">
    <ContentPresenter
        Margin="{TemplateBinding Padding}"
        HorizontalAlignment="{TemplateBinding HorizontalContentAlignment}"
        VerticalAlignment="{TemplateBinding VerticalContentAlignment}" />
</AdornerDecorator>
```

该 `ContentPresenter` 位于对话框窗口的控制模板中。它不关心内容是：

- `TextBoxPresenter`
- `ImagePresenter`
- `CardPresenter`
- 表单 Presenter
- 项目 Presenter
- 任意业务 ViewModel

它只负责呈现 `DialogWindow.Content`。具体显示内容由 WPF 根据 `Content` 的运行时类型选择对应的 `DataTemplate`。

### 2.2 `TextBoxPresenter` 只保存状态

`TextBoxPresenter` 是一个轻量数据对象：

```csharp
public interface ITextBoxPresenter
{
    string Text { get; set; }
}

[Icon("\xE70F")]
public class TextBoxPresenter : DisplayBindableBase, ITextBoxPresenter
{
    public string Text { get; set; }
}
```

它没有继承 `TextBox`，也没有创建任何 UI 元素。它只定义：

- `Text` 状态。
- 属性通知能力。
- 图标、名称等元数据。
- 与对话框服务协作的行为。

### 2.3 `DataTemplate` 定义实际 UI

`H.Presenters.Common/Presenters/TextBoxPresenter.xaml` 中定义：

```xaml
<ResourceDictionary
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:h="https://github.com/HeBianGu"
    xmlns:local="clr-namespace:H.Presenters.Common.Presenters">

    <DataTemplate DataType="{x:Type local:TextBoxPresenter}">
        <TextBox
            Height="Auto"
            MinWidth="100"
            MinHeight="{DynamicResource {x:Static h:LayoutKeys.ItemHeight}}"
            Margin="5"
            VerticalAlignment="Stretch"
            VerticalContentAlignment="Center"
            AcceptsReturn="True"
            Text="{Binding Text}"
            TextWrapping="Wrap"
            ToolTip="{Binding Text}" />
    </DataTemplate>
</ResourceDictionary>
```

当 `ContentPresenter.Content` 是 `TextBoxPresenter` 时，WPF 自动选中该模板并创建 `TextBox`。

---

## 3. `ContentPresenter` 与 `ContentControl` 的区别

| 类型 | 主要职责 | 常见用途 |
|---|---|---|
| `ContentPresenter` | 单纯呈现内容，通常放在 `ControlTemplate` 内。 | 自定义窗口、对话框、控件模板中的内容占位。 |
| `ContentControl` | 既是控件又能承载单个内容，提供 `Content`、`ContentTemplate` 等属性。 | 页面区域、动态区域、简单宿主。 |
| `ItemsControl` | 承载多个项目，使用 `ItemTemplate` 呈现集合项。 | 列表、菜单、导航、卡片集合。 |

`ContentPresenter` 示例：

```xaml
<ContentPresenter Content="{Binding CurrentPresenter}" />
```

`ContentControl` 示例：

```xaml
<ContentControl Content="{Binding CurrentPresenter}" />
```

两者都可以利用隐式 `DataTemplate`。框架的窗口和复杂控件模板中通常使用 `ContentPresenter`，因为宿主控件已经拥有 `Content` 属性，只需要在模板中提供一个呈现位置。

---

## 4. 隐式 `DataTemplate` 如何匹配

下面的模板没有显式 `x:Key`：

```xaml
<DataTemplate DataType="{x:Type local:TextBoxPresenter}">
    <TextBox Text="{Binding Text}" />
</DataTemplate>
```

这称为**隐式数据模板**。WPF 会把 `DataType` 转换为隐式模板键，并在资源查找范围内按数据对象类型查找模板。

调用：

```csharp
var presenter = new TextBoxPresenter
{
    Text = "请输入内容"
};

contentControl.Content = presenter;
```

宿主无需指定模板：

```xaml
<ContentPresenter Content="{Binding CurrentPresenter}" />
```

WPF 自动完成：

```text
CurrentPresenter 的运行时类型 = TextBoxPresenter
    ↓
查找 DataTemplate(DataType = TextBoxPresenter)
    ↓
创建 TextBox
    ↓
DataContext = 当前 TextBoxPresenter 实例
    ↓
TextBox.Text 绑定到 TextBoxPresenter.Text
```

---

## 5. 定义 Presenter + DataTemplate

### 5.1 定义数据对象

推荐 Presenter / ViewModel 保持 UI 无关：

```csharp
public class CustomerPresenter : BindableBase
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

    private string _email;

    public string Email
    {
        get => _email;
        set
        {
            _email = value;
            RaisePropertyChanged();
        }
    }
}
```

不要在 Presenter 中保存以下 UI 对象：

```csharp
public TextBox NameTextBox { get; set; }
public Window OwnerWindow { get; set; }
public UserControl View { get; set; }
```

这些对象会使数据对象与 WPF 视觉树强耦合，降低序列化、复用和测试能力。

### 5.2 定义默认呈现模板

创建 `CustomerPresenter.xaml`：

```xaml
<ResourceDictionary
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:local="clr-namespace:MyApp.Presenters">

    <DataTemplate DataType="{x:Type local:CustomerPresenter}">
        <Grid Margin="12">
            <Grid.RowDefinitions>
                <RowDefinition Height="Auto" />
                <RowDefinition Height="Auto" />
            </Grid.RowDefinitions>
            <Grid.ColumnDefinitions>
                <ColumnDefinition Width="Auto" />
                <ColumnDefinition Width="*" />
            </Grid.ColumnDefinitions>

            <TextBlock VerticalAlignment="Center" Text="名称" />
            <TextBox
                Grid.Column="1"
                MinWidth="240"
                Margin="8,0,0,0"
                Text="{Binding Name, UpdateSourceTrigger=PropertyChanged}" />

            <TextBlock Grid.Row="1" VerticalAlignment="Center" Text="邮箱" />
            <TextBox
                Grid.Row="1"
                Grid.Column="1"
                Margin="8,8,0,0"
                Text="{Binding Email, UpdateSourceTrigger=PropertyChanged}" />
        </Grid>
    </DataTemplate>
</ResourceDictionary>
```

### 5.3 合并资源字典

在 `App.xaml` 或模块主题资源中合并：

```xaml
<Application.Resources>
    <ResourceDictionary>
        <ResourceDictionary.MergedDictionaries>
            <ResourceDictionary Source="/MyApp;component/Presenters/CustomerPresenter.xaml" />
        </ResourceDictionary.MergedDictionaries>
    </ResourceDictionary>
</Application.Resources>
```

### 5.4 放入占位区域

```xaml
<ContentPresenter Content="{Binding CurrentPresenter}" />
```

```csharp
public CustomerPresenter CurrentPresenter { get; } = new()
{
    Name = "张三",
    Email = "zhangsan@example.com"
};
```

---

## 6. 显式模板与隐式模板

### 6.1 隐式模板：按数据类型自动选择

```xaml
<DataTemplate DataType="{x:Type local:CustomerPresenter}">
    <TextBlock Text="{Binding Name}" />
</DataTemplate>
```

适合：一个对象类型通常只有一个默认呈现方式。

### 6.2 显式模板：同一类型存在多个视图

```xaml
<DataTemplate x:Key="CustomerCompactTemplate" DataType="{x:Type local:CustomerPresenter}">
    <TextBlock Text="{Binding Name}" />
</DataTemplate>

<DataTemplate x:Key="CustomerDetailTemplate" DataType="{x:Type local:CustomerPresenter}">
    <StackPanel>
        <TextBlock FontWeight="Bold" Text="{Binding Name}" />
        <TextBlock Text="{Binding Email}" />
    </StackPanel>
</DataTemplate>
```

使用：

```xaml
<ContentPresenter
    Content="{Binding CurrentPresenter}"
    ContentTemplate="{StaticResource CustomerDetailTemplate}" />
```

适合：

- 同一数据在列表、详情、打印、预览中有不同视图。
- 同一 Presenter 有编辑态与只读态。
- 外部调用者需要明确选择某种显示方式。

---

## 7. 通过资源覆盖 `DataTemplate`

### 7.1 为什么可以覆盖

框架只依赖：

```xaml
<ContentPresenter Content="{Binding Presenter}" />
```

而不依赖一个固定的 `UserControl` 类型。只要外部资源字典为该 Presenter 类型提供新的 `DataTemplate`，就可以改变呈现效果，不需要修改：

- Presenter 类。
- 对话框服务。
- 宿主控件。
- 框架库源代码。

### 7.2 应用程序覆盖框架默认模板

假设框架定义了：

```xaml
<DataTemplate DataType="{x:Type h:TextBoxPresenter}">
    <TextBox Text="{Binding Text}" />
</DataTemplate>
```

应用程序可在自己的 `App.xaml` 中重新定义同类型模板：

```xaml
<Application.Resources>
    <ResourceDictionary>
        <DataTemplate DataType="{x:Type presenters:TextBoxPresenter}">
            <Border Padding="16" CornerRadius="8" Background="#FFF5F7FA">
                <StackPanel>
                    <TextBlock Margin="0,0,0,6" FontWeight="Bold" Text="自定义输入" />
                    <TextBox
                        MinWidth="280"
                        Text="{Binding Text, UpdateSourceTrigger=PropertyChanged}" />
                </StackPanel>
            </Border>
        </DataTemplate>
    </ResourceDictionary>
</Application.Resources>
```

因为应用资源位于更靠近使用位置的资源范围中，它会优先参与资源解析。若通过合并字典覆盖同层资源，通常将应用覆盖字典放在框架资源字典之后：

```xaml
<ResourceDictionary.MergedDictionaries>
    <ResourceDictionary Source="/H.Presenters.Common;component/Presenters/TextBoxPresenter.xaml" />
    <ResourceDictionary Source="/MyApp;component/Themes/PresenterOverrides.xaml" />
</ResourceDictionary.MergedDictionaries>
```

> 同一个资源字典的 `MergedDictionaries` 中，后合并的字典通常具有更高的查找优先级。局部资源（例如 `Window.Resources`、`ContentPresenter.Resources`）则比应用资源更接近控件，适合局部覆盖。

### 7.3 窗口级局部覆盖

只希望某一个窗口或功能区域使用不同 UI 时，将模板放到局部资源：

```xaml
<Window.Resources>
    <DataTemplate DataType="{x:Type local:CustomerPresenter}">
        <Border Padding="8" BorderBrush="SteelBlue" BorderThickness="1">
            <TextBlock Text="{Binding Name}" />
        </Border>
    </DataTemplate>
</Window.Resources>

<Grid>
    <ContentPresenter Content="{Binding CurrentPresenter}" />
</Grid>
```

这样不会影响应用中其他位置的 `CustomerPresenter`。

### 7.4 主题级覆盖

不同主题可以合并不同模板资源字典：

```xaml
<ResourceDictionary.MergedDictionaries>
    <ResourceDictionary Source="/MyApp;component/Themes/Presenters.Default.xaml" />
    <!-- 深色主题时替换为 Presenters.Dark.xaml -->
</ResourceDictionary.MergedDictionaries>
```

Presenter 数据和宿主代码保持不变，主题只负责替换视觉结构、画刷和布局。

---

## 8. 外部控制显示

这种模式允许外部应用决定“同一个数据对象如何显示”。

### 8.1 框架提供数据契约，应用决定视图

框架模块定义：

```csharp
public class ReportPresenter : BindableBase
{
    public string Title { get; set; }
    public IReadOnlyList<ReportRow> Rows { get; set; }
}
```

框架宿主使用：

```xaml
<ContentPresenter Content="{Binding ReportPresenter}" />
```

外部应用可以定义普通表格视图：

```xaml
<DataTemplate DataType="{x:Type reports:ReportPresenter}">
    <DataGrid ItemsSource="{Binding Rows}" />
</DataTemplate>
```

也可以定义卡片视图：

```xaml
<DataTemplate DataType="{x:Type reports:ReportPresenter}">
    <ItemsControl ItemsSource="{Binding Rows}">
        <ItemsControl.ItemsPanel>
            <ItemsPanelTemplate>
                <WrapPanel />
            </ItemsPanelTemplate>
        </ItemsControl.ItemsPanel>
    </ItemsControl>
</DataTemplate>
```

也可以定义打印视图：

```xaml
<DataTemplate x:Key="ReportPrintTemplate" DataType="{x:Type reports:ReportPresenter}">
    <StackPanel>
        <TextBlock FontSize="20" FontWeight="Bold" Text="{Binding Title}" />
        <ItemsControl ItemsSource="{Binding Rows}" />
    </StackPanel>
</DataTemplate>
```

同一个 `ReportPresenter` 无需增加对 `DataGrid`、`WrapPanel`、打印控件的引用。

### 8.2 外部模块替换框架视图

适合场景：

- OEM 客户需要品牌化界面。
- 插件系统需要添加特定业务显示。
- 同一功能在桌面端、触摸端、工业屏有不同布局。
- 默认框架视图只作为基础实现，应用需要替换为定制视图。

外部模块只需：

1. 引用 Presenter 所在程序集。
2. 合并自己的资源字典。
3. 为目标类型声明同类型 `DataTemplate`。

---

## 9. 数据驱动 UI 的优势

### 9.1 状态与视图解耦

Presenter：

```csharp
public class LoginPresenter : BindableBase
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public ICommand LoginCommand { get; set; }
}
```

模板：

```xaml
<DataTemplate DataType="{x:Type local:LoginPresenter}">
    <StackPanel>
        <TextBox Text="{Binding UserName}" />
        <PasswordBox />
        <Button Command="{Binding LoginCommand}" Content="登录" />
    </StackPanel>
</DataTemplate>
```

业务对象只描述状态和操作；模板决定控件、布局、颜色和视觉效果。

### 9.2 减少 `if` / `switch` 创建视图代码

不推荐：

```csharp
if (presenter is TextBoxPresenter text)
    return new TextBox { Text = text.Text };
if (presenter is ImagePresenter image)
    return new Image { Source = image.ImageSource };
```

推荐：

```xaml
<ContentPresenter Content="{Binding CurrentPresenter}" />
```

并通过多个 `DataTemplate` 自动分派。

好处：

- 新增 Presenter 时不需要修改中央 `switch`。
- 更符合开闭原则。
- 模板可由外部程序集覆盖。
- XAML 设计器可独立设计 UI。

### 9.3 对集合同样适用

`ItemsControl` 中使用 `DataTemplate`：

```xaml
<ItemsControl ItemsSource="{Binding Items}">
    <ItemsControl.ItemTemplate>
        <DataTemplate DataType="{x:Type local:TaskItemPresenter}">
            <CheckBox Content="{Binding Title}" IsChecked="{Binding IsCompleted}" />
        </DataTemplate>
    </ItemsControl.ItemTemplate>
</ItemsControl>
```

当集合存在多种派生类型时，也可为每种类型定义隐式模板，WPF 自动按项目运行时类型选择。

---

## 10. 序列化保存的优势

### 10.1 数据对象比控件更容易保存

可序列化数据对象：

```csharp
public class DashboardLayout
{
    public string Title { get; set; }
    public int ColumnCount { get; set; }
    public List<WidgetDefinition> Widgets { get; set; } = new();
}
```

不应序列化 WPF 控件：

```csharp
public StackPanel DashboardPanel { get; set; }
public DataGrid ResultGrid { get; set; }
public Window EditorWindow { get; set; }
```

WPF 控件包含：

- 依赖属性和资源引用。
- 视觉树与逻辑树父级。
- Dispatcher 线程关联。
- 模板生成的临时状态。
- 事件处理器和非业务状态。

这些内容不适合持久化。

### 10.2 保存数据，加载后重新呈现

```csharp
var layout = new DashboardLayout
{
    Title = "生产看板",
    ColumnCount = 3
};

serializer.Save("dashboard.json", layout);
```

加载：

```csharp
DashboardLayout layout = serializer.Load<DashboardLayout>("dashboard.json");
CurrentPresenter = new DashboardPresenter(layout);
```

XAML：

```xaml
<ContentPresenter Content="{Binding CurrentPresenter}" />
```

`DataTemplate` 会根据当前 Presenter 重新建立 UI。持久化文件只保存业务意图，而不保存某一次运行的控件实例。

### 10.3 与 Project 模块结合

Project 模块中建议项目项保存项目数据 DTO 或 Presenter 的可序列化状态：

```csharp
protected override object GetSaveFileData()
{
    return new MyProjectData
    {
        Layout = this.DashboardPresenter.Layout,
        Filters = this.FilterPresenter.Filters.ToList()
    };
}
```

加载后重新创建 Presenter：

```csharp
public override bool Load(out string message)
{
    message = null;
    if (this.LoadFile<MyProjectData>(out var data) && data != null)
    {
        this.DashboardPresenter = new DashboardPresenter(data.Layout);
        this.FilterPresenter = new FilterPresenter(data.Filters);
    }
    return true;
}
```

不要保存实际的 `UserControl`、`ContentPresenter` 或 `DataGrid`。

---

## 11. 单元测试的优势

### 11.1 Presenter 可脱离 WPF UI 测试

```csharp
[Fact]
public void AddItem_AddsNewItem()
{
    var presenter = new OrderPresenter();

    presenter.AddItem("A-001", 2);

    Assert.Single(presenter.Items);
    Assert.Equal(2, presenter.Items[0].Quantity);
}
```

该测试不需要：

- 创建 `Window`。
- 启动 WPF Dispatcher。
- 查找 `TextBox` 或 `Button`。
- 依赖具体 `DataTemplate`。

### 11.2 模板独立做 UI 测试

模板需要验证时，可单独加载资源字典、实例化 `ContentPresenter` 并断言视觉树；但大部分业务规则测试应集中在 Presenter / Service 层。

### 11.3 更容易模拟外部服务

Presenter 依赖接口而不是控件：

```csharp
public class ExportPresenter
{
    private readonly IExportService _exportService;

    public ExportPresenter(IExportService exportService)
    {
        _exportService = exportService;
    }
}
```

测试中可传入假实现或 Mock；不需要测试通过按钮 Click 事件才能执行。

---

## 12. 与依赖注入和 Ioc 结合

框架支持通过 IOC 获取 Presenter，然后交给 `ContentPresenter`：

```xaml
<ContentPresenter Content="{Ioc Type={x:Type h:IThemeOptions}}" />
```

或：

```xaml
<ContentPresenter Content="{Ioc Type={x:Type h:ISwitchThemeViewPresenter}}" />
```

此时：

1. `Ioc` 根据注册返回对象。
2. `ContentPresenter` 承载返回对象。
3. WPF 根据对象实际类型选择 `DataTemplate`。
4. 模块资源字典提供默认模板。
5. 外部应用可通过资源覆盖模板。

注意：服务接口适合描述能力；`DataTemplate` 匹配通常依赖运行时具体类型或显式 `ContentTemplate`。如果仅对接口声明模板，需确认实际资源查找是否满足预期；对可替换视图，优先给具体 Presenter 类型或明确的视图模型基类定义模板。

---

## 13. 模板选择器：同类型多状态呈现

如果一个类型需要根据状态选择不同模板，可使用 `DataTemplateSelector`。

```csharp
public class OrderTemplateSelector : DataTemplateSelector
{
    public DataTemplate NormalTemplate { get; set; }
    public DataTemplate WarningTemplate { get; set; }

    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    {
        if (item is OrderPresenter order && order.IsOverdue)
            return this.WarningTemplate;
        return this.NormalTemplate;
    }
}
```

XAML：

```xaml
<Window.Resources>
    <DataTemplate x:Key="OrderNormalTemplate">
        <TextBlock Text="{Binding Number}" />
    </DataTemplate>

    <DataTemplate x:Key="OrderWarningTemplate">
        <TextBlock Foreground="Red" Text="{Binding Number}" />
    </DataTemplate>

    <local:OrderTemplateSelector
        x:Key="OrderTemplateSelector"
        NormalTemplate="{StaticResource OrderNormalTemplate}"
        WarningTemplate="{StaticResource OrderWarningTemplate}" />
</Window.Resources>

<ContentPresenter
    Content="{Binding CurrentOrder}"
    ContentTemplateSelector="{StaticResource OrderTemplateSelector}" />
```

适用场景：

- 正常、空、加载中、错误等状态不同。
- 只读、编辑、打印模式不同。
- 同一对象需要按权限显示不同 UI。

如果只是不同数据类型，优先使用隐式 `DataTemplate`，无需 Selector。

---

## 14. 资源覆盖优先级建议

建议按范围安排模板：

| 范围 | 用途 |
|---|---|
| 控件 / `ContentPresenter.Resources` | 单个区域临时或特殊呈现。 |
| `UserControl.Resources` / `Window.Resources` | 一个页面或窗口的局部定制。 |
| 模块资源字典 | 模块默认呈现。 |
| `Application.Resources` | 应用全局默认呈现或应用层覆盖。 |
| 主题资源字典 | 按主题替换视觉表现。 |

实践建议：

1. 框架模块提供合理的默认 `DataTemplate`。
2. 业务应用通过 `Application.Resources` 覆盖框架模板。
3. 特殊页面通过 `Window.Resources` 覆盖应用默认模板。
4. 不要为了修改布局而复制或修改 Presenter 业务逻辑。
5. 覆盖模板时保持必要的命令、验证和双向绑定。

---

## 15. 常见错误

### 15.1 没有找到模板，只显示类型名称

当 WPF 找不到匹配的 `DataTemplate` 时，可能显示对象的 `ToString()` 或类型名称。

检查：

1. `DataTemplate.DataType` 是否指向正确类型。
2. 对应资源字典是否已合并。
3. XAML 命名空间和程序集名是否正确。
4. 当前 `Content` 是否确实是预期 Presenter 类型。

### 15.2 用 `StaticResource` 固定了旧模板

如果要通过主题或资源字典动态替换 `ContentTemplate`，使用 `DynamicResource`：

```xaml
<ContentPresenter Content="{Binding CurrentPresenter}"
                  ContentTemplate="{DynamicResource CustomerDetailTemplate}" />
```

如果模板不需要运行时切换，`StaticResource` 更简单。

### 15.3 在 Presenter 中直接创建控件

不推荐：

```csharp
public FrameworkElement CreateView()
{
    return new TextBox { Text = this.Text };
}
```

问题：

- Presenter 依赖 WPF。
- 难以替换外观。
- 不易序列化。
- 不易单元测试。

推荐：提供数据和命令，由 `DataTemplate` 创建控件。

### 15.4 模板中的绑定没有更新数据

编辑控件使用：

```xaml
<TextBox Text="{Binding Name, UpdateSourceTrigger=PropertyChanged}" />
```

并确保 Presenter 实现属性通知。对于需要提交时更新的字段，可使用默认更新方式或明确设置 `UpdateSourceTrigger=LostFocus`。

### 15.5 覆盖模板后功能丢失

覆盖框架模板时，新的模板必须重新保留必要能力，例如：

- 命令绑定。
- 验证样式。
- 双向绑定。
- `ItemsSource` 和选择状态绑定。
- 主题资源引用。

不要只复制视觉元素而忽略行为绑定。

---

## 16. 二次开发建议

- 宿主控件只负责承载，优先放置 `ContentPresenter` 或 `ContentControl`。
- Presenter / ViewModel 只保存状态、命令和业务规则，不持有具体控件实例。
- 使用 `DataTemplate DataType="{x:Type ...}"` 定义默认呈现。
- 将模板放入资源字典，不要把所有视图逻辑写入 C# `switch`。
- 需要应用定制时，在应用资源字典重新定义同类型模板。
- 需要局部定制时，在窗口或控件资源中定义模板。
- 需要多种固定视图时使用带 `x:Key` 的显式模板。
- 需要按状态切换模板时使用 `DataTemplateSelector`。
- 保存项目、配置和布局时保存纯数据 DTO 或可序列化 Presenter 状态，不保存 WPF 视觉对象。
- 单元测试优先测试 Presenter、服务和命令逻辑；模板只做少量 UI 集成测试。

---

## 17. 总结

`ContentPresenter` + `DataTemplate` 的组合将“放在哪里显示”和“显示成什么样”分离：

```text
ContentPresenter：提供稳定占位和内容生命周期
DataTemplate：按类型呈现数据对象
ResourceDictionary：注册和覆盖呈现规则
Presenter / ViewModel：保存状态和业务行为
```

这种方式使框架可以提供默认 UI，同时允许应用或外部模块在不改动业务对象和宿主容器的前提下替换显示；并使状态更容易保存、复用和测试。
