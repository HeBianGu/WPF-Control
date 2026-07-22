# SplashScreen 启动页面二次开发文档

**适用项目：** `H.Modules.SplashScreen`、`H.Extensions.ApplicationBase`、`H.Services.Common`  
**核心类型：** `ISplashScreenViewPresenter`、`SplashScreenViewPresenter`、`BackgroundSplashScreenViewPresenter`、`SplashScreenOptions`、`ISplashLoadable`  
**相关能力：** 启动页、初始化进度、设置加载、默认模板加载、服务预加载、主题预加载、启动失败处理

本文介绍框架中 SplashScreen 启动页面的注册、配置、执行流程、加载任务扩展和自定义界面方式。

---

## 1. SplashScreen 模块定位

框架 SplashScreen 不只是静态启动图片，而是应用启动初始化过程的可视化宿主。

它负责：

- 在主窗口显示前呈现启动页面。
- 加载主题配置，避免主界面显示后发生主题闪烁。
- 加载 `IocSetting` 中的设置项。
- 加载默认模板。
- 顺序执行所有 `ISplashLoadable` 初始化服务。
- 将当前任务名称和进度写入 `Message`。
- 初始化失败时终止后续启动。
- 未注册启动页时仍在后台执行相同初始化流程。

核心流程：

```text
ApplicationBase.OnStartup
    ↓
Configure
    ↓
单实例检查
    ↓
创建主窗口，但暂不显示
    ↓
OnSplashScreen
    ├── 加载主题设置
    ├── 显示 Splash Presenter
    ├── 加载应用设置
    ├── 加载默认模板
    └── 执行 ISplashLoadable
    ↓
OnLogin
    ↓
恢复主窗口状态
    ↓
MainWindow.Show
```

---

## 2. 相关项目职责

| 项目 | 说明 |
|---|---|
| `H.Modules.SplashScreen` | 默认启动页、背景启动页、配置项和服务注册扩展。 |
| `H.Extensions.ApplicationBase` | 在 `OnStartup` 中调度启动页和初始化任务。 |
| `H.Services.Common` | 定义 `ISplashScreenViewPresenter`、`ISplashLoadable` 等公共契约。 |
| `H.Services.Setting` | 提供 `IocSetting`，在启动页阶段加载设置。 |
| `H.Services.Message` | 通过窗口消息服务显示启动 Presenter。 |

---

## 3. 快速开始

### 3.1 引用模块

应用项目需要引用：

```text
H.Modules.SplashScreen
H.Extensions.ApplicationBase
```

通常还需要窗口消息服务及框架默认应用服务。

### 3.2 注册普通启动页

```csharp
protected override void ConfigureServices(IServiceCollection services)
{
    services.AddApplicationServices();
    services.AddSplashScreen();
}
```

带配置：

```csharp
services.AddSplashScreen(options =>
{
    options.Product = "生产管理平台";
    options.Sub = "Manufacturing Execution System";
    options.SleepMicroseconds = 100;
});
```

### 3.3 注册背景启动页

```csharp
protected override void ConfigureServices(IServiceCollection services)
{
    services.AddApplicationServices();
    services.AddBackgroundSplashScreen(options =>
    {
        options.Product = "生产管理平台";
        options.Sub = "正在初始化系统";
        options.SleepMicroseconds = 100;
    });
}
```

两种注册方式只应选择一种，因为它们都注册同一个接口：

```text
ISplashScreenViewPresenter
```

且注册内部使用 `TryAdd`，先注册的实现会生效。

### 3.4 应用配置阶段

```csharp
protected override void Configure(IApplicationBuilder app)
{
    base.Configure(app);
    app.UseSplashScreenOptions(options =>
    {
        options.Product = "生产管理平台";
        options.Sub = "让生产更简单";
    });
}
```

`UseSplashScreenOptions()` 当前只修改 `SplashScreenOptions.Instance`，没有把它加入设置页：

```csharp
public static IApplicationBuilder UseSplashScreenOptions(
    this IApplicationBuilder builder,
    Action<ISplashScreenOptions> option = null)
{
    option?.Invoke(SplashScreenOptions.Instance);
    return builder;
}
```

