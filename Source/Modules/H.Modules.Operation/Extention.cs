// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Extensions.ViewModel;
using H.Modules.Operation;
using H.Providers.Ioc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Reflection.Emit;

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
            service.AddSingleton<IRepositoryViewModel<hi_dd_operation>, RepositoryViewModel<hi_dd_operation>>();
            service.AddSingleton<IOperationViewPresenter, OperationViewPresenter>();
        }

        public static void AddOperationService(this IServiceCollection service)
        {
            service.AddSingleton<IOperationService, OperationService>();
        }
    }
}
