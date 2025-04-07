using System.Windows.Media;

namespace H.Modules.Theme;

public interface IIconFontFamilysOptions
{
    FontFamily IconFontFamily { get; set; }
    List<FontFamily> IconFontFamilys { get; }
}