using H.Extensions.ApplicationBase;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace H.ApplicationBases.Modules
{
    public abstract partial class ModulesApplicationBase : ApplicationBase
    {
        protected override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);
            services.AddDefaultModuleServices();
        }

        protected override void Configure(IApplicationBuilder app)
        {
            base.Configure(app);
            app.UseDefaultModuleOptions();
        }
    }
}
