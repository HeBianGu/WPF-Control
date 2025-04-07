using H.Extensions.ApplicationBase;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Windows.Markup;

namespace H.ApplicationBases.Identify
{
    public abstract partial class IdentifyApplicationBase : ApplicationBase
    {
        protected override void ConfigureServices(IServiceCollection services)
        {
            services.AddDefaultIdentify();
        }

        protected override void Configure(IApplicationBuilder app)
        {
            base.Configure(app);
            app.UseDefaultIdentifyOptions();
        }
    }
}
