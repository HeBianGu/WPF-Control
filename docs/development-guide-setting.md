# Setting 设置系统二次开发文档
# Setting 设置系统二次开发文档

**适用项目：** `H.Services.Setting`、`H.Extensions.Setting`、`H.Modules.Setting`  
**核心类型：** `ISettingDataService`、`IocSetting`、`ISettable`、`SettableBase`、`IocOptionInstance<T>`、`SettingViewPresenter`  
**相关能力：** 设置项注册、配置持久化、设置页展示、属性特性驱动表单编辑

本文介绍 WPF-Control 的设置系统，重点说明 `Setting` 模块的组成、`Option` 配置对象的创建和注册方式，以及如何通过属性特性控制设置页中的属性显示、分组、编辑器和序列化行为。

---

## 1. Setting 系统定位

Setting 系统用于统一管理应用配置项，主要负责：

- 收集各模块的设置对象。
- 在启动页加载配置文件。
- 在设置页中按分组展示配置项。
- 保存配置到本地 JSON 文件。
- 支持恢复默认、取消修改、清空设置。
- 支持登录前配置和登录后用户配置。
- 通过属性特性自动生成表单编辑界面。

相关项目职责：

| 项目 | 说明 |
|---|---|
| `H.Services.Setting` | 定义设置服务接口、设置项接口、分组名称和静态访问入口。 |
| `H.Extensions.Setting` | 提供 `SettableBase`、`Settable<T>`、`IocOptionInstance<T>` 等设置项基类。 |
| `H.Modules.Setting` | 提供设置页 UI、设置数据服务、设置命令和默认系统设置项。 |
| `H.Controls.Form` | 根据属性和特性生成设置项编辑界面。 |
| `H.Controls.Form.PropertyItem` | 提供更多属性编辑器，如密码、文件选择、颜色、集合等。 |

---

## 2. 核心接口和类

### 2.1 `ISettable`

`ISettable` 是所有设置对象的基础接口：

```csharp
public interface ISettable : INameable, IOrderable, IGroupable
{
    bool IsVisibleInSetting { get; set; }
}
```

含义：

- `Name`：设置项显示名称。
- `GroupName`：设置项所在分组。
- `Order`：排序值。
- `IsVisibleInSetting`：是否显示在设置页。

### 2.2 `ISettingDataService`

设置数据服务接口：

```csharp
public interface ISettingDataService : ISettingDataOption
{
    ObservableCollection<ISettable> Settings { get; set; }
    void Cancel();
    bool Load(Action<ISettable> action, out string message);
    bool LoadLoginedLoad(Action<ISettable> action, out string message);
    void Remove(params ISettable[] settings);
    bool Save(out string message);
    void SetDefault();
    void Clear();
}
```

主要职责：

- `Settings`：保存所有注册的设置对象。
- `Add` / `Remove`：添加或移除设置对象。
- `Load`：启动时加载所有实现 `ILoadable` 的设置项。
- `LoadLoginedLoad`：登录后加载实现 `ILoginedSplashLoadable` 的设置项。
- `Save`：保存所有实现 `ISaveable` 的设置项。
- `SetDefault`：调用所有实现 `IDefaultable` 的设置项恢复默认值。
- `Clear`：调用所有实现 `IClearable` 的设置项清理配置。
- `Cancel`：重新加载配置，相当于撤销未保存修改。

### 2.3 `IocSetting`

`IocSetting` 是设置服务的静态访问入口：

```csharp
public class IocSetting : Ioc<ISettingDataService>
{
}
```

使用方式：

```csharp
IocSetting.Instance.Add(MyOptions.Instance);
IocSetting.Instance.Save(out string message);
IocSetting.Instance.Load(null, out string message);
```

---

## 3. 设置项基类

### 3.1 `SettableBase`

`SettableBase` 是设置对象最常用的基类：