因此它更适合应用启动时固定配置品牌信息。

---

## 4. 完整应用示例

```csharp
public partial class App : ApplicationBase
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddApplicationServices();
        services.AddWindowMessage();
        services.AddWindowDialogMessage();

        services.AddBackgroundSplashScreen(options =>
        {
            options.Product = "设备监控平台";
            options.Sub = "Equipment Monitoring Platform";
            options.SleepMicroseconds = 100;
        });

        services.AddSingleton<ISplashLoadable, DatabaseSplashLoadService>();
        services.AddSingleton<ISplashLoadable, CacheSplashLoadService>();
    }

    protected override void Configure(IApplicationBuilder app)
    {
        base.Configure(app);
        app.UseSplashScreenOptions(options =>
        {
            options.Product = "设备监控平台";
            options.Sub = "正在启动";
        });
    }

    protected override Window CreateMainWindow(StartupEventArgs e)
    {
        return new MainWindow();
    }
}
```

---

## 5. 启动时机

`ApplicationBase.OnStartup()` 的关键顺序：

```csharp
protected override void OnStartup(StartupEventArgs e)
{
    this.Configure();
    this.OnSingleton(e);
    base.OnStartup(e);

    Window window = this.CreateMainWindow(e);

    this.OnSplashScreen(e);
    this.OnLogin();

    Ioc<IMainWindowSavableService>.Instance?.Load(window);
    this.MainWindow.Show();
}
```

重要特征：

- 主窗口已经创建，但尚未调用 `Show()`。
- SplashScreen 在登录页面之前执行。
- 初始化失败时主窗口不会显示。
- 主窗口 `Loaded` 阶段的服务不属于 SplashScreen 加载阶段。

如果某项工作必须在主窗口显示前完成，应实现 `ISplashLoadable`；如果必须等待主窗口视觉树加载后执行，应使用 `IMainWindowLoadedLoadable`。

---

## 6. `ISplashScreenViewPresenter`

启动页 Presenter 契约：

```csharp
public interface ISplashScreenViewPresenter
{
    string Message { get; set; }
    int SleepMicroseconds { get; }
}
```

| 成员 | 说明 |
|---|---|
| `Message` | 当前初始化任务和错误信息，由启动流程持续更新。 |
| `SleepMicroseconds` | 每一步完成后的等待值。实际实现传给 `Thread.Sleep(int)`。 |

默认 Presenter：

```csharp
public class SplashScreenViewPresenter : BindableBase,
    ISplashScreenViewPresenter,
    IWindowInitable
{
    private string _message;

    public string Message
    {
        get => _message;
        set
        {
            _message = value;
            RaisePropertyChanged();
        }
    }

    public int SleepMicroseconds =>
        SplashScreenOptions.Instance.SleepMicroseconds;
}
```

`Message` 实现属性通知，因此 XAML 中的加载提示可实时更新。

### 6.1 关于 `SleepMicroseconds` 名称

当前代码执行：

```csharp
Thread.Sleep(sleep);
```

`Thread.Sleep(int)` 的单位实际是**毫秒**，而不是微秒。因此：

```csharp
options.SleepMicroseconds = 100;
```

当前实际表示等待约 `100 ms`。

二次开发时应根据现有行为配置，不要按微秒换算。若未来修正属性名或单位，需要评估现有应用配置兼容性。

---

## 7. `SplashScreenOptions`

接口：

```csharp
public interface ISplashScreenOptions : ISettable
{
    string Product { get; set; }
    int SleepMicroseconds { get; set; }
    string Sub { get; set; }
}
```

实际实现还提供：

| 属性 | 默认值 / 来源 | 说明 |
|---|---|---|
| `Product` | `ApplicationProvider.Product` | 产品名称。 |
| `Sub` | `null` | 副标题。 |
| `SubFontSize` | `20` | 副标题字号。 |
| `Background` | 模块内嵌 `background.jpg` | 背景启动页图片地址。 |
| `SleepMicroseconds` | `100` | 每个初始化步骤之后的等待时间，当前实际单位为毫秒。 |

默认背景：

```text
pack://application:,,,/H.Modules.SplashScreen;component/Assets/background.jpg
```

