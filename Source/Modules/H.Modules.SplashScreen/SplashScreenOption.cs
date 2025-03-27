using H.Extensions.Common;
using H.Extensions.Setting;
using H.Services.Setting;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace H.Modules.SplashScreen;

[Display(Name = "启动页面", GroupName = SettingGroupNames.GroupSystem, Description = "启动页面设置信息")]
public class SplashScreenOption : IocOptionInstance<SplashScreenOption>
{
    public override void LoadDefault()
    {
        base.LoadDefault();
        this.Product = ApplicationProvider.Product;
    }

    private string _product;
    [Display(Name = "登录标题")]
    public string Product
    {
        get { return _product; }
        set
        {
            _product = value;
            RaisePropertyChanged();
        }
    }

    private double _productFontSize;
    [DefaultValue(50)]
    [Display(Name = "字体大小")]
    public double ProductFontSize
    {
        get { return _productFontSize; }
        set
        {
            _productFontSize = value;
            RaisePropertyChanged();
        }
    }

}
