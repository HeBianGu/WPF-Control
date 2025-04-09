// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control
using H.Common.Attributes;
using H.Extensions.FontIcon;
using System.ComponentModel.DataAnnotations;

namespace H.Modules.Messages.Snack
{
    [Icon(FontIcons.DefenderApp)]
    [Display(Name = "严重错误", Description = "这是一条严重错误")]
    public class FatalMessagePresenter : MessagePresenterBase
    {
        public FatalMessagePresenter()
        {
            this.Level = 5;
        }
    }
}
