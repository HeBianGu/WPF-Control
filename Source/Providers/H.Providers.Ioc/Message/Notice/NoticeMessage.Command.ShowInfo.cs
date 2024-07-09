// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Providers.Ioc
{
    public class ShowInfoNoticeMessageCommand : ShowNoticeMessageCommandBase
    {
        public override void Execute(object parameter)
        {
            Ioc<INoticeMessageService>.Instance.ShowInfo(this.Message);
        }
    }
}
