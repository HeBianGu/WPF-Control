// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Components.Vision.NodeGroups.TemplateMatchings;
using H.Components.Visions.OpenCV.NodeDatas.Detector;

namespace H.VisionMaster.OpenCVs.TemplateMatch.NodeDatas;

[Icon(FontIcons.Color)]
[Display(Name = "颜色匹配", GroupName = "模板匹配", Description = "颜色匹配", Order = 0)]
public class HSVTemplateMatch : RenderBlobs, ITemplateMatchingGroupableNodeData, IColorNodeData
{
    public override void LoadDefault()
    {
        base.LoadDefault();
        this.UseRenderBlobs = false;
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
        if (fromImage.Channels() == 1)
            return this.Error(fromImage.Clone(), "输入图像不能为灰度图像");
        Mat hsv = fromImage.ToHSVMat();
        Mat mask = new Mat();
        Tuple<Scalar, Scalar> range = this.Color.GetHSVRange(this.hRange, this.sRange, this.vRange);
        Scalar lowerScalar = range.Item1;
        Scalar upperScalar = range.Item2;
        // 需要前序流程颜色处理 ColorConversionCodes.BGR2HSV)
        Cv2.InRange(hsv, lowerScalar, upperScalar, mask);
        ////  Do ：反转黑白
        //Cv2.BitwiseNot(mask, mask);
        //this.ImageColorPickerPresenter.ImageSource = fromImage?.ToImageSource();
        return this.InvokeMat(fromImage, mask, fromImage);
    }

    protected override IEnumerable<IViewState> CreateViewStates()
    {
        return base.CreateViewStates().Concat(new PickPixelColorState(this).ToEnumerable());
    }
}

