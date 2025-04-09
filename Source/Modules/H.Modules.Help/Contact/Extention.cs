// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Services.Serializable.Web;
using H.Services.Setting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace H.Modules.Help.Contact;

public static class Extention
{
    /// <summary>
    /// 注册
    /// </summary>
    /// <param name="service"></param>
    public static void AddContact(this IServiceCollection services, Action<IContactOptions> setupAction = null)
    {
        services.AddOptions();
        services.TryAdd(ServiceDescriptor.Singleton<IContactService, ContactService>());
        if (setupAction != null)
            services.Configure(setupAction);
    }

    /// <summary>
    /// 配置
    /// </summary>
    /// <param name="service"></param>
    public static void UseContact(this IApplicationBuilder service, Action<IContactOptions> action = null)
    {
        action?.Invoke(ContactOptions.Instance);
        IocSetting.Instance.Add(ContactOptions.Instance);
    }
}
