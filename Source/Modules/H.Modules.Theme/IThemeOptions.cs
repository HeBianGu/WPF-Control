using H.Common.Interfaces;
using H.Services.Common.Theme;
using H.Themes.Backgrounds;
using H.Themes.Extensions;
using System.Reflection;
using System.Windows.Media;

namespace H.Modules.Theme;
public interface IThemeOptions : IIconFontFamilysOptions, IColorThemeOptions
{
    IBackgroundResource BackgroundResource { get; set; }
    List<IBackgroundResource> BackgroundResources { get; }
    FontFamily FontFamily { get; set; }
    List<FontFamily> FontFamilys { get; }
    FontSizeThemeType FontSize { get; set; }
    LayoutThemeType Layout { get; set; }
}
