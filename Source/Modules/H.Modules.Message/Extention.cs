using H.Modules.Message;
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
        public static void AddWindowDialogMessage(this IServiceCollection services)
        {
            services.AddSingleton<IDialogMessage, WindowDialogMessage>();
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddAdornerDialogMessage(this IServiceCollection services)
        {
            services.AddSingleton<IDialogMessage, AdornerDialogMessage>();
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddWindowMessage(this IServiceCollection services)
        {
            services.AddSingleton<IWindowMessage, WindowDialogMessage>();
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddNoticeMessage(this IServiceCollection services)
        {
            services.AddSingleton<INoticeMessageService, NoticeMessageService>();
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddFormMessage(this IServiceCollection services)
        {
            services.AddSingleton<IFormMessage, FormMessage>();
        }


        ///// <summary>
        ///// 配置
        ///// </summary>
        ///// <param name="service"></param>
        //public static void UseMessage(this IApplicationBuilder service, Action<IAboutViewPresenterOption> action = null)
        //{
        //    action?.Invoke(AboutViewPresenter.Instance);
        //}
    }
}
