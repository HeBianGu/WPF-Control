// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;

namespace H.Controls.PropertyGrid
{
    public class IsDefaultCategoryConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string categoryName = value as string;
            if (categoryName != null)
            {
                return categoryName == CategoryAttribute.Default.Category;
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
