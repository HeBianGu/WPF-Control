﻿// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows.Data;

namespace H.Controls.PropertyGrid
{
    public class SelectedObjectConverter : IValueConverter
    {
        private const string ValidParameterMessage = @"parameter must be one of the following strings: 'Type', 'TypeName', 'SelectedObjectName'";
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null)
                throw new ArgumentNullException("parameter");

            if (!(parameter is string))
                throw new ArgumentException(SelectedObjectConverter.ValidParameterMessage);

            if (this.CompareParam(parameter, "Type"))
            {
                return this.ConvertToType(value, culture);
            }
            else if (this.CompareParam(parameter, "TypeName"))
            {
                return this.ConvertToTypeName(value, culture);
            }
            else if (this.CompareParam(parameter, "SelectedObjectName"))
            {
                return this.ConvertToSelectedObjectName(value, culture);
            }
            else
            {
                throw new ArgumentException(SelectedObjectConverter.ValidParameterMessage);
            }
        }

        private bool CompareParam(object parameter, string parameterValue)
        {
            return string.Compare((string)parameter, parameterValue, true) == 0;
        }

        private object ConvertToType(object value, CultureInfo culture)
        {
            return (value != null)
              ? value.GetType()
              : null;
        }

        private object ConvertToTypeName(object value, CultureInfo culture)
        {
            if (value == null)
                return string.Empty;

            Type newType = value.GetType();

            //ICustomTypeProvider is only available in .net 4.5 and over. Use reflection so the .net 4.0 and .net 3.5 still works.
            if (newType.GetInterface("ICustomTypeProvider", true) != null)
            {
                MethodInfo methodInfo = newType.GetMethod("GetCustomType");
                newType = methodInfo.Invoke(value, null) as Type;
            }

            DisplayNameAttribute displayNameAttribute = newType.GetCustomAttributes(false).OfType<DisplayNameAttribute>().FirstOrDefault();

            return (displayNameAttribute == null)
              ? newType.Name
              : displayNameAttribute.DisplayName;
        }

        private object ConvertToSelectedObjectName(object value, CultureInfo culture)
        {
            if (value == null)
                return String.Empty;

            Type newType = value.GetType();
            PropertyInfo[] properties = newType.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.Name == "Name")
                    return property.GetValue(value, null);
            }

            return String.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
