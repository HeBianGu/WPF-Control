﻿// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Globalization;

namespace H.Controls.Diagram.Presenter.DiagramDatas;
public class NodeDataGroupsTreeViewPresenter : DisplayBindableBase
{
    public NodeDataGroupsTreeViewPresenter(IEnumerable<INodeDataGroup> nodeDataGroups)
    {
        this._nodeDataGroups = nodeDataGroups;
    }

    private IEnumerable<INodeDataGroup> _nodeDataGroups;
    public IEnumerable<INodeDataGroup> NodeDataGroups
    {
        get { return _nodeDataGroups; }
        set
        {
            _nodeDataGroups = value;
            RaisePropertyChanged();
        }
    }
}

public class GetNodeDataGroupsTreeViewPresenter : MarkupValueConverterBase
{
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is IEnumerable<INodeDataGroup> nodegroups)
            return new NodeDataGroupsTreeViewPresenter(nodegroups);
        return null;
    }
}
