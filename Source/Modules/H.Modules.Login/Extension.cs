using H.Modules.Login;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class Extension
    {
        public static  IServiceCollection AddLogin(this IServiceCollection services, Action<LoginOptions> setupAction = null)
        {
            services.AddOptions();
            services.TryAdd(ServiceDescriptor.Singleton<ILoginViewPresenter, LoginViewPresenter>());
            if (setupAction != null)
                services.Configure(setupAction);
            return services;
        }
    }
}
