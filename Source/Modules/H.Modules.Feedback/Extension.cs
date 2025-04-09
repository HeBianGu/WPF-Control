using H.Modules.Feedback;
using H.Services.Common.Feedback;
using H.Services.Setting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace System;

public static class Extension
{
    public static IServiceCollection AddFeedBack(this IServiceCollection services, Action<IFeedbackOptions> setupAction = null)
    {
        services.AddOptions();
        services.TryAdd(ServiceDescriptor.Singleton<IFeedbackViewPresenter, FeedbackViewPresenter>());
        services.TryAdd(ServiceDescriptor.Singleton<IFeedBackMailService, FeedBackMailService>());
        if (setupAction != null)
            services.Configure(new Action<FeedbackOptions>(setupAction));
        return services;
    }

    public static IApplicationBuilder UseFeedBackOptions(this IApplicationBuilder builder, Action<IFeedbackOptions> option = null)
    {
        IocSetting.Instance.Add(FeedbackOptions.Instance);
        option?.Invoke(FeedbackOptions.Instance);
        return builder;
    }
}
