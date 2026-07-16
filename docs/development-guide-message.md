# Message 消息系统二次开发文档
# Message 消息系统二次开发文档

**适用项目：** `H.Services.Message`、`H.Modules.Messages.*`、`H.ApplicationBases.Messages`  
**核心类型：** `IocMessage`、`IDialogMessageService`、`ISnackMessageService`、`INoticeMessageService`、`IFormMessageService`  
**相关能力：** 对话框、窗口消息、Adorner 消息、Snack、Notice、表单编辑、文件/文件夹选择、任务栏、系统通知

本文介绍 WPF-Control 的 Message 消息系统，重点说明 `IocMessage` 中所有消息入口、各消息服务接口职责、注册方式，以及二次开发时如何使用和扩展消息服务。

---

## 1. Message 系统定位

Message 系统用于统一处理应用中的用户提示和交互反馈，包括：

- 普通确认对话框。
- Window 模态对话框。
- 主窗口 Adorner 对话框。
- Snack 轻提示。
- Notice 通知消息。
- 表单编辑和表单查看。
- 文件选择、文件保存、文件夹选择。
- 任务栏进度和任务栏状态。
- 系统托盘气泡通知。

推荐业务代码通过 `IocMessage` 访问消息能力，避免直接依赖某个具体 UI 实现。

---

## 2. 相关项目

| 项目 | 说明 |
|---|---|
| `H.Services.Message` | 定义消息服务接口、`IocMessage` 静态入口、对话框基础类型和命令。 |
| `H.Modules.Messages.Dialog` | 提供 `WindowDialogMessageService`、`WindowTransparencyDialogMessageService`、`AdornerDialogMessageService` 等对话框实现。 |
| `H.Modules.Messages.Snack` | 提供 Snack 轻提示实现。 |
| `H.Modules.Messages.Notice` | 提供 Notice 通知实现。 |
| `H.Modules.Messages.Form` | 提供表单编辑/查看消息实现。 |
| `H.Extensions.OpenFolderDialog` | 提供文件夹选择服务实现。 |
| `H.ApplicationBases.Messages` | 提供一组默认消息服务注册扩展。 |

---

## 3. `IocMessage` 总入口

`IocMessage` 是消息服务的静态门面：

```csharp
public static class IocMessage
{
    public static IAdornerDialogMessageService Adorner => Ioc.GetService<IAdornerDialogMessageService>(false);
    public static IWindowDialogMessageService Window => Ioc.GetService<IWindowDialogMessageService>(false);
    public static IDialogMessageService Dialog => Ioc.GetService<IDialogMessageService>(false);
    public static ISnackMessageService Snack => Ioc.GetService<ISnackMessageService>(false);
    public static ITaskBarMessage TaskBar => Ioc.GetService<ITaskBarMessage>(false);
    public static ISystemNotifyMessage SystemNotify => Ioc.GetService<ISystemNotifyMessage>(false);
    public static INoticeMessageService Notify => Ioc.GetService<INoticeMessageService>(false);
    public static IFormMessageService Form => Ioc.GetService<IFormMessageService>(false);
    public static IIOFileDialogService IOFileDialog => Ioc.GetService<IIOFileDialogService>(false) ?? new IOFileDialogService();
    public static IIOFolderDialogService IOFolderDialog => Ioc.GetService<IIOFolderDialogService>(false);
}
```

消息入口说明：

| 入口 | 接口 | 说明 |
|---|---|---|
| `IocMessage.Adorner` | `IAdornerDialogMessageService` | 主窗口 Adorner 层对话框。 |
| `IocMessage.Window` | `IWindowDialogMessageService` | 独立 Window 对话框。 |
| `IocMessage.Dialog` | `IDialogMessageService` | 默认对话框服务，可能是 Adorner 或 Window。 |
| `IocMessage.Snack` | `ISnackMessageService` | Snack 轻提示。 |
| `IocMessage.Notify` | `INoticeMessageService` | Notice 通知消息。 |
| `IocMessage.Form` | `IFormMessageService` | 表单编辑/查看消息。 |
| `IocMessage.IOFileDialog` | `IIOFileDialogService` | 文件打开/保存对话框；未注册时使用默认 `IOFileDialogService`。 |
| `IocMessage.IOFolderDialog` | `IIOFolderDialogService` | 文件夹选择对话框。 |
| `IocMessage.TaskBar` | `ITaskBarMessage` | 任务栏进度和状态。 |
| `IocMessage.SystemNotify` | `ISystemNotifyMessage` | 系统托盘气泡通知。 |

