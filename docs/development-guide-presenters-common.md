# H.Presenters.Common 开发文档

**适用项目：** `H.Presenters.Common`  
**核心类型：** `DisplayBindableBase`、`ItemsSourcePresenterBase`、`IDialogMessageService`、`DialogServiceExtension`  
**相关能力：** 通用数据展示、文本和密码输入、列表与树查看、图片预览、文件路径选择、命令集合、设置 Presenter

本文介绍 `H.Presenters.Common` 的通用 Presenter、隐式 `DataTemplate`、对话框扩展方法和标记命令。该项目用于快速组合常见 WPF 交互，不直接承担领域业务规则。

---

## 1. 项目定位

`H.Presenters.Common` 提供一组可复用的 Presenter 和其 XAML 模板，解决常见的显示与输入需求：

```text
业务 Service / ViewModel
        ↓
通用 Presenter
        ↓
IDialogMessageService.ShowDialog(...)
        ↓
Presenter 对应的 DataTemplate
        ↓
WPF 对话框或 ContentControl
```

适用场景：

- 显示集合、数据表和树。
- 输入单行文本或密码。
- 预览图片和选择图片路径。
- 显示进度、等待和消息。
- 以命令形式打开常用对话框。
- 将对象或命令集合以统一模板呈现。

不适合：

- 复杂业务流程编排。
- 领域对象持久化。
- 登录、权限或敏感凭据处理。
- 具有专属交互规范的大型业务页面。

这些场景应创建模块专属 Presenter 和 Service。

---

## 2. 项目结构

```text
H.Presenters.Common/
├── Presenters/
│   ├── *.xaml.cs             Presenter、接口、对话框扩展
│   └── *.xaml                对应隐式 DataTemplate
├── Themes/Generic.xaml       合并所有 Presenter 模板
├── ShowListBoxCommand.cs     列表展示标记命令
└── H.Presenters.Common.csproj
```

每个 Presenter 通常包含：

```text
Presenter 类
    ↓
可选公开接口
    ↓
DialogServiceExtension 扩展方法
    ↓
同名 XAML 中的 DataTemplate
```

例如：

```text
TextBoxPresenter.xaml.cs
TextBoxPresenter.xaml
ShowTextBox(...)
```

---

## 3. DataTemplate 工作方式

Presenter 本身通常不创建 Window 或 UserControl，而是通过隐式模板呈现：

```xaml
<DataTemplate DataType="{x:Type local:TextBoxPresenter}">
    <!-- 文本输入 UI -->
</DataTemplate>
```

对话框服务解析 Presenter 后，WPF 根据实际类型查找 `DataTemplate`：

```csharp
await IocMessage.Dialog.ShowDialog<TextBoxPresenter>(...);
```

因此，通用 Presenter 可同时用于：

```xaml
<!-- 嵌入页面 -->
<ContentControl Content="{Binding MyPresenter}" />
```

```csharp
// 对话框显示
await IocMessage.Dialog.ShowDialog<MyPresenter>(...);
```

### 3.1 模板找不到时

检查：

1. 是否引用 `H.Presenters.Common`。
2. 程序集主题资源 `Themes/Generic.xaml` 是否能被 WPF 发现。
3. 实际 Presenter 类型是否与 `DataTemplate.DataType` 相同。
4. 应用资源是否覆盖了同类型的模板。
5. 是否在运行时创建了未提供模板的派生 Presenter。

---

## 4. 使用前提

通用 Presenter 的对话框扩展依赖消息和对话框服务。应用一般需要注册窗口消息和对话框实现：

```csharp
protected override void ConfigureServices(IServiceCollection services)
{
    services.AddWindowMessage();
    services.AddAdornerDialogMessage();
}
```

具体组合根据应用窗口和消息模块选择。调用前可检查：

```csharp
if (IocMessage.Dialog != null)
{
    // 显示对话框。
}
```

业务代码通常调用：

```csharp
await IocMessage.Dialog.ShowTextBox(...);
```

`ShowDialog` 返回：

```csharp
Task<bool?>
```

约定：

