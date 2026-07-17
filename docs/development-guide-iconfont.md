# IconFont 系统图标二次开发文档

**适用项目：** `H.Extensions.FontIcon`、`H.Style`、`H.Theme`、业务应用项目  
**核心类型：** `FontIcons`、`IconFontFamilys`、`FontIconTextBlock`、`FontIconButton`、`FontIconToggleButton`、`IconAttribute`、`SystemKeys.FontFamilyIcon`  
**相关能力：** 字体图标、Segoe MDL2 Assets、Segoe Fluent Icons、命令图标、主题图标字体切换、图标浏览和复制

本文介绍 WPF-Control 中 IconFont 系统图标的详细使用方式，重点说明 `Segoe MDL2 Assets` 和 `Segoe Fluent Icons` 是什么、如何在 XAML 和命令中使用字体图标、如何通过主题系统切换图标字体，以及如何扩展自己的图标使用方式。

---

## 1. IconFont 系统定位

IconFont 是用字体字形表示图标的方案。每个图标本质上是字体中的一个 Unicode 字符，例如：

```csharp
FontIcons.Add = "\ue710";
FontIcons.Delete = "\ue74d";
FontIcons.Save = "\ue74e";
```

在 WPF 中，只要控件使用正确的 `FontFamily`，并把对应字符设置到 `Text` 或 `Content`，就能显示图标。

典型使用：

```xaml
<TextBlock FontFamily="Segoe MDL2 Assets" Text="&#xE710;" />
```

或使用框架封装：

```xaml
<FontIconTextBlock Text="{x:Static h:FontIcons.Add}" />
```

---

## 2. 相关项目职责

| 项目 | 说明 |
|---|---|
| `H.Extensions.FontIcon` | 定义 `FontIcons`、图标字体 `IconFontFamilys`、图标枚举浏览扩展。 |
| `H.Style` | 提供 `FontIconTextBlock`、`FontIconButton`、`FontIconToggleButton` 样式。 |
| `H.Theme` | 提供 `SystemKeys.FontFamilyIcon`，用于主题级图标字体切换。 |
| `H.Modules.Theme` | 在 `ThemeOptions` 中维护可选图标字体集合。 |
| `H.Common` | 提供 `IconAttribute`、`IIconable`，用于命令和 Presenter 图标。 |

---

## 3. Segoe MDL2 Assets 是什么

`Segoe MDL2 Assets` 是 Windows 系统中的图标字体，最早广泛用于 UWP / Windows 10 的 MDL2 设计语言图标。

特点：

- Windows 系统内置字体。
- 包含常用系统图标，如添加、删除、保存、设置、搜索、返回、主页等。
- 图标通过 Private Use Area Unicode 码点访问，多数在 `E700` 附近开始。
- 在 WPF 中可以通过 `FontFamily="Segoe MDL2 Assets"` 使用。

示例：

```xaml
<TextBlock FontFamily="Segoe MDL2 Assets" Text="&#xE710;" />
<TextBlock FontFamily="Segoe MDL2 Assets" Text="&#xE74D;" />
<TextBlock FontFamily="Segoe MDL2 Assets" Text="&#xE74E;" />
```

对应：

| 码点 | 常见含义 |
|---|---|
| `E710` | Add |
| `E74D` | Delete |
| `E74E` | Save |

---

## 4. Segoe Fluent Icons 是什么

`Segoe Fluent Icons` 是 Microsoft Fluent Design 图标字体，面向 Windows 11 和新 Fluent 视觉风格。

特点：

- 图标风格更接近 Fluent Design。
- 新系统中通常内置，也可随项目资源嵌入使用。
- 覆盖大量 Fluent 风格系统图标。
- 与 `Segoe MDL2 Assets` 有部分相同或相近码点，但显示效果可能不同。
- 某些旧范围图标被标记为 legacy，不推荐新设计继续使用。

示例：

```xaml
<TextBlock FontFamily="Segoe Fluent Icons" Text="&#xE710;" />
<TextBlock FontFamily="Segoe Fluent Icons" Text="&#xE77B;" />
```

在项目中也可以使用内嵌字体：

```xaml
<TextBlock FontFamily="{x:Static h:IconFontFamilys.ResourceSegoeFluentIcons}" Text="&#xE710;" />
```

