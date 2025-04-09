using H.Mvvm;
using H.Mvvm.Commands;
using System.Windows.Input;

namespace H.Controls.ScheduleBox
{
    public class ScheduleCommands
    {
        public static RoutedUICommand Start { get; } = new RoutedUICommand("启动", nameof(Start), typeof(Commands));
        public static RoutedUICommand Stop { get; } = new RoutedUICommand("停止", nameof(Stop), typeof(Commands));

        public static RoutedUICommand Add { get; } = new RoutedUICommand("添加任务", nameof(Add), typeof(Commands));
        public static RoutedUICommand Edit { get; } = new RoutedUICommand("编辑任务", nameof(Edit), typeof(Commands));
        public static RoutedUICommand Delete { get; } = new RoutedUICommand("删除任务", nameof(Delete), typeof(Commands));


    }
}
