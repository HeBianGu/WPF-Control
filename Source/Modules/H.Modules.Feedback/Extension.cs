using H.Services.Common.Feedback;
using H.Services.Setting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace H.Modules.Feedback;

public static class Extension
{
    public static IServiceCollection AddFeedBack(this IServiceCollection services, Action<FeedbackOptions> setupAction = null)
    {
        services.AddOptions();
        services.TryAdd(ServiceDescriptor.Singleton<IFeedbackViewPresenter, FeedbackViewPresenter>());
        if (setupAction != null)
            services.Configure(setupAction);
        return services;
    }

    public static IApplicationBuilder UseFeedBack(this IApplicationBuilder builder, Action<FeedbackOptions> option = null)
    {
        IocSetting.Instance.Add(FeedbackOptions.Instance);
        option?.Invoke(FeedbackOptions.Instance);
        return builder;
    }
}
