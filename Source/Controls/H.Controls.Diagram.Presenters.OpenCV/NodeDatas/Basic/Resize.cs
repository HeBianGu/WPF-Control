// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Basic;

[Display(Name = "图像缩放", GroupName = "基础函数", Description = "会改变整个图像的像素数量和分辨率", Order = 3)]
public class Resize : BasicOpenCVNodeDataBase
{
    private ResizeMode _resizeMode = ResizeMode.Scale;
    [RefreshOnValueChanged]
    [DefaultValue(ResizeMode.Scale)]
    [Display(Name = "缩放模式", GroupName = "数据")]
    public ResizeMode ResizeMode
    {
        get { return _resizeMode; }
        set
        {
            _resizeMode = value;
            RaisePropertyChanged();
        }
    }

    private System.Windows.Size _size = new System.Windows.Size(640, 640);
    [BindingVisiblableMethodName(nameof(GetSizeVisible))]
    [Display(Name = "固定尺寸", GroupName = "数据")]
    public System.Windows.Size Size
    {
        get { return _size; }
        set
        {
            _size = value;
            RaisePropertyChanged();
        }
    }

    public bool GetSizeVisible()
    {
        return this.ResizeMode == ResizeMode.Fixed;
    }

    private double _scale = 1.0;
    [DefaultValue(1.0)]
    [Range(0.0, double.MaxValue)]
    [BindingVisiblableMethodName(nameof(GetScaleVisible))]
    [Display(Name = "比例", GroupName = "数据")]
    public double Scale
    {
        get { return _scale; }
        set
        {
            _scale = value;
            RaisePropertyChanged();
        }
    }

    public bool GetScaleVisible()
    {
        return this.ResizeMode == ResizeMode.Scale;
    }

    protected override FlowableResult<Mat> Invoke(ISrcImageNodeData srcImageNodeData, IOpenCVNodeData from, IFlowableDiagramData diagram)
    {
        Mat resizedImage = new Mat();
        if (this.ResizeMode == ResizeMode.Scale)
            Cv2.Resize(from.Mat, resizedImage, new Size(from.Mat.Width * this.Scale, from.Mat.Height * this.Scale));
        else
            Cv2.Resize(from.Mat, resizedImage, this.Size.ToCVSize());
        return this.OK(resizedImage);
    }
}

public enum ResizeMode
{
    /// <summary>
    /// 等比缩放
    /// </summary>
    Scale,
    /// <summary>
    /// 固定尺寸
    /// </summary>
    Fixed,
}
