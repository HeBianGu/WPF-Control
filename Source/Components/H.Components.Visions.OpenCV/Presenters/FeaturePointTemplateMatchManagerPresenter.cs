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

[Display(Name = "特征匹配模板设置")]
public class FeaturePointTemplateMatchManagerPresenter : TemplateManagerPresenterBase, IRectCropable
{
    private readonly FeaturePointTemplateMatch _NodeData;
    public FeaturePointTemplateMatchManagerPresenter(FeaturePointTemplateMatch nodeData) : base(nodeData)
    {
        _NodeData = nodeData;
        this.ResultKeyPoints = nodeData.KeyPoints;
        this.Rect = nodeData.Rect;
        this.Base64String = nodeData.Base64String;
    }

    private System.Windows.Rect _Rect;
    [ReadOnly(true)]
    [Display(Name = "模板范围")]
    public System.Windows.Rect Rect
    {
        get { return _Rect; }
        set
        {
            _Rect = value;
            RaisePropertyChanged();
        }
    }
    public KeyPoint[] ResultKeyPoints { get; set; }
    public string Base64String { get; set; }
    protected override IEnumerable<IViewState> GetViewStates()
    {
        var result = new FeaturePointTemplateMatchRectCropableState(this);
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
            this.ResultKeyPoints = null;
            return;
        }

        Mat mat = this.FilePath != null ? new Mat(this.FilePath, ImreadModes.Grayscale) : this._NodeData.ResultImage?.Mat.Clone();
        if (mat == null || mat.Empty())
            return;
        var roiRect = this.Rect.ToCVRect();

        // 防止越界
        roiRect.X = Math.Max(0, roiRect.X);
        roiRect.Y = Math.Max(0, roiRect.Y);
        roiRect.Width = Math.Min(roiRect.Width, mat.Width - roiRect.X);
        roiRect.Height = Math.Min(roiRect.Height, mat.Height - roiRect.Y);

        if (roiRect.Width <= 5 || roiRect.Height <= 5)
        {
            this.ResultKeyPoints = null;
            return;
        }

        using var templateImage = new Mat(mat, roiRect);
        using var grayTemplate = templateImage.ToGrayMat();
        using AKAZE akaze = AKAZE.Create();
        using Mat descriptors = new Mat();
        akaze.DetectAndCompute(grayTemplate, null, out KeyPoint[] keyPoints, descriptors);

        // 还原 ROI 坐标 -> 原图坐标
        for (int i = 0; i < keyPoints.Length; i++)
        {
            var p = keyPoints[i].Pt;
            keyPoints[i].Pt = new Point2f(p.X + roiRect.X, p.Y + roiRect.Y);
        }

        this.ResultKeyPoints = keyPoints;
        this.Base64String = templateImage.ToBase64String();

        List<IShape> shapes = new List<IShape>();
        var shape = this.Base64String.ToImageShape();
        if (shape != null)
        {
            shape.Rect = new System.Windows.Rect(this.Rect.Location.X, this.Rect.Location.Y, shape.Rect.Width, shape.Rect.Height);
            shapes.Add(shape);
        }
        shapes.AddRange(this.ResultKeyPoints.ToPointShapes());
        this.Shapes = shapes.ToObservable();
    }
}

[Icon(FontIcons.Photo)]
[Display(Name = "截取图片")]
public class FeaturePointTemplateMatchRectCropableState : RectCropableState
{
    private readonly FeaturePointTemplateMatchManagerPresenter _nodeData;
    public FeaturePointTemplateMatchRectCropableState(FeaturePointTemplateMatchManagerPresenter shapeTemplateMatch) : base(shapeTemplateMatch)
    {
        this._nodeData = shapeTemplateMatch;
    }

    protected override IEnumerable<IShape> GetStateShapes()
    {
        foreach (var item in base.GetStateShapes())
            yield return item;
        var keyPoints = this._nodeData.ResultKeyPoints;
        var shapes = keyPoints.ToPointShapes();
        if (shapes == null)
            yield break;
        foreach (var item in shapes)
            yield return item;
    }

    public override void Enter()
    {
        base.Enter();
        this.OnRectChanged(this.ROIRectShape.Rect, this.ROIRectShape.Rect);
    }
}

