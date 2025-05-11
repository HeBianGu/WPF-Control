// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.Setting;

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
