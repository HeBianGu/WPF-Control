using H.Modules.Messages.Dialog;
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
        public static void AddWindowDialogMessage(this IServiceCollection services)
        {
            services.AddSingleton<IDialogMessageService, WindowDialogMessageService>();
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddAdornerDialogMessage(this IServiceCollection services)
        {
            services.AddSingleton<IDialogMessageService, AdornerDialogMessageService>();
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddWindowMessage(this IServiceCollection services)
        {
            services.AddSingleton<IWindowDialogMessageService, WindowDialogMessageService>();
        }
    }
}
