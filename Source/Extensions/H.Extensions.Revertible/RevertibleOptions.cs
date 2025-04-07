using H.Extensions.Setting;
using H.Services.Setting;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace H.Extensions.Revertible;

[Display(Name = "登录页面", GroupName = SettingGroupNames.GroupSystem, Description = "登录页面设置的信息")]
public class RevertibleOptions : IocOptionInstance<RevertibleOptions>, IRevertibleOptions
{
    private int _capacity;
    [ReadOnly(true)]
    [DefaultValue(10)]
    [Display(Name = "容量")]
    public int Capacity
    {
        get { return _capacity; }
        set
        {
            _capacity = value;
            RaisePropertyChanged();
        }
    }
    [Browsable(false)]
    [DefaultValue(false)]
    [Display(Name = "自动初始化")]
    public bool AutoInitializedOnCreate { get; set; }
}
