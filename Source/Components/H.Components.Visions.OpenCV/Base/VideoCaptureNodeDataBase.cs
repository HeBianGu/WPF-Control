// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Components.Vision.Base;
global using H.Components.Visions.OpenCV.NodeDatas.Src;

namespace H.Components.Visions.OpenCV.Base;
[Icon(FontIcons.Camera)]
public abstract class VideoCaptureNodeDataBase : OpenCVSrcFilesNodeDataBase, IVideoCaptureNodeData
{
    public async Task<IFlowableResult> InvokeVideoFlowable(IFlowableDiagramData diagram, Func<Task<IFlowableResult>> action)
    {
        IEnumerable<IVideoFlowable> videos = diagram.NodeDatas.OfType<IVideoFlowable>();
        foreach (IVideoFlowable video in videos)
        {
            video.Begin();
        }
        Task<IFlowableResult> r = action.Invoke();
        foreach (IVideoFlowable video in videos)
        {
            video.End();
        }
        return await r;
    }

    protected override FlowableResult<IMatImage> Invoke(IMatImage fromImage)
    {
        return this.OK(null);
    }
}