| 返回值 | 含义 |
|---|---|
| `true` | 用户提交且提交校验通过。 |
| `false` | 用户取消、拒绝或提交流程未完成。 |
| `null` | 对话框没有明确结果或由无按钮模式关闭。 |

不要只依赖返回值；需要输入内容时应在提交回调中处理 Presenter 状态。

---

## 5. Presenter 分类

| 分类 | 类型 | 主要用途 |
|---|---|---|
| 文本输入 | `TextBoxPresenter`、`PasswordBoxPresenter`、`StringPresenter` | 输入文字、密码和字符串显示。 |
| 数据显示 | `DataGridPresenter`、`DataGridTypePresenter`、`ItemsControlPresenter`、`ListBoxPresenter` | 显示集合和表格。 |
| 树展示 | `TreeViewPresenter`、`ExploreTreePresenter` | 显示分层数据和资源树。 |
| 图片与文件 | `ImageViewPresenter`、`ImageFilePathPresenter`、`AddImageFilePathPresenter`、`AddImageListPresenter`、`FilePathTextBoxPresenter`、`FileInfoPresenter` | 图片预览、路径输入和文件信息。 |
| 命令 | `CommandsPresenter`、`CommandsBindablePresenter` | 呈现命令集合或可绑定命令。 |
| 状态与消息 | `MessagePresenter`、`WaitPresenter`、`PercentPresenter` | 消息、等待和百分比进度。 |
| 设置 | `MetaSettingPresenter<T>`、`MetaSettingsPresenter<T>` | 元数据设置和设置集合。 |

---

## 6. 文本输入 `TextBoxPresenter`

`TextBoxPresenter` 用于单行或普通文本输入。

接口：

```csharp
public interface ITextBoxPresenter
{
    string Text { get; set; }
}
```

基本调用：

```csharp
await IocMessage.Dialog.ShowTextBox(
    text: "默认名称",
    sumitAction: value =>
    {
        SaveName(value);
    },
    builder: dialog =>
    {
        dialog.Title = "输入名称";
    });
```

完整形式：

```csharp
await IocMessage.Dialog.ShowTextBox(
    option: presenter =>
    {
        presenter.Text = "默认名称";
    },
    sumitAction: presenter =>
    {
        SaveName(presenter.Text);
    },
    builder: dialog =>
    {
        dialog.Title = "输入名称";
        dialog.MinWidth = 320;
    },
    canSumit: presenter =>
    {
        bool valid = !string.IsNullOrWhiteSpace(presenter.Text);
        return Task.FromResult(valid);
    });
```

`canSumit` 返回 `false` 时，对话框不会提交。复杂校验应在领域 Service 中再次执行。

### 6.1 输入指定类型

框架提供泛型输入扩展，将文本转换为目标类型：

```csharp
await IocMessage.Dialog.ShowTextBox<int>(
    text: "10",
    sumitAction: count =>
    {
        SaveCount(count);
    },
    builder: dialog => dialog.Title = "输入数量");
```

转换失败时框架会显示类型不合法提示，不会继续提交。仍应在业务侧检查范围、权限和业务规则。

### 6.2 编辑对象属性

`ShowPropertyTextBoxCommand` 可根据 `PropertyName` 读取对象属性、显示输入框并尝试转换回属性类型。

适合简单标量属性：

```xaml
<Button
    Command="{h:ShowPropertyTextBoxCommand PropertyName=Name}"
    CommandParameter="{Binding}"
    Content="编辑名称" />
```

不适合复杂对象、敏感字段、异步验证或需要审计的字段。

---

## 7. 密码输入 `PasswordBoxPresenter`

接口：

```csharp
public interface IPasswordBoxPresenter
{
    string Password { get; set; }
}
```

调用：

```csharp
await IocMessage.Dialog.ShowPasswordBox(
    password: null,
    sumitAction: password =>
    {
        ChangePassword(password);
    },
    builder: dialog => dialog.Title = "输入密码",
    canSumit: presenter =>
    {
        return Task.FromResult(!string.IsNullOrWhiteSpace(presenter.Password));
    });
```

