# MainWindow 主窗口开发文档

**适用项目：** `H.Windows.Main`  
**核心类型：** `MainWindow`、`TransparencyMainWindow`、`MainWindowOption`、`WindowSetting`、`IMainWindowSavableService`  
**相关能力：** 自定义标题栏、窗口状态切换、关闭保存、窗口尺寸持久化、侧栏模板、透明窗口

本文介绍框架中 `H.Windows.Main.MainWindow` 主窗口控件的注册、模板、标题栏按钮、关闭流程、设置持久化和二次开发方式。

> 用户所称的 `H.Window.MainWindow` 对应当前仓库程序集和命名空间 `H.Windows.Main`，核心窗口类型为 `H.Windows.Main.MainWindow`。

---

## 1. 控件定位

`MainWindow` 是一个带自定义标题栏的 WPF `Window`。它继承框架窗口基类，提供：

- 默认标题栏区域。
- 最小化、最大化、还原和关闭命令。
- 可替换的标题栏 `CaptionTempate`。
- 可选的左侧或顶部扩展 `SideTemplate`。
- 主题资源和窗口样式键。
- 主窗口关闭时保存设置和业务状态。
- 上次窗口尺寸、状态和启动位置恢复。
- 透明窗口派生类型 `TransparencyMainWindow`。

典型布局：

```text
MainWindow
├── SideTemplate      可选侧栏/导航区
├── 标题栏
│   ├── Icon
│   ├── Title
│   ├── CaptionTempate 自定义内容
│   └── 窗口状态按钮
└── Content           应用主内容
```

---

## 2. 项目引用与资源

项目引用：

```xml
<ItemGroup>
  <ProjectReference Include="..\..\Windows\H.Windows.Main\H.Windows.Main.csproj" />
</ItemGroup>
```

XAML 命名空间：

```xaml
xmlns:h="https://github.com/HeBianGu"
```

`MainWindow` 默认样式由 `H.Windows.Main/Themes/Generic.xaml` 提供。应用还应按需合并框架主题和样式资源：

```xaml
<Application.Resources>
    <ResourceDictionary>
        <ResourceDictionary.MergedDictionaries>
            <FontSizeTheme Type="Default" />
            <LayoutTheme Type="Default" />
            <ColorTheme Type="Default" />
            <ConciseStyle />
        </ResourceDictionary.MergedDictionaries>
    </ResourceDictionary>
</Application.Resources>
```

主题资源、`BrushKeys`、`FontSizeKeys` 和 `LayoutKeys` 的详细说明见 [`development-guide-theme.md`](development-guide-theme.md)。

---

## 3. 基础使用

```xaml
<h:MainWindow
    x:Class="MyApp.MainWindow"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:h="https://github.com/HeBianGu"
    Width="1100"
    Height="700"
    Icon="Assets/Logo.ico"
    Title="MyApp"
    WindowStartupLocation="CenterScreen">
    <Grid>
        <TextBlock
            HorizontalAlignment="Center"
            VerticalAlignment="Center"
            Text="主窗口内容" />
    </Grid>
</h:MainWindow>
```

代码后置：

```csharp
namespace MyApp;

public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();
    }
}
```

`ApplicationBase` 创建主窗口：

```csharp
protected override Window CreateMainWindow(StartupEventArgs e)
{
    return new MainWindow();
}
```

---

## 4. 默认样式资源键

`MainWindowKeys` 公开：

```csharp
MainWindowKeys.Default
```

对应资源 ID：

```text
S.MainWindow.Default
```

在自定义样式中基于默认窗口样式：

```xaml
<Style
    x:Key="MyMainWindowStyle"
    BasedOn="{StaticResource {x:Static h:MainWindowKeys.Default}}"
    TargetType="{x:Type h:MainWindow}">
    <Setter Property="Background" Value="{DynamicResource {x:Static h:BrushKeys.CaptionBackground}}" />
</Style>
```

使用：

```xaml
<h:MainWindow Style="{StaticResource MyMainWindowStyle}" />
```

如果修改完整 `ControlTemplate`，应从当前仓库 `H.Windows.Main/MainWindow.xaml` 的默认模板开始，避免遗漏标题栏按钮、窗口拖动、边缘调整或主题绑定。

---

## 5. `MainWindow` 公开属性

