# Globalization 多语言二次开发文档

**适用项目：** `H.Globalization`、`H.Modules.Globalization`、`H.Common`、各模块 `Properties/Resources*.resx`  
**核心类型：** `GlobalizationOptions`、`ILoadGlobalizationOptionsService`、`GlobalizationBinding`、`GetGlobalizationValueConverter`、`ResourceManagerExtesion`  
**相关能力：** 语言设置、启动加载语言、`.resx` 资源文件、卫星程序集、代码取资源、XAML 动态多语言、类型/属性/枚举多语言

本文介绍 WPF-Control 中 Globalization 多语言模块的详细使用方式，重点说明如何注册多语言服务、添加资源文件、切换语言、在代码和 XAML 中使用多语言资源，以及类型、属性、枚举等框架内置多语言命名约定。

---

## 1. Globalization 模块定位

多语言模块用于统一管理应用语言和资源文本，主要能力：

- 根据用户设置加载 `CultureInfo`。
- 设置 `CurrentCulture` 和 `CurrentUICulture`。
- 扫描应用目录下可用语言。
- 将语言设置加入设置页。
- 提供语言设置界面和命令。
- 支持 `.resx` 资源文件多语言。
- 支持代码中读取资源文本。
- 支持 XAML 中动态绑定多语言文本。
- 支持类型、属性、枚举显示名称多语言。

核心流程：

```text
AddGlobalization 注册服务
    ↓
ApplicationBase 构建 IOC 后立即加载 ILoadGlobalizationOptionsService
    ↓
GlobalizationOptions.Load 设置当前 CultureInfo
    ↓
ResourceManager 根据 CurrentUICulture 读取对应 Resources.xx.resx
    ↓
界面和代码显示对应语言文本
```

---

## 2. 相关项目职责

| 项目 | 说明 |
|---|---|
| `H.Globalization` | 基础通用资源，包含框架通用文本的多语言 `.resx`。 |
| `H.Modules.Globalization` | 多语言设置项、语言切换界面、动态绑定扩展和加载服务。 |
| `H.Common` | 提供 `ResourceManagerExtesion`，封装资源读取和命名约定。 |
| 各模块 `Properties/Resources*.resx` | 各模块自己的本地化资源。 |
| `H.Extensions.ValueConverter` | 提供枚举多语言转换器。 |

---

## 3. 注册多语言服务

### 3.1 注册服务

在 `ApplicationBase.ConfigureServices` 中注册：

```csharp
protected override void ConfigureServices(IServiceCollection services)
{
    services.AddGlobalization();
}
```

`AddGlobalization()` 会注册：

```csharp
services.TryAdd(ServiceDescriptor.Singleton<ILoadGlobalizationOptionsService, LoadGlobalizationOptionsService>());
services.TryAdd(ServiceDescriptor.Singleton<IGlobalizationViewPresenter, GlobalizationViewPresenter>());
```

### 3.2 使用设置项

在 `Configure` 中加入多语言设置项：

```csharp
protected override void Configure(IApplicationBuilder app)
{
    base.Configure(app);
    app.UseGlobalizations();
}
```

`UseGlobalizations()` 会执行：

```csharp
IocSetting.Instance.Add(GlobalizationOptions.Instance);
```

这样设置页中会出现“语言设置”。

### 3.3 完整示例

```csharp
public partial class App : ApplicationBase
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddApplicationServices();
        services.AddGlobalization();
        services.AddSetting();
    }

    protected override void Configure(IApplicationBuilder app)
    {
        base.Configure(app);
        app.UseGlobalizations();
        app.UseSettingViewOptions();
    }
}
```

---

## 4. 启动加载语言

`ApplicationBase.InitServiceCollection()` 在构建 IOC 后、显示窗口前加载多语言：

```csharp
protected void InitServiceCollection()
{
    ServiceCollection sc = new ServiceCollection();
    this.ConfigureServices(sc);
    Ioc.Build(sc);
    Ioc.GetService<ILoadGlobalizationOptionsService>(false)?.Load(out string message);
}
```

原因：

- 多语言要尽早加载。
- 主窗口创建前设置 `CurrentUICulture`。
- 避免窗口已创建后再切换导致部分静态资源未更新。

`LoadGlobalizationOptionsService` 内部调用：

```csharp
return GlobalizationOptions.Instance.Load(out message);
```

---

## 5. `GlobalizationOptions` 配置项

`GlobalizationOptions` 是语言设置项：

