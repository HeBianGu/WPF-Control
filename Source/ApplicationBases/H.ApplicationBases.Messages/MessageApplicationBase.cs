using H.Extensions.ApplicationBase;
using H.Services.Message.Dialog;
using H.Services.Message.Form;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Windows.Markup;

namespace H.ApplicationBases.Messages
{
    public abstract partial class MessageApplicationBase : ApplicationBase
    {
        protected override void ConfigureServices(IServiceCollection services)
        {
            services.AddDefaultMessages();
        }
    }
}