| 属性 | 类型 | 默认值 | 说明 |
|---|---|---:|---|
| `CaptionHeight` | `double` | `45` | 自定义标题栏高度。 |
| `CaptionTempate` | `ControlTemplate` | `null` | 标题栏中央或扩展区域的自定义模板。名称保留当前 API 拼写 `Tempate`。 |
| `SideTemplate` | `ControlTemplate` | `null` | 可选侧栏模板。为 `null` 时默认模板隐藏侧栏区域。 |

`MainWindow` 还继承普通 WPF `Window` 属性：

```text
Title
Icon
Width / Height
MinWidth / MinHeight
WindowState
WindowStartupLocation
ResizeMode
Background
Foreground
Content
```

`CaptionHeight` 示例：

```xaml
<h:MainWindow CaptionHeight="52" />
```

框架默认模板中的标题栏行高度绑定 `CaptionHeight`。修改时应同时检查自定义标题内容和窗口按钮是否仍能完整显示。

---

## 6. 自定义标题栏 `CaptionTempate`

`CaptionTempate` 用于在标题栏中放置工具栏、搜索框、面包屑、用户信息或导航入口。

> API 名称拼写为 `CaptionTempate`，不是 `CaptionTemplate`；XAML 和 C# 都必须使用实际名称。

```xaml
<h:MainWindow
    x:Class="MyApp.MainWindow"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:h="https://github.com/HeBianGu"
    Title="MyApp">
    <h:MainWindow.CaptionTempate>
        <ControlTemplate>
            <Grid Margin="12,0">
                <Grid.ColumnDefinitions>
                    <ColumnDefinition Width="*" />
                    <ColumnDefinition Width="Auto" />
                </Grid.ColumnDefinitions>

                <TextBlock
                    VerticalAlignment="Center"
                    FontSize="{DynamicResource {x:Static h:FontSizeKeys.Header}}"
                    Text="数据分析平台" />

                <TextBox
                    Grid.Column="1"
                    Width="220"
                    VerticalAlignment="Center"
                    Text="{Binding SearchText, UpdateSourceTrigger=PropertyChanged}" />
            </Grid>
        </ControlTemplate>
    </h:MainWindow.CaptionTempate>

    <Grid />
</h:MainWindow>
```

默认模板会继续显示窗口图标、标题和右侧系统按钮。`CaptionTempate` 只负责模板插入区域，不需要重复添加最小化、最大化、还原和关闭按钮。

### 6.1 标题栏中的主题资源

标题栏内容应使用语义主题资源：

```xaml
<TextBlock
    Foreground="{DynamicResource {x:Static h:BrushKeys.CaptionForeground}}"
    FontSize="{DynamicResource {x:Static h:FontSizeKeys.Header}}" />
```

不要假定标题栏始终是浅色或深色，也不要在通用标题栏模板中写死前景色。

---

## 7. 自定义侧栏 `SideTemplate`

`SideTemplate` 可用于放置导航栏、品牌区、状态区或全局操作入口。

```xaml
<h:MainWindow
    x:Class="MyApp.MainWindow"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:h="https://github.com/HeBianGu">
    <h:MainWindow.SideTemplate>
        <ControlTemplate>
            <Border
                Width="220"
                Background="{DynamicResource {x:Static h:BrushKeys.MenuBackground}}">
                <StackPanel Margin="12">
                    <TextBlock
                        Margin="0,0,0,12"
                        FontSize="{DynamicResource {x:Static h:FontSizeKeys.Header}}"
                        Foreground="{DynamicResource {x:Static h:BrushKeys.MenuForeground}}"
                        Text="导航" />
                    <Button Content="首页" />
                    <Button Content="项目" />
                    <Button Content="设置" />
                </StackPanel>
            </Border>
        </ControlTemplate>
    </h:MainWindow.SideTemplate>

    <Grid>
        <TextBlock Text="主内容" />
    </Grid>
</h:MainWindow>
```

默认模板在 `SideTemplate` 为 `null` 时折叠该区域。侧栏模板应限制明确宽度，避免挤压主内容或影响最小窗口尺寸。

---

## 8. 窗口状态命令

默认标题栏使用框架窗口命令：

| 命令 | 作用 |
|---|---|
| `MinimizeWindowCommand` | 最小化窗口。 |
| `MaximizeWindowCommand` | 最大化窗口。 |
| `RestoreWindowCommand` | 将最大化窗口还原。 |
| `CloseWindowCommand` | 关闭窗口。 |
| `CloseAfterSaveWindowCommand` | 主窗口关闭前执行设置和注册保存服务。 |
| `TranslationCloseWindowCommand` | 与窗口关闭相关的本地化命令。 |

