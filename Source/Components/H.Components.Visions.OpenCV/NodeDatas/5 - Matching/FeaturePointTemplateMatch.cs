// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Components.Vision.NodeGroups.TemplateMatchings;
using H.Components.Vision.Presenters;
using H.Components.Visions.OpenCV.NodeDatas.Detector;
using H.Components.Visions.OpenCV.Presenters;
using H.Controls.ShapeBox.Drawings;
using H.Extensions.Mvvm.Commands;
using Point = OpenCvSharp.Point;

namespace H.VisionMaster.OpenCVs.TemplateMatch.NodeDatas;

/// <summary>
/// 使用 AKAZE 特征点算法在图像中查找与模板匹配的区域。
/// </summary>
[Icon(FontIcons.Search)]
[Display(Name = "特征匹配", GroupName = "模板匹配", Description = "使用特征点匹配算法在图像中查找与模板相似的区域", Order = 4)]
public class FeaturePointTemplateMatch : MatchingNodeData<IMatImage>, ITemplateMatchingGroupableNodeData, IRectCropable, IBase64MatchingNodeData, IOpenCVNodeData, IContoursable
{
    private int _goodMatchesToKeep = 100;
    [DefaultValue(100)]
    [PropertyItem(typeof(Int32SliderTextPropertyItem))]
    [Display(Name = "保留匹配点数", GroupName = VisionPropertyGroupNames.RunParameters, Order = 0)]
    [Range(4, 100)]
    public int GoodMatchesToKeep
    {
        get => _goodMatchesToKeep;
        set
        {
            _goodMatchesToKeep = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private int _minMatchCount = 4;
    [DefaultValue(4)]
    [PropertyItem(typeof(Int32SliderTextPropertyItem))]
    [Display(Name = "最少匹配点数", GroupName = VisionPropertyGroupNames.RunParameters, Order = 1)]
    [Range(4, 100)]
    public int MinMatchCount
    {
        get => _minMatchCount;
        set
        {
            _minMatchCount = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private KeyPoint[] _KeyPoints;
    [ReadOnly(true)]
    [DefaultValue(null)]
    [PropertyItem(typeof(KeyPointShapeViewPropertyItem))]
    [Display(Name = "特征模板数据", GroupName = VisionPropertyGroupNames.RunParameters, Order = 3)]
    public KeyPoint[] KeyPoints
    {
        get => _KeyPoints;
        set
        {
            _KeyPoints = value;
            RaisePropertyChanged();
        }
    }

    private string _base64String;
    [Browsable(false)]
    public string Base64String
    {
        get { return _base64String; }
        set
        {
            _base64String = value;
            RaisePropertyChanged();
        }
    }

    protected virtual Feature2D CreateFeatureDetector()
    {
        return AKAZE.Create();
        //return ORB.Create(1000);

        //return AKAZE.Create();
        //return BRISK.Create();
        //return ORB.Create(1000);
        //return BRISK.Create();
        //return FREAK.Create();
        //return KAZE.Create();
        //return MSER.Create();
        //return StarDetector.Create();

        //return SIFT.Create();
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

    private DrawContourType _drawContourType = DrawContourType.DrawContours;
    [DefaultValue(DrawContourType.DrawContours)]
    [Display(Name = "轮廓结果类型", GroupName = VisionPropertyGroupNames.RunParameters)]
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

    protected override FlowableResult<IMatImage> Invoke(IMatImage fromImage)
    {
        var fromMat = fromImage?.Mat;
        if (fromMat == null || fromMat.Empty())
            return this.Error(fromImage.Mat.Clone().ToMatImage(), "输入图像为空");

        if (string.IsNullOrEmpty(this.Base64String))
            return this.Error(fromImage.Mat.Clone().ToMatImage(), "未设置模板图片，请先设置 ROI 以生成模板");

        //if (this.KeyPoints == null || this.KeyPoints.Length == 0)
        //    return this.Error(fromImage.Mat.Clone().ToMatImage(), "模板特征点为空，请先设置 ROI 以检测特征点");

        var roiRect = this.Rect.ToCVRect();
        roiRect.X = Math.Max(0, roiRect.X);
        roiRect.Y = Math.Max(0, roiRect.Y);
        roiRect.Width = Math.Min(roiRect.Width, fromMat.Width - roiRect.X);
        roiRect.Height = Math.Min(roiRect.Height, fromMat.Height - roiRect.Y);

        var resultImage = this.GetExpressionResultImage(fromMat.ToMatImage()).ToMatImage();
        bool hasRoi = !this.Rect.IsZoreOrEmpty() && roiRect.Width > 0 && roiRect.Height > 0;
        if (!hasRoi)
            return this.OK(resultImage, "未设置有效 ROI，无法生成模板区域");

        // 统一使用灰度提升稳定性
        using var grayFrom = new Mat();
        if (fromMat.Channels() > 1)
            Cv2.CvtColor(fromMat, grayFrom, ColorConversionCodes.BGR2GRAY);
        else
            fromMat.CopyTo(grayFrom);

        using var featrue = this.CreateFeatureDetector();
        using Mat descriptors1 = new Mat();
        using Mat descriptors2 = new Mat();
        Mat img2 = fromImage.Mat;
        using Mat templateMat = this.Base64String.ToMat();
        featrue.DetectAndCompute(templateMat, null, out KeyPoint[] keyPoints1, descriptors1);
        featrue.DetectAndCompute(img2, null, out KeyPoint[] keyPoints2, descriptors2);

        ////using AKAZE akaze = AKAZE.Create();
        //using Mat descriptors1 = new Mat();
        //using Mat descriptors2 = new Mat();

        //// 1) 模板：直接基于 this.KeyPoints（原图坐标）在整图上计算描述子
        //KeyPoint[] keyPoints1 = this.KeyPoints.ToArray(); // 避免 AKAZE.Compute 修改到原数组引用
        //akaze.Compute(grayFrom, ref keyPoints1, descriptors1);

        //// 2) 搜索图：全图检测 + 计算
        //featrue.DetectAndCompute(grayFrom, null, out KeyPoint[] keyPoints2, descriptors2);

        if (keyPoints1 == null || keyPoints2 == null || keyPoints1.Length < 2 || keyPoints2.Length < 2)
            return this.OK(resultImage, "模板或图像中的特征点过少，无法进行匹配");

        if (descriptors1.Empty() || descriptors2.Empty())
            return this.OK(resultImage, "模板或图像描述子为空，无法进行匹配");

        // 3) KNN + Lowe ratio test（比 crossCheck 更稳）
        using BFMatcher bf = new BFMatcher(NormTypes.Hamming, crossCheck: false);
        DMatch[][] knnMatches = bf.KnnMatch(descriptors1, descriptors2, k: 2);

        const double ratioThresh = 0.75;
        var goodMatches = new List<DMatch>(this.GoodMatchesToKeep);
        foreach (var pair in knnMatches)
        {
            if (pair.Length < 2)
                continue;

            if (pair[0].Distance < ratioThresh * pair[1].Distance)
                goodMatches.Add(pair[0]);
        }

        goodMatches = goodMatches
            .OrderBy(x => x.Distance)
            .Take(this.GoodMatchesToKeep)
            .ToList();

        if (goodMatches.Count < this.MinMatchCount)
            return this.OK(resultImage, $"未找到足够的匹配点，当前匹配点数: {goodMatches.Count}");

        // 4) RANSAC 单应 + inlier 检查
        Point2d[] srcPts = goodMatches
            .Select(m => keyPoints1[m.QueryIdx].Pt)
            .Select(p => new Point2d(p.X, p.Y))
            .ToArray();

        Point2d[] dstPts = goodMatches
            .Select(m => keyPoints2[m.TrainIdx].Pt)
            .Select(p => new Point2d(p.X, p.Y))
            .ToArray();

        using var inlierMask = new Mat();
        using Mat homography = Cv2.FindHomography(srcPts, dstPts, HomographyMethods.Ransac, 3.0, inlierMask);

        if (homography.Empty() || homography.Rows < 3 || homography.Cols < 3)
            return this.OK(resultImage, "无法计算变换矩阵，匹配点可能共线");

        int inlierCount = inlierMask.Empty() ? 0 : Cv2.CountNonZero(inlierMask);
        if (inlierCount < this.MinMatchCount)
            return this.OK(resultImage, $"内点过少：{inlierCount}，匹配不可靠");

        //// 5) 变换 ROI 边界：这里 homography 是 "原图坐标 -> 原图坐标"，
        //// 所以 templateBounds 必须使用原图坐标的 ROI 四角
        //Point2d[] templateBounds =
        //{
        //    new Point2d(roiRect.X, roiRect.Y),
        //    new Point2d(roiRect.X, roiRect.Y + roiRect.Height - 1),
        //    new Point2d(roiRect.X + roiRect.Width - 1, roiRect.Y + roiRect.Height - 1),
        //    new Point2d(roiRect.X + roiRect.Width - 1, roiRect.Y),
        //};

        int h = templateMat.Height, w = templateMat.Width;

        Point2d[] templateBounds = new[]
{
        new Point2d(0, 0),
        new Point2d(0, h-1),
        new Point2d(w-1, h-1),
        new Point2d(w-1, 0),
    };
        //Point2d[] img2BoundsTransformed = Cv2.PerspectiveTransform(img2Bounds, homography);

        Point2d[] transformedBounds = Cv2.PerspectiveTransform(templateBounds, homography);
        Point[] drawingPoints = transformedBounds.Select(p => (Point)p).ToArray();
        var shapes = dstPts.Select(x => x.ToPoint()).ToPointShapes(x => x.Title = "特征点").ToList();
        List<IShape> resultShapes = new List<IShape>();
        resultShapes.AddRange(shapes);


        //OpenCvSharp.Point[][] contours;
        //HierarchyIndex[] hierarchly;
        //Cv2.FindContours(homography, out contours, out hierarchly, RetrievalModes.Tree, ContourApproximationModes.ApproxSimple);

        Point[][] contours = new Point[][] { drawingPoints };
        if (this.DetectDisplayMode == DetectDisplayMode.Dimension)
        {
            var dshapes = contours.SelectMany(x => x.ToDimensionShapes(this.DrawContourType));
            resultShapes.AddRange(dshapes);
        }
        else if (this.DetectDisplayMode == DetectDisplayMode.Default)
        {
            var dshapes = contours.Select(x => x.ToShape(this.DrawContourType)).OfType<IShape>().ToObservable();
            resultShapes.AddRange(dshapes);
        }
        else
        {
            Cv2.DrawContours(resultImage.Mat, contours, -1, Scalar.RandomColor(), 2);
        }
        //var boundShape = drawingPoints.ToPolygonShape();
        //this.ResultShapes.Add(boundShape);
        this.ResultContours = contours.ToPointss();
        this.ResultShapes = resultShapes.ToObservable();
        return this.OK(resultImage, shapes.ToResultPresenter(), $"成功找到匹配项，匹配点:{goodMatches.Count}，内点:{inlierCount}");
    }
}

public class KeyPointShapeViewPropertyItem : ShapeViewPropertyItem
{
    public KeyPointShapeViewPropertyItem(PropertyInfo property, object obj) : base(property, obj)
    {

    }

    protected override IEnumerable<IShape> GetShapes(object value)
    {
        if (this.Obj is IBase64MatchingNodeData base64MatchingNodeData)
        {
            var shape = base64MatchingNodeData.Base64String.ToImageShape();
            if (shape != null)
            {
                shape.Rect = new System.Windows.Rect(base64MatchingNodeData.Rect.Location.X, base64MatchingNodeData.Rect.Location.Y, shape.Rect.Width, shape.Rect.Height);
                yield return shape;
            }
        }

        if (value is KeyPoint[] points)
        {
            foreach (var item in points.ToPointShapes())
            {
                yield return item;
            }
        }
    }

    [Icon(FontIcons.Setting)]
    [Display(Name = "模板设置")]
    public DisplayCommand ShowTemplateManagerCommand => new DisplayCommand(async x =>
    {
        if (this.Obj is FeaturePointTemplateMatch nodeData)
        {
            var p = new FeaturePointTemplateMatchManagerPresenter(nodeData);
            var r = await IocMessage.ShowDialog(p, x =>
            {
                x.HorizontalContentAlignment = HorizontalAlignment.Center;
                x.VerticalContentAlignment = VerticalAlignment.Center;
                x.VerticalAlignment = VerticalAlignment.Center;
                x.Margin = new Thickness(50);
            });
            if (r != true)
                return;
            nodeData.Base64String = p.Base64String;
            nodeData.Rect = p.Rect;
            nodeData.KeyPoints = p.ResultKeyPoints;
        }
    });

    [Icon(FontIcons.Delete)]
    [Display(Name = "删除模板")]
    public DisplayCommand DeleteTemplateCommand => new DisplayCommand(x =>
    {
        if (this.Obj is FeaturePointTemplateMatch nodeData)
        {
            nodeData.Base64String = null;
            nodeData.Rect = System.Windows.Rect.Empty;
            nodeData.KeyPoints = null;
        }
    });
}