// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.Form.PropertyItem.Attribute;
using H.Controls.ShapeBox.State;
using H.Controls.ShapeBox.State.Base;
using System.Text.Json.Serialization;

namespace H.Components.VisionDiagrams.OpenCV.Base;
[Icon(FontIcons.Photo)]
public abstract class OpenCVDetectorNodeDataBase : OpenCVNodeDataBase
{
    //private PreviewType _detectorPreviewType = PreviewType.Src;
    //[Display(Name = "输出预览类型", GroupName = VisionTabNames.DisplayParameters, Description = "设置从原图输出匹配结果还是上一结果中输出")]
    //public PreviewType DetectorPreviewType
    //{
    //    get { return _detectorPreviewType; }
    //    set
    //    {
    //        _detectorPreviewType = value;
    //        RaisePropertyChanged();
    //    }
    //}

    private int _matchingCountResult;
    [ReadOnly(true)]
    [Expressionable]
    [Tab(VisionTabNames.ResultParameters)]
    [Display(Name = "匹配数量", GroupName = VisionTabNames.ResultParameters, Description = "结果参数，此结果可应用再条件分支等作为判断参数")]
    public int MatchingCountResult
    {
        get { return _matchingCountResult; }
        set
        {
            _matchingCountResult = value;
            RaisePropertyChanged();
        }
    }

    private IExpressionKey _ResultImageExpression;
    [GetMethodNameSource(nameof(GetImageFromExpressions))]
    [PropertyItem(typeof(InputExpressionComboBoxTextPropertyItem))]
    [Tab(VisionTabNames.DisplayParameters)]
    [Display(Name = "选择输出图像", GroupName = VisionTabNames.DisplayParameters, Description = "选择前序结果图像作为此流程图像源")]
    public IExpressionKey ResultImageExpression
    {
        get { return _ResultImageExpression; }
        set
        {
            _ResultImageExpression = value;
            RaisePropertyChanged();
        }
    }

    protected IMatImage GetExpressionResultImage(IMatImage def = default)
    {
        var r = this.GetExpressionValue<IMatImage>(this.ResultImageExpression);
        if (r.success)
            return r.value;
        return def;
    }

    //protected virtual Mat GetPrviewMat(Mat from)
    //{
    //    var preview = from;
    //    if (this.TryGetExpressionValue(this._ResultImageExpression, out MatImage image))
    //        preview = image.Mat.Clone();
    //    if (preview?.IsValid() == true)
    //        return preview?.Clone();
    //    return from.Clone();
    //}

    //private System.Windows.Rect _CaliperRect;
    ////[DisplayMemberPath("Name")]
    ////[MethodNameSourcePropertyItem(typeof(PresenterComboBoxPropertyItem), nameof(GetROIs))]
    //[Display(Name = "卡尺范围", GroupName = VisionTabNames.BaseParameters, Order = 1000)]
    //public System.Windows.Rect CaliperRect
    //{
    //    get { return _CaliperRect; }
    //    set
    //    {
    //        _CaliperRect = value;
    //        RaisePropertyChanged();
    //    }
    //}

    private IShape _CaliperShape;
    public IShape CaliperShape
    {
        get { return _CaliperShape; }
        set
        {
            _CaliperShape = value;
            RaisePropertyChanged();
            this.UpdateViewStates();
        }
    }

    [JsonIgnore]
    [Browsable(false)]
    [Expressionable]
    [Tab(VisionTabNames.ResultParameters)]
    [Display(Name = "识别结果列表", GroupName = VisionTabNames.ResultParameters, Description = "当前流程运行完返回的图像结果")]
    public List<IVisionResultImage<IMatImage>> ResultImages { get; protected set; } = new List<IVisionResultImage<IMatImage>>();

    [JsonIgnore]
    [Expressionable]
    [Tab(VisionTabNames.ResultParameters)]
    [Display(Name = "识别结果列表(0)", GroupName = VisionTabNames.ResultParameters, Description = "当前流程运行完返回的图像结果中第一个")]
    public virtual IMatImage FirstResultImage { get; set; }

    protected override IEnumerable<IViewState> CreateViewStates()
    {
        return base.CreateViewStates().Concat(new ShowShapeState<IShape>() { Shape = this.CaliperShape }.ToEnumerable());
    }

    protected virtual IShape CreateCaliperShape(Mat fromImage)
    {
        RectShape rectShape = new RectShape()
        {
            Rect = fromImage.ToRect(),
            UseHandle = true
        };
        return rectShape;
    }

    protected override FlowableResult<IMatImage> Invoke(IStartVisionNodeData srcImageNodeData, IVisionNodeData from, IMatImage fromImage, IFlowableDiagramData diagram)
    {
        if (this.CaliperShape == null)
            this.CaliperShape = this.CreateCaliperShape(fromImage.Mat);
        return base.Invoke(srcImageNodeData, from, fromImage, diagram);
    }

    //protected override FlowableResult<IMatImage> Invoke(Mat fromImage)
    //{
    //    Mat roiImage = fromImage;
    //    Rect? rect = null;
    //    if (this.CaliperShape != null && !this.CaliperShape.BoundingBox.IsZoreOrEmpty())
    //    {
    //        roiImage = fromImage.ToROIImage(this.CaliperShape.BoundingBox.ToCVRect());
    //        rect = this.CaliperShape.BoundingBox.ToCVRect();
    //    }
    //    return this.InvokeCaliperRect(roiImage, rect);
    //}

    //protected abstract FlowableResult<Mat> InvokeCaliperRect(Mat roiImage, Rect? roi);
}

public enum PreviewType
{
    [Display(Name = "原图", GroupName = "预览选项", Description = "使用流程输入的原始图像进行结果预览")]
    Src = 0,
    [Display(Name = "前图", GroupName = "预览选项", Description = "使用前一节点输出的图像进行结果预览")]
    Previous = 1,
    [Display(Name = "识别结果", GroupName = "预览选项", Description = "使用当前检测节点生成的识别结果图像进行预览")]
    Result = 2
}
