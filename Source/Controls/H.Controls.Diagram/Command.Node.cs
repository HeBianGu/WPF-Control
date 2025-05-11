// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Commands;
namespace H.Controls.Diagram;

public class RemoveNodeCommand : MarkupCommandBase
{
    public override void Execute(object parameter)
    {
        if (parameter is Node node)
            node.Delete();

        if (parameter is ContextMenu menu)
        {
            menu.PlacementTarget.GetParent<Node>()?.Delete();
        }
    }
}
