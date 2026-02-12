// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.FontIcon;

namespace H.Presenters.Design.Presenter;

[Icon(FontIcons.Calendar)]
[Display(Name = "当前日期")]
public class DateTimeNowPresenter : TitlePresenter, IUpdateable
{
    public DateTimeNowPresenter()
    {
        this.Title = "当前日期：";
        this.UpdateData();

    }
    public override void LoadDefault()
    {
        base.LoadDefault();
    }

    public void UpdateData()
    {
        this.Text = DateTime.Now.ToString(this.Format);
    }

    private string _format = "yyyy-MM-dd HH:mm:ss";
    [Display(Name = "日期格式", GroupName = "常用,样式")]
    public string Format
    {
        get { return _format; }
        set
        {
            _format = value;
            RaisePropertyChanged();
        }
    }

}