安全注意事项：

- `string` 在内存中不可清零，Presenter 只适合短暂交互。
- 不要记录 Presenter、密码值或提交回调参数。
- 不要把密码作为默认值、设置项或日志内容。
- 真正的密码规则、哈希和认证必须在可信身份服务处理。
- 登录和注册场景优先参考 [`development-guide-login.md`](development-guide-login.md)。

---

## 8. 数据表 `DataGridPresenter`

通用数据源接口：

```csharp
public interface IItemsSourcePresenter
{
    IEnumerable ItemsSource { get; set; }
    DataTemplate ItemContentTemplate { get; set; }
}
```

`DataGridPresenter` 用于显示集合：

```csharp
await IocMessage.Dialog.ShowDataGrid(
    option: presenter =>
    {
        presenter.ItemsSource = orders;
    },
    builder: dialog =>
    {
        dialog.Title = "订单列表";
        dialog.MinWidth = 900;
        dialog.MinHeight = 600;
    });
```

默认扩展会设置：

```text
HorizontalContentAlignment = Stretch
MinWidth = 200
Padding = 2
```

### 8.1 显式元素类型

`DataGridTypePresenter` 增加：

```csharp
Type Type { get; set; }
```

泛型调用：

```csharp
await IocMessage.Dialog.ShowTypeDataGrid<Order>(
    option: presenter => presenter.ItemsSource = orders,
    builder: dialog => dialog.Title = "订单列表");
```

当 `ItemsSource` 为空、延迟枚举或运行时类型不易推断时，显式 `Type` 有助于模板和列生成逻辑确定元素类型。

### 8.2 XAML 标记命令

`ShowDataGridCommand` 和 `ShowTypeDataGridCommand` 继承 `ShowSourceCommandBase`，可从命令参数或 `ItemsSource` 属性获取数据：

```xaml
<Button
    Command="{h:ShowDataGridCommand}"
    CommandParameter="{Binding Orders}"
    Content="查看订单" />
```

`ShowTypeDataGridCommand` 可额外指定：

```xaml
<Button
    Command="{h:ShowTypeDataGridCommand Type={x:Type local:Order}}"
    CommandParameter="{Binding Orders}"
    Content="查看订单" />
```

---

## 9. 列表与 `ItemsControl`

`ItemsControlPresenter`、`ListBoxPresenter` 继承 `ItemsSourcePresenterBase`，用于通用集合展示。

`ListBoxPresenter` 接口：

```csharp
public interface IListBoxPresenter : IItemsSourcePresenter
{
}
```

适用选择：

| 需求 | 建议 Presenter |
|---|---|
| 需要表格列、批量数据查看 | `DataGridPresenter`。 |
| 需要简单项列表或自定义项模板 | `ListBoxPresenter`。 |
| 只需要重复显示，不需要选择 | `ItemsControlPresenter`。 |
| 需要分层数据 | `TreeViewPresenter`。 |

通过 `ItemContentTemplate` 提供项目模板：

```csharp
await IocMessage.Dialog.ShowListBox(
    option: presenter =>
    {
        presenter.ItemsSource = products;
        presenter.ItemContentTemplate =
            Application.Current.FindResource("ProductItemTemplate") as DataTemplate;
    },
    builder: dialog => dialog.Title = "产品");
```

模板资源示例：

```xaml
<DataTemplate x:Key="ProductItemTemplate" DataType="{x:Type local:Product}">
    <StackPanel Margin="8" Orientation="Horizontal">
        <TextBlock Text="{Binding Name}" />
        <TextBlock Margin="12,0,0,0" Text="{Binding Price}" />
    </StackPanel>
</DataTemplate>
```

---

## 10. 树 `TreeViewPresenter`

`TreeViewPresenter` 用于展示 `IEnumerable` 树数据：

```csharp
public interface ITreeViewPresenter : IItemsSourcePresenter
{
}
```

调用：

