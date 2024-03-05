// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;


namespace H.Controls.PropertyGrid
{
    public class NewItemTypesComboBoxConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {

            if (values.Length != 2)
                throw new ArgumentException("The 'values' argument should contain 2 objects.");

            if (values[1] != null)
            {
                if (!values[1].GetType().IsGenericType || !(values[1].GetType().GetGenericArguments().First().GetType() is Type))
                    throw new ArgumentException("The 'value' argument is not of the correct type.");

                return values[1];
            }
            else if (values[0] != null)
            {
                if (!(values[0].GetType() is Type))
                    throw new ArgumentException("The 'value' argument is not of the correct type.");

                List<Type> types = new List<Type>();
                Type listType = ListUtilities.GetListItemType((Type)values[0]);
                if (listType != null)
                {
                    types.Add(listType);
                }

                return types;
            }

            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
