# Diagram 图形、流程和连线控件二次开发文档

**适用项目：** `H.Controls.Diagram`、`H.Controls.Diagram.Presenter`、`H.Controls.Diagram.Presenters.Workflow`  
**核心类型：** `Diagram`、`Node`、`Link`、`Port`、`INodeData`、`ILinkData`、`IPortData`、`DiagramDataSource`  
**相关能力：** 图形编辑、流程图、节点拖放、端口连线、数据源转换、布局、缩放、序列化保存、流程执行

本文介绍 WPF-Control 中 Diagram 控件的使用方式，以及流程、节点、连线、端口的数据定义、呈现、交互和保存方式。

---

## 1. Diagram 控件定位

`Diagram` 是一个可编辑图形画布，用于构建：

- 工作流、审批流、状态机。
- 工业流程、设备拓扑、数据处理管道。
- 节点编辑器、规则编排、低代码页面。
- 组织关系、网络拓扑、模块依赖图。

核心结构：

```text
Diagram
├── NodeLayer       节点层
├── LinkLayer       已完成连线层
└── DynamicLayer    连线拖拽过程中的临时连线层

Node
├── Content         INodeData 节点数据
├── Ports           Port 集合
└── Links           入线、出线、关联线

Port
├── Content         IPortData 端口数据
├── Dock            停靠方向
└── PortType        输入、输出、双向

Link
├── Content         ILinkData 连线数据
├── FromNode / ToNode
└── FromPort / ToPort
```

`Diagram` 本身继承 `ContentControl`，通过控件模板创建 `NodeLayer`、`LinkLayer` 与 `DynamicLayer`。

---

## 2. 相关项目职责

| 项目 | 职责 |
|---|---|
| `H.Controls.Diagram` | 核心画布、节点、连线、端口、图层、布局、连线绘制器和交互行为。 |
| `H.Controls.Diagram.Presenter` | 可编辑 Diagram 数据模型、节点模板、流程执行、表达式和模板管理。 |
| `H.Controls.Diagram.Presenters.Workflow` | 工作流节点库，如开始、结束、判断、延时、文件、内容等节点。 |
| `H.Test.Diagram` | 基础 Diagram 控件、拖放节点和连线方式示例。 |

---

## 3. 核心对象与数据对象

Diagram 将运行时 UI 对象和可保存数据对象分离。

| 运行时对象 | 数据对象 | 作用 |
|---|---|---|
| `Node` | `INodeData` | 画布节点和节点业务数据。 |
| `Port` | `IPortData` | 节点端口和端口定义。 |
| `Link` | `ILinkData` | 连线 UI 和连线关系数据。 |
| `Diagram` | `IDiagramData` / 数据源 | 画布和整张图数据。 |

这种模式使保存、加载、单元测试不依赖 WPF 视觉对象。

---

## 4. `Diagram` 基础使用

### 4.1 XAML 创建画布

```xaml
xmlns:h="https://github.com/HeBianGu"
xmlns:b="http://schemas.microsoft.com/xaml/behaviors"

<h:Diagram
    x:Name="diagram"
    Width="1500"
    Height="1500"
    Background="Transparent"
    BorderBrush="{DynamicResource {x:Static h:BrushKeys.Blue}}"
    BorderThickness="2"
    Focusable="True"
    UseAnimation="False">

    <h:Diagram.Layout>
        <h:LocationLayout />
    </h:Diagram.Layout>

    <h:Diagram.LinkDrawer>
        <h:BrokenLinkDrawer />
    </h:Diagram.LinkDrawer>
</h:Diagram>
```

`H.Test.Diagram/MainWindow.xaml` 使用了同样的模式。

### 4.2 默认模板结构

`Diagram.xaml` 的默认模板：

```xaml
<ControlTemplate TargetType="{x:Type local:Diagram}">
    <AdornerDecorator>
        <Border Background="{TemplateBinding Background}">
            <Grid>
                <ly:NodeLayer x:Name="NodeLayer" />
                <ly:LinkLayer x:Name="LinkLayer" />
                <ly:LinkLayer x:Name="DynamicLayer" />
            </Grid>
        </Border>
    </AdornerDecorator>
</ControlTemplate>
```

