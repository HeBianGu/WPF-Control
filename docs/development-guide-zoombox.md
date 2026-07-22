# Zoombox 控件二次开发文档

**适用项目：** `H.Controls.ZoomBox`、`H.Controls.ZoomBox.Extension`  
**核心类型：** `Zoombox`、`ZoomboxView`、`ZoomboxViewStack`、`ZoomboxViewFinderDisplay`、`IZoombox`  
**相关能力：** 内容缩放、平移、区域缩放、适应窗口、填充窗口、视图历史、定位器、图片预览

本文介绍框架中 Zoombox 控件的基础使用、鼠标键盘交互、视图和历史栈、公开命令、扩展行为，以及图片预览对话框的开发方式。

> 控件的实际类型名是 `Zoombox`，不是 `ZoomBox`。命名空间为 `H.Controls.ZoomBox`。

---

## 1. 控件定位

`Zoombox` 继承 `ContentControl`，用于显示一个可缩放、可平移的 `UIElement`：

```text
Zoombox
    ↓ Content
Image / Canvas / Grid / Diagram / 自定义 FrameworkElement
```

典型场景：

- 图片查看器。
- 流程图、拓扑图和 Diagram 画布。
- 大尺寸设计图或地图。
- 报表、文档和可视化预览。
- 需要“适应窗口、实际尺寸、局部放大”的内容区域。

核心能力：

- 按比例放大和缩小。
- 围绕控件中心或鼠标位置缩放。
- 鼠标拖动平移。
- 中键拖动平移。
- 框选区域并放大。
- 显示当前视口定位器。
- 保存和前后导航历史视图。
- 使用动画切换视图。
- 通过公开方法、路由命令和 MVVM 行为控制。

---

## 2. 项目引用

基础控件：

```text
H.Controls.ZoomBox
```

如果需要自动适应行为、图片 Presenter 或对话框扩展，还需引用：

```text
H.Controls.ZoomBox.Extension
```

控件目标为 WPF，应用应使用 Windows 桌面目标框架，例如：

```xml
<TargetFramework>net8.0-windows</TargetFramework>
<UseWPF>true</UseWPF>
```

---

## 3. XAML 命名空间

框架统一 XAML 命名空间：

```xaml
xmlns:h="https://github.com/HeBianGu"
```

基础使用：

```xaml
<h:Zoombox>
    <Image Source="Assets/sample.jpg" />
</h:Zoombox>
```

`Zoombox.Content` 必须是 `UIElement`。运行时设置普通字符串或非 UI 对象会抛出 `InvalidContentException`。

---

## 4. 图片查看器快速示例

```xaml
<h:Zoombox
    x:Name="ImageZoombox"
    MinScale="0.1"
    MaxScale="20"
    ZoomPercentage="10"
    UseMiddleButtonDrag="True"
    ViewFinderVisibility="Visible">
    <Image
        RenderOptions.BitmapScalingMode="HighQuality"
        Source="{Binding ImageSource}" />
</h:Zoombox>
```

代码中适应窗口：

```csharp
ImageZoombox.FitToBounds();
```

恢复 100%：

```csharp
ImageZoombox.ZoomTo(1.0);
```

放大 10%：

```csharp
ImageZoombox.Zoom(0.1);
```

缩小 10%：

```csharp
ImageZoombox.Zoom(-0.1);
```

注意：

- `Scale = 1.0` 表示 100%。
- `Zoom(double percentage)` 接收相对比例，`0.1` 表示在当前比例上增加 10%。
- `ZoomTo(double scale)` 接收绝对缩放倍数。

---

## 5. Canvas 和设计画布

```xaml
<h:Zoombox
    x:Name="CanvasZoombox"
    AutoWrapContentWithViewbox="False"
    KeepContentInBounds="False"
    MinScale="0.05"
    MaxScale="50"
    UseMiddleButtonDrag="True">
    <Canvas Width="2000" Height="1200" Background="White">
        <Rectangle
            Canvas.Left="100"
            Canvas.Top="100"
            Width="300"
            Height="180"
            Fill="LightBlue" />
        <TextBlock
            Canvas.Left="150"
            Canvas.Top="160"
            FontSize="32"
            Text="可缩放画布" />
    </Canvas>
</h:Zoombox>
```

对于自身具有明确坐标系和尺寸的 `Canvas`、Diagram 或设计器，通常可设置：

```xaml
AutoWrapContentWithViewbox="False"
```

是否关闭自动包装应根据实际内容测量结果决定。

---

## 6. 自动包装内容

`AutoWrapContentWithViewbox` 默认值为 `true`。

