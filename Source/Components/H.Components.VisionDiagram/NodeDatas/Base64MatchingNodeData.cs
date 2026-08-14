// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Common.Attributes;
global using H.Controls.Diagram.Presenter.DiagramDatas.Base;
global using H.Controls.Diagram.Presenter.Expressions;
global using H.Controls.Diagram.Presenter.Flowables;
global using H.Controls.Diagram.Presenter.NodeDatas.Base;
global using H.Controls.Form.Attributes;
global using H.Extensions.Common;
global using H.Extensions.FontIcon;
global using H.Extensions.TypeConverter;
global using System.Text.Json.Serialization;
global using System.Windows.Media.Imaging;
using H.Controls.Form.PropertyItem.Attribute;
using H.Controls.ShapeBox.Extension;
using H.Controls.ShapeBox.Shapes;
using H.Controls.ShapeBox.State.Adds;

namespace H.Components.VisionDiagram.NodeDatas;
[Icon(FontIcons.Annotation)]
[Display(Name = "截取图片", GroupName = "交互工具", Description = "在结果图像上绘制矩形并截取匹配模板")]
public class CropImageState : AddRectShapeState
{
    private readonly IBase64MatchingNodeData _nodeData;
    public CropImageState(IBase64MatchingNodeData nodeData)
    {
        this._nodeData = nodeData;
        this.Shape = new RectShape() { UseHandle = true, UseCross = true, UseDimension = true };
    }

    public override void Enter()
    {
        base.Enter();
        this.Shape.Rect = this._nodeData.Rect.IsZoreOrEmpty() ? Rect.Empty : this._nodeData.Rect;
    }

    protected override void Sumit()
    {
        base.Sumit();
        this._nodeData.Rect = this.Shape.Rect;
        if (this.Shape.Rect.IsZoreOrEmpty())
            return;
        if (this._nodeData.ResultImageSource is BitmapSource bitmapSource)
        {
            Int32Rect int32Rect = new Int32Rect((int)this.Shape.Rect.X, (int)this.Shape.Rect.Y, (int)this.Shape.Rect.Width, (int)this.Shape.Rect.Height);
            this._nodeData.Base64String = bitmapSource.ToCroppedImageBase64String(int32Rect);
        }
        ;
        ;
        this.DrawStateShape(this.Shape);
    }

    public void Refresh()
    {
        this.DrawStateShape(this.Shape);
    }
}

public interface IBase64MatchingNodeData : IDiagramableNodeData, IResultImageSourceNodeData
{
    string Base64String { get; set; }
    Rect Rect { get; set; }
}

public abstract class MatchingNodeData<T> : ROINodeData<T> where T : class, IVisionImage
{
    private IExpressionKey _ResultImageExpression;
    [GetMethodNameSource(nameof(GetImageFromExpressions))]
    [PropertyItem(typeof(InputExpressionComboBoxTextPropertyItem))]
    [Tab(VisionTabNames.DisplayParameters)]
    [Display(Name = "选择输出图像", GroupName = VisionTabNames.DisplayParameters, Description = "选择输出显示的结果图像")]
    public IExpressionKey ResultImageExpression
    {
        get { return _ResultImageExpression; }
        set
        {
            _ResultImageExpression = value;
            RaisePropertyChanged();
        }
    }

    protected T GetExpressionResultImage(T def = default)
    {
        var r = this.GetExpressionValue<T>(this.ResultImageExpression);
        if (r.success)
            return r.value;
        return def;
    }

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
    private double _confidence;
    [ReadOnly(true)]
    [Expressionable]
    [Tab(VisionTabNames.ResultParameters)]
    [Display(Name = "置信度", GroupName = VisionTabNames.ResultParameters, Description = "输出最佳模板匹配结果的置信度")]
    public double Confidence
    {
        get { return _confidence; }
        set
        {
            _confidence = value;
            RaisePropertyChanged();
        }
    }

    private Rect _Rect;
    [ReadOnly(true)]
    [TypeConverter(typeof(IntRectConverter))]
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "截图区域", GroupName = VisionTabNames.RunParameters, Description = "设置模板图像在原图中的矩形截取范围")]
    public Rect Rect
    {
        get { return _Rect; }
        set
        {
            _Rect = value;
            RaisePropertyChanged();
        }
    }

    private DetectDisplayMode _DetectDisplayMode = DetectDisplayMode.Default;
    [DefaultValue(DetectDisplayMode.Default)]
    [Tab(VisionTabNames.DisplayParameters)]
    [Display(Name = "绘制结果方式", GroupName = VisionTabNames.DisplayParameters, Description = "选择模板匹配结果在图像上的绘制方式")]
    public DetectDisplayMode DetectDisplayMode
    {
        get { return _DetectDisplayMode; }
        set
        {
            _DetectDisplayMode = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    [JsonIgnore]
    [Browsable(false)]
    [Expressionable]
    [Tab(VisionTabNames.ResultParameters)]
    [Display(Name = "识别结果列表", GroupName = VisionTabNames.ResultParameters, Description = "当前流程运行完返回的图像结果")]
    public List<IVisionResultImage<T>> ResultImages { get; protected set; } = new List<IVisionResultImage<T>>();

    [JsonIgnore]
    [Expressionable]
    [Tab(VisionTabNames.ResultParameters)]
    [Display(Name = "识别结果列表(0)", GroupName = VisionTabNames.ResultParameters, Description = "当前流程运行完返回的图像结果中第一个")]
    public virtual T FirstResultImage { get; set; }
}

