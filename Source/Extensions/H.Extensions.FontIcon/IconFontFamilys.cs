using System.IO;
using System.Windows;
using System.Windows.Media;

namespace H.Extensions.FontIcon;

public static class IconFontFamilys
{
    public static FontFamily SystemSegoeMDL2Asset => new FontFamily("Segoe MDL2 Assets");

    public static FontFamily SystemSegoeFluentIcons => new FontFamily("Segoe Fluent Icons");

    //public static FontFamily SegoeMDL2Asset => new FontFamily(new Uri("pack://application:,,,/H.Extensions.FontIcon;component/Assets/SegMDL2.ttf"), "Segoe MDL2 Assets");
    public static FontFamily LocationSegoeMDL2Asset
    {
        get
        {
            return Fonts.GetFontFamilies(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets/SegMDL2.ttf")).FirstOrDefault();
        }
    }

    public static FontFamily locationSegoeFluentIcons
    {
        get
        {
          return  Fonts.GetFontFamilies(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets/Segoe Fluent Icons.ttf")).FirstOrDefault();
        }
    }
}

