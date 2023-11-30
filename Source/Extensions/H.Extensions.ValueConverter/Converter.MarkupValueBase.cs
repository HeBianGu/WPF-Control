using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;


namespace H.Extensions.ValueConverter
{
    public abstract class MarkupValueConverterBase : MarkupExtension, IValueConverter, INotifyPropertyChanged
    {
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        public virtual object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        public virtual void Refresh()
        {
            this.ConvertBack(null, null, null, null);
        }

        public object DefaultValue { get; set; } = DependencyProperty.UnsetValue;

        public object DefaultBackValue { get; set; } = DependencyProperty.UnsetValue;

        //  Do ：目前作用不大
        #region - INotifyPropertyChanged -

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

            this.Refresh();
        }

        #endregion
    }
}
