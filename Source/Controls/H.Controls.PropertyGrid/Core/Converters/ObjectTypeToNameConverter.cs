// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;
using System.Windows.Data;

namespace H.Controls.PropertyGrid
{
    public class ObjectTypeToNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                string valueString = value.ToString();
                if (string.IsNullOrEmpty(valueString)
                 || (valueString == value.GetType().UnderlyingSystemType.ToString()))
                {
                    return value.GetType().Name;
                }
                return value;
            }
            return null;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
