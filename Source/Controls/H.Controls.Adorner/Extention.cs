﻿// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using H.Controls.Adorner;
using H.Providers.Ioc;

namespace System
{
    public static class Extention
    {
        ///// <summary>
        ///// 注册
        ///// </summary>
        ///// <param name="service"></param>
        //public static void AddPropertyGrid(this IServiceCollection service)
        //{
        //    service.AddSingleton<IService, Service>();
        //}

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="service"></param>
        public static void UseAdorner(this IApplicationBuilder service, Action<AdornerSetting> action = null)
        {
            action?.Invoke(AdornerSetting.Instance);
            SettingDataManager.Instance?.Add(AdornerSetting.Instance);
        }
    }


}