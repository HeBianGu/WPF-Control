using H.Extensions.ApplicationBase;
using H.Modules.Messages.Dialog;
using H.Modules.Messages.Form;
using H.Services.Common;
using H.Styles;
using H.Styles.Default;
using H.Themes.Colors.Accent;
using H.Themes.Colors.Gray;
using H.Themes.Colors.Purple;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace H.NugetTemplates.Default;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : ApplicationBase
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddAbout();
        services.AddAdornerDialogMessage();
        //services.AddWindowDialogMessage();
        services.AddSetting();
        services.AddSwitchThemeViewPresenter(x =>
        {
            x.Dark = new GrayDarkColorResource();
        });
        services.AddFormMessageService();
        services.AddNoticeMessage();
        services.AddSnackMessage();
    }

    protected override void Configure(IApplicationBuilder app)
    {
        //app.UseSetting(x =>
        //{
        //    x.Add(LoginSetting.Instance);
        //});
        //app.UseSettingDefault();
        app.UseTheme(x =>
        {
            x.ColorResources.Add(new PurpleDarkColorResource());
            x.ColorResources.Add(new PurpleLightColorResource());
            x.ColorResources.Add(new GrayDarkColorResource());
            x.ColorResources.Add(new GrayLightColorResource());
            //x.ColorResources.Add(new BlueDarkColorResource());
            //x.ColorResources.Add(new BlueLightColorResource());
            x.ColorResources.Add(new AccentLightColorResource());
            x.ColorResources.Add(new AccentDarkColorResource());
        });

        app.UseWindowSetting();
        app.UseAbout();
        app.UseSwithTheme();
        app.UseSettingSecurity();
        app.UseMainWindowSetting();
        app.UseAdorner();
        app.UseWindowSetting();
    }

    protected override Window CreateMainWindow(StartupEventArgs e)
    {
        return new MainWindow();
    }
}