---

## 5. `IconFontFamilys` 图标字体来源

`H.Extensions.FontIcon.IconFontFamilys` 提供系统字体和资源字体两类入口：

```csharp
public static class IconFontFamilys
{
    public static FontFamily SystemSegoeMDL2Asset => new FontFamily("Segoe MDL2 Assets");

    public static FontFamily SystemSegoeFluentIcons => new FontFamily("Segoe Fluent Icons");

    public static FontFamily ResourceSegoeMDL2Asset =>
        new FontFamily(new Uri("pack://application:,,,/H.Extensions.FontIcon;component/Assets/SegMDL2.ttf", UriKind.Absolute),
            "./#Segoe MDL2 Assets");

    public static FontFamily ResourceSegoeFluentIcons =>
        new FontFamily(new Uri("pack://application:,,,/H.Extensions.FontIcon;component/Assets/Segoe Fluent Icons.ttf", UriKind.Absolute),
            "./#Segoe Fluent Icons");
}
```

| 属性 | 说明 |
|---|---|
| `SystemSegoeMDL2Asset` | 使用操作系统安装的 `Segoe MDL2 Assets`。 |
| `SystemSegoeFluentIcons` | 使用操作系统安装的 `Segoe Fluent Icons`。 |
| `ResourceSegoeMDL2Asset` | 使用项目内嵌的 `SegMDL2.ttf`。 |
| `ResourceSegoeFluentIcons` | 使用项目内嵌的 `Segoe Fluent Icons.ttf`。 |

建议：

- 如果希望跨系统稳定显示，优先使用 `ResourceSegoeMDL2Asset` 或 `ResourceSegoeFluentIcons`。
- 如果只面向确定存在对应字体的 Windows 环境，可使用系统字体。
- 项目默认主题中优先加入资源字体，避免目标机器缺少字体导致图标显示为方块。

---

## 6. `FontIcons` 常量

`FontIcons` 是常用图标码点的 C# 常量集合：

```csharp
public static class FontIcons
{
    public const string Add = "\ue710";
    public const string Cancel = "\ue711";
    public const string Setting = "\ue713";
    public const string Search = "\ue721";
    public const string Delete = "\ue74d";
    public const string Save = "\ue74e";
    public const string Globe = "\ue774";
}
```

XAML 使用：

```xaml
<FontIconTextBlock Text="{x:Static h:FontIcons.Add}" />
<FontIconTextBlock Text="{x:Static h:FontIcons.Delete}" />
<FontIconTextBlock Text="{x:Static h:FontIcons.Save}" />
```

C# 使用：

```csharp
button.Content = FontIcons.Add;
button.FontFamily = IconFontFamilys.ResourceSegoeMDL2Asset;
```

---

## 7. 基础 WPF 使用方式

### 7.1 `TextBlock` 直接使用

```xaml
<TextBlock
    FontFamily="Segoe MDL2 Assets"
    FontSize="24"
    Text="&#xE710;" />
```

或：

```xaml
<TextBlock
    FontFamily="{x:Static h:IconFontFamilys.ResourceSegoeMDL2Asset}"
    FontSize="24"
    Text="{x:Static h:FontIcons.Add}" />
```

### 7.2 `Button` 直接使用

```xaml
<Button
    FontFamily="Segoe MDL2 Assets"
    FontSize="20"
    Content="&#xE74E;" />
```

### 7.3 文本 + 图标组合

```xaml
<Button>
    <StackPanel Orientation="Horizontal">
        <TextBlock
            FontFamily="{DynamicResource {x:Static h:SystemKeys.FontFamilyIcon}}"
            Text="{x:Static h:FontIcons.Save}" />
        <TextBlock Margin="6,0,0,0" Text="保存" />
    </StackPanel>
</Button>
```

---

## 8. 使用 `FontIconTextBlock`

`FontIconTextBlock` 是专门用于显示字体图标的 `TextBlock`：

```csharp
public class FontIconTextBlock : TextBlock
{
    static FontIconTextBlock()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(FontIconTextBlock), new FrameworkPropertyMetadata(typeof(FontIconTextBlock)));
    }
}
```

默认样式：

