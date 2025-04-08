using H.ApplicationBases.Themes;
using H.Extensions.Common;
using H.Extensions.FontIcon;
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
        public static void AddDefaultThemeServices(this IServiceCollection services, Action<IDefaultThemeOptions> option = null)
        {
            DefaultThemeOptions opt = new DefaultThemeOptions();
            option?.Invoke(opt);
            //services.AddSwitchThemeViewPresenter(opt.GetConfigOptions<Action<ISwitchThemeOptions>>());
            //services.AddLoadThemeOptionsService(opt.GetConfigOptions<Action<IThemeOptions>>());
            services.AddTheme(opt.GetConfigOptions<Action<IThemeOptions>>());
            services.AddColorThemeViewPresenter(opt.GetConfigOptions<Action<IColorThemeOptions>>());
        }

        public static void UseDefaultThemeOptions(this IApplicationBuilder app, Action<IDefaultThemeOptions> option = null)
        {
            DefaultThemeOptions opt = new DefaultThemeOptions();
            option?.Invoke(opt);
            app.UseDefaultColorResources(opt.GetConfigOptions<Action<IColorThemeOptions>>());
            app.UseDefaultIconFontFamilys(opt.GetConfigOptions<Action<IIconFontFamilysOptions>>());
        }

        public static void UseDefaultColorResources(this IApplicationBuilder app, Action<IColorThemeOptions> option = null)
        {
            app.UseThemeOptions(x =>
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

        public static void UseDefaultIconFontFamilys(this IApplicationBuilder app, Action<IIconFontFamilysOptions> option = null)
        {
            app.UseThemeOptions(x =>
            {
                x.IconFontFamilys.Add(IconFontFamilys.SystemSegoeMDL2Asset);
                x.IconFontFamilys.Add(IconFontFamilys.SystemSegoeFluentIcons);
                x.IconFontFamilys.Add(IconFontFamilys.LocationSegoeMDL2Asset);
                x.IconFontFamilys.Add(IconFontFamilys.locationSegoeFluentIcons);
                option?.Invoke(x);
            });
        }
    }
}