启用后，`Zoombox` 会在真实内容外创建一个内部 `Viewbox`：

```text
Zoombox
    ↓
内部 Viewbox
    ↓
真实 Content
```

作用：

- 统一内容测量和缩放。
- 使普通图片、Grid 等内容更容易参与视图计算。
- 监听真实 `FrameworkElement` 的尺寸变化。

```xaml
<h:Zoombox AutoWrapContentWithViewbox="True">
    <Image Source="{Binding ImageSource}" />
</h:Zoombox>
```

不希望增加内部 `Viewbox` 时：

```xaml
<h:Zoombox AutoWrapContentWithViewbox="False">
    <Canvas Width="2000" Height="1200" />
</h:Zoombox>
```

切换该属性会重新强制转换 `Content`。不要在频繁交互过程中反复切换。

---

## 7. 缩放属性

| 属性 | 默认值 | 说明 |
|---|---:|---|
| `Scale` | `NaN` | 当前绝对缩放倍数；初始化后由当前视图更新。 |
| `MinScale` | 控件代码 `0.01`，默认样式 `0.1` | 最小缩放倍数。应用默认样式后通常为 10%。 |
| `MaxScale` | `100` | 最大缩放倍数。 |
| `ZoomPercentage` | `5` | 每次命令或滚轮缩放的百分比步长。 |
| `ZoomOrigin` | `0.5,0.5` | 普通缩放中心的相对坐标，中心点为 `(0.5, 0.5)`。 |
| `ZoomOn` | `Content` | 缩放坐标参照内容还是视图。 |
| `ZoomOnPreview` | `true` | 在 Preview 鼠标滚轮阶段处理缩放。 |
| `IsAnimated` | `true` | 视图变化是否使用动画。 |
| `AnimationDuration` | `300 ms` | 动画持续时间。 |
| `AnimationAccelerationRatio` | `0` | 动画加速比例，范围 `0～1`。 |
| `AnimationDecelerationRatio` | `0` | 动画减速比例，范围 `0～1`。 |

### 7.1 设置缩放范围

```xaml
<h:Zoombox
    MinScale="0.2"
    MaxScale="8"
    ZoomPercentage="10" />
```

表示：

- 最小 20%。
- 最大 800%。
- 每次放大/缩小步长为 10%。

`Scale` 会被限制在 `MinScale` 与 `MaxScale` 之间。如果 `MinScale` 大于 `MaxScale`，依赖属性回调会进行强制调整。

### 7.2 关闭动画

适用于高频更新或大内容：

```xaml
<h:Zoombox IsAnimated="False" />
```

自定义动画：

```xaml
<h:Zoombox
    IsAnimated="True"
    AnimationDuration="0:0:0.2"
    AnimationAccelerationRatio="0.2"
    AnimationDecelerationRatio="0.8" />
```

加速比和减速比必须位于 `0～1`。

---

## 8. `Fit`、`Fill` 和 `Center`

### 8.1 `FitToBounds()`

在保持宽高比的前提下，让全部内容都显示在控件范围内：

```csharp
zoombox.FitToBounds();
```

特点：

- 内容不会被裁掉。
- 控件可能出现空白区域。
- 最适合图片、文档和全图预览。

### 8.2 `FillToBounds()`

保持宽高比并填满控件：

```csharp
zoombox.FillToBounds();
```

特点：

- 控件范围被内容填满。
- 内容边缘可能超出视口。
- 适合封面和背景预览。

### 8.3 `CenterContent()`

保持当前缩放比例，把内容重新居中：

```csharp
zoombox.CenterContent();
```

### 8.4 对比

| 操作 | 改变比例 | 保证完整显示 | 可能裁切 |
|---|---|---|---|
| `FitToBounds()` | 是 | 是 | 否 |
| `FillToBounds()` | 是 | 否 | 是 |
| `CenterContent()` | 通常不改变 | 不保证 | 取决于当前比例 |
| `ZoomTo(1.0)` | 设置为 100% | 不保证 | 取决于内容尺寸 |

---

## 9. 鼠标交互与修饰键

默认样式配置：

```xaml
DragModifiers="Ctrl,Exact"
ZoomModifiers="Shift,Exact"
RelativeZoomModifiers="Ctrl,Alt,Exact"
ZoomToSelectionModifiers="Alt,Exact"
```

