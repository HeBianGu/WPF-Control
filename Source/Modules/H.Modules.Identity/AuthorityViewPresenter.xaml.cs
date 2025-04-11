using H.Common.Attributes;
using H.Extensions.FontIcon;
using H.Services.Common;
using H.Services.Identity.Author;

namespace H.Modules.Identity
{
    [Icon(FontIcons.Edit)]
    [Display(Name = "用户管理", GroupName = SettingGroupNames.GroupAuthority, Description = "应用此功能进行用户管理")]
    public class AuthorityViewPresenter : IAuthorityViewPresenter
    {
    }
}
