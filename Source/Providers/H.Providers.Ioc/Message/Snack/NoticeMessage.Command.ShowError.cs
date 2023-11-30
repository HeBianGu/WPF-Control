// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;

namespace H.Providers.Ioc
{
    public class ShowErrorSnackMessageCommand : ShowSnackMessageCommandBase
    {
        public override void Execute(object parameter)
        {
            Ioc<ISnackMessageService>.Instance.ShowError(this.Message);
        }
    }
}
