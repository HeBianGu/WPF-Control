// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Services.Setting;

namespace H.Controls.Adorner;

public static class Extention
{

    /// <summary>
    /// 配置
    /// </summary>
    /// <param name="service"></param>
    public static void UseAdorner(this IApplicationBuilder service, Action<AdornerSetting> action = null)
    {
        action?.Invoke(AdornerSetting.Instance);
        IocSetting.Instance.Add(AdornerSetting.Instance);
    }
}
