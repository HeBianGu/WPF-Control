using System;
using System.Windows;
using System.Windows.Markup;

namespace H.Styles.Default
{
    public class DefaultStyleThemeExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return new ResourceDictionary()
            {
                Source = new Uri("pack://application:,,,/H.Styles.Default;component/ConciseControls.xaml")
            }; ;
        }
    }
}
