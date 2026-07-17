# Theme 主题系统二次开发文档

**适用项目：** `H.Theme`、`H.Modules.Theme`、`H.ApplicationBases.Themes`、`H.Themes.Colors.*`  
**核心类型：** `ThemeOptions`、`ColorResourceBase`、`IColorResource`、`ColorKeys`、`BrushKeys`、`SystemKeys`、`ThemeTypeExtension`  
**相关能力：** 颜色主题、背景主题、字号主题、布局主题、字体切换、图标字体切换、动态资源替换、`ComponentResourceKey` 资源键

本文介绍 WPF-Control 中 Theme 主题系统的详细使用方式，重点说明主题注册、主题切换、自定义主题资源，以及如何使用 `ComponentResourceKey` 定义和引用资源。

---

## 1. Theme 系统定位

Theme 系统用于统一管理应用视觉资源，包括：

- 颜色资源 `Color`。
- 画刷资源 `Brush`。
- 背景资源 `BackgroundResource`。
- 字号资源 `FontSizeThemeType`。
- 布局尺寸资源 `LayoutThemeType`。
- 全局字体 `FontFamily`。
- 图标字体 `IconFontFamily`。
- 主题配置保存和启动加载。
- 运行时切换主题资源字典。

主题系统的核心思想是：

```text
稳定资源键 ComponentResourceKey
    ↓
控件样式通过 DynamicResource 引用资源键
    ↓
主题切换时替换 ResourceDictionary
    ↓
界面自动刷新颜色、画刷、字号、布局和字体
```

---

## 2. 相关项目职责

| 项目 | 说明 |
|---|---|
| `H.Theme` | 基础主题资源，包含颜色键、画刷键、系统键、字号、布局、背景和主题扩展方法。 |
| `H.Modules.Theme` | 主题设置项 `ThemeOptions`、主题加载服务和主题切换界面 Presenter。 |
| `H.ApplicationBases.Themes` | 默认主题注册组合，批量注册内置配色资源。 |
| `H.Themes.Colors.*` | 多套颜色主题资源，如 Blue、Gray、Office、Technology、Web、Industrial 等。 |

---

## 3. 最小主题资源引用

在 `App.xaml` 中合并基础主题资源：

```xaml
<Application.Resources>
    <ResourceDictionary>
        <ResourceDictionary.MergedDictionaries>
            <FontSizeTheme Type="Default" />
            <LayoutTheme Type="Default" />
            <ColorTheme Type="Default" />
            <BackgroundTheme Type="SolidColorBrush" />
        </ResourceDictionary.MergedDictionaries>
    </ResourceDictionary>
</Application.Resources>
```

常见扩展：

| 扩展 | 说明 |
|---|---|
| `ColorTheme` | 加载颜色资源字典。 |
| `BackgroundTheme` | 加载背景画刷资源字典。 |
| `FontSizeTheme` | 加载字号资源字典。 |
| `LayoutTheme` | 加载布局尺寸资源字典。 |

示例源码：

```csharp
public class ColorThemeExtension : MarkupExtension
{
    public ColorThemeType Type { get; set; }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        if (this.Type == ColorThemeType.Default)
            return new DefaultColorResource().Resource;
        if (this.Type == ColorThemeType.Dark)
            return new DarkColorResource().Resource;
        if (this.Type == ColorThemeType.Light)
            return new LightColorResource().Resource;
        return null;
    }
}
```

---

## 4. 注册主题服务

### 4.1 注册基础主题服务

```csharp
protected override void ConfigureServices(IServiceCollection services)
{
    services.AddSetting();
    services.AddTheme();
}
```

`AddTheme()` 会注册：

```csharp
services.TryAdd(ServiceDescriptor.Singleton<ILoadThemeOptionsService, LoadThemeOptionsService>());
services.TryAdd(ServiceDescriptor.Singleton<ISwitchThemeViewPresenter, SwitchThemeViewPresenter>());
services.TryAdd(ServiceDescriptor.Singleton<IColorThemeViewPresenter, ColorThemeViewPresenter>());
```

### 4.2 使用主题配置

```csharp
protected override void Configure(IApplicationBuilder app)
{
    base.Configure(app);
    app.UseThemeOptions();
}
```

