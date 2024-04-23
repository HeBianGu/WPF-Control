// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Windows.Input;

namespace H.Controls.PropertyGrid
{
    public static class CalculatorCommands
    {
        private static RoutedCommand _calculatorButtonClickCommand = new RoutedCommand();

        public static RoutedCommand CalculatorButtonClick
        {
            get
            {
                return _calculatorButtonClickCommand;
            }
        }
    }
}
