// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.Setting;
using H.Services.Identity.Author;
using System.Collections.ObjectModel;

namespace H.Modules.Identity
{
    [Display(Name = "权限设置", GroupName = SettingGroupNames.GroupSystem)]
    public class AuthoritySetting : Settable<AuthoritySetting>
    {
        private bool _useAuthority = true;
        [Display(Name = "启用权限控制")]
        public bool UseAuthority
        {
            get { return _useAuthority; }
            set
            {
                _useAuthority = value;
                RaisePropertyChanged();
            }
        }

        private bool _useAllIfNoLogin = true;
        [Display(Name = "启用如果没有登录用户加载所有权限")]
        public bool UseAllIfNoLggin
        {
            get { return _useAllIfNoLogin; }
            set
            {
                _useAllIfNoLogin = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<IAuthority> _authoritys = new ObservableCollection<IAuthority>();
        public ObservableCollection<IAuthority> Authoritys
        {
            get { return _authoritys; }
            set
            {
                _authoritys = value;
                RaisePropertyChanged();
            }
        }
    }
}