```csharp
await IocMessage.Dialog.ShowTreeView(
    option: presenter =>
    {
        presenter.ItemsSource = rootNodes;
        presenter.ItemContentTemplate =
            Application.Current.FindResource("NodeTemplate") as DataTemplate;
    },
    builder: dialog =>
    {
        dialog.Title = "资源树";
        dialog.MinWidth = 400;
        dialog.MinHeight = 500;
    });
```

`ShowTreeViewCommand` 可从命令参数或 `ItemsSource` 属性获取数据，并尝试从命令目标元素读取项目模板。

```xaml
<Button
    Command="{h:ShowTreeViewCommand}"
    CommandParameter="{Binding RootNodes}"
    Content="查看资源树" />
```

树节点的层级属性、子集合名称和模板行为以具体数据模型及 TreeView 模板为准。需要复杂树编辑、异步加载或虚拟化时，应使用模块专属 Presenter。

---

## 11. 图片预览 `ImageViewPresenter`

接口：

```csharp
public interface IImageViewPresenter
{
    ImageSource ImageSource { get; set; }
}
```

显示 `ImageSource`：

```csharp
await IocMessage.Dialog.ShowImageSource(
    imageSource,
    builder: dialog =>
    {
        dialog.Title = "图片预览";
        dialog.MinWidth = 640;
        dialog.MinHeight = 480;
    });
```

显示本地文件：

```csharp
await IocMessage.Dialog.ShowImageSource(
    @"D:\Images\sample.png");
```

该扩展默认设置：

```text
DialogButton = None
MinWidth = 200
```

### 11.1 图片命令

| 命令 | 用途 |
|---|---|
| `ShowImageFileCommand` | 使用命令参数或 `FilePath` 属性打开图片文件预览；路径为空时打开图片选择对话框。 |
| `ShowImageSourceCommand` | 仅当命令参数是 `ImageSource` 时显示图片。 |

示例：

```xaml
<Button
    Command="{h:ShowImageFileCommand FilePath={Binding ImagePath}}"
    Content="预览图片" />
```

```xaml
<Button
    Command="{h:ShowImageSourceCommand}"
    CommandParameter="{Binding PreviewImage}"
    Content="预览图片" />
```

### 11.2 与 ZoomBox 的区别

`ImageViewPresenter` 是普通图片对话框。需要平移、滚轮缩放、定位器或大图预览时，应使用：

```text
H.Controls.ZoomBox.Extension
ShowZoomViewImage(...)
```

详见 [`development-guide-zoombox.md`](development-guide-zoombox.md)。

---

## 12. 文件与图片路径 Presenter

相关类型：

| 类型 | 用途 |
|---|---|
| `FilePathTextBoxPresenter` | 选择或输入文件路径；实现 `IOpenFilePathable`。 |
| `ImageFilePathPresenter` | 显示或编辑图片文件路径。 |
| `AddImageFilePathPresenter` | 增加图片文件路径。 |
| `AddImageListPresenter` | 管理图片列表。 |
| `FileInfoPresenter` | 显示文件信息。 |

`FilePathTextBoxPresenter` 接口：

```csharp
public interface IFilePathTextBoxPresenter : IOpenFilePathable
{
}
```

这些 Presenter 适合简单文件选择和预览入口。批量导入、文件校验、上传、权限、网络路径和长期任务应放入业务 Service。

文件操作建议：

- 使用文件对话框后验证路径存在性、扩展名、文件大小和可访问性。
- 不信任用户提供的路径。
- 图片解码和大文件读取避免阻塞 UI 线程。
- 文件路径不等同于安全授权；网络路径和外部文件需要额外验证。

---

## 13. 命令 Presenter

相关类型：

```text
CommandsBindablePresenter
CommandsPresenter
CommandsPresenterBase
CommandsPresenter<T>
DialogCommandsPresenter<T>
TestCommandsPresenter
```

用途：将一组 `ICommand`、标记命令或业务操作以统一 UI 呈现，适合工具栏、操作面板、上下文菜单或对话框操作区。

分层建议：

```text
领域 Service       执行业务操作
ViewModel          决定命令可见性、可执行状态和参数
CommandsPresenter  呈现命令集合
DataTemplate       决定按钮、菜单或卡片外观
```

