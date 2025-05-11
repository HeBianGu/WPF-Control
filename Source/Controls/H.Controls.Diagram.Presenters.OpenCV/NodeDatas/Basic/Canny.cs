// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Basic;
[Display(Name = "边缘检测", GroupName = "基础函数", Description = "查找图片边缘，图片>灰度>二值化>边缘检测", Order = 10)]
public class Canny : BasicOpenCVNodeDataBase
{
    private double _threshold1;
    [DefaultValue(50.0)]
    [Range(50.0, 100.0)]
    [Display(Name = "低阈值", GroupName = "数据", Description = "用于边缘连接的最小梯度值,低于此值的像素不会被考虑为边缘")]
    public double Threshold1
    {
        get { return _threshold1; }
        set
        {
            _threshold1 = value;
            RaisePropertyChanged();
        }
    }

    private double _threshold2;
    [DefaultValue(200.0)]
    [Range(150.0, 200.0)]
    [Display(Name = "高阈值", GroupName = "数据", Description = "确定强边缘的最小梯度值,高于此值的像素被确定为强边缘,通常设置为 threshold1 的 2-3 倍")]
    public double Threshold2
    {
        get { return _threshold2; }
        set
        {
            _threshold2 = value;
            RaisePropertyChanged();
        }
    }

    private int _apertureSize = 3;
    [DefaultValue(3)]
    [Display(Name = "Sobel核大小", GroupName = "数据", Description = "用于计算图像梯度的 Sobel 算子的孔径大小,可选值: 3, 5, 7,较大的孔径可以检测到更模糊的边缘，但计算量更大")]
    public int ApertureSize
    {
        get { return _apertureSize; }
        set
        {
            _apertureSize = value;
            RaisePropertyChanged();
        }
    }

    private bool _l2gradient = false;
    [DefaultValue(false)]
    [Display(Name = "梯度计算方式", GroupName = "数据", Description = "指定计算梯度幅值的方法,false (默认): 使用 L1 范数 (|dI/dx| + |dI/dy|)，计算更快,true: 使用更精确的 L2 范数 (√[(dI/dx)² + (dI/dy)²])")]
    public bool L2gradient
    {
        get { return _l2gradient; }
        set
        {
            _l2gradient = value;
            RaisePropertyChanged();
        }
    }

    protected override FlowableResult<Mat> Invoke(ISrcImageNodeData srcImageNodeData, IOpenCVNodeData from, IFlowableDiagramData diagram)
    {
        Mat preMat = from.Mat;
        Mat result = new Mat();
        Cv2.Canny(preMat, result, this.Threshold1, this.Threshold2, this.ApertureSize, this.L2gradient);
        return this.OK(result);
    }
}
