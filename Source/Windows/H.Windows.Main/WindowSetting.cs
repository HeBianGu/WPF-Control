// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.Setting;
using H.Services.Setting;
using System.ComponentModel;
using System.Windows.Media;

namespace H.Windows.Main;

[Display(Name = "窗口设置", GroupName = SettingGroupNames.GroupControl, Description = "设置窗口参数")]
public class WindowSetting : Settable<WindowSetting>
{
    private string _backImagePath;
    [DefaultValue("pack://application:,,,/H.Extensions.BackgroundImage;component/b41.png")]
    [Display(Name = "窗口背景图片")]
    public string BackImagePath
    {
        get { return _backImagePath; }
        set
        {
            _backImagePath = value;
            RaisePropertyChanged();
        }
    }

    private bool _useBackImage;
    [DefaultValue(false)]
    [Display(Name = "启用窗口背景图片")]
    public bool UseBackImage
    {
        get { return _useBackImage; }
        set
        {
            _useBackImage = value;
            RaisePropertyChanged();
        }
    }

    private double _opacity;
    [DefaultValue(0.3)]
    [Display(Name = "图片透明度")]
    public double Opacity
    {
        get { return _opacity; }
        set
        {
            _opacity = value;
            RaisePropertyChanged();
        }
    }

    private Stretch _stretch;
    [DefaultValue(Stretch.UniformToFill)]
    [Display(Name = "图片拉伸")]
    public Stretch Stretch
    {
        get { return _stretch; }
        set
        {
            _stretch = value;
            RaisePropertyChanged();
        }
    }

    private bool _useNoticeOnMainWindowClose;
    [DefaultValue(true)]
    [Display(Name = "主窗口关闭提示", Description = "当主窗口点击关闭时会提示是否关闭窗口")]
    public bool UseNoticeOnMainWindowClose
    {
        get { return _useNoticeOnMainWindowClose; }
        set
        {
            _useNoticeOnMainWindowClose = value;
            RaisePropertyChanged();
        }
    }

    private bool _useSaveOnMainWindowClose;
    [DefaultValue(true)]
    [Display(Name = "主窗口关闭保存数据", Description = "当主窗口点击关闭时主窗口关闭保存数据")]
    public bool UseSaveOnMainWindowClose
    {
        get { return _useSaveOnMainWindowClose; }
        set
        {
            _useSaveOnMainWindowClose = value;
            RaisePropertyChanged();
        }
    }
}
