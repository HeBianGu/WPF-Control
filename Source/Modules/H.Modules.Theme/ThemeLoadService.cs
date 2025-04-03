using H.Services.Common.Theme;

namespace H.Modules.Theme;
public class ThemeLoadService : IThemeLoadService
{
    public bool Load(out string message)
    {
        return ThemeOptions.Instance.Load(out message);
    }
}
