global using H.Iocable;
global using H.Services.Logger;
using H.Extensions.Mail;
using H.Services.Setting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace System;

public static class Extention
{
    /// <summary>
    /// 注册
    /// </summary>
    /// <param name="service"></param>
    public static IServiceCollection AddMail(this IServiceCollection services, Action<ISmtpSendOptions> setupAction = null)
    {
        services.AddOptions();
        services.TryAdd(ServiceDescriptor.Singleton<IMailService, MailService>());
        services.TryAdd(ServiceDescriptor.Singleton<ILogMailService, LogMailService>());
        if (setupAction != null)
            services.Configure(new Action<SmtpSendOptions>(setupAction));
        return services;
    }

    public static IApplicationBuilder UseMailOptions(this IApplicationBuilder builder, Action<ISmtpSendOptions> option = null)
    {
        IocSetting.Instance.Add(SmtpSendOptions.Instance);
        option?.Invoke(SmtpSendOptions.Instance);
        return builder;
    }
}