### 7.1 默认值加载

```csharp
public override void LoadDefault()
{
    base.LoadDefault();
    this.Product = ApplicationProvider.Product;
}
```

`Background` 使用 `[DefaultValue]`，由设置基类的默认值机制初始化。

### 7.2 序列化行为

`Product`、`Sub`、`SubFontSize`、`Background` 标记了 `System.Text.Json.Serialization.JsonIgnore`。它们主要作为应用品牌和运行时显示配置，而非用户持久化设置。

`SleepMicroseconds` 未标记忽略，可随具体设置序列化机制保存。

---

## 8. 普通启动页

`SplashScreenViewPresenter.xaml` 使用隐式 `DataTemplate`：

```xaml
<DataTemplate DataType="{x:Type local:SplashScreenViewPresenter}">
    <Grid Width="550" Height="280">
        <Grid Margin="20">
            <TextBlock
                Text="{Binding Source={x:Static local:SplashScreenOptions.Instance}, Path=Sub}" />

            <Viewbox>
                <TextBlock
                    Text="{Binding Source={x:Static local:SplashScreenOptions.Instance}, Path=Product}" />
            </Viewbox>

            <TextBlock Text="{Binding Message}" />
            <TextBlock Text="{x:Static ApplicationProvider.Copyright}" />
        </Grid>
    </Grid>
</DataTemplate>
```

显示内容包括：

- 产品名称。
- 副标题。
- 当前加载消息。
- 应用版权信息。

窗口由 `IocMessage.Window.ShowAction()` 提供，Presenter 只负责内容和窗口初始化。

---

## 9. 背景启动页

注册：

```csharp
services.AddBackgroundSplashScreen();
```

`BackgroundSplashScreenViewPresenter` 继承普通 Presenter，并在 `InitWindow()` 中设置背景：

```csharp
public override void InitWindow(Window window)
{
    base.InitWindow(window);

    var background = SplashScreenOptions.Instance?.Background;
    if (background != null)
    {
        var converter = TypeDescriptor.GetConverter(typeof(ImageSource));
        ImageBrush imageBrush = new ImageBrush();
        imageBrush.ImageSource =
            converter.ConvertFromInvariantString(background) as ImageSource;
        window.Background = imageBrush;

        Cattach.SetCornerRadius(window, new CornerRadius(10));
        Cattach.SetCaptionForeground(window, Brushes.White);
    }
}
```

背景版默认尺寸为：

```xaml
<Grid Width="900" Height="450" TextBlock.Foreground="White">
```

适用于：

- 品牌化产品启动页。
- 需要展示产品海报或宣传图。
- 深色背景和白色文本场景。

### 9.1 替换背景图

使用 Pack URI：

```csharp
app.UseSplashScreenOptions(options =>
{
    ((SplashScreenOptions)options).Background =
        "pack://application:,,,/MyApp;component/Assets/Splash/background.jpg";
});
```

由于 `ISplashScreenOptions` 当前没有暴露 `Background`，通过接口配置回调直接设置背景时需要转换为 `SplashScreenOptions`，或在注册后直接设置：

```csharp
SplashScreenOptions.Instance.Background =
    "pack://application:,,,/MyApp;component/Assets/Splash/background.jpg";
```

业务项目也可以注册自定义 Presenter，并在 `InitWindow` 中使用自己的背景配置契约。

---

## 10. 启动窗口初始化

默认 Presenter 实现 `IWindowInitable`：

```csharp
public virtual void InitWindow(Window window)
{
    window.SizeToContent = SizeToContent.WidthAndHeight;
    Cattach.SetCaptionBackground(window, null);
}
```

作用：

- 窗口尺寸由 DataTemplate 内容决定。
- 清除默认标题栏背景，使启动窗口更简洁。

自定义 Presenter 可进一步设置：

```csharp
public void InitWindow(Window window)
{
    window.SizeToContent = SizeToContent.WidthAndHeight;
    window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
    window.ResizeMode = ResizeMode.NoResize;
    window.ShowInTaskbar = false;
    window.Topmost = true;
}
```

实际窗口仍由消息服务创建，不需要 Presenter 自己 `new Window()`。

---