```csharp
public abstract class SettableBase : ResxDisplayBindableBase, ISettable, ILoadable,
    ISaveable, IDefaultable, IClearable, IDefaultTemplateable
{
}
```

它实现了：

- `ISettable`：可被设置系统收集和展示。
- `ILoadable`：支持从配置文件加载。
- `ISaveable`：支持保存到配置文件。
- `IDefaultable`：支持恢复默认值。
- `IClearable`：支持清理配置文件。
- `IDefaultTemplateable`：支持从默认模板初始化配置。

### 3.2 配置文件路径规则

`SettableBase` 默认配置文件名：

```text
{TypeName}.json
```

路径规则：

```csharp
protected virtual string GetDefaultFolder()
{
    if (this is ILoginedSplashLoadable)
        return AppPaths.Instance.UserSetting;
    return AppPaths.Instance.Setting;
}
```

含义：

- 普通设置项保存在 `AppPaths.Instance.Setting`。
- 实现 `ILoginedSplashLoadable` 的设置项保存在 `AppPaths.Instance.UserSetting`。

示例：

```text
{AppPath}\Default\Setting\LoginOptions.json
{AppPath}\admin\Setting\UserOptions.json
```

### 3.3 保存和加载

保存：

```csharp
public virtual bool Save(out string message)
{
    string path = this.GetDefaultPath();
    string folder = Path.GetDirectoryName(path);
    if (!Directory.Exists(folder))
        Directory.CreateDirectory(folder);
    this.GetSerializerService()?.Save(path, this);
    return true;
}
```

加载：

```csharp
public virtual bool Load(out string message)
{
    var path = this.GetDefaultPath();
    if (!this.HasFile())
    {
        message = "文件不存在:" + path;
        this.UpdateResx();
        return true;
    }
    this.Load(path);
    this.UpdateResx();
    return true;
}
```

默认序列化服务：

```csharp
protected virtual ISerializerService GetSerializerService()
{
    return new TextJsonSerializerService();
}
```

如果需要 XML 或自定义序列化，可重写 `GetSerializerService()`。

### 3.4 `Settable<T>`

`Settable<T>` 提供懒加载静态实例：

```csharp
public abstract class Settable<T> : LazySettableInstance<T> where T : new()
{
}
```

适合不需要 `Microsoft.Extensions.Options` 的普通设置对象。

### 3.5 `IocOptionInstance<T>`

`IocOptionInstance<T>` 是模块 `Options` 最常用基类：

```csharp
public abstract class IocOptionInstance<Setting> : SettableBase, IOptions<Setting>
    where Setting : class, new()
{
    public static Setting Instance => Ioc.GetService<IOptions<Setting>>().Value;
}
```

特点：

- 同时是设置项和 `IOptions<T>` 配置对象。
- 可通过 `Options` 模式在 `ConfigureServices` 中配置。
- 可通过 `T.Instance` 静态访问当前配置。
- 可被 `IocSetting.Instance.Add(...)` 加入设置页。

适用场景：

- 模块参数配置。
- 设置页可编辑配置。
- 需要 `services.Configure<TOptions>(...)` 的配置对象。

---

## 4. 注册 Setting 模块

`H.Modules.Setting` 提供扩展方法。

### 4.1 注册设置服务和设置页

```csharp
protected override void ConfigureServices(IServiceCollection services)
{
    services.AddSetting();
}
```

`AddSetting()` 会：

- 注册 `ISettingDataService` 默认实现 `SettingDataService`。
- 注册 `ISettingViewPresenter` 默认实现 `SettingViewPresenter`。
- 支持配置 `ISettingViewOptions`。

等价核心逻辑：

```csharp
services.AddOptions();
services.TryAdd(ServiceDescriptor.Singleton<ISettingDataService, SettingDataService>());
services.TryAdd(ServiceDescriptor.Singleton<ISettingViewPresenter, SettingViewPresenter>());
```

### 4.2 配置设置页参数

