// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.ApplicationBase;
using H.Modules.About;
using H.Modules.Feedback;
using H.Modules.Guide;
using H.Modules.Help.Contact;
using H.Modules.Help.ReleaseVersions;
using H.Modules.Help.Support;
using H.Modules.Help.WebSite;
using H.Modules.Setting;
using H.Modules.SplashScreen;

namespace H.ApplicationBases.Modules
{

    public class DefaultModuleOptions : CacheActionOptionsBase, IDefaultModuleOptions
    {
        public void UseAboutOptions(Action<IAboutOptions> action)
        {
            this.ConfigOptions(action);
        }

        public void UseContactOptions(Action<IContactOptions> action)
        {
            this.ConfigOptions(action);
        }

        public void UseGuideOptions(Action<IGuideOptions> action)
        {
            this.ConfigOptions(action);
        }

        public void UseReleaseVersionsOptions(Action<IReleaseVersionsOptions> action)
        {
            this.ConfigOptions(action);
        }

        public void UseSettingSecurityViewOptions(Action<ISettingSecurityViewOption> action)
        {
            this.ConfigOptions(action);
        }

        public void UseSettingViewOptions(Action<ISettingViewOptions> action)
        {
            this.ConfigOptions(action);
        }

        public void UseSplashScreenOptions(Action<ISplashScreenOptions> action)
        {
            this.ConfigOptions(action);
        }

        public void UseSupportOptions(Action<ISupportOptions> action)
        {
            this.ConfigOptions(action);
        }

        public void UseWebsiteOptions(Action<IWebsiteOptions> action)
        {
            this.ConfigOptions(action);
        }

        public void UseFeedbackOptions(Action<IFeedbackOptions> action)
        {
            this.ConfigOptions(action);
        }

    }
}
