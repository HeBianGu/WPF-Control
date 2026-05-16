// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.ShapeBox.Drawings;
using H.Extensions.TypeConverter;

namespace H.Components.Visions.OpenCV.NodeDatas.Detector;
[Icon(FontIcons.LargeErase)]
[Display(Name = "轮廓查找", GroupName = "查找", Description = "二指图片的效果反转既黑色变白色，白色变黑色", Order = 21)]
public class FindContours : OpenCVDetectorNodeDataBase, IDetectorGroupableNodeData
{
    public FindContours()
    {
        this.DetectDisplayMode = DetectDisplayMode.Default;
    }
    private RetrievalModes _retrievalMode = RetrievalModes.Tree;
    [DefaultValue(RetrievalModes.Tree)]
    [Display(Name = "轮廓检索模式", GroupName = VisionPropertyGroupNames.RunParameters, Description = "设置轮廓的层级检索方式，如External、List、CComp、Tree等。影响返回的层级关系和数量。")]
    public RetrievalModes RetrievalMode
    {
        get { return _retrievalMode; }
        set
        {
            _retrievalMode = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private ContourApproximationModes _contourApproximationModes = ContourApproximationModes.ApproxNone;
    [DefaultValue(ContourApproximationModes.ApproxNone)]
    [Display(Name = "轮廓近似模式", GroupName = VisionPropertyGroupNames.RunParameters, Description = "设置轮廓点的近似策略，如None、Simple(多边形简化)、TC89L1、TC89KCOS。影响点数量与形状平滑度。")]
    public ContourApproximationModes ContourApproximationMode
    {
        get { return _contourApproximationModes; }
        set
        {
            _contourApproximationModes = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private OpenCvSharp.Point? _offset = null;
    [DefaultValue(null)]
    [Display(Name = "查找偏移量", GroupName = VisionPropertyGroupNames.RunParameters, Description = "在FindContours时为所有坐标应用的偏移量（X,Y）。用于对输入坐标进行整体平移。")]
    public OpenCvSharp.Point? Offset
    {
        get { return _offset; }
        set
        {
            _offset = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private int _contourIdx = -1;
    [DefaultValue(-1)]
    [Display(Name = "轮廓索引", GroupName = VisionPropertyGroupNames.RunParameters, Description = "指定绘制的轮廓索引；-1表示绘制所有轮廓。与MaxLevel、RetrievalMode组合使用。")]
    public int ContourIdx
    {
        get { return _contourIdx; }
        set
        {
            _contourIdx = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private LineTypes _lineType = LineTypes.Link8;
    [DefaultValue(LineTypes.Link8)]
    [Display(Name = "线型", GroupName = VisionPropertyGroupNames.RunParameters, Description = "绘制轮廓时的线段连接类型，如8连接、4连接、抗锯齿。影响线条显示效果。")]
    public LineTypes LineType
    {
        get { return _lineType; }
        set
        {
            _lineType = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private int _maxLevel = int.MaxValue;
    [DefaultValue(int.MaxValue)]
    [Display(Name = "最大层级", GroupName = VisionPropertyGroupNames.RunParameters, Description = "绘制轮廓时的层级深度：0仅绘制指定索引，1包含其子级，以此类推；设为int.MaxValue绘制所有层级。")]
    public int MaxLevel
    {
        get { return _maxLevel; }
        set
        {
            _maxLevel = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private OpenCvSharp.Point? _drawOffset;
    [DefaultValue(null)]
    [Display(Name = "绘制偏移量", GroupName = VisionPropertyGroupNames.RunParameters, Description = "在绘制轮廓时对所有点应用的偏移量（X,Y）。用于整体平移绘制位置。")]
    public OpenCvSharp.Point? DrawOffset
    {
        get { return _drawOffset; }
        set
        {
            _drawOffset = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private DrawContourType _drawContourType = DrawContourType.DrawContours;
    [DefaultValue(DrawContourType.DrawContours)]
    [Display(Name = "识别轮廓类型", GroupName = VisionPropertyGroupNames.RunParameters)]
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

    private DetectDisplayMode _DetectDisplayMode = DetectDisplayMode.Dimension;
    [DefaultValue(DetectDisplayMode.Dimension)]
    [Display(Name = "绘制结果方式", GroupName = VisionPropertyGroupNames.DisplayParameters)]
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

    private double _maxArea = -1;
    [DefaultValue(-1)]
    [Display(Name = "筛选最大面积", GroupName = VisionPropertyGroupNames.RunParameters, Description = "按面积过滤结果数据最大值")]
    public double MaxArea
    {
        get { return _maxArea; }
        set
        {
            _maxArea = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private double _minArea = -1;
    [DefaultValue(-1)]
    [Display(Name = "筛选最小面积", GroupName = VisionPropertyGroupNames.RunParameters, Description = "按面积过滤结果数据最大值")]
    public double MinArea
    {
        get { return _minArea; }
        set
        {
            _minArea = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private RotatedRect _MaxRotatedRect;
    [ReadOnly(true)]
    [Expressionable]
    [Display(Name = "最小外接矩形", GroupName = VisionPropertyGroupNames.ResultParameters, Description = "按面积过滤结果数据最大值")]
    public RotatedRect MaxRotatedRect
    {
        get { return _MaxRotatedRect; }
        set
        {
            _MaxRotatedRect = value;
            RaisePropertyChanged();
        }
    }

    public override void Dispose()
    {
        base.Dispose();

        foreach (var item in this.ResultImages)
        {
            item.Image.Dispose();
        }
    }

    protected override FlowableResult<IMatImage> Invoke(Mat fromImage)
    {
        var resultImage = this.GetExpressionResultImage(fromImage.ToMatImage()).ToMatImage();

        OpenCvSharp.Point[][] contours;
        HierarchyIndex[] hierarchly;
        Cv2.FindContours(fromImage, out contours, out hierarchly, this.RetrievalMode, this.ContourApproximationMode, this.Offset);
        //var dst = new Mat(this._srcFilePath, ImreadModes.Color);
        //Mat dst = fromImage.Clone();

        IEnumerable<OpenCvSharp.Rect> rects = contours.Select(x => Cv2.BoundingRect(x));

        if (this.MaxArea > 0)
            rects = rects.Where(x => x.Width * x.Height < this.MaxArea);
        if (this.MinArea > 0)
            rects = rects.Where(x => x.Width * x.Height > this.MinArea);

        this.ResultImages = rects.ToResultImages(fromImage).ToList();
        this.FirstResultImage = this.ResultImages.FirstOrDefault()?.Image;

        this.MaxRotatedRect = contours.Select(x => Cv2.MinAreaRect(x)).MaxBy(x =>
         {
             return x.Size.Width * x.Size.Height;
         });

        if (this.CaliperShape is RectShape caliper)
            contours = contours.Where(x => x.All(k => caliper.Rect.Contains(k.ToPoint()))).ToArray();

        if (this.DetectDisplayMode == DetectDisplayMode.Dimension)
        {
            this.ResultShapes = contours.SelectMany(x => x.ToDimensionShapes(this.DrawContourType)).OfType<IShape>().ToObservable();
        }
        else if (this.DetectDisplayMode == DetectDisplayMode.Default)
        {
            this.ResultShapes = contours.Select(x => x.ToShape(this.DrawContourType)).OfType<IShape>().ToObservable();
        }
        else
        {
            Cv2.DrawContours(resultImage.Mat, contours, -1, Scalar.RandomColor(), 2);
        }

        var resultPresenter = this.ResultShapes.ToAutoResultPresenter();
        return this.OK(resultImage, resultPresenter);
    }

    public FlowableResult<IMatImage> Method(RotatedRect rr, Mat fromImage)
    {
        // OpenCV 的 RotatedRect.Angle 定义为矩形与水平轴的角度（-90 ~ 0），
        // 为了让其长边水平，计算合适旋转角度
        float angle = rr.Angle;
        var size = rr.Size;

        // 调整角度使得宽度为水平长边
        if (size.Width < size.Height)
        {
            angle += 90f;
            (size.Width, size.Height) = (size.Height, size.Width);
        }

        // 5. 以矩形中心为旋转中心进行仿射旋转
        var center = new Point2f(rr.Center.X, rr.Center.Y);
        using var rotMat = Cv2.GetRotationMatrix2D(center, angle, 1.0);
        Mat rotated = new Mat();
        // 计算旋转后整图包络尺寸，以避免裁剪边界
        Cv2.WarpAffine(fromImage, rotated, rotMat, fromImage.Size(), InterpolationFlags.Linear, BorderTypes.Constant, new Scalar(0, 0, 0));

        // 6. 在旋转后的图上对矩形做轴对齐裁剪
        // 将旋转矩形的四点旋转到新坐标，得到裁剪区域
        var boxPts = rr.Points();
        var rotatedPts = new Point2f[4];
        for (int i = 0; i < 4; i++)
        {
            // 仿射变换应用： [x'; y'] = M * [x; y; 1]
            double x = rotMat.Get<double>(0, 0) * boxPts[i].X + rotMat.Get<double>(0, 1) * boxPts[i].Y + rotMat.Get<double>(0, 2);
            double y = rotMat.Get<double>(1, 0) * boxPts[i].X + rotMat.Get<double>(1, 1) * boxPts[i].Y + rotMat.Get<double>(1, 2);
            rotatedPts[i] = new Point2f((float)x, (float)y);
        }

        // 计算旋转后矩形的轴对齐包围盒
        float minX = rotatedPts.Min(p => p.X);
        float maxX = rotatedPts.Max(p => p.X);
        float minY = rotatedPts.Min(p => p.Y);
        float maxY = rotatedPts.Max(p => p.Y);

        var cropRect = new OpenCvSharp.Rect(
            Math.Max(0, (int)Math.Floor(minX)),
            Math.Max(0, (int)Math.Floor(minY)),
            Math.Min(rotated.Width - Math.Max(0, (int)Math.Floor(minX)), (int)Math.Ceiling(maxX - minX)),
            Math.Min(rotated.Height - Math.Max(0, (int)Math.Floor(minY)), (int)Math.Ceiling(maxY - minY))
        );

        if (cropRect.Width <= 0 || cropRect.Height <= 0)
        {
            rotated.Dispose();
            return this.OK(fromImage.Clone(), "裁剪区域无效，返回原图");
        }

        using var cropped = new Mat(rotated, cropRect);
        rotated.Dispose(); // 释放不再使用的大图

        // 7. 将裁剪后的内容居中放置到与原图同尺寸的画布中
        var canvas = new Mat(fromImage.Size(), fromImage.Type(), new Scalar(0, 0, 0));
        int dstX = (canvas.Width - cropped.Width) / 2;
        int dstY = (canvas.Height - cropped.Height) / 2;

        // 安全边界检查
        dstX = Math.Max(0, Math.Min(dstX, canvas.Width - cropped.Width));
        dstY = Math.Max(0, Math.Min(dstY, canvas.Height - cropped.Height));

        var roi = new OpenCvSharp.Rect(dstX, dstY, cropped.Width, cropped.Height);
        using (var targetROI = new Mat(canvas, roi))
        {
            cropped.CopyTo(targetROI);
        }

        return this.OK(canvas, "最小外接矩形校正完成并居中显示");
    }

}

[TypeConverter(typeof(DisplayEnumConverter))]
public enum DrawContourType
{
    [Display(Name = "轮廓")]
    DrawContours = 0,
    [Display(Name = "外接矩形")]
    BoundingRect,
    [Display(Name = "最小外接矩形")]
    MinAreaRect,
    [Display(Name = "最小凸多边形")]
    ConvexHull,
    [Display(Name = "近似多边形拟合")]
    ApproxPolyDP,
    [Display(Name = "圆形")]
    Circle,
    //[Display(Name = "椭圆")]
    //Ellipse,

}
