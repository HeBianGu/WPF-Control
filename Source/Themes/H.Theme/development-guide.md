# H.Theme 开发文档

`H.Theme` 是 WPF-Control 的基础主题资源项目，负责提供颜色、画刷、字号、布局、背景、阴影和系统级资源键。项目以 WPF `ResourceDictionary` 为核心，通过 `ComponentResourceKey` 暴露稳定资源键，并通过 `MarkupExtension` 与扩展方法支持在 XAML 或运行时切换主题资源。

## 1. 项目定位

- 提供基础主题资源：颜色、画刷、字号、布局、背景、阴影和系统资源。
- 定义统一资源键：通过 `ColorKeys`、`BrushKeys`、`FontSizeKeys`、`LayoutKeys` 等静态类集中管理。
- 提供主题入口：通过 `ColorThemeExtension`、`FontSizeThemeExtension`、`LayoutThemeExtension`、`BackgroundThemeExtension` 在 XAML 中引用资源字典。
- 支持运行时切换：通过 `ThemeTypeExtension` 替换 `Application.Current.Resources.MergedDictionaries` 中的主题资源。

## 2. 目录结构

```text
H.Theme/
├─ Backgrounds/                    # 背景资源主题
│  ├─ Default.xaml                  # 默认纯色背景资源
│  ├─ LinearGradientBrush.xaml      # 线性渐变背景资源
│  ├─ RadialGradientBrush.xaml      # 径向渐变背景资源
│  ├─ ImageBrush.xaml               # 图片背景资源
│  ├─ DrawingBrush.xaml             # DrawingBrush 背景资源
│  ├─ IBackgroundResource.cs        # 背景资源接口
│  ├─ *BrushResource.cs             # 各背景资源封装类
│  ├─ BackgroundThemeType.cs        # 背景主题枚举
│  └─ BackgroundThemeExtension.cs   # XAML 背景主题扩展
├─ Colors/                         # 颜色主题资源
│  ├─ Dark.xaml / Light.xaml        # 深色、浅色主题颜色资源
│  ├─ ColorThemeType.xaml(.cs)      # 颜色主题枚举及 XAML 资源
│  ├─ IColorResource.cs             # 颜色资源接口
│  ├─ ColorResourceBase.cs          # 颜色资源基类
│  ├─ ResxColorResourceBase.cs      # 支持多语言显示信息的颜色资源基类
│  ├─ DefaultColorResource.cs       # 默认颜色主题
│  ├─ DarkColorResource.cs          # 深色颜色主题
│  ├─ LightColorResource.cs         # 浅色颜色主题
│  └─ ColorThemeExtension.cs        # XAML 颜色主题扩展
├─ Extensions/ThemeTypeExtension.cs # 主题资源字典生成、刷新与运行时切换扩展
├─ FontSizes/                      # 字号主题资源
├─ Layouts/                        # 布局尺寸主题资源
├─ Properties/                     # 多语言资源 Resources*.resx
├─ Themes/Generic.xaml             # WPF 默认主题资源入口
├─ ColorKeys.xaml(.cs)             # 默认颜色资源与资源键
├─ BrushKeys.xaml(.cs)             # 画刷资源与资源键
├─ FontSizeKeys.xaml(.cs)          # 默认字号资源与资源键
├─ LayoutKeys.xaml(.cs)            # 默认布局尺寸资源与资源键
├─ DropShadowEffectKeys.xaml(.cs)  # 阴影效果资源与资源键
├─ SystemKeys.xaml(.cs)            # 系统级资源与资源键
├─ IResourceable.cs                # 通用资源对象接口
└─ H.Theme.csproj                  # 项目文件
```

`obj/` 和 `bin/` 是构建输出目录，不属于源码维护范围。

## 3. 资源加载关系

```text
ColorKeys.xaml / Colors/*.xaml
        ↓
BrushKeys.xaml
        ↓
控件样式、业务界面、其他主题包
```

`BrushKeys.xaml` 中的画刷通常使用 `DynamicResource` 绑定 `ColorKeys`。运行时替换颜色资源字典后，界面可以跟随主题更新。

## 4. 关键类说明

### `IResourceable`

通用资源描述接口，包含 `Name` 和 `Resource`。颜色资源和背景资源都基于该接口扩展。

