using H.Extensions.ApplicationBase;
using H.Modules.Dependency;
using Microsoft.Extensions.DependencyInjection;

namespace H.Templates.Default;
public partial class App : ApplicationBase
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddApplicationServices();
        services.AddDependency(x=>
        {
            x.DependencyItems.Add(DependencyItems.WPFControl);
        });
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
