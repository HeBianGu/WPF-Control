// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Modules.Help.WebSite;
using H.Services.Serializable.Web;
using H.Services.Setting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace H.Modules.Help.Website;

public static class Extention
{
    /// <summary>
    /// 注册
    /// </summary>
    /// <param name="service"></param>
    public static void AddWebsite(this IServiceCollection services, Action<IWebsiteOptions> setupAction = null)
    {
        services.AddOptions();
        services.TryAdd(ServiceDescriptor.Singleton<IWebsiteService, WebsiteService>());
        if (setupAction != null)
            services.Configure(setupAction);
    }

    /// <summary>
    /// 配置
    /// </summary>
    /// <param name="service"></param>
    public static void UseWebsite(this IApplicationBuilder service, Action<IWebsiteOptions> action = null)
    {
        action?.Invoke(WebsiteOptions.Instance);
        IocSetting.Instance.Add(WebsiteOptions.Instance);
    }
}
