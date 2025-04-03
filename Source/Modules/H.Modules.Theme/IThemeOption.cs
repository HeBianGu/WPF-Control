using H.Common.Interfaces;
using H.Services.Common.Theme;
using H.Themes.Default.Colors;
using H.Themes.Default.Extensions;
using System.Reflection;
using System.Windows.Media;

namespace H.Modules.Theme;
public interface IThemeOption
{
    IColorResource ColorResource { get; set; }
    List<IColorResource> ColorResources { get; }
    IBackgroundResource BackgroundResource { get; set; }
    List<IBackgroundResource> BackgroundResources { get; }
    FontFamily FontFamily { get; set; }
    List<FontFamily> FontFamilys { get; }
    FontSizeThemeType FontSize { get; set; }
    LayoutThemeType Layout { get; set; }
}