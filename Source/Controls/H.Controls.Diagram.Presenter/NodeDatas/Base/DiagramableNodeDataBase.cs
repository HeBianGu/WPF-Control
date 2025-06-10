// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.Diagram.Presenter.Extensions;

namespace H.Controls.Diagram.Presenter.NodeDatas.Base;

public interface IDiagramableNodeData : INodeData
{
    IDiagramData DiagramData { get; }
}

public abstract class DiagramableNodeDataBase : TextNodeData, IDiagramableNodeData
{
    private IDiagramData _diagramData;
    public IDiagramData DiagramData => this._diagramData;
    protected override void Loaded(object obj)
    {
        base.Loaded(obj);
        if (obj is IDiagramData diagramData)
            _diagramData = diagramData;
    }

    protected IEnumerable<INodeData> AllFromNodeDatas => this.GetAllFromNodeDatas();

    protected IEnumerable<INodeData> FromNodeDatas => this.GetFromNodeDatas();

    protected IEnumerable<INodeData> ToNodeDatas => this.GetToNodeDatas();
}