## 11. 启动初始化流程详解

`ApplicationBase.OnSplashScreen()` 首先获取 Presenter：

```csharp
ISplashScreenViewPresenter presenter =
    Ioc.Services.GetService<ISplashScreenViewPresenter>();
```

如果没有注册 Presenter，初始化任务仍会执行，只是不显示页面。

### 11.1 主题预加载

```csharp
var tls = Ioc.GetService<ILoadThemeOptionsService>(false);
tls?.Load(out string message);
```

主题在启动窗口显示前加载，用于避免：

- SplashScreen 使用默认主题后突然切换。
- 主窗口首次显示时颜色闪烁。
- 字体、布局和颜色资源在窗口创建后才更新。

### 11.2 收集加载任务

```csharp
IEnumerable<ISplashLoadable> loads =
    Ioc.GetAssignableFromServices<ISplashLoadable>().Distinct();

IEnumerable<IDefaultTemplateable> templates =
    Ioc.GetAssignableFromServices<IDefaultTemplateable>().Distinct();
```

所有注册到 IOC、实现对应接口的服务都会参与启动加载。

### 11.3 加载设置

```csharp
IocSetting.Instance?.Load(item =>
{
    presenter.Message = $"加载设置<{item.Name}>...";

    if (item is IDefaultTemplateable templateable)
        templateable.LoadDefaultTemplate();
}, out message);
```

设置项按 `IocSetting` 中的顺序加载，消息显示当前设置名称。

### 11.4 加载默认模板

```csharp
foreach (IDefaultTemplateable item in templates)
{
    presenter.Message = $"加载模板<{item.Name}>...";
    item.LoadDefaultTemplate();
}
```

适合项目模板、布局模板、Diagram 模板等启动前准备。

### 11.5 加载 `ISplashLoadable`

```csharp
foreach (ISplashLoadable load in loads)
{
    presenter.Message = $"加载{load.Name}...";

    bool result = load.Load(out string message);
    if (!result)
        return false;
}
```

任一加载服务返回 `false`，启动流程停止。

### 11.6 完成与失败

成功：

```csharp
presenter.Message = Resources.Message_LoadSuccess;
return true;
```

失败：

```csharp
if (result == false)
{
    IocLog.Info("启动失败，程序退出");
    this.Shutdown();
}
```

未注册启动 Presenter 时，失败会抛出加载异常：

```csharp
throw new ArgumentException(Resources.Message_LoadException);
```

---

## 12. 开发 `ISplashLoadable`

接口：

```csharp
public interface ISplashLoadable : ILoadable
{
    string Name { get; }
}

public interface ILoadable
{
    bool Load(out string message);
}
```

### 12.1 数据库初始化示例

```csharp
public class DatabaseSplashLoadService : ISplashLoadable
{
    private readonly AppDbContext _dbContext;

    public DatabaseSplashLoadService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public string Name => "数据库";

    public bool Load(out string message)
    {
        try
        {
            _dbContext.Database.EnsureCreated();
            message = "数据库初始化完成";
            return true;
        }
        catch (Exception ex)
        {
            message = $"数据库初始化失败：{ex.Message}";
            return false;
        }
    }
}
```

注册：

```csharp
services.AddSingleton<ISplashLoadable, DatabaseSplashLoadService>();
```

### 12.2 缓存初始化示例

```csharp
public class CacheSplashLoadService : ISplashLoadable
{
    private readonly ICacheService _cache;

    public CacheSplashLoadService(ICacheService cache)
    {
        _cache = cache;
    }

    public string Name => "缓存";

    public bool Load(out string message)
    {
        try
        {
            _cache.Initialize();
            message = null;
            return true;
        }
        catch (Exception ex)
        {
            message = ex.Message;
            return false;
        }
    }
}
```

### 12.3 适合放入 Splash 阶段的任务

- 数据库结构检查和迁移。
- 本地缓存和索引加载。
- License 校验。
- 项目模板和 Diagram 模板加载。
- 必需配置验证。
- 插件清单扫描。
- 主界面显示前必须准备的基础数据。

不建议放入：

