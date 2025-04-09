// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using H.Extensions.Setting;
using H.Services.Setting;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace H.Controls.Adorner;

[Display(Name = "装饰层配置", GroupName = SettingGroupNames.GroupStyle)]
public class AdornerSetting : LazySettableInstance<AdornerSetting>
{
    private double _dragAornerOpacity;
    [Display(Name = "拖拽时控件的透明度")]
    [DefaultValue(0.9)]
    [Range(0.0, 1.0, ErrorMessage = "数据超出范围范围: 0.0 - 1.0")]
    public double DragAornerOpacity
    {
        get { return _dragAornerOpacity; }
        set
        {
            _dragAornerOpacity = value;
            RaisePropertyChanged();
        }
    }
}

//public class GetAdorner : MarkupExtension
//{
//    public Type Type { get; set; }

//    public object[] Params { get; set; }

//    public override object ProvideValue(IServiceProvider serviceProvider)
//    {
//        return null;
//    }
//}
