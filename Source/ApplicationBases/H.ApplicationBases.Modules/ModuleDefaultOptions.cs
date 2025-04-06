using H.Modules.About;
using H.Modules.Guide;
using H.Modules.Help.Contact;
using H.Modules.Help.ReleaseVersions;
using H.Modules.Help.Support;
using H.Modules.Help.WebSite;
using H.Modules.Setting;
using H.Modules.SplashScreen;

namespace H.ApplicationBases.Module
{

    public class ModuleDefaultOptions : IModuleDefaultOptions
    {
        public Action<IAboutOptions> AboutOptions { get;private  set; }

        public Action<IGuideOptions> GuideOptions { get; private set; }

        public Action<ISplashScreenOptions> SplashScreenOptions { get; private set; }

        public Action<ISettingViewOptions> SettingViewOptions { get; private set; }

        public Action<IReleaseVersionsOptions> ReleaseVersionsOptions { get; private set; }

        public Action<ISupportOptions> SupportOptions { get; private set; }

        public Action<IWebsiteOptions> WebsiteOptions { get; private set; }

        public Action<IContactOptions> ContactOptions { get; private set; }

        public void UseAbout(Action<IAboutOptions> action)
        {
            this.AboutOptions = action;
        }

        public void UseContactOptions(Action<IContactOptions> action)
        {
            this.ContactOptions = action;
        }

        public void UseGuideOptions(Action<IGuideOptions> action)
        {
            this.GuideOptions = action;
        }

        public void UseReleaseVersionsOptionsAbout(Action<IReleaseVersionsOptions> action)
        {
            this.ReleaseVersionsOptions = action;
        }

        public void UseSettingViewOptions(Action<ISettingViewOptions> action)
        {
            this.SettingViewOptions = action;
        }

        public void UseSplashScreenOptions(Action<ISplashScreenOptions> action)
        {
            this.SplashScreenOptions = action;
        }

        public void UseSupportOptions(Action<ISupportOptions> action)
        {
            this.SupportOptions = action;
        }

        public void UseWebsiteOptions(Action<IWebsiteOptions> action)
        {
            this.WebsiteOptions = action;
        }
    }
}
