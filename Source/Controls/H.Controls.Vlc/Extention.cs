// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase


using H.Controls.Vlc;
using H.Providers.Ioc;

namespace System
{
    public static class PropertyGridExtention
    {
        ///// <summary>
        ///// 注册
        ///// </summary>
        ///// <param name="service"></param>
        //public static void AddVlc(this IServiceCollection service)
        //{
        //    service.AddSingleton<IService, Service>();
        //}

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="service"></param>
        public static void UseVlc(this IApplicationBuilder service, Action<VlcSetting> action)
        {
            action?.Invoke(VlcSetting.Instance);
            SettingDataManager.Instance?.Add(VlcSetting.Instance);
        }
    }


}
