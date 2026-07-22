# Theme 主题系统二次开发文档

**适用项目：** `H.Theme`、`H.Modules.Theme`、`H.ApplicationBases.Themes`、`H.Themes.Colors.*`  
**核心类型：** `ThemeOptions`、`ColorResourceBase`、`IColorResource`、`ColorKeys`、`BrushKeys`、`LayoutKeys`、`FontSizeKeys`、`SystemKeys`、`ThemeTypeExtension`  
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

### 8.5 `BrushKeys` 完整语义

`BrushKeys` 提供可直接赋给 `Background`、`Foreground`、`BorderBrush`、`Fill`、`Stroke` 等属性的 `Brush` 资源。它与 `ColorKeys` 的区别是：

```text
ColorKeys  → Color
BrushKeys  → Brush（通常是引用 ColorKeys 的 SolidColorBrush）
```

业务控件一般优先使用 `BrushKeys`：

```xaml
<Border
    Background="{DynamicResource {x:Static h:BrushKeys.CaptionBackground}}"
    BorderBrush="{DynamicResource {x:Static h:BrushKeys.BorderBrush}}">
    <TextBlock
        Foreground="{DynamicResource {x:Static h:BrushKeys.Foreground}}"
        Text="主题文本" />
</Border>
```

#### 8.5.1 背景类 Key

| Key | ResourceId | 意义与推荐用途 | 当前状态 |
|---|---|---|---|
| `CaptionBackground` | `S.Brush.CaptionBackground` | 标题栏、分组标题、卡片标题或强调区域的背景。 | 可用。 |
| `Background` | `S.Brush.TextBackground` | 控件和内容区域的通用背景。 | 静态 Key 存在，但 `BrushKeys.xaml` 中对应画刷当前被注释；使用前确认所选主题是否另行提供。 |
| `BackgroundDisabled` | `S.Brush.TextBackground.Disabled` | 禁用状态背景。 | `[Obsolete]`，默认画刷字典未定义，不建议新代码使用。 |
| `AlternatingRowBackground` | `S.Brush.RowIndex.BackGround` | `DataGrid`、列表、表格偶数/奇数交替行背景。 | 可用。 |

交替行示例：

```xaml
<DataGrid
    AlternatingRowBackground="{DynamicResource {x:Static h:BrushKeys.AlternatingRowBackground}}"
    AlternationCount="2" />
```

#### 8.5.2 前景和交互状态 Key

| Key | ResourceId | 意义与推荐用途 | 当前状态 |
|---|---|---|---|
| `CaptionForeground` | `S.Brush.CaptionForeground` | 标题栏或标题背景上的文字、图标前景。 | 可用。 |
| `Foreground` | `S.Brush.TextForeground` | 正文、普通标签和默认图标前景。 | 可用。 |
| `ForegroundTitle` | `S.Brush.TextForeground.Title` | 页面标题、分组标题、重点标题文字。 | 可用。 |
| `ForegroundSelect` | `S.Brush.TextForeground.Select` | 选中项上的文字或图标前景，应与 `Selected` 搭配保证对比度。 | 可用。 |
| `MouseOver` | `S.Brush.TextMouseOver` | 鼠标悬停时的背景或浅层交互高亮，不是普通正文前景。 | 可用。 |
| `Selected` | `S.Brush.TextSelected` | 选中、按下或激活项的背景。 | 可用。 |
| `ForegroundAssist` | `S.Brush.TextForeground.Assist` | 次要说明、占位提示、辅助文字、弱化图标。 | 可用。 |
| `ForegroundLink` | `S.Brush.TextForeground.Link` | `Hyperlink`、可点击文字和导航入口。 | 可用。 |

状态样式示例：

```xaml
<Style TargetType="ListBoxItem">
    <Setter Property="Foreground" Value="{DynamicResource {x:Static h:BrushKeys.Foreground}}" />
    <Style.Triggers>
        <Trigger Property="IsMouseOver" Value="True">
            <Setter Property="Background" Value="{DynamicResource {x:Static h:BrushKeys.MouseOver}}" />
        </Trigger>
        <Trigger Property="IsSelected" Value="True">
            <Setter Property="Background" Value="{DynamicResource {x:Static h:BrushKeys.Selected}}" />
            <Setter Property="Foreground" Value="{DynamicResource {x:Static h:BrushKeys.ForegroundSelect}}" />
        </Trigger>
    </Style.Triggers>
</Style>
```

#### 8.5.3 白色及透明度前景 Key

