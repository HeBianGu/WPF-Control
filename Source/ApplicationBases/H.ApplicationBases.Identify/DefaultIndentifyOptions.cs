// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.DataBases.Sqlite;
using H.Extensions.ApplicationBase;
using H.Modules.Identity;
using H.Modules.Login;

namespace H.ApplicationBases.Identify
{
    public class DefaultIndentifyOptions : CacheActionOptionsBase, IDefaultIndentifyOptions
    {
        public void UseLoginOptions(Action<ILoginOptions> action)
        {
            this.ConfigOptions(action);
        }

        public void UseIdentifyOptions(Action<IIdentifyOptions> action)
        {
            this.ConfigOptions(action);
        }

        public void UseRegistorOptions(Action<IRegistorOptions> action)
        {
            this.ConfigOptions(action);
        }

        public void UseSqliteSettable(Action<ISqliteSettable> action)
        {
            this.ConfigOptions(action);
        }
    }
}