`UseThemeOptions()` 会把 `ThemeOptions.Instance` 加入设置系统：

```csharp
IocSetting.Instance.Add(ThemeOptions.Instance);
```

### 4.3 使用默认主题组合

如果希望一次性启用默认主题服务和大量内置配色，可使用：

```csharp
protected override void ConfigureServices(IServiceCollection services)
{
    services.AddDefaultThemeServices();
}

protected override void Configure(IApplicationBuilder app)
{
    base.Configure(app);
    app.UseDefaultThemeOptions();
}
```

`UseDefaultThemeOptions()` 内部会：

- 添加默认颜色资源集合。
- 添加默认图标字体集合。
- 添加 `ThemeOptions.Instance` 到设置系统。

---

## 5. 启动加载主题

`ApplicationBase.OnSplashScreen` 和 `ApplicationBase.OnLogin` 中会优先加载主题：

```csharp
var tls = Ioc.GetService<ILoadThemeOptionsService>(false);
tls?.Load(out string message);
```

`LoadThemeOptionsService` 内部调用：

```csharp
return ThemeOptions.Instance.Load(out message);
```

这样可以保证主窗口显示前主题已经加载，避免启动时出现颜色闪烁或样式切换。

---

## 6. `ThemeOptions` 配置项

`ThemeOptions` 是主题设置项：

```csharp
[Display(Name = "主题设置", GroupName = SettingGroupNames.GroupStyle, Description = "主题设置设置的信息")]
public class ThemeOptions : IocOptionInstance<ThemeOptions>, ILoginedSplashLoadable, IThemeOptions, IIconFontFamilysOptions, IColorThemeOptions
{
}
```

主要属性：

| 属性 | 说明 |
|---|---|
| `FontSize` | 字号主题。 |
| `Layout` | 布局主题。 |
| `ColorResource` | 当前颜色资源。 |
| `ColorResources` | 可选颜色资源集合。 |
| `BackgroundResource` | 当前背景资源。 |
| `BackgroundResources` | 可选背景资源集合。 |
| `FontFamily` | 全局字体。 |
| `IconFontFamily` | 图标字体。 |
| `IconFontFamilys` | 可选图标字体集合。 |
| `IsDark` | 当前是否为深色主题。 |
| `RefreshThemeCommand` | 刷新主题命令。 |

设置页中主题选择使用 `ComboBoxPropertyItem`：

```csharp
[GetPropertyNameSource(nameof(ColorResources))]
[PropertyItem(typeof(ComboBoxPropertyItem))]
[Display(Name = "配色")]
public IColorResource ColorResource { get; set; }
```

---

## 7. 主题切换流程

`ThemeOptions.RefreshTheme()` 会执行：

```csharp
this.FontSize.ChangeFontSizeThemeType();
this.Layout.ChangeLayoutThemeType();
this.ChangeColorTheme();
this.ChangeFontFamily();
this.ChangeIconFontFamily();
this.ChangeBackgroundTheme();
this.RefreshBrushResourceDictionary();
```

对应效果：

| 方法 | 作用 |
|---|---|
| `ChangeFontSizeThemeType()` | 替换字号资源字典。 |
| `ChangeLayoutThemeType()` | 替换布局资源字典。 |
| `ChangeColorTheme()` | 替换当前颜色资源字典。 |
| `ChangeBackgroundTheme()` | 替换当前背景资源字典。 |
| `ChangeFontFamily()` | 设置 `Application.Current.Resources[SystemKeys.FontFamily]`。 |
| `ChangeIconFontFamily()` | 设置 `Application.Current.Resources[SystemKeys.FontFamilyIcon]`。 |
| `RefreshBrushResourceDictionary()` | 刷新依赖颜色资源的画刷资源字典。 |

颜色主题替换核心逻辑：

```csharp
private void ChangeColorTheme()
{
    if (this.ColorResource == null)
        return;

    ResourceDictionary resource = this.ColorResource.Resource;
    ThemeTypeExtension.ChangeResourceDictionary(resource, x =>
    {
        return this.ColorResources.Any(l => l.Resource.Source == x.Source);
    });
}
```

