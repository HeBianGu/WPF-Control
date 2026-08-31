**适用项目：** `H.Modules.Dependency`  
**核心类型：** `DependencyOptions`、`IDependencyItem`、`DependencyItemBase`、`DependencyItems`、`DependencyViewPresenter`  
**相关能力：** 第三方依赖信息登记、分组展示、许可证与版本信息展示、自定义依赖条目

本文介绍 `H.Modules.Dependency` 的注册、依赖项配置、内置条目、自定义条目和第三方依赖展示方式。

---

## 1. 模块定位

`H.Modules.Dependency` 用于在应用中集中展示第三方库和相关组件的信息，包括：

- 名称。
- 作者。
- 说明。
- 项目或官方网站地址。
- 当前登记的版本。
- 许可证。
- 分组。

该模块是依赖信息展示模块，不负责：

- 安装或更新 NuGet 包。
- 自动读取项目文件中的 `PackageReference`。
- 分析程序集的实际依赖关系。
- 验证许可证兼容性。
- 自动同步当前安装版本。

所有展示数据均来自 `DependencyOptions.Instance.DependencyItems`。内置依赖项中的版本和许可证也是静态登记信息，发布应用前应与实际引用版本核对。

---

## 2. 项目引用

项目引用：

```xml
<ItemGroup>
  <ProjectReference Include="..\..\Modules\H.Modules.Dependency\H.Modules.Dependency.csproj" />
</ItemGroup>
```

模块依赖框架的设置、消息、MVVM、表单和图标能力。若使用 `ShowDependenciesCommand` 打开依赖项页面，宿主还应按应用基类约定注册消息与对话框服务。

---

## 3. 注册与配置

在基于 `ApplicationBase` 的应用中注册：

```csharp
using H.Extensions.ApplicationBase;
using H.Modules.Dependency;
using Microsoft.Extensions.DependencyInjection;

public partial class App : ApplicationBase
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);
        services.AddDependency();
    }

    protected override void Configure(IApplicationBuilder app)
    {
        base.Configure(app);

        app.UseDependencyOptions(options =>
        {
            options.DependencyItems.Add(DependencyItems.WPFControl);
            options.DependencyItems.Add(DependencyItems.NewtonsoftJson);
            options.DependencyItems.Add(DependencyItems.Log4Net);
        });
    }
}
```

两个扩展方法的职责：

| 方法 | 作用 |
|---|---|
| `AddDependency()` | 注册 Options 基础设施以及 `IDependencyViewPresenter` 的默认实现。 |
| `UseDependencyOptions()` | 配置 `DependencyOptions.Instance`，并将其加入 `IocSetting`。 |

当前依赖项界面直接绑定 `DependencyOptions.Instance.DependencyItems`，因此建议在 `UseDependencyOptions()` 中添加展示项。

`AddDependency(setupAction)` 使用标准 Options 配置管道，而默认 Presenter 不读取注入的 `IOptions<DependencyOptions>`。如果目标是配置内置依赖展示界面，应优先使用 `UseDependencyOptions()`。

---

## 4. `DependencyOptions`

`DependencyOptions` 继承 `IocOptionInstance<DependencyOptions>` 并实现 `IDependencyOptions`。

核心属性：

```csharp
ObservableCollection<IDependencyItem> DependencyItems { get; set; }
```

默认集合为空。模块不会自动把 `DependencyItems` 中的全部内置条目加入集合，应用需要显式选择实际使用的依赖项。

该属性具有以下特征：

- 使用 `ObservableCollection`，运行期间增删条目会通知界面更新。
- 标记为 `JsonIgnore`，不会通过 JSON 设置序列化保存。
- 标记为只读展示项，不适合让最终用户在设置界面中直接编辑。
- `UseDependencyOptions()` 会把整个 `DependencyOptions.Instance` 加入设置系统，但依赖项集合本身不参与 JSON 持久化。

建议始终根据应用真实引用配置集合，不要为了展示完整列表而加入未实际使用的库。

---

## 5. 使用内置依赖项

`DependencyItems` 提供以下内置工厂属性：