- 与首次页面无关的大文件预加载。
- 可延迟执行的统计、同步或后台任务。
- 永不结束的监听服务。
- 需要主窗口 Handle 或完整视觉树的操作。

这些任务会延长启动时间，应放到主窗口加载后或后台服务中。

---

## 13. 加载顺序与依赖

`ISplashLoadable` 来自 IOC 服务枚举。不要把关键业务依赖建立在未明确保证的注册枚举顺序上。

推荐将强依赖组合到一个编排服务：

```csharp
public class ApplicationSplashLoadService : ISplashLoadable
{
    private readonly IDatabaseInitializer _database;
    private readonly ICacheInitializer _cache;
    private readonly IPluginInitializer _plugins;

    public string Name => "应用核心服务";

    public bool Load(out string message)
    {
        if (!_database.Load(out message))
            return false;

        if (!_cache.Load(out message))
            return false;

        if (!_plugins.Load(out message))
            return false;

        message = null;
        return true;
    }
}
```

这样顺序由业务代码明确控制，失败信息也更容易定位。

---

## 14. 自定义启动页 Presenter

### 14.1 定义 Presenter

```csharp
public class CompanySplashScreenPresenter : BindableBase,
    ISplashScreenViewPresenter,
    IWindowInitable
{
    private string _message;

    public string Message
    {
        get => _message;
        set
        {
            _message = value;
            RaisePropertyChanged();
        }
    }

    public string Product => "Contoso Studio";
    public string Version => ApplicationProvider.Version?.ToString();
    public int SleepMicroseconds => 80;

    public void InitWindow(Window window)
    {
        window.SizeToContent = SizeToContent.WidthAndHeight;
        window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        window.ResizeMode = ResizeMode.NoResize;
        window.ShowInTaskbar = false;
    }
}
```

### 14.2 定义 DataTemplate

```xaml
<ResourceDictionary
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:local="clr-namespace:MyApp.SplashScreen">

    <DataTemplate DataType="{x:Type local:CompanySplashScreenPresenter}">
        <Border
            Width="720"
            Height="360"
            Padding="32"
            Background="#FF152238"
            CornerRadius="10">
            <Grid>
                <Grid.RowDefinitions>
                    <RowDefinition />
                    <RowDefinition Height="Auto" />
                    <RowDefinition Height="Auto" />
                </Grid.RowDefinitions>

                <StackPanel VerticalAlignment="Center">
                    <TextBlock
                        FontSize="42"
                        FontWeight="Bold"
                        Foreground="White"
                        Text="{Binding Product}" />
                    <TextBlock
                        Margin="0,8,0,0"
                        Foreground="#FFB8C7DD"
                        Text="{Binding Version}" />
                </StackPanel>

                <ProgressBar
                    Grid.Row="1"
                    Height="4"
                    Margin="0,0,0,12"
                    IsIndeterminate="True" />

                <TextBlock
                    Grid.Row="2"
                    Foreground="White"
                    Text="{Binding Message}" />
            </Grid>
        </Border>
    </DataTemplate>
</ResourceDictionary>
```

把资源字典合并到应用资源：

```xaml
<ResourceDictionary.MergedDictionaries>
    <ResourceDictionary Source="/MyApp;component/SplashScreen/CompanySplashScreenPresenter.xaml" />
</ResourceDictionary.MergedDictionaries>
```

### 14.3 注册自定义 Presenter

```csharp
services.AddSplashScreen<CompanySplashScreenPresenter>(options =>
{
    options.SleepMicroseconds = 80;
});
```

泛型约束：

```csharp
where T : class, ISplashScreenViewPresenter
```

如果需要自定义窗口外观，再实现 `IWindowInitable`。

---

## 15. 覆盖默认 DataTemplate

框架使用 Presenter + 隐式 `DataTemplate` 呈现启动页。因此应用可以不重写启动流程，只覆盖视觉模板。

例如，为默认 Presenter 定义应用级模板：

