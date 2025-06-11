// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Windows.Controls;
using H.Controls.Diagram.Presenter.Extensions;

namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Image;
[Display(Name = "判断图片像素大小", GroupName = "判断条件", Description = "设置像素阈值，根据阈值执行不同路径逻辑", Order = 20)]
public class PixelThresholdIfCondition : IfConditionNodeDataBase
{
    protected override IEnumerable<IPortData> CreatePortDatas()
    {
        {
            var port = CreatePortData();
            port.Dock = Dock.Top;
            port.PortType = PortType.Input;

            yield return port;
        }
        {
            var port = CreatePortData();
            port.Dock = Dock.Left;
            port.PortType = PortType.OutPut;
            port.Name = "像素小于";
            port.Description = "像素小于阈值";
            yield return port;
        }
        {
            var port = CreatePortData();
            port.Dock = Dock.Right;
            port.PortType = PortType.OutPut;
            port.Name = "像素大于";
            port.Description = "像素大于阈值";
            yield return port;
        }
    }

    private int _pixel = 500;
    [Display(Name = "像素阈值", GroupName = "数据")]
    public int Pixel
    {
        get { return _pixel; }
        set
        {
            _pixel = value;
            RaisePropertyChanged();
        }
    }

    protected override IEnumerable<Tuple<IFlowablePortData, Predicate<IFlowableLinkData>>> GetFlowablePortDatas(IFlowableDiagramData diagramData)
    {
        var srcImageNodeData = diagramData.GetStartNodeDatas().OfType<ISrcImageNodeData>().FirstOrDefault();
        var ports = base.GetFlowablePortDatas(diagramData);
        bool r = srcImageNodeData.Mat.Width > this.Pixel || srcImageNodeData.Mat.Height > this.Pixel;
        if (r)
        {
            return ports.Where(p => p.Item1.Name == "像素大于");
        }
        else
        {
            return ports.Where(p => p.Item1.Name == "像素小于");
        }
    }

    protected override FlowableResult<Mat> Invoke(ISrcImageNodeData srcImageNodeData, IOpenCVNodeData from, IFlowableDiagramData diagram)
    {
        return this.OK(from.Mat);
    }
}
