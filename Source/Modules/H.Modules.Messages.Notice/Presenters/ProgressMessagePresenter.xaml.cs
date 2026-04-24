// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Attributes;
using H.Extensions.Common;
using H.Extensions.FontIcon;
using System.Collections;
namespace H.Modules.Messages.Notice
{
    [Icon(FontIcons.Sync)]
    [Display(Name = "进度条消息", Description = "这是一条进度条消息")]
    public class ProgressMessagePresenter : MessagePresenterBase, IPercentNoticeItem
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

        public Func<Task<bool>> CanExecuteAsync { get; set; }
        public RelayCommand CancelCommand => new RelayCommand(async x =>
        {
            var r = await this.CanExecuteAsync.Invoke();
            if (r == false)
                return;
            if (x is FrameworkElement element)
            {
                if (element.GetParent<ItemsControl>().ItemsSource is IList list)
                    list.Remove(this);
            }
        });

        public bool IsCanceling { get; set; }
    }
}
