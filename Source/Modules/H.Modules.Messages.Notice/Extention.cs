using H.Modules.Messages.Notice;
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
        public static void AddNoticeMessage(this IServiceCollection services)
        {
            services.AddSingleton<INoticeMessageService, NoticeMessageService>();
        }
    }
}