三个图层职责：

| 图层 | 作用 |
|---|---|
| `NodeLayer` | 显示、移动和选择节点。 |
| `LinkLayer` | 显示已建立的连接。 |
| `DynamicLayer` | 用户拖动端口建立连接时显示临时虚线。 |

---

## 5. 节点定义：`INodeData`

`INodeData` 是节点持久化数据接口：

```csharp
public interface INodeData : IPartData
{
    string ID { get; set; }
    Point Location { get; set; }
    INodeData Create();
}
```

| 成员 | 说明 |
|---|---|
| `ID` | 节点唯一标识，连线通过它关联起始和终止节点。 |
| `Location` | 节点在 Diagram 坐标系中的位置。 |
| `Create()` | 创建当前节点类型的新实例，常用于从工具箱拖放新节点。 |

### 5.1 自定义节点数据

```csharp
public class TaskNodeData : BindableBase, INodeData, IPortableNodeData
{
    public string ID { get; set; } = Guid.NewGuid().ToString();

    public Point Location { get; set; }

    public string Title { get; set; } = "任务";

    public List<IPortData> PortDatas { get; set; } = new();

    public INodeData Create()
    {
        return new TaskNodeData
        {
            Title = this.Title,
            PortDatas = new List<IPortData>
            {
                new DefaultPortData { Dock = Dock.Left, PortType = PortType.Input, Name = "输入" },
                new DefaultPortData { Dock = Dock.Right, PortType = PortType.OutPut, Name = "输出" }
            }
        };
    }
}
```

建议：

- `ID` 使用 `Guid` 或业务唯一键。
- `Location` 必须保存，否则重新加载后节点位置丢失。
- `Create()` 应创建新 ID，不要直接返回当前实例。
- 节点业务属性，如名称、配置、执行参数，应放在节点数据对象中。

---

## 6. 节点呈现：`DataTemplate`

节点数据对象不直接创建 UI，而是通过 `DataTemplate` 呈现。示例：

```xaml
<ResourceDictionary
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:local="clr-namespace:MyApp.Diagram">

    <DataTemplate DataType="{x:Type local:TaskNodeData}">
        <Border
            MinWidth="120"
            Padding="12"
            Background="#FF2B579A"
            BorderBrush="#FF16365C"
            BorderThickness="1"
            CornerRadius="4">
            <TextBlock
                HorizontalAlignment="Center"
                Foreground="White"
                Text="{Binding Title}" />
        </Border>
    </DataTemplate>
</ResourceDictionary>
```

Diagram 中的 `Node.Content` 是 `INodeData`。WPF 根据 `Node.Content` 的实际类型选中对应 `DataTemplate`。

这意味着：

- 节点数据负责业务状态。
- 模板负责节点外观。
- 外部应用可通过资源字典覆盖节点外观。
- 同一个节点数据可以在编辑、预览、打印模式采用不同模板。

---

## 7. 端口定义：`IPortData`

端口是节点可连线的位置。接口定义：

```csharp
public interface IPortData : ILinkInitializer, IData
{
    string ID { get; set; }
    string NodeID { get; set; }
    string Name { get; set; }
    Dock Dock { get; set; }
    PortType PortType { get; set; }
    Thickness PortMargin { get; set; }
}
```

| 成员 | 说明 |
|---|---|
| `ID` | 端口唯一标识，连线通过它定位来源和目标端口。 |
| `NodeID` | 所属节点 ID。 |
| `Name` | 端口名称，如“输入”“成功”“失败”。 |
| `Dock` | 端口停靠方向：`Left`、`Top`、`Right`、`Bottom`。 |
| `PortType` | 输入、输出或双向端口。 |
| `PortMargin` | 调整端口在节点边缘的偏移。 |
| `InitLink` | 建立连线后初始化 `Link`。 |

