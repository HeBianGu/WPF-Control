using H.ApplicationBases.Module;
using H.Extensions.FontIcon;
using H.Modules.Help.Contact;
using H.Modules.Help.ReleaseVersions;
using H.Modules.Help.Support;
using H.Modules.Help.Website;
using H.Styles;
using Microsoft.Extensions.DependencyInjection;

namespace System
{
    public static class Extention
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddModulesDefault(this IServiceCollection services, Action<IModuleDefaultOptions> options = null)
        {
            ModuleDefaultOptions mo = new ModuleDefaultOptions();
            options?.Invoke(mo);
            services.AddAbout(mo.AboutOptions);
            services.AddGuide(mo.GuideOptions);
            services.AddSplashScreen(mo.SplashScreenOptions);
            services.AddSetting(mo.SettingViewOptions);
            services.AddReleaseVersions(mo.ReleaseVersionsOptions);
            services.AddSupport(mo.SupportOptions);
            services.AddWebsite(mo.WebsiteOptions);
            services.AddSponsor();
            services.AddContact(mo.ContactOptions);
        }

        public static void UseModulesDefault(this IApplicationBuilder app, Action<IModuleDefaultOptions> options = null)
        {
            ModuleDefaultOptions mo = new ModuleDefaultOptions();
            options?.Invoke(mo);
            app.UseAbout(mo.AboutOptions);
            app.UseSplashScreen(mo.SplashScreenOptions);
            app.UseGuide(mo.GuideOptions);
            app.UseSettingView(mo.SettingViewOptions);
            //  ToDo：带加入
            app.UseSettingSecurity();
            app.UseReleaseVersions(mo.ReleaseVersionsOptions);
            app.UseSupport(mo.SupportOptions);
            app.UseWebsite(mo.WebsiteOptions);
            app.UseContact(mo.ContactOptions);
        }
    }
}
