// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Services.Identity;

namespace H.Presenters.Design.Presenter;

[Display(Name = "当前用户")]
public class UserPresenter : TitlePresenter
{
    public UserPresenter()
    {
        this.Title = "当前用户：";
        this.Text = Ioc<ILoginService>.Instance?.User?.Name;
    }
    public override void LoadDefault()
    {
        base.LoadDefault();
    }
}
