﻿using H.Modules.Messages.Snack;
using H.Providers.Ioc;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Input;

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