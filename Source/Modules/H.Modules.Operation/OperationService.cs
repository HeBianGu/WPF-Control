using H.Extensions.ViewModel;
using H.Providers.Ioc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace H.Modules.Operation
{
    internal class OperationService : IOperationService
    {
        public void Log<T>(string title, string message = null, OperationType operationType = OperationType.Default, bool result = true, [CallerMemberName] string methodName = null)
        {
            if (typeof(hi_dd_operation) == typeof(T))
                return;
            var vm = Ioc.GetService<IRepositoryViewModel<hi_dd_operation>>();
            string typeName = typeof(T).GetCustomAttribute<DisplayAttribute>()?.Name ?? typeof(T).Name;
            var operation = new hi_dd_operation()
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
