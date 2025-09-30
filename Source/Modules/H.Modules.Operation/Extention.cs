// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.DataBase.Repository;
using H.Modules.Operation;
using H.Services.Operation;
using Microsoft.Extensions.DependencyInjection;

namespace System
{
    public static class Extention
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddOperationViewPresenter(this IServiceCollection service)
        {
            service.AddOperationService();
            service.AddSingleton<IRepositoryBindable<hi_dd_operation>, RepositoryBindable<hi_dd_operation>>();
            service.AddSingleton<IOperationViewPresenter, OperationViewPresenter>();
        }

        public static void AddOperationService(this IServiceCollection service)
        {
            service.AddSingleton<IOperationService, OperationService>();
        }
    }
}
