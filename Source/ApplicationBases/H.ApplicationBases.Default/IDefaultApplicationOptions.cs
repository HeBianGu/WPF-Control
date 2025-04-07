using H.ApplicationBases.Module;
using H.ApplicationBases.Themes;

namespace H.ApplicationBases.Default
{
    public interface IDefaultApplicationOptions
    {
        void UseModulesOptions(Action<IDefaultModuleOptions> action);

        void UseThemeModuleOptions(Action<IDefaultThemeOptions> action);
    }
}
