using System.Windows.Media;

namespace H.Styles;

public class LogoDataProvider
{
    public static ImageSource Logo
    {
        get
        {
            ImageSourceConverter converter = new ImageSourceConverter();
            string path = "pack://application:,,,/H.Style;component/Logo.ico";
            return converter.ConvertFromString(path) as ImageSource;
        }
    }
}