---

## 4. 注册消息服务

### 4.1 常用注册

在 `ApplicationBase.ConfigureServices` 中注册：

```csharp
protected override void ConfigureServices(IServiceCollection services)
{
    services.AddWindowMessage();
    services.AddAdornerDialogMessage();
    services.AddSnackMessage();
    services.AddNoticeMessage();
    services.AddFormMessageService();
    services.AddIOFolderDialogService();
}
```

### 4.2 对话框注册方法

`H.Modules.Messages.Dialog` 提供：

| 方法 | 注册服务 | 说明 |
|---|---|---|
| `AddWindowDialogMessage()` | `IDialogMessageService -> WindowDialogMessageService` | 默认对话框使用独立 Window。 |
| `AddWindowTransparencyDialogMessage()` | `IDialogMessageService -> WindowTransparencyDialogMessageService` | 默认对话框使用透明 Window。 |
| `AddAdornerDialogMessage()` | `IDialogMessageService -> AdornerDialogMessageService` | 默认对话框使用主窗口 Adorner。 |
| `AddWindowMessage()` | `IWindowDialogMessageService -> WindowDialogMessageService` | 注册 Window 对话框入口。 |
| `AddWindowTransparencyMessage()` | `IWindowDialogMessageService -> WindowTransparencyDialogMessageService` | 注册透明 Window 对话框入口。 |

### 4.3 Snack、Notice、Form 注册方法

```csharp
services.AddSnackMessage();       // ISnackMessageService
services.AddNoticeMessage();      // INoticeMessageService
services.AddFormMessageService(); // IFormMessageService
```

### 4.4 默认消息组合

`H.ApplicationBases.Messages` 中提供 `AddDefaultMessages()`，默认包含：

```csharp
services.AddAdornerDialogMessage();
services.AddWindowTransparencyMessage();
services.AddFormMessageService();
services.AddNoticeMessage();
services.AddSnackMessage();
services.AddIOFolderDialogService();
```

适合应用快速启用常用消息能力。

---

## 5. `ShowDialogMessage` 普通提示对话框

```csharp
public static async Task<bool?> ShowDialogMessage(
    string message,
    string title = null,
    DialogButton dialogButton = DialogButton.Sumit)
```

用途：显示文本消息，并返回用户操作结果。

示例：

```csharp
bool? result = await IocMessage.ShowDialogMessage("确认删除当前数据？", "确认", DialogButton.SumitAndCancel);
if (result == true)
{
    // 用户点击确定
}
```

内部逻辑：

- 如果主窗口未加载，或未注册 `Dialog`，优先使用 `Window`。
- 如果 `Window` 也未注册，则回退到 WPF `MessageBox.Show`。
- 主窗口加载后且注册了 `Dialog`，使用 `Dialog.Show(...)`。

返回值：

| 返回值 | 说明 |
|---|---|
| `true` | 用户确认。 |
| `false` | 用户取消。 |
| `null` | 没有结果或对话框关闭。 |

---

## 6. `ShowDialog` 自定义内容对话框

```csharp
public static async Task<bool?> ShowDialog(object presenter, Action<IDialog> builder = null)
```

用途：显示任意 Presenter、ViewModel、UserControl 或字符串内容。

示例：

```csharp
var presenter = new MyEditPresenter();
bool? result = await IocMessage.ShowDialog(presenter, dialog =>
{
    dialog.Title = "编辑数据";
    dialog.MinWidth = 600;
    dialog.DialogButton = DialogButton.SumitAndCancel;
});
```

如果未注册窗口消息服务，会创建一个基础 `Window` 作为回退：

```csharp
new Window()
{
    Content = presenter,
    SizeToContent = SizeToContent.WidthAndHeight,
    WindowStartupLocation = WindowStartupLocation.CenterScreen
}.ShowDialog();
```

---

## 7. `ShowWindowMessage` 强制 Window 消息

