// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Diagram.Datas;

public interface IPortData : ILinkInitializer, IData
{
    string ID { get; set; }
    string NodeID { get; set; }
    string Name { get; set; }
    Dock Dock { get; set; }
    PortType PortType { get; set; }
    Thickness PortMargin { get; set; }
}
