// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Components.VisionDiagrams.OpenCV.NodeDatas.Other;

public abstract class SeamlessCloneBase : OpenCVNodeDataBase
{
    private SeamlessCloneMethods _method = SeamlessCloneMethods.NormalClone;
    [DefaultValue(SeamlessCloneMethods.NormalClone)]
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "Method", GroupName = VisionTabNames.RunParameters)]
    public SeamlessCloneMethods Method
    {
        get { return _method; }
        set
        {
            _method = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    //public override IFlowableResult Invoke(Part previors, Node diagram)
    //{
    //    var path = this.DetectFilePath;
    //    Mat src = new Mat(path, ImreadModes.Color);
    //    Mat dst = this.GetFromMat(diagram);
    //    Mat src0 = src.Resize(dst.Size(), 0, 0, InterpolationFlags.Lanczos4);
    //    Mat mask = Mat.Zeros(src0.Size(), MatType.CV_8UC3);
    //    mask.Circle(200, 200, 100, Scalar.White, -1);
    //    Mat blend = new Mat();
    //    Cv2.SeamlessClone(src0, dst, mask, this.Point, blend, this.Method);
    //    this.Mat = blend;
    //    this.UpdateMatToView();
    //    return base.Invoke(previors, diagram);
    //}
}

[Display(Name = "无缝融合", GroupName = "基础函数", Description = "将一幅图像中的指定目标复制后粘贴到另一幅图像中，并自然的融合", Order = 80)]
public class SeamlessClone : SeamlessCloneBase, IOtherGroupableNodeData
{
    public SeamlessClone()
    {
        this.DetectFilePath = "Assets\\OpenCV\\cat.jpg";
    }

    private string _detectFilePath;
    [PropertyItem(typeof(OpenFileDialogPropertyItem))]
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "检测图片路径", GroupName = VisionTabNames.RunParameters)]
    public string DetectFilePath
    {
        get { return _detectFilePath; }
        set
        {
            _detectFilePath = value;
            RaisePropertyChanged();
        }
    }

    private System.Windows.Point _point = new System.Windows.Point(260, 270);
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "放置位置", GroupName = VisionTabNames.RunParameters)]
    public System.Windows.Point Point
    {
        get { return _point; }
        set
        {
            _point = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    public override void LoadDefault()
    {
        base.LoadDefault();
        this.Point = new System.Windows.Point(260, 270);
    }

    protected override async Task<IFlowableResult> BeforeInvokeAsync(IFlowableLinkData previors, IFlowableDiagramData diagram)
    {
        if (File.Exists(this.DetectFilePath) == false)
        {
            bool? r = await IocMessage.Form?.ShowEdit(this, x => x.Title = $"{this.Name}:请先选择文件", null, x =>
            {
                x.UsePropertyNames = nameof(DetectFilePath);
            });
            if (r != true)
                return this.Error("未设置源文件地址");
        }
        return await base.BeforeInvokeAsync(previors, diagram);
    }

    protected override FlowableResult<IMatImage> Invoke(Mat fromImage)
    {
        //string path = this.DetectFilePath;
        //using Mat src = new Mat(path, ImreadModes.Color);
        //using Mat dst = from.Mat;
        //using Mat src0 = src.Resize(dst.Size(), 0, 0, InterpolationFlags.Lanczos4);
        //using Mat mask = Mat.Zeros(src0.Size(), MatType.CV_8UC3);
        //mask.Circle(200, 200, 100, Scalar.White, -1);
        //Mat blend = new Mat();
        //Cv2.SeamlessClone(src0, dst, mask, this.Point.ToCVPoint(), blend, this.Method);
        //return this.OK(blend);

        string path = this.DetectFilePath;
        if (!File.Exists(path))
            return this.Error(fromImage, "请先设置融合图片");

        using Mat src = new Mat(path, ImreadModes.Color);
        using Mat dst = fromImage;

        if (src.Width > dst.Width || src.Height > dst.Height)
            return this.Error(fromImage, "融合图片不能大于源图像尺寸");
        //using Mat src0 = src.Resize(dst.Size(), 0, 0, InterpolationFlags.Lanczos4);
        using Mat mask = new Mat(src.Size(), MatType.CV_8UC1, Scalar.White);
        //mask.Circle(200, 200, 100, Scalar.White, -1);
        OpenCvSharp.Point center = new OpenCvSharp.Point(dst.Width / 2, dst.Height / 2);
        Mat blend = new Mat();
        Cv2.SeamlessClone(src, dst, mask, center, blend, this.Method);
        return this.OK(blend);

        //// 创建掩膜（假设要插入img2到img1中）
        //Mat mask = new Mat(from.Mat.Size(), MatType.CV_8UC1, Scalar.White);
        //Point center = new Point(src.Width / 2, src.Height / 2);

        //Mat result = new Mat();
        //Cv2.SeamlessClone(from.Mat, src, mask, center, result, SeamlessCloneMethods.NormalClone);
        //return this.OK(blend);
    }
}
