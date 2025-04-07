using H.Modules.Theme;

namespace H.ApplicationBases.Themes
{
    public interface IDefaultThemeOptions
    {
        void UseColorThemeOptions(Action<IColorThemeOptions> action);
        void UseIconFontFamilysOptions(Action<IIconFontFamilysOptions> action);
        void UseThemeOptions(Action<IThemeOptions> action);
    }
}