```csharp
[Display(Name = "语言设置", GroupName = SettingGroupNames.GroupStyle, Description = "语言设置设置的信息")]
public class GlobalizationOptions : IocOptionInstance<GlobalizationOptions>, IGlobalizationOptions, IPropertyItemValueChanged
{
}
```

核心属性：

| 属性 | 说明 |
|---|---|
| `CultureInfo` | 当前语言文化。设置后会更新当前线程和 UI 线程文化。 |
| `CultureInfoKey` | 保存到配置文件中的语言名称，如 `zh-Hans`、`en-US`。 |
| `SupportedCultureInfos` | 当前应用可用语言集合。 |

`CultureInfo` 在设置页中通过下拉框选择：

```csharp
[GetPropertyNameSource(nameof(SupportedCultureInfos))]
[DisplayMemberPath("NativeName")]
[PropertyItem(typeof(ComboBoxPropertyItem))]
[Display(Name = "语言")]
public CultureInfo CultureInfo { get; set; }
```

---

## 6. 可用语言扫描规则

`GlobalizationOptions.GetSupportedCultureInfos()` 会扫描应用基目录下的语言目录：

```csharp
foreach (var dir in Directory.GetDirectories(AppDomain.CurrentDomain.BaseDirectory))
{
    if (!File.Exists(Path.Combine(dir, $"{name}.resources.dll")))
        continue;
    DirectoryInfo langDir = new DirectoryInfo(dir);
    infos.Add(CultureInfo.GetCultureInfo(langDir.Name));
}
```

其中 `name` 是入口程序集名称：

```csharp
var name = Assembly.GetEntryAssembly().GetName().Name;
```

因此，发布后典型目录结构：

```text
App.exe
zh-Hans/App.resources.dll
zh-Hant/App.resources.dll
en-US/App.resources.dll
ja/App.resources.dll
ko/App.resources.dll
```

只有存在 `{EntryAssemblyName}.resources.dll` 的语言目录，才会出现在 `SupportedCultureInfos` 中。

> 注意：如果只是库项目有 `H.Modules.Login.resources.dll`，但入口程序集没有对应语言卫星程序集，该语言可能不会出现在语言下拉列表中。建议入口项目也提供对应语言的 `Resources.xx.resx`。

---

## 7. 切换语言机制

`GlobalizationOptions.UpdateCultureInfo` 会设置：

```csharp
CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;
Thread.CurrentThread.CurrentUICulture = culture;
Thread.CurrentThread.CurrentCulture = culture;
Application.Current.Dispatcher.Thread.CurrentUICulture = culture;
Application.Current.Dispatcher.Thread.CurrentCulture = culture;
```

作用：

- 新线程默认使用指定文化。
- 当前线程资源读取使用指定文化。
- UI 线程资源读取使用指定文化。
- 日期、数字等格式化受 `CurrentCulture` 影响。
- `.resx` 文本读取受 `CurrentUICulture` 影响。

语言改变后会提示重启：

```csharp
var r = await IocMessage.ShowDialogMessage("语言修改需要重启软件才生效，是否立即重启?".GetStringResx(this, "Message_NeedRestart"));
if (r == true)
{
    this.Save(out string message);
    Application.Current.Shutdown();
}
```

原因：

- 一些 XAML 使用 `x:Static` 或静态资源，只在加载时解析。
- 一些控件、菜单、命令在启动时已生成显示文本。
- 重启能保证所有模块使用新的 `CurrentUICulture` 重新构建。

---

## 8. 添加 `.resx` 多语言资源

### 8.1 资源文件命名

每个项目一般包含：

```text
Properties/Resources.resx          // 默认资源
Properties/Resources.zh-Hans.resx  // 简体中文
Properties/Resources.zh-Hant.resx  // 繁体中文
Properties/Resources.en-US.resx    // 英文
Properties/Resources.ja.resx       // 日文
Properties/Resources.ko.resx       // 韩文
```

命名格式：

```text
Resources.{culture-name}.resx
```

常用文化名称：

| 文化名 | 说明 |
|---|---|
| `zh-Hans` | 简体中文 |
| `zh-Hant` | 繁体中文 |
| `en-US` | 英文（美国） |
| `ja` | 日文 |
| `ko` | 韩文 |
| `de` | 德文 |
| `fr` | 法文 |
| `ru` | 俄文 |
| `es` | 西班牙文 |
| `pt` | 葡萄牙文 |

### 8.2 添加资源键

`Resources.resx`：

| Name | Value |
|---|---|
| `AppTitle` | `WPF Control` |
| `Message_NeedRestart` | `语言修改需要重启软件才生效，是否立即重启?` |

