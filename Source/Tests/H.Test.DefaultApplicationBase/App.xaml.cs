using H.ApplicationBases.Default;
using H.Extensions.ApplicationBase;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;

namespace H.Test.DefaultApplicationBase;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : ApplicationBase
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);
        services.AddApplicationDefault(x =>
        {
            x.UseModuleDefaultOptions(x =>
            {
                x.UseAbout(x => x.ProductName = "Test");
                x.UseSplashScreenOptions(x => x.Product = "TTTTT");
            });
        });
    }
    protected override void Configure(IApplicationBuilder app)
    {
        base.Configure(app);
        app.UseApplicationDefault(x =>
        {
            x.UseModuleDefaultOptions(x =>
            {
                x.UseAbout(x => x.ProductName = "Test");
                x.UseSplashScreenOptions(x => x.Product = "TTTTT");
            });
        });
    }
    protected override Window CreateMainWindow(StartupEventArgs e)
    {
        return new MainWindow();
    }
}

