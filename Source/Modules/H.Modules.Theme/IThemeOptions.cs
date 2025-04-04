using H.Common.Interfaces;
using H.Services.Common.Theme;
using H.Theme.Colors;
using H.Theme.Extensions;
using System.Reflection;
using System.Windows.Media;

namespace H.Modules.Theme;
public interface IThemeOptions
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