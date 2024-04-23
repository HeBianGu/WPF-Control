// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;

namespace H.Providers.Ioc
{
    public abstract class ShowNoticeMessageCommandBase : IocMarkupCommandBase
    {
        public string Message { get; set; } = "默认消息";

        public override void Execute(object parameter)
        {
            Ioc<INoticeMessageService>.Instance.ShowInfo(this.Message);
        }
    }
}