默认 `MainWindow` 模板使用 `CloseAfterSaveWindowCommand`，而不是直接关闭命令。这样主窗口关闭时可以保存设置和业务状态。

### 8.1 在自定义标题栏中使用命令

```xaml
xmlns:window="clr-namespace:H.Windows.Main.Commands;assembly=H.Windows.Main"

<StackPanel HorizontalAlignment="Right" Orientation="Horizontal">
    <Button
        Command="{window:MinimizeWindowCommand}"
        CommandParameter="{Binding RelativeSource={RelativeSource AncestorType={x:Type Window}}}"
        Content="—" />
    <Button
        Command="{window:MaximizeWindowCommand}"
        CommandParameter="{Binding RelativeSource={RelativeSource AncestorType={x:Type Window}}}"
        Content="□" />
    <Button
        Command="{window:CloseAfterSaveWindowCommand}"
        CommandParameter="{Binding RelativeSource={RelativeSource AncestorType={x:Type Window}}}"
        Content="×" />
</StackPanel>
```

窗口命令需要目标 `Window`。默认模板在命令上下文中提供了窗口参数；在自定义布局中应显式传入当前窗口。

---

## 9. 关闭、保存和确认流程

`CloseAfterSaveWindowCommand` 的主窗口流程：

```text
点击关闭
    ↓
判断是否为 Application.Current.MainWindow
    ↓
WindowSetting.UseSaveOnMainWindowClose = true？
    ↓ 是
保存 IocSetting 中的选项
执行已注册的 ISplashSave
    ↓
WindowSetting.UseNoticeOnMainWindowClose = true？
    ↓ 是
显示确认对话框
    ↓
SystemCommands.CloseWindow(window)
```

### 9.1 保存范围

关闭保存命令会保存：

1. `IocSetting.Instance` 中注册的设置项。
2. IOC 中所有 `ISplashSave` 服务。

因此模块的 `UseXxxOptions()` 需要把 Options 实例加入 `IocSetting`，需要在关闭时持久化的业务服务应实现 `ISplashSave`。

```csharp
public class SaveWorkspaceService : ISplashSave
{
    public string Name => "工作区";

    public bool Save(out string message)
    {
        // 保存业务数据。
        message = null;
        return true;
    }
}

services.AddSingleton<ISplashSave, SaveWorkspaceService>();
```

### 9.2 保存失败和取消

命令会在可用时使用 `IocMessage.Dialog.ShowString()` 显示保存进度，并提供取消按钮。用户取消保存流程时，不会继续关闭窗口。

保存服务应：

- 快速返回成功或失败状态。
- 在 `message` 中提供可读错误信息。
- 避免在 UI 线程中长时间阻塞。
- 对非关键保存失败定义明确降级策略。

### 9.3 关闭确认

关闭确认由：

```csharp
WindowSetting.Instance.UseNoticeOnMainWindowClose
```

控制。默认值为：

```text
true
```

应用退出由系统关闭窗口后继续执行，业务不应在多个位置重复调用 `Application.Shutdown()`。

---

## 10. `WindowSetting`

`WindowSetting` 是窗口显示和关闭流程的设置项。

| 属性 | 默认值 | 说明 |
|---|---:|---|
| `BackImagePath` | 空 | 窗口背景图片路径。 |
| `UseBackImage` | `false` | 是否使用窗口背景图片。 |
| `Opacity` | `0.3` | 背景图片透明度。 |
| `Stretch` | `UniformToFill` | 背景图片拉伸方式。 |
| `UseNoticeOnMainWindowClose` | `true` | 关闭主窗口前是否要求确认。 |
| `UseSaveOnMainWindowClose` | `true` | 关闭主窗口前是否保存设置和 `ISplashSave` 服务。 |

注册和配置：

```csharp
protected override void Configure(IApplicationBuilder app)
{
    base.Configure(app);

    app.UseWindowSetting(options =>
    {
        options.UseNoticeOnMainWindowClose = true;
        options.UseSaveOnMainWindowClose = true;
        options.UseBackImage = false;
    });
}
```

`UseWindowSetting()` 会将 `WindowSetting.Instance` 加入设置系统。

---

## 11. 窗口大小与状态持久化

`MainWindowOption` 用于保存和恢复主窗口状态：

