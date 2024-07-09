using H.Modules.Messages.Snack;
using H.Services.Common;
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