不要在 `CommandsPresenter` 中直接实现数据库访问、网络请求或权限判断。命令执行前应由 Service 或命令本身完成授权检查。

---

## 14. 消息、等待与进度 Presenter

| 类型 | 用途 |
|---|---|
| `MessagePresenter` | 呈现一般消息。 |
| `WaitPresenter` | 显示等待或处理中状态。 |
| `PercentPresenter` | 使用 `Value` 表示整数百分比进度。 |
| `StringPresenter` | 使用 `IStringPresenter` 表示可变字符串内容。 |

`PercentPresenter`：

```csharp
var presenter = new PercentPresenter
{
    Value = 0
};

presenter.Value = 50;
presenter.Value = 100;
```

更新 UI 绑定属性必须在 UI 线程执行，或使用 `Dispatcher` 切换到 UI 线程：

```csharp
await Application.Current.Dispatcher.InvokeAsync(() =>
{
    presenter.Value = progress;
});
```

`WaitPresenter` 和 `MessagePresenter` 适合显示状态，不负责取消、重试、超时和错误恢复策略。长任务应提供 `CancellationToken`、进度汇报和明确的错误消息。

---

## 15. 设置 Presenter

| 类型 | 用途 |
|---|---|
| `MetaSettingPresenter<T>` | 显示或编辑一个泛型设置对象。 |
| `MetaSettingsPresenter<T>` | 显示一组泛型设置或元数据设置。 |

这些 Presenter 用于把设置元数据与框架 Form/PropertyGrid 能力组合显示。设置对象通常应：

- 实现属性变更通知。
- 使用 `DisplayAttribute`、`DefaultValueAttribute` 等元数据。
- 通过 `IocOptionInstance<T>` 或模块 Options 参与持久化。
- 将业务验证放在设置服务或保存流程中。

主题、登录和窗口 Options 的具体模式分别见对应专题文档。

---

## 16. 自定义 Presenter 的标准方式

需要自定义显示而复用对话框机制时，可创建自己的 Presenter、接口和模板。

### 16.1 定义接口与 Presenter

```csharp
public interface IConfirmDeletePresenter
{
    string Name { get; set; }
    bool DeleteFiles { get; set; }
}

public class ConfirmDeletePresenter : DisplayBindableBase, IConfirmDeletePresenter
{
    private string _name;
    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            RaisePropertyChanged();
        }
    }

    private bool _deleteFiles;
    public bool DeleteFiles
    {
        get => _deleteFiles;
        set
        {
            _deleteFiles = value;
            RaisePropertyChanged();
        }
    }
}
```

### 16.2 定义模板

```xaml
<DataTemplate DataType="{x:Type local:ConfirmDeletePresenter}">
    <StackPanel MinWidth="320" Margin="12">
        <TextBlock Text="{Binding Name}" />
        <CheckBox
            Margin="0,12,0,0"
            Content="同时删除关联文件"
            IsChecked="{Binding DeleteFiles}" />
    </StackPanel>
</DataTemplate>
```

### 16.3 定义对话框扩展

```csharp
public static class ConfirmDeleteDialogExtension
{
    public static Task<bool?> ShowConfirmDelete(
        this IDialogMessageService service,
        Action<IConfirmDeletePresenter> option,
        Action<IConfirmDeletePresenter> submit,
        Action<IDialog> builder = null)
    {
        return service.ShowDialog<ConfirmDeletePresenter>(
            option,
            submit,
            dialog =>
            {
                dialog.Title = "确认删除";
                builder?.Invoke(dialog);
            });
    }
}
```

### 16.4 调用

```csharp
await IocMessage.Dialog.ShowConfirmDelete(
    option: presenter => presenter.Name = item.Name,
    submit: presenter =>
    {
        DeleteItem(item, presenter.DeleteFiles);
    });
```

如果 Presenter 是框架外部调用的稳定契约，应注册接口实现或只在扩展方法中创建其具体类型。不要让业务调用方依赖模板内部元素。

---

## 17. 异步提交与错误处理

