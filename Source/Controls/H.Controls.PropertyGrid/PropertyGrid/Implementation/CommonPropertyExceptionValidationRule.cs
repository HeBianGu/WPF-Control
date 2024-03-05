// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Controls;

namespace H.Controls.PropertyGrid
{
    internal class CommonPropertyExceptionValidationRule : ValidationRule
    {
        private TypeConverter _propertyTypeConverter;
        private Type _type;

        internal CommonPropertyExceptionValidationRule(Type type)
        {
            _propertyTypeConverter = TypeDescriptor.GetConverter(type);
            _type = type;
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            ValidationResult result = new ValidationResult(true, null);

            if (GeneralUtilities.CanConvertValue(value, _type))
            {
                try
                {
                    _propertyTypeConverter.ConvertFrom(value);
                }
                catch (Exception e)
                {
                    // Will display a red border in propertyGrid
                    result = new ValidationResult(false, e.Message);
                }
            }
            return result;
        }
    }
}
