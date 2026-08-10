//// Copyright (c) HeBianGu Authors. All Rights Reserved. 
//// Author: HeBianGu 
//// Github: https://github.com/HeBianGu/WPF-Control 
//// Document: https://hebiangu.github.io/WPF-Control-Docs  
//// QQ:908293466 Group:971261058 
//// bilibili: https://space.bilibili.com/370266611 
//// Licensed under the MIT License (the "License")

//namespace H.Components.VisionDiagrams.OpenCV.NodeDatas.Image;
//[Icon(FontIcons.Dial6)]
//[Display(Name = "判断图片像素大小", GroupName = "逻辑模块", Description = "设置像素阈值，根据阈值执行不同路径逻辑", Order = 20)]
//public class PixelThresholdIfConditionNodeData : OpenCVNodeDataBase, IConditionGroupableNodeData
//{
//    protected override IEnumerable<IPortData> CreatePortDatas()
//    {
//        {
//            IFlowablePortData port = CreatePortData();
//            port.Dock = Dock.Top;
//            port.PortType = PortType.Input;

//            yield return port;
//        }
//        {
//            IFlowablePortData port = CreatePortData();
//            port.Dock = Dock.Left;
//            port.PortType = PortType.OutPut;
//            port.Name = "像素小于";
//            port.Description = "像素小于阈值";
//            yield return port;
//        }
//        {
//            IFlowablePortData port = CreatePortData();
//            port.Dock = Dock.Right;
//            port.PortType = PortType.OutPut;
//            port.Name = "像素大于";
//            port.Description = "像素大于阈值";
//            yield return port;
//        }
//    }

//    private int _pixel = 500;
//    [Tab(VisionTabNames.RunParameters)]
//    [Display(Name = "像素阈值", GroupName = VisionTabNames.RunParameters)]
//    public int Pixel
//    {
//        get { return _pixel; }
//        set
//        {
//            _pixel = value;
//            RaisePropertyChanged();
//        }
//    }

//    protected override IEnumerable<Tuple<IFlowablePortData, Predicate<IFlowableLinkData>>> GetFlowablePortDatas(IFlowableDiagramData diagramData)
//    {
//        IOpenCVNodeData srcImageNodeData = diagramData.GetStartNodeDatas().OfType<IOpenCVNodeData>().FirstOrDefault();
//        IEnumerable<Tuple<IFlowablePortData, Predicate<IFlowableLinkData>>> ports = base.GetFlowablePortDatas(diagramData);
//        bool r = srcImageNodeData.ResultImage.Mat.Width > this.Pixel || srcImageNodeData.ResultImage.Mat.Height > this.Pixel;
//        return r ? ports.Where(p => p.Item1.Name == "像素大于") : ports.Where(p => p.Item1.Name == "像素小于");
//    }

//    protected override FlowableResult<IMatImage> Invoke(Mat fromImage)
//    {
//        return this.OK(fromImage);
//    }
//}
