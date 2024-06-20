
using H.Extensions.ValueConverter;
using System;
using System.Globalization;

namespace H.Extensions.Unit
{
    public abstract class UnitableValueConverterBase : MarkupValueConverterBase
    {

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return GetUnitable().ToString(value);
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return GetUnitable().Parse(value?.ToString());
        }

        protected abstract IUnitable GetUnitable();
    }
}

