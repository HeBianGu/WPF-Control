// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Diagram.Presenter.DiagramDatas.Base;

public class AssemblyNodeDataGroup<T> : NodeDataGroupBase where T : INodeData
{
    protected virtual IEnumerable<T> CreateAssemblyNodeDatas()
    {
        foreach (var item in this.GetType().Assembly.GetInstances<T>())
        {
            yield return item;
        }
        foreach (var item in Assembly.GetEntryAssembly().GetInstances<T>())
        {
            yield return item;
        }
    }

    protected override IEnumerable<INodeData> CreateDatas()
    {
        return this.CreateAssemblyNodeDatas<T>().OrderBy(x =>
        {
            if (x is IOrderable orderable)
            {
                return orderable.Order;
            }
            return 0;
        }).OfType<INodeData>();
    }
}