```xaml
<Style TargetType="{x:Type local:FontIconTextBlock}">
    <Setter Property="FontFamily" Value="{DynamicResource {x:Static SystemKeys.FontFamilyIcon}}" />
    <Setter Property="Text" Value="&#xE700;" />
    <Setter Property="VerticalAlignment" Value="Center" />
    <Setter Property="FontSize" Value="{DynamicResource {x:Static FontSizeKeys.Icon}}" />
</Style>
```

使用示例：

```xaml
<FontIconTextBlock Text="{x:Static h:FontIcons.Add}" />
<FontIconTextBlock Text="{x:Static h:FontIcons.Search}" FontSize="18" />
<FontIconTextBlock Text="&#xE774;" />
```

优点：

- 自动绑定图标字体 `SystemKeys.FontFamilyIcon`。
- 自动使用主题字号 `FontSizeKeys.Icon`。
- 可以随主题系统切换图标字体。

---

## 9. 使用 `FontIconButton`

`FontIconButton` 是用于图标按钮的 `Button`：

```csharp
public class FontIconButton : Button
{
}
```

默认样式：

```xaml
<Setter Property="FontFamily" Value="{DynamicResource {x:Static SystemKeys.FontFamilyIcon}}" />
<Setter Property="Content" Value="&#xE711;" />
<Setter Property="Width" Value="{DynamicResource {x:Static LayoutKeys.ItemHeight}}" />
<Setter Property="Height" Value="{DynamicResource {x:Static LayoutKeys.ItemHeight}}" />
<Setter Property="FontSize" Value="{DynamicResource {x:Static FontSizeKeys.Icon}}" />
```

普通图标按钮：

```xaml
<FontIconButton Content="{x:Static h:FontIcons.Add}" />
<FontIconButton Content="{x:Static h:FontIcons.Delete}" />
```

命令图标按钮：

```xaml
<FontIconButton
    Command="{ShowGlobalizationViewCommand}"
    Style="{DynamicResource {x:Static h:FontIconButtonKeys.Command}}" />
```

`FontIconButtonKeys.Command` 样式会绑定：

```xaml
<Setter Property="Content" Value="{Binding RelativeSource={RelativeSource Mode=Self}, Path=Command.Icon}" />
<Setter Property="ToolTip" Value="{Binding RelativeSource={RelativeSource Mode=Self}, Path=Command.Name}" />
```

也就是说，只要命令实现了 `Icon` 和 `Name`，按钮就能自动显示图标和提示文本。

---

## 10. 使用 `FontIconToggleButton`

`FontIconToggleButton` 是用于开关图标的 `ToggleButton`：

```csharp
public class FontIconToggleButton : ToggleButton
{
    public string CheckedGlyph { get; set; }
    public string UncheckedGlyph { get; set; }
}
```

默认样式：

```xaml
<Setter Property="FontFamily" Value="{DynamicResource {x:Static SystemKeys.FontFamilyIcon}}" />
<Setter Property="CheckedGlyph" Value="\ue70e" />
<Setter Property="UncheckedGlyph" Value="\ue70d" />
```

触发器：

```xaml
<Trigger Property="IsChecked" Value="True">
    <Setter Property="Content" Value="{Binding RelativeSource={RelativeSource Mode=Self}, Path=CheckedGlyph}" />
</Trigger>
<Trigger Property="IsChecked" Value="False">
    <Setter Property="Content" Value="{Binding RelativeSource={RelativeSource Mode=Self}, Path=UncheckedGlyph}" />
</Trigger>
```

使用示例：

```xaml
<FontIconToggleButton />

<FontIconToggleButton
    CheckedGlyph="&#xEDB5;"
    UncheckedGlyph="&#xEF3B;" />
```

---

## 11. 命令图标：`IconAttribute`

框架中的命令通常继承 `DisplayMarkupCommandBase`，该基类会读取 `DisplayAttribute` 和 `IconAttribute`：

```csharp
public abstract class DisplayMarkupCommandBase : AsyncMarkupCommandBase, IIconable, INameable, IDescriptionable, IGroupable, IOrderable
{
    protected DisplayMarkupCommandBase()
    {
        DisplayAttribute d = this.GetType().GetCustomAttribute<DisplayAttribute>();
        this.Name = d?.Name;
        this.Description = d?.Description;
        this.GroupName = d?.GroupName;

        IconAttribute icon = this.GetType().GetCustomAttribute<IconAttribute>();
        this.Icon = icon?.Icon;
    }

    public string Name { get; set; }
    public string Icon { get; set; }
}
```