```xaml
<DataTemplate DataType="{x:Type splash:SplashScreenViewPresenter}">
    <Border Width="600" Height="300" Background="DarkSlateBlue">
        <StackPanel VerticalAlignment="Center">
            <TextBlock
                HorizontalAlignment="Center"
                FontSize="36"
                Foreground="White"
                Text="{Binding Source={x:Static splash:SplashScreenOptions.Instance}, Path=Product}" />
            <TextBlock
                Margin="0,20,0,0"
                HorizontalAlignment="Center"
                Foreground="White"
                Text="{Binding Message}" />
        </StackPanel>
    </Border>
</DataTemplate>
```

应用资源应位于可覆盖框架默认模板的资源范围。局部或应用级资源通常优先于程序集 `Generic.xaml` 中的默认资源。

优点：

- 初始化流程保持不变。
- 只替换品牌、布局和动画。
- 不需要复制 `ApplicationBase.OnSplashScreen()`。
- 可以按不同应用或主题提供不同启动页。

---

## 16. 启动消息和进度

框架当前公开的是文本消息：

```csharp
string Message { get; set; }
```

消息格式包含：

```text
[当前步骤/总步骤]加载设置...
[当前步骤/总步骤]加载设置<主题配置>...
[当前步骤/总步骤]加载模板<流程模板>...
[当前步骤/总步骤]加载数据库...
加载成功
```

默认 Presenter 没有公开数值进度属性。如果需要确定进度条，可扩展 Presenter 契约或根据消息解析，但更推荐新建应用自己的进度服务，例如：

```csharp
public interface IStartupProgress
{
    int Current { get; set; }
    int Total { get; set; }
    string Message { get; set; }
}
```

若不修改框架启动调度器，只能使用 `IsIndeterminate="True"` 的进度条并绑定 `Message`。

---

## 17. 取消和失败处理

启动执行回调会检查对话框状态：

```csharp
if (dialog?.IsCancel == true)
    return null;
```

默认显示配置：

```csharp
x.DialogButton = DialogButton.None;
```

因此默认启动页没有取消按钮。自定义窗口消息策略可提供取消入口，但任务本身仍是同步 `Load(out message)`，只能在任务之间检查取消，不能中断正在执行的单个长任务。

加载失败约定：

```csharp
return false;
```

并通过：

```csharp
message = "具体错误原因";
```

向启动页和日志提供错误信息。

开发建议：

- `Load` 内捕获可预期异常。
- 返回清晰、可操作的错误消息。
- 同时记录完整异常日志。
- 不要用空的 `catch` 隐藏启动失败原因。
- 对可恢复问题可加载默认配置并返回 `true`。
- 对主程序无法运行的问题返回 `false`。

---

## 18. 同步执行与线程注意事项

`ISplashLoadable.Load(out message)` 是同步接口。开发时需要注意：

- 不要在 `Load` 中使用 `.Result` / `.Wait()` 阻塞依赖 UI 上下文的异步方法。
- 不要从后台线程直接访问 WPF `DispatcherObject`。
- 耗时 I/O 如果必须异步，应在服务内部谨慎编排，确保不会产生 UI 线程死锁。
- 启动任务完成前主窗口不会显示，任务越多启动越慢。

如果任务适合后台运行且不影响首屏，应在主窗口显示后启动，而不是注册为 `ISplashLoadable`。

---

## 19. 未注册 SplashScreen 时的行为

如果没有调用：

```csharp
services.AddSplashScreen();
```

也没有注册自定义 `ISplashScreenViewPresenter`，则：

```csharp
presenter == null
```

框架仍执行：

- 设置加载。
- 默认模板加载。
- `ISplashLoadable` 加载。

只是没有可见启动窗口和 `Message` 更新。

这允许：

- 测试环境禁用启动页。
- 命令行或后台模式复用初始化流程。
- 简单应用不显示 SplashScreen，但保持框架启动加载机制。

---

## 20. 与登录页面的顺序

启动顺序为：

```text
SplashScreen
    ↓
Login
    ↓
MainWindow
```

因此：

- License、主题、数据库、用户存储等登录前依赖应在 Splash 阶段准备。
- 需要当前登录用户的信息不能在 Splash 阶段完成。
- 用户级数据应在登录成功后加载。
- SplashScreen 不应承担登录输入功能；登录模块有独立 Presenter 和流程。

---

## 21. 单元测试建议

`ISplashLoadable` 是普通同步接口，可脱离 UI 测试：

