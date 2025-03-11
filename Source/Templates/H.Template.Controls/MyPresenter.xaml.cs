using H.Mvvm;
using H.Mvvm.ViewModels.Base;
namespace H.Template.Controls;
public class MyPresenter : DisplayBindableBase
{
    private double _minHeight = 100;
    [DefaultValue(100)]
    [TypeConverter(typeof(LengthConverter))]
    [Display(Name = "最小高度", Description = "设置页面最小高度")]
    public double MinHeight
    {
        get { return _minHeight; }
        set
        {
            _minHeight = value;
        }
    }
}
