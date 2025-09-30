// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Modules.About;
using H.Modules.Guide;
using H.Modules.Help.Contact;
using H.Modules.Help.ReleaseVersions;
using H.Modules.Help.Support;
using H.Modules.Help.WebSite;
using H.Modules.Setting;
using H.Modules.SplashScreen;

namespace H.ApplicationBases.Modules
{
    public interface IDefaultModuleOptions
    {
        void UseAboutOptions(Action<IAboutOptions> action);
        void UseContactOptions(Action<IContactOptions> action);
        void UseGuideOptions(Action<IGuideOptions> action);
        void UseReleaseVersionsOptions(Action<IReleaseVersionsOptions> action);
        void UseSettingViewOptions(Action<ISettingViewOptions> action);
        void UseSplashScreenOptions(Action<ISplashScreenOptions> action);
        void UseSupportOptions(Action<ISupportOptions> action);
        void UseWebsiteOptions(Action<IWebsiteOptions> action);
        void UseSettingSecurityViewOptions(Action<ISettingSecurityViewOption> action);
    }
}
