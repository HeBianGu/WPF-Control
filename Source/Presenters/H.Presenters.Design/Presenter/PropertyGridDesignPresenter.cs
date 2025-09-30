// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Presenters.Design.Presenter;

[Display(Name = "属性列表")]
public class PropertyGridDesignPresenter : CommandsDesignPresenterBase
{
    private object _data;
    [Display(Name = "数据源", GroupName = "常用,数据")]
    public object Data
    {
        get { return _data; }
        set
        {
            _data = value;
            RaisePropertyChanged();
        }
    }
}