```csharp
public static async Task<bool?> ShowWindowMessage(
    string message,
    string title = null,
    DialogButton dialogButton = DialogButton.Sumit)
```

用途：不走 `Dialog` 选择逻辑，直接使用 `Window` 消息；如果未注册则回退 `MessageBox`。

示例：

```csharp
await IocMessage.ShowWindowMessage("启动失败，请检查配置文件。", "错误");
```

适用场景：

- 应用主窗口尚未加载。
- 启动流程、异常处理、登录前提示。
- 希望强制弹出独立窗口。

---

## 8. `ShowSnackInfo` Snack 快捷提示

```csharp
public static async void ShowSnackInfo(string message = "操作成功")
```

用途：显示轻提示信息。

示例：

```csharp
IocMessage.ShowSnackInfo("保存成功");
```

内部逻辑：

- 如果未注册 `Snack`，回退为 `ShowDialogMessage(message)`。
- 如果已注册 `Snack`，调用 `Snack.ShowInfo(message)`。

> 注意：这是 `async void` 快捷方法，适合 UI 事件和简单提示。复杂业务建议直接使用 `IocMessage.Snack` 的异步方法。

---

## 9. `ShowNotifyInfo` Notice 快捷提示

```csharp
public static async void ShowNotifyInfo(string message = "操作成功")
```

用途：显示 Notice 通知信息。

示例：

```csharp
IocMessage.ShowNotifyInfo("导入完成");
```

内部逻辑：

- 如果未注册 Snack，回退为 `ShowDialogMessage(message)`。
- 否则调用 `Notify.ShowInfo(message)`。

> 当前实现中判断条件是 `Snack == null`，但实际调用的是 `Notify.ShowInfo`。使用时建议同时注册 `Snack` 和 `Notice`，或直接调用 `IocMessage.Notify` 并做好空判断。

---

## 10. Dialog 消息服务

### 10.1 `IDialogMessageService`

核心接口：

```csharp
public interface IDialogMessageService
{
    Task<bool?> Show(object presenter, Action<IDialog> builder = null, Func<Task<bool>> canSumit = null);
    Task<T> ShowAction<P, T>(P presenter, Action<IDialog> builder = null, Func<IDialog, P, T> action = null);
    Task<T> ShowPercent<T>(Func<IDialog, IPercentPresenter, T> action, Action<IDialog> build = null);
    Task<T> ShowString<T>(Func<IDialog, IStringPresenter, T> action, Action<IDialog> build = null);
    Task<T> ShowWait<T>(Func<IDialog, T> action, Action<IDialog> build = null);
    Task<bool> ShowForeach<T>(Func<IEnumerable<T>> getList, Func<T, Tuple<bool, string>> itemAction, Action<IDialog> build = null);
}
```

### 10.2 `Show`

显示普通对话框：

```csharp
bool? result = await IocMessage.Dialog.Show("保存成功", x =>
{
    x.Title = "提示";
    x.DialogButton = DialogButton.Sumit;
});
```

带确定条件：

```csharp
await IocMessage.Dialog.Show(presenter, builder, async () =>
{
    return await presenter.ValidateAsync();
});
```

### 10.3 `ShowAction`

显示带操作的对话框，适合启动页、登录后加载页、长流程任务：

```csharp
bool result = await IocMessage.Dialog.ShowAction(new MyPresenter(), x =>
{
    x.Title = "执行任务";
    x.DialogButton = DialogButton.None;
}, (dialog, presenter) =>
{
    presenter.Message = "正在执行...";
    return true;
});
```

### 10.4 `ShowPercent`

显示百分比进度：

```csharp
bool result = await IocMessage.Dialog.ShowPercent((dialog, percent) =>
{
    for (int i = 0; i <= 100; i++)
    {
        percent.Value = i;
        percent.Message = $"{i}%";
        Thread.Sleep(20);
    }
    return true;
});
```

### 10.5 `ShowString`

显示字符串进度或日志：

```csharp
await IocMessage.Dialog.ShowString((dialog, presenter) =>
{
    presenter.Value = "正在导入文件...";
    return true;
});
```

### 10.6 `ShowWait`

显示等待对话框：

