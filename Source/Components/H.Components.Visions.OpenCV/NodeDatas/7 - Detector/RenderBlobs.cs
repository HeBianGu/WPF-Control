// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.ShapeBox.Drawings;

namespace H.Components.Visions.OpenCV.NodeDatas.Detector;
[Icon(FontIcons.LargeErase)]
[Display(Name = "连通区域查找", GroupName = "查找", Order = 3)]
public class RenderBlobs : OpenCVDetectorNodeDataBase, IDetectorGroupableNodeData
{
    private PixelConnectivity _PixelConnectivity = PixelConnectivity.Connectivity4;
    [DefaultValue(PixelConnectivity.Connectivity4)]
    [Display(Name = "像素连通性", GroupName = VisionPropertyGroupNames.RunParameters, Description = "像素连通性是指在图像处理操作中如何定义像素之间的连接关系")]
    public PixelConnectivity PixelConnectivity
    {
        get { return _PixelConnectivity; }
        set
        {
            _PixelConnectivity = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private ConnectedComponentsAlgorithmsTypes _ConnectedComponentsAlgorithmsTypes = ConnectedComponentsAlgorithmsTypes.Default;
    [DefaultValue(ConnectedComponentsAlgorithmsTypes.Default)]
    [Display(Name = "连通算法类型", GroupName = VisionPropertyGroupNames.RunParameters)]
    public ConnectedComponentsAlgorithmsTypes ConnectedComponentsAlgorithmsTypes
    {
        get { return _ConnectedComponentsAlgorithmsTypes; }
        set
        {
            _ConnectedComponentsAlgorithmsTypes = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private double _minArea = 100.0;
    [PropertyItem(typeof(DoubleSliderTextPropertyItem))]
    [Range(0.0, 1000.0)]
    [DefaultValue(100.0)]
    [Display(Name = "最小面积", GroupName = VisionPropertyGroupNames.RunParameters)]
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

    private double _maxArea = 10000000.0;
    [PropertyItem(typeof(DoubleSliderTextPropertyItem))]
    [Range(0.0, 10000000.0)]
    [DefaultValue(10000000.0)]
    [Display(Name = "最大面积", GroupName = VisionPropertyGroupNames.RunParameters)]
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

    private bool _useRenderBlobs = true;
    [DefaultValue(true)]
    [Display(Name = "渲染联通区域", GroupName = VisionPropertyGroupNames.DisplayParameters)]
    public bool UseRenderBlobs
    {
        get { return _useRenderBlobs; }
        set
        {
            _useRenderBlobs = value;
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

    //protected override FlowableResult<IMatImage> Invoke(Mat fromImage)
    //{
    //    return this.InvokeMat(srcImageNodeData, fromImage, fromImage, diagram);
    //}

    protected FlowableResult<IMatImage> InvokeMat(Mat fromMat, Mat mask, Mat roiImage)
    {
        Mat preMat = mask;
        var resultImage = this.GetExpressionResultImage(fromMat.ToMatImage()).ToMatImage();
        ConnectedComponents cc = Cv2.ConnectedComponentsEx(preMat, this.PixelConnectivity, this.ConnectedComponentsAlgorithmsTypes);
        if (cc.LabelCount <= 1)
        {
            this.MatchingCountResult = 0;
            return this.OK(resultImage, "没有识别出联通区域");
        }
        //using Mat labelview = preMat.EmptyClone();
        if (this.UseRenderBlobs)
            cc.RenderBlobs(resultImage.Mat);
        List<ConnectedComponents.Blob> finds = cc.Blobs.Skip(1).Where(x => x.Area > this.MinArea && x.Area < this.MaxArea).ToList();

        if (this.CaliperShape is RectShape caliper)
            finds = finds.Where(x => caliper.Rect.Contains(x.Rect.ToWindowRect())).ToList();

        if (finds.Count == 0)
        {
            this.MatchingCountResult = 0;
            return this.OK(resultImage, "没有识别出联通区域");
        }
        //Mat result = new Mat();
        //Mat maskBlob = new Mat();
        //cc.FilterByBlobs(pMat, maskBlob, finds);
        //Cv2.AddWeighted(pMat, 0.1, maskBlob, 0.9, 15, result);

        //foreach (ConnectedComponents.Blob blob in finds)
        //{
        //    result.Rectangle(blob.Rect, VisionSettings.Instance.OutputColor.ToScalar(), result.ToThickness());
        //}

        this.MatchingCountResult = finds.Count;
        if (this.DetectDisplayMode == DetectDisplayMode.Dimension)
        {
            this.ResultShapes = finds.SelectMany(x => x.Rect.ToWindowRect().ToDimensionShapes(x => x.Text = this.GetWorldDistance(x.Length))).OfType<IShape>().ToObservable();
        }
        else if (this.DetectDisplayMode == DetectDisplayMode.Default)
        {
            this.ResultShapes = finds.Select(x => new RectShape(x.Rect.ToWindowRect())).OfType<IShape>().ToObservable();
        }
        else
        {
            var color = VisionSettings.Instance.OutputColor.ToScalar();                                          
            foreach (ConnectedComponents.Blob blob in finds)
            {
                resultImage.Mat.Rectangle(blob.Rect, color, resultImage.Mat.ToThickness());
            }
        }
        this.ResultImages = finds.Select(x => x.Rect).ToResultImages(fromMat).ToList();
        this.FirstResultImage = this.ResultImages.FirstOrDefault()?.Image;
        var resultPresenter = finds.ToResultPresenter();
        return this.OK(resultImage, resultPresenter, this.MatchingCountResult.ToDetectSuccessMessage());
    }

    protected override FlowableResult<IMatImage> Invoke(Mat fromImage)
    {
        return this.InvokeMat(fromImage, fromImage, fromImage);
    }
}