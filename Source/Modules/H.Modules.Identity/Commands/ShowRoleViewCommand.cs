using H.Common.Attributes;
using H.Extensions.FontIcon;
using H.Services.Message.Dialog.Commands;

namespace H.Modules.Identity.Commands
{
    [Icon(FontIcons.People)]
    [Display(Name = "角色管理", GroupName = SettingGroupNames.GroupAuthority, Description = "应用此功能进行角色管理")]
    public class ShowRoleViewCommand : ShowIocCommand
    {
        public ShowRoleViewCommand()
        {
            this.Type = typeof(IRoleViewPresenter);
        }
    }
}


