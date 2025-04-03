using H.Services.Common.Theme;

namespace H.Modules.Theme;
public class ThemeLoadService : IThemeLoadService
{
    public bool Load(out string message)
    {
        return ThemeOption.Instance.Load(out message);
    }
}
