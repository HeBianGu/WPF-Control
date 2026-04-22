// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.Form.Attributes;
using H.Controls.Form.PropertyItem.Attribute;
using H.Controls.Form.PropertyItem.ComboBoxPropertyItems;
using H.Mvvm.ViewModels.Base;

namespace H.App.AIDI.ViewModel;
public class LabelItem : BindableBase
{
    private string _labelName;
    [Display(Name = "标记名称")]
    public string LabelName
    {
        get { return _labelName; }
        set
        {
            _labelName = value;
            RaisePropertyChanged();
        }
    }

    private Color _labelColor = Colors.Chartreuse;
    [GetHightlightColorsSource]
    [PropertyItem(typeof(ColorComboBoxPropertyItem))]
    [Display(Name = "标记颜色")]
    public Color LabelColor
    {
        get { return _labelColor; }
        set
        {
            _labelColor = value;
            RaisePropertyChanged();
        }
    }
}