### 7.1 `PortType`

```csharp
[Flags]
public enum PortType
{
    Input = 1,
    OutPut = 2,
    Both = Input | OutPut
}
```

| 类型 | 作用 |
|---|---|
| `Input` | 作为连线目标。 |
| `OutPut` | 作为连线来源。 |
| `Both` | 可作为来源和目标。 |

> `OutPut` 是框架现有枚举名称。使用时必须保持该拼写。

### 7.2 使用默认端口数据

```csharp
var input = new DefaultPortData
{
    NodeID = node.ID,
    Name = "输入",
    Dock = Dock.Left,
    PortType = PortType.Input
};

var output = new DefaultPortData
{
    NodeID = node.ID,
    Name = "输出",
    Dock = Dock.Right,
    PortType = PortType.OutPut
};
```

`DefaultPortData` 提供了默认 ID、位置和类型属性；业务场景通常需要派生或实现自定义端口数据，以实现连接验证和连线初始化。

---

## 8. 支持端口的节点：`IPortableNodeData`

节点要拥有端口，应实现：

```csharp
public interface IPortableNodeData : ILinkDataCreator
{
    List<IPortData> PortDatas { get; set; }
}
```

数据源创建运行时节点时，会读取 `PortDatas`：

```csharp
if (unit is IPortableNodeData portData)
{
    foreach (IPortData pd in portData.PortDatas)
    {
        Port port = Port.Create(node);
        port.Content = pd;
        port.Dock = pd.Dock;
        port.PortType = pd.PortType;
        port.Margin = pd.PortMargin;
        node.AddPort(port);
    }
}
```

因此，端口应作为节点数据的一部分保存：

```csharp
public class DecisionNodeData : BindableBase, INodeData, IPortableNodeData
{
    public string ID { get; set; } = Guid.NewGuid().ToString();
    public Point Location { get; set; }
    public List<IPortData> PortDatas { get; set; } = new()
    {
        new DefaultPortData { Name = "输入", Dock = Dock.Left, PortType = PortType.Input },
        new DefaultPortData { Name = "是", Dock = Dock.Right, PortType = PortType.OutPut },
        new DefaultPortData { Name = "否", Dock = Dock.Bottom, PortType = PortType.OutPut }
    };

    public INodeData Create() => new DecisionNodeData();
}
```

新建节点后，建议将端口的 `NodeID` 更新为新节点 ID。

---

## 9. 连线定义：`ILinkData`

连线数据接口：

```csharp
public interface ILinkData : IPartData
{
    string FromNodeID { get; set; }
    string ToNodeID { get; set; }
    string FromPortID { get; set; }
    string ToPortID { get; set; }
}
```

连线保存的是关系，而非 WPF `Link` 控件实例：

```text
来源节点 ID + 来源端口 ID
        ↓
目标节点 ID + 目标端口 ID
```

### 9.1 使用默认连线数据

```csharp
public class DefaultLinkData : BindableBase, ILinkData
{
    public string Message { get; set; }
    public string FromNodeID { get; set; }
    public string ToNodeID { get; set; }
    public string FromPortID { get; set; }
    public string ToPortID { get; set; }
}
```

`Message` 可用于保存连线标签、条件说明或业务描述。

### 9.2 自定义连线数据

```csharp
public class WorkflowLinkData : DefaultLinkData
{
    public string Condition { get; set; }
    public int Priority { get; set; }
    public bool IsDefaultPath { get; set; }
}
```

可用于：

- 条件分支，如 `审核通过` / `审核拒绝`。
- 流程优先级。
- 数据映射规则。
- 执行状态或错误信息。

---

## 10. 数据源：`DiagramDataSource`

`DiagramDataSource` 是数据对象和运行时图形对象的转换器：

```csharp
public class DiagramDataSource : GraphSource<INodeData, ILinkData>, IDiagramDataSource
{
    public DiagramDataSource(IEnumerable<INodeData> nodes, IEnumerable<ILinkData> links)
        : base(nodes, links)
    {
    }
}
```

