using H.Common.Attributes;
using H.Extensions.FontIcon;
using H.Services.Message.Dialog.Commands;

namespace H.Modules.Identity.Commands
{
    [Icon(FontIcons.AddFriend)]
    [Display(Name = "用户管理", GroupName = SettingGroupNames.GroupAuthority, Description = "应用此功能进行用户管理")]
    public class ShowUserViewCommand : ShowIocCommand
    {
        public ShowUserViewCommand()
        {
            this.Type = typeof(IUserViewPresenter);
        }
    }
}