### `IColorResource`

颜色资源接口，继承 `IResourceable`，增加 `GroupName` 和 `IsDark`，用于主题列表展示、分组、排序和深浅色判断。

### `ColorResourceBase`

颜色资源基类。构造时读取 `DisplayAttribute` 中的 `Name`、`GroupName`、`Order`、`Prompt`、`Description` 等展示信息，并要求派生类实现 `Resource`。

### `ResxColorResourceBase`

继承 `ColorResourceBase`，用于从 `Properties.Resources` 读取多语言展示信息。资源键约定为 `{ClassName}`、`{ClassName}_GroupName`、`{ClassName}_Prompt`、`{ClassName}_Description`。

### `DefaultColorResource`

默认颜色主题资源，加载 `pack://application:,,,/H.Theme;component/ColorKeys.xaml`。

### `DarkColorResource` / `LightColorResource`

内置深色和浅色主题：`DarkColorResource` 加载 `Colors/Dark.xaml` 且 `IsDark = true`；`LightColorResource` 加载 `Colors/Light.xaml` 且 `IsDark = false`。

### `ColorThemeExtension`

XAML 颜色主题入口，根据 `ColorThemeType` 返回 `DefaultColorResource`、`DarkColorResource` 或 `LightColorResource`。

```xaml
<ResourceDictionary.MergedDictionaries>
    <colors:ColorThemeExtension Type="Light" />
</ResourceDictionary.MergedDictionaries>
```

### `BackgroundThemeExtension`

XAML 背景主题入口，根据 `BackgroundThemeType` 返回 `Backgrounds/Default.xaml` 或 `Backgrounds/LinearGradientBrush.xaml`。

### `FontSizeThemeExtension`

XAML 字号主题入口，根据 `FontSizeThemeType` 返回 `FontSizeKeys.xaml`、`FontSizes/Large.xaml` 或 `FontSizes/Small.xaml`。

### `LayoutThemeExtension`

XAML 布局主题入口，根据 `LayoutThemeType` 返回 `LayoutKeys.xaml`、`Layouts/Large.xaml` 或 `Layouts/Small.xaml`。

### `ThemeTypeExtension`

主题切换核心扩展类，提供资源字典构造、替换和刷新能力。常用方法包括：

- `GetLayoutResource(LayoutThemeType type)`
- `GetFontSizeResource(FontSizeThemeType type)`
- `GetSystemsResource()`
- `ChangeLayoutThemeType(LayoutThemeType type)`
- `ChangeFontSizeThemeType(FontSizeThemeType type)`
- `ChangeResourceDictionary(ResourceDictionary, Func<ResourceDictionary, bool>, bool force = false)`
- `RefreshResourceDictionary(ResourceDictionary)`
- `RefreshBrushResourceDictionary()`

```csharp
using H.Themes.Extensions;
using H.Themes.FontSizes;
using H.Themes.Layouts;

FontSizeThemeType.Large.ChangeFontSizeThemeType();
LayoutThemeType.Small.ChangeLayoutThemeType();
```

## 5. 资源键类

- `ColorKeys`：定义颜色资源键，例如 `Accent`、`Background`、`Foreground`、`BorderBrush`、`Dark0` 到 `Dark10` 等。
- `BrushKeys`：定义画刷资源键，画刷颜色通常来自 `ColorKeys`。业务界面建议优先引用 `BrushKeys`。
- `FontSizeKeys`：定义全局字号资源键，避免硬编码字号。
- `LayoutKeys`：定义边距、圆角、高度、宽度、间距等布局尺寸资源键。
- `DropShadowEffectKeys`：定义阴影效果资源键，用于统一浮层、卡片、弹窗等视觉阴影。
- `SystemKeys`：定义系统级资源入口，适合放置跨主题、跨控件共用的基础资源。

## 6. 使用方式

### 合并基础主题资源

```xaml
<Application.Resources>
    <ResourceDictionary>
        <ResourceDictionary.MergedDictionaries>
            <ResourceDictionary Source="pack://application:,,,/H.Theme;component/ColorKeys.xaml" />
            <ResourceDictionary Source="pack://application:,,,/H.Theme;component/BrushKeys.xaml" />
            <ResourceDictionary Source="pack://application:,,,/H.Theme;component/FontSizeKeys.xaml" />
            <ResourceDictionary Source="pack://application:,,,/H.Theme;component/LayoutKeys.xaml" />
            <ResourceDictionary Source="pack://application:,,,/H.Theme;component/SystemKeys.xaml" />
        </ResourceDictionary.MergedDictionaries>
    </ResourceDictionary>
</Application.Resources>
```