大部分对话框扩展提供：

```csharp
Func<TPresenter, Task<bool>> canSumit
```

`canSumit` 是当前 API 的实际拼写。它适合在提交前执行异步校验：

```csharp
await IocMessage.Dialog.ShowTextBox(
    option: presenter => presenter.Text = userName,
    sumitAction: presenter => SaveUserName(presenter.Text),
    canSumit: async presenter =>
    {
        bool exists = await _userService.ExistsAsync(presenter.Text);
        if (exists)
        {
            IocMessage.Snack.ShowInfo("名称已存在");
            return false;
        }

        return true;
    });
```

建议：

- 本地格式校验可以直接返回完成的 `Task<bool>`。
- 远程校验应处理超时、取消和异常。
- 失败时向用户显示可理解消息，日志中记录技术细节。
- `sumitAction` 中的持久化失败应有明确策略；不要假设提交回调一定成功。
- 涉及删除、支付、权限或安全操作时，服务端仍需再次验证。

---

## 18. 样式和主题

Presenter 模板位于 `Presenters/*.xaml`，应使用框架主题资源：

```xaml
<Border
    Padding="{DynamicResource {x:Static h:LayoutKeys.Padding}}"
    Background="{DynamicResource {x:Static h:BrushKeys.CaptionBackground}}"
    BorderBrush="{DynamicResource {x:Static h:BrushKeys.BorderBrush}}">
    <TextBlock
        FontSize="{DynamicResource {x:Static h:FontSizeKeys.Default}}"
        Foreground="{DynamicResource {x:Static h:BrushKeys.Foreground}}" />
</Border>
```

规则：

- 使用 `DynamicResource` 支持运行时主题切换。
- 文字、边框、背景优先使用语义化 `BrushKeys`。
- 尺寸、间距、圆角优先使用 `LayoutKeys`。
- 字号优先使用 `FontSizeKeys`。
- 业务模块需要专属颜色时定义自己的 `ComponentResourceKey`，不要滥用全局 Key。

详见 [`development-guide-theme.md`](development-guide-theme.md)。

---

## 19. 常见问题

### 对话框显示为空白

检查 Presenter 的 `DataTemplate` 是否已合并，实际类型是否匹配，以及是否被应用资源中的同类型模板覆盖。

### 调用 `IocMessage.Dialog` 时为空

应用未注册对话框消息服务。注册合适的窗口和对话框服务后再调用。

### 提交后没有执行回调

检查：

1. 用户是否点击提交而不是取消。
2. `canSumit` 是否返回 `true`。
3. 输入类型转换是否成功。
4. 对话框是否设置了 `DialogButton=None`。

### 集合能显示但项外观不正确

提供 `ItemContentTemplate`，或在应用资源中定义目标项类型的隐式 `DataTemplate`。

### 大集合显示卡顿

不要把无边界数据一次性放入 `DataGridPresenter` 或 `ListBoxPresenter`。使用分页、筛选、虚拟化或模块专属数据视图。

### 图片预览无法缩放

`ImageViewPresenter` 只负责普通图片展示。需要缩放和平移时使用 ZoomBox 图片预览扩展。

### 输入 Presenter 中保存了敏感数据

通用 Presenter 不提供安全存储。不要把密码、令牌或验证码写入 Options、日志或长期 ViewModel 状态。

---

## 20. 二次开发建议

- 简单显示和输入优先复用 Common Presenter，避免为每个字段创建 Window。
- Presenter 只描述 UI 状态；领域操作委托给 Service。
- 需要稳定对外调用时提供接口和 `DialogServiceExtension`。
- 每个自定义 Presenter 必须有可发现的 `DataTemplate`。
- 调用对话框时在 `builder` 中集中设置标题、尺寸和按钮策略。
- 使用 `canSumit` 做交互前校验，使用 Service 做最终业务校验。
- 大数据、复杂树、长任务和复杂编辑流程创建领域专属 Presenter。
- 模板使用主题资源，避免硬编码颜色和尺寸。
- 密码、令牌、验证码和文件路径按安全边界处理。
