// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Services.Common
{
    public class ShowEditCommand : MessageCommandBase
    {
        public bool UseModelState { get; set; } = true;
        public object Value { get; set; }
        public override void Execute(object parameter)
        {
            IocMessage.Form.ShowEdit(this.Value ?? parameter, this.UseModelState ? null : x => true, x => this.Build(x));
        }
    }
}
