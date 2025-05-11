// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.Vlc;
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
