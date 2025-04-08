// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Services.Message.Dialog;

/// <summary>
/// 对话框消息服务接口
/// </summary>
public interface IDialogMessageService
{
    /// <summary>
    /// 显示对话框
    /// </summary>
    /// <param name="presenter">对话框的呈现者</param>
    /// <param name="builder">对话框构建器</param>
    /// <param name="canSumit">确定按钮是否可用的条件</param>
    /// <returns>对话框的结果</returns>
    Task<bool?> Show(object presenter, Action<IDialog> builder = null, Func<Task<bool>> canSumit = null);

    /// <summary>
    /// 显示带操作的对话框
    /// </summary>
    /// <typeparam name="P">呈现者的类型</typeparam>
    /// <typeparam name="T">操作结果的类型</typeparam>
    /// <param name="presenter">对话框的呈现者</param>
    /// <param name="builder">对话框构建器</param>
    /// <param name="action">对话框操作的委托</param>
    /// <returns>对话框的操作结果</returns>
    Task<T> ShowAction<P, T>(P presenter, Action<IDialog> builder = null, Func<IDialog, P, T> action = null);

    /// <summary>
    /// 显示百分比对话框
    /// </summary>
    /// <typeparam name="T">操作结果的类型</typeparam>
    /// <param name="action">百分比对话框操作的委托</param>
    /// <param name="build">对话框构建器</param>
    /// <returns>对话框的操作结果</returns>
    Task<T> ShowPercent<T>(Func<IDialog, IPercentPresenter, T> action, Action<IDialog> build = null);

    /// <summary>
    /// 显示字符串对话框
    /// </summary>
    /// <typeparam name="T">操作结果的类型</typeparam>
    /// <param name="action">字符串对话框操作的委托</param>
    /// <param name="build">对话框构建器</param>
    /// <returns>对话框的操作结果</returns>
    Task<T> ShowString<T>(Func<IDialog, IStringPresenter, T> action, Action<IDialog> build = null);

    /// <summary>
    /// 显示等待对话框
    /// </summary>
    /// <typeparam name="T">操作结果的类型</typeparam>
    /// <param name="action">等待对话框操作的委托</param>
    /// <param name="build">对话框构建器</param>
    /// <returns>对话框的操作结果</returns>
    Task<T> ShowWait<T>(Func<IDialog, T> action, Action<IDialog> build = null);

    /// <summary>
    /// 显示循环对话框
    /// </summary>
    /// <typeparam name="T">列表项的类型</typeparam>
    /// <param name="getList">获取列表的委托</param>
    /// <param name="itemAction">列表项操作的委托</param>
    /// <param name="build">对话框构建器</param>
    /// <returns>对话框的操作结果</returns>
    Task<bool> ShowForeach<T>(Func<IEnumerable<T>> getList, Func<T, Tuple<bool, string>> itemAction, Action<IDialog> build = null);
}

/// <summary>
/// 对话框消息服务扩展类
/// </summary>
public static class DialogMessageExtension
{
    /// <summary>
    /// 显示对话框
    /// </summary>
    /// <param name="service">对话框消息服务</param>
    /// <param name="presenter">对话框的呈现者</param>
    /// <param name="sumitAction">确定按钮点击后的操作</param>
    /// <param name="builder">对话框构建器</param>
    /// <param name="canSumit">确定按钮是否可用的条件</param>
    /// <returns>对话框的结果</returns>
    public static async Task<bool?> ShowDialog(this IDialogMessageService service, object presenter, Action<bool?> sumitAction, Action<IDialog> builder = null, Func<Task<bool>> canSumit = null)
    {
        bool? r = await service.Show(presenter, builder, canSumit);
        if (r != true)
            return r;
        sumitAction?.Invoke(r);
        return r;
    }

    /// <summary>
    /// 显示对话框
    /// </summary>
    /// <typeparam name="T">呈现者的类型</typeparam>
    /// <param name="dialog">对话框消息服务</param>
    /// <param name="option">对话框呈现者的选项设置</param>
    /// <param name="sumitAction">确定按钮点击后的操作</param>
    /// <param name="builder">对话框构建器</param>
    /// <param name="canSumit">确定按钮是否可用的条件</param>
    /// <returns>对话框的结果</returns>
    public static async Task<bool?> ShowDialog<T>(this IDialogMessageService dialog, Action<T> option, Action<T> sumitAction, Action<IDialog> builder = null, Func<T, Task<bool>> canSumit = null) where T : new()
    {
        T presenter = new T();
        option?.Invoke(presenter);
        return await dialog.ShowDialog(presenter, x => sumitAction?.Invoke(presenter), builder, () => canSumit?.Invoke(presenter));
    }

    /// <summary>
    /// 显示全部删除对话框
    /// </summary>
    /// <param name="service">对话框消息服务</param>
    /// <param name="sumitAction">确定按钮点击后的操作</param>
    /// <returns>对话框的结果</returns>
    public static async Task<bool?> ShowDeleteAllDialog(this IDialogMessageService service, Action<bool?> sumitAction)
    {
        return await service.ShowDialog("删除数据无法恢复，确定要全部删除？", sumitAction);
    }

    /// <summary>
    /// 显示删除对话框
    /// </summary>
    /// <param name="service">对话框消息服务</param>
    /// <param name="sumitAction">确定按钮点击后的操作</param>
    /// <returns>对话框的结果</returns>
    public static async Task<bool?> ShowDeleteDialog(this IDialogMessageService service, Action<bool?> sumitAction)
    {
        return await service.ShowDialog("删除数据无法恢复，确定要删除？", sumitAction);
    }

    /// <summary>
    /// 显示未实现功能对话框
    /// </summary>
    /// <param name="service">对话框消息服务</param>
    /// <returns>对话框的结果</returns>
    public static async Task<bool?> ShowNotImplementedDialog(this IDialogMessageService service)
    {
        return await service.Show("没有实现该功能");
    }
}