```csharp
services.AddSetting(option =>
{
    option.Width = 1000;
    option.Height = 700;
    option.MinWidth = 600;
    option.MinHeight = 400;
    option.UseSetDefault = true;
});
```

### 4.3 在 `Configure` 中添加设置页配置项

```csharp
protected override void Configure(IApplicationBuilder app)
{
    base.Configure(app);
    app.UseSettingViewOptions();
    app.UseSettingSecurityOptions();
}
```

常用方法：

| 方法 | 说明 |
|---|---|
| `UseSettingDataOptions` | 直接操作 `IocSetting.Instance` 添加设置项。 |
| `UseSettingViewOptions` | 将 `SettingViewOptions.Instance` 加入设置系统。 |
| `UseSettingSecurityOptions` | 将设置页密码控制配置加入设置系统。 |
| `UseSettingDefaultOptions` | 添加默认系统设置项集合。 |

---

## 5. 设置页展示流程

`SettingViewPresenter` 从 `IocSetting.Instance.Settings` 读取设置项：

```csharp
this.Groups = IocSetting.Instance.Settings?
    .Where(x => x.IsVisibleInSetting)
    .GroupBy(l => l.GroupName)
    .Select(x => new SettableGroup()
    {
        Name = x.Key,
        Collection = x.ToObservable()
    })
    .ToObservable();
```

设置页展示规则：

1. 只显示 `IsVisibleInSetting == true` 的设置项。
2. 按 `GroupName` 分组。
3. 每组内部显示对应 `ISettable` 对象。
4. 选中设置项后使用 `H.Controls.Form` 根据属性生成表单。

打开设置页常用命令：

```xaml
<Button Command="{ShowSettingCommand}" Content="设置" />
```

切换到指定设置项：

```xaml
<Button Command="{ShowSettingCommand SwitchToType={x:Type h:IThemeOptions}}" />
```

代码方式：

```csharp
await SettingViewPresenter.Instance.Show(typeof(MyOptions));
```

---

## 6. 创建自定义 Option

### 6.1 定义接口

```csharp
public interface IMyFeatureOptions
{
    bool UseFeature { get; set; }
    string ServiceUrl { get; set; }
    int TimeoutSeconds { get; set; }
}
```

### 6.2 定义 Option 类

```csharp
using H.Common.Attributes;
using H.Extensions.Setting;
using H.Services.Setting;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

[Icon("\xE713")]
[Display(Name = "我的功能", GroupName = SettingGroupNames.GroupApp, Description = "配置我的功能参数", Order = 10)]
public class MyFeatureOptions : IocOptionInstance<MyFeatureOptions>, IMyFeatureOptions
{
    private bool _useFeature = true;

    [DefaultValue(true)]
    [Display(Name = "启用功能", Description = "是否启用我的功能", Order = 1)]
    public bool UseFeature
    {
        get => _useFeature;
        set
        {
            _useFeature = value;
            RaisePropertyChanged();
        }
    }

    private string _serviceUrl = "https://localhost";

    [DefaultValue("https://localhost")]
    [Display(Name = "服务地址", Description = "接口服务地址", Order = 2)]
    public string ServiceUrl
    {
        get => _serviceUrl;
        set
        {
            _serviceUrl = value;
            RaisePropertyChanged();
        }
    }

    private int _timeoutSeconds = 30;

    [DefaultValue(30)]
    [Range(1, 300)]
    [Display(Name = "超时时间", Description = "请求超时时间，单位秒", Order = 3)]
    public int TimeoutSeconds
    {
        get => _timeoutSeconds;
        set
        {
            _timeoutSeconds = value;
            RaisePropertyChanged();
        }
    }
}
```

### 6.3 注册 Option

