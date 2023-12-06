

using H.Extensions.Encryption;
using System.Reflection;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using H.Providers.Ioc;

namespace System
{
    public static class Extention
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddDESCryptService(this IServiceCollection service)
        {
            service.AddSingleton<ICryptService, DESCryptService>();
        }
    }
}
