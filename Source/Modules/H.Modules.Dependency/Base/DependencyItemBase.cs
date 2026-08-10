// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")


// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.Form.Attributes;
using H.Controls.Form.PropertyItem.TextPropertyItems;
using H.Extensions.Mvvm.ViewModels.Base;

namespace H.Modules.Dependency.Base;

public class DependencyItemBase : DisplayBindableBase, IDependencyItem
{
    private string _name;
    /// <summary>
    /// 获取或设置对象的名称。
    /// </summary>
    [Display(Name = "名称")]
    [Browsable(true)]
    public override string Name
    {
        get { return _name; }
        set
        {
            _name = value;
            RaisePropertyChanged();
        }
    }

    private string _description;
    /// <summary>
    /// 获取或设置对象的描述。
    /// </summary>
    [Display(Name = "说明")]
    [Browsable(true)]
    public override string Description
    {
        get { return _description; }
        set
        {
            _description = value;
            RaisePropertyChanged();
        }
    }
    private string _Uri;
    [PropertyItem(typeof(HyperlinkPropertyItem))]
    [Display(Name = "地址")]
    public string Uri
    {
        get { return _Uri; }
        set
        {
            _Uri = value;
            RaisePropertyChanged();
        }
    }


    private string _Version;
    [Display(Name = "版本")]
    public string Version
    {
        get { return _Version; }
        set
        {
            _Version = value;
            RaisePropertyChanged();
        }
    }


    private string _Licence;
    [Display(Name = "许可")]
    public string Licence
    {
        get { return _Licence; }
        set
        {
            _Licence = value;
            RaisePropertyChanged();
        }
    }

}
