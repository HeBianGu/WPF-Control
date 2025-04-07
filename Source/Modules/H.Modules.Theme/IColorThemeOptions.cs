using H.Themes.Colors;

namespace H.Modules.Theme;

public interface IColorThemeOptions
{
    IColorResource ColorResource { get; set; }
    List<IColorResource> ColorResources { get; }
}
