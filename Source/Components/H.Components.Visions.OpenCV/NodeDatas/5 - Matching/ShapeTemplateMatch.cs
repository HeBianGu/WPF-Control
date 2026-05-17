// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Components.Vision.NodeGroups.TemplateMatchings;
using H.Components.Vision.Presenters;
using H.Components.Visions.OpenCV.Extensions;
using H.Components.Visions.OpenCV.NodeDatas.Detector;
using H.Components.Visions.OpenCV.Presenters;
using H.Controls.ShapeBox.Drawings;
using H.Extensions.Mvvm.Commands;
using OpenCvSharp;
using System.Linq;
using static System.Formats.Asn1.AsnWriter;
using Point = OpenCvSharp.Point;

namespace H.VisionMaster.OpenCVs.TemplateMatch.NodeDatas;

/// <summary>
/// 提取图像前景的最小外界矩形（RotatedRect），并基于主成分分布与质心对图像执行自动翻转校正。
/// </summary>
[ThresholdFromNodeValidation]
[Icon(FontIcons.Color)]
[Display(Name = "轮廓匹配", GroupName = "模板匹配", Description = "使用形状匹配算法在图像中查找与模板相似的轮廓", Order = 3)]
public class ShapeTemplateMatch : MatchingNodeData<IMatImage>, ITemplateMatchingGroupableNodeData, IRectCropable, IOpenCVNodeData
{
    private double _minScore = 0.3;
    [DefaultValue(0.3)]
    [PropertyItem(typeof(DoubleSliderTextPropertyItem))]
    [Display(Name = "最小匹配分数", GroupName = VisionPropertyGroupNames.RunParameters, Order = 0)]
    [Range(0.0, 1.0)]
    public double MinScore
    {
        get => _minScore;
        set
        {
            _minScore = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private double _minArea = 100;
    [DefaultValue(100.0)]
    [Display(Name = "最小轮廓面积", GroupName = VisionPropertyGroupNames.RunParameters, Order = 1)]
    public double MinArea
    {
        get => _minArea;
        set
        {
            _minArea = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private double _maxArea = -1;
    [DefaultValue(-1)]
    [Display(Name = "最大轮廓面积", GroupName = VisionPropertyGroupNames.RunParameters, Order = 2)]
    public double MaxArea
    {
        get => _maxArea;
        set
        {
            _maxArea = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private ShapeMatchModes _shapeMatchMode = ShapeMatchModes.I1;
    [DefaultValue(ShapeMatchModes.I1)]
    [Display(Name = "匹配模式", GroupName = VisionPropertyGroupNames.RunParameters, Order = 3)]
    public ShapeMatchModes ShapeMatchMode
    {
        get => _shapeMatchMode;
        set
        {
            _shapeMatchMode = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private bool _UseShapeAngle = true;
    [DefaultValue(true)]
    [Display(Name = "显示绘制结果箭头", GroupName = VisionPropertyGroupNames.DisplayParameters, Order = 2)]
    public bool UseShapeAngle
    {
        get => _UseShapeAngle;
        set
        {
            _UseShapeAngle = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private bool _UseShapeDimension = false;
    [DefaultValue(false)]
    [Display(Name = "显示绘制结果标尺", GroupName = VisionPropertyGroupNames.DisplayParameters, Order = 2)]
    public bool UseShapeDimension
    {
        get => _UseShapeDimension;
        set
        {
            _UseShapeDimension = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private bool _UseShapeTitle = true;
    [DefaultValue(true)]
    [Display(Name = "显示绘制结果标题", GroupName = VisionPropertyGroupNames.DisplayParameters, Order = 2)]
    public bool UseShapeTitle
    {
        get => _UseShapeTitle;
        set
        {
            _UseShapeTitle = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private System.Windows.Point[][] _TemplateContours;
    //[TypeConverter(typeof(PointsPointsTypeConverter))]
    [PropertyItem(typeof(PointssShapeViewPropertyItem))]
    [DefaultValue(null)]
    [Display(Name = "形状模板数据", GroupName = VisionPropertyGroupNames.RunParameters, Order = 3)]
    public System.Windows.Point[][] TemplateContours
    {
        get => _TemplateContours;
        set
        {
            _TemplateContours = value;
            RaisePropertyChanged();
        }
    }


    // 运行参数：自动翻转控制
    private bool _autoFlipHorizontal = true;
    [Display(Name = "自动水平翻转", GroupName = VisionPropertyGroupNames.RunParameters, Description = "根据左右质量分布自动翻转图像")]
    public bool AutoFlipHorizontal
    {
        get => _autoFlipHorizontal;
        set { _autoFlipHorizontal = value; RaisePropertyChanged(); }
    }

    private bool _autoFlipVertical = true;
    [Display(Name = "自动垂直翻转", GroupName = VisionPropertyGroupNames.RunParameters, Description = "根据上下质量分布自动翻转图像")]
    public bool AutoFlipVertical
    {
        get => _autoFlipVertical;
        set { _autoFlipVertical = value; RaisePropertyChanged(); }
    }

    private double _flipSensitivity = 0.01;
    [DefaultValue(0.01)]
    [Display(Name = "翻转敏感度", GroupName = VisionPropertyGroupNames.RunParameters, Description = "左右/上下质量差异比例超过阈值触发翻转，范围[0,0.01]")]
    public double FlipSensitivity
    {
        get => _flipSensitivity;
        set { _flipSensitivity = Math.Clamp(value, 0.0, 0.01); RaisePropertyChanged(); }
    }

    // 输出结果（聚合）
    private RotatedRectFlipResult _rectFlipResult;
    [ReadOnly(true)]
    [Expressionable]
    [Display(Name = "翻转校正结果", GroupName = VisionPropertyGroupNames.ResultParameters, Description = "前景最小外接矩形与翻转判定的统一结果")]
    public RotatedRectFlipResult RectFlipResult
    {
        get => _rectFlipResult;
        set { _rectFlipResult = value; RaisePropertyChanged(); }
    }

    private DrawContourType _drawContourType = DrawContourType.DrawContours;
    [DefaultValue(DrawContourType.DrawContours)]
    [Display(Name = "绘制轮廓类型", GroupName = VisionPropertyGroupNames.RunParameters)]
    public DrawContourType DrawContourType
    {
        get { return _drawContourType; }
        set
        {
            _drawContourType = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private System.Windows.Point[][] _ResultContours;
    [Expressionable]
    [PropertyItem(typeof(PointssShapeViewPropertyItem))]
    [DefaultValue(null)]
    [Display(Name = "轮廓结果数据", GroupName = VisionPropertyGroupNames.ResultParameters, Order = 4)]
    public System.Windows.Point[][] ResultContours
    {
        get => _ResultContours;
        set
        {
            _ResultContours = value;
            RaisePropertyChanged();
        }
    }

    private NodeDataExpression _FromContoursExpression;
    [GetMethodNameSource(nameof(GetPointssFromExpressions))]
    [PropertyItem(typeof(ExpressionComboBoxPropertyItem))]
    [Display(Name = "输入轮廓数据", GroupName = VisionPropertyGroupNames.RunParameters, Description = "用来演示如何增加节点表达式参数")]
    public NodeDataExpression FromContoursExpression
    {
        get { return _FromContoursExpression; }
        set
        {
            _FromContoursExpression = value;
            RaisePropertyChanged();
        }
    }

    private System.Windows.Point[][] GetFromContours(System.Windows.Point[][] from = null)
    {
        if (this.TryGetExpressionValue(this.FromContoursExpression, out System.Windows.Point[][] image))
            return image;
        return from;
    }

    protected override FlowableResult<IMatImage> Invoke(IMatImage fromImage)
    {
        var mat = fromImage.Mat;
        if (mat == null || mat.Empty())
            return this.Error(null, "输入图像为空");
        if (this.TemplateContours == null || this.TemplateContours.Length == 0)
            return this.Error(mat.ToMatImage(), "轮廓模板数据无效");

        var fromContours = this.GetFromContours();
        if (fromContours == null)
            return this.Error(fromImage.ToMatImage(), "未能获取输入轮廓数据");
        var contours = fromContours.ToPointss();
        ///Cv2.FindContours(mat, out Point[][] contours, out _, RetrievalModes.Tree, ContourApproximationModes.ApproxNone);
        var templateContour = TemplateContours.ToPointss().OrderByDescending(c => Cv2.ContourArea(c)).FirstOrDefault();
        if (templateContour == null)
            return this.Error(fromImage.ToMatImage(), "无法从模板中提取有效轮廓");

        var resultImage = this.GetExpressionResultImage(fromImage).ToMatImage();

        List<Tuple<Point[], double>> whereContours = new List<Tuple<Point[], double>>();
        for (int i = 0; i < contours.Length; i++)
        {
            double area = Cv2.ContourArea(contours[i]);
            if ((area < this.MinArea && this.MinArea > 0) || (area > this.MaxArea && this.MaxArea > 0))
                continue;

            double score = Cv2.MatchShapes(templateContour, contours[i], ShapeMatchMode, 0);
            if (score <= this.MinScore)
                whereContours.Add(Tuple.Create(contours[i], score));
        }

        if (this.DetectDisplayMode == DetectDisplayMode.Dimension)
        {
            this.ResultShapes = whereContours.SelectMany(x => x.Item1.ToDimensionShapes(this.DrawContourType)).OfType<IShape>().ToObservable();
        }
        else if (this.DetectDisplayMode == DetectDisplayMode.Default)
        {
            this.ResultShapes = whereContours.Select(x => x.Item1.ToShape(this.DrawContourType, s => s.Title = this.Name + $"分数: {x.Item2:F10}")).OfType<IShape>().ToObservable();
        }
        else
        {
            Cv2.DrawContours(resultImage.Mat, contours, -1, Scalar.RandomColor(), 2);
        }
        var resultPresenter = this.ResultShapes.ToAutoResultPresenter();

        IRotatedRectShape first = this.ResultShapes.OfType<IRotatedRectShape>()?.FirstOrDefault();
        if (first != null)
        {
            var rectFlipResult = new RotatedRectFlipResult()
            {
                RotatedRectResult = new RotatedRect(first.Center.ToCVPoint(), first.Size.ToCVSize(), (float)first.Angle),
                IsFlippedHorizontal = this.AutoFlipHorizontal,
                IsFlippedVertical = this.AutoFlipVertical
            };
            if (!this.AutoFlipHorizontal)
                rectFlipResult = rectFlipResult with { IsFlippedHorizontal = false };
            if (!this.AutoFlipVertical)
                rectFlipResult = rectFlipResult with { IsFlippedVertical = false };
            this.RectFlipResult = rectFlipResult;
        }
        this.ResultContours = whereContours.Select(x => x.Item1).ToArray().ToPointss();
        this.MatchingCountResult = this.ResultShapes.Count;
        this.Confidence = this.MinScore;
        this.ResultImages = whereContours.Select(x => x.Item1.ToResultImage(this.DrawContourType, fromImage.Mat)).ToList();
        this.FirstResultImage = this.ResultImages.FirstOrDefault()?.Image;
        if (this.ResultShapes.Count == 0)
            return this.OK(resultImage, resultPresenter, "未找到匹配的形状");
        return this.OK(resultImage, resultPresenter, $"成功找到 {this.ResultShapes.Count} 个匹配项");
    }
}

public class PointssShapeViewPropertyItem : ShapeViewPropertyItem
{
    public PointssShapeViewPropertyItem(PropertyInfo property, object obj) : base(property, obj)
    {

    }

    protected override IEnumerable<IShape> GetShapes(object value)
    {
        if (value is System.Windows.Point[][] pointss)
        {
            return pointss.ToPointss().ToPolygonShapes();
        }
        return null;
    }

    [Icon(FontIcons.Setting)]
    [Display(Name = "模板设置")]
    public DisplayCommand ShowTemplateManagerCommand => new DisplayCommand(async x =>
    {
        if (this.Obj is ShapeTemplateMatch nodeData)
        {
            var p = new ShapeTemplateManagerPresenter(nodeData);
            var r = await IocMessage.ShowDialog(p, x =>
            {
                x.HorizontalContentAlignment = HorizontalAlignment.Center;
                x.VerticalContentAlignment = VerticalAlignment.Center;
                x.VerticalAlignment = VerticalAlignment.Center;
                x.Margin = new Thickness(50);
            });
            if (r != true)
                return;
            nodeData.TemplateContours = p.ResultTemplateContours;
            nodeData.Rect = p.Rect;
        }
    });

    [Icon(FontIcons.Delete)]
    [Display(Name = "删除模板")]
    public DisplayCommand DeleteTemplateCommand => new DisplayCommand(x =>
    {
        if (this.Obj is ShapeTemplateMatch nodeData)
        {
            nodeData.TemplateContours = null;
            nodeData.Rect = System.Windows.Rect.Empty;
        }
    });
}