| Key | 意义 | 当前状态 |
|---|---|---|
| `ForegroundWhite` | 纯白前景。 | `[Obsolete]`，默认定义被注释。新代码使用 `White` 或语义化前景。 |
| `ForegroundWhiteOpacity9` | 90% 不透明白色前景。 | `[Obsolete]`，默认定义被注释。 |
| `ForegroundWhiteOpacity8` | 80% 不透明白色前景。 | `[Obsolete]`，默认定义被注释。 |
| `ForegroundWhiteOpacity7` | 70% 不透明白色前景。 | `[Obsolete]`，默认定义被注释。 |
| `ForegroundWhiteOpacity6` | 60% 不透明白色前景。 | `[Obsolete]`，默认定义被注释。 |
| `ForegroundWhiteOpacity5` | 50% 不透明白色前景。 | 未标记 `[Obsolete]`，但默认定义同样被注释，使用前必须自行提供资源。 |
| `ForegroundWhiteOpacity4` | 40% 不透明白色前景。 | `[Obsolete]`，默认定义被注释。 |
| `ForegroundWhiteOpacity3` | 30% 不透明白色前景。 | `[Obsolete]`，默认定义被注释。 |
| `ForegroundWhiteOpacity2` | 20% 不透明白色前景。 | `[Obsolete]`，默认定义被注释。 |
| `ForegroundWhiteOpacity1` | 10% 不透明白色前景。 | `[Obsolete]`，默认定义被注释。 |

这些 Key 保留主要是为了兼容旧样式。新代码应优先使用 `CaptionForeground`、`Foreground`、`ForegroundAssist` 等语义 Key，让深色和浅色主题自行决定颜色。

#### 8.5.4 边框 Key

| Key | ResourceId | 意义与推荐用途 | 当前状态 |
|---|---|---|---|
| `BorderBrush` | `S.Brush.TextBorderBrush` | 输入框、按钮、卡片、分隔区域的默认边框。 | 可用。 |
| `BorderBrushTitle` | `S.Brush.TextBorderBrush.Title` | 标题区域、重点分组或较明显的边界。 | 可用。 |
| `BorderBrushAssist` | `S.Brush.TextBorderBrush.Assist` | 辅助分隔线、弱边框和次要区域边界。 | 可用。 |
| `BorderBrushDisabled` | `S.Brush.TextBorderBrush.Disabled` | 禁用控件边框。 | `[Obsolete]`，默认画刷字典未定义。 |

边框强弱建议：

```text
BorderBrushTitle  → 较强调
BorderBrush       → 常规
BorderBrushAssist → 较弱
```

实际明暗关系由当前颜色主题决定，不应在业务代码中假设具体 RGB 值。

#### 8.5.5 强调色 Key

| Key | ResourceId | 意义与推荐用途 |
|---|---|---|
| `Accent` | `S.Brush.Accent` | 当前主题主强调色，用于主按钮、焦点、选中标识、进度和关键链接。 |

```xaml
<Button
    Background="{DynamicResource {x:Static h:BrushKeys.Accent}}"
    Foreground="{DynamicResource {x:Static h:BrushKeys.ForegroundSelect}}"
    Content="确定" />
```

#### 8.5.6 中性明暗阶梯 Key

`Dark*` 是一组连续中性色阶。默认浅色资源中，`Dark10` 位于较深端，`Dark0` 位于最浅端；不同颜色主题可以替换其实际颜色。