```csharp
public static class MyFeatureExtension
{
    public static IServiceCollection AddMyFeature(this IServiceCollection services, Action<IMyFeatureOptions> setupAction = null)
    {
        services.AddOptions();
        services.TryAdd(ServiceDescriptor.Singleton<IMyFeatureService, MyFeatureService>());
        if (setupAction != null)
            services.Configure(new Action<MyFeatureOptions>(setupAction));
        return services;
    }

    public static IApplicationBuilder UseMyFeatureOptions(this IApplicationBuilder builder, Action<IMyFeatureOptions> option = null)
    {
        IocSetting.Instance.Add(MyFeatureOptions.Instance);
        option?.Invoke(MyFeatureOptions.Instance);
        return builder;
    }
}
```

### 6.4 在应用中使用

```csharp
protected override void ConfigureServices(IServiceCollection services)
{
    services.AddSetting();
    services.AddMyFeature(option =>
    {
        option.UseFeature = true;
        option.TimeoutSeconds = 60;
    });
}

protected override void Configure(IApplicationBuilder app)
{
    base.Configure(app);
    app.UseSettingViewOptions();
    app.UseMyFeatureOptions(option =>
    {
        option.ServiceUrl = "https://api.example.com";
    });
}
```

---

## 7. 属性特性配置

设置页属性编辑主要依赖 .NET 标准特性和 `H.Controls.Form` 扩展特性。

### 7.1 设置项类上的特性

```csharp
[Icon(FontIcons.Connect)]
[Display(Name = "登录页面设置", GroupName = SettingGroupNames.GroupSystem, Description = "登录页面设置的信息", Order = 10)]
public class LoginOptions : IocOptionInstance<LoginOptions>, ILoginOptions
{
}
```

常用类级特性：

| 特性 | 作用 |
|---|---|
| `[Display(Name = ...)]` | 设置项显示名称。 |
| `[Display(GroupName = ...)]` | 设置项所在分组。 |
| `[Display(Description = ...)]` | 设置项说明。 |
| `[Display(Order = ...)]` | 设置项排序。 |
| `[Icon(...)]` | 设置项图标。 |

建议使用 `SettingGroupNames` 常量作为分组名：

```csharp
SettingGroupNames.GroupBase
SettingGroupNames.GroupApp
SettingGroupNames.GroupStyle
SettingGroupNames.GroupSecurity
SettingGroupNames.GroupAuthority
SettingGroupNames.GroupSystem
SettingGroupNames.GroupControl
SettingGroupNames.GroupMessage
SettingGroupNames.GroupData
SettingGroupNames.GroupOther
```

### 7.2 属性上的显示特性

```csharp
[Display(Name = "登录标题", Description = "显示在登录页顶部的产品标题", GroupName = "基础", Order = 1)]
public string Product { get; set; }
```

| 特性属性 | 说明 |
|---|---|
| `Name` | 属性显示标题。 |
| `Description` | 属性说明，通常用于提示或 ToolTip。 |
| `GroupName` | 属性在表单中的内部分组。 |
| `Order` | 属性排序。 |
| `Prompt` | 提示信息，部分编辑器可使用。 |

### 7.3 默认值 `[DefaultValue]`

```csharp
[DefaultValue(true)]
[Display(Name = "记住密码")]
public bool Remember { get; set; } = true;
```

`LoadDefault()` 会结合默认值相关逻辑恢复默认配置。建议每个可配置属性都显式标注默认值。

复杂类型默认值示例：

```csharp
[DefaultValue(typeof(Thickness), "20,20,20,20")]
[Display(Name = "页面边距")]
public Thickness Margin { get; set; } = new Thickness(20);
```

### 7.4 只读 `[ReadOnly]`

```csharp
[ReadOnly(true)]
[Display(Name = "产品名称")]
public string ProductName { get; set; }
```

用于设置页展示但不允许编辑的属性。

### 7.5 隐藏属性 `[Browsable(false)]`

```csharp
[Browsable(false)]
[JsonIgnore]
[XmlIgnore]
public IFormOption FormOption { get; internal set; }
```

用途：

