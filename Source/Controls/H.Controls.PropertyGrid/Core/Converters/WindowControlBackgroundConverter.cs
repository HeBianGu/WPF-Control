// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace H.Controls.PropertyGrid
{
    public class WindowControlBackgroundConverter : IMultiValueConverter
    {
        /// <summary>
        /// Used in the WindowContainer Template to calculate the resulting background brush
        /// from the WindowBackground (values[0]) and WindowOpacity (values[1]) propreties.
        /// </summary>
        /// <param name="values"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            Brush backgroundColor = (Brush)values[0];
            double opacity = (double)values[1];

            if (backgroundColor != null)
            {
                // Do not override any possible opacity value specifically set by the user.
                // Only use WindowOpacity value if the user did not set an opacity first.
                if (backgroundColor.ReadLocalValue(Brush.OpacityProperty) == System.Windows.DependencyProperty.UnsetValue)
                {
                    backgroundColor = backgroundColor.Clone();
                    backgroundColor.Opacity = opacity;
                }
            }
            return backgroundColor;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