加载流程：

```text
List<INodeData> + List<ILinkData>
    ↓
DiagramDataSource
    ↓
INodeData → Node
IPortData → Port
ILinkData → Link
    ↓
Diagram 显示图形
```

保存流程：

```text
Diagram 中的 Node / Port / Link
    ↓
DiagramDataSource.GetNodeDatas()
DiagramDataSource.GetLinkDatas()
    ↓
更新节点位置、端口集合、节点和端口关联 ID
    ↓
序列化节点数据和连线数据
```

### 10.1 加载 Diagram 数据

```csharp
var nodes = new List<INodeData>
{
    new TaskNodeData
    {
        ID = "start",
        Location = new Point(100, 100),
        Title = "开始"
    },
    new TaskNodeData
    {
        ID = "end",
        Location = new Point(400, 100),
        Title = "结束"
    }
};

var links = new List<ILinkData>();

diagram.DataSource = new DiagramDataSource(nodes, links);
```

### 10.2 运行时对象转换规则

`DiagramDataSource.ConvertToNode()` 会：

1. 创建 `Node`。
2. 将节点数据设置到 `Node.Content`。
3. 设置 `Node.Location`。
4. 若节点实现 `IPortableNodeData`，逐个创建 `Port`。

`ConvertToLink()` 会：

1. 根据 `FromNodeID` 和 `ToNodeID` 找到节点。
2. 根据 `FromPortID` 和 `ToPortID` 找到端口。
3. 调用 `fromNode.CreateLinkTo(...)` 创建运行时 `Link`。
4. 将 `ILinkData` 设为 `Link.Content`。

因此，加载连线前必须确保：

- 节点 ID 唯一。
- 端口 ID 唯一。
- `ILinkData` 中所有关联 ID 都存在。

---

## 11. 保存 Diagram 数据

从数据源导出：

```csharp
List<INodeData> nodes = diagram.DataSource.GetNodeDatas();
List<ILinkData> links = diagram.DataSource.GetLinkDatas();
```

`GetNodeDatas()` 会同步：

- `Node.Location` 到 `INodeData.Location`。
- 运行时 `Port.Content` 到 `IPortableNodeData.PortDatas`。

`GetLinkDatas()` 会同步：

```csharp
wire.FromNodeID = link.FromNode?.GetContent<INodeData>()?.ID;
wire.ToNodeID = link.ToNode?.GetContent<INodeData>()?.ID;
wire.FromPortID = link.FromPort?.GetContent<IPortData>()?.ID;
wire.ToPortID = link.ToPort?.GetContent<IPortData>()?.ID;
```

建议保存 DTO：

```csharp
public class WorkflowDiagramFile
{
    public List<INodeData> Nodes { get; set; }
    public List<ILinkData> Links { get; set; }
}
```

注意：接口集合通常需要配置多态序列化，或保存为明确的节点/连线基类集合并记录类型信息。不要直接序列化 `Diagram`、`Node`、`Port`、`Link` 等 WPF UI 对象。

---

## 12. 端口连线初始化和验证

`IPortData` 继承 `ILinkInitializer`：

```csharp
public interface ILinkInitializer
{
    void InitLink(Link link);
}
```

端口可以在连线创建后设置连线内容和样式：

```csharp
public class WorkflowPortData : DefaultPortData
{
    public override void InitLink(Link link)
    {
        link.Content = new WorkflowLinkData
        {
            Message = this.Name
        };
    }
}
```

`DefaultPortData` 还提供：

```csharp
public bool CanDrop(Part part, out string message);
public ILinkData CreateLinkData();
```

默认实现不允许直接落放、也不创建连线数据。业务端口应根据需求实现：

- 输出端口不能连接到输出端口。
- 一个输入端口是否只允许一条入线。
- 节点是否允许连接自身。
- 两个节点类型是否允许连接。
- 连线建立时创建什么 `ILinkData`。

示例规则：