| 操作 | 默认交互 |
|---|---|
| 拖动内容 | 按住 `Ctrl`，使用鼠标左键拖动。 |
| 普通滚轮缩放 | 按住 `Shift`，滚动滚轮。 |
| 围绕鼠标位置缩放 | 按住 `Ctrl + Alt`，滚动滚轮。 |
| 框选区域放大 | 按住 `Alt`，鼠标左键框选。 |
| 中键拖动 | 设置 `UseMiddleButtonDrag=True` 后，按住鼠标中键拖动。 |

`Exact` 表示修饰键必须精确匹配。例如 `Ctrl,Exact` 要求只有 Ctrl 处于按下状态，额外按下 Shift 会导致不匹配。

### 9.1 无修饰键滚轮缩放

空的 `KeyModifierCollection` 始终处于激活状态：

```xaml
<h:Zoombox
    ZoomModifiers=""
    RelativeZoomModifiers="Blocked" />
```

这样滚轮直接执行普通缩放，并禁用相对鼠标位置缩放。

如果希望滚轮围绕鼠标位置缩放：

```xaml
<h:Zoombox
    ZoomModifiers="Blocked"
    RelativeZoomModifiers="" />
```

不要同时把两个集合都设置为空。两者同时激活时，控件会优先采用相对缩放。

### 9.2 无修饰键左键拖动

```xaml
<h:Zoombox
    DragModifiers=""
    ZoomToSelectionModifiers="Blocked" />
```

需要内容内部接收左键操作时，不建议启用无修饰键拖动，否则可能与节点选择、按钮点击或绘图行为冲突。设计器通常使用中键拖动更合适：

```xaml
<h:Zoombox
    DragModifiers="Blocked"
    UseMiddleButtonDrag="True" />
```

### 9.3 `DragOnPreview` 和 `ZoomOnPreview`

```xaml
<h:Zoombox
    DragOnPreview="True"
    ZoomOnPreview="True" />
```

- `true`：在 Preview/隧道路由阶段处理，更容易优先于子元素获得输入。
- `false`：在普通冒泡阶段处理，子元素可以先处理事件。

包含交互控件的画布应谨慎使用 Preview 模式。

---

## 10. 修饰键类型

`KeyModifierCollection` 支持：

```text
Alt
LeftAlt
RightAlt
Ctrl
LeftCtrl
RightCtrl
Shift
LeftShift
RightShift
None
Exact
Blocked
```

含义：

| 值 | 说明 |
|---|---|
| `Ctrl`、`Alt`、`Shift` | 左右任意对应按键均可。 |
| `Left*`、`Right*` | 只接受指定侧按键。 |
| `None` | 不要求修饰键。与 `Exact` 配合可要求没有任何修饰键。 |
| `Exact` | 当前按下修饰键必须与集合精确一致。 |
| `Blocked` | 永远禁止该操作。 |
| 空集合 | 始终激活。 |

示例：

```xaml
<h:Zoombox
    DragModifiers="Ctrl,Shift,Exact"
    ZoomModifiers="None,Exact"
    ZoomToSelectionModifiers="Alt,Exact" />
```

---

## 11. 中键拖动

框架增加了 `UseMiddleButtonDrag`：

```xaml
<h:Zoombox UseMiddleButtonDrag="True" />
```

默认值：

```text
false
```

开启后：

1. 中键按下时进入 `IsDraggingContent=True`。
2. 捕获鼠标。
3. 鼠标移动时根据当前 `Scale` 换算平移量。
4. 中键释放时结束拖动并释放捕获。

该方式特别适合 Diagram、Canvas 和图像标注工具，因为不会占用普通左键选择。

---

## 12. 区域缩放

默认按住 `Alt` 并使用左键框选：

```xaml
<h:Zoombox ZoomToSelectionModifiers="Alt,Exact" />
```

代码直接缩放到内容坐标区域：

```csharp
zoombox.ZoomTo(new Rect(100, 80, 400, 300));
```

如果矩形宽度或高度为 `0`，控件会调整为 `1`，避免无效缩放区域。

适用场景：

- 地图局部查看。
- 图片缺陷区域查看。
- Diagram 节点组定位。
- 图表数据区间放大。

`Rect` 使用内容坐标，而不是屏幕坐标。将鼠标框选区域传入前，需要确认坐标转换关系。

---

## 13. 平移和位置

属性：

| 属性 | 说明 |
|---|---|
| `Position` | 当前平移位置。改变它会切换到对应绝对视图。 |
| `PanDistance` | `PanLeft`、`PanRight`、`PanUp`、`PanDown` 每次移动距离，默认 `5`。 |
| `KeepContentInBounds` | 是否限制内容，避免被完全拖出可视范围。 |
| `IsDraggingContent` | 只读，当前是否正在拖动。 |
| `IsSelectingRegion` | 只读，当前是否正在框选缩放区域。 |

