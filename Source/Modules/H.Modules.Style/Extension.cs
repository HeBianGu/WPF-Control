using H.Modules.Style;
using H.Services.Common.MainWindow;
using H.Services.Setting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace System;

public static partial class Extension
{
    public static IApplicationBuilder UseStyleOptions(this IApplicationBuilder builder, Action<IStyleOptions> option = null)
    {
        option?.Invoke(StyleOptions.Instance);
        IocSetting.Instance.Add(StyleOptions.Instance);
        return builder;
    }
}
