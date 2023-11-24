using System;
using System.Windows;
using System.Windows.Markup;

namespace H.Themes.Colors.Accent
{
    public class AccentLightThemeExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return new AccentLightColorResource().Resource;
        }
    }
}
