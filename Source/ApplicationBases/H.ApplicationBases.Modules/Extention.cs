// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.ApplicationBases.Modules;
using H.Modules.About;
using H.Modules.Feedback;
using H.Modules.Guide;
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
            services.AddAbout(opt.GetConfigOptions<Action<IAboutOptions>>());
            services.AddGuide(opt.GetConfigOptions<Action<IGuideOptions>>());
            services.AddSplashScreen(opt.GetConfigOptions<Action<ISplashScreenOptions>>());
            services.AddSetting(opt.GetConfigOptions<Action<ISettingViewOptions>>());
            services.AddReleaseVersions(opt.GetConfigOptions<Action<IReleaseVersionsOptions>>());
            services.AddSupport(opt.GetConfigOptions<Action<ISupportOptions>>());
            services.AddWebsite(opt.GetConfigOptions<Action<IWebsiteOptions>>());
            services.AddSponsor();
            services.AddContact(opt.GetConfigOptions<Action<IContactOptions>>());
            services.AddFeedBack(opt.GetConfigOptions<Action<IFeedbackOptions>>());
        }

        public static void UseDefaultModuleOptions(this IApplicationBuilder app, Action<IDefaultModuleOptions> options = null)
        {
            DefaultModuleOptions opt = new DefaultModuleOptions();
            options?.Invoke(opt);
            app.UseAboutOptions(opt.GetConfigOptions<Action<IAboutOptions>>());
            app.UseSplashScreenOptions(opt.GetConfigOptions<Action<ISplashScreenOptions>>());
            app.UseGuideOptions(opt.GetConfigOptions<Action<IGuideOptions>>());
            app.UseSettingViewOptions(opt.GetConfigOptions<Action<ISettingViewOptions>>());
            app.UseSettingSecurityOptions(opt.GetConfigOptions<Action<ISettingSecurityViewOption>>());
            app.UseReleaseVersions(opt.GetConfigOptions<Action<IReleaseVersionsOptions>>());
            app.UseSupport(opt.GetConfigOptions<Action<ISupportOptions>>());
            app.UseWebsite(opt.GetConfigOptions<Action<IWebsiteOptions>>());
            app.UseContact(opt.GetConfigOptions<Action<IContactOptions>>());
            app.UseFeedBackOptions(opt.GetConfigOptions<Action<IFeedbackOptions>>());
        }
    }
}
