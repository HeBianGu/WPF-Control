// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Components.Vision.NodeGroups.Takeoffs;
global using H.Controls.ShapeBox.State.Base;

namespace H.Components.Visions.OpenCV.NodeDatas.Basic;

[Icon(FontIcons.Annotation)]
[Display(Name = "HSV二值分割", GroupName = "分割提取", Description = "根据设定的阈值范围，从图像中提取符合要求的像素区域，生成一个二值掩膜（Mask）", Order = 10)]
public class HSVInRange : OpenCVNodeDataBase, ITakeoffGroupableNodeData, IColorNodeData
{
    public override void LoadDefault()
    {
        base.LoadDefault();

        //// 定义红色的BGR范围（注意OpenCV默认是BGR格式）
        //Scalar lowerRed = new Scalar(0, 50, 50);    // B最小值, G最小值, R最小值
        //Scalar upperRed = new Scalar(10, 255, 200);  // B最大值, G最大值, R最大值

        //// 红色需要两个范围（色相在 0° 和 180° 附近）
        //Scalar lowerRed1 = new Scalar(0, 70, 30);    // 色相 0° 附近，饱和度和明度下限
        //Scalar upperRed1 = new Scalar(8, 255, 150);  // 色相 8° 范围，饱和度和明度上限

        //Scalar lowerRed2 = new Scalar(172, 70, 30);  // 色相 172° 附近（紫红色调）
        //Scalar upperRed2 = new Scalar(180, 255, 150); // 色相 180° 范围

        //// 定义绿色的HSV范围
        //Scalar lowerGreen = new Scalar(35, 50, 50);   // H最小值, S最小值, V最小值
        //Scalar upperGreen = new Scalar(85, 255, 255); // H最大值, S最大值, V最R最大值
        //this._lowerScalar = lowerGreen;
        //this._upperScalar = upperGreen;
    }

    private bool _UseInRange = true;
    [DefaultValue(true)]
    [Display(Name = "启用二值分割", GroupName = VisionPropertyGroupNames.RunParameters, Description = "禁用时可以从工具栏中拾取来源图像主色")]
    public bool UseInRange
    {
        get { return _UseInRange; }
        set
        {
            _UseInRange = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private Color _Color = Colors.Green;
    [Display(Name = "设置主色", GroupName = VisionPropertyGroupNames.RunParameters, Description = "从图片提取颜色或手动设置色相")]
    public Color Color
    {
        get { return _Color; }
        set
        {
            _Color = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private int _hRange;
    [PropertyItem(typeof(Int32SliderTextPropertyItem))]
    [Display(Name = "H色相生成范围", GroupName = VisionPropertyGroupNames.RunParameters, Description = "用于控制吸管工具生成上下限参数")]
    [DefaultValue(35)]
    [Range(0, 85)]
    public int hRange
    {
        get { return _hRange; }
        set
        {
            _hRange = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private int _sRange;
    [PropertyItem(typeof(Int32SliderTextPropertyItem))]
    [Display(Name = "S饱和度生成范围", GroupName = VisionPropertyGroupNames.RunParameters, Description = "用于控制吸管工具生成上下限参数")]
    [DefaultValue(30)]
    [Range(0, 255)]
    public int sRange
    {
        get { return _sRange; }
        set
        {
            _sRange = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private int _vRange;
    [PropertyItem(typeof(Int32SliderTextPropertyItem))]
    [Display(Name = "V明度生成范围", GroupName = VisionPropertyGroupNames.RunParameters, Description = "用于控制吸管工具生成上下限参数")]
    [DefaultValue(30)]
    [Range(0, 255)]
    public int vRange
    {
        get { return _vRange; }
        set
        {
            _vRange = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    protected override FlowableResult<IMatImage> Invoke(Mat fromImage)
    {
        if (!this.UseInRange)
            return this.OK(fromImage.Clone());
        Mat hsv = fromImage.CvtColor(ColorConversionCodes.BGR2HSV);
        Mat mask = new Mat();
        Tuple<Scalar, Scalar> range = this.Color.GetHSVRange(this.hRange, this.sRange, this.vRange);
        Scalar lowerScalar = range.Item1;
        Scalar upperScalar = range.Item2;
        // 需要前序流程颜色处理 ColorConversionCodes.BGR2HSV)
        Cv2.InRange(hsv, lowerScalar, upperScalar, mask);
        ////  Do ：反转黑白
        //Cv2.BitwiseNot(mask, mask);
        //this.ImageColorPickerPresenter.ImageSource = fromImage.ToImageSource();
        return this.OK(mask);
    }

    protected override IEnumerable<IViewState> CreateViewStates()
    {
        return base.CreateViewStates().Concat(new PickPixelColorState(this).ToEnumerable());
    }
}