`ChangeResourceDictionary` 会在 `Application.Current.Resources.MergedDictionaries` 中找到旧主题字典并替换为新主题字典。

---

## 8. 使用主题资源

### 8.1 XAML 使用 `BrushKeys`

```xaml
<TextBlock
    Text="Hello Theme"
    Foreground="{DynamicResource {x:Static h:BrushKeys.Foreground}}" />
```

```xaml
<Border
    Background="{DynamicResource {x:Static h:BrushKeys.CaptionBackground}}"
    BorderBrush="{DynamicResource {x:Static h:BrushKeys.BorderBrush}}" />
```

### 8.2 XAML 使用 `ColorKeys`

```xaml
<SolidColorBrush
    x:Key="MyAccentBrush"
    Color="{DynamicResource {x:Static h:ColorKeys.Accent}}" />
```

### 8.3 使用系统键

```xaml
<TextBlock FontFamily="{DynamicResource {x:Static h:SystemKeys.FontFamily}}" />
<TextBlock FontFamily="{DynamicResource {x:Static h:SystemKeys.FontFamilyIcon}}" />
```

`ThemeOptions.ChangeFontFamily()` 会更新：

```csharp
Application.Current.Resources[SystemKeys.FontFamily] = this.FontFamily;
```

`ThemeOptions.ChangeIconFontFamily()` 会更新：

```csharp
Application.Current.Resources[SystemKeys.FontFamilyIcon] = this.IconFontFamily;
```

### 8.4 `DynamicResource` 与 `StaticResource`

主题资源建议使用 `DynamicResource`：

```xaml
Foreground="{DynamicResource {x:Static h:BrushKeys.Foreground}}"
```

原因：主题切换时会替换 `ResourceDictionary`，`DynamicResource` 能自动重新解析资源并刷新 UI。

不建议用于主题资源：

```xaml
Foreground="{StaticResource {x:Static h:BrushKeys.Foreground}}"
```

`StaticResource` 在加载时解析，后续主题切换通常不会自动更新。

---

## 9. `ComponentResourceKey` 定义资源

### 9.1 XAML 中定义

`ColorKeys.xaml` 中使用：

```xaml
<Color x:Key="{ComponentResourceKey ResourceId=S.Color.Accent,
                                TypeInTargetAssembly={x:Type local:ColorKeys}}">
    #FF3399FF
</Color>
```

含义：

- `TypeInTargetAssembly={x:Type local:ColorKeys}`：资源键所属类型。
- `ResourceId=S.Color.Accent`：资源键 ID。
- 两者组合形成唯一资源键。

### 9.2 C# 中定义静态键

`ColorKeys.xaml.cs` 中对应定义：

```csharp
public static class ColorKeys
{
    public static ComponentResourceKey Accent =>
        new ComponentResourceKey(typeof(ColorKeys), "S.Color.Accent");
}
```

`BrushKeys.xaml.cs` 示例：

```csharp
public static class BrushKeys
{
    public static ComponentResourceKey Accent =>
        new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Accent");
}
```

### 9.3 XAML 中引用

推荐方式：

```xaml
<Border Background="{DynamicResource {x:Static h:BrushKeys.Accent}}" />
```

也可以直接写完整键，但不推荐，因为冗长且不利于重构：

```xaml
<Border Background="{DynamicResource {ComponentResourceKey ResourceId=S.Brush.Accent,
    TypeInTargetAssembly={x:Type h:BrushKeys}}}" />
```

### 9.4 C# 中访问

```csharp
var accentBrush = Application.Current.TryFindResource(BrushKeys.Accent) as Brush;
var accentColor = (Color)Application.Current.FindResource(ColorKeys.Accent);
```

---

## 10. 为什么使用 `ComponentResourceKey`

相比字符串 Key：

```xaml
<SolidColorBrush x:Key="AccentBrush" />
```

`ComponentResourceKey` 的优势：

### 10.1 避免资源键冲突

字符串资源键在整个资源查找范围内容易冲突。不同控件库都可能定义：

```xaml
<SolidColorBrush x:Key="AccentBrush" />
```

`ComponentResourceKey` 使用 `Type + ResourceId` 作为组合键：

```csharp
new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Accent")
```

