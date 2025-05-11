// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using System.Windows.Media;
using H.Common.Interfaces;

namespace H.Controls.Diagram.Presenters.OpenCV.Base;

public interface IOpenCVImageNodeData : INodeData, IOrderable
{

}

public abstract class OpenCVImageNodeDataBase : SrcImageNodeDataBase, IOpenCVImageNodeData
{
    public OpenCVImageNodeDataBase()
    {
        this.UseStart = true;
        this.Icon = "\xe843";
        this.SrcFilePath = this.GetImagePath().ToDataPath();
    }

    protected virtual string GetImagePath()
    {
        return null;
    }

}
