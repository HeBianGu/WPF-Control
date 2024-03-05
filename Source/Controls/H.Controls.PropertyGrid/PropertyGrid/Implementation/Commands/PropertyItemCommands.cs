// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Windows.Input;

namespace H.Controls.PropertyGrid
{
    public static class PropertyItemCommands
    {
        private static RoutedCommand _resetValueCommand = new RoutedCommand();
        public static RoutedCommand ResetValue
        {
            get
            {
                return _resetValueCommand;
            }
        }
    }
}
