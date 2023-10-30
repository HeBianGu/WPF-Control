// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

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