| 属性 | 默认值 | 说明 |
|---|---:|---|
| `Width` | `1100` | 窗口宽度。 |
| `Height` | `700` | 窗口高度。 |
| `WindowStartupLocation` | `CenterScreen` | 启动位置。 |
| `WindowState` | `Normal` | 正常、最小化或最大化状态。 |

注册持久化服务：

```csharp
protected override void ConfigureServices(IServiceCollection services)
{
    services.AddMainWindowSavableService(options =>
    {
        options.Width = 1200;
        options.Height = 800;
        options.WindowStartupLocation = WindowStartupLocation.CenterScreen;
    });
}
```

注册选项：

```csharp
protected override void Configure(IApplicationBuilder app)
{
    base.Configure(app);
    app.UseMainWindowSetting();
}
```

`MainWindowSavableService`：

- `Load(Window window)`：恢复 `WindowStartupLocation`、`WindowState`、`Width`、`Height`。
- `Save(out string message)`：读取 `Application.Current.MainWindow` 的这些属性并保存 `MainWindowOption`。

`ApplicationBase` 在创建主窗口后会尝试解析 `IMainWindowSavableService` 并调用加载逻辑。

### 11.1 注意事项

- 该默认实现只保存尺寸、状态和启动位置，不保存 `Left`、`Top`、显示器信息或 DPI 信息。
- 多显示器、负坐标和屏幕布局变化场景需要自定义 `IMainWindowSavableService`。
- 直接恢复最大化状态时仍会同时写入宽高；这是当前默认实现行为。
- 设置项只有通过 `UseMainWindowSetting()` 加入 `IocSetting` 后才参与默认设置保存。

### 11.2 自定义窗口状态服务

```csharp
public class MonitorAwareMainWindowSavableService : IMainWindowSavableService
{
    public string Name => "主窗口状态";

    public void Load(Window window)
    {
        // 验证保存位置仍位于可用显示器范围后再恢复。
    }

    public bool Save(out string message)
    {
        // 保存 Left、Top、Width、Height、WindowState 和当前屏幕标识。
        message = null;
        return true;
    }
}
```

替换默认实现：

```csharp
services.Replace(
    ServiceDescriptor.Singleton<IMainWindowSavableService,
        MonitorAwareMainWindowSavableService>());
```

---

## 12. 透明主窗口

`TransparencyMainWindow` 继承 `MainWindow`。

构造时设置：

```csharp
AllowsTransparency = true;
WindowStyle = WindowStyle.None;
```

并在鼠标左键按下时调用：

```csharp
DragMove();
```

基础用法：

```xaml
<h:TransparencyMainWindow
    x:Class="MyApp.TransparentWindow"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:h="https://github.com/HeBianGu"
    Background="Transparent"
    Title="透明窗口">
    <Border
        Padding="24"
        Background="{DynamicResource {x:Static h:BrushKeys.CaptionBackground}}"
        CornerRadius="12">
        <TextBlock Text="透明主窗口内容" />
    </Border>
</h:TransparencyMainWindow>
```

### 12.1 透明窗口注意事项

- WPF `AllowsTransparency=True` 可能降低复杂动画、大图片和大列表的渲染性能。
- 透明区域仍会参与命中测试；需要穿透时应使用专门的命中测试或 Win32 方案。
- 默认左键拖动会与窗口内按钮、文本框和画布交互冲突。复杂窗口建议调整拖动逻辑，只在标题栏区域调用 `DragMove()`。
- 透明窗口同样应使用主题资源，避免硬编码背景和前景颜色。

---

## 13. 内容和装饰层

`MainWindow.GetElement()` 返回当前用于承载内容或装饰层的元素：

```csharp
UIElement element = mainWindow.GetElement();
```

在默认结构中，控件优先返回内部装饰边框；如果装饰层不存在则返回 `Content as UIElement`。

该方法适合需要向主窗口添加 Adorner、遮罩、引导层或窗口级动画的框架扩展。普通业务代码应优先使用内容区域和消息服务，而不是直接依赖内部视觉树。

---

## 14. 完整注册示例

### `App.xaml.cs`