| Key | ResourceId | 色阶位置和推荐用途 |
|---|---|---|
| `Dark10` | `S.Brush.Dark.10` | 最深端；高对比文字、深色遮罩或深色结构。 |
| `Dark9_5` | `S.Brush.Dark.9.5` | `Dark10` 与 `Dark9` 之间的半级。 |
| `Dark9` | `S.Brush.Dark.9` | 很深的中性色。 |
| `Dark8_5` | `S.Brush.Dark.8.5` | `Dark9` 与 `Dark8` 之间的半级。 |
| `Dark8` | `S.Brush.Dark.8` | 深色背景或深色边界。 |
| `Dark7_5` | `S.Brush.Dark.7.5` | `Dark8` 与 `Dark7` 之间的半级。 |
| `Dark7` | `S.Brush.Dark.7` | 较深中性色。 |
| `Dark6_5` | `S.Brush.Dark.6.5` | `Dark7` 与 `Dark6` 之间的半级。 |
| `Dark6` | `S.Brush.Dark.6` | 中深色结构、图标或文字。 |
| `Dark5_5` | `S.Brush.Dark.5.5` | `Dark6` 与 `Dark5` 之间的半级。 |
| `Dark5` | `S.Brush.Dark.5` | 中间偏深的中性色。 |
| `Dark4_5` | `S.Brush.Dark.4.5` | `Dark5` 与 `Dark4` 之间的半级。 |
| `Dark4` | `S.Brush.Dark.4` | 中间中性色。 |
| `Dark3_5` | `S.Brush.Dark.3.5` | `Dark4` 与 `Dark3` 之间的半级。 |
| `Dark3` | `S.Brush.Dark.3` | 中间偏浅的中性色。 |
| `Dark2_5` | `S.Brush.Dark.2.5` | `Dark3` 与 `Dark2` 之间的半级。 |
| `Dark2` | `S.Brush.Dark.2` | 较浅边框或表面。 |
| `Dark1_5` | `S.Brush.Dark.1.5` | `Dark2` 与 `Dark1` 之间的半级。 |
| `Dark1` | `S.Brush.Dark.1` | 浅色边界或次级表面。 |
| `Dark0_9` | `S.Brush.Dark.0.9` | 浅色阶第 9 级。 |
| `Dark0_8` | `S.Brush.Dark.0.8` | 浅色阶第 8 级。 |
| `Dark0_7` | `S.Brush.Dark.0.7` | 浅色阶第 7 级。 |
| `Dark0_6` | `S.Brush.Dark.0.6` | 浅色阶第 6 级。 |
| `Dark0_5` | `S.Brush.Dark.0.5` | 浅色阶中间值。 |
| `Dark0_4` | `S.Brush.Dark.0.4` | 很浅的结构色。 |
| `Dark0_3` | `S.Brush.Dark.0.3` | 很浅的边框或表面色。 |
| `Dark0_2` | `S.Brush.Dark.0.2` | 接近背景的浅色。 |
| `Dark0_1` | `S.Brush.Dark.0.1` | 极浅的交替或网格表面。 |
| `Dark0` | `S.Brush.Dark.0` | 最浅端；默认主题中接近白色。 |

色阶适合绘图、网格、层级背景和需要精细灰阶的控件。常规业务文本、边框和交互状态仍应优先使用语义 Key，以获得更稳定的主题适配。

#### 8.5.7 固定色名 Key

| Key | 意义与推荐用途 | 当前状态 |
|---|---|---|
| `LightGray` | 浅灰色，适合非关键背景或占位区域。 | 可用。 |
| `LightGrayOpacity5` | 50% 浅灰色。 | `[Obsolete]`，默认画刷字典未定义。 |
| `Gray` | 灰色，适合中性状态。 | 可用。 |
| `GrayOpacity5` | 50% 灰色。 | `[Obsolete]`，默认画刷字典未定义；其 ResourceId 还保留历史尾逗号。 |
| `Black` | 框架定义的黑/深色，不保证等于 `#000000`。 | 可用。 |
| `Orange` | 警告、提醒、待处理状态。 | 可用。 |
| `Red` | 错误、危险、删除、失败状态。 | 可用。 |
| `Green` | 成功、通过、正常、在线状态。 | 可用。 |
| `Yellow` | 提醒、高亮、注意状态。 | 可用。 |
| `Blue` | 信息、普通链接或蓝色业务标识。 | 可用。 |
| `Purple` | 紫色分类或业务状态。 | 可用。 |
| `Brown` | 棕色分类或业务状态。 | 可用。 |
| `LightBlue` | 浅蓝信息背景或弱提示。 | 可用。 |
| `Pink` | 粉色分类或业务状态。 | 可用。 |
| `White` | 固定白色，默认定义为 `#FFFFFF`。 | 可用。 |

固定色名 Key 主要用于具有固定颜色语义的状态。若颜色需要跟随品牌主题，应使用 `Accent` 或其他语义 Key。

#### 8.5.8 系统、透明纹理和菜单 Key

