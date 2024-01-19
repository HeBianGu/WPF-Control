using H.Providers.Mvvm;
using System.Windows.Input;

namespace H.Controls.ScheduleBox
{
    public class ScheduleCommands
    {
        public static RoutedUICommand Start { get; } = new RoutedUICommand("启动", nameof(Start), typeof(Commands));
        public static RoutedUICommand Add { get; } = new RoutedUICommand("添加工作", nameof(Add), typeof(Commands));


    }
}
