// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


namespace H.Services.Common
{
    public class ShowViewCommand : MessageCommandBase
    {
        public object Value { get; set; }

        public override async Task ExecuteAsync(object parameter)
        {
            await IocMessage.Form.ShowView(this.Value ?? parameter, x => this.Build(x));
        }

        public override bool CanExecute(object parameter)
        {
            return base.CanExecute(parameter) && (parameter != null || this.Value != null);
        }
    }
}
