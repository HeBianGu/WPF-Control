// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.Common;
using H.Extensions.Setting;
using H.Services.Setting;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace H.Modules.SplashScreen;

public interface ISplashScreenOptions : ISettable
{
    string Product { get; set; }
    double ProductFontSize { get; set; }

    string Sub { get; set; }
}

[Display(Name = "启动页面", GroupName = SettingGroupNames.GroupSystem, Description = "启动页面设置信息")]
public class SplashScreenOptions : IocOptionInstance<SplashScreenOptions>, ISplashScreenOptions
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

    private string _sub;
    [DefaultValue(null)]
    [Display(Name = "副标题")]
    public string Sub
    {
        get { return _sub; }
        set
        {
            _sub = value;
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

    private double _subFontSize;
    [DefaultValue(20)]
    [Display(Name = "副标题字体大小")]
    public double SubFontSize
    {
        get { return _subFontSize; }
        set
        {
            _subFontSize = value;
            RaisePropertyChanged();
        }
    }

}
