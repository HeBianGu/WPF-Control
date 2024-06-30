using H.Themes.Default;
using System;
using System.Windows.Markup;
using System.Windows.Media;

namespace H.Styles.Default
{

    public class LogoExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return LogoDataProvider.Logo;
        }
    }

    public class LogoDataProvider
    {
        public static ImageSource Logo
        {
            get
            {
                ImageSourceConverter converter = new ImageSourceConverter();
                string path = "pack://application:,,,/H.Styles.Default;component/Logo.ico";
                return converter.ConvertFromString(path) as ImageSource;
            }
        }


    }
}
