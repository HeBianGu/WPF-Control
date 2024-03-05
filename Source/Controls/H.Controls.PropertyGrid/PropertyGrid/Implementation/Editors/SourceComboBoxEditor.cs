// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;

namespace H.Controls.PropertyGrid
{
    public class SourceComboBoxEditor : ComboBoxEditor
    {
        private ICollection _collection;
        private TypeConverter _typeConverter;

        public SourceComboBoxEditor(ICollection collection, TypeConverter typeConverter)
        {
            _collection = collection;
            _typeConverter = typeConverter;
        }

        protected override IEnumerable CreateItemsSource(PropertyItem propertyItem)
        {
            return _collection;
        }

        protected override IValueConverter CreateValueConverter()
        {
            //When using a stringConverter, we need to convert the value
            if ((_typeConverter != null) && (_typeConverter is StringConverter))
                return new SourceComboBoxEditorConverter(_typeConverter);
            return null;
        }
    }

    internal class SourceComboBoxEditorConverter : IValueConverter
    {
        private TypeConverter _typeConverter;

        internal SourceComboBoxEditorConverter(TypeConverter typeConverter)
        {
            _typeConverter = typeConverter;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (_typeConverter != null)
            {
                if (_typeConverter.CanConvertTo(typeof(string)))
                    return _typeConverter.ConvertTo(value, typeof(string));
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (_typeConverter != null)
            {
                if (_typeConverter.CanConvertFrom(value.GetType()))
                    return _typeConverter.ConvertFrom(value);
            }
            return value;
        }
    }
}
