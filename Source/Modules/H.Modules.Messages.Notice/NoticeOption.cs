// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Extensions.Setting;
using H.Services.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

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
