// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.Diagram.Presenter.NodeDatas.Base;

namespace H.Controls.Diagram.Presenters.OpenCV.Base;

public interface IOpenCVNodeData : IFlowableNodeData, IResultPresenterNodeData
{
    Mat Mat { get; }
}

public static class MatExtension
{
    public static ImageSource ToImageSource(this Mat mat)
    {
        if (mat?.IsDisposed != false)
            return null;
        return mat.ToBitmapSource();
    }
}
