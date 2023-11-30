// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace H.Extensions.ValueConverter
{
    /// <summary>
    /// 并运算
    /// </summary>
    public class BoolAndConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null)
                return null;
            List<bool> result = values.OfType<bool>()?.ToList();

            return result.TrueForAll(l => l == true);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    /// <summary>
    /// 或运算
    /// </summary>
    public class BoolOrConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null) return null;

            List<bool> result = values.OfType<bool>()?.ToList();

            return result.Any(l => l == true);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 区间范围筛选
    /// </summary>
    public class MultiComboboxSelectConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null) return null;

            if (values.Length != 2) return null;

            if (values[1] == null) return null;

            int selectIndex = (int)values[0];

            IEnumerable<object> enumerable = values[1] as IEnumerable<object>;

            return enumerable.Skip(selectIndex).ToList();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