即使其他程序集也有同名 `S.Brush.Accent`，只要 `TypeInTargetAssembly` 不同，就不会冲突。

### 10.2 更适合控件库和主题库

WPF-Control 是多程序集控件库，`ComponentResourceKey` 适合在库中公开稳定资源入口：

```csharp
BrushKeys.Accent
ColorKeys.Accent
SystemKeys.FontFamily
```

外部应用只引用公开键，不需要知道资源实际放在哪个 XAML 文件中。

### 10.3 支持强类型静态入口

资源键集中定义在 C# 静态类中：

```csharp
public static ComponentResourceKey Foreground =>
    new ComponentResourceKey(typeof(BrushKeys), "S.Brush.TextForeground");
```

优点：

- 可通过 IntelliSense 查找。
- 可集中维护注释和命名。
- 重构时比散落字符串更安全。
- 控件样式引用更统一。

### 10.4 便于主题替换

所有主题资源使用相同的键，不同主题只提供不同值。

```text
DefaultColorResource -> ColorKeys.Accent = 蓝色
DarkColorResource    -> ColorKeys.Accent = 深色主题强调色
LightColorResource   -> ColorKeys.Accent = 浅色主题强调色
```

控件始终引用：

```xaml
{DynamicResource {x:Static h:BrushKeys.Accent}}
```

主题切换时只替换资源字典，控件不需要修改。

### 10.5 资源层级更清晰

项目中通常分为：

| 键类型 | 说明 |
|---|---|
| `ColorKeys` | 原始颜色值，类型为 `Color`。 |
| `BrushKeys` | 画刷资源，通常依赖 `ColorKeys`。 |
| `SystemKeys` | 系统级资源，如字体、图标字体。 |

`BrushKeys.xaml` 中常见写法：

```xaml
<SolidColorBrush
    x:Key="{ComponentResourceKey ResourceId=S.Brush.Accent, TypeInTargetAssembly={x:Type local:BrushKeys}}"
    Color="{DynamicResource {x:Static local:ColorKeys.Accent}}" />
```

这表示画刷资源依赖颜色资源。主题切换颜色后，刷新画刷资源即可同步更新界面。

---

## 11. 自定义颜色主题

### 11.1 新建 XAML 资源字典

例如创建 `MyBlue.xaml`：

```xaml
<ResourceDictionary
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:local="clr-namespace:H.Themes;assembly=H.Theme">

    <Color x:Key="{ComponentResourceKey ResourceId=S.Color.Accent,
                                    TypeInTargetAssembly={x:Type local:ColorKeys}}">#FF0066CC</Color>
    <Color x:Key="{ComponentResourceKey ResourceId=S.Color.TextBackground,
                                    TypeInTargetAssembly={x:Type local:ColorKeys}}">#FFFFFFFF</Color>
    <Color x:Key="{ComponentResourceKey ResourceId=S.Color.TextForeground,
                                    TypeInTargetAssembly={x:Type local:ColorKeys}}">#FF202020</Color>
    <Color x:Key="{ComponentResourceKey ResourceId=S.Color.TextBorderBrush,
                                    TypeInTargetAssembly={x:Type local:ColorKeys}}">#FFE0E0E0</Color>
</ResourceDictionary>
```

注意：自定义主题需要覆盖控件实际使用到的关键颜色。建议参考 `H.Theme/ColorKeys.xaml` 或已有 `H.Themes.Colors.*/*.xaml`。

### 11.2 新建 `IColorResource` 实现

```csharp
[Display(Name = "我的蓝色", GroupName = "自定义主题", Description = "自定义蓝色配色")]
public class MyBlueColorResource : ColorResourceBase
{
    public MyBlueColorResource()
    {
        this.IsDark = false;
    }

    public override ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/MyApp;component/Themes/MyBlue.xaml")
    };
}
```

如果需要多语言显示名称，可参考内置 `ResxColorResourceBase`。

### 11.3 注册到主题选项

```csharp
protected override void Configure(IApplicationBuilder app)
{
    base.Configure(app);

    app.UseThemeOptions(x =>
    {
        x.ColorResources.Add(new MyBlueColorResource());
    });
}
```

或在默认主题组合后追加：

