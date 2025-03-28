using H.Extensions.ApplicationBase;
using H.Services.Message.Dialog;
using H.Services.Message.Form;
using H.Themes.Colors.Accent;
using H.Themes.Colors.Blue;
using H.Themes.Colors.Gray;
using H.Themes.Colors.Purple;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Windows.Markup;

namespace H.ApplicationBases.Default
{
    public abstract partial class DefaultMessageApplicationBase : ApplicationBase
    {
        protected override void ConfigureServices(IServiceCollection services)
        {
            services.AddDefaultMessages();
        }
    }

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
