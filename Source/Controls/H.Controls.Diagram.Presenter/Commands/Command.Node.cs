// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Diagram.Presenter.Commands;

public class ApplayStyleCommand : MarkupCommandBase
{
    public bool UseApplayToAll { get; set; }

    public override void Execute(object parameter)
    {
        if (parameter is ContextMenu menu)
        {
            Node node = menu.PlacementTarget.GetParent<Node>();
            Diagram diagram = node.GetParent<Diagram>();
            if (node.Content is NodeData data)
            {
                if (this.UseApplayToAll)
                {
                    diagram.Nodes.ForEach(x =>
                    {
                        if (x.Content is NodeData nodeData)
                            data.ApplayStyleTo(nodeData);
                    });
                }
                else
                {
                    IEnumerable<NodeData> finds = diagram.Nodes.Select(x => x.Content).OfType<NodeData>().Where(x => x.GetType().IsAssignableFrom(data.GetType()));

                    foreach (NodeData item in finds)
                    {
                        data.ApplayStyleTo(item);
                    }
                }

            }
        }

    }
}