```csharp
var result = await IocMessage.Dialog.ShowWait(dialog =>
{
    Thread.Sleep(1000);
    return true;
});
```

### 10.7 `ShowForeach`

按集合逐项执行任务：

```csharp
bool result = await IocMessage.Dialog.ShowForeach(
    () => files,
    file =>
    {
        // 返回 Item1 表示是否成功，Item2 表示提示信息
        return Tuple.Create(true, file);
    });
```

---

## 11. Dialog 扩展方法

`DialogMessageExtension` 提供常用语义化方法。

### 11.1 `ShowDialog`

```csharp
await IocMessage.Dialog.ShowDialog("确认提交？", r =>
{
    if (r == true)
    {
        // 提交
    }
});
```

泛型创建 Presenter：

```csharp
await IocMessage.Dialog.ShowDialog<MyPresenter>(
    option => option.Name = "test",
    presenter => Save(presenter));
```

### 11.2 删除确认

```csharp
await IocMessage.Dialog.ShowDeleteDialog(r =>
{
    if (r == true)
        DeleteCurrent();
});
```

全部删除确认：

```csharp
await IocMessage.Dialog.ShowDeleteAllDialog(r =>
{
    if (r == true)
        DeleteAll();
});
```

### 11.3 未实现提示

```csharp
await IocMessage.Dialog.ShowNotImplementedDialog();
```

---

## 12. Window 与 Adorner 对话框

### 12.1 `IWindowDialogMessageService`

继承 `IDialogMessageService`，用于独立 Window 对话框。

```csharp
await IocMessage.Window.Show("窗口消息", x =>
{
    x.Title = "Window";
});
```

适用场景：

- 主窗口未加载。
- 启动、登录、异常处理。
- 希望对话框脱离主窗口视觉层。

### 12.2 `IAdornerDialogMessageService`

继承 `IDialogMessageService`，用于主窗口 Adorner 层对话框。

```csharp
await IocMessage.Adorner.Show("主窗口内消息", x =>
{
    x.Title = "Adorner";
});
```

适用场景：

- 主窗口内嵌弹层。
- 不希望创建额外 Window。
- 保持应用视觉沉浸感。

---

## 13. Snack 消息

`ISnackMessageService` 用于轻提示消息：

```csharp
public interface ISnackMessageService
{
    Task<bool?> ShowDialog(string message);
    void ShowError(string message = "运行错误");
    void ShowFatal(string message = "严重错误");
    void ShowInfo(string message = "运行完成");
    void Show(ISnackItem message);
    Task<T> ShowProgress<T>(Func<IPercentSnackItem, T> action);
    Task<T> ShowString<T>(Func<ISnackItem, T> action);
    void ShowSuccess(string message = "运行成功");
    void ShowWarn(string message = "异常警告");
}
```

常用示例：

```csharp
IocMessage.Snack.ShowInfo("正在加载...");
IocMessage.Snack.ShowSuccess("保存成功");
IocMessage.Snack.ShowWarn("参数不完整");
IocMessage.Snack.ShowError("保存失败");
IocMessage.Snack.ShowFatal("严重错误");
```

进度：

```csharp
await IocMessage.Snack.ShowProgress(progress =>
{
    for (int i = 0; i <= 100; i++)
    {
        progress.Value = i;
        progress.Message = $"{i}%";
        Thread.Sleep(20);
    }
    return true;
});
```

跨线程调用扩展：

```csharp
IocMessage.Snack.ShowSuccessDispatcher("后台任务完成");
```

---

## 14. Notice 消息

`INoticeMessageService` 与 Snack 类似，适合通知列表或通知区域展示。

```csharp
IocMessage.Notify.ShowInfo("运行完成");
IocMessage.Notify.ShowSuccess("导入成功");
IocMessage.Notify.ShowWarn("磁盘空间不足");
IocMessage.Notify.ShowError("导入失败");
IocMessage.Notify.ShowFatal("严重错误");
```

进度通知：

```csharp
await IocMessage.Notify.ShowProgress(progress =>
{
    progress.Value = 50;
    progress.Message = "处理中...";
    return true;
});
```

字符串动态通知：

```csharp
await IocMessage.Notify.ShowString(item =>
{
    item.Message = "正在上传...";
    return true;
});
```

