// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Diagram.Datas;

public interface IDropable
{
    /// <summary>
    /// 检查当前节点是否可以放下
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    bool CanDrop(Part part, out string message);
}
