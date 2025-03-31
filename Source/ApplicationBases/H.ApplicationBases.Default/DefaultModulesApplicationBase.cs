using H.Extensions.ApplicationBase;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace H.ApplicationBases.Default
{
    public abstract partial class DefaultModulesApplicationBase : ApplicationBase
    {
        protected override void ConfigureServices(IServiceCollection services)
        {
            services.AddDefaultModules();
        }

        protected override void Configure(IApplicationBuilder app)
        {
            base.Configure(app);
            app.UseDefaultModules();
        }
    }
}