| Key | ResourceId | 意义与推荐用途 | 当前状态 |
|---|---|---|---|
| `DialogCover` | `S.Brush.Dialog.Cover` | 模态对话框背后的遮罩层。 | 静态 Key 存在，但默认 `BrushKeys.xaml` 未定义；需由消息/主题模块或应用提供。 |
| `Tranparent` | `S.Brush.Tranparent` | 名称保留历史拼写；当前实际是小尺寸棋盘格 `DrawingBrush`，用于表示透明区域，并非 `Brushes.Transparent`。 | 可用。 |
| `Tile` | `S.Brush.Tile` | 较大棋盘格平铺背景，默认 Viewport 为 `25 × 25`。 | 可用。 |
| `Tile25` | `S.Brush.Tile.25` | 更密集的棋盘格平铺背景，当前 Viewport 为 `12 × 12`。 | 可用。 |
| `MenuBackground` | `S.Brush.Menu.Background` | 菜单、侧边菜单或导航区域背景。 | 画刷定义可用，所选颜色主题还需提供 `ColorKeys.MenuBackground`。 |
| `MenuForeground` | `S.Brush.Menu.Foreground` | 菜单、侧边菜单或导航区域文字与图标前景。 | 画刷定义可用，所选颜色主题还需提供 `ColorKeys.MenuForeground`。 |

透明棋盘格示例：

```xaml
<Border
    Width="120"
    Height="80"
    Background="{DynamicResource {x:Static h:BrushKeys.Tranparent}}" />
```

如果需要真正透明的画刷，应直接使用：

```xaml
Background="Transparent"
```

#### 8.5.9 Key 存在不代表默认资源一定存在

`BrushKeys.xaml.cs` 是公开 Key 清单，而 `BrushKeys.xaml` 是默认资源实现。部分兼容 Key 已废弃、被注释或需要其他模块提供。使用非主流 Key 前可检查：

```csharp
Brush brush = Application.Current.TryFindResource(BrushKeys.DialogCover) as Brush;
```

自定义主题应至少完整提供应用实际使用的 Key。缺少动态资源通常不会在编译期报错，而会在运行时表现为属性没有得到预期画刷。

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
ThemeOptions.Instance.Layout = LayoutThemeType.Small;
ThemeOptions.Instance.RefreshThemeCommand.Execute(null);
```

### 13.3 `LayoutKeys` 完整说明

`LayoutKeys` 统一描述控件高度、间距和圆角，资源值类型不完全相同：

| Key | ResourceId | 资源类型 | 意义与推荐用途 |
|---|---|---|---|
| `WindowCaptionHeight` | `S.Layout.WindowCaptionHeight` | `Double` | 自定义窗口标题栏高度、标题栏按钮高度。 |
| `ItemHeight` | `S.Layout.ItemHeight` | `Double` | 普通按钮、输入框、菜单项、列表项的统一交互高度。 |
| `IconHeight` | `S.Layout.IconHeight` | `Double` | 普通图标的建议宽高。 |
| `RowHeight` | `S.Layout.RowHeight` | `Double` | `DataGridRow`、表格行或较高列表行的高度。 |
| `CornerRadius` | `S.Layout.CornerRadius` | `CornerRadius` | 按钮、输入框、卡片和弹层的统一圆角。 |
| `Padding` | `S.Layout.Padding` | `Thickness` | 控件内部内容留白。 |
| `Margin` | `S.Layout.Margin` | `Thickness` | 相邻控件之间的外部间距。 |

使用示例：

```xaml
<Button
    Height="{DynamicResource {x:Static h:LayoutKeys.ItemHeight}}"
    Margin="{DynamicResource {x:Static h:LayoutKeys.Margin}}"
    Padding="{DynamicResource {x:Static h:LayoutKeys.Padding}}"
    Content="保存" />
```

```xaml
<Border
    CornerRadius="{DynamicResource {x:Static h:LayoutKeys.CornerRadius}}">
    <Image
        Width="{DynamicResource {x:Static h:LayoutKeys.IconHeight}}"
        Height="{DynamicResource {x:Static h:LayoutKeys.IconHeight}}" />