`Resources.en-US.resx`：

| Name | Value |
|---|---|
| `AppTitle` | `WPF Control` |
| `Message_NeedRestart` | `Changing the language requires restarting the application. Restart now?` |

### 8.3 生成卫星程序集

SDK-style 项目会根据 `Resources.xx.resx` 自动生成卫星程序集。编译输出类似：

```text
bin/Debug/net8.0-windows/en-US/MyApp.resources.dll
bin/Debug/net8.0-windows/zh-Hans/MyApp.resources.dll
```

如果没有生成：

- 检查 `.resx` 是否在项目中。
- 检查文件名是否符合 `Resources.xx.resx`。
- 检查 `Resources.resx` 是否存在。
- 检查项目是否正确包含资源文件。

---

## 9. 代码中读取多语言文本

### 9.1 直接使用强类型资源

```csharp
string title = Properties.Resources.AppTitle;
```

优点：简单、类型安全。  
限制：如果 UI 已经加载，静态文本通常不会自动刷新，需要重启或重新绑定。

### 9.2 使用 `GetStringResx`

`H.Common.ResourceManagerExtesion` 提供：

```csharp
public static string GetStringResx(this string def, Assembly assembly, string key)
{
    return assembly.GetResx(key, def) ?? def;
}

public static string GetStringResx(this string def, object assembly, string key)
{
    return assembly.GetType().Assembly.GetResx(key, def) ?? def;
}
```

示例：

```csharp
await IocMessage.ShowDialogMessage(
    "取消配置将不会保存，是否继续？".GetStringResx(this, "Message_CancelCannotSave"));
```

或指定程序集：

```csharp
string title = "数据过滤器".GetStringResx(typeof(FilterBox).Assembly, "Title_FilterBox");
```

好处：

- 没有资源时回退默认中文文本。
- 可按当前对象所在程序集读取资源。
- 适合消息提示、日志、命令文本等场景。

---

## 10. XAML 中使用多语言

### 10.1 `x:Static` 方式

```xaml
xmlns:p="clr-namespace:MyApp.Properties"

<TextBlock Text="{x:Static p:Resources.AppTitle}" />
```

特点：

- 写法简单。
- 加载时读取当前 `CurrentUICulture`。
- 运行时切换语言后不会自动更新，通常需要重启。

### 10.2 `GlobalizationBinding` 动态绑定

`GlobalizationBinding` 是多语言绑定扩展：

```csharp
public class GlobalizationBinding : MarkupExtension
{
    public string Key { get; set; }
    public Type ResourcesType { get; set; }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        Binding binding = new Binding(nameof(GlobalizationOptions.CultureInfo))
        {
            Source = GlobalizationOptions.Instance,
            Converter = new GetGlobalizationValueConverter
            {
                ResourcesType = this.ResourcesType,
                Key = Key
            }
        };
        binding.IsAsync = true;
        return binding.ProvideValue(serviceProvider);
    }
}
```

使用：

```xaml
xmlns:p="clr-namespace:MyApp.Properties"

<Button Content="{GlobalizationBinding ResourcesType={x:Type p:Resources}, Key=AppTitle}" />
```

当 `GlobalizationOptions.Instance.CultureInfo` 变化并触发属性通知时，绑定会重新通过 `ResourceManager` 获取资源。

### 10.3 显式 Converter 方式

```xaml
<Button Content="{Binding Source={x:Static h:GlobalizationOptions.Instance},
                          Path=CultureInfo,
                          IsAsync=True,
                          Converter={GetGlobalizationValueConverter ResourcesType={x:Type p:Resources}, Key=AppTitle}}" />
```

`GlobalizationBinding` 本质上是对上述写法的封装。

---

## 11. `GetGlobalizationValueConverter`

转换器实现：

```csharp
public class GetGlobalizationValueConverter : MarkupValueConverterBase
{
    public string Key { get; set; }
    public Type ResourcesType { get; set; }

    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (this.ResourcesType == null)
            return DependencyProperty.UnsetValue;
        var name = this.ResourcesType.FullName;
        var rm = new ResourceManager(name, this.ResourcesType.Assembly);
        return rm.GetString(this.Key);
    }
}
```

说明：

- `ResourcesType` 传入资源类类型，如 `MyApp.Properties.Resources`。
- `Key` 是资源键。
- `ResourceManager` 会根据当前 `CurrentUICulture` 读取对应 `.resx`。

---

## 12. 类型、属性、枚举多语言命名约定

