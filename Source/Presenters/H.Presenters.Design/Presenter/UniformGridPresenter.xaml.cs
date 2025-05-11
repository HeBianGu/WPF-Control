// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Presenters.Design.Presenter;

[Display(Name = "Uniform")]
public class UniformGridPresenter : PanelPresenterBase
{
    private int _columns = 6;
    [DefaultValue(6)]
    [Display(Name = "列数", GroupName = "常用,布局")]
    public int Columns
    {
        get { return _columns; }
        set
        {
            _columns = value;
            RaisePropertyChanged();
        }
    }

    private int _rows = 6;
    [DefaultValue(6)]
    [Display(Name = "行数", GroupName = "常用,布局")]
    public int Rows
    {
        get { return _rows; }
        set
        {
            _rows = value;
            RaisePropertyChanged();
        }
    }
}
