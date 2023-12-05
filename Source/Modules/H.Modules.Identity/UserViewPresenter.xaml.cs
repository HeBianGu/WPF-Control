using H.Providers.Ioc;
using System.ComponentModel.DataAnnotations;

namespace H.Modules.Identity
{
    public class Filter : IFilter
    {
        public bool IsMatch(object obj)
        {
            return true;
        }
    }
    [Display(Name = "用户管理", GroupName = SystemSetting.GroupAuthority, Description = "应用此功能进行用户管理")]
    public class UserViewPresenter : IUserViewPresenter
    {

    }
}