```csharp
public class InputPortData : DefaultPortData
{
    public InputPortData()
    {
        this.PortType = PortType.Input;
    }

    public bool CanDrop(Part part, out string message)
    {
        if (part is not Port source || source.PortType == PortType.Input)
        {
            message = "输入端口只能连接输出端口";
            return false;
        }

        message = null;
        return true;
    }

    public ILinkData CreateLinkData()
    {
        return new WorkflowLinkData();
    }
}
```

---

## 13. 创建节点和连线

### 13.1 通过数据源创建

推荐加载或初始化时使用数据源：

```csharp
diagram.DataSource = new DiagramDataSource(nodes, links);
```

### 13.2 直接添加运行时节点

`IDiagram` 提供：

```csharp
void AddNode(params Node[] nodes);
void AddLink(Link link);
void RemoveNode(params Node[] nodes);
```

直接操作适合临时交互、调试或高级扩展；业务数据仍应同步回 `DataSource` 后保存。

### 13.3 从工具箱拖放创建节点

`H.Test.Diagram` 的工具箱使用：

```xaml
<ItemsControl.ItemTemplate>
    <DataTemplate>
        <ContentPresenter Content="{Binding}">
            <b:Interaction.Behaviors>
                <DraggableDataTemplateAdornerBehavior />
            </b:Interaction.Behaviors>
        </ContentPresenter>
    </DataTemplate>
</ItemsControl.ItemTemplate>
```

Diagram 使用类型拖放行为：

```xaml
<h:Diagram>
    <b:Interaction.Behaviors>
        <h:DiagramDropTypeNodeDataBehavior NodeType="{Binding NodeType}" />
    </b:Interaction.Behaviors>
</h:Diagram>
```

流程：

```text
工具箱中的节点模板数据
    ↓
DraggableDataTemplateAdornerBehavior 开始拖拽
    ↓
DiagramDropTypeNodeDataBehavior 接收 Drop
    ↓
调用节点数据 Create()
    ↓
设置新节点 Location
    ↓
加入 Diagram 和 DataSource
```

这使工具箱只提供可创建的节点数据，不直接持有画布节点 UI。

---

## 14. 连线绘制方式

`Diagram.LinkDrawer` 控制连线几何形状。测试示例包含：

```xaml
<ComboBox SelectedItem="{Binding ElementName=diagram, Path=LinkDrawer}">
    <h:ArcLinkDrawer />
    <h:BezierLinkDrawer />
    <h:LineLinkDrawer />
    <h:BrokenLinkDrawer />
</ComboBox>
```

| 绘制器 | 说明 |
|---|---|
| `LineLinkDrawer` | 直线连接，适合简单拓扑。 |
| `BrokenLinkDrawer` | 折线连接，适合流程图和矩形节点。 |
| `BezierLinkDrawer` | 贝塞尔曲线，适合视觉柔和的关系图。 |
| `ArcLinkDrawer` | 弧线连接，适合避免直线重叠的场景。 |

```xaml
<h:Diagram.LinkDrawer>
    <h:BrokenLinkDrawer />
</h:Diagram.LinkDrawer>
```

实际选择建议：

- 审批流、工业流程：`BrokenLinkDrawer`。
- 思维导图、关系图：`BezierLinkDrawer` 或 `ArcLinkDrawer`。
- 网络拓扑、小规模简单图：`LineLinkDrawer`。

---

## 15. 节点布局

`Diagram.Layout` 控制节点布局策略：

```xaml
<h:Diagram.Layout>
    <h:LocationLayout />
</h:Diagram.Layout>
```

`LocationLayout` 使用节点的 `Location`，适合保存并恢复用户手动排版后的坐标。

Diagram 还提供自动布局命令：

```xaml
<Button
    Command="{x:Static h:DiagramCommands.Aligment}"
    CommandTarget="{Binding ElementName=diagram}"
    Content="自动布局" />
```

> `Aligment` 是框架现有命令名称，使用时保持该拼写。

选择布局策略时：

- 需要保存用户拖动结果：使用 `LocationLayout`。
- 需要自动排布：调用自动布局命令或选择合适布局实现。
- 自动布局后应导出节点数据，以保存新的 `Location`。

