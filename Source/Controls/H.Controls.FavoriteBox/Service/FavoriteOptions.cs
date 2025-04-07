using H.Extensions.Setting;
using H.Services.Setting;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace H.Controls.FavoriteBox
{
    [Display(Name = "收藏夹管理", GroupName = SettingGroupNames.GroupSystem, Description = "登录页面设置的信息")]
    public class FavoriteOptions : IocOptionInstance<FavoriteOptions>, IFavoriteOptions
    {
        [Browsable(false)]
        public ObservableCollection<FavoriteItem> FavoriteItems { get; set; } = new ObservableCollection<FavoriteItem>();
    }
}
