// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Components.Visions.OpenCV.NodeDatas.Filter;
[Icon(FontIcons.InPrivate)]
[Display(Name = "基础滤波", GroupName = "滤波模块", Description = "用于图像的平滑、去噪、边缘检测等任务", Order = 0)]
public class Blur : OpenCVNodeDataBase, IBlurGroupableNodeData
{
    public override void LoadDefault()
    {
        base.LoadDefault();
        this.KSize = new System.Windows.Size(8, 8);
    }
    private System.Windows.Size _ksize = new System.Windows.Size(8, 8);
    [Display(Name = "核大小", GroupName = VisionPropertyGroupNames.RunParameters, Description = "核的大小决定了滤波的范围。核越大，平滑效果越强，但计算量也会增加")]
    public System.Windows.Size KSize
    {
        get { return _ksize; }
        set
        {
            _ksize = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private OpenCvSharp.Point? _anchor;
    [DefaultValue(null)]
    [Display(Name = "锚点", GroupName = VisionPropertyGroupNames.RunParameters, Description = "内核的锚点，表示内核的参考点。null 表示使用内核的中心作为锚点")]
    public OpenCvSharp.Point? Anchor
    {
        get { return _anchor; }
        set
        {
            _anchor = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private BorderTypes _borderType = BorderTypes.Default;
    [DefaultValue(BorderTypes.Default)]
    [Display(Name = "边界处理", GroupName = VisionPropertyGroupNames.RunParameters, Description = "由于滤波核在边界无法完全覆盖图像，需要指定如何填充边界外的像素")]
    public BorderTypes BorderType
    {
        get { return _borderType; }
        set
        {
            _borderType = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    protected override FlowableResult<IMatImage> Invoke(Mat fromImage)
    {
        Mat preMat = fromImage;
        Cv2.Blur(preMat, preMat, KSize.ToCVSize(), Anchor, BorderType);
        return this.OK(preMat.Clone());
    }
}
