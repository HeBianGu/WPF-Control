// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control
using H.Modules.Sponsor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace System;

public static class Extention
{
    /// <summary>
    /// 注册
    /// </summary>
    /// <param name="service"></param>
    public static void AddSponsor(this IServiceCollection services)
    {
        services.Add(ServiceDescriptor.Singleton<ISponsorPresenter, SponsorPresenter>());
    }
}
