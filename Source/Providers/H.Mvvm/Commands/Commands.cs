// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Mvvm.Commands;

public static class Commands
{
    /// <summary>
    ///     搜索
    /// </summary>
    public static RoutedCommand Search { get; } = new RoutedCommand(nameof(Search), typeof(Commands));

    /// <summary>
    ///     设置
    /// </summary>
    public static RoutedCommand Setting { get; } = new RoutedCommand(nameof(Setting), typeof(Commands));

    /// <summary>
    ///     清除
    /// </summary>
    public static RoutedCommand Clear { get; } = new RoutedCommand(nameof(Clear), typeof(Commands));

    /// <summary>
    ///     切换
    /// </summary>
    public static RoutedCommand Switch { get; } = new RoutedCommand(nameof(Switch), typeof(Commands));

    /// <summary>
    ///     右转
    /// </summary>
    public static RoutedCommand RotateRight { get; } = new RoutedCommand(nameof(RotateRight), typeof(Commands));

    /// <summary>
    ///     左转
    /// </summary>
    public static RoutedCommand RotateLeft { get; } = new RoutedCommand(nameof(RotateLeft), typeof(Commands));

    /// <summary>
    ///     小
    /// </summary>
    public static RoutedCommand Reduce { get; } = new RoutedCommand(nameof(Reduce), typeof(Commands));

    /// <summary>
    ///     大
    /// </summary>
    public static RoutedCommand Enlarge { get; } = new RoutedCommand(nameof(Enlarge), typeof(Commands));

    /// <summary>
    ///     还原
    /// </summary>
    public static RoutedCommand Restore { get; } = new RoutedCommand(nameof(Restore), typeof(Commands));

    /// <summary>
    ///     新建
    /// </summary>
    public static RoutedCommand New { get; } = new RoutedCommand(nameof(New), typeof(Commands));

    /// <summary>
    ///     打开
    /// </summary>
    public static RoutedCommand Open { get; } = new RoutedCommand(nameof(Open), typeof(Commands));

    /// <summary>
    ///     保存
    /// </summary>
    public static RoutedCommand Save { get; } = new RoutedCommand(nameof(Save), typeof(Commands));

    /// <summary>
    ///     选中
    /// </summary>
    public static RoutedCommand Selected { get; } = new RoutedCommand(nameof(Selected), typeof(Commands));

    /// <summary>
    ///     关闭
    /// </summary>
    public static RoutedCommand Close { get; } = new RoutedCommand(nameof(Close), typeof(Commands));

    /// <summary>
    ///     取消
    /// </summary>
    public static RoutedCommand Cancel { get; } = new RoutedCommand(nameof(Cancel), typeof(Commands));

    /// <summary>
    ///     确定
    /// </summary>
    public static RoutedCommand Confirm { get; } = new RoutedCommand(nameof(Confirm), typeof(Commands));

    /// <summary>
    ///     是
    /// </summary>
    public static RoutedCommand Yes { get; } = new RoutedCommand(nameof(Yes), typeof(Commands));

    /// <summary>
    ///     否
    /// </summary>
    public static RoutedCommand No { get; } = new RoutedCommand(nameof(No), typeof(Commands));

    /// <summary>
    ///     关闭所有
    /// </summary>
    public static RoutedCommand CloseAll { get; } = new RoutedCommand(nameof(CloseAll), typeof(Commands));

    /// <summary>
    ///     关闭其他
    /// </summary>
    public static RoutedCommand CloseOther { get; } = new RoutedCommand(nameof(CloseOther), typeof(Commands));

    /// <summary>
    ///     上一个
    /// </summary>
    public static RoutedCommand Prev { get; } = new RoutedCommand(nameof(Prev), typeof(Commands));

    /// <summary>
    ///     下一个
    /// </summary>
    public static RoutedCommand Next { get; } = new RoutedCommand(nameof(Next), typeof(Commands));

    /// <summary>
    ///     第一个
    /// </summary>
    public static RoutedCommand First { get; } = new RoutedCommand(nameof(First), typeof(Commands));

    /// <summary>
    ///     最后一个
    /// </summary>
    public static RoutedCommand Last { get; } = new RoutedCommand(nameof(Last), typeof(Commands));

    /// <summary>
    ///     上午
    /// </summary>
    public static RoutedCommand Am { get; } = new RoutedCommand(nameof(Am), typeof(Commands));

    /// <summary>
    ///     下午
    /// </summary>
    public static RoutedCommand Pm { get; } = new RoutedCommand(nameof(Pm), typeof(Commands));

    /// <summary>
    ///     确认
    /// </summary>
    public static RoutedCommand Sure { get; } = new RoutedCommand(nameof(Sure), typeof(Commands));

    /// <summary>
    ///     小时改变
    /// </summary>
    public static RoutedCommand HourChange { get; } = new RoutedCommand(nameof(HourChange), typeof(Commands));

    /// <summary>
    ///     分钟改变
    /// </summary>
    public static RoutedCommand MinuteChange { get; } = new RoutedCommand(nameof(MinuteChange), typeof(Commands));

    /// <summary>
    ///     秒改变
    /// </summary>
    public static RoutedCommand SecondChange { get; } = new RoutedCommand(nameof(SecondChange), typeof(Commands));

    /// <summary>
    ///     鼠标移动
    /// </summary>
    public static RoutedCommand MouseMove { get; } = new RoutedCommand(nameof(MouseMove), typeof(Commands));

    /// <summary>
    /// 粘贴
    /// </summary>
    public static RoutedCommand Paste { get; } = new RoutedCommand(nameof(Paste), typeof(Commands));

    /// <summary>
    /// 全选
    /// </summary>
    public static RoutedCommand CheckAll { get; } = new RoutedCommand(nameof(CheckAll), typeof(Commands));

    /// <summary>
    /// 删除
    /// </summary>
    public static RoutedCommand Delete { get; } = new RoutedCommand(nameof(Delete), typeof(Commands));

    /// <summary>
    /// 全部删除
    /// </summary>
    public static RoutedCommand DeleteAll { get; } = new RoutedCommand(nameof(DeleteAll), typeof(Commands));

    /// <summary>
    /// 删除选中
    /// </summary>
    public static RoutedCommand DeleteAllChecked { get; } = new RoutedCommand(nameof(DeleteAllChecked), typeof(Commands));

    /// <summary>
    /// 编辑
    /// </summary>
    public static RoutedCommand Edit { get; } = new RoutedCommand(nameof(Edit), typeof(Commands));

    /// <summary>
    /// 查看
    /// </summary>
    public static RoutedCommand View { get; } = new RoutedCommand(nameof(View), typeof(Commands));

    /// <summary>
    /// 刷新
    /// </summary>
    public static RoutedCommand Refresh { get; } = new RoutedCommand(nameof(Refresh), typeof(Commands));

    public static void InvalidateRequerySuggested()
    {
        CommandManager.InvalidateRequerySuggested();
    }
}
