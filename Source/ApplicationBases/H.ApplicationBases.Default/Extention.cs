using H.Modules.About;
using H.Services.Common.About;
using H.Themes.Colors.Accent;
using H.Themes.Colors.Blue;
using H.Themes.Colors.Gray;
using H.Themes.Colors.Purple;
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
            });
            app.UseSwithTheme();
        }
    }
}
