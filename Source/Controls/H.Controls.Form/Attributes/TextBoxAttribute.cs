// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Form.Attributes;

public class TextBoxAttribute : Attribute
{
    //public TextBoxAttribute(TextWrapping textWrapping)
    //{
    //    this.TextWrapping = textWrapping;
    //}
    public TextWrapping TextWrapping { get; set; }
    public bool UseClear { get; set; }
}
