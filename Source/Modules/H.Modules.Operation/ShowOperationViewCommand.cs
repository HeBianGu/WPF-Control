using H.Common.Attributes;
using H.Extensions.FontIcon;
using H.Services.Message.Dialog.Commands;
using H.Services.Operation;
using H.Services.Setting;
using System.ComponentModel.DataAnnotations;

namespace H.Modules.Operation
{
    [Icon(FontIcons.Handwriting)]
    [Display(Name = "操作日志", GroupName = SettingGroupNames.GroupAuthority, Description = "应用此功能查看操作日志")]
    public class ShowOperationViewCommand : ShowIocCommand
    {
        public ShowOperationViewCommand()
        {
            this.Type = typeof(IOperationViewPresenter);
        }
    }
}
