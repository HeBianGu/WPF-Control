// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.ApplicationBases.Modules;
using H.Modules.About;
using H.Modules.Dependency;
using H.Modules.Feedback;
using H.Modules.Guide;
using H.Modules.Help.CommercialLicense;
using H.Modules.Help.Contact;
using H.Modules.Help.ReleaseVersions;
using H.Modules.Help.Support;
using H.Modules.Help.Website;
using H.Modules.Help.WebSite;
using H.Modules.Setting;
using H.Modules.SplashScreen;
using Microsoft.Extensions.DependencyInjection;

namespace System
{
    public static class Extention
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddDefaultModuleServices(this IServiceCollection services, Action<IDefaultModuleOptions> options = null)
        {
            DefaultModuleOptions opt = new DefaultModuleOptions();
            options?.Invoke(opt);
            services.AddAbout(opt.GetConfigOptions<IAboutOptions>());
            services.AddGuide(opt.GetConfigOptions<IGuideOptions>());
            services.AddBackgroundSplashScreen(opt.GetConfigOptions<ISplashScreenOptions>());
            services.AddSetting(opt.GetConfigOptions<ISettingViewOptions>());
            services.AddReleaseVersions(opt.GetConfigOptions<IReleaseVersionsOptions>());
            services.AddSupport(opt.GetConfigOptions<ISupportOptions>());
            services.AddWebsite(opt.GetConfigOptions<IWebsiteOptions>());
            services.AddSponsor();
            services.AddContact(opt.GetConfigOptions<IContactOptions>());
            services.AddFeedBack(opt.GetConfigOptions<IFeedbackOptions>());
            services.AddDependency(opt.GetConfigOptions<IDependencyOptions>());
            services.AddCommercialLicense();

        }

        public static void UseDefaultModuleOptions(this IApplicationBuilder app, Action<IDefaultModuleOptions> options = null)
        {
            DefaultModuleOptions opt = new DefaultModuleOptions();
            options?.Invoke(opt);
            app.UseAboutOptions(opt.GetConfigOptions<IAboutOptions>());
            app.UseSplashScreenOptions(opt.GetConfigOptions<ISplashScreenOptions>());
            app.UseGuideOptions(opt.GetConfigOptions<IGuideOptions>());
            app.UseSettingViewOptions(opt.GetConfigOptions<ISettingViewOptions>());
            app.UseSettingSecurityOptions(opt.GetConfigOptions<ISettingSecurityViewOption>());
            app.UseReleaseVersions(opt.GetConfigOptions<IReleaseVersionsOptions>());
            app.UseSupport(opt.GetConfigOptions<ISupportOptions>());
            app.UseWebsite(opt.GetConfigOptions<IWebsiteOptions>());
            app.UseContact(opt.GetConfigOptions<IContactOptions>());
            app.UseFeedBackOptions(opt.GetConfigOptions<IFeedbackOptions>());
            app.UseDependencyOptions(opt.GetConfigOptions<IDependencyOptions>());
        }
    }
}
