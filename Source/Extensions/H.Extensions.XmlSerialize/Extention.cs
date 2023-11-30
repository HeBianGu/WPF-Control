// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using H.Extensions.XmlSerialize;
using H.Providers.Ioc;
using Microsoft.Extensions.DependencyInjection;

namespace System
{
    public static class Extention
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddXmlMetaSettingService(this IServiceCollection service)
        {
            service.AddSingleton<IMetaSettingService, XmlMetaSettingService>();
        }
    }
}
