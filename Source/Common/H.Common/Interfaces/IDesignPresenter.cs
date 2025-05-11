// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Windows.Input;

namespace H.Common.Interfaces;

public interface IDesignPresenter : ISelectable
{
    Brush Background { get; set; }
    Brush BorderBrush { get; set; }
    Thickness BorderThickness { get; set; }
    int Column { get; set; }
    int ColumnSpan { get; set; }
    ICommand DeleteCommand { get; }
    double Height { get; set; }
    HorizontalAlignment HorizontalAlignment { get; set; }
    HorizontalAlignment HorizontalContentAlignment { get; set; }
    bool IsEnabled { get; set; }
    bool IsMouseOver { get; set; }
    bool IsVisible { get; set; }
    Thickness Margin { get; set; }
    double MinHeight { get; set; }
    double MinWidth { get; set; }
    double Opacity { get; set; }
    Thickness Padding { get; set; }
    int Row { get; set; }
    int RowSpan { get; set; }
    VerticalAlignment VerticalAlignment { get; set; }
    VerticalAlignment VerticalContentAlignment { get; set; }
    double Width { get; set; }
}
