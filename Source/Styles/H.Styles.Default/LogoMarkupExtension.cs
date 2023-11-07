using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Windows.Media;

namespace H.Styles.Default
{
    public class LogoExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var converter = new ImageSourceConverter();
            string path = "pack://application:,,,/H.Styles.Default;component/Logo.ico";
            return converter.ConvertFromString(path);
        }
    }
}