| 属性 | 对应依赖 |
|---|---|
| `WPFControl` | WPF-Control。 |
| `OpenCvSharp4` | OpenCvSharp4。 |
| `EntityFrameworkCore` | Entity Framework Core。 |
| `NewtonsoftJson` | Newtonsoft.Json。 |
| `Log4Net` | log4net。 |
| `MvCameraControl` | MvCameraControl。 |
| `MicrosoftExtensionsDependencyInjection` | Microsoft.Extensions.DependencyInjection。 |
| `CommunityToolkitMvvm` | CommunityToolkit.Mvvm。 |
| `MicrosoftXamlBehaviorsWpf` | Microsoft.Xaml.Behaviors.Wpf。 |
| `AvalonDock` | AvalonDock。 |
| `WpfToolkit` | WPF Toolkit。 |
| `DataGridFilter` | DataGrid Filter。 |
| `PdfiumViewer` | PdfiumViewer。 |
| `QRCoder` | QRCoder。 |
| `Quartz` | Quartz。 |
| `ColorPicker` | ColorPicker。 |
| `VlcDotNet` | Vlc.DotNet。 |
| `CSCore` | CSCore。 |
| `OdysseyWpf` | Odyssey WPF。 |
| `WpfControlBase` | WPF-Control-Base。 |
| `AutoUpdaterNet` | AutoUpdater.NET。 |

完整配置示例：

```csharp
protected override void Configure(IApplicationBuilder app)
{
    base.Configure(app);

    app.UseDependencyOptions(options =>
    {
        options.DependencyItems.Add(DependencyItems.WPFControl);
        options.DependencyItems.Add(DependencyItems.MicrosoftExtensionsDependencyInjection);
        options.DependencyItems.Add(DependencyItems.CommunityToolkitMvvm);
        options.DependencyItems.Add(DependencyItems.MicrosoftXamlBehaviorsWpf);
        options.DependencyItems.Add(DependencyItems.NewtonsoftJson);
    });
}
```

每次访问 `DependencyItems.NewtonsoftJson` 等属性都会创建一个新对象。重复添加同一工厂属性会在界面中产生重复条目，模块不会自动去重。

---

## 6. `DependencyItemBase` 字段

`DependencyItemBase` 是默认依赖条目基类，实现 `IDependencyItem`，公开以下展示字段：

| 属性 | 说明 |
|---|---|
| `Name` | 依赖名称。 |
| `Author` | 作者或维护组织。 |
| `Description` | 依赖用途说明。 |
| `Uri` | 项目主页、源码仓库或产品地址。 |
| `Version` | 应用当前使用或声明的版本。 |
| `Licence` | 许可证名称。当前 API 使用 `Licence` 拼写。 |
| `GroupName` | 继承自显示模型，用于界面分组。 |

`Uri` 使用超链接属性编辑器展示。应提供完整的 `https://` 地址。

`IDependencyItem` 当前是空标记接口。默认界面依赖条目对象的可展示属性，因此自定义条目优先继承 `DependencyItemBase`，而不是仅直接实现 `IDependencyItem`。

---

## 7. 创建自定义依赖项

可以直接使用通用 `DependencyItem`：

```csharp
using H.Modules.Dependency.Items;

var dependency = new DependencyItem
{
    Name = "Contoso.Chart",
    GroupName = "图表组件",
    Author = "Contoso",
    Description = "用于业务数据可视化。",
    Uri = "https://example.com/contoso-chart",
    Version = "2.1.0",
    Licence = "MIT License"
};

DependencyOptions.Instance.DependencyItems.Add(dependency);
```

需要复用时，建议定义专用类型：

```csharp
using H.Modules.Dependency.Base;

[Display(
    Name = "Contoso.Chart",
    GroupName = "图表组件",
    Description = "用于业务数据可视化。")]
public sealed class ContosoChartDependencyItem : DependencyItemBase
{
    public ContosoChartDependencyItem()
    {
        Name = "Contoso.Chart";
        GroupName = "图表组件";
        Author = "Contoso";
        Description = "用于业务数据可视化。";
        Uri = "https://example.com/contoso-chart";
        Version = "2.1.0";
        Licence = "MIT License";
    }
}
```

注册：

```csharp
app.UseDependencyOptions(options =>
{
    options.DependencyItems.Add(new ContosoChartDependencyItem());
});
```

自定义条目建议同时设置 `DisplayAttribute.GroupName` 和实例的 `GroupName`，确保显示元数据与运行时分组信息一致。

---

## 8. 打开依赖项界面

`AddDependency()` 默认注册：

```text
IDependencyViewPresenter -> DependencyViewPresenter
```

通过 `ShowDependenciesCommand` 打开页面：

```xaml
<Button
    Command="{h:ShowDependenciesCommand}"
    Content="第三方依赖项" />
```

也可以在菜单中使用：

```xaml
<MenuItem
    Command="{h:ShowDependenciesCommand}"
    Header="第三方依赖项" />
```

该命令继承 `ShowIocCommand`，会从 IOC 中解析 `IDependencyViewPresenter` 并交给框架消息能力展示。

使用前应确保：

1. 已调用 `services.AddDependency()`。
2. 已注册应用所需的窗口消息或对话框服务。
3. 已调用 `app.UseDependencyOptions()` 并添加依赖条目。