- 不在设置页显示。
- 不参与属性表单生成。
- 常配合 `JsonIgnore`、`XmlIgnore` 使用。

### 7.6 序列化忽略

```csharp
[System.Text.Json.Serialization.JsonIgnore]
[System.Xml.Serialization.XmlIgnore]
[Browsable(false)]
public string RuntimeValue { get; set; }
```

说明：

- `[JsonIgnore]`：不保存到 JSON。
- `[XmlIgnore]`：`SettableBase.Load(ISettable)` 中会跳过带 `XmlIgnoreAttribute` 的属性。
- `[Browsable(false)]`：不显示到设置页。

如果属性只是运行时状态，建议三个特性一起使用。

### 7.7 校验特性

常用 `System.ComponentModel.DataAnnotations`：

```csharp
[Required]
[Display(Name = "管理员账号")]
public string AdminName { get; set; }

[Range(1, 300)]
[Display(Name = "超时时间")]
public int TimeoutSeconds { get; set; }
```

常用校验：

- `[Required]`
- `[Range(min, max)]`
- `[StringLength(max)]`
- `[RegularExpression(...)]`

### 7.8 类型转换 `[TypeConverter]`

用于让设置页或序列化系统正确编辑特殊类型：

```csharp
[TypeConverter(typeof(LengthConverter))]
[DefaultValue(double.NaN)]
[Display(Name = "页面宽度")]
public double Width { get; set; } = double.NaN;
```

---

## 8. `H.Controls.Form` 扩展特性

设置页通常通过 `H.Controls.Form` 自动生成表单。可以使用扩展特性控制编辑器和交互。

### 8.1 指定编辑器 `[PropertyItem]`

```csharp
using H.Controls.Form.Attributes;
using H.Controls.Form.PropertyItem.TextPropertyItems;

[PropertyItem(typeof(PasswordTextPropertyItem))]
[DefaultValue("123456")]
[Display(Name = "设置管理员密码")]
public string Password { get; set; }
```

常见编辑器类型：

| 编辑器 | 说明 |
|---|---|
| `PasswordTextPropertyItem` | 密码输入框。 |
| `OpenFileDialogPropertyItem` | 文件选择。 |
| `OpenSystemPathTextPropertyItem` | 打开系统路径。 |
| `SliderTextPropertyItem` | 滑块数值。 |
| `UnitTextPropertyItem` | 带单位文本。 |
| `ColorPropertyItem` | 颜色选择。 |
| `BrushPropertyItem` | 画刷选择。 |
| `EnumComboBoxPropertyItem` | 枚举下拉。 |
| `PrimitiveListPropertyItem` | 基础类型列表。 |

### 8.2 单位 `[Unit]`

```csharp
[Unit("秒")]
[Display(Name = "超时时间")]
public int TimeoutSeconds { get; set; }
```

适合显示时间、长度、大小等单位。

### 8.3 标签页 `[Tab]`

```csharp
[Tab("高级")]
[Display(Name = "高级参数")]
public string AdvancedValue { get; set; }
```

用于表单内部按标签页组织属性。

### 8.4 属性可见性绑定

按属性控制可见性：

```csharp
[Display(Name = "启用邮箱注册")]
public bool UseMail { get; set; }

[BindingVisibleablePropertyName(nameof(UseMail))]
[Display(Name = "发送验证码邮箱")]
public string MailAccount { get; set; }
```

按方法控制可见性：

```csharp
[BindingVisiblableMethodName(nameof(CanShowAdvanced))]
[Display(Name = "高级配置")]
public string Advanced { get; set; }

public bool CanShowAdvanced()
{
    return UseAdvanced;
}
```

### 8.5 属性变化通知方法

```csharp
[NotifyMethodName(nameof(OnServiceUrlChanged))]
[Display(Name = "服务地址")]
public string ServiceUrl { get; set; }

public void OnServiceUrlChanged()
{
    // 属性修改后执行刷新逻辑。
}
```

### 8.6 属性变化后刷新其他数据源

