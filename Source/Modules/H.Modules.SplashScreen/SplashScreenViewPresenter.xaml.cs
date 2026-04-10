// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Mvvm.ViewModels.Base;
global using H.Services.Common.SplashScreen;
using H.Attach;
using H.Common.Interfaces;

namespace H.Modules.SplashScreen;

public class SplashScreenViewPresenter : BindableBase, ISplashScreenViewPresenter, IWindowInitable
{
    private string _message;
    public string Message
    {
        get { return _message; }
        set
        {
            _message = value;
            RaisePropertyChanged();
        }
    }

    public int SleepMicroseconds => SplashScreenOptions.Instance.SleepMicroseconds;

    public virtual void InitWindow(Window window)
    {
        window.SizeToContent = SizeToContent.WidthAndHeight;
        Cattach.SetCaptionBackground(window, null);
    }
}