`ResourceManagerExtesion` 约定了一组资源键命名规则。

### 12.1 类型名称

```csharp
[Display(Name = "主题设置", GroupName = "显示设置", Description = "主题设置设置的信息")]
public class ThemeOptions : IocOptionInstance<ThemeOptions>
{
}
```

资源键：

| 键 | 说明 |
|---|---|
| `Type_ThemeOptions` | 类型显示名称。 |
| `Type_ThemeOptions_GroupName` | 类型分组名称。 |
| `Type_ThemeOptions_Description` | 类型说明。 |

读取方法：

```csharp
string name = typeof(ThemeOptions).GetNameResx("主题设置");
string group = typeof(ThemeOptions).GetGroupNameResx("显示设置");
string desc = typeof(ThemeOptions).GetDescriptionResx("主题设置设置的信息");
```

`ResxDisplayBindableBase.UpdateResx()` 会自动使用这些规则更新 `Name`、`GroupName` 和 `Description`。

### 12.2 属性名称

属性：

```csharp
[Display(Name = "语言")]
public CultureInfo CultureInfo { get; set; }
```

资源键：

| 键 | 说明 |
|---|---|
| `Property_GlobalizationOptions_CultureInfo` | 属性显示名称。 |
| `Property_GlobalizationOptions_CultureInfo_GroupName` | 属性分组名称。 |
| `Property_GlobalizationOptions_CultureInfo_TabName` | 属性 Tab 名称。 |
| `Property_GlobalizationOptions_CultureInfo_Description` | 属性说明。 |

读取方法：

```csharp
string text = typeof(GlobalizationOptions).GetPropertyNameResx(nameof(GlobalizationOptions.CultureInfo), "语言");
```

表单控件、自动列和属性项会使用这些方法读取属性显示文本。

### 12.3 枚举名称

枚举：

```csharp
public enum ProjectSaveMode
{
    [Display(Name = "退出时保存")]
    OnAppExit = 0,

    [Display(Name = "项目切换时保存")]
    OnProjectChanged
}
```

资源键：

| 键 | 说明 |
|---|---|
| `Enum_ProjectSaveMode_OnAppExit` | 枚举显示名称。 |
| `Enum_ProjectSaveMode_OnAppExit_GroupName` | 枚举分组名称。 |
| `Enum_ProjectSaveMode_OnAppExit_Description` | 枚举说明。 |
| `Enum_ProjectSaveMode_OnProjectChanged` | 枚举显示名称。 |

读取方法：

```csharp
string text = ProjectSaveMode.OnAppExit.GetEnumNameResx("退出时保存");
```

XAML 转换器：

```xaml
<TextBlock Text="{Binding ., Converter={GetEnumResxConverter}}" />
<TextBlock Text="{Binding ., Converter={GetEnumGroupNameResxConverter}}" />
<TextBlock Text="{Binding ., Converter={GetEnumDescriptionResxConverter}}" />
```

---

## 13. 入口程序集资源优先级

`ResourceManagerExtesion.GetResx(this Type type, ...)` 先查入口程序集资源，再查类型所在程序集资源：

```csharp
var entryResult = Assembly.GetEntryAssembly().GetResourceManager()?.GetString(key);
if (entryResult != null)
    return entryResult;
return type.Assembly.GetResx(key, def);
```

这意味着：

- 应用项目可以覆盖框架库或模块库中的资源键。
- 框架模块可以提供默认资源。
- 业务应用只需在入口项目的 `Resources.xx.resx` 中添加同名键即可覆盖显示文本。

示例：

模块 `H.Modules.Project` 中默认：

```text
Type_ProjectOptions = 工程配置
```

业务入口项目可以覆盖：

```text
Type_ProjectOptions = 项目管理配置
```

---

## 14. 多语言设置界面和命令

### 14.1 `IGlobalizationViewPresenter`

`AddGlobalization()` 会注册语言设置 Presenter：

```csharp
services.TryAdd(ServiceDescriptor.Singleton<IGlobalizationViewPresenter, GlobalizationViewPresenter>());
```

`GlobalizationViewPresenter` 提供选择变化命令：

```csharp
public RelayCommand SelectionChangedCommand => new RelayCommand(async x =>
{
    await GlobalizationOptions.Instance.ShowShutDownAsync();
});
```

### 14.2 `ShowGlobalizationViewCommand`

```csharp
[Icon(FontIcons.Globe)]
[Display(Name = "语言设置", Description = "显示设置语言")]
public class ShowGlobalizationViewCommand : ShowIocPresenterCommandBase<IGlobalizationViewPresenter>
{
}
```

