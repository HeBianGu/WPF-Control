using System;
using System.Windows;
using System.Windows.Markup;

namespace H.Themes.Colors.Gray
{
    public class PurpleDarkThemeExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return new GrayDarkColorResource().Resource;
        }
    }
}
