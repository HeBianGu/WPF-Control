using H.Extensions.ApplicationBase;
using Microsoft.Extensions.DependencyInjection;

namespace H.Templates.Default;
public partial class App : ApplicationBase
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddApplicationServices();
    }

    protected override void Configure(IApplicationBuilder app)
    {
        app.UseApplicationOptions();
    }

    protected override Window CreateMainWindow(StartupEventArgs e)
    {
        return new MainWindow();
    }
}
