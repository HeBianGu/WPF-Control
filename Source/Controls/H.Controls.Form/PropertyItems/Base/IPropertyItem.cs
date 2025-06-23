// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Form.PropertyItems.Base;

public interface IPropertyItem
{
    string Name { get; set; }
    int Order { get; set; }
    string TabGroup { get; set; }
    string GroupName { get; set; }
    PropertyInfo PropertyInfo { get; set; }
    object Obj { get; set; }
}

public interface IPropertyViewItem : IPropertyItem
{

}

public interface IHitTestPropertyViewItem: IPropertyViewItem
{
    bool IsHitTestVisible { get; set; }
}