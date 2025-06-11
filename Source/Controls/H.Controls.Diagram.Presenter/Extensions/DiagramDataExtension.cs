// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.Diagram.Presenter.DiagramDatas.Base;
using H.Presenters.Common;

namespace H.Controls.Diagram.Presenter.Extensions;


public static class DiagramDataExtension
{

    public static IEnumerable<INodeData> GetStartNodeDatas(this IDiagramData diagramData)
    {
        return diagramData.NodeDatas.Where(x =>
        {
            var finds = x.GetFromNodeDatas(diagramData);
            if (x is IPortableNodeData portable && portable.PortDatas.Count(x => x.PortType == PortType.OutPut) == 0)
                return false;
            return finds == null || finds.Count() == 0;
        });
    }

    public static async Task<T> TryGetStartNodeData<T>(this IDiagramData diagramData) where T : INodeData
    {
        var starts = diagramData.GetStartNodeDatas().OfType<T>();
        if (starts == null || starts.Count() == 0)
        {
            await IocMessage.ShowDialogMessage("未找到起始节点");
            return default;
        }

        if (starts.Count() > 1)
        {
            var r = await IocMessage.Dialog.ShowListBox<T>(x =>
            {
                x.ItemsSource = starts.ToList();
                //x.DisplayMemberPath = "Name";
                x.SelectedItem = starts.FirstOrDefault();
                x.UseDelete = false;
            }, null, x => x.Title = "存在多个起始节点，请选择一个执行");
            if (r == null)
            {
                await IocMessage.ShowDialogMessage("存在多个起始节点");
                return default;
            }
            return r;
        }
        return starts.First();
    }

    public static IEnumerable<INodeData> GetEndNodeDatas(this IDiagramData diagramData)
    {
        return diagramData.NodeDatas.Where(x => x.GetToNodeDatas(diagramData) == null);
    }
    public static IEnumerable<IPortData> GetPortDatas(this IDiagramData diagramData)
    {
        return diagramData.NodeDatas.SelectMany(x => x.GetPortDatas(diagramData));
    }

    public static void DeleteNodeData(this IDiagramData diagramData, INodeData nodeData)
    {
        var links = nodeData.GetLinks(diagramData);
        foreach (var item in links)
        {
            diagramData.LinkDatas.Remove(item);
        }
        diagramData.NodeDatas.Remove(nodeData);
    }

    public static void DeletePartData(this IDiagramData diagramData, IPartData partData)
    {
        if (partData is INodeData nodeData)
            diagramData.DeleteNodeData(nodeData);
        if (partData is ILinkData linkData)
            diagramData.LinkDatas.Remove(linkData);
        if (partData is IPortData portData)
            return;
    }
}

