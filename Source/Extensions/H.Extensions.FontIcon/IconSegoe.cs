// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Mvvm.Commands;
using H.Mvvm.ViewModels.Base;
using System.Windows;

namespace H.Extensions.FontIcon;

public class IconSegoe : BindableBase
{
    public int CodePoint { get; set; }
    public string Key { get; set; }
    public string Value { get; set; }
    public string CodeKey { get; set; }
    public RelayCommand CopyCommand => new RelayCommand(x =>
    {
        Clipboard.SetText(this.Key);
    });

    public RelayCommand CopyCodeKeyCommand => new RelayCommand(x =>
    {
        Clipboard.SetText(this.CodeKey);
    });
}
