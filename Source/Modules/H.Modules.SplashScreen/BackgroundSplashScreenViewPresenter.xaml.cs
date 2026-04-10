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

public class BackgroundSplashScreenViewPresenter : SplashScreenViewPresenter
{
    public override void InitWindow(Window window)
    {
        base.InitWindow(window);
        var background = SplashScreenOptions.Instance?.Background;
        if (background != null)
        {
            var converter = TypeDescriptor.GetConverter(typeof(ImageSource));
            ImageBrush imageBrush = new ImageBrush();
            imageBrush.ImageSource = converter.ConvertFromInvariantString(background) as ImageSource;
            window.Background = imageBrush;
            Cattach.SetCornerRadius(window, new CornerRadius(10));
            Cattach.SetCaptionForeground(window, Brushes.White);
        }
    }
}
