// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Diagram.Layouts;

[DisplayName("坐标位置")]
[TypeConverter(typeof(DisplayNameConverter))]
public class LocationLayout : Layout
{
    /// <summary> 布局点和线 </summary>
    public override void DoLayout(params Node[] nodes)
    {
        foreach (Node node in nodes)
        {
            //  Do ：布局Node
            NodeLayer.SetPosition(node, node.Location);
        }

        //this.UpdateNode(nodes);
    }

    public override void RemoveNode(params Node[] nodes)
    {
        foreach (Node node in nodes)
        {
            node.Delete();
        }
    }

    public override void AddNode(params Node[] nodes)
    {
        this.DoLayout(nodes);
    }
}
