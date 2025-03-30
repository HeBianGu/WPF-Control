using H.Extensions.ApplicationBase;
using H.Modules.Setting;
using H.Services.Setting;
using H.Styles.Default;
using H.Styles.Default.Controls;
using H.Themes.Colors.Accent;
using H.Themes.Colors.Blue;
using H.Themes.Colors.Copper;
using H.Themes.Colors.Futurism;
using H.Themes.Colors.Gray;
using H.Themes.Colors.Industrial;
using H.Themes.Colors.Mineral;
using H.Themes.Colors.Purple;
using H.Themes.Colors.Technology;
using H.Themes.Colors.Web;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;

namespace H.Test.Theme
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : ApplicationBase
    {
        protected override void ConfigureServices(IServiceCollection services)
        {
            
            services.AddSetting();
            services.AddAdornerDialogMessage();
            services.AddSwitchThemeViewPresenter(x=>
            {
                x.Dark = new PurpleDarkColorResource();
            });
        }

        protected override Window CreateMainWindow(StartupEventArgs e)
        {
            return new MainWindow();
        }

        protected override void Configure(IApplicationBuilder app)
        {
            base.Configure(app);
            app.UseTheme(x=>
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
                x.ColorResources.Add(new FuturisticGreenDarkColorResource());
                x.ColorResources.Add(new IndustrialDarkColorResource());
                x.ColorResources.Add(new AntDesignProColorResource());
                x.ColorResources.Add(new BootstrapColorResource());
                x.ColorResources.Add(new LayUIColorResource());
                x.ColorResources.Add(new WeUIColorResource());
                x.ColorResources.Add(new ColorUIGAColorResource());
                //x.ColorResources.Add(new IndustrialDarkColorResource());
                //x.ColorResources.Add(new IndustrialDarkColorResource());
                //x.ColorResources.Add(new IndustrialDarkColorResource());
                //x.ColorResources.Add(new IndustrialDarkColorResource());
                //x.ColorResources.Add(new IndustrialDarkColorResource());
                //x.ColorResources.Add(new IndustrialDarkColorResource());
                //x.ColorResources.Add(new IndustrialDarkColorResource());
                //x.ColorResources.Add(new IndustrialDarkColorResource());
                //x.ColorResources.Add(new IndustrialDarkColorResource());
                //x.ColorResources.Add(new IndustrialDarkColorResource());
                //x.ColorResources.Add(new IndustrialDarkColorResource());
                //x.ColorResources.Add(new IndustrialDarkColorResource());
                //x.ColorResources.Add(new IndustrialDarkColorResource());
                //x.ColorResources.Add(new IndustrialDarkColorResource());
                //x.ColorResources.Add(new IndustrialDarkColorResource());
                x.ColorResources.Add(new AmberTerminalDarkColorResource());
                
            });

            app.UseSwithTheme();
            app.UseWindowSetting(x =>
            {
                x.BackImagePath = "pack://application:,,,/H.Extensions.BackgroundImage;component/b13.png";
            });
        }
    }
}
