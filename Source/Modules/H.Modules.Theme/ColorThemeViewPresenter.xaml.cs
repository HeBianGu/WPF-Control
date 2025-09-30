// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using Microsoft.Extensions.Options;

namespace H.Modules.Theme;
public class ColorThemeViewPresenter : IColorThemeViewPresenter
{
    private readonly IOptions<ThemeOptions> _options;
    public ColorThemeViewPresenter(IOptions<ThemeOptions> options)
    {
        _options = options;
    }

    //private ObservableCollection<IColorResource> _collection = new ObservableCollection<IColorResource>();
    ///// <summary> 说明  </summary>
    //public ObservableCollection<IColorResource> Collection
    //{
    //    get { return _collection; }
    //    set
    //    {
    //        _collection = value;
    //    }
    //}

}