```xaml
<h:Zoombox
    KeepContentInBounds="True"
    PanDistance="20" />
```

代码移动：

```csharp
zoombox.Position = new Point(100, 50);
```

如果只需要相对平移，优先使用公开命令或基于当前 `Position` 计算新位置。

---

## 14. 滚动条

启用：

```xaml
<h:Zoombox IsUsingScrollBars="True" />
```

默认模板包含：

```text
PART_VerticalScrollBar
PART_HorizontalScrollBar
```

`IsUsingScrollBars=True` 时模板把两个滚动条设为可见。自定义控件模板必须保留同名模板部件，否则滚动条同步功能无法正常工作。

滚动事件：

```csharp
zoombox.Scroll += (_, e) =>
{
    // e.ScrollEventType、e.NewValue
};
```

---

## 15. 定位器 ViewFinder

定位器显示：

- 内容全貌缩略图。
- 当前可见区域。
- 前进、后退、主页、Fit、Fill、Center 按钮。

主要属性：

| 属性 | 说明 |
|---|---|
| `ViewFinderVisibility` | 定位器显示状态，默认样式设为 `Collapsed`。 |
| `UseShowViewFinder` | 是否显示定位器开关入口，默认 `true`。 |
| `ViewFinder` | 只读，模板中实际定位器元素。 |
| `Viewport` | 只读，当前内容坐标系中的可见区域。 |

启用：

```xaml
<h:Zoombox
    UseShowViewFinder="True"
    ViewFinderVisibility="Visible" />
```

彻底隐藏开关：

```xaml
<h:Zoombox
    UseShowViewFinder="False"
    ViewFinderVisibility="Collapsed" />
```

`ViewFinderVisibility` 是附加依赖属性，但也可以直接设置在 `Zoombox` 上。

### 15.1 监听视口变化

```xaml
<h:Zoombox ViewportChanged="Zoombox_ViewportChanged" />
```

```csharp
private void Zoombox_ViewportChanged(object sender, RoutedEventArgs e)
{
    var zoombox = (Zoombox)sender;
    Rect viewport = zoombox.Viewport;
}
```

可用于：

- 同步坐标状态栏。
- 按视口延迟加载图块。
- 与其他缩略图或视图联动。

---

## 16. 视图模型 `ZoomboxView`

`ZoomboxView` 表示一种查看状态。

内置静态视图：

```csharp
ZoomboxView.Fit
ZoomboxView.Fill
ZoomboxView.Center
ZoomboxView.Empty
```

绝对视图：

```csharp
var view = new ZoomboxView(
    scale: 2.0,
    position: new Point(-100, -50));
```

区域视图：

```csharp
var view = new ZoomboxView(
    new Rect(100, 80, 400, 300));
```

使用：

```csharp
zoombox.ZoomTo(view);
```

`ZoomboxViewKind`：

| 值 | 说明 |
|---|---|
| `Absolute` | 由 `Scale` 和/或 `Position` 描述绝对视图。 |
| `Fit` | 全部内容适应边界。 |
| `Fill` | 内容填充边界。 |
| `Center` | 内容居中。 |
| `Empty` | 空视图。 |
| `Region` | 显示指定内容区域。 |

`Position`、`Scale`、`Region` 只可在对应类型的视图上读取，否则会抛出 `InvalidOperationException`。

---

## 17. 视图历史栈

`Zoombox` 可以记录视图变化，支持浏览器式前进和后退。

主要属性：

| 属性 | 说明 |
|---|---|
| `ViewStackMode` | 配置历史栈模式。 |
| `EffectiveViewStackMode` | 只读，实际生效模式。 |
| `ViewStack` | 视图集合。 |
| `ViewStackSource` | 外部绑定的视图数据源。 |
| `ViewStackIndex` | 当前历史索引。 |
| `ViewStackCount` | 只读，历史数量。 |
| `HasBackStack` | 只读，是否可后退。 |
| `HasForwardStack` | 只读，是否可前进。 |
| `CurrentView` | 只读，当前视图。 |
| `CurrentViewIndex` | 只读，当前视图关联索引。 |

### 17.1 `ZoomboxViewStackMode`

| 值 | 说明 |
|---|---|
| `Auto` | 自动记录视图变化。 |
| `Default` | 没有外部源时使用 `Auto`；有 `ViewStackSource` 时使用 `Manual`。 |
| `Disabled` | 禁用历史栈，索引被强制为 `-1`。 |
| `Manual` | 使用显式提供的视图集合。 |

自动历史：

