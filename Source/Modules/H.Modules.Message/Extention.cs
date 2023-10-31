
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
        public static void AddMessage(this IServiceCollection services)
        {
            services.AddSingleton<IDialogMessage, DialogMessage>();
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
