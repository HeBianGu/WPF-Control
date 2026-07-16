# WPF-Control 二次开发文档

**版本：1.2.0** | **框架：.NET 8.0-windows** | **许可证：MIT**

本文档面向 WPF-Control 的二次开发者、模块贡献者和维护者，系统说明解决方案架构、核心技术、模块开发流程、扩展方式及开发规范。

---

## 目录

- [1. 项目概述](#1-项目概述)
- [2. 环境要求与快速开始](#2-环境要求与快速开始)
- [3. 解决方案结构](#3-解决方案结构)
- [4. 核心基础设施](#4-核心基础设施)
  - [4.1 IOC 容器](#41-ioc-容器)
  - [4.2 MVVM 框架](#42-mvvm-框架)
  - [4.3 附加属性系统（Cattach）](#43-附加属性系统cattach)
  - [4.4 应用启动流程](#44-应用启动流程)
- [5. 主题与样式系统](#5-主题与样式系统)
- [6. 控件库详解](#6-控件库详解)
- [7. 模块系统](#7-模块系统)
- [8. 服务层](#8-服务层)
- [9. 扩展机制](#9-扩展机制)
- [10. 二次开发实战](#10-二次开发实战)
  - [10.1 创建新模块](#101-创建新模块)
  - [10.2 创建自定义控件](#102-创建自定义控件)
  - [10.3 编写单元测试示例](#103-编写单元测试示例)
  - [10.4 自定义主题](#104-自定义主题)
  - [10.5 集成第三方库](#105-集成第三方库)
- [11. 构建与发布](#11-构建与发布)
- [12. 常见模式与最佳实践](#12-常见模式与最佳实践)
- [13. 兼容性规范](#13-兼容性规范)

---

## 1. 项目概述

WPF-Control 是一个基于 **.NET 8+** 的高性能 WPF 控件库，由 HeBianGu 开发维护。项目采用**多程序集拆分 + 模块化架构**，将基础能力、控件、模块、服务、主题和测试分层管理。

### 主要特性

- **丰富的控件库**：Form、Dock、Diagram（流程图）、Chart2D（图表）、PDF、VLC 视频、QRCode、PropertyGrid、TagBox、Step 等
- **完整主题系统**：支持亮色/暗色主题切换、多套配色方案（Blue、Gray、Purple、Copper、Office 等）、字体尺寸/布局尺寸可调
- **模块化设计**：Login、Project、Theme、Setting、License、Guide、Upgrade 等业务模块即插即用
- **IOC 依赖注入**：基于 `Microsoft.Extensions.DependencyInjection` 统一管理服务生命周期
- **MVVM 架构**：自研 `H.Mvvm` + `CommunityToolkit.Mvvm`，支持命令、绑定、验证
- **数据驱动 UI**：支持 Presenter 模式，数据对象 + DataTemplate 自动生成界面
- **主题化附加属性**：Cattach 系统提供 50+ 附加属性，一行 XAML 即可完成样式、动画、行为配置

### 依赖方向

```
App / Tests
  → ApplicationBases
    → Modules / Windows / Presenters / Controls
      → Services / Extensions
        → Base / Common
```

**原则**：基础层不反向依赖控件层，通用控件不依赖具体业务模块，禁止循环引用。

---

## 2. 环境要求与快速开始

### 环境要求

| 组件 | 要求 |
|------|------|
| 操作系统 | Windows 10 / Windows 11 |
| IDE | Visual Studio 2022（推荐安装 XAML 设计器、WPF UI Debugging Tools） |
| SDK | .NET 8 SDK + Windows Desktop workload |
| NuGet | 可访问 nuget.org 包源 |

### 快速开始

```bash
git clone https://github.com/HeBianGu/WPF-Control.git
cd WPF-Control
dotnet restore
```

构建指定项目：

```bash
dotnet build Source/Themes/H.Theme/H.Theme.csproj
dotnet build Source/Controls/H.Controls.Diagram/H.Controls.Diagram.csproj
```

运行测试示例：

```bash
dotnet run --project Source/Tests/H.Test.Theme/H.Test.Theme.csproj
dotnet run --project Source/Tests/H.Test.Diagram/H.Test.Diagram.csproj
dotnet run --project Source/Tests/H.Test.Form/H.Test.Form.csproj
dotnet run --project Source/Tests/H.Test.Dock/H.Test.Dock.csproj
```

### 推荐的阅读顺序

1. 先运行 `Source/Tests` 下的示例项目，直观了解各控件的功能
2. 阅读 `Source/Base` 了解基础设施
3. 阅读 `Source/Themes` 和 `Source/Styles` 了解主题系统
4. 阅读 `Source/Services` 和 `Source/Extensions` 了解服务抽象与实现
5. 阅读 `Source/Modules` 了解模块开发模式
6. 最后阅读 `Source/Controls` 深入了解控件实现

---

## 3. 解决方案结构

解决方案按功能分为 8 个逻辑文件夹（Solution Folder），每个文件夹包含多个程序集。

### 3.1 解决方案文件夹一览

| 解决方案文件夹 | 物理目录 | 定位 | 包含程序集示例 |
|---|---|---|---|
| `0 - Base` | `Source/Base` | 基础层 | H.Attach、H.Mvvm、H.ValueConverter、H.MarkupExtension、H.Globalization |
| `1 - Common` | `Source/Common` | 公共层 | H.Common（接口、BindingProxy、LazyInstance） |
| `2 - Datas` | `Source/Datas` | 数据层 | H.Data.Test（测试数据模型） |
| `3 - Controls` | `Source/Controls` | 控件层 | H.Controls.Form、H.Controls.Dock、H.Controls.Diagram、H.Controls.PDF、H.Controls.Vlc |
| `4 - Extensions` | `Source/Extensions` | 扩展层 | H.Extensions.ApplicationBase、H.Extensions.Tree |
| `5 - Services` | `Source/Services` | 服务层 | H.Services.Common、H.Services.AppPath、H.Services.Logger、H.Services.Identity |
| `6 - Providers` | `Source/Providers` | 提供者 | H.Iocable（IOC 容器封装） |
| `7 - Themes` | `Source/Themes` | 主题层 | H.Theme、H.Style、H.Themes.Colors.*、H.Styles.* |
| `8 - Modules` | `Source/Modules` | 模块层 | H.Modules.Login、H.Modules.Project、H.Modules.Theme、H.Modules.Setting、H.Modules.Upgrade |
| `9 - Windows` | `Source/Windows` | 窗口层 | H.Windows.Main、H.Windows.Dialog、H.Windows.Dock、H.Windows.Ribbon |
| `10 - ApplicationBases` | `Source/ApplicationBases` | 应用基类 | 各应用组合基类（主题应用、模块应用等） |
| `11 - Templates` | `Source/Templates` | 模板层 | H.Templates.Default、H.Templates.Project |
| `12 - Tests` | `Source/Tests` | 测试示例 | H.Test.*（每个控件/模块的独立测试项目） |
| `13 - App` | `Source/App` | 完整应用 | H.App.AIDI、H.App.FileManager |

### 3.2 目录与解决方案文件夹对照

实际物理目录 `Source/` 下的子目录与解决方案文件夹并非一一对应，但通过 `.sln` 文件中的组织方式保持了逻辑分层。下表为物理目录对照：

```
Source/
├── Base/           → 基础层 (0 - Base)
├── Common/         → 公共层 (1 - Common)
├── Datas/          → 数据层 (2 - Datas)
├── Controls/       → 控件层 (3 - Controls)
├── Extensions/     → 扩展层 (4 - Extensions)
├── Services/       → 服务层 (5 - Services)
├── Providers/      → 提供者 (6 - Providers)
├── Themes/         → 主题层 (7 - Themes)
├── Styles/         → 样式层 (7 - Themes 的子集)
├── Modules/        → 模块层 (8 - Modules)
├── Windows/        → 窗口层 (9 - Windows)
├── ApplicationBases/ → 应用基类 (10 - ApplicationBases) [部分项目]
├── Templates/      → 模板层 (11 - Templates)
├── Tests/          → 测试示例 (12 - Tests)
└── App/            → 完整应用 (13 - App)
```

### 3.3 关键程序集说明

#### 基础层 (Base)

| 程序集 | 说明 | 关键类型 |
|--------|------|----------|
| H.Attach | 附加属性系统，提供 50+ 附加属性 | `Cattach`（静态分部类） |
| H.Mvvm | MVVM 基础设施 | `Bindable`（ViewModel 基类）、`RelayCommand` |
| H.ValueConverter | 值转换器集合 | `Converter`（静态类，包含多种转换器实例） |
| H.MarkupExtension | 标记扩展 | 各种 MarkupExtension 实现 |
| H.Globalization | 全球化/本地化 | 资源文件与国际化服务 |

#### 控件层 (Controls)

| 程序集 | 说明 | 关键控件 |
|--------|------|----------|
| H.Controls.Form | 表单控件，自动根据对象属性生成编辑界面 | `Form`（ItemsControl） |
| H.Controls.Dock | Dock 布局（基于 AvalonDock） | Dock 窗口管理器 |
| H.Controls.Diagram | 流程图控件，支持节点、连线、分组、缩放 | Diagram、Node、Link |
| H.Controls.Chart2D | 2D 图表控件 | Chart、GeoMap |
| H.Controls.PDF | PDF 查看器（基于 PdfiumViewer） | `PDFBox` |
| H.Controls.Vlc | 视频播放控件（基于 Vlc.DotNet） | `VlcPlayer` |
| H.Controls.PropertyGrid | 属性网格编辑器 | PropertyGrid |
| H.Controls.Step | 步骤/流程指示器 | `Step` |
| H.Controls.TagBox | 标签输入/展示控件 | `TagBox`、`Tag` |
| H.Controls.ROIBox | 感兴趣区域选择框 | `ROIBox` |
| H.Controls.Panel | 特殊面板布局 | `HexGrid`（六边形网格） |
| H.Controls.ColorBox | 颜色选择器 | 颜色选择服务 |
| H.Controls.ShapeBox | 形状视图 | 形状接口 `IView` |
| H.Controls.PDF | PDF 查看器 | `PDFBox` |

#### 模块层 (Modules)

| 程序集 | 说明 | 关键服务/类型 |
|--------|------|----------|
| H.Modules.Login | 登录/注册模块 | `ILoginService`、`LoginViewPresenter` |
| H.Modules.Project | 项目管理模块 | `IProjectService`、`ProjectService` |
| H.Modules.Theme | 主题设置模块 | `ILoadThemeOptionsService`、`ThemeOptions` |
| H.Modules.Setting | 系统设置模块 | 设置面板 UI |
| H.Modules.About | 关于页面 | `AboutOptions` |
| H.Modules.Home | 首页/欢迎页 | `HomeBox`、`HomeOptions` |
| H.Modules.Guide | 操作向导 | `GuideBox`、`GuideService` |
| H.Modules.Identity | 身份认证（用户、角色、权限） | 权限管理 UI |
| H.Modules.License | 许可证书管理 | 许可证验证 |
| H.Modules.Upgrade | 自动升级 | 升级服务 |
| H.Modules.Logger | 日志查看器 | 日志展示 |
| H.Modules.Feedback | 反馈模块 | 用户反馈 |
| H.Modules.Help | 帮助文档 | 帮助系统 |
| H.Modules.Sponsor | 赞助展示 | 二维码展示 |
| H.Modules.Style | 样式选择 | `StyleType` |
| H.Modules.Operation | 操作日志 | 操作记录 |

#### 提供者层 (Providers)

| 程序集 | 说明 | 关键类型 |
|--------|------|----------|
| H.Iocable | IOC 容器封装 | `Ioc`（静态服务定位器）、`IocExtension`、`IocBindable` |

#### 服务层 (Services)

| 程序集 | 说明 | 关键类型 |
|--------|------|----------|
| H.Services.Common | 公共服务入口 | `DbIoc`（数据库 IOC）、消息服务接口 |
| H.Services.AppPath | 应用路径服务 | `AppPaths`（全局路径入口） |
| H.Services.Logger | 日志服务 | `IocLog`、`LogType` |
| H.Services.Identity | 身份服务 | `IUser`、`IRole` |

#### 主题层 (Themes)

| 程序集 | 说明 | 关键文件 |
|--------|------|----------|
| H.Theme | 基础主题资源 | `ColorKeys.xaml`、`BrushKeys.xaml`、`FontSizeKeys.xaml`、`LayoutKeys.xaml` |
| H.Style | 默认控件样式 | `Controls/*.xaml`（每个 WPF 控件一个样式文件） |
| H.Styles.Bootstrap | Bootstrap 风格 | `Share.xaml` |
| H.Themes.Colors.Blue | 蓝色配色 | `Dark.xaml`、`Light.xaml` |
| H.Themes.Colors.Gray | 灰色配色 | `Dark.xaml`、`Light.xaml` |
| H.Themes.Colors.Purple | 紫色配色 | `Dark.xaml` |
| H.Themes.Colors.Copper | 铜色配色 | `Dark.xaml` |
| H.Themes.Colors.Accent | 强调色配色 | `Dark.xaml` |
| H.Themes.Colors.Office | Office 风格配色 | `Dark.xaml` |
| H.Themes.Colors.Web | Web 风格配色 | `JDStyle.xaml`、`LayUI.xaml`、`QQStyle.xaml`、`WeUI.xaml` |

#### 窗口层 (Windows)

| 程序集 | 说明 | 关键类型 |
|--------|------|----------|
| H.Windows.Main | 主窗口框架 | `IMainWindow`、`MainWindow`、`Extension` |
| H.Windows.Dialog | 对话框系统 | `Commands`、`DialogKeys` |
| H.Windows.Dock | Dock 窗口 | `DockWindow` |
| H.Windows.Ribbon | Ribbon 窗口 | `RibbonSet`、`Service` |

---

## 4. 核心基础设施

### 4.1 IOC 容器

IOC 容器是基于 `Microsoft.Extensions.DependencyInjection` 的封装，核心位于 `H.Iocable` 程序集。

#### Ioc 静态类（全局服务定位器）

`Ioc` 是一个静态服务定位器，在 `ApplicationBase.ConfigureServices` 阶段完成构建。

```csharp
// H.Iocable/Ioc.cs 核心 API
public static class Ioc
{
    // 获取服务提供者
    public static IServiceProvider Services => _services;

    // 构建容器（在 ApplicationBase 中调用）
    public static void Build(IServiceCollection serviceCollection);

    // 获取服务（默认抛出异常）
    public static T GetService<T>(bool throwIfNone = true);

    // 获取可选的（不抛异常）
    public static bool Exist<T>();

    // 运行时配置服务
    public static void ConfigureServices(Action<IServiceCollection> action);

    // 查找所有已注册的服务描述符
    public static IEnumerable<ServiceDescriptor> FindAll(Func<ServiceDescriptor, bool> predicate = null);

    // 获取所有可赋值给指定类型的服务实例
    public static IEnumerable<T> GetAssignableFromServices<T>(Func<T, bool> predicate = null);
}
```

#### Ioc<T> 抽象类（强类型单例访问器）

```csharp
// 带接口的访问器
public abstract class Ioc<T, Interface> where T : class, Interface
{
    public static T Instance => Ioc.GetService<Interface>() as T;
}

// 无接口的访问器
public abstract class Ioc<Interface>
{
    public static Interface Instance => Ioc.GetService<Interface>(false);
}
```

**使用示例：**

```csharp
// 定义服务接口
public interface IMyService
{
    void DoWork();
}

// 实现服务
public class MyService : IMyService
{
    public void DoWork() => Console.WriteLine("Working...");
}

// 注册服务（在 ConfigureServices 中）
services.AddSingleton<IMyService, MyService>();

// 使用服务（在业务代码中）
Ioc.GetService<IMyService>().DoWork();
```

#### DbIoc（数据库专用 IOC）

`DbIoc` 是专门为数据库上下文提供的独立 IOC 容器，与主 IOC 容器隔离但支持回退。

```csharp
public static class DbIoc
{
    public static IServiceProvider Services => _services;
    public static void ConfigureServices(Action<IServiceCollection> action);
    public static void Rebuild();
    public static T GetService<T>(bool throwIfNone = true);
}
```

#### IocExtension（XAML 标记扩展）

在 XAML 中通过 `IocExtension` 直接获取服务实例：

```xml
<Window xmlns:h="https://github.com/HeBianGu">
    <!-- 从 IOC 容器获取服务 -->
    <TextBlock Text="{h:Ioc {x:Type local:IMyService}}" />
</Window>
```

#### IocBindable（可绑定基类）

继承 `IocBindable` 的 ViewModel 可以自动从 IOC 容器解析服务：

```csharp
public class MyViewModel : IocBindable
{
    // IocBindable 内部自动从 DI 容器解析服务
    // 也可以手动调用 Ioc.GetService<T>()
}
```

### 4.2 MVVM 框架

MVVM 基础设施位于 `H.Mvvm` 程序集。

#### Bindable 基类

`Bindable` 是 ViewModel 的基类，继承自 `CommunityToolkit.Mvvm.ComponentModel.BindableBase`。

```csharp
// H.Mvvm/ViewModels/Base/Bindable.cs
public abstract class Bindable : BindableBase
{
    // 通用命令 - 调用 RelayMethod(object obj)
    public RelayCommand RelayCommand { get; set; }

    // Loaded 命令 - 在视图加载时触发
    public RelayCommand LoadedCommand => new RelayCommand(Loaded);

    // 方法调用命令 - 通过方法名调用
    public RelayCommand CallMethodCommand { get; set; }

    // 子类重写此方法处理 RelayCommand
    protected virtual void RelayMethod(object obj) { }

    // 加载事件处理
    protected virtual void Loaded(object obj) { }

    // 通过反射调用方法
    protected virtual void CallMethod(object obj) { }

    // 获取目标 UIElement
    protected T GetTargetElement<T>() where T : UIElement;
}
```

**典型用法：**

```csharp
public class MyViewModel : Bindable
{
    private string _name;
    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value); // 使用 SetProperty 触发通知
    }

    protected override void RelayMethod(object obj)
    {
        // 根据命令参数路由到不同逻辑
        switch (obj?.ToString())
        {
            case "Save": Save(); break;
            case "Load": Load(); break;
        }
    }

    private void Save() { /* 保存逻辑 */ }
    private void Load() { /* 加载逻辑 */ }
}
```

#### RelayCommand

```csharp
// 无参数命令
public class RelayCommand : ICommand
{
    public RelayCommand(Action<object> execute);
    public RelayCommand(Action<object> execute, Func<object, bool> canExecute);
}

// 泛型命令
public class RelayCommand<T> : ICommand
{
    public RelayCommand(Action<T> execute);
    public RelayCommand(Action<T> execute, Func<T, bool> canExecute);
}
```

**在 XAML 中使用：**

```xml
<Window xmlns:h="https://github.com/HeBianGu"
        xmlns:vm="clr-namespace:MyApp.ViewModels">
    <Window.DataContext>
        <vm:MyViewModel />
    </Window.DataContext>
    <StackPanel>
        <!-- 使用 RelayCommand（参数将传递给 RelayMethod） -->
        <Button Content="保存" Command="{Binding RelayCommand}" CommandParameter="Save" />
        <Button Content="加载" Command="{Binding RelayCommand}" CommandParameter="Load" />

        <!-- 使用 LoadedCommand -->
        <Grid h:Cattach.LoadedCommand="{Binding LoadedCommand}" />
    </StackPanel>
</Window>
```

### 4.3 附加属性系统（Cattach）

`Cattach` 是本控件的核心基础设施之一，位于 `H.Attach` 程序集。它是一个 `static partial class`，按功能分散在多个文件中：

| 文件 | 功能分类 | 包含的附加属性 |
|------|----------|---------------|
| `Cattach.cs` | 基础属性 | UseWatermark、Watermark、CornerRadius、Password、SelectedItems、IsBuzy、BuzyText、Path、IsDragEnter、DynamicResources |
| `Cattach.Attach.cs` | 样式继承属性 | Attach、AttachControlTemplate、AttachTemplate、AttachForeground、AttachBackground、AttachBorderBrush、AttachWidth、AttachBorderThickness、AttachHorizontalAlignment、AttachCornerRaius、AttachVerticalAlignment、AttachMargin、AttachHeight、AttachDock |
| `Cattach.Animation.cs` | 动画 | 动画相关附加属性 |
| `Cattach.Caption.cs` | 标题 | 标题栏相关 |
| `Cattach.Check.cs` | 选中状态 | 勾选相关 |
| `Cattach.Dock.cs` | Dock 布局 | Dock 相关 |
| `Cattach.Except.cs` | 异常处理 | 异常相关 |
| `Cattach.Focus.cs` | 焦点 | 焦点相关 |
| `Cattach.Guide.cs` | 向导提示 | GuideData、GuideTitle、UseGuide |
| `Cattach.Icon.cs` | 图标 | 图标相关 |
| `Cattach.Item.cs` | 项 | 项相关 |
| `Cattach.ItemsControl.cs` | 项容器 | ItemsControl 相关 |
| `Cattach.MouseOver.cs` | 鼠标悬停 | 鼠标悬停相关 |
| `Cattach.NewGuide.cs` | 新版向导 | 新版向导相关 |
| `Cattach.Press.cs` | 按下状态 | 按下相关 |
| `Cattach.Search.cs` | 搜索 | 搜索相关 |
| `Cattach.Select.cs` | 选中状态 | 选中相关 |
| `Cattach.Title.cs` | 标题 | 标题相关 |

**典型使用示例：**

```xml
<!-- watermark 水印 -->
<TextBox h:Cattach.Watermark="请输入用户名" h:Cattach.UseWatermark="True" />

<!-- 圆角 -->
<Border h:Cattach.CornerRadius="8,8,0,0" />

<!-- 密码框绑定 -->
<PasswordBox h:Cattach.Password="{Binding Password}" />

<!-- ListBox 多选绑定 -->
<ListBox h:Cattach.SelectedItems="{Binding SelectedItems}" SelectionMode="Multiple" />

<!-- 忙状态指示 -->
<Grid h:Cattach.IsBuzy="{Binding IsLoading}" h:Cattach.BuzyText="处理中..." />

<!-- 附加样式继承 -->
<StackPanel h:Cattach.AttachMargin="5" h:Cattach.AttachCornerRaius="4">
    <Button Content="按钮1" />
    <Button Content="按钮2" /><!-- 自动继承上方设置的附加属性 -->
</StackPanel>

<!-- 向导提示 -->
<TextBox h:Cattach.GuideTitle="用户名" h:Cattach.GuideData="请输入您的用户名" h:Cattach.UseGuide="True" />
```

### 4.4 应用启动流程

应用启动流程由 `H.Extensions.ApplicationBase` 中的 `ApplicationBase` 类管理。

#### 标准启动顺序

```
App.Main()
  → ApplicationBase 构造函数
    → 创建 IServiceCollection
    → 调用 ConfigureServices(services) 注册服务
    → 构建依赖注入容器 (Ioc.Build)
    → 调用 Configure(app) 应用配置
    → 加载主题
    → 加载模块设置
    → 显示主窗口
```

#### 典型 App.xaml.cs

```csharp
// 使用 ThemesApplicationBase（带主题支持的应用基类）
public partial class App : ThemesApplicationBase
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);

        // 注册项目服务
        services.AddProject();

        // 注册主题服务
        services.AddTheme();

        // 注册登录服务
        services.AddLoginViewPresenter();
        services.AddTestLoginService();

        // 注册自定义服务
        services.AddSingleton<IMyService, MyService>();
    }

    protected override void Configure(IApplicationBuilder app)
    {
        base.Configure(app);

        // 配置项目选项
        app.UseProjectOptions(options =>
        {
            options.SaveMode = ProjectSaveMode.OnAppExit;
        });

        // 配置主题选项
        app.UseThemeOptions(options =>
        {
            options.UseDefault = true;
        });
    }
}
```

#### 注册模式规范

每个模块/服务都遵循固定的注册模式：

1. **`AddXxx()` 扩展方法** — 在 `IServiceCollection` 上注册服务
   ```csharp
   public static IServiceCollection AddProject(this IServiceCollection services, Action<IProjectOptions> setupAction = null)
   ```

2. **`UseXxxOptions()` 扩展方法** — 在 `IApplicationBuilder` 上配置选项
   ```csharp
   public static IApplicationBuilder UseProjectOptions(this IApplicationBuilder builder, Action<ProjectOptions> option = null)
   ```

3. **服务通过 `TryAdd` 注册** — 允许使用者替换默认实现
   ```csharp
   services.TryAdd(ServiceDescriptor.Singleton<IProjectService, ProjectService>());
   ```

4. **选项通过 `IOptions<T>` 模式配置**
   ```csharp
   services.Configure(new Action<ProjectOptions>(setupAction));
   ```

5. **选项实例注册到 `IocSetting.Instance`**
   ```csharp
   IocSetting.Instance.Add(ProjectOptions.Instance);
   ```

---

## 5. 主题与样式系统

主题系统位于 `Source/Themes` 和 `Source/Styles`，采用分层设计。

### 5.1 主题体系架构

```
H.Theme (基础主题资源层)
├── ColorKeys.xaml       — 颜色键定义（Dark.xaml / Light.xaml 的桥梁）
├── BrushKeys.xaml       — 画刷键定义（引用 ColorKeys）
├── FontSizeKeys.xaml    — 字体尺寸键定义
├── LayoutKeys.xaml      — 布局尺寸键定义
├── SystemKeys.xaml      — 系统键定义
└── Themes/Generic.xaml  — 主题通用资源

H.Themes.Colors.* (颜色主题扩展)
├── Blue/Dark.xaml, Light.xaml
├── Gray/Dark.xaml, Light.xaml
├── Purple/Dark.xaml
├── Copper/Dark.xaml
├── Accent/Dark.xaml
└── ...

H.Style (控件样式层)
├── Controls/*.xaml     — 每个 WPF 控件对应一个样式文件
├── Themes/Generic.xaml  — 通用主题入口
├── ConciseControls.xaml — 简洁控件风格
├── NoneControls.xaml    — 无样式/裸控件风格
└── Share.xaml           — 共享资源

H.Styles.* (风格扩展)
├── Bootstrap/Share.xaml — Bootstrap 风格
└── ...
```

### 5.2 资源键约定

所有主题资源键遵循 `S.Xxxx.Yyyy` 命名约定：

| 资源键 | 类型 | 说明 |
|--------|------|------|
| `S.Brush.Background` | Brush | 默认背景色画刷 |
| `S.Brush.Foreground` | Brush | 默认前景色画刷 |
| `S.Brush.BorderBrush` | Brush | 默认边框色画刷 |
| `S.Brush.Card` | Brush | 卡片背景色画刷 |
| `S.FontSize.Default` | double | 默认字体尺寸 |
| `S.FontSize.Small` | double | 小字体尺寸 |
| `S.FontSize.Large` | double | 大字体尺寸 |
| `S.CornerRadius.Default` | CornerRadius | 默认圆角大小 |
| `S.CornerRadius.Button` | CornerRadius | 按钮圆角大小 |
| `S.Layout.Default` | double | 默认布局尺寸 |
| `S.Layout.Small` | double | 紧凑布局尺寸 |
| `S.Layout.Large` | double | 宽松布局尺寸 |

### 5.3 引入主题资源

```xml
<ResourceDictionary.MergedDictionaries>
    <ResourceDictionary Source="pack://application:,,,/H.Theme;component/ColorKeys.xaml" />
    <ResourceDictionary Source="pack://application:,,,/H.Theme;component/BrushKeys.xaml" />
    <ResourceDictionary Source="pack://application:,,,/H.Theme;component/FontSizeKeys.xaml" />
    <ResourceDictionary Source="pack://application:,,,/H.Theme;component/LayoutKeys.xaml" />
</ResourceDictionary.MergedDictionaries>
```

### 5.4 使用主题资源

```xml
<!-- 支持主题切换的使用 DynamicResource -->
<Border Background="{DynamicResource S.Brush.Background}"
        CornerRadius="{DynamicResource S.CornerRadius.Default}">
    <TextBlock Foreground="{DynamicResource S.Brush.Foreground}"
               FontSize="{DynamicResource S.FontSize.Default}"
               Text="主题化文本" />
</Border>

<!-- 不随主题变化的资源使用 StaticResource -->
<Border Background="{StaticResource S.Brush.Card}" />
```

### 5.5 编程方式切换主题

```csharp
// 切换字体尺寸
FontSizeThemeType.Large.ChangeFontSizeThemeType();
FontSizeThemeType.Small.ChangeFontSizeThemeType();

// 切换布局尺寸
LayoutThemeType.Small.ChangeLayoutThemeType();
LayoutThemeType.Large.ChangeLayoutThemeType();

// 刷新画刷资源（切换颜色主题后调用）
ThemeTypeExtension.RefreshBrushResourceDictionary();
```

### 5.6 添加新控件样式

在 `H.Style/Controls/` 下创建对应的 XAML 文件，遵循以下规范：

```xml
<!-- 文件：H.Style/Controls/Button.xaml -->
<ResourceDictionary xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">
    
    <!-- 按钮的默认样式 -->
    <Style TargetType="{x:Type Button}">
        <Setter Property="Background" Value="{DynamicResource S.Brush.Background}" />
        <Setter Property="Foreground" Value="{DynamicResource S.Brush.Foreground}" />
        <Setter Property="BorderBrush" Value="{DynamicResource S.Brush.BorderBrush}" />
        <Setter Property="Padding" Value="12,6" />
        <Setter Property="Template">
            <Setter.Value>
                <ControlTemplate TargetType="{x:Type Button}">
                    <Border Background="{TemplateBinding Background}"
                            BorderBrush="{TemplateBinding BorderBrush}"
                            BorderThickness="{TemplateBinding BorderThickness}"
                            CornerRadius="{DynamicResource S.CornerRadius.Default}">
                        <ContentPresenter HorizontalAlignment="Center"
                                          VerticalAlignment="Center" />
                    </Border>
                    <ControlTemplate.Triggers>
                        <Trigger Property="IsMouseOver" Value="True">
                            <Setter Property="Background" Value="{DynamicResource S.Brush.Hover}" />
                        </Trigger>
                        <Trigger Property="IsPressed" Value="True">
                            <Setter Property="Background" Value="{DynamicResource S.Brush.Pressed}" />
                        </Trigger>
                    </ControlTemplate.Triggers>
                </ControlTemplate>
            </Setter.Value>
        </Setter>
    </Style>
</ResourceDictionary>
```

然后在 `Themes/Generic.xaml` 中合并：

```xml
<ResourceDictionary>
    <ResourceDictionary.MergedDictionaries>
        <ResourceDictionary Source="/H.Style;component/Controls/Button.xaml" />
        <ResourceDictionary Source="/H.Style;component/Controls/TextBox.xaml" />
        <!-- 其他控件样式 -->
    </ResourceDictionary.MergedDictionaries>
</ResourceDictionary>
```

---

## 6. 控件库详解

### 6.1 Form 控件（H.Controls.Form）

`Form` 控件是一个 `ItemsControl`，能够根据绑定的数据对象自动生成属性编辑界面。

#### 核心特性

- 自动识别属性类型（string、int、bool、enum、DateTime 等）并生成对应编辑器
- 支持 `[Display(Name="显示名")]`、`[Range(min,max)]`、`[Browsable(false)]` 等数据注解
- 支持分组显示（`UseGroup = True`）
- 支持搜索过滤
- 支持属性值变更事件通知
- 支持属性编辑器定制

#### 基本用法

```csharp
// 数据模型
public class ExportOptions
{
    [Display(Name = "文件名")]
    public string FileName { get; set; } = "output.png";

    [Display(Name = "宽度")]
    [Range(100, 10000)]
    public int Width { get; set; } = 1920;

    [Display(Name = "高度")]
    [Range(100, 10000)]
    public int Height { get; set; } = 1080;

    [Display(Name = "格式")]
    public ExportFormat Format { get; set; } = ExportFormat.PNG;

    [Display(Name = "保持比例")]
    public bool KeepAspectRatio { get; set; } = true;
}

public enum ExportFormat { PNG, JPG, BMP, TIFF }
```

```xml
<!-- XAML 用法 -->
<h:Form x:Name="form" SelectObject="{Binding ExportOptions}"
        UseGroup="True" UseEnum="True" UseBoolen="True" />
```

#### Form 关键依赖属性

| 属性 | 类型 | 说明 |
|------|------|------|
| `SelectObject` | object | 要编辑的数据对象 |
| `SearchText` | string | 搜索关键字，过滤匹配的属性 |
| `TitleWidth` | double | 标题列宽度 |
| `MessageWidth` | double | 错误消息列宽度 |
| `UseEnum` | bool | 是否显示枚举属性（默认 true） |
| `UseString` | bool | 是否显示字符串属性（默认 true） |
| `UseBoolen` | bool | 是否显示布尔属性（默认 true） |
| `UseDateTime` | bool | 是否显示日期属性（默认 true） |
| `UseCommand` | bool | 是否显示命令属性 |
| `UseClass` | bool | 是否显示类类型属性 |
| `UseArray` | bool | 是否显示数组/集合属性 |
| `UseGroup` | bool | 是否启用分组显示 |
| `UseSetDefault` | bool | 是否显示恢复默认值按钮 |
| `UseOrder` | bool | 是否按 [Order] 排序 |
| `UseTitle` | bool | 是否显示属性名称列 |
| `UseDisplayOnly` | bool | 仅显示标记了 DisplayAttribute 的属性 |
| `UseDeclaredOnly` | bool | 仅显示当前类型声明的属性（不包含基类） |
| `UsePropertyView` | bool | 使用属性视图模式 |
| `ExceptPropertyNames` | string | 排除的属性名（逗号分隔） |
| `UsePropertyNames` | string | 仅显示的属性名（逗号分隔） |
| `UseGroupNames` | string | 仅显示的组名（逗号分隔） |

### 6.2 Diagram 流程图控件（H.Controls.Diagram）

流程图控件由多个程序集共同组成：

- `H.Controls.Diagram` — 核心控件（节点、连线、选择、拖拽、缩放）
- `H.Controls.Diagram.Presenter` — 数据驱动 Presenter 层

#### 三层架构

1. **控件层**：Diagram 画布、Node 节点、Link 连线、选择框、缩放、布局
2. **数据层**：`NodeData`、`LinkData`、`DiagramData`、保存/加载
3. **Presenter 层**：`NodeData` 子类、`NodeDataGroup`、`DataTemplate`、工具箱

#### 自定义节点

```csharp
// 创建自定义节点数据
public class DelayNodeData : NodeData
{
    public int Milliseconds { get; set; } = 1000;

    public DelayNodeData()
    {
        this.Name = "延时";
        this.Description = "等待指定毫秒数后继续执行";
    }
}

// 创建节点分组
public class MyNodeGroup : NodeDataGroupBase
{
    public override IEnumerable<INodeData> CreateNodeDatas()
    {
        yield return new DelayNodeData();
        yield return new LogNodeData();
        yield return new HttpRequestNodeData();
    }
}
```

#### 自定义节点外观

```xml
<DataTemplate DataType="{x:Type local:DelayNodeData}">
    <Border Background="#FFE0F7E0" CornerRadius="6" Padding="12">
        <StackPanel>
            <TextBlock Text="{Binding Name}" FontWeight="Bold" />
            <TextBlock Text="{Binding Description}" Opacity="0.7" />
            <StackPanel Orientation="Horizontal">
                <TextBlock Text="延时：" />
                <TextBox Text="{Binding Milliseconds}" Width="80" />
                <TextBlock Text="ms" />
            </StackPanel>
        </StackPanel>
    </Border>
</DataTemplate>
```

### 6.3 Step 步骤控件（H.Controls.Step）

步骤/流程指示器控件，用于显示多步骤进度。

```xml
<h:Step x:Name="stepControl"
        Steps="{Binding Steps}"
        SelectedIndex="{Binding CurrentStep}"
        StepState="{Binding StepState}" />
```

步骤数据模型：

```csharp
public class StepState
{
    public string Title { get; set; }
    public string Description { get; set; }
    public StepStateEnum State { get; set; } // Pending, Processing, Completed, Error
}
```

### 6.4 TagBox 标签控件（H.Controls.TagBox）

标签输入和展示控件。

```xml
<!-- 标签展示 -->
<h:TagBox ItemsSource="{Binding Tags}" />

<!-- 标签项模板 -->
<DataTemplate DataType="{x:Type h:Tag}">
    <Border CornerRadius="12" Background="{DynamicResource S.Brush.Card}">
        <TextBlock Text="{Binding Name}" Margin="8,2" />
    </Border>
</DataTemplate>
```

### 6.5 PDF 控件（H.Controls.PDF）

基于 PdfiumViewer 的 PDF 查看器。

```xml
<h:PDFBox x:Name="pdfViewer" FilePath="{Binding PdfFilePath}" />
```

### 6.6 VLC 视频控件（H.Controls.Vlc）

基于 Vlc.DotNet 的视频播放控件。

```xml
<h:VlcPlayer x:Name="vlcPlayer"
             Source="{Binding VideoPath}"
             IsPlaying="{Binding IsPlaying}" />
```

---

## 7. 模块系统

模块是 WPF-Control 的核心组织方式，每个模块封装一组相关功能，提供注册方法和配置选项。

### 7.1 模块结构规范

每个模块遵循统一的结构：

```
H.Modules.Xxx/
├── Extension.cs          — 服务注册扩展方法
├── XxxOptions.cs         — 配置选项类
├── IXxxOptions.cs        — 配置选项接口
├── XxxService.cs         — 服务实现
├── Presenters/           — Presenter 层（可选）
│   └── XxxPresenter.cs
├── Views/                — 视图（可选）
│   ├── XxxView.xaml
│   └── XxxView.xaml.cs
└── AssemblyInfo.cs       — 程序集信息
```

### 7.2 Extension.cs 注册模式

每个模块的 `Extension.cs` 提供两类扩展方法：

```csharp
// Extension.cs 完整规范示例
namespace System;  // 统一放在 System 命名空间

public static class Extension
{
    // 1. 服务注册方法（泛型版本，允许替换实现）
    public static IServiceCollection AddMyModule<T>(this IServiceCollection services,
        Action<IMyModuleOptions> setupAction = null) where T : class, IMyModuleService
    {
        services.AddOptions();
        services.TryAdd(ServiceDescriptor.Singleton<IMyModuleService, T>());
        services.TryAdd(ServiceDescriptor.Singleton<IMyModuleViewPresenter, MyModuleViewPresenter>());
        if (setupAction != null)
            services.Configure(new Action<MyModuleOptions>(setupAction));
        return services;
    }

    // 2. 便捷重载（使用默认实现）
    public static IServiceCollection AddMyModule(this IServiceCollection services,
        Action<IMyModuleOptions> setupAction = null)
    {
        return services.AddMyModule<MyModuleService>(setupAction);
    }

    // 3. 配置方法
    public static IApplicationBuilder UseMyModuleOptions(this IApplicationBuilder builder,
        Action<IMyModuleOptions> option = null)
    {
        IocSetting.Instance.Add(MyModuleOptions.Instance);
        option?.Invoke(MyModuleOptions.Instance);
        return builder;
    }
}
```

### 7.3 Options 模式

```csharp
// 接口定义
public interface IMyModuleOptions
{
    bool EnableFeature { get; set; }
    string DefaultPath { get; set; }
}

// 类实现（单例 + 默认值）
public class MyModuleOptions : IMyModuleOptions
{
    public static MyModuleOptions Instance { get; } = new MyModuleOptions();

    public bool EnableFeature { get; set; } = true;
    public string DefaultPath { get; set; } = "Data";
}
```

### 7.4 登录模块详解（示例）

登录模块是模块化开发的典型范例：

**注册方式：**

```csharp
// App.ConfigureServices
services.AddLoginViewPresenter(options =>
{
    options.AdminName = "admin";
});
services.AddTestLoginService();
```

**服务接口：**

```csharp
public interface ILoginService
{
    bool Login(string name, string password, out string message);
    bool Logout(out string message);
    IUser User { get; }
}
```

**Presenter 接口：**

```csharp
public interface ILoginViewPresenter
{
    // 提供登录界面的 ViewModel 和 View
}
```

**配置选项：**

```csharp
public class LoginOptions : ILoginOptions
{
    public static LoginOptions Instance { get; } = new LoginOptions();
    public string AdminName { get; set; } = "admin";
    public string AdminPassword { get; set; } = "admin";
}
```

### 7.5 项目模块详解

**注册方式：**

```csharp
services.AddProject(options =>
{
    options.SaveMode = ProjectSaveMode.OnAppExit;
});
app.UseProjectOptions();
```

**核心服务接口：**

```csharp
public interface IProjectService
{
    Task<bool> LoadAsync(string path);
    Task<bool> SaveAsync(string path = null);
    Task<bool> NewAsync(string path);
    bool IsOpened { get; }
    string CurrentPath { get; }
}
```

**保存模式：**

- `ProjectSaveMode.OnAppExit` — 应用退出时保存
- `ProjectSaveMode.OnProjectChanged` — 项目内容变化时自动保存
- `ProjectSaveMode.Manual` — 手动保存

---

## 8. 服务层

服务层位于 `Source/Services`，提供服务抽象和默认实现。

### 8.1 AppPaths（路径服务）

`AppPaths` 提供全局路径入口，统一管理应用目录。

```csharp
public static class AppPaths
{
    // 应用数据目录（用户可写）
    public string UserDataPath { get; }

    // 配置目录
    public string ConfigPath { get; }

    // 缓存目录
    public string CachePath { get; }

    // 日志目录
    public string LogPath { get; }

    // 临时目录
    public string TempPath { get; }
}
```

**使用建议：**

```csharp
// ❌ 不推荐：硬编码路径
string file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config", "app.json");

// ✅ 推荐：通过路径服务
public class MyConfigStore
{
    public string GetConfigFile()
    {
        return Path.Combine(AppPaths.Instance.UserDataPath, "Config", "app.json");
    }
}
```

### 8.2 日志服务

```csharp
public class IocLog
{
    // 调试日志
    public static void Debug(string message);
    // 信息日志
    public static void Info(string message);
    // 警告日志
    public static void Warn(string message);
    // 错误日志
    public static void Error(string message);
    // 致命错误
    public static void Fatal(string message);
}

public enum LogType
{
    Debug,
    Info,
    Warn,
    Error,
    Fatal
}
```

### 8.3 身份服务

```csharp
public interface IUser
{
    string Id { get; set; }
    string Name { get; set; }
    string DisplayName { get; set; }
    string Avatar { get; set; }
}

public interface IRole
{
    string Id { get; set; }
    string Name { get; set; }
    IList<IPermission> Permissions { get; set; }
}
```

---

## 9. 扩展机制

### 9.1 扩展方法（Extension 类）

项目中广泛使用 `Extension` 类（注意命名不一致有些叫 `Extention`、`Extension`）提供 `IServiceCollection` 和 `IApplicationBuilder` 的扩展方法。

**推荐约定：**

```csharp
// 统一命名空间
namespace System;

public static class Extension
{
    // 注册服务
    public static IServiceCollection AddXxx(this IServiceCollection services, ...);
    
    // 配置选项
    public static IApplicationBuilder UseXxxOptions(this IApplicationBuilder app, ...);
}
```

### 9.2 Presenter 模式

Presenter 模式是数据驱动 UI 的核心设计模式。

**三层架构：**

1. **数据模型（Data）**：描述业务数据和状态
2. **Presenter**：组织数据和命令，为 View 提供所需的一切
3. **DataTemplate**：定义 Presenter 的显示方式

**完整示例：**

```csharp
// 1. Presenter 类
public class UserCardPresenter
{
    public string Name { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string Avatar { get; set; } = string.Empty;
    public ICommand OpenCommand { get; set; }
    public ICommand DeleteCommand { get; set; }
}

// 2. ViewModel 中创建
public class UserListViewModel : Bindable
{
    public ObservableCollection<UserCardPresenter> Users { get; }
        = new ObservableCollection<UserCardPresenter>();

    public UserListViewModel()
    {
        // 从数据源加载并创建 Presenter
        foreach (var user in LoadUsers())
        {
            Users.Add(new UserCardPresenter
            {
                Name = user.Name,
                Role = user.Role,
                OpenCommand = new RelayCommand(p => OpenUser(user.Id)),
                DeleteCommand = new RelayCommand(p => DeleteUser(user.Id))
            });
        }
    }
}
```

```xml
<!-- 3. DataTemplate 定义外观 -->
<DataTemplate DataType="{x:Type local:UserCardPresenter}">
    <Border Padding="12" CornerRadius="8" Background="{DynamicResource S.Brush.Card}"
            Margin="4">
        <Grid>
            <Grid.ColumnDefinitions>
                <ColumnDefinition Width="Auto" />
                <ColumnDefinition Width="*" />
                <ColumnDefinition Width="Auto" />
            </Grid.ColumnDefinitions>
            <Ellipse Width="40" Height="40" Fill="{DynamicResource S.Brush.Accent}" />
            <StackPanel Grid.Column="1" Margin="8,0">
                <TextBlock Text="{Binding Name}" FontWeight="Bold" />
                <TextBlock Text="{Binding Role}" Opacity="0.7" FontSize="12" />
            </StackPanel>
            <StackPanel Grid.Column="2" Orientation="Horizontal">
                <Button Content="打开" Command="{Binding OpenCommand}" Margin="2" />
                <Button Content="删除" Command="{Binding DeleteCommand}" Margin="2" />
            </StackPanel>
        </Grid>
    </Border>
</DataTemplate>
```

### 9.3 消息服务（IocMessage）

`IocMessage` 是消息/对话框服务的统一入口，让 ViewModel 无需直接操作窗口。

```csharp
// 显示 Snack 消息
await IocMessage.ShowSnackInfo("保存成功");
await IocMessage.ShowSnackError("操作失败");

// 显示确认对话框
bool result = await IocMessage.ShowDialogSure("确认要删除吗？");

// 显示自定义对话框
object view = Ioc.Services.GetService(typeof(MySettingsView));
await IocMessage.ShowDialog(view, x =>
{
    x.Title = "系统设置";
    x.DialogButton = DialogButton.Sumit;
});
```

### 9.4 ITree 接口（树形数据抽象）

```csharp
public interface ITree
{
    IEnumerable GetChildren(object parent);
}
```

用于树形数据的统一抽象，支持 TreeView、下拉选择树等场景。

---

## 10. 二次开发实战

### 10.1 创建新模块

以创建一个 "通知推送模块" `H.Modules.Notification` 为例：

#### 步骤 1：创建项目目录和 csproj

```
Source/Modules/H.Modules.Notification/
├── H.Modules.Notification.csproj
├── AssemblyInfo.cs
├── Extension.cs
├── INotificationService.cs
├── NotificationService.cs
├── NotificationOptions.cs
├── Presenters/
│   ├── NotificationViewPresenter.cs
│   └── NotificationListViewPresenter.cs
└── Views/
    ├── NotificationView.xaml
    └── NotificationView.xaml.cs
```

#### 步骤 2：创建 csproj

```xml
<Project Sdk="Microsoft.NET.Sdk">
    <PropertyGroup>
        <RootNamespace>H.Modules.Notification</RootNamespace>
        <AssemblyName>H.Modules.Notification</AssemblyName>
    </PropertyGroup>
    <ItemGroup>
        <ProjectReference Include="..\..\Base\H.Mvvm\H.Mvvm.csproj" />
        <ProjectReference Include="..\..\Providers\H.Iocable\H.Iocable.csproj" />
        <ProjectReference Include="..\..\Services\H.Services.Common\H.Services.Common.csproj" />
    </ItemGroup>
</Project>
```

#### 步骤 3：定义接口

```csharp
// INotificationService.cs
public interface INotificationService
{
    Task SendAsync(string title, string message);
    Task ScheduleAsync(string title, string message, DateTime time);
}
```

#### 步骤 4：实现服务

```csharp
// NotificationService.cs
internal class NotificationService : INotificationService
{
    public async Task SendAsync(string title, string message)
    {
        // 实现发送通知逻辑
        await IocLog.Info($"发送通知: {title} - {message}");
    }

    public async Task ScheduleAsync(string title, string message, DateTime time)
    {
        // 实现定时通知逻辑
        await Task.Delay(time - DateTime.Now);
        await SendAsync(title, message);
    }
}
```

#### 步骤 5：定义选项

```csharp
// NotificationOptions.cs
public interface INotificationOptions
{
    bool EnableSound { get; set; }
    bool EnableToast { get; set; }
    int MaxRetryCount { get; set; }
}

public class NotificationOptions : INotificationOptions
{
    public static NotificationOptions Instance { get; } = new NotificationOptions();
    public bool EnableSound { get; set; } = true;
    public bool EnableToast { get; set; } = true;
    public int MaxRetryCount { get; set; } = 3;
}
```

#### 步骤 6：实现注册扩展

```csharp
// Extension.cs
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.DependencyInjection.Extensions;
using H.Services.Setting;

namespace System;

public static class Extension
{
    public static IServiceCollection AddNotification<T>(this IServiceCollection services,
        Action<INotificationOptions> setupAction = null) where T : class, INotificationService
    {
        services.AddOptions();
        services.TryAdd(ServiceDescriptor.Singleton<INotificationService, T>());
        services.TryAdd(ServiceDescriptor.Singleton<INotificationViewPresenter, NotificationViewPresenter>());
        if (setupAction != null)
            services.Configure(new Action<NotificationOptions>(setupAction));
        return services;
    }

    public static IServiceCollection AddNotification(this IServiceCollection services,
        Action<INotificationOptions> setupAction = null)
    {
        return services.AddNotification<NotificationService>(setupAction);
    }

    public static IApplicationBuilder UseNotificationOptions(this IApplicationBuilder builder,
        Action<INotificationOptions> option = null)
    {
        IocSetting.Instance.Add(NotificationOptions.Instance);
        option?.Invoke(NotificationOptions.Instance);
        return builder;
    }
}
```

#### 步骤 7：在 App 中使用

```csharp
protected override void ConfigureServices(IServiceCollection services)
{
    base.ConfigureServices(services);
    services.AddNotification(options =>
    {
        options.EnableSound = true;
        options.EnableToast = true;
    });
}

protected override void Configure(IApplicationBuilder app)
{
    base.Configure(app);
    app.UseNotificationOptions();
}
```

### 10.2 创建自定义控件

以创建一个 "评分控件 `RatingControl`" 为例：

#### 控件代码

```csharp
// RatingControl.cs
[TemplatePart(Name = PART_Root, Type = typeof(Grid))]
public class RatingControl : Control
{
    private const string PART_Root = "PART_Root";

    static RatingControl()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(RatingControl),
            new FrameworkPropertyMetadata(typeof(RatingControl)));
    }

    // 评分值
    public static readonly DependencyProperty ValueProperty =
        DependencyProperty.Register(nameof(Value), typeof(double), typeof(RatingControl),
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                OnValueChanged, CoerceValue));

    // 最大分值
    public static readonly DependencyProperty MaximumProperty =
        DependencyProperty.Register(nameof(Maximum), typeof(int), typeof(RatingControl),
            new FrameworkPropertyMetadata(5, OnMaximumChanged));

    // 星号数量
    public static readonly DependencyProperty StarCountProperty =
        DependencyProperty.Register(nameof(StarCount), typeof(int), typeof(RatingControl),
            new FrameworkPropertyMetadata(5));

    // 是否只读
    public static readonly DependencyProperty IsReadOnlyProperty =
        DependencyProperty.Register(nameof(IsReadOnly), typeof(bool), typeof(RatingControl),
            new FrameworkPropertyMetadata(false));

    // 星号大小
    public static readonly DependencyProperty StarSizeProperty =
        DependencyProperty.Register(nameof(StarSize), typeof(double), typeof(RatingControl),
            new FrameworkPropertyMetadata(24.0));

    public double Value
    {
        get => (double)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    public int Maximum
    {
        get => (int)GetValue(MaximumProperty);
        set => SetValue(MaximumProperty, value);
    }

    public int StarCount
    {
        get => (int)GetValue(StarCountProperty);
        set => SetValue(StarCountProperty, value);
    }

    public bool IsReadOnly
    {
        get => (bool)GetValue(IsReadOnlyProperty);
        set => SetValue(IsReadOnlyProperty, value);
    }

    public double StarSize
    {
        get => (double)GetValue(StarSizeProperty);
        set => SetValue(StarSizeProperty, value);
    }

    private static object CoerceValue(DependencyObject d, object baseValue)
    {
        var control = (RatingControl)d;
        double value = (double)baseValue;
        return Math.Max(0, Math.Min(value, control.Maximum));
    }

    private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        // 值变更后的处理
    }

    private static void OnMaximumChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        // 最大值变更后重新检查当前值
        d.CoerceValue(ValueProperty);
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        // 从模板中获取 PART_Root
        var root = GetTemplateChild(PART_Root) as Grid;
    }
}
```

#### 默认样式

```xml
<!-- Generic.xaml 或主题控件样式目录 -->
<ResourceDictionary xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
                    xmlns:local="clr-namespace:MyControls">
    <Style TargetType="{x:Type local:RatingControl}">
        <Setter Property="Template">
            <Setter.Value>
                <ControlTemplate TargetType="{x:Type local:RatingControl}">
                    <Grid x:Name="PART_Root" Background="Transparent">
                        <ItemsControl ItemsSource="{Binding Stars, RelativeSource={RelativeSource TemplatedParent}}">
                            <ItemsControl.ItemsPanel>
                                <ItemsPanelTemplate>
                                    <StackPanel Orientation="Horizontal" />
                                </ItemsPanelTemplate>
                            </ItemsControl.ItemsPanel>
                            <ItemsControl.ItemTemplate>
                                <DataTemplate>
                                    <Path Data="M...星号Path..." Fill="{Binding Fill}"
                                          Width="{Binding Size}" Height="{Binding Size}"
                                          Margin="2,0" />
                                </DataTemplate>
                            </ItemsControl.ItemTemplate>
                        </ItemsControl>
                    </Grid>
                </ControlTemplate>
            </Setter.Value>
        </Setter>
    </Style>
</ResourceDictionary>
```

#### 注册控件到主题

在 `Themes/Generic.xaml` 中合并控件样式：

```xml
<ResourceDictionary.MergedDictionaries>
    <ResourceDictionary Source="pack://application:,,,/MyControls;component/Generic.xaml" />
</ResourceDictionary.MergedDictionaries>
```

### 10.3 编写单元测试示例

```csharp
// 测试 Bindable 基类
[TestClass]
public class BindableTests
{
    [TestMethod]
    public void TestPropertyChanged()
    {
        var vm = new TestViewModel();
        bool changed = false;
        vm.PropertyChanged += (s, e) => { if (e.PropertyName == "Name") changed = true; };
        vm.Name = "Test";
        Assert.IsTrue(changed);
        Assert.AreEqual("Test", vm.Name);
    }

    [TestMethod]
    public void TestRelayCommand()
    {
        var vm = new TestViewModel();
        string result = null;
        vm.RelayCommand = new RelayCommand(p => result = p?.ToString());
        vm.RelayCommand.Execute("Hello");
        Assert.AreEqual("Hello", result);
    }
}

public class TestViewModel : Bindable
{
    private string _name;
    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }
}
```

### 10.4 自定义主题

#### 创建新的配色方案

1. 在 `Source/Themes/` 下创建目录 `H.Themes.Colors.MyColor/`
2. 创建 `Dark.xaml` 和/或 `Light.xaml`

```xml
<!-- H.Themes.Colors.MyColor/Dark.xaml -->
<ResourceDictionary xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">
    <!-- 覆盖颜色键 -->
    <Color x:Key="S.Color.Primary">#FF6C5CE7</Color>
    <Color x:Key="S.Color.Background">#FF1A1A2E</Color>
    <Color x:Key="S.Color.Surface">#FF16213E</Color>
    <Color x:Key="S.Color.Text">#FFE0E0E0</Color>
</ResourceDictionary>
```

#### 创建新的控件风格

1. 在 `Source/Styles/` 下创建目录 `H.Styles.MyStyle/`
2. 创建 `Share.xaml` 和 `Extension.cs`

```csharp
// Extension.cs
public static class Extension
{
    public static IServiceCollection AddMyStyle(this IServiceCollection services)
    {
        // 注册自定义风格资源
        return services;
    }
}
```

### 10.5 集成第三方库

以集成一个图表库为例：

#### 步骤 1：添加 NuGet 引用

```xml
<ItemGroup>
    <PackageReference Include="LiveChartsCore.SkiaSharpView.WPF" Version="2.0.0-*" />
</ItemGroup>
```

#### 步骤 2：创建封装控件

```csharp
// H.Controls.LiveChart/ChartBox.cs
public class ChartBox : ContentControl
{
    static ChartBox()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(ChartBox),
            new FrameworkPropertyMetadata(typeof(ChartBox)));
    }

    public static readonly DependencyProperty SeriesProperty =
        DependencyProperty.Register(nameof(Series), typeof(IEnumerable<ISeries>),
            typeof(ChartBox), new PropertyMetadata(null, OnSeriesChanged));

    public IEnumerable<ISeries> Series
    {
        get => (IEnumerable<ISeries>)GetValue(SeriesProperty);
        set => SetValue(SeriesProperty, value);
    }

    private static void OnSeriesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        // 更新 LiveCharts 控件
    }
}
```

#### 步骤 3：创建服务注册

```csharp
public static class Extension
{
    public static IServiceCollection AddChart(this IServiceCollection services)
    {
        services.TryAddSingleton<IChartService, ChartService>();
        return services;
    }
}
```

---

## 11. 构建与发布

### 11.1 构建配置

项目统一配置在 `Directory.Build.Props` 和 `Directory.Build.targets` 中：

```xml
<!-- Directory.Build.Props -->
<Project>
    <PropertyGroup>
        <UseWPF>true</UseWPF>
        <TargetFrameworks>net8.0-windows</TargetFrameworks>
        <ImplicitUsings>enable</ImplicitUsings>
        <LangVersion>11.0</LangVersion>
        <Version>1.2.0</Version>
        <FileVersion>1.2.0</FileVersion>
        <AssemblyVersion>1.2.0</AssemblyVersion>
        <Copyright>Copyright © HeBianGu 2023-2025</Copyright>
        <Authors>HeBianGu</Authors>
        <GeneratePackageOnBuild>False</GeneratePackageOnBuild>
        <RepositoryUrl>https://github.com/HeBianGu/WPF-Control</RepositoryUrl>
        <PackageProjectUrl>https://github.com/HeBianGu/WPF-Control</PackageProjectUrl>
        <PackageLicenseExpression>MIT</PackageLicenseExpression>
        <PackageTags>WPF;Control;Theme;DataBase;Project;Style;Module;IOC;</PackageTags>
    </PropertyGroup>
</Project>
```

### 11.2 构建命令

```bash
# 构建所有项目
dotnet build --configuration Release

# 构建指定项目
dotnet build Source/Controls/H.Controls.Form/H.Controls.Form.csproj -c Release

# 打包 NuGet
dotnet pack Source/Controls/H.Controls.Form/H.Controls.Form.csproj -c Release
```

### 11.3 NuGet 发布

项目使用 GitHub Actions 自动发布 NuGet 包（参考 `.github/workflows/main.yml`）：

```yaml
name: NuGet Push
on:
  push:
    branches: [main]
jobs:
  build:
    runs-on: windows-latest
    steps:
      - uses: actions/checkout@v2
      - uses: actions/setup-dotnet@v1
      - run: dotnet build --configuration Release
      - run: dotnet nuget push .\Source\*\*\bin\Release\*.nupkg
             --source https://api.nuget.org/v3/index.json
             --api-key ${{ secrets.NuGetAPIKey }}
             --skip-duplicate --no-symbols 1
```

### 11.4 打包注意事项

- 确保每个程序集的 `.csproj` 包含正确的 `PackageId`、`Description`、`Tags`
- 在 `Directory.Build.Props` 中配置了 `PackageIcon` 和 `PackageReadmeFile`
- 资源文件（`.png`、`.jpg`、`.cur`、`.ico`）自动包含（通过通配符）

---

## 12. 常见模式与最佳实践

### 12.1 代码规范

#### 命名约定

- **命名空间**：`H.Xxx.Yyy`（H 为项目前缀）
- **类名**：PascalCase
- **方法名**：PascalCase
- **属性名**：PascalCase
- **私有字段**：`_camelCase`
- **依赖属性**：`XxxProperty`（静态字段）
- **附加属性**：`XxxProperty`（静态字段，RegisterAttached）

#### 文件头注释

每个源文件头部必须包含 MIT 许可声明：

```csharp
// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")
```

#### Global Usings

`Directory.Build.targets` 中配置了全局 using：

```xml
<Using Include="System.Diagnostics" />
<Using Include="System.ComponentModel.DataAnnotations" />
<Using Include="System.Threading.Tasks" />
<Using Include="System.ComponentModel" />
<Using Include="System.Reflection" />
<Using Include="System.Collections.ObjectModel" />
<Using Include="System.Windows" />
<Using Include="System.Windows.Controls" />
<Using Include="System.Windows.Media" />
```

### 12.2 模式总结

| 模式 | 说明 | 适用场景 |
|------|------|----------|
| **模块注册模式** | Extension.cs + Add/Use 方法 | 所有模块和服务 |
| **Options 模式** | 接口 + 单例实现类 + IOptions<T> | 模块配置 |
| **Presenter 模式** | 数据 + Presenter + DataTemplate | 数据驱动 UI |
| **Ioc 服务定位** | `Ioc.GetService<T>()` | 在非 DI 构造函数中获取服务 |
| **Cattach 附加属性** | `Cattach.XxxProperty` | 控件行为扩展 |
| **Bindable 继承** | ViewModel 继承 Bindable | 所有 ViewModel |
| **TryAdd 注册** | `services.TryAdd` | 允许替换默认实现 |

### 12.3 最佳实践

#### 依赖注入

- ✅ **始终通过构造函数注入服务**
- ✅ **模块间通过接口解耦**
- ✅ **使用 TryAdd 注册服务以便覆盖**
- ❌ 避免在 ViewModel 中 new 服务实例
- ❌ 避免在业务代码中直接 `new Window()`

#### XAML 开发

- ✅ **主题化资源使用 DynamicResource**
- ✅ **静态资源使用 StaticResource**
- ✅ **复杂模板按控件拆分文件**
- ❌ 避免在模板中硬编码颜色、字体、尺寸
- ❌ 避免大型 ResourceDictionary 不拆分

#### 控件开发

- ✅ **使用 TemplatePartAttribute 声明模板部件**
- ✅ **依赖属性使用 FrameworkPropertyMetadata**
- ✅ **支持主题化的资源使用 DynamicResource**
- ❌ 避免在控件代码中直接操作视觉树

#### 模块开发

- ✅ **遵循 Add/Use 注册模式**
- ✅ **提供接口抽象和默认实现**
- ✅ **使用 Options 模式管理配置**
- ❌ 避免模块间直接依赖具体类

### 12.4 常见陷阱

| 问题 | 原因 | 解决方案 |
|------|------|----------|
| 主题切换不生效 | 使用了 StaticResource | 改为 DynamicResource |
| IOC 服务解析失败 | 服务未注册 | 检查 ConfigureServices 中是否调用 AddXxx |
| XAML 设计器报错 | 设计时无法解析 IOC 服务 | 使用 `Ioc.GetService<T>(false)` 或添加设计时数据 |
| 资源键冲突 | 多个资源字典定义了相同 Key | 遵循命名约定 `S.Xxx.Yyy` |
| 程序集循环引用 | 基础层依赖控件层 | 检查依赖方向，接口提到公共层 |

---

## 13. 兼容性规范

### 13.1 兼容性检查清单

修改以下内容前必须评估兼容性：

- 公开类名、接口名、方法名
- XAML 命名空间（`xmlns:h="https://github.com/HeBianGu"`）
- Pack URI 路径（`pack://application:,,,/H.Theme;component/...`）
- 资源 Key（`S.Brush.Background` 等）
- 枚举成员名称
- 项目文件 JSON 字段
- DI 注册服务类型
- 默认样式 `TargetType`

### 13.2 兼容性策略

| 场景 | 推荐做法 |
|------|----------|
| 删除 API | 先标记 `[Obsolete]`，至少保留一个大版本后再移除 |
| 重命名 API | 保留旧 API 并内部转发到新 API |
| 修改资源 Key | 保留旧 Key 并链接到新资源 |
| 修改项目文件格式 | 增加版本字段和迁移逻辑 |
| 修改默认行为 | 提供 Options 配置项控制新旧行为 |

### 13.3 版本号规范

遵循 SemVer（语义化版本）：

- **主版本**：不兼容的 API 修改
- **次版本**：向下兼容的功能新增
- **修订号**：向下兼容的问题修复

### 13.4 程序集 README 模板

每个程序集建议添加 `README.md`：

```markdown
# H.Xxx

## 简介
说明程序集职责和适用场景。

## 依赖关系
| 依赖 | 用途 |
|------|------|
| H.Mvvm | MVVM 基础能力 |

## 快速开始
给出最小 XAML 或 C# 示例。

## 核心类型
| 类型 | 说明 |
|------|------|
| XxxService | Xxx 服务 |

## 服务注册
说明 `AddXxxServices()` 和 `UseXxxOptions()`。

## 主题资源
说明资源字典、Pack URI 和资源 Key。

## 扩展点
说明接口、基类、模板部件、DataTemplate 和 Presenter。

## 测试建议
说明应覆盖的测试和示例项目。

## 兼容性说明
说明公开 API、资源 Key、XAML 命名空间的兼容要求。
```

---

## 附录

### A. 常用链接

| 资源 | 链接 |
|------|------|
| GitHub 仓库 | https://github.com/HeBianGu/WPF-Control |
| 在线文档 | https://hebiangu.github.io/WPF-Control-Docs/ |
| NuGet | https://www.nuget.org/packages?q=HeBianGu |
| WPF 官方文档 | https://learn.microsoft.com/zh-cn/dotnet/api/system.windows.controls |
| 参考源码 | https://referencesource.microsoft.com/ |
| 作者 B 站 | https://space.bilibili.com/370266611 |

### B. 完整解决方案程序集

| 编号 | 程序集名称 | 解决方案文件夹 |
|------|-----------|--------------|
| 1 | H.Attach | 0 - Base |
| 2 | H.Mvvm | 0 - Base |
| 3 | H.ValueConverter | 0 - Base |
| 4 | H.MarkupExtension | 0 - Base |
| 5 | H.Globalization | 0 - Base |
| 6 | H.Common | 1 - Common |
| 7 | H.Data.Test | 2 - Datas |
| 8 | H.Controls.Form | 3 - Controls |
| 9 | H.Controls.PropertyGrid | 3 - Controls |
| 10 | H.Controls.Diagram | 3 - Controls |
| 11 | H.Controls.Dock | 3 - Controls |
| 12 | H.Controls.Chart2D | 3 - Controls |
| 13 | H.Controls.PDF | 3 - Controls |
| 14 | H.Controls.Vlc | 3 - Controls |
| 15 | H.Controls.Step | 3 - Controls |
| 16 | H.Controls.TagBox | 3 - Controls |
| 17 | H.Controls.ROIBox | 3 - Controls |
| 18 | H.Controls.Panel | 3 - Controls |
| 19 | H.Controls.ColorBox | 3 - Controls |
| 20 | H.Controls.ShapeBox | 3 - Controls |
| 21 | H.Extensions.ApplicationBase | 4 - Extensions |
| 22 | H.Extensions.Common | 4 - Extensions |
| 23 | H.Extensions.Tree | 4 - Extensions |
| 24 | H.Services.Common | 5 - Services |
| 25 | H.Services.AppPath | 5 - Services |
| 26 | H.Services.Logger | 5 - Services |
| 27 | H.Services.Identity | 5 - Services |
| 28 | H.Iocable | 6 - Providers |
| 29 | H.Theme | 7 - Themes |
| 30 | H.Style | 7 - Themes |
| 31 | H.Styles.Bootstrap | 7 - Themes |
| 32 | H.Themes.Colors.Blue | 7 - Themes |
| 33 | H.Themes.Colors.Gray | 7 - Themes |
| 34 | H.Themes.Colors.Purple | 7 - Themes |
| 35 | H.Themes.Colors.Copper | 7 - Themes |
| 36 | H.Themes.Colors.Accent | 7 - Themes |
| 37 | H.Themes.Colors.Office | 7 - Themes |
| 38 | H.Themes.Colors.Web | 7 - Themes |
| 39 | H.Modules.Login | 8 - Modules |
| 40 | H.Modules.Project | 8 - Modules |
| 41 | H.Modules.Theme | 8 - Modules |
| 42 | H.Modules.Setting | 8 - Modules |
| 43 | H.Modules.About | 8 - Modules |
| 44 | H.Modules.Home | 8 - Modules |
| 45 | H.Modules.Guide | 8 - Modules |
| 46 | H.Modules.Identity | 8 - Modules |
| 47 | H.Modules.License | 8 - Modules |
| 48 | H.Modules.Upgrade | 8 - Modules |
| 49 | H.Modules.Logger | 8 - Modules |
| 50 | H.Modules.Feedback | 8 - Modules |
| 51 | H.Modules.Help | 8 - Modules |
| 52 | H.Modules.Sponsor | 8 - Modules |
| 53 | H.Modules.Style | 8 - Modules |
| 54 | H.Modules.Operation | 8 - Modules |
| 55 | H.Windows.Main | 9 - Windows |
| 56 | H.Windows.Dialog | 9 - Windows |
| 57 | H.Windows.Dock | 9 - Windows |
| 58 | H.Windows.Ribbon | 9 - Windows |

---

*文档版本：1.2.0 | 最后更新：2025-07*  
*Copyright © HeBianGu 2023-2025. Licensed under MIT License.*
