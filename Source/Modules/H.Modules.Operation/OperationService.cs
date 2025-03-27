using H.Extensions.DataBase.Repository;
using H.Services.Common;
using H.Services.Identity;
using H.Services.Operation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace H.Modules.Operation
{
    internal class OperationService : IOperationService
    {
        public void Log<T>(string title, string message = null, OperationType operationType = OperationType.Default, bool result = true, [CallerMemberName] string methodName = null)
        {
            if (typeof(hi_dd_operation) == typeof(T))
                return;
            IRepositoryBindable<hi_dd_operation> vm = Ioc.GetService<IRepositoryBindable<hi_dd_operation>>();
            string typeName = typeof(T).GetCustomAttribute<DisplayAttribute>()?.Name ?? typeof(T).Name;
            vm.UseMessage = false;
            hi_dd_operation operation = new hi_dd_operation()
            {
                Title = title,
                Message = message,
                Method = methodName,
                OperationType = operationType,
                Type = typeName,
                UserID = Ioc<ILoginService>.Instance?.User?.ID,
                UserName = Ioc<ILoginService>.Instance?.User?.Account
            };
            vm.Add(operation);
        }
    }
}
