using System;
using System.Windows.Markup;
using System.Windows.Media;

namespace H.Styles.Default
{
    public class LogoExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            ImageSourceConverter converter = new ImageSourceConverter();
            string path = "pack://application:,,,/H.Styles.Default;component/Logo.png";
            return converter.ConvertFromString(path);
        }
    }
}