```csharp
[RefreshSourceOnValueChanged(nameof(Items))]
[Display(Name = "类型")]
public string Type { get; set; }

public IEnumerable<string> Items { get; set; }
```

用于属性变化后刷新下拉数据、过滤条件或关联属性。

---

## 9. 登录后用户配置

如果某个设置项需要按登录用户隔离，可以实现 `ILoginedSplashLoadable`。

```csharp
public class UserEditorOptions : IocOptionInstance<UserEditorOptions>, ILoginedSplashLoadable
{
    public string Name => "用户编辑器设置";

    private string _theme;

    [DefaultValue("Default")]
    [Display(Name = "编辑器主题")]
    public string Theme
    {
        get => _theme;
        set
        {
            _theme = value;
            RaisePropertyChanged();
        }
    }

    public override bool Load(out string message)
    {
        return base.Load(out message);
    }
}
```

路径会从应用级 `Setting` 切换到用户级 `UserSetting`：

```csharp
if (this is ILoginedSplashLoadable)
    return AppPaths.Instance.UserSetting;
```

注册时仍然加入设置系统：

```csharp
app.UseSettingDataOptions(x =>
{
    x.Add(UserEditorOptions.Instance);
});
```

---

## 10. 默认模板配置

`SettableBase` 支持从默认模板初始化配置：

```csharp
public virtual (bool success, string message) LoadDefaultTemplate()
{
    var path = this.GetDefaultPath();
    if (File.Exists(path))
        return (false, "已存在配置文件:" + path);

    var defPath = this.GetDefaultTemplatePath();
    if (!File.Exists(defPath))
        return (false, "默认模板文件文件不存在:" + defPath);

    File.Copy(defPath, this.GetDefaultPath());
    return (true, defPath);
}
```

默认模板路径：

```text
Assets\DefaultTemplates\Setting\{OptionTypeName}.json
```

适用场景：

- 首次启动提供默认配置文件。
- 产品预置配置模板。
- 多环境默认参数初始化。

---

## 11. 设置页命令

### 11.1 保存设置

`SumitSettingDataCommand`：

```csharp
IocSetting.Instance.Save(out string message);
```

保存成功后提交对话框。

### 11.2 取消设置

`CancelSettingDataCommand`：

```csharp
IocSetting.Instance.Load(null, out string message);
```

取消时重新加载配置文件，撤销未保存修改。

### 11.3 恢复默认

`SettingDefaultCommand` 会：

1. 调用 `IocSetting.Instance.SetDefault()`。
2. 调用 `IocSetting.Instance.Clear()`。
3. 重新执行应用配置 `configureable.Configure()`。
4. 重新加载设置。
5. 刷新设置页分组。

---

## 12. 完整示例

```csharp
public interface IReportOptions
{
    bool UseAutoSave { get; set; }
    string OutputFolder { get; set; }
    int MaxCount { get; set; }
}

[Icon("\xE8A5")]
[Display(Name = "报表设置", GroupName = SettingGroupNames.GroupData, Description = "配置报表导出参数", Order = 20)]
public class ReportOptions : IocOptionInstance<ReportOptions>, IReportOptions
{
    private bool _useAutoSave = true;

    [DefaultValue(true)]
    [Display(Name = "自动保存", Description = "导出后自动保存报表", Order = 1)]
    public bool UseAutoSave
    {
        get => _useAutoSave;
        set
        {
            _useAutoSave = value;
            RaisePropertyChanged();
        }
    }

    private string _outputFolder;

    [PropertyItem(typeof(OpenSystemPathTextPropertyItem))]
    [Display(Name = "输出目录", Description = "报表导出目录", Order = 2)]
    public string OutputFolder
    {
        get => _outputFolder;
        set
        {
            _outputFolder = value;
            RaisePropertyChanged();
        }
    }

    private int _maxCount = 100;

    [DefaultValue(100)]
    [Range(1, 10000)]
    [Display(Name = "最大条数", Description = "单次导出的最大记录数", Order = 3)]
    public int MaxCount
    {
        get => _maxCount;
        set
        {
            _maxCount = value;
            RaisePropertyChanged();
        }
    }
}
```

