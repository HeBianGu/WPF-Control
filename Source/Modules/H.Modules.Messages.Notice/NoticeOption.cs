// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

global using H.Extensions.Setting;
global using System.ComponentModel.DataAnnotations;
global using System.ComponentModel;
global using H.Services.Setting;
global using H.Common.Transitionable;

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
