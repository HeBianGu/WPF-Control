using H.ApplicationBases.Default;
using H.ApplicationBases.Module;
using H.ApplicationBases.Themes;
using H.Extensions.FontIcon;
using H.Modules.About;
using H.Modules.Help.Contact;
using H.Modules.Help.ReleaseVersions;
using H.Modules.Help.Support;
using H.Modules.Help.Website;
using H.Modules.Theme;
using H.Services.Common.About;
using H.Styles;
using H.Themes.Colors.Accent;
using H.Themes.Colors.Blue;
using H.Themes.Colors.Copper;
using H.Themes.Colors.Gray;
using H.Themes.Colors.Industrial;
using H.Themes.Colors.Mineral;
using H.Themes.Colors.Platform;
using H.Themes.Colors.Purple;
using H.Themes.Colors.Solid;
using H.Themes.Colors.Technology;
using H.Themes.Colors.Web;
using Microsoft.Extensions.DependencyInjection;

namespace System
{
    public static class Extention
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddApplicationServices(this IServiceCollection services, Action<IDefaultApplicationOptions> options = null)
        {
            DefaultApplicationOptions opt = new DefaultApplicationOptions();
            options?.Invoke(opt);
            services.AddDefaultMessages();
            services.AddDefaultModuleServices(opt.GetConfigOptions<Action<IDefaultModuleOptions>>());
            services.AddDefaultThemeServices(opt.GetConfigOptions<Action<IDefaultThemeOptions>>());

            services.AddLog4net();
        }

        public static void UseApplicationOptions(this IApplicationBuilder app, Action<IDefaultApplicationOptions> options = null)
        {
            DefaultApplicationOptions opt = new DefaultApplicationOptions();
            options?.Invoke(opt);
            app.UseStyleOptions();
            app.UseDefaultModuleOptions(opt.GetConfigOptions<Action<IDefaultModuleOptions>>());
            app.UseDefaultThemeOptions(opt.GetConfigOptions<Action<IDefaultThemeOptions>>());

            app.UseAddLog4netOptions();
        }
    }
}
