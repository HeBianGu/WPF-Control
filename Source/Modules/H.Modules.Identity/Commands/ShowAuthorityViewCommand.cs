using H.Common.Attributes;
using H.Extensions.FontIcon;
using H.Services.Identity.Author;
using H.Services.Message.Dialog.Commands;

namespace H.Modules.Identity.Commands
{
    [Icon(FontIcons.Edit)]
    [Display(Name = "权限管理", GroupName = SettingGroupNames.GroupAuthority, Description = "应用此功能进行权限管理")]
    public class ShowAuthorityViewCommand : ShowIocCommand
    {
        public ShowAuthorityViewCommand()
        {
            this.Type = typeof(IAuthorityViewPresenter);
        }
    }
}


