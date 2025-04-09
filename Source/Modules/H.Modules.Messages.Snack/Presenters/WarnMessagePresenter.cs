// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Common.Attributes;
using H.Extensions.FontIcon;
using System.ComponentModel.DataAnnotations;

namespace H.Modules.Messages.Snack
{
    [Icon(FontIcons.OverwriteWordsFillKorean)]
    [Display(Name = "警告消息", Description = "这是一条警告消息")]
    public class WarnMessagePresenter : MessagePresenterBase
    {
        public WarnMessagePresenter()
        {
            this.Level = 3;
        }
    }
}
