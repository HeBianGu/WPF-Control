// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;

namespace H.Providers.Ioc
{
    public class ShowInfoSnackMessageCommand : ShowSnackMessageCommandBase
    {
        public override void Execute(object parameter)
        {
            Ioc<ISnackMessageService>.Instance.ShowInfo(this.Message);
        }
    }
}
