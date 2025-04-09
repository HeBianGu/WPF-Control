using H.ApplicationBases.Module;
using H.ApplicationBases.Themes;
using H.Extensions.ApplicationBase;

namespace H.ApplicationBases.Default
{
    public class DefaultApplicationOptions : CacheActionOptionsBase, IDefaultApplicationOptions
    {
        public void UseModulesOptions(Action<IDefaultModuleOptions> action)
        {
            this.ConfigOptions(action);
        }

        public void UseThemeModuleOptions(Action<IDefaultThemeOptions> action)
        {
            this.ConfigOptions(action);
        }
    }
}
