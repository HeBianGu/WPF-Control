using H.Modules.About;
using H.Modules.Theme;
using H.Services.Common.About;
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
        public static void AddDefaultModules(this IServiceCollection services)
        {
            services.AddDefaultMessages();
            services.AddAbout();
            services.AddGuide();
            services.AddSplashScreen();
            services.AddSetting();
            services.AddSwitchThemeViewPresenter(x =>
            {
                x.IsDark = false;
                x.Dark = new GrayDarkColorResource();
            });
            services.AddThemeLoadService();
            services.AddColorThemeViewPresenter();
        }

        public static void AddDefaultMessages(this IServiceCollection services)
        {
            services.AddAdornerDialogMessage();
            //services.AddWindowDialogMessage();
            services.AddWindowMessage();
            services.AddFormMessageService();
            services.AddNoticeMessage();
            services.AddSnackMessage();
        }


        public static void UseDefaultModules(this IApplicationBuilder app)
        {
            app.UseStyle();
            app.UseSettingSecurity();
            app.UseMainWindowSetting();
            app.UseWindowSetting();
        }

        public static void UseAllThemes(this IApplicationBuilder app, Action<ThemeOptions> option = null)
        {
            app.UseTheme(x =>
            {
                x.ColorResources.Add(new PurpleDarkColorResource());
                x.ColorResources.Add(new PurpleLightColorResource());
                x.ColorResources.Add(new GrayDarkColorResource());
                x.ColorResources.Add(new GrayLightColorResource());
                x.ColorResources.Add(new BlueDarkColorResource());
                x.ColorResources.Add(new BlueLightColorResource());
                x.ColorResources.Add(new AccentLightColorResource());
                x.ColorResources.Add(new AccentDarkColorResource());
                x.ColorResources.Add(new CopperColorResource());
                x.ColorResources.Add(new VintageFilmColorResource());
                x.ColorResources.Add(new CyberpunkColorResource());
                x.ColorResources.Add(new MineralColorResource());
                x.ColorResources.Add(new FuturismColorResource());
                x.ColorResources.Add(new TechnologyBlueDarkColorResource());
                x.ColorResources.Add(new TechnologyPurpleColorResource());
                x.ColorResources.Add(new FuturisticGreenDarkColorResource());
                x.ColorResources.Add(new AmberTerminalDarkColorResource());
                x.ColorResources.Add(new IndustrialDarkColorResource());
                x.ColorResources.Add(new AntDesignProColorResource());
                x.ColorResources.Add(new BootstrapColorResource());
                x.ColorResources.Add(new LayUIColorResource());
                x.ColorResources.Add(new WeUIColorResource());
                x.ColorResources.Add(new ColorUIGAColorResource());
                x.ColorResources.Add(new FluentUIColorResource());
                x.ColorResources.Add(new MaterialDesignColorResource());
                x.ColorResources.Add(new AppleColorResource());
                x.ColorResources.Add(new OracleDarkColorResource());
                x.ColorResources.Add(new VikingDarkColorResource());
                x.ColorResources.Add(new ChambrayDarkColorResource());
                x.ColorResources.Add(new TechnologyCyanColorResource());
                x.ColorResources.Add(new TechnologyIndigoColorResource());
                x.ColorResources.Add(new TechnologySOIORColorResource());
                x.ColorResources.Add(new TechnologyPinkColorResource());
                //x.ColorResources.Add(new IndustrialDarkColorResource());
                //x.ColorResources.Add(new IndustrialDarkColorResource());
                //x.ColorResources.Add(new IndustrialDarkColorResource());
                //x.ColorResources.Add(new IndustrialDarkColorResource());
                option?.Invoke(x);

            });
        }
    }
}