---

## 16. 缩放和视图适配

`IDiagram` 提供：

```csharp
void ZoomTo(Point point);
void ZoomTo(Rect rect, double scale);
void ZoomToFit(double scale);
void ZoomToFit(double scale, params Part[] parts);
```

常用命令：

```xaml
<Button
    Command="{x:Static h:DiagramCommands.ZoomToFit}"
    CommandTarget="{Binding ElementName=diagram}"
    Content="适配画布" />
```

测试项目还把 Diagram 放到 `Zoombox` 中：

```xaml
<h:Zoombox ZoomOn="Content">
    <h:Diagram x:Name="diagram" />
</h:Zoombox>
```

适用方式：

- Diagram 内部命令用于整体内容适配。
- `Zoombox` 用于鼠标滚轮、平移、视图栈等更完整的缩放交互。

---

## 17. 内置命令和快捷键

Diagram 内置了一组命令绑定。

| 命令 | 默认交互 | 说明 |
|---|---|---|
| `DiagramCommands.DeleteSelected` | `Delete` | 删除选中节点和连线。 |
| `DiagramCommands.SelectAll` | `Ctrl+A` | 全选或取消全选节点。 |
| `DiagramCommands.ZoomToFit` | 左键双击 | 将内容缩放到可见区域。 |
| `DiagramCommands.Aligment` | 工具栏触发 | 自动对齐/布局节点。 |
| `DiagramCommands.Next` | `Tab` | 选择下一个节点。 |
| `DiagramCommands.Previous` | 工具栏触发 | 选择前一个节点。 |
| `DiagramCommands.MoveLeft` | `Left` | 向左移动选中节点。 |
| `DiagramCommands.MoveRight` | `Right` | 向右移动选中节点。 |
| `DiagramCommands.MoveUp` | `Up` | 向上移动选中节点。 |
| `DiagramCommands.MoveDown` | `Down` | 向下移动选中节点。 |
| `DiagramCommands.Clear` | 工具栏触发 | 清空 Diagram 内容。 |

工具栏示例：

```xaml
<StackPanel Orientation="Horizontal">
    <Button Command="{x:Static h:DiagramCommands.Clear}"
            CommandTarget="{Binding ElementName=diagram}"
            Content="清空" />
    <Button Command="{x:Static h:DiagramCommands.DeleteSelected}"
            CommandTarget="{Binding ElementName=diagram}"
            Content="删除" />
    <Button Command="{x:Static h:DiagramCommands.SelectAll}"
            CommandTarget="{Binding ElementName=diagram}"
            Content="全选" />
    <Button Command="{x:Static h:DiagramCommands.ZoomToFit}"
            CommandTarget="{Binding ElementName=diagram}"
            Content="适配" />
</StackPanel>
```

---

## 18. 监听编辑状态变化

Diagram 提供 `ItemsChanged` 与 `SelectedPartChanged` 事件。可通过 XAML Behaviors 转为 ViewModel 命令：

```xaml
<b:Interaction.Triggers>
    <b:EventTrigger EventName="ItemsChanged">
        <b:InvokeCommandAction
            Command="{Binding ItemsChangedCommand}"
            PassEventArgsToCommand="True" />
    </b:EventTrigger>

    <b:EventTrigger EventName="SelectedPartChanged">
        <b:InvokeCommandAction
            Command="{Binding SelectedPartChangedCommand}"
            PassEventArgsToCommand="True" />
    </b:EventTrigger>
</b:Interaction.Triggers>
```

适用场景：

- 节点或连线变化后标记项目为“未保存”。
- 节点选中后在属性面板显示配置项。
- 删除节点后同步业务校验。
- 自动保存、撤销重做和审计日志。

示例：

```csharp
public RelayCommand ItemsChangedCommand => new RelayCommand(_ =>
{
    this.IsDirty = true;
});
```

---

## 19. 流程图建模流程

建议按以下流程实现可保存流程图：

