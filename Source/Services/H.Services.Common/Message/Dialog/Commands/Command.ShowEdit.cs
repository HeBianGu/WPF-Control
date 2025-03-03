// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace H.Services.Common
{
    public class ShowEditCommand : MessageCommandBase
    {
        public bool UseModelState { get; set; } = true;
        public object Value { get; set; }

        public override async Task ExecuteAsync(object parameter)
        {
            await IocMessage.Form.ShowEdit(this.Value ?? parameter, x => this.Build(x), this.UseModelState ? null : x => true);
        }
    }

    public class ShowTabEditCommand : MessageCommandBase
    {
        public bool UseModelState { get; set; } = true;
        public object Value { get; set; }
        public string TabNames { get; set; }

        public override async Task ExecuteAsync(object parameter)
        {
            await IocMessage.Form.ShowTabEdit(this.Value ?? parameter, x => this.Build(x), this.UseModelState ? null : x => true, x => x.UseGroupNames = TabNames);
        }
    }
}
