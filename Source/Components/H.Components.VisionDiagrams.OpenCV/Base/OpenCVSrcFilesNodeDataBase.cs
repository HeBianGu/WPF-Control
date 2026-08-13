// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Services.AppPath;

namespace H.Components.VisionDiagrams.OpenCV.Base;

public abstract class OpenCVSrcFilesNodeDataBase : SrcFilesVisionNodeData<IMatImage>
{
    protected override FlowableResult<IMatImage> Invoke(IMatImage fromImage)
    {
        Mat mat = new Mat(this.SrcFilePath.SrcFilePath, ImreadModes.Color);
        this.PixelWidth = mat.Width;
        this.PixelHeight = mat.Height;
        this.ImageColorType = mat.Type().Value;
        if (mat.IsValid())
            this.GrayImage = new MatImage(mat.CvtColor(ColorConversionCodes.BGR2GRAY, 0));
        return this.OK(new MatImage(mat));
    }
}
