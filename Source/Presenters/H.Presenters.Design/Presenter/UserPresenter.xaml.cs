// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.FontIcon;
using H.Services.Identity;

namespace H.Presenters.Design.Presenter;

[Icon(FontIcons.UserAPN)]
[Display(Name = "当前用户")]
public class UserPresenter : TitlePresenter, IUpdateable
{
    public UserPresenter()
    {
        this.Title = "当前用户：";
        this.UpdateData();
    }
    public override void LoadDefault()
    {
        base.LoadDefault();
    }

    public void UpdateData()
    {
        this.Text = Ioc<ILoginService>.Instance?.User?.Name;
    }
}
