// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Controls.Diagram.Extension;
using H.Providers.Ioc;
using Microsoft.Extensions.DependencyInjection;

namespace System
{
    public static class Extention
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddDiagram(this IServiceCollection service)
        {
            service.AddSingleton<IDiagramService, DiagramService>();
        }

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="service"></param>
        public static void UseDiagram(this IApplicationBuilder service, Action<DiagramSetting> action = null)
        {
            action?.Invoke(DiagramSetting.Instance);
            SettingDataManager.Instance.Add(DiagramSetting.Instance);
        }

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="service"></param>
        public static void UseApp(this IApplicationBuilder service, Action<AppSetting> action = null)
        {
            action?.Invoke(AppSetting.Instance);
            SettingDataManager.Instance.Add(AppSetting.Instance);
        }
    }


}