定义命令：

```csharp
[Icon(FontIcons.Globe)]
[Display(Name = "语言设置", Description = "显示设置语言")]
public class ShowGlobalizationViewCommand : ShowIocPresenterCommandBase<IGlobalizationViewPresenter>
{
}
```

XAML 使用：

```xaml
<FontIconButton
    Command="{ShowGlobalizationViewCommand}"
    Style="{DynamicResource {x:Static h:FontIconButtonKeys.Command}}" />
```

显示效果：

- `Content` = `Command.Icon`。
- `ToolTip` = `Command.Name`。
- `FontFamily` = `SystemKeys.FontFamilyIcon`。

---

## 12. 与 Theme 主题系统集成

主题系统通过 `SystemKeys.FontFamilyIcon` 管理图标字体：

```csharp
public static class SystemKeys
{
    public static ComponentResourceKey FontFamilyIcon =>
        new ComponentResourceKey(typeof(SystemKeys), "S.System.FontFamily.Icon");
}
```

图标控件默认使用：

```xaml
FontFamily="{DynamicResource {x:Static SystemKeys.FontFamilyIcon}}"
```

`ThemeOptions` 中维护可选图标字体：

```csharp
iconFontFamilys.Add(IconFontFamilys.ResourceSegoeFluentIcons);
iconFontFamilys.Add(IconFontFamilys.ResourceSegoeMDL2Asset);
iconFontFamilys.Add(IconFontFamilys.SystemSegoeMDL2Asset);
iconFontFamilys.Add(IconFontFamilys.SystemSegoeFluentIcons);
this.IconFontFamilys = iconFontFamilys;
this.IconFontFamily = this.IconFontFamilys.FirstOrDefault();
```

切换图标字体：

```csharp
Application.Current.Resources[SystemKeys.FontFamilyIcon] = this.IconFontFamily;
```

因此，所有使用 `DynamicResource {x:Static SystemKeys.FontFamilyIcon}` 的图标控件都会随主题配置更新。

---

## 13. 图标浏览和复制

### 13.1 `GetFontIcons`

`GetFontIconsExtension` 返回 `FontIcons` 中定义的常量集合：

```csharp
public class GetFontIconsExtension : MarkupExtension
{
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return FontIcons.GetValues().OrderBy(x => x.Item1);
    }
}
```

XAML 示例：

```xaml
<ListBox ItemsSource="{GetFontIcons}">
    <ListBox.ItemTemplate>
        <DataTemplate>
            <StackPanel Orientation="Horizontal">
                <FontIconTextBlock Text="{Binding Item2}" />
                <TextBlock Text="{Binding Item1}" />
            </StackPanel>
        </DataTemplate>
    </ListBox.ItemTemplate>
</ListBox>
```

### 13.2 `GetIconSegoes`

`GetIconSegoesExtension` 可按码点范围生成图标列表：

```csharp
public class GetIconSegoesExtension : MarkupExtension
{
    public int From { get; set; } = 0xE700;
    public int To { get; set; } = 0xE900;

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        var ds = IconSegoeProvider.GetIconSegoes(this.From, this.To);
        return new IconSegoes(ds);
    }
}
```

XAML 示例：

```xaml
<ContentPresenter Content="{GetIconSegoes From=0xE700, To=0xE900}" />
<ContentPresenter Content="{GetIconSegoes From=0xEA00, To=0xEC00}" />
<ContentPresenter Content="{GetIconSegoes From=0xF000, To=0xF200}" />
```

生成项 `IconSegoe` 包含：

| 属性 | 说明 |
|---|---|
| `CodePoint` | 十进制码点。 |
| `Key` | XAML 实体形式，如 `&#xE710;`。 |
| `Value` | 实际 Unicode 字符。 |
| `CodeKey` | C# 字符串形式，如 `\xE710`。 |
| `CopyCommand` | 复制 `Key`。 |
| `CopyCodeKeyCommand` | 复制 `CodeKey`。 |

---

## 14. 码点写法说明

