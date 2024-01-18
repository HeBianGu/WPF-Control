using System;
using System.ComponentModel;
using System.Globalization;


namespace H.Extensions.Unit
{
    public abstract class UnitableTypeConverterBase : System.ComponentModel.TypeConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            return GetUnitable().ToString(value);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return GetUnitable().ToValue(value?.ToString());
        }

        protected abstract IUnitable GetUnitable();
    }
}

