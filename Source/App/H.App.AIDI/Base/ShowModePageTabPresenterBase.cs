// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Attributes;
using H.Extensions.FontIcon;

namespace H.App.AIDI.Base;
[Icon(FontIcons.Click)]
public abstract class ShowModePageTabPresenterBase : PageTabPresenterBase
{
    protected ShowModePageTabPresenterBase(IPagePresenter pagePresenter) : base(pagePresenter)
    {

    }
    private bool _ShowLabelName = true;
    public bool ShowLabelName
    {
        get { return _ShowLabelName; }
        set
        {
            _ShowLabelName = value;
            RaisePropertyChanged();
        }
    }

}