跨线程调用：

```csharp
IocMessage.Notify.ShowInfoDispatcher("后台通知");
```

---

## 15. Form 表单消息

`IFormMessageService` 用于对象编辑和查看。

```csharp
public interface IFormMessageService
{
    Task<bool?> ShowEdit<T>(T value, Action<IDialog> action = null, Predicate<T> match = null, Action<IFormOption> option = null, Window owner = null);
    Task<bool?> ShowView<T>(T value, Action<IDialog> action = null, Action<IFormOption> option = null, Window owner = null);
    Task<bool?> ShowTabEdit<T>(T value, Action<IDialog> action = null, Predicate<T> match = null, Action<ITabFormOption> option = null, Window owner = null);
}
```

编辑对象：

```csharp
var model = new UserOptions();
bool? result = await IocMessage.Form.ShowEdit(model, dialog =>
{
    dialog.Title = "编辑用户";
});
```

只读查看：

```csharp
await IocMessage.Form.ShowView(model, dialog =>
{
    dialog.Title = "查看详情";
});
```

Tab 表单编辑：

```csharp
await IocMessage.Form.ShowTabEdit(model, dialog =>
{
    dialog.Title = "高级设置";
});
```

适合场景：

- 快速编辑配置对象。
- 展示数据详情。
- 根据属性特性自动生成表单。

---

## 16. 文件和文件夹对话框

### 16.1 文件对话框

`IIOFileDialogService`：

```csharp
string ShowOpenFile(Action<IIOFileDialogOption> optionAction);
string[] ShowOpenFiles(Action<IIOFileDialogOption> optionAction);
string ShowSaveFile(Action<IIOSaveFileDialogOption> optionAction);
```

示例：

```csharp
string file = IocMessage.IOFileDialog.ShowOpenFile(option =>
{
    option.Filter = "文本文件|*.txt|所有文件|*.*";
    option.Title = "选择文件";
});
```

多选：

```csharp
string[] files = IocMessage.IOFileDialog.ShowOpenFiles(option =>
{
    option.Filter = "图片|*.png;*.jpg;*.jpeg";
});
```

保存：

```csharp
string saveFile = IocMessage.IOFileDialog.ShowSaveFile(option =>
{
    option.Filter = "JSON|*.json";
    option.FileName = "config.json";
});
```

`IocMessage.IOFileDialog` 未注册时会回退为默认 `IOFileDialogService`。

### 16.2 文件夹对话框

`IIOFolderDialogService`：

```csharp
string folder = IocMessage.IOFolderDialog.ShowOpenFolder("选择输出目录");
```

带回调扩展：

```csharp
IocMessage.IOFolderDialog.ShowOpenFolderAction(folder =>
{
    OutputFolder = folder;
});
```

需要注册：

```csharp
services.AddIOFolderDialogService();
```

---

## 17. TaskBar 任务栏消息

`ITaskBarMessage` 用于控制 Windows 任务栏状态：

```csharp
public interface ITaskBarMessage
{
    void Show(Action<TaskbarItemInfo> action);
    void ShowImage(ImageSource image);
    void ShowNormal(Action<TaskbarItemInfo> action);
    Task ShowPercent(Action<TaskbarItemInfo> action);
    Task<bool> ShowWaitting(Func<bool> action);
}
```

示例：

```csharp
IocMessage.TaskBar?.Show(info =>
{
    info.ProgressState = TaskbarItemProgressState.Normal;
    info.ProgressValue = 0.5;
});
```

等待任务：

```csharp
await IocMessage.TaskBar.ShowWaitting(() =>
{
    Thread.Sleep(1000);
    return true;
});
```

---

## 18. SystemNotify 系统通知

`ISystemNotifyMessage` 用于系统托盘气泡通知：

```csharp
public interface ISystemNotifyMessage
{
    void Show(string message, string title = null, NotifyBalloonIcon tipIcon = NotifyBalloonIcon.Info, int timeout = 1000);
}
```

示例：

```csharp
IocMessage.SystemNotify?.Show("任务完成", "提示", NotifyBalloonIcon.Info, 3000);
```

适合：

- 后台任务完成提示。
- 最小化到托盘后的提示。
- 不打断用户操作的通知。

