// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Form;

public class TabFormPresenter : FormPresenter
{
    public TabFormPresenter()
    {

    }
    public TabFormPresenter(object value) : base(value)
    {

    }

    public ObservableCollection<string> TabNames { get; set; } = new ObservableCollection<string>();
}