注册扩展：

```csharp
public static class ReportExtension
{
    public static IServiceCollection AddReport(this IServiceCollection services, Action<IReportOptions> setupAction = null)
    {
        services.AddOptions();
        if (setupAction != null)
            services.Configure(new Action<ReportOptions>(setupAction));
        return services;
    }

    public static IApplicationBuilder UseReportOptions(this IApplicationBuilder builder, Action<IReportOptions> option = null)
    {
        IocSetting.Instance.Add(ReportOptions.Instance);
        option?.Invoke(ReportOptions.Instance);
        return builder;
    }
}
```

应用配置：

```csharp
protected override void ConfigureServices(IServiceCollection services)
{
    services.AddSetting();
    services.AddReport(x =>
    {
        x.UseAutoSave = true;
        x.MaxCount = 500;
    });
}

protected override void Configure(IApplicationBuilder app)
{
    base.Configure(app);
    app.UseSettingViewOptions();
    app.UseReportOptions(x =>
    {
        x.OutputFolder = AppPaths.Instance.UserData;
    });
}
```

---

## 13. 二次开发建议

- 模块配置优先继承 `IocOptionInstance<T>`。
- 普通单例设置可继承 `Settable<T>`。
- 设置项类上必须配置 `[Display]`，建议同时配置 `[Icon]`。
- 设置项分组优先使用 `SettingGroupNames`。
- 可编辑属性建议配置 `[Display]` 和 `[DefaultValue]`。
- 运行时属性使用 `[Browsable(false)]`、`[JsonIgnore]`、`[XmlIgnore]` 隐藏和忽略保存。
- 密码、文件路径、颜色、集合等特殊属性使用 `[PropertyItem]` 指定编辑器。
- 用户私有配置实现 `ILoginedSplashLoadable`，并在登录后读取。
- 写入外部文件前确认目录存在。
- 不要在 `ConfigureServices` 之前访问 `Option.Instance`。

---

## 14. 常见问题

### 设置页中看不到配置项

检查：

1. 是否调用 `services.AddSetting()`。
2. 是否调用 `IocSetting.Instance.Add(MyOptions.Instance)` 或对应 `UseXxxOptions()`。
3. `IsVisibleInSetting` 是否为 `true`。
4. 类上是否正确设置 `Display(GroupName = ...)`。

### `Option.Instance` 获取失败

`IocOptionInstance<T>.Instance` 依赖 `IOptions<T>`，需要：

```csharp
services.AddOptions();
services.Configure<MyOptions>(...);
```

通常通过模块的 `AddXxx` 扩展方法完成。

### 配置没有保存

检查：

1. 设置对象是否继承 `SettableBase` 或实现 `ISaveable`。
2. 是否调用了 `IocSetting.Instance.Save(out message)`。
3. 属性是否被 `[JsonIgnore]` 或 `[XmlIgnore]` 忽略。
4. 配置目录是否有写入权限。

### 点击取消后数据没有恢复

取消命令会重新调用 `IocSetting.Instance.Load(...)`。如果属性未被序列化保存，或 `Load(ISettable)` 中被 `XmlIgnore` 跳过，则不会恢复。

### 恢复默认后设置项丢失

`SettingDefaultCommand` 会 `Clear()` 后重新执行 `Configure()`。确保自定义设置项是在 `Configure(IApplicationBuilder app)` 的 `UseXxxOptions()` 中加入设置系统，而不是只在临时位置添加。

### 属性编辑器不符合预期

使用 `[PropertyItem(typeof(...))]` 指定编辑器，并确认对应属性项类型所在项目已被引用，例如 `H.Controls.Form.PropertyItem`。
