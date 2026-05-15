// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.ShapeBox.Drawings;
using H.VisionMaster.OpenCVs.TemplateMatch.NodeDatas;

namespace H.Components.Visions.OpenCV.Presenters;

[Display(Name = "ÂÖÀªÆ¥ÅäÄ£°åÉèÖÃ")]
public class ShapeTemplateManagerPresenter : TemplateManagerPresenterBase, IRectCropable
{
    public ShapeTemplateManagerPresenter(ShapeTemplateMatch nodeData) : base(nodeData)
    {
        this.ResultTemplateContours = nodeData.TemplateContours;
        this.Rect = nodeData.Rect;
    }

    private System.Windows.Rect _Rect;
    [ReadOnly(true)]
    [Display(Name = "Ä£°å·¶Î§")]
    public System.Windows.Rect Rect
    {
        get { return _Rect; }
        set
        {
            _Rect = value;
            RaisePropertyChanged();
        }
    }

    private System.Windows.Point[][] _ResultTemplateContours;
    [Browsable(false)]
    [Display(Name = "Ä£°åÊý¾Ý")]
    public System.Windows.Point[][] ResultTemplateContours
    {
        get { return _ResultTemplateContours; }
        set
        {
            _ResultTemplateContours = value;
            RaisePropertyChanged();
        }
    }

    protected override IEnumerable<IViewState> GetViewStates()
    {
        var result = new ShapeTemplateMatchRectCropableState(this);
        result.RectChanged += (s, e) =>
        {
            var nRect = e;
            this.Rect = nRect;
            this.UpdateResult();
        };
        yield return result;
    }

    protected override void UpdateResult()
    {
        if (this.Rect.IsZoreOrEmpty())
        {
            this.ResultTemplateContours = null;
            return;
        }
        Mat mat = null;
        if (this.FilePath != null)
            mat = new Mat(this.FilePath, ImreadModes.Grayscale);
        if (this.SelectedImageSourceNodeData is IOpenCVNodeData openCVNodeData)
            mat = openCVNodeData.ResultImage?.Mat.Clone();
        if (mat == null || mat.Empty())
            return;
        using var gray = mat.ToGrayMat();
        var roiRect = this.Rect;
        var contours = gray.FindContours(this.Rect.ToCVRect());
        this.ResultTemplateContours = contours.ToPointss();
        this.Shapes = this.ResultTemplateContours.ToPointss().ToPolygonShapes().OfType<IShape>().ToObservable();
    }
}

[Icon(FontIcons.Crop)]
[Display(Name = "½ØÈ¡Í¼Æ¬")]
public class ShapeTemplateMatchRectCropableState : RectCropableState
{
    private readonly ShapeTemplateManagerPresenter _nodeData;
    public ShapeTemplateMatchRectCropableState(ShapeTemplateManagerPresenter shapeTemplateMatch) : base(shapeTemplateMatch)
    {
        this._nodeData = shapeTemplateMatch;
    }

    protected override IEnumerable<IShape> GetStateShapes()
    {
        foreach (var item in base.GetStateShapes())
            yield return item;
        var contours = this._nodeData.ResultTemplateContours.ToPointss();
        var shapes = contours.ToPolygonShapes(x =>
        {
            x.Stroke = this.ROIRectShape.Stroke;
            x.StrokeThickness = 3;
        });
        foreach (var item in shapes)
            yield return item;
    }

    public override void Enter()
    {
        base.Enter();
        this.OnRectChanged(this.ROIRectShape.Rect, this.ROIRectShape.Rect);
    }
}

