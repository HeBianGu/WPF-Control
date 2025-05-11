// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.ApplicationBases.Modules;
using H.ApplicationBases.Themes;
using H.Extensions.ApplicationBase;

namespace H.ApplicationBases.Default
{
    public class DefaultApplicationOptions : CacheActionOptionsBase, IDefaultApplicationOptions
    {
        public void UseModulesOptions(Action<IDefaultModuleOptions> action)
        {
            this.ConfigOptions(action);
        }

        public void UseThemeModuleOptions(Action<IDefaultThemeOptions> action)
        {
            this.ConfigOptions(action);
        }
    }
}
