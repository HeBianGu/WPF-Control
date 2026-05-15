// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Components.Vision.NodeGroups.TemplateMatchings;
using H.Components.Vision.Presenters;
using H.Components.Visions.OpenCV.Presenters;
using H.Extensions.Mvvm.Commands;
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

    protected override FlowableResult<IMatImage> Invoke(IMatImage fromImage)
    {
        var mat = fromImage.Mat;
        if (mat == null || mat.Empty())
            return this.Error(null, "输入图像为空");

        if (TemplateContours == null || TemplateContours.Length == 0)
            return this.Error(mat.ToMatImage(), "轮廓模板数据无效");

        Cv2.FindContours(mat, out Point[][] contours, out _, RetrievalModes.Tree, ContourApproximationModes.ApproxNone);
        var templateContour = TemplateContours.ToPointss().OrderByDescending(c => Cv2.ContourArea(c)).FirstOrDefault();
        if (templateContour == null)
            return this.Error(fromImage.ToMatImage(), "无法从模板中提取有效轮廓");

        List<RotatedRect> rotatedRects = new List<RotatedRect>();
        List<RotatedRectShape> resultShapes = new List<RotatedRectShape>();
        for (int i = 0; i < contours.Length; i++)
        {
            double area = Cv2.ContourArea(contours[i]);
            if ((area < MinArea && MinArea > 0) || (area > MaxArea && MaxArea > 0))
                continue;

            double score = Cv2.MatchShapes(templateContour, contours[i], ShapeMatchMode, 0);
            if (score <= MinScore)
            {
                RotatedRect rotatedRect = Cv2.MinAreaRect(contours[i]);
                rotatedRects.Add(rotatedRect);
                var shape = rotatedRect.ToRotatedRectShape(x =>
                {
                    x.UseAngle = this.UseShapeAngle;
                    x.UseDimension = this.UseShapeDimension;
                    x.UseTitle = this.UseShapeTitle;
                });
                shape.Title = $"分数: {score:F10}";
                resultShapes.Add(shape);
                //Cv2.DrawContours(resultImage, contours, i, Scalar.RandomColor(), 2);
            }
        }

        var first = resultShapes.FirstOrDefault();
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

        var resultImage = this.GetExpressionResultImage(fromImage).ToMatImage();
        this.MatchingCountResult = resultShapes.Count;
        this.Confidence = this.MinScore;
        this.ResultShapes = resultShapes.OfType<IShape>().ToObservable();
        this.ResultImages = rotatedRects.ToResultImages(mat).ToList();
        this.FirstResultImage = this.ResultImages.FirstOrDefault()?.Image;
        if (resultShapes.Count == 0)
            return this.OK(resultImage, "未找到匹配的形状");
        return this.OK(resultImage, resultShapes.ToResultPresenter(), $"成功找到 {resultShapes.Count} 个匹配项");
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

