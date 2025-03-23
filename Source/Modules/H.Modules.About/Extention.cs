
using H.Modules.About;
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
        public static void AddAbout(this IServiceCollection service, Action<IAboutViewPresenterOption> action = null)
        {
            service.AddSingleton<IAboutViewPresenter, AboutViewPresenter>();
        }

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="service"></param>
        [Obsolete]
        public static void UseAbout(this IApplicationBuilder service, Action<IAboutViewPresenterOption> action = null)
        {
            action?.Invoke(AboutViewPresenter.Instance);

        }
    }
}