```xaml
<h:Zoombox ViewStackMode="Auto" />
```

禁用历史以减少状态维护：

```xaml
<h:Zoombox ViewStackMode="Disabled" />
```

### 17.2 XAML 声明视图

```xaml
<h:Zoombox ViewStackIndex="0" ViewStackMode="Manual">
    <h:Zoombox.ViewStack>
        <h:ZoomboxView>Fit</h:ZoomboxView>
        <h:ZoomboxView>Fill</h:ZoomboxView>
        <h:ZoomboxView>1.0</h:ZoomboxView>
    </h:Zoombox.ViewStack>
    <Image Source="Assets/sample.jpg" />
</h:Zoombox>
```

`ZoomboxViewConverter` 支持在 XAML 中把 `Fit`、`Fill`、`Center` 和数值等文本转换为视图。

### 17.3 历史导航

```csharp
zoombox.GoBack();
zoombox.GoForward();
zoombox.GoHome();
zoombox.RefocusView();
```

- `GoHome()` 返回索引 `0`。
- `RefocusView()` 在手动模式下重新应用当前栈项。
- 禁用历史栈后这些方法不会执行。

---

## 18. 路由命令

`Zoombox` 公开以下 `RoutedUICommand`：

| 命令 | 作用 |
|---|---|
| `Zoombox.Back` | 后退到上一个历史视图。 |
| `Zoombox.Forward` | 前进到下一个历史视图。 |
| `Zoombox.Home` | 返回第一个视图。 |
| `Zoombox.Center` | 居中内容。 |
| `Zoombox.Fit` | 全部内容适应边界。 |
| `Zoombox.Fill` | 内容填充边界。 |
| `Zoombox.Refocus` | 重新应用当前手动视图。 |
| `Zoombox.ZoomIn` | 按 `ZoomPercentage` 放大。 |
| `Zoombox.ZoomOut` | 按 `ZoomPercentage` 缩小。 |
| `Zoombox.PanLeft` | 向左移动 `PanDistance`。 |
| `Zoombox.PanRight` | 向右移动 `PanDistance`。 |
| `Zoombox.PanUp` | 向上移动 `PanDistance`。 |
| `Zoombox.PanDown` | 向下移动 `PanDistance`。 |

工具栏：

```xaml
<ToolBar>
    <Button Command="{x:Static h:Zoombox.ZoomOut}" CommandTarget="{Binding ElementName=Zoom}" Content="-" />
    <Button Command="{x:Static h:Zoombox.ZoomIn}" CommandTarget="{Binding ElementName=Zoom}" Content="+" />
    <Button Command="{x:Static h:Zoombox.Fit}" CommandTarget="{Binding ElementName=Zoom}" Content="适应" />
    <Button Command="{x:Static h:Zoombox.Fill}" CommandTarget="{Binding ElementName=Zoom}" Content="填充" />
    <Button Command="{x:Static h:Zoombox.Center}" CommandTarget="{Binding ElementName=Zoom}" Content="居中" />
    <Button Command="{x:Static h:Zoombox.Back}" CommandTarget="{Binding ElementName=Zoom}" Content="后退" />
    <Button Command="{x:Static h:Zoombox.Forward}" CommandTarget="{Binding ElementName=Zoom}" Content="前进" />
</ToolBar>

<h:Zoombox x:Name="Zoom">
    <Image Source="{Binding ImageSource}" />
</h:Zoombox>
```

必须设置 `CommandTarget`，否则焦点不在 Zoombox 时路由命令可能找不到目标。

### 18.1 默认键盘绑定

默认模板包含：

| 按键 | 命令 |
|---|---|
| `Ctrl + Left` / `Ctrl + Up` | `ZoomOut`。 |
| `Ctrl + Right` / `Ctrl + Down` | `ZoomIn`。 |
| `Ctrl + Shift + Left` | `PanLeft`。 |
| `Ctrl + Shift + Right` | `PanRight`。 |
| `Ctrl + Shift + Up` | `PanUp`。 |
| `Ctrl + Shift + Down` | `PanDown`。 |
| `BrowserBack` / `Back` | `Back`。 |
| `BrowserForward` | `Forward`。 |

默认样式把 `Focusable` 和 `IsTabStop` 设为 `false`。如需键盘交互，可覆盖：

```xaml
<h:Zoombox Focusable="True" IsTabStop="True" />
```

---

## 19. 公开方法与 `IZoombox`

`Zoombox` 实现：