```csharp
app.UseDefaultThemeOptions(x =>
{
    x.ConfigOptions<IColorThemeOptions>(color =>
    {
        color.ColorResources.Add(new MyBlueColorResource());
    });
});
```

> 如果你的 `IDefaultThemeOptions` 扩展封装不开放该写法，可直接调用 `app.UseThemeOptions(...)` 追加颜色资源。

---

## 12. 自定义资源键

如果业务模块需要独立主题资源，建议定义自己的 Key 类型。

### 12.1 定义 Key 类

```csharp
namespace MyApp.Themes;

public static class MyModuleBrushKeys
{
    public static ComponentResourceKey WarningBackground =>
        new ComponentResourceKey(typeof(MyModuleBrushKeys), "MyModule.Brush.WarningBackground");

    public static ComponentResourceKey WarningForeground =>
        new ComponentResourceKey(typeof(MyModuleBrushKeys), "MyModule.Brush.WarningForeground");
}
```

### 12.2 在 XAML 中定义资源

```xaml
<ResourceDictionary
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:theme="clr-namespace:MyApp.Themes">

    <SolidColorBrush
        x:Key="{ComponentResourceKey ResourceId=MyModule.Brush.WarningBackground,
                                        TypeInTargetAssembly={x:Type theme:MyModuleBrushKeys}}"
        Color="#FFFFF4CE" />

    <SolidColorBrush
        x:Key="{ComponentResourceKey ResourceId=MyModule.Brush.WarningForeground,
                                        TypeInTargetAssembly={x:Type theme:MyModuleBrushKeys}}"
        Color="#FF7A3E00" />
</ResourceDictionary>
```

### 12.3 引用资源

```xaml
<Border Background="{DynamicResource {x:Static theme:MyModuleBrushKeys.WarningBackground}}">
    <TextBlock
        Text="警告"
        Foreground="{DynamicResource {x:Static theme:MyModuleBrushKeys.WarningForeground}}" />
</Border>
```

---

## 13. 自定义字号和布局主题

字号和布局主题由枚举 + 资源字典组成。

### 13.1 字号主题

`FontSizeThemeExtension`：

```csharp
public class FontSizeThemeExtension : MarkupExtension
{
    public FontSizeThemeType Type { get; set; }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return this.Type.GetFontSizeResource();
    }
}
```

切换：

```csharp
ThemeOptions.Instance.FontSize = FontSizeThemeType.Large;
ThemeOptions.Instance.RefreshThemeCommand.Execute(null);
```

### 13.2 布局主题

`LayoutThemeExtension`：

```csharp
public class LayoutThemeExtension : MarkupExtension
{
    public LayoutThemeType Type { get; set; }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return this.Type.GetLayoutResource();
    }
}
```

切换：

```csharp
ThemeOptions.Instance.Layout = LayoutThemeType.Compact;
ThemeOptions.Instance.RefreshThemeCommand.Execute(null);
```

---

## 14. 背景主题

`BackgroundThemeExtension` 根据 `BackgroundThemeType` 加载背景资源：

```csharp
public class BackgroundThemeExtension : MarkupExtension
{
    public BackgroundThemeType Type { get; set; }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        if (this.Type == BackgroundThemeType.LinearGradientBrush)
            return new LinearGradientBrushResource().Resource;
        return new SolidColorBrushResource().Resource;
    }
}
```

`ThemeOptions` 默认提供：

```csharp
this.BackgroundResources.Add(new SolidColorBrushResource());
this.BackgroundResources.Add(new LinearGradientBrushResource());
this.BackgroundResources.Add(new RadialGradientBrushResource());
this.BackgroundResources.Add(new DrawingBrushResource());
this.BackgroundResources.Add(new ImageBrushResource());
```

切换背景时调用 `ChangeBackgroundTheme()` 替换资源字典。

---

## 15. 在控件样式中使用主题资源

控件库样式中建议只引用稳定资源键，不写死颜色：

```xaml
<Style TargetType="Button">
    <Setter Property="Foreground" Value="{DynamicResource {x:Static h:BrushKeys.Foreground}}" />
    <Setter Property="Background" Value="{DynamicResource {x:Static h:BrushKeys.CaptionBackground}}" />
    <Setter Property="BorderBrush" Value="{DynamicResource {x:Static h:BrushKeys.BorderBrush}}" />
</Style>
```

