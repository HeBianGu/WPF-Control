// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Components.Visions.OpenCV.Extensions;
using H.Components.Visions.OpenCV.NodeDatas.Detector;
using Point = OpenCvSharp.Point;

namespace H.VisionMaster.OpenCV.NodeDatas.Detector;

[Icon(FontIcons.LargeErase)]
[Display(Name = "角点查找(Harris)", GroupName = "查找", Description = "基于Harris响应函数的角点检测；计算角点响应并按阈值筛选，在原图上标注角点位置", Order = 21)]
public class CornerHarris : PointDetectorNodeDataBase, IDetectorGroupableNodeData
{
    private int _blockSize = 2;
    [DefaultValue(2)]
    [Range(1, 50)]
    [Display(Name = "Block 大小", GroupName = VisionPropertyGroupNames.RunParameters, Description = "用于计算协方差矩阵的邻域大小")]
    public int BlockSize
    {
        get { return _blockSize; }
        set
        {
            _blockSize = Math.Max(1, value);
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private int _ksize = 3;
    [DefaultValue(3)]
    [Range(1, 31)]
    [Display(Name = "Sobel 核大小", GroupName = VisionPropertyGroupNames.RunParameters, Description = "Sobel 算子孔径大小（建议奇数：3/5/7/…）")]
    public int Ksize
    {
        get { return _ksize; }
        set
        {
            _ksize = Math.Max(1, value);
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private double _harrisK = 0.04;
    [PropertyItem(typeof(DoubleSliderTextPropertyItem))]
    [DefaultValue(0.04)]
    [Range(0.01, 0.2)]
    [Display(Name = "Harris k", GroupName = VisionPropertyGroupNames.RunParameters, Description = "Harris 响应函数经验系数")]
    public double HarrisK
    {
        get { return _harrisK; }
        set
        {
            _harrisK = Math.Max(0.0, value);
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private BorderTypes _BorderTypes;
    [Display(Name = "BorderTypes", GroupName = VisionPropertyGroupNames.RunParameters, Description = "BorderTypes 响应函数经验系数")]
    public BorderTypes BorderTypes
    {
        get { return _BorderTypes; }
        set
        {
            _BorderTypes = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }


    private double _responseThreshold = 200.0;
    [PropertyItem(typeof(DoubleSliderTextPropertyItem))]
    [DefaultValue(200.0)]
    [Range(0.0, 255.0)]
    [Display(Name = "响应阈值", GroupName = VisionPropertyGroupNames.RunParameters, Description = "归一化到 0~255 后的角点响应阈值")]
    public double ResponseThreshold
    {
        get { return _responseThreshold; }
        set
        {
            _responseThreshold = Math.Max(0.0, value);
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    protected override FlowableResult<IMatImage> Invoke(Mat fromImage)
    {
        var resultImage = this.GetExpressionResultImage(fromImage.ToMatImage()).ToMatImage();
        Mat gray = new Mat();
        if (fromImage.Channels() == 3 || fromImage.Channels() == 4)
            Cv2.CvtColor(fromImage, gray, ColorConversionCodes.BGR2GRAY);
        else
            gray = fromImage.Clone();

        using Mat dst = new Mat();
        Cv2.CornerHarris(gray, dst, this.BlockSize, this.Ksize, this.HarrisK, this.BorderTypes);
        gray.Dispose();

        using Mat dstNorm = new Mat();
        Cv2.Normalize(dst, dstNorm, 0, 255, NormTypes.MinMax, -1);
        List<Point2f> pts = new List<Point2f>();
        for (int y = 0; y < dstNorm.Rows; y++)
        {
            for (int x = 0; x < dstNorm.Cols; x++)
            {
                float response = dstNorm.At<float>(y, x);
                if (response <= this.ResponseThreshold)
                    continue;
                var center = new Point(x, y);
                pts.Add(center);
            }
        }

        var caliper = this.CaliperShape as CaliperPointShape;
        pts = this.SearchType switch
        {
            PointSearchType.All => pts,
            PointSearchType.Caliper => (caliper != null)
                ? pts.Where(x => caliper.Contains(x.ToPoint().ToPoint())).ToList()
                : pts,
            _ => pts,
        };
        var shapes = pts.Select(x => new PointShape(x.ToPoint().ToPoint())).ToList();
        this.MatchingCountResult = pts.Count;
        this.ResultShapes = shapes.OfType<IShape>().ToObservable();
        return this.OK(resultImage);
    }
}