```csharp
public interface IZoombox
{
    void FillToBounds();
    void FitToBounds();
    void Zoom(double percentage);
    void Zoom(double percentage, Point relativeTo);
    void ZoomTo(double scale);
    void ZoomTo(double scale, Point relativeTo);
    void ZoomTo(Point position);
    void ZoomTo(Rect region);
}
```

接口适合让 ViewModel 行为或服务只依赖缩放能力，不依赖具体控件类型。

常用方法：

```csharp
zoombox.Zoom(0.2);                         // 当前比例增加 20%
zoombox.Zoom(-0.2);                        // 当前比例减少 20%
zoombox.Zoom(0.1, mousePoint);             // 围绕内容点缩放
zoombox.ZoomTo(2.0);                       // 设置为 200%
zoombox.ZoomTo(2.0, contentPoint);         // 围绕指定点设为 200%
zoombox.ZoomTo(new Point(100, 50));        // 平移到位置
zoombox.ZoomTo(new Rect(0, 0, 500, 300));  // 显示指定区域
```

---

## 20. 事件

| 事件 | 说明 |
|---|---|
| `AnimationBeginning` | 视图动画开始。 |
| `AnimationCompleted` | 视图动画完成。 |
| `CurrentViewChanged` | 当前 `ZoomboxView` 改变。 |
| `ViewStackIndexChanged` | 历史栈索引改变。 |
| `ViewportChanged` | 当前可见内容区域改变。 |
| `Scroll` | 内置滚动条发生滚动。 |

```xaml
<h:Zoombox
    AnimationCompleted="Zoombox_AnimationCompleted"
    CurrentViewChanged="Zoombox_CurrentViewChanged"
    ViewportChanged="Zoombox_ViewportChanged" />
```

`CurrentViewChanged` 的事件参数包含旧视图、新视图和相关历史索引，适合同步状态栏或保存用户视图。

高频平移时 `ViewportChanged` 可能连续触发。不要在事件中执行昂贵同步 I/O，可使用节流、取消或后台加载。

---

## 21. 自动适应行为

`H.Controls.ZoomBox.Extension` 提供两个 Behavior。

### 21.1 加载后适应

```xaml
xmlns:b="http://schemas.microsoft.com/xaml/behaviors"
xmlns:zoom="clr-namespace:H.Controls.ZoomBox.Extension;assembly=H.Controls.ZoomBox.Extension"

<h:Zoombox>
    <b:Interaction.Behaviors>
        <zoom:ZoomBoxFitOnLoadedBehavior />
    </b:Interaction.Behaviors>
    <Image Source="{Binding ImageSource}" />
</h:Zoombox>
```

行为在 `Loaded` 时调用：

```csharp
AssociatedObject.FitToBounds();
```

### 21.2 尺寸变化后适应

```xaml
<h:Zoombox>
    <b:Interaction.Behaviors>
        <zoom:ZoomBoxFitOnSizeChangedBehavior />
    </b:Interaction.Behaviors>
    <Image Source="{Binding ImageSource}" />
</h:Zoombox>
```

窗口或布局尺寸变化时重新调用 `FitToBounds()`。

### 21.3 同时使用

```xaml
<h:Zoombox>
    <b:Interaction.Behaviors>
        <zoom:ZoomBoxFitOnLoadedBehavior />
        <zoom:ZoomBoxFitOnSizeChangedBehavior />
    </b:Interaction.Behaviors>
    <Image Source="{Binding ImageSource}" />
</h:Zoombox>
```

注意：持续自动 Fit 会覆盖用户手动缩放。编辑器窗口通常只使用 Loaded 行为；纯预览窗口可以同时使用。

---

## 22. 图片异步加载后适应

图片源可能在 Zoombox 已加载后才完成绑定。可监听 `TargetUpdated` / `SourceUpdated`：

```xaml
<h:Zoombox x:Name="Zoom">
    <Image
        Source="{Binding ImageSource,
                         IsAsync=True,
                         NotifyOnSourceUpdated=True,
                         NotifyOnTargetUpdated=True}">
        <b:Interaction.Triggers>
            <b:EventTrigger EventName="TargetUpdated">
                <h:CallMethodActionEx
                    MethodName="FitToBounds"
                    TargetObject="{Binding ElementName=Zoom}" />
            </b:EventTrigger>
        </b:Interaction.Triggers>
    </Image>
</h:Zoombox>
```

也可以在图片下载完成或 `ImageOpened` 后从代码调用 `FitToBounds()`。

---

## 23. 图片预览对话框扩展

`H.Controls.ZoomBox.Extension` 提供 `ImageZoomViewPresenter` 和对话框扩展。

### 23.1 使用 `ImageSource`

