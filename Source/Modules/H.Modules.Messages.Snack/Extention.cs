// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Modules.Messages.Snack;
using Microsoft.Extensions.DependencyInjection;

namespace System
{
    public static class Extention
    {

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddSnackMessage(this IServiceCollection services)
        {
            services.AddSingleton<ISnackMessageService, SnackMessageService>();
        }
    }
}
