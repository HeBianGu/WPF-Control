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

namespace H.Windows.Main;

[Display(Name = "主窗口设置", GroupName = SettingGroupNames.GroupControl, Description = "设置主窗口设置参数")]
public class MainWindowOption : IocOptionInstance<MainWindowOption>
{
    private double _width;
    [ReadOnly(true)]
    [Display(Name = "宽度", Description = "设置主窗口高度")]
    [DefaultValue(1100.0)]
    public double Width
    {
        get { return _width; }
        set
        {
            _width = value;
            RaisePropertyChanged();
        }
    }

    private double _height;
    [ReadOnly(true)]
    [Display(Name = "高度", Description = "设置主窗口高度")]
    [DefaultValue(700.0)]
    public double Height
    {
        get { return _height; }
        set
        {
            _height = value;
            RaisePropertyChanged();
        }
    }

    private WindowStartupLocation _WindowStartupLocation;
    [ReadOnly(true)]
    [Display(Name = "位置", Description = "设置主窗口居中位置")]
    [DefaultValue(WindowStartupLocation.CenterScreen)]
    public WindowStartupLocation WindowStartupLocation
    {
        get { return _WindowStartupLocation; }
        set
        {
            _WindowStartupLocation = value;
            RaisePropertyChanged();
        }
    }

    private WindowState _windowState;
    [ReadOnly(true)]
    [Display(Name = "状态", Description = "设置主窗口状态，最大、最小和常规")]
    [DefaultValue(WindowState.Normal)]
    public WindowState WindowState
    {
        get { return _windowState; }
        set
        {
            _windowState = value;
            RaisePropertyChanged();
        }
    }
}