```csharp
await IocMessage.Dialog.ShowZoomViewImage(imageSource);
```

### 23.2 使用文件路径

```csharp
await IocMessage.Dialog.ShowZoomViewImage(
    @"D:\Images\sample.png");
```

### 23.3 配置对话框

```csharp
await IocMessage.Dialog.ShowZoomViewImage(
    imageSource,
    dialog =>
    {
        dialog.Title = "图片预览";
        dialog.MinWidth = 600;
        dialog.MinHeight = 400;
    });
```

内部默认配置：

```text
DialogButton = None
MinWidth = 200
```

Presenter：

```csharp
public interface IImageZoomViewPresenter
{
    ImageSource ImageSource { get; set; }
}
```

该扩展适用于列表缩略图、附件、截图和检测结果的快速放大查看。

---

## 24. 图片预览命令

扩展项目还提供：

```text
ShowZoomViewImageCommand
ShowZoomViewImageFileCommand
ShowZoomViewImageSourceCommand
```

用于从框架命令系统打开图片预览。根据命令类型传入文件路径、`ImageSource` 或相应参数。

在业务中优先使用类型匹配的命令，避免在 UI 层重复创建对话框。

---

## 25. MVVM 使用建议

不要让普通 ViewModel 长期持有 `Zoombox` 控件。推荐方式：

1. ViewModel 暴露图片、视图参数和业务命令。
2. View 使用路由命令控制 Zoombox。
3. 必要时使用 Behavior 调用 `IZoombox` 方法。
4. 通过事件转命令报告 `Viewport` 或当前比例。

状态绑定：

```xaml
<Slider
    Minimum="0.1"
    Maximum="10"
    Value="{Binding ElementName=Zoom, Path=Scale, Mode=TwoWay}" />

<TextBlock Text="{Binding ElementName=Zoom, Path=Scale, StringFormat={}{0:P0}}" />
```

`Scale` 初始可能是 `NaN`。复杂转换器应处理 `NaN` 和未初始化状态。

---

## 26. 自定义控件模板

`Zoombox` 标记模板部件：

```text
PART_VerticalScrollBar
PART_HorizontalScrollBar
```

自定义模板如需滚动条必须保留这些名称和 `ScrollBar` 类型。

默认模板还包含：

- `_contentPresenter`
- `ViewFinder`
- `ViewFinderDisplay`
- `ShowViewFinder`
- 缩放历史和 Fit/Fill 工具按钮

控件内部会在 `OnApplyTemplate()` 后搜索和绑定这些元素。复制模板时应从当前仓库版本的 `Zoombox.xaml` 开始，不要依赖旧版第三方模板结构。

主题资源：

```xaml
Background="{DynamicResource {x:Static h:BrushKeys.Tile}}"
Foreground="{DynamicResource {x:Static h:BrushKeys.Foreground}}"
BorderBrush="{DynamicResource {x:Static h:BrushKeys.BorderBrush}}"
```

`BrushKeys.Tile` 是透明棋盘格样式，适合图片透明区域预览。

---

## 27. 性能建议

### 图片

- 大图片优先按显示需求解码，设置 `BitmapImage.DecodePixelWidth` / `DecodePixelHeight`。
- 缩放质量和性能之间使用 `RenderOptions.BitmapScalingMode` 权衡。
- 图片文件流使用完后及时释放。
- 不要反复创建同一张大图的 `BitmapImage`。

### Canvas / Diagram

- 大量节点使用虚拟化、分层或按视口加载。
- `ViewportChanged` 高频事件应节流。
- 拖动过程中避免实时执行数据库和网络操作。
- 大内容交互卡顿时关闭 `IsAnimated`。
- 不需要历史时设置 `ViewStackMode="Disabled"`。
- 不需要定位器时关闭 `UseShowViewFinder`，减少 VisualBrush 和缩略图绘制成本。

### 自动适应

`ZoomBoxFitOnSizeChangedBehavior` 会在每次尺寸变化时 Fit。窗口拖动调整大小时可能高频执行，应根据场景使用防抖或只在调整完成后适应。

---

## 28. 常见问题

### 内容没有显示

检查：

1. `Content` 是否为 `UIElement`。
2. 图片源是否加载成功。
3. 内容是否具有可测量尺寸。
4. 是否在加载后调用 `FitToBounds()`。
5. `Scale` 是否被设置到过小值。

### 鼠标滚轮不能缩放

默认要求：

```text
Shift + 滚轮
```

围绕鼠标位置缩放默认要求：

```text
Ctrl + Alt + 滚轮
```