鼠标悬停或选中状态：

```xaml
<Trigger Property="IsMouseOver" Value="True">
    <Setter Property="Background" Value="{DynamicResource {x:Static h:BrushKeys.MouseOver}}" />
</Trigger>

<Trigger Property="IsPressed" Value="True">
    <Setter Property="Background" Value="{DynamicResource {x:Static h:BrushKeys.Selected}}" />
</Trigger>
```

---

## 16. 运行时切换主题

### 16.1 设置当前颜色资源

```csharp
var dark = ThemeOptions.Instance.ColorResources.FirstOrDefault(x => x.IsDark);
if (dark != null)
{
    ThemeOptions.Instance.ColorResource = dark;
    ThemeOptions.Instance.RefreshThemeCommand.Execute(null);
}
```

### 16.2 切换明暗主题

`ThemeOptions.SwitchDark()` 会根据 `IsDark` 在 `ColorResources` 中选择深色或浅色资源，并刷新主题。

使用方式：

```csharp
ThemeOptions.Instance.IsDark = !ThemeOptions.Instance.IsDark;
ThemeOptions.Instance.RefreshThemeCommand.Execute(null);
```

### 16.3 保存主题配置

```csharp
ThemeOptions.Instance.Save(out string message);
```

下次启动时 `LoadThemeOptionsService` 会自动加载配置并刷新主题。

---

## 17. 二次开发建议

- 应用启动资源中先合并 `FontSizeTheme`、`LayoutTheme`、`ColorTheme`、`BackgroundTheme`。
- 业务控件样式使用 `DynamicResource` 引用 `BrushKeys`，不要写死颜色值。
- 原始颜色用 `ColorKeys`，UI 属性优先用 `BrushKeys`。
- 字体使用 `SystemKeys.FontFamily`，图标字体使用 `SystemKeys.FontFamilyIcon`。
- 自定义主题资源必须使用与系统一致的 `ComponentResourceKey`，否则控件无法自动匹配。
- 自定义模块资源建议创建独立 `MyModuleBrushKeys`，避免污染全局主题键。
- 大量颜色主题建议继承 `ColorResourceBase` 并注册到 `ThemeOptions.ColorResources`。
- 主题切换后如果画刷依赖颜色资源，记得刷新 `BrushKeys.xaml` 对应资源字典。
- 主题资源用于运行时切换时使用 `DynamicResource`，静态不变资源才考虑 `StaticResource`。

---

## 18. 常见问题

### 主题切换后界面没有刷新

检查：

1. 控件是否使用 `DynamicResource`。
2. 是否调用了 `ThemeOptions.Instance.RefreshThemeCommand`。
3. 自定义资源字典是否使用了与系统一致的 `ComponentResourceKey`。
4. 新主题资源是否已加入 `ThemeOptions.ColorResources`。

### 自定义颜色主题没有出现在设置页

检查：

```csharp
app.UseThemeOptions(x =>
{
    x.ColorResources.Add(new MyBlueColorResource());
});
```

并确认已调用：

```csharp
services.AddTheme();
app.UseThemeOptions();
```

### `ComponentResourceKey` 找不到资源

检查：

1. `TypeInTargetAssembly` 是否和 C# 静态键类型一致。
2. `ResourceId` 字符串是否完全一致。
3. 资源字典是否已合并到 `Application.Current.Resources.MergedDictionaries`。
4. XAML 命名空间是否指向正确程序集。

### 使用 `StaticResource` 后主题不更新

主题资源需要使用：

```xaml
{DynamicResource {x:Static h:BrushKeys.Foreground}}
```

不要使用：

```xaml
{StaticResource {x:Static h:BrushKeys.Foreground}}
```

### 画刷颜色没有随颜色主题变化

`BrushKeys.xaml` 中的画刷依赖 `ColorKeys`，颜色主题切换后需要刷新画刷资源字典。`ThemeOptions.RefreshTheme()` 已调用：

```csharp
ThemeTypeExtension.RefreshBrushResourceDictionary();
```

如果手动切换颜色资源，请确保也调用刷新主题流程。
