// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Common.Transitionable;
global using H.Extensions.Setting;
global using H.Services.Setting;
global using System.ComponentModel;
global using System.ComponentModel.DataAnnotations;

namespace H.Modules.Messages.Notice
{
    [Display(Name = "提示消息设置", GroupName = SettingGroupNames.GroupControl, Description = "提示消息设置的信息")]
    public class NoticeOption : IocOptionInstance<NoticeOption>
    {
        private ITransitionable _transitionable;
        [Browsable(false)]
        [Display(Name = "过渡动画", Description = "选择过渡动画")]
        public ITransitionable Transitionable
        {
            get { return _transitionable; }
            set
            {
                _transitionable = value;
                RaisePropertyChanged();
            }
        }

    }
}