XAML 示例：

```xaml
<FontIconButton Command="{ShowGlobalizationViewCommand}" />
```

也可以通过设置页打开：

```xaml
<Button Command="{ShowSettingCommand SwitchToType={x:Type h:GlobalizationOptions}}" />
```

---

## 15. 完整示例

### 15.1 添加资源

`Properties/Resources.resx`：

```text
AppTitle = 示例程序
Button_Save = 保存
Message_SaveSuccess = 保存成功
Type_MyOptions = 我的配置
Property_MyOptions_Name = 名称
Enum_MyMode_Default = 默认模式
```

`Properties/Resources.en-US.resx`：

```text
AppTitle = Demo App
Button_Save = Save
Message_SaveSuccess = Saved successfully
Type_MyOptions = My Options
Property_MyOptions_Name = Name
Enum_MyMode_Default = Default Mode
```

### 15.2 注册

```csharp
protected override void ConfigureServices(IServiceCollection services)
{
    services.AddGlobalization();
    services.AddSetting();
}

protected override void Configure(IApplicationBuilder app)
{
    base.Configure(app);
    app.UseGlobalizations();
    app.UseSettingViewOptions();
}
```

### 15.3 XAML 使用

```xaml
xmlns:p="clr-namespace:MyApp.Properties"

<StackPanel>
    <TextBlock Text="{GlobalizationBinding ResourcesType={x:Type p:Resources}, Key=AppTitle}" />
    <Button Content="{GlobalizationBinding ResourcesType={x:Type p:Resources}, Key=Button_Save}" />
</StackPanel>
```

### 15.4 代码使用

```csharp
IocMessage.ShowSnackInfo("保存成功".GetStringResx(this, "Message_SaveSuccess"));
```

---

## 16. 二次开发建议

- 每个模块维护自己的 `Properties/Resources.resx` 和对应语言文件。
- 入口程序集也提供对应语言资源，确保 `SupportedCultureInfos` 能扫描到语言目录。
- 可动态刷新的 XAML 文本使用 `GlobalizationBinding`。
- 不需要动态刷新或启动时固定的文本可使用 `x:Static p:Resources.Key`。
- 代码消息优先使用 `"默认文本".GetStringResx(this, "Key")`，避免资源缺失时报空。
- 类型、属性、枚举优先按框架命名约定添加资源键。
- 切换语言后建议重启应用，保证所有静态资源、命令文本和已创建控件刷新。
- 新增语言时保持所有项目语言文件命名一致，如统一使用 `en-US`、`zh-Hans`。
- 避免资源键重复表达不同含义，建议按模块或语义命名，如 `Message_SaveSuccess`、`Button_Save`。

---

## 17. 常见问题

### 语言下拉列表没有某种语言

检查：

1. 入口程序集是否存在 `Resources.xx.resx`。
2. 编译输出目录是否存在 `xx/EntryAssembly.resources.dll`。
3. 语言目录名是否是合法 `CultureInfo` 名称，如 `en-US`、`zh-Hans`。
4. 是否在启动前注册了 `AddGlobalization()`。

### 修改语言后部分界面没有变化

原因：部分文本使用 `x:Static`、静态命令或控件已在旧语言下创建。

解决：

- 使用 `GlobalizationBinding` 做动态绑定。
- 或按提示重启应用。

### `GetStringResx` 返回默认文本

检查：

1. 资源键是否存在。
2. 资源文件是否在当前对象所在程序集或入口程序集。
3. 当前 `CurrentUICulture` 是否正确。
4. `.resx` 是否生成到了输出目录。

### 类型或属性名称没有多语言

检查资源键是否符合约定：

```text
Type_{TypeName}
Type_{TypeName}_GroupName
Type_{TypeName}_Description
Property_{TypeName}_{PropertyName}
Property_{TypeName}_{PropertyName}_Description
```

### 枚举没有多语言

检查资源键是否符合约定：

```text
Enum_{EnumTypeName}_{EnumValue}
Enum_{EnumTypeName}_{EnumValue}_GroupName
Enum_{EnumTypeName}_{EnumValue}_Description
```

并确认 XAML 中使用了 `GetEnumResxConverter` 或代码中调用 `GetEnumNameResx()`。

### 发布后没有生成语言目录

检查：

1. 是否存在对应 `Resources.xx.resx`。
2. 项目文件是否包含这些 `.resx`。
3. 发布配置是否排除了卫星程序集。
4. 入口项目是否也有对应语言资源文件。
