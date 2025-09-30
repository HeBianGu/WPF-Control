// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.ColorBox;
using Microsoft.Extensions.DependencyInjection;

namespace System
{
    public static class ColorBoxExtention
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddColorBox(this IServiceCollection service)
        {
            service.AddSingleton<IColorBoxService, ColorBoxService>();
        }

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="service"></param>
        public static void UseColorBox(this IApplicationBuilder service, Action<ColorBoxSetting> action)
        {
            action?.Invoke(ColorBoxSetting.Instance);
            //SystemSetting.Instance.Add(ColorBoxSetting.Instance);
        }
    }
}
