// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Common.Attributes;
using H.Extensions.FontIcon;
using H.Services.Common;
using System.ComponentModel.DataAnnotations;

namespace H.Modules.Messages.Snack
{
    [Icon(FontIcons.Refresh)]
    [Display(Name = "进度条消息", Description = "这是一条进度条消息")]
    public class ProgressMessagePresenter : MessagePresenterBase, IPercentSnackItem
    {
        private double _value;
        public double Value
        {
            get { return _value; }
            set
            {
                _value = value;
                RaisePropertyChanged();
            }
        }
    }
}