```text
1. 定义节点数据类型 INodeData
2. 为节点定义 DataTemplate
3. 为可连接节点实现 IPortableNodeData
4. 定义输入、输出端口 IPortData
5. 定义连线数据 ILinkData
6. 使用 DiagramDataSource 加载节点和连线
7. 用户拖放、移动、连接、删除
8. 导出 GetNodeDatas / GetLinkDatas
9. 序列化为项目文件或数据库记录
10. 下次加载后重新生成 Diagram
```

推荐持久化结构：

```csharp
public class WorkflowDefinition
{
    public string Name { get; set; }
    public List<WorkflowNodeData> Nodes { get; set; } = new();
    public List<WorkflowLinkData> Links { get; set; } = new();
}
```

---

## 20. 与 ContentPresenter / DataTemplate 模式结合

Diagram 节点可以采用框架的“数据驱动 UI”模式：

```text
Node.Content = INodeData
    ↓
ContentPresenter
    ↓
按 INodeData 实际类型匹配 DataTemplate
    ↓
生成节点视觉 UI
```

例如：

```xaml
<DataTemplate DataType="{x:Type local:StartNodeData}">
    <Ellipse Width="80" Height="40" Fill="Green">
        <TextBlock HorizontalAlignment="Center" VerticalAlignment="Center" Text="开始" />
    </Ellipse>
</DataTemplate>

<DataTemplate DataType="{x:Type local:DecisionNodeData}">
    <Polygon Points="50,0 100,30 50,60 0,30" Fill="Orange" />
</DataTemplate>
```

好处：

- 节点数据可序列化保存。
- 外部应用可覆盖节点模板而不修改流程模型。
- 不同节点类型自动采用不同视觉样式。
- 单元测试只测试节点、端口和连线业务规则。
- 主题可通过资源字典替换节点外观。

---

## 21. 常见问题

### 节点显示为类型名称

检查：

1. 是否定义了对应节点类型的 `DataTemplate`。
2. 资源字典是否已合并。
3. `Node.Content` 是否正确设置为节点数据对象。
4. XAML 命名空间是否正确。

### 连线加载后丢失

检查：

1. `FromNodeID`、`ToNodeID` 是否存在对应节点。
2. `FromPortID`、`ToPortID` 是否存在对应端口。
3. 节点是否实现 `IPortableNodeData` 并保存了 `PortDatas`。
4. 端口 ID 是否在重新加载时保持稳定。

### 节点位置没有保存

保存前调用：

```csharp
List<INodeData> nodes = diagram.DataSource.GetNodeDatas();
```

该方法会把运行时 `Node.Location` 回写到 `INodeData.Location`。

### 端口不能连接

检查：

1. `PortType` 是否匹配输入/输出规则。
2. 自定义 `CanDrop` 是否返回 `true`。
3. `CreateLinkData()` 是否返回有效的 `ILinkData`。
4. `InitLink` 是否正确初始化连线内容。

### 删除节点后仍保留业务连线

删除后应从 Diagram 数据源导出最新 `GetLinkDatas()`，再保存业务数据。不要只保存旧的连线集合。

---

## 22. 二次开发建议

- 节点、端口、连线都使用独立数据对象，不保存 WPF 控件。
- 节点、端口和连线 ID 必须唯一且稳定。
- 每种业务节点使用独立 `INodeData` 类型和 `DataTemplate`。
- 使用 `IPortableNodeData.PortDatas` 保存端口定义。
- 连接规则放到端口数据或业务校验服务，不放在 UI 事件中。
- 连线条件、优先级和标签放到自定义 `ILinkData`。
- 项目保存前统一通过 `GetNodeDatas()` 和 `GetLinkDatas()` 导出数据。
- 用户手动排版的流程建议使用 `LocationLayout` 并保存 `Location`。
- 节点工具箱使用拖放行为，避免直接把同一个节点实例加入多个画布。
- 流程执行逻辑与 Diagram UI 分离，Diagram 只负责编辑和显示流程定义。
