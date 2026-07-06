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

[Display(Name = "仿射变换", GroupName = "基础函数", Description = "是一种几何变换，用于对图像进行线性变换，图像旋转，平移，缩放，剪切倾斜，对齐，校正", Order = 4)]
public class WarpAffineTransform : OpenCVNodeDataBase, IOtherGroupableNodeData
{
    public override void LoadDefault()
    {
        base.LoadDefault();
        this._srcPoints = new PointCollection { new System.Windows.Point(0, 0), new System.Windows.Point(1, 0), new System.Windows.Point(0, 1) };
        this._dstPoints = new PointCollection { new System.Windows.Point(0, 0), new System.Windows.Point(1, 0), new System.Windows.Point(0, 1) };
    }
    private PointCollection _srcPoints;
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "源点", GroupName = VisionTabNames.RunParameters)]
    public PointCollection SrcPoints
    {
        get { return _srcPoints; }
        set
        {
            _srcPoints = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }
    private PointCollection _dstPoints;
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "目标点", GroupName = VisionTabNames.RunParameters)]
    public PointCollection DstPoints
    {
        get { return _dstPoints; }
        set
        {
            _dstPoints = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    //private Matrix _matrix;
    //public Matrix Matrix
    //{
    //    get { return _matrix; }
    //    set
    //    {
    //        _matrix = value;
    //        RaisePropertyChanged();
    //    }
    //}

    protected override FlowableResult<IMatImage> Invoke(Mat fromImage)
    {
        var scount = _srcPoints.Dispatcher.Invoke(() => _srcPoints.Count);
        var dcount = _dstPoints.Dispatcher.Invoke(() => _dstPoints.Count);
        if (scount != 3 || dcount != 3)
            return this.Error(fromImage, "源点和目标点必须包含三个点。");

        //// 定义旋转中心和角度
        //Point2f center = new Point2f(src.Cols / 2, src.Rows / 2);
        //double angle = 45.0; // 旋转角度
        //double scale = 1.0;  // 缩放比例
        //// 计算旋转矩阵
        //Mat rotationMatrix = Cv2.GetRotationMatrix2D(center, angle, scale);

        IEnumerable<Point2f> src = this._srcPoints.Dispatcher.Invoke(() => this._srcPoints.Select(p => new Point2f((float)p.X, (float)p.Y)).ToList());
        IEnumerable<Point2f> dst = this._dstPoints.Dispatcher.Invoke(() => this._dstPoints.Select(p => new Point2f((float)p.X, (float)p.Y)).ToList());
        Mat transformMatrix = Cv2.GetAffineTransform(src, dst);
        Mat transformedImage = new Mat();
        Cv2.WarpAffine(fromImage, transformedImage, transformMatrix, fromImage.Size());
        return this.OK(transformedImage);
    }
}