---

## 19. DialogButton 和返回值

常用按钮枚举：

| 枚举 | 说明 |
|---|---|
| `DialogButton.None` | 不显示按钮。 |
| `DialogButton.Sumit` | 只显示确定。 |
| `DialogButton.Cancel` | 只显示取消。 |
| `DialogButton.SumitAndCancel` | 显示确定和取消。 |

常见返回值：

| 返回值 | 说明 |
|---|---|
| `true` | 用户确认或流程成功。 |
| `false` | 用户取消或流程失败。 |
| `null` | 对话框关闭、取消进度或无结果。 |

---

## 20. 自定义消息服务

如果需要替换默认消息实现，实现对应接口并注册即可。

示例：自定义 Snack：

```csharp
public class MySnackMessageService : ISnackMessageService
{
    public void ShowInfo(string message = "运行完成")
    {
        // 自定义显示逻辑
    }

    public void ShowSuccess(string message = "运行成功") { }
    public void ShowWarn(string message = "异常警告") { }
    public void ShowError(string message = "运行错误") { }
    public void ShowFatal(string message = "严重错误") { }
    public void Show(ISnackItem message) { }
    public Task<bool?> ShowDialog(string message) => Task.FromResult<bool?>(true);
    public Task<T> ShowProgress<T>(Func<IPercentSnackItem, T> action) => Task.FromResult(default(T));
    public Task<T> ShowString<T>(Func<ISnackItem, T> action) => Task.FromResult(default(T));
}
```

注册：

```csharp
services.AddSingleton<ISnackMessageService, MySnackMessageService>();
```

建议：

- 应用层替换默认实现时，注意注册顺序。
- 模块层默认注册可使用 `TryAdd`，避免覆盖应用层自定义实现。
- 业务代码使用 `IocMessage.Snack`，不要直接依赖具体实现类。

---

## 21. 二次开发建议

- 普通确认用 `IocMessage.ShowDialogMessage(...)`。
- 主窗口加载前或启动异常用 `IocMessage.ShowWindowMessage(...)`。
- 复杂内容弹窗用 `IocMessage.ShowDialog(presenter, builder)`。
- 主窗口内部弹层优先使用 `IocMessage.Dialog` 或 `IocMessage.Adorner`。
- 轻提示优先使用 `IocMessage.Snack`。
- 通知列表或持久化通知使用 `IocMessage.Notify`。
- 对象编辑优先使用 `IocMessage.Form.ShowEdit(...)`。
- 文件路径选择使用 `IocMessage.IOFileDialog` / `IOFolderDialog`。
- 后台线程更新 UI 消息时使用 `ShowInfoDispatcher` 等 Dispatcher 扩展。
- 对可选消息服务使用空判断：`IocMessage.TaskBar?.Show(...)`。

---

## 22. 常见问题

### `IocMessage.Dialog` 为空

原因：没有注册默认对话框服务。

解决：

```csharp
services.AddAdornerDialogMessage();
// 或
services.AddWindowDialogMessage();
```

### 启动阶段对话框不显示在主窗口内

启动阶段 `Application.Current.MainWindow` 可能还未加载，`IocMessage` 会走 Window 或 MessageBox 回退逻辑。这是预期行为。

### Snack 没有效果

检查是否注册：

```csharp
services.AddSnackMessage();
```

如果未注册，`ShowSnackInfo` 会回退为对话框提示。

### Notice 没有效果

检查是否注册：

```csharp
services.AddNoticeMessage();
```

建议直接调用前做空判断：

```csharp
IocMessage.Notify?.ShowInfo("消息");
```

### 文件夹选择为空

`IocMessage.IOFolderDialog` 没有默认回退实现，需要注册：

```csharp
services.AddIOFolderDialogService();
```

### 对话框确定按钮需要校验

使用 `IDialogMessageService.Show(..., canSumit)`：

```csharp
await IocMessage.Dialog.Show(presenter, null, async () =>
{
    return await presenter.ValidateAsync();
});
```

### 后台线程调用消息报错

使用 Dispatcher 扩展：

```csharp
IocMessage.Snack.ShowSuccessDispatcher("完成");
IocMessage.Notify.ShowInfoDispatcher("完成");
```
