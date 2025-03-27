global using H.Common.Interfaces.Where;

namespace H.Modules.Identity
{
    public class Filter : IFilterable
    {
        public bool IsMatch(object obj)
        {
            return true;
        }
    }
    [Display(Name = "用户管理", GroupName = SettingGroupNames.GroupAuthority, Description = "应用此功能进行用户管理")]
    public class UserViewPresenter : IUserViewPresenter
    {

    }
}


