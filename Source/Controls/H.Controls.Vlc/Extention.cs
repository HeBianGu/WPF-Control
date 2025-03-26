// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using H.Controls.Vlc;
using H.Services.Common;
using H.Services.Setting;

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
            IocSetting.Instance?.Add(VlcSetting.Instance);
        }
    }


}
