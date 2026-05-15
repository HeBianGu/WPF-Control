// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Windows.Threading;

namespace H.Components.Visions.OpenCV.Base;
[Icon(FontIcons.Tablet)]
public abstract class MorphologyOpenCVNodeDataBase : OpenCVNodeDataBase
{
    public override void LoadDefault()
    {
        base.LoadDefault();
        this.KernelValues = new Int32Collection(new int[] { 0, 1, 0, 1, 1, 1, 0, 1, 0 });
    }
    private OpenCvSharp.Point? _anchor = null;
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

    private int _iterations = 1;
    [DefaultValue(1)]
    [Display(Name = "迭代次数", GroupName = VisionPropertyGroupNames.RunParameters, Description = "形态学操作的迭代次数。表示操作将重复执行的次数")]
    public int Iterations
    {
        get { return _iterations; }
        set
        {
            _iterations = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private BorderTypes _borderTypes = BorderTypes.Constant;
    [DefaultValue(BorderTypes.Constant)]
    [Display(Name = "界处理方式", GroupName = VisionPropertyGroupNames.RunParameters, Description = "该参数用于指定在形态学操作过程中如何处理图像边界")]
    public BorderTypes BorderType
    {
        get { return _borderTypes; }
        set
        {
            _borderTypes = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private bool _useKernel = true;
    [DefaultValue(3)]
    [Display(Name = "使用内核", GroupName = VisionPropertyGroupNames.RunParameters, Description = "用于控制是否在形态学操作中使用内核")]
    public bool UseKernel
    {
        get { return _useKernel; }
        set
        {
            _useKernel = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private Int32Collection _kernelValues = new Int32Collection(new int[] { 0, 1, 0, 1, 1, 1, 0, 1, 0 });
    [Display(Name = "内核参数", GroupName = VisionPropertyGroupNames.RunParameters, Description = "允许用户自定义内核的形状和大小，从而影响形态学操作的结果")]
    public Int32Collection KernelValues
    {
        get { return _kernelValues; }
        set
        {
            _kernelValues = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private int _kernelRows = 3;
    [DefaultValue(3)]
    [Display(Name = "内核行数", GroupName = VisionPropertyGroupNames.RunParameters, Description = "定义了形态学操作内核的行数")]
    public int KernelRows
    {
        get { return _kernelRows; }
        set
        {
            _kernelRows = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private int _kernelCols = 3;
    [DefaultValue(3)]
    [Display(Name = "内核列数", GroupName = VisionPropertyGroupNames.RunParameters, Description = "定义了形态学操作内核的列数")]
    public int KernelCols
    {
        get { return _kernelCols; }
        set
        {
            _kernelCols = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    protected abstract MorphTypes GetMorphType();

    protected override FlowableResult<IMatImage> Invoke(Mat fromImage)
    {
        Mat src = fromImage;
        Mat dst = new Mat();

        if (this.UseKernel)
        {
            byte[] kernelValues = this.KernelValues.GetDispatcherValue(x => x.Select(x => (byte)x).ToArray()); // cross (+)
            Mat kernel = Mat.FromPixelData(KernelRows, KernelCols, MatType.CV_8UC1, kernelValues);
            Cv2.MorphologyEx(src, dst, this.GetMorphType(), kernel, Anchor, Iterations, BorderType);
        }
        else
        {
            Cv2.MorphologyEx(src, dst, this.GetMorphType(), null, Anchor, Iterations, BorderType);
        }
        return this.OK(dst);
    }
}
