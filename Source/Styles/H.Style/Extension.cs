using H.Services.Common.MainWindow;
using H.Styles.Controls;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace H.Styles;

public static partial class Extension
{
    public static IServiceCollection AddMainWindowSavableService(this IServiceCollection service, Action<MainWindowOption> setupAction = null)
    {
        service.AddOptions();
        service.TryAdd(ServiceDescriptor.Singleton<IMainWindowSavableService, MainWindowSavableService>());
        if (setupAction != null)
            service.Configure(setupAction);
        return service;
    }

    public static IApplicationBuilder UseMainWindowSetting(this IApplicationBuilder builder, Action<MainWindowOption> option = null)
    {
        IocSetting.Instance.Add(MainWindowOption.Instance);
        option?.Invoke(MainWindowOption.Instance);
        return builder;
    }

    public static IApplicationBuilder UseWindowSetting(this IApplicationBuilder builder, Action<WindowSetting> option = null)
    {
        option?.Invoke(WindowSetting.Instance);
        IocSetting.Instance.Add(WindowSetting.Instance);
        return builder;
    }
}