如需直接滚轮缩放，应调整 `ZoomModifiers` 或 `RelativeZoomModifiers`。

### 左键不能拖动

默认要求按住 `Ctrl`。也可设置：

```xaml
DragModifiers=""
```

但可能与内容点击冲突，推荐使用：

```xaml
UseMiddleButtonDrag="True"
```

### Fit 调用后没有正确适应

可能在内容尚未完成测量时调用。使用：

```xaml
<zoom:ZoomBoxFitOnLoadedBehavior />
```

异步图片还需在图片源更新后再次 Fit。

### 定位器没有显示

检查：

```xaml
UseShowViewFinder="True"
ViewFinderVisibility="Visible"
```

默认样式把 `ViewFinderVisibility` 设置为 `Collapsed`。

### 键盘快捷键无效

默认样式设置：

```text
Focusable = false
IsTabStop = false
```

需要键盘操作时覆盖为 `true`，并确保 Zoombox 获得焦点。

### 前进后退按钮不可用

检查：

- `ViewStackMode` 是否为 `Disabled`。
- 是否已有多个历史视图。
- `ViewStackIndex` 是否位于可导航范围。
- 使用外部 `ViewStackSource` 时是否采用 `Manual` 模式。

### 拖动后内容完全移出视口

设置：

```xaml
KeepContentInBounds="True"
```

### 自定义模板滚动条不工作

确认保留：

```text
PART_VerticalScrollBar
PART_HorizontalScrollBar
```

---

## 29. 完整图片查看器示例

```xaml
<Grid>
    <Grid.RowDefinitions>
        <RowDefinition Height="Auto" />
        <RowDefinition />
    </Grid.RowDefinitions>

    <ToolBar>
        <Button
            Command="{x:Static h:Zoombox.ZoomOut}"
            CommandTarget="{Binding ElementName=Zoom}"
            Content="缩小" />
        <Button
            Command="{x:Static h:Zoombox.ZoomIn}"
            CommandTarget="{Binding ElementName=Zoom}"
            Content="放大" />
        <Button
            Command="{x:Static h:Zoombox.Fit}"
            CommandTarget="{Binding ElementName=Zoom}"
            Content="适应" />
        <Button
            Command="{x:Static h:Zoombox.Fill}"
            CommandTarget="{Binding ElementName=Zoom}"
            Content="填充" />
        <Button
            Command="{x:Static h:Zoombox.Center}"
            CommandTarget="{Binding ElementName=Zoom}"
            Content="居中" />
        <Separator />
        <TextBlock VerticalAlignment="Center" Text="{Binding ElementName=Zoom, Path=Scale, StringFormat={}{0:P0}}" />
    </ToolBar>

    <h:Zoombox
        x:Name="Zoom"
        Grid.Row="1"
        Background="{DynamicResource {x:Static h:BrushKeys.Tile25}}"
        IsAnimated="True"
        KeepContentInBounds="True"
        MaxScale="20"
        MinScale="0.1"
        RelativeZoomModifiers=""
        UseMiddleButtonDrag="True"
        ViewFinderVisibility="Visible"
        ViewStackMode="Auto"
        ZoomModifiers="Blocked"
        ZoomPercentage="10">
        <b:Interaction.Behaviors>
            <zoom:ZoomBoxFitOnLoadedBehavior />
        </b:Interaction.Behaviors>
        <Image
            RenderOptions.BitmapScalingMode="HighQuality"
            Source="{Binding ImageSource}" />
    </h:Zoombox>
</Grid>
```

此配置实现：

- 加载后自动适应。
- 普通滚轮围绕鼠标位置缩放。
- 中键拖动。
- 10% 缩放步长。
- 10%～2000% 缩放范围。
- 定位器和历史视图。
- 透明棋盘格图片背景。

---

## 30. 二次开发建议

- 图片和文档预览默认使用 `FitToBounds()`。
- Diagram 和标注工具优先启用中键拖动，避免占用左键。
- 清楚区分 `Zoom()` 的相对比例和 `ZoomTo()` 的绝对比例。
- 直接滚轮缩放时只激活 `ZoomModifiers`、`RelativeZoomModifiers` 之一。
- 自定义模板保留滚动条模板部件。
- 异步图片加载完成后重新 Fit。
- 不需要的动画、定位器和历史栈应关闭以降低大内容开销。
- 业务 ViewModel 不直接持有控件，使用命令、行为和事件转命令交互。
- 高频 `ViewportChanged` 使用节流，避免阻塞 UI 线程。
- 图片快速预览优先使用 `ShowZoomViewImage()` 扩展。