```csharp
using H.Extensions.ApplicationBase;
using H.Services.Common;
using H.Windows.Main;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace MyApp;

public partial class App : ApplicationBase
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddWindowMessage();
        services.AddAdornerDialogMessage();

        services.AddMainWindowSavableService(options =>
        {
            options.Width = 1200;
            options.Height = 800;
            options.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        });

        services.AddSingleton<ISplashSave, SaveWorkspaceService>();
    }

    protected override void Configure(IApplicationBuilder app)
    {
        base.Configure(app);

        app.UseMainWindowSetting();
        app.UseWindowSetting(options =>
        {
            options.UseSaveOnMainWindowClose = true;
            options.UseNoticeOnMainWindowClose = true;
        });
    }

    protected override Window CreateMainWindow(StartupEventArgs e)
    {
        return new MainWindow();
    }
}
```

### `MainWindow.xaml`

```xaml
<h:MainWindow
    x:Class="MyApp.MainWindow"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:h="https://github.com/HeBianGu"
    MinHeight="600"
    MinWidth="900"
    Title="MyApp">
    <h:MainWindow.CaptionTempate>
        <ControlTemplate>
            <TextBlock
                VerticalAlignment="Center"
                FontSize="{DynamicResource {x:Static h:FontSizeKeys.Header}}"
                Text="工作台" />
        </ControlTemplate>
    </h:MainWindow.CaptionTempate>

    <Grid>
        <TextBlock
            HorizontalAlignment="Center"
            VerticalAlignment="Center"
            Text="主窗口内容" />
    </Grid>
</h:MainWindow>
```

---

## 15. 二次开发建议

- 普通应用优先使用 `MainWindow`，只在确有透明窗口需求时使用 `TransparencyMainWindow`。
- 修改标题栏内容使用 `CaptionTempate`，不要复制整个窗口模板。
- 使用 `SideTemplate` 添加导航区，避免把导航逻辑硬编码进窗口模板。
- 自定义标题栏按钮时继续使用框架窗口命令，确保关闭前保存流程不被绕过。
- 需要恢复窗口状态时同时注册 `AddMainWindowSavableService()` 与 `UseMainWindowSetting()`。
- 需要关闭确认、背景图或关闭保存策略时调用 `UseWindowSetting()`。
- 关键业务数据实现 `ISplashSave`，让关闭流程统一保存。
- 多显示器应用应替换默认 `IMainWindowSavableService`，校验保存的屏幕位置。
- 窗口模板和标题栏使用 `DynamicResource` 绑定主题资源。
- 不要将长时间保存、网络调用或数据库迁移直接放在窗口关闭 UI 线程中。

---

## 16. 常见问题

### 主窗口尺寸没有恢复

检查：

1. 是否注册 `services.AddMainWindowSavableService()`。
2. 是否调用 `app.UseMainWindowSetting()`。
3. 关闭时是否使用 `CloseAfterSaveWindowCommand` 或确保状态保存服务被调用。
4. 设置目录是否可写，`MainWindowOption.Save()` 是否成功。

### 关闭窗口没有保存模块设置

检查：

1. 是否通过各模块的 `UseXxxOptions()` 把选项加入 `IocSetting`。
2. `WindowSetting.UseSaveOnMainWindowClose` 是否为 `true`。
3. 自定义关闭按钮是否绕过了 `CloseAfterSaveWindowCommand`。
4. 自定义保存服务是否注册为 `ISplashSave`。

### 关闭确认没有显示

检查：

```csharp
WindowSetting.Instance.UseNoticeOnMainWindowClose
```

以及 `IocMessage` 对话框服务是否已注册。

### 自定义标题栏不显示

检查：

1. 是否使用了实际属性名 `CaptionTempate`。
2. `ControlTemplate` 是否放在 `MainWindow.CaptionTempate` 属性元素中。
3. 是否修改了完整窗口模板并遗漏了对应 `TemplateBinding`。

### 自定义侧栏不显示

确认：

```xaml
<h:MainWindow.SideTemplate>
    <ControlTemplate>
        <!-- 可视内容 -->
    </ControlTemplate>
</h:MainWindow.SideTemplate>
```

默认模板在 `SideTemplate` 为 `null` 时隐藏侧栏。

### 透明窗口中的按钮点击后窗口被拖动

`TransparencyMainWindow` 当前会在任意左键按下时调用 `DragMove()`。对复杂交互窗口应派生并限制为标题栏区域拖动。

### 最大化后内容被任务栏或屏幕边缘遮挡

检查窗口样式、DPI、多显示器和自定义模板。不要依赖固定屏幕像素；必要时使用自定义窗口状态服务和系统工作区信息。