消息服务的注册方式见 [`development-guide-message.md`](development-guide-message.md)。

---

## 9. 界面分组和展示行为

默认 `DependencyViewPresenter` 的 DataTemplate：

- 绑定 `DependencyOptions.Instance.DependencyItems`。
- 使用 `CollectionViewSource` 创建集合视图。
- 按每个条目的 `GroupName` 分组。
- 使用 `GroupBox` 显示分组名称。
- 使用 `StaticForm` 展示条目属性。
- 使用滚动区域承载完整依赖列表。

因此，条目应设置非空且稳定的 `GroupName`。没有分组名称的条目可能集中显示在空分组中。

Presenter 还会根据继承显示模型的 `IsVisible` 和 `IsAuthority` 控制可见性。自定义 Presenter 或依赖条目时应保留应用现有的权限和可见性约定。

---

## 10. 替换默认 Presenter

`AddDependency()` 使用 `TryAdd` 注册默认 Presenter，因此应用可以在调用前注册自定义实现：

```csharp
protected override void ConfigureServices(IServiceCollection services)
{
    base.ConfigureServices(services);

    services.AddSingleton<IDependencyViewPresenter,
        MyDependencyViewPresenter>();
    services.AddDependency();
}
```

自定义 Presenter 可用于：

- 增加搜索和筛选。
- 根据产品版本显示不同依赖集合。
- 增加许可证全文入口。
- 从应用自己的清单生成依赖信息。
- 调整布局和品牌样式。

如果自定义 Presenter 仍由 `ShowDependenciesCommand` 打开，必须继续实现 `IDependencyViewPresenter`。

---

## 11. 推荐配置方式

建议在一个集中位置维护产品依赖清单：

```csharp
private static void ConfigureDependencies(IDependencyOptions options)
{
    options.DependencyItems.Clear();

    options.DependencyItems.Add(DependencyItems.WPFControl);
    options.DependencyItems.Add(DependencyItems.MicrosoftExtensionsDependencyInjection);
    options.DependencyItems.Add(DependencyItems.NewtonsoftJson);
    options.DependencyItems.Add(new ContosoChartDependencyItem());
}

protected override void Configure(IApplicationBuilder app)
{
    base.Configure(app);
    app.UseDependencyOptions(ConfigureDependencies);
}
```

使用 `Clear()` 可以避免同一配置流程被重复调用时不断累加条目，但应用应首先确认没有其他模块需要向该集合追加内容。

发布前建议：

1. 根据项目文件和锁定文件核对实际包版本。
2. 根据上游项目官方网站核对许可证名称和链接。
3. 删除应用未实际使用的内置条目。
4. 检查依赖项链接是否仍然有效。
5. 对商业组件或双许可证组件补充准确说明。

---

## 12. 常见问题

### 打开页面后没有任何依赖项

`DependencyOptions.DependencyItems` 默认是空集合。检查是否在 `UseDependencyOptions()` 中显式添加了条目：

```csharp
app.UseDependencyOptions(options =>
{
    options.DependencyItems.Add(DependencyItems.WPFControl);
});
```

### `AddDependency()` 中配置了条目但界面仍为空

当前默认界面绑定 `DependencyOptions.Instance`，而 `AddDependency(setupAction)` 配置的是标准 Options 管道。将界面条目配置移动到 `UseDependencyOptions()`。

### 点击命令没有显示页面

检查：

1. 是否调用 `services.AddDependency()`。
2. `IDependencyViewPresenter` 是否能从 IOC 中解析。
3. 消息和对话框服务是否已注册。
4. XAML 是否使用了实际命令名 `ShowDependenciesCommand`。

### 条目没有按预期分组

确认实例的 `GroupName` 已赋值。只给自定义类添加 `DisplayAttribute.GroupName` 而未设置运行时 `GroupName` 时，应检查显示基类是否已正确读取元数据；显式设置实例属性更可靠。

### 版本与项目实际引用不一致

内置条目不会自动读取 NuGet 版本。应更新自定义条目，或替换、修改加入集合的条目实例：

```csharp
var item = DependencyItems.NewtonsoftJson as DependencyItemBase;
item.Version = "实际使用版本";
options.DependencyItems.Add(item);
```

### 同一依赖出现多次

`DependencyItems` 工厂每次返回新实例，集合也不执行去重。检查配置方法是否重复调用，或在添加前按名称判断：

```csharp
if (!options.DependencyItems
    .OfType<DependencyItemBase>()
    .Any(x => x.Name == "Newtonsoft.Json"))
{
    options.DependencyItems.Add(DependencyItems.NewtonsoftJson);
}
```