</Border>
```

不要把 `CornerRadius` 或 `Thickness` Key 用在要求 `Double` 的属性上。资源类型不匹配会导致 XAML 运行时错误或属性无法设置。

### 13.4 三套 `LayoutThemeType` 的实际值

| Key | `Small` 紧凑 | `Default` 常规 | `Large` 宽松 |
|---|---:|---:|---:|
| `WindowCaptionHeight` | `40` | `45` | `50` |
| `ItemHeight` | `30` | `35` | `40` |
| `IconHeight` | `15` | `18` | `20` |
| `RowHeight` | `35` | `40` | `45` |
| `CornerRadius` | `1` | `2` | `4` |
| `Padding` | `2 0` | `5 0` | `10 0` |
| `Margin` | `2 1` | `5 3` | `10 6` |

切换时框架会替换：

```text
Default → /H.Theme;component/LayoutKeys.xaml
Large   → /H.Theme;component/Layouts/Large.xaml
Small   → /H.Theme;component/Layouts/Small.xaml
```

控件只有使用 `DynamicResource` 引用 `LayoutKeys`，才能在切换后自动采用新尺寸。

### 13.5 `FontSizeKeys` 完整说明

`FontSizeKeys` 的资源类型都是 `Double`：

| Key | ResourceId | 意义与推荐用途 |
|---|---|---|
| `Default` | `S.FontSize.Default` | 正文、表单、按钮、菜单和普通控件默认字号。 |
| `Header` | `S.FontSize.Header` | 普通标题或分组标题，比默认正文略大。 |
| `Header1` | `S.FontSize.Header.1` | 最大一级标题，适合页面主标题。 |
| `Header2` | `S.FontSize.Header.2` | 二级标题。 |
| `Header3` | `S.FontSize.Header.3` | 三级标题或大号强调文本。 |
| `Header4` | `S.FontSize.Header.4` | 四级标题或卡片主标题。 |
| `Header5` | `S.FontSize.Header.5` | 五级标题；默认主题中与 `Header` 同为 `14`。 |
| `Header6` | `S.FontSize.Header.6` | 六级标题；默认主题中与正文同为 `12`。 |
| `Header7` | `S.FontSize.Header.7` | 小号辅助文本。 |
| `Header8` | `S.FontSize.Header.8` | 更小的标注文字。 |
| `Header9` | `S.FontSize.Header.9` | 最小字号层级，只适合特殊缩略标注。 |
| `Icon` | `S.FontSize.Icon` | 字体图标字号，不等同于 `LayoutKeys.IconHeight`。 |

标题编号越小，字号越大：

```text
Header1 > Header2 > ... > Header9
```

使用示例：

```xaml
<StackPanel>
    <TextBlock
        FontSize="{DynamicResource {x:Static h:FontSizeKeys.Header1}}"
        Text="页面标题" />
    <TextBlock
        FontSize="{DynamicResource {x:Static h:FontSizeKeys.Header4}}"
        Text="卡片标题" />
    <TextBlock
        FontSize="{DynamicResource {x:Static h:FontSizeKeys.Default}}"
        Text="正文内容" />
    <TextBlock
        FontFamily="{DynamicResource {x:Static h:SystemKeys.FontFamilyIcon}}"
        FontSize="{DynamicResource {x:Static h:FontSizeKeys.Icon}}"
        Text="&#xE8B7;" />
</StackPanel>
```

### 13.6 三套 `FontSizeThemeType` 的实际值

| Key | `Small` | `Default` | `Large` |
|---|---:|---:|---:|
| `Default` | `10` | `12` | `14` |
| `Header` | `12` | `14` | `16` |
| `Header1` | `20` | `22` | `24` |
| `Header2` | `18` | `20` | `22` |
| `Header3` | `16` | `18` | `20` |
| `Header4` | `14` | `16` | `18` |
| `Header5` | `12` | `14` | `16` |
| `Header6` | `10` | `12` | `14` |
| `Header7` | `8` | `10` | `12` |
| `Header8` | `6` | `8` | `10` |
| `Header9` | `4` | `6` | `8` |
| `Icon` | `16` | `16` | `20` |

注意：

- `Small` 的 `Header9 = 4` 非常小，不适合普通可读文本。
- `Small` 与 `Default` 的 `Icon` 都是 `16`，只有 `Large` 提升到 `20`。
- 字体图标为了清晰，优先使用字体推荐的离散字号；框架默认注释建议 `16、20、24、32、40、48、64`。
- `FontSizeKeys.Icon` 控制字体图标字号，`LayoutKeys.IconHeight` 控制布局占用尺寸，两者可以同时使用。

### 13.7 在控件默认样式中统一应用

```xaml
<Style TargetType="TextBlock">
    <Setter Property="FontSize" Value="{DynamicResource {x:Static h:FontSizeKeys.Default}}" />
    <Setter Property="Foreground" Value="{DynamicResource {x:Static h:BrushKeys.Foreground}}" />
</Style>

<Style TargetType="Button">
    <Setter Property="Height" Value="{DynamicResource {x:Static h:LayoutKeys.ItemHeight}}" />
    <Setter Property="Padding" Value="{DynamicResource {x:Static h:LayoutKeys.Padding}}" />
    <Setter Property="Margin" Value="{DynamicResource {x:Static h:LayoutKeys.Margin}}" />
    <Setter Property="FontSize" Value="{DynamicResource {x:Static h:FontSizeKeys.Default}}" />
</Style>
```

这样切换字号和布局主题时，业务页面无需逐个修改控件属性。

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
