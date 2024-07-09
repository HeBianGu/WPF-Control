// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Providers.Ioc
{
    public class ShowViewCommand : MessageCommandBase
    {
        public object Value { get; set; }
        public override void Execute(object parameter)
        {
            IocMessage.Form.ShowView(this.Value);
        }
    }
}
