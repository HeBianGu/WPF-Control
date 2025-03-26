global using H.Mvvm.Commands;
global using System.Windows.Input;

namespace H.Controls.PDF
{
    public class PDFBoxCommands
    {
        public static RoutedUICommand Open { get; } = new RoutedUICommand("打开", nameof(Open), typeof(Commands));
        public static RoutedUICommand SaveAsImages { get; } = new RoutedUICommand("另存为图片", nameof(SaveAsImages), typeof(Commands));
        public static RoutedUICommand RenderToMemory { get; } = new RoutedUICommand("加载数据", nameof(RenderToMemory), typeof(Commands));
        public static RoutedUICommand ShowBookmarks { get; } = new RoutedUICommand("显示标签", nameof(ShowBookmarks), typeof(Commands));
        public static RoutedUICommand Searchterm { get; } = new RoutedUICommand("搜索", nameof(Searchterm), typeof(Commands));
        public static RoutedUICommand HandTool { get; } = new RoutedUICommand("手型工具", nameof(HandTool), typeof(Commands));
        public static RoutedUICommand ToRight { get; } = new RoutedUICommand("居右", nameof(ToRight), typeof(Commands));
        public static RoutedUICommand ToLeft { get; } = new RoutedUICommand("居左", nameof(ToLeft), typeof(Commands));
        public static RoutedUICommand Previous { get; } = new RoutedUICommand("上一页", nameof(Previous), typeof(Commands));
        public static RoutedUICommand Next { get; } = new RoutedUICommand("下一页", nameof(Next), typeof(Commands));
        public static RoutedUICommand ZoomIn { get; } = new RoutedUICommand("放大", nameof(ZoomIn), typeof(Commands));
        public static RoutedUICommand ZoomOut { get; } = new RoutedUICommand("缩小", nameof(ZoomOut), typeof(Commands));
        public static RoutedUICommand SinglePage { get; } = new RoutedUICommand("单页", nameof(SinglePage), typeof(Commands));
        public static RoutedUICommand TwoPage { get; } = new RoutedUICommand("两页", nameof(TwoPage), typeof(Commands));
        public static RoutedUICommand ContinuousPage { get; } = new RoutedUICommand("连续页", nameof(ContinuousPage), typeof(Commands));
        public static RoutedUICommand FitToWidth { get; } = new RoutedUICommand("适应宽度", nameof(FitToWidth), typeof(Commands));
        public static RoutedUICommand FitToHeight { get; } = new RoutedUICommand("适应高度", nameof(FitToHeight), typeof(Commands));

        public static RoutedUICommand RotateToRight { get; } = new RoutedUICommand("右转", nameof(RotateToRight), typeof(Commands));

        public static RoutedUICommand RotateToLeft { get; } = new RoutedUICommand("左转", nameof(RotateToLeft), typeof(Commands));

        public static RoutedUICommand Transparent { get; } = new RoutedUICommand("背景透明", nameof(Transparent), typeof(Commands));

        public static RoutedUICommand ShowPageText { get; } = new RoutedUICommand("页面文本", nameof(ShowPageText), typeof(Commands));
        public static RoutedUICommand ShowInfo { get; } = new RoutedUICommand("页面信息", nameof(ShowInfo), typeof(Commands));
        public static RoutedUICommand FindPrevious { get; } = new RoutedUICommand("上一个", nameof(FindPrevious), typeof(Commands));

        public static RoutedUICommand FindNext { get; } = new RoutedUICommand("下一个", nameof(FindNext), typeof(Commands));

        public static RoutedUICommand Close { get; } = new RoutedUICommand("关闭", nameof(FindNext), typeof(Commands));
    }
}