同一个图标码点在不同场景写法不同。

| 场景 | 写法 | 示例 |
|---|---|---|
| XAML 字符实体 | `&#xE710;` | `<TextBlock Text="&#xE710;" />` |
| C# 字符串 Unicode 转义 | `"\ue710"` | `button.Content = "\ue710";` |
| C# 十六进制字符串 | `"\xE710"` | 属性或复制展示场景。 |
| 常量 | `FontIcons.Add` | `Text="{x:Static h:FontIcons.Add}"` |

推荐：

- 业务代码和 XAML 中优先使用 `FontIcons.xxx`。
- 临时测试或复制自官方文档时使用 `&#xE710;`。
- 自定义新图标常量时使用 `"\ue710"`。

---

## 15. 使用 Segoe MDL2 Assets 和 Segoe Fluent Icons 的区别

| 对比项 | Segoe MDL2 Assets | Segoe Fluent Icons |
|---|---|---|
| 设计体系 | MDL2 / Windows 10 风格 | Fluent / Windows 11 风格 |
| 常见环境 | Windows 10+ | Windows 11 新环境更常见 |
| 图标风格 | 线性、系统传统图标 | Fluent 风格，更现代 |
| 兼容性 | 老项目兼容性较好 | 新项目视觉更统一 |
| 使用方式 | `FontFamily="Segoe MDL2 Assets"` | `FontFamily="Segoe Fluent Icons"` |
| 项目资源 | `ResourceSegoeMDL2Asset` | `ResourceSegoeFluentIcons` |

注意：

- 相同码点在两个字体中可能显示不同图标。
- 某些码点在某个字体中不存在，会显示为空白或方块。
- 如果切换图标字体后发现图标含义变化，应固定使用指定字体或调整码点。

---

## 16. 自定义图标常量

如果官方字体中存在新图标，但 `FontIcons` 中没有常量，可以在业务项目中定义扩展常量：

```csharp
public static class MyFontIcons
{
    public const string Robot = "\ue99a";
    public const string Dashboard = "\ue9d2";
}
```

XAML 使用：

```xaml
<FontIconTextBlock Text="{x:Static local:MyFontIcons.Robot}" />
```

如果希望贡献到框架，可以将常用码点加入 `H.Extensions.FontIcon.FontIcons`。

---

## 17. 自定义图标字体

除了 Segoe 系列，也可以使用业务自己的图标字体。

### 17.1 引入字体文件

例如：

```text
Assets/MyIcons.ttf
```

设置为资源后定义：

```csharp
public static class MyIconFontFamilys
{
    public static FontFamily MyIcons =>
        new FontFamily(new Uri("pack://application:,,,/MyApp;component/Assets/MyIcons.ttf", UriKind.Absolute),
            "./#MyIcons");
}
```

### 17.2 设置为全局图标字体

```csharp
Application.Current.Resources[SystemKeys.FontFamilyIcon] = MyIconFontFamilys.MyIcons;
```

或加入主题选项：

```csharp
app.UseThemeOptions(x =>
{
    x.IconFontFamilys.Add(MyIconFontFamilys.MyIcons);
});
```

> 是否可以直接添加取决于当前 `IIconFontFamilysOptions` 暴露的集合类型。若集合只读，可通过自定义 `ThemeOptions` 或应用启动时直接设置 `SystemKeys.FontFamilyIcon`。

### 17.3 定义自定义码点

```csharp
public static class MyIcons
{
    public const string Home = "\ue001";
    public const string Report = "\ue002";
}
```

使用：

```xaml
<FontIconTextBlock
    FontFamily="{x:Static local:MyIconFontFamilys.MyIcons}"
    Text="{x:Static local:MyIcons.Home}" />
```

---

## 18. 常见使用场景

### 18.1 菜单命令图标

```csharp
[Icon(FontIcons.Save)]
[Display(Name = "保存")]
public class SaveCommand : DisplayMarkupCommandBase
{
    public override Task ExecuteAsync(object parameter)
    {
        return Task.CompletedTask;
    }
}
```

```xaml
<MenuItem Command="{local:SaveCommand}" />
```

如果菜单样式绑定 `Command.Icon`，即可自动显示图标。

### 18.2 工具栏按钮

