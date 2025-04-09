// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Common.Attributes;
using H.Extensions.FontIcon;

namespace H.Modules.Messages.Notice
{
    [Icon(FontIcons.Info)]
    [Display(Name = "提示消息", Description = "这是一条提示消息")]
    public class InfoMessagePresenter : MessagePresenterBase
    {
        public InfoMessagePresenter()
        {
            this.Level = 2;
        }
    }
}
