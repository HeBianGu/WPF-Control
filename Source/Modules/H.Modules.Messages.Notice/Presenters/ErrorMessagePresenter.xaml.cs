// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Common.Attributes;
using H.Extensions.FontIcon;

namespace H.Modules.Messages.Notice
{
    [Icon(FontIcons.ErrorBadge)]
    [Display(Name = "错误消息", Description = "这是一条错误消息")]
    public class ErrorMessagePresenter : MessagePresenterBase
    {
        public ErrorMessagePresenter()
        {
            this.Level = 4;
        }
    }
}
