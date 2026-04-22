// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.App.AIDI.DB;
using H.Extensions.Mvvm.ViewModels;

namespace H.App.AIDI.ViewModel;
public class ImageBindable : SelectBindable<fm_dd_image>
{
    public ImageBindable(fm_dd_image t) : base(t)
    {

    }

    public void Clear()
    {
        this.Model.Labels.Clear();
    }
}



