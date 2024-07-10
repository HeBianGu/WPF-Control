global using H.Iocable;
global using H.Services.Logger;
using H.Extensions.Mail;
using H.Services.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace System
{
    public static class Extention
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static IServiceCollection AddMail(this IServiceCollection services, Action<SmtpSendOptions> setupAction = null)
        {
            services.AddOptions();
            services.TryAdd(ServiceDescriptor.Singleton<IMailService, MailService>());
            services.TryAdd(ServiceDescriptor.Singleton<IMailLogService, MailLogService>());
            if (setupAction != null)
                services.Configure(setupAction);
            return services;
        }

        public static IApplicationBuilder UseMail(this IApplicationBuilder builder, Action<SmtpSendOptions> option = null)
        {
            SettingDataManager.Instance.Add(SmtpSendOptions.Instance);
            option?.Invoke(SmtpSendOptions.Instance);
            return builder;
        }
    }
}
