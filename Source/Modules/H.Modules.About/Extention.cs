using H.Modules.About;
using H.Services.Common.About;
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
            action?.Invoke(AboutViewPresenter.Instance);
            service.AddSingleton<IAboutViewPresenter, AboutViewPresenter>();
        }
    }
}
