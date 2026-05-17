// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Common.Attributes;
global using H.Controls.Form.PropertyItem.Attribute;
global using H.Controls.ShapeBox.State;
global using H.Extensions.FontIcon;
global using System.Text.Json.Serialization;

namespace H.Components.Visions.OpenCV.Base;
[Icon(FontIcons.Photo)]
public abstract class OpenCVDetectorNodeDataBase : OpenCVNodeDataBase
{
    //private PreviewType _detectorPreviewType = PreviewType.Src;
    //[Display(Name = "输出预览类型", GroupName = VisionPropertyGroupNames.DisplayParameters, Description = "设置从原图输出匹配结果还是上一结果中输出")]
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
    [Display(Name = "匹配数量", GroupName = VisionPropertyGroupNames.ResultParameters, Description = "结果参数，此结果可应用再条件分支等作为判断参数")]
    public int MatchingCountResult
    {
        get { return _matchingCountResult; }
        set
        {
            _matchingCountResult = value;
            RaisePropertyChanged();
        }
    }

    private NodeDataExpression _ResultImageExpression;
    [GetMethodNameSource(nameof(GetImageFromExpressions))]
    [PropertyItem(typeof(ExpressionComboBoxPropertyItem))]
    [Display(Name = "选择输出图像", GroupName = VisionPropertyGroupNames.DisplayParameters, Description = "选择前序结果图像作为此流程图像源")]
    public NodeDataExpression ResultImageExpression
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
        if (this.TryGetExpressionValue(this.ResultImageExpression, out IMatImage image))
            return image;
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
    //[Display(Name = "卡尺范围", GroupName = VisionPropertyGroupNames.BaseParameters, Order = 1000)]
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
    [Display(Name = "识别结果列表", GroupName = VisionPropertyGroupNames.ResultParameters, Description = "当前流程运行完返回的图像结果")]
    public List<IVisionResultImage<IMatImage>> ResultImages { get; protected set; } = new List<IVisionResultImage<IMatImage>>();

    [JsonIgnore]
    [Expressionable]
    [Display(Name = "识别结果列表(0)", GroupName = VisionPropertyGroupNames.ResultParameters, Description = "当前流程运行完返回的图像结果中第一个")]
    public virtual IMatImage FirstResultImage { get; set; }


    public override void Dispose()
    {
        base.Dispose();
        foreach (var item in this.ResultImages)
        {
            item.Image.Dispose();
        }
        this.FirstResultImage?.Dispose();
    }

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

    protected override FlowableResult<IMatImage> Invoke(IStartVisionNodeData<IMatImage> srcImageNodeData, IVisionNodeData<IMatImage> from, IMatImage fromImage, IFlowableDiagramData diagram)
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
    [Display(Name = "原图")]
    Src = 0,
    [Display(Name = "前图")]
    Previous = 1,
    [Display(Name = "识别结果")]
    Result = 2
}