### 使用主题扩展加载资源

```xaml
<ResourceDictionary.MergedDictionaries>
    <colors:ColorThemeExtension Type="Dark" />
    <fontSizes:FontSizeThemeExtension Type="Large" />
    <layouts:LayoutThemeExtension Type="Small" />
</ResourceDictionary.MergedDictionaries>
```

命名空间示例：

```xaml
xmlns:colors="clr-namespace:H.Themes.Colors;assembly=H.Theme"
xmlns:fontSizes="clr-namespace:H.Themes.FontSizes;assembly=H.Theme"
xmlns:layouts="clr-namespace:H.Themes.Layouts;assembly=H.Theme"
xmlns:theme="clr-namespace:H.Themes;assembly=H.Theme"
```

### 在控件中引用主题资源

```xaml
<Border
    Background="{DynamicResource {x:Static theme:BrushKeys.CaptionBackground}}"
    BorderBrush="{DynamicResource {x:Static theme:BrushKeys.BorderBrush}}"
    BorderThickness="1">
    <TextBlock
        Foreground="{DynamicResource {x:Static theme:BrushKeys.Foreground}}"
        FontSize="{DynamicResource {x:Static theme:FontSizeKeys.Default}}"
        Text="H.Theme" />
</Border>
```

## 7. 新增颜色主题流程

1. 在 `Colors/` 下新增主题 XAML，例如 `MyTheme.xaml`。
2. 在 XAML 中使用与 `ColorKeys.xaml` 一致的资源键，保证 `BrushKeys.xaml` 能继续通过 `DynamicResource` 找到颜色。
3. 新增 `MyThemeColorResource : ResxColorResourceBase`，返回对应 `ResourceDictionary`。
4. 如需通过枚举使用，扩展 `ColorThemeType` 与 `ColorThemeExtension`。
5. 如需多语言展示，在 `Properties/Resources*.resx` 中补充对应资源文本。

## 8. 新增资源键流程

1. 在对应的 `*Keys.xaml.cs` 中新增静态 `ComponentResourceKey`。
2. 在对应的 `*Keys.xaml` 中新增资源项，`ResourceId` 必须与代码中的字符串保持一致。
3. 如果资源需要随主题变化，优先使用 `DynamicResource` 链接到上游资源。
4. 检查 `Dark.xaml`、`Light.xaml` 或其他主题字典是否需要同步补齐该键。
5. 控件样式和业务界面只引用 `*Keys` 静态键，避免散落字符串资源键。

## 9. 开发约定

- 优先使用 `ComponentResourceKey`，不要在业务样式中硬编码字符串资源键。
- 颜色类资源放在 `ColorKeys` 或 `Colors/*.xaml`，画刷类资源放在 `BrushKeys.xaml`。
- 画刷建议依赖颜色资源，避免同一个颜色值在多个画刷中重复维护。
- 可被主题切换影响的资源应使用 `DynamicResource`。
- 不需要运行时变化的资源可使用 `StaticResource`。
- 新增主题资源时保持 `pack://application:,,,/H.Theme;component/...` 路径格式一致。
- 多语言展示信息优先放入 `Properties/Resources*.resx`。
- 不要手工维护 `obj/`、`bin/` 下的生成文件。

## 10. 排查建议

- 资源找不到：检查 `ComponentResourceKey` 的 `ResourceId` 是否与 XAML 中完全一致。
- 主题切换后不刷新：检查引用处是否使用了 `DynamicResource`。
- 画刷颜色未变化：检查 `BrushKeys.xaml` 是否仍引用旧颜色键，必要时调用 `RefreshBrushResourceDictionary()`。
- 布局或字号切换无效：确认应用资源中存在 `FontSizes` 或 `Layouts` 对应资源字典，且 `ThemeTypeExtension` 的筛选条件可识别其 `Source`。
- 多语言名称不生效：检查 `.resx` 键名是否与资源类名及后缀一致。
