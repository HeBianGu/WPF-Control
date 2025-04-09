using H.Extensions.ApplicationBase;
using H.Modules.About;
using H.Modules.Guide;
using H.Modules.Help.Contact;
using H.Modules.Help.ReleaseVersions;
using H.Modules.Help.Support;
using H.Modules.Help.WebSite;
using H.Modules.Setting;
using H.Modules.SplashScreen;
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
    public abstract partial class DefaultApplicationBase : ApplicationBase
    {
        protected override void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationServices();
        }

        protected override void Configure(IApplicationBuilder app)
        {
            base.Configure(app);
            app.UseApplicationOptions();
        }
    }
}
