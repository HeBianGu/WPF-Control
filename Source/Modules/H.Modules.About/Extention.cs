using H.Modules.About;
using H.Services.Common.About;
using H.Services.Setting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace System
{
    public static class Extention
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddAbout(this IServiceCollection services, Action<IAboutOptions> setupAction = null)
        {
            services.AddOptions();
            services.TryAdd(ServiceDescriptor.Singleton<IAboutViewPresenter, AboutViewPresenter>());
            if (setupAction != null)
                services.Configure(new Action<AboutOptions>(setupAction));
        }

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="service"></param>
        public static void UseAboutOptions(this IApplicationBuilder service, Action<IAboutOptions> action = null)
        {
            action?.Invoke(AboutOptions.Instance);
            IocSetting.Instance.Add(AboutOptions.Instance);
        }
    }
}