```xaml
<StackPanel Orientation="Horizontal">
    <FontIconButton Content="{x:Static h:FontIcons.Add}" ToolTip="新增" />
    <FontIconButton Content="{x:Static h:FontIcons.Edit}" ToolTip="编辑" />
    <FontIconButton Content="{x:Static h:FontIcons.Delete}" ToolTip="删除" />
    <FontIconButton Content="{x:Static h:FontIcons.Save}" ToolTip="保存" />
</StackPanel>
```

### 18.3 状态提示图标

```xaml
<StackPanel Orientation="Horizontal">
    <FontIconTextBlock Text="{x:Static h:FontIcons.Warning}" Foreground="Orange" />
    <TextBlock Margin="6,0,0,0" Text="存在未保存的更改" />
</StackPanel>
```

### 18.4 图标开关

```xaml
<FontIconToggleButton
    CheckedGlyph="{x:Static h:FontIcons.ChevronUp}"
    UncheckedGlyph="{x:Static h:FontIcons.ChevronDown}" />
```

---

## 19. 二次开发建议

- 优先使用 `FontIconTextBlock`、`FontIconButton`、`FontIconToggleButton`，避免重复设置字体和字号。
- 图标字体优先使用 `SystemKeys.FontFamilyIcon`，便于主题统一切换。
- 常用图标优先使用 `FontIcons` 常量，避免散落硬编码码点。
- 命令类使用 `[Icon(FontIcons.xxx)]`，配合 `FontIconButtonKeys.Command` 自动显示。
- 如果要求跨机器一致显示，优先使用资源字体 `ResourceSegoeMDL2Asset` 或 `ResourceSegoeFluentIcons`。
- 切换 `Segoe MDL2 Assets` 与 `Segoe Fluent Icons` 后要检查码点含义是否一致。
- 图标只表达辅助含义，重要操作仍建议保留文字或 `ToolTip`。
- 不建议用图标字体承载业务图片、复杂彩色图标或品牌 Logo。

---

## 20. 常见问题

### 图标显示为方块

检查：

1. `FontFamily` 是否正确。
2. 当前字体是否包含该码点。
3. 是否使用了系统不存在的字体。
4. 是否应改用 `IconFontFamilys.ResourceSegoeMDL2Asset` 或 `ResourceSegoeFluentIcons`。

### XAML 中 `\ue710` 不显示

XAML 属性中通常应写字符实体：

```xaml
<TextBlock Text="&#xE710;" />
```

或使用常量：

```xaml
<TextBlock Text="{x:Static h:FontIcons.Add}" />
```

`\ue710` 是 C# 字符串写法，不是普通 XAML 属性写法。

### 切换图标字体后图标变了

同一码点在 `Segoe MDL2 Assets` 和 `Segoe Fluent Icons` 中可能对应不同图形。解决方式：

- 固定使用某一个字体。
- 选择两个字体中都一致的码点。
- 针对不同字体定义不同图标常量。

### 命令按钮没有图标

检查：

1. 命令是否设置了 `Icon`。
2. 命令类是否标注 `[Icon(FontIcons.xxx)]`。
3. 按钮是否使用 `FontIconButtonKeys.Command` 样式。
4. 按钮 `FontFamily` 是否为图标字体。

### 图标大小不一致

图标字体不同，视觉边界可能不同。可通过：

```xaml
FontSize="20"
Width="32"
Height="32"
HorizontalContentAlignment="Center"
VerticalContentAlignment="Center"
```

统一显示。

---

## 21. 扩展资料

- Microsoft Learn：Segoe Fluent Icons font  
  `https://learn.microsoft.com/windows/apps/design/style/segoe-fluent-icons-font`

- Microsoft Learn：Segoe MDL2 Assets icons  
  `https://learn.microsoft.com/windows/apps/design/style/segoe-ui-symbol-font`

- WPF FontFamily 文档  
  `https://learn.microsoft.com/dotnet/api/system.windows.media.fontfamily`

- WPF 字体资源打包说明  
  `https://learn.microsoft.com/dotnet/desktop/wpf/advanced/packaging-fonts-with-applications`

- Unicode Private Use Area 说明  
  `https://learn.microsoft.com/windows/apps/design/style/segoe-fluent-icons-font#how-do-i-get-this-font`
