// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.Setting;
using H.Services.Setting;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace H.Controls.FavoriteBox
{
    [Display(Name = "收藏夹管理", GroupName = SettingGroupNames.GroupSystem, Description = "登录页面设置的信息")]
    public class FavoriteOptions : IocOptionInstance<FavoriteOptions>, IFavoriteOptions
    {
        [Browsable(false)]
        public ObservableCollection<FavoriteItem> FavoriteItems { get; set; } = new ObservableCollection<FavoriteItem>();
    }
}
