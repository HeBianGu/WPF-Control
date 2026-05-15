// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Components.Visions.OpenCV.Base;

public abstract class OpenCVSrcFilesNodeDataBase : SrcFilesVisionNodeData<IMatImage>
{
    protected override FlowableResult<IMatImage> Invoke(IMatImage fromImage)
    {
        Mat mat = new Mat(this.SrcFilePath, ImreadModes.Color);
        this.PixelWidth = mat.Width;
        this.PixelHeight = mat.Height;
        this.ImageColorType = mat.Type().Value;
        this.GrayImage = new MatImage(mat.CvtColor(ColorConversionCodes.BGR2GRAY, 0));
        return this.OK(new MatImage(mat));
    }
}
