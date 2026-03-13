// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Attach;
using H.Extensions.Mvvm.Commands;
using H.Extensions.Mvvm.ViewModels.Base;
using H.Services.Message.Dialog;

namespace H.Modules.Login.Presenters;

[Icon(FontIcons.Connect)]
[Display(Name = "登录页面", GroupName = SettingGroupNames.GroupSystem, Description = "登录页面的呈现")]
public class BackgroundRigisterLoginViewPresenter : RigisterLoginViewPresenter
{
    public BackgroundRigisterLoginViewPresenter(IOptions<LoginOptions> options) : base(options)
    {

    }

    public override void InitWindow(Window window)
    {
        var background = LoginOptions.Instance?.Background;
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