```csharp
[Fact]
public void Load_ReturnsFalse_WhenDatabaseInitializationFails()
{
    var database = new FakeDatabaseInitializer
    {
        ShouldFail = true
    };
    var service = new DatabaseSplashLoadService(database);

    bool result = service.Load(out string message);

    Assert.False(result);
    Assert.False(string.IsNullOrWhiteSpace(message));
}
```

成功测试：

```csharp
[Fact]
public void Load_ReturnsTrue_WhenCacheIsInitialized()
{
    var cache = new FakeCacheService();
    var service = new CacheSplashLoadService(cache);

    bool result = service.Load(out string message);

    Assert.True(result);
    Assert.True(cache.IsInitialized);
}
```

Presenter 测试：

```csharp
[Fact]
public void Message_RaisesPropertyChanged()
{
    var presenter = new SplashScreenViewPresenter();
    string changedProperty = null;
    presenter.PropertyChanged += (_, e) => changedProperty = e.PropertyName;

    presenter.Message = "正在加载";

    Assert.Equal(nameof(SplashScreenViewPresenter.Message), changedProperty);
}
```

UI 模板只需少量集成测试；加载成功、失败和依赖顺序应主要测试服务层。

---

## 22. 常见问题

### 启动页没有显示

检查：

1. 是否注册了 `AddSplashScreen()` 或 `AddBackgroundSplashScreen()`。
2. 是否注册了窗口消息服务。
3. 是否使用继承自 `ApplicationBase` 的应用类。
4. 是否存在 Presenter 对应的 `DataTemplate`。
5. 是否有其他服务先注册了 `ISplashScreenViewPresenter`，导致 `TryAdd` 不再替换。

### 启动页只显示类型名称

说明 WPF 没找到隐式 `DataTemplate`。检查：

- 模块资源是否正确合并或可通过 `Generic.xaml` 发现。
- 自定义 Presenter 是否定义了 `DataTemplate DataType`。
- XAML 中类型的命名空间和程序集是否正确。

### 背景图片不显示

检查：

1. Pack URI 是否正确。
2. 图片生成操作是否为 `Resource`。
3. 程序集名和路径大小写是否匹配。
4. `ImageSource` TypeConverter 是否能解析该字符串。
5. 是否注册了 `BackgroundSplashScreenViewPresenter`。

### 加载消息不更新

检查：

- Presenter 的 `Message` Setter 是否调用 `RaisePropertyChanged()`。
- DataTemplate 是否绑定 `{Binding Message}`。
- 长任务是否持续阻塞，使 UI 无机会重绘。
- 是否将一个耗时大任务拆分为多个可报告步骤。

### 启动页停留时间过长

检查：

- `SleepMicroseconds` 当前实际按毫秒执行。
- 是否注册了过多 `ISplashLoadable`。
- 是否把可延迟任务放入启动阶段。
- 是否重复加载了默认模板。
- 数据库或网络初始化是否存在超时。

### 初始化失败后程序退出

这是默认设计。检查 `ISplashLoadable.Load()` 返回的 `false` 和 `message`，并查看日志。对于非关键服务，可以记录警告后返回 `true`，随后在主界面提供降级功能。

---

## 23. 二次开发建议

- 普通产品使用 `AddSplashScreen()`，品牌化产品使用 `AddBackgroundSplashScreen()`。
- 初始化任务实现 `ISplashLoadable`，不要把业务初始化堆在 `App.OnStartup()`。
- 必需任务失败返回 `false`，非必需任务采用降级策略。
- 明确区分 Splash 阶段、登录后阶段和主窗口加载后阶段。
- `SleepMicroseconds` 当前实际单位是毫秒，配置时按现有实现理解。
- 自定义外观优先覆盖 `DataTemplate`，不要复制整个启动调度流程。
- 自定义窗口行为通过 `IWindowInitable` 实现。
- 长期任务和可延迟任务移到主窗口显示后执行，优化首屏时间。
- 加载服务应可独立单元测试，不依赖具体启动窗口。
- 背景图使用 Pack URI，并确认资源生成操作正确。
- 不要在启动页加载不可信插件或不可信序列化文件而缺少校验。
