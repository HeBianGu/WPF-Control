// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Controls.Diagram.Presenter.Flowables;
global using H.Controls.Diagram.Presenter.NodeDatas.Base;

namespace H.Components.Visions.OpenCV.Base;
public abstract class OpenCVNodeDataBase : ROINodeData<IMatImage>, IOpenCVNodeData
{
    protected override FlowableResult<IMatImage> Invoke(IMatImage fromImage)
    {
        return this.Invoke(fromImage.Mat);
    }

    protected abstract FlowableResult<IMatImage> Invoke(Mat fromImage);

    protected virtual FlowableResult<IMatImage> OK(Mat mat, string message = "运行成功")
    {
        return base.OK(new MatImage(mat), message);
    }

    protected virtual FlowableResult<IMatImage> OK(Mat mat, IResultPresenter resultPresenter, string message = "运行成功")
    {
        return base.OK(new MatImage(mat), resultPresenter, message);
    }

    protected virtual FlowableResult<IMatImage> Error(Mat mat, string message = "运行错误")
    {
        return base.Error(new MatImage(mat), message);
    }
}

