using H.Themes.Default.Colors;
using H.Themes.Default.Extensions;
using System.Reflection;
using System.Windows.Media;

namespace H.Modules.Theme;
public interface IThemeSetting
{
    IColorResource ColorResource { get; set; }
    List<IColorResource> ColorResources { get; }
    int ColorResourceSelectedIndex { get; set; }
    FontFamily FontFamily { get; set; }
    List<FontFamily> FontFamilys { get; }
    FontSizeThemeType FontSize { get; set; }
    LayoutThemeType Layout { get; set; }
}