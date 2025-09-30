// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Diagram.Layouts.Base;

public interface ILayout
{
    Diagram Diagram { get; set; }
    void DoLayout(params Node[] nodes);
    void UpdateNode(params Node[] nodes);
    void RemoveNode(params Node[] nodes);
    void AddNode(params Node[] nodes);
    void DoLayoutLink(Link link);
}
