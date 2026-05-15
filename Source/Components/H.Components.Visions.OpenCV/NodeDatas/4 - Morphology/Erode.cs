// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Components.Visions.OpenCV.NodeDatas.Morphology;
[Icon(FontIcons.HomeGroup)]
[Display(Name = "腐蚀", GroupName = "形态学", Description = "缩小物体边界，去除小物体", Order = 0)]
public class Erode : MorphologyOpenCVNodeDataBase, IMorphologyGroupableNodeData
{
    protected override MorphTypes GetMorphType()
    {
        return MorphTypes.Erode;
    }
    //private Point? _anchor = null;
    //[Display(Name = "Anchor", GroupName = VisionGroupNames.RunParameters)]
    //public Point? Anchor
    //{
    //    get { return _anchor; }
    //    set
    //    {
    //        _anchor = value;
    //        RaisePropertyChanged();
    //    }
    //}

    //private int _iterations = 1;
    //[Display(Name = "Iterations", GroupName = VisionGroupNames.RunParameters)]
    //public int Iterations
    //{
    //    get { return _iterations; }
    //    set
    //    {
    //        _iterations = value;
    //        RaisePropertyChanged();
    //    }
    //}

    //private BorderTypes _borderTypes = BorderTypes.Constant;
    //[Display(Name = "BorderType", GroupName = VisionGroupNames.RunParameters)]
    //public BorderTypes BorderType
    //{
    //    get { return _borderTypes; }
    //    set
    //    {
    //        _borderTypes = value;
    //        RaisePropertyChanged();
    //    }
    //}

    //private bool _useKernel;
    //[Display(Name = "UseKernel", GroupName = VisionGroupNames.RunParameters)]
    //public bool UseKernel
    //{
    //    get { return _useKernel; }
    //    set
    //    {
    //        _useKernel = value;
    //        RaisePropertyChanged();
    //    }
    //}

    //private Int32Collection _kernelValues = new Int32Collection(new int[] { 0, 1, 0, 1, 1, 1, 0, 1, 0 });
    //[Display(Name = "KernelValues", GroupName = VisionGroupNames.RunParameters)]
    //public Int32Collection KernelValues
    //{
    //    get { return _kernelValues; }
    //    set
    //    {
    //        _kernelValues = value;
    //        RaisePropertyChanged();
    //    }
    //}

    //private int _kernelRows = 3;
    //[Display(Name = "KernelRows", GroupName = VisionGroupNames.RunParameters)]
    //public int KernelRows
    //{
    //    get { return _kernelRows; }
    //    set
    //    {
    //        _kernelRows = value;
    //        RaisePropertyChanged();
    //    }
    //}

    //private int _kernelCols = 3;
    //[Display(Name = "KernelCols", GroupName = VisionGroupNames.RunParameters)]
    //public int KernelCols
    //{
    //    get { return _kernelCols; }
    //    set
    //    {
    //        _kernelCols = value;
    //        RaisePropertyChanged();
    //    }
    //}

    //public override IFlowableResult Invoke(Part previors, Node diagram)
    //{
    //    var src = GetFromMat(diagram);
    //    var dilate = new Mat();

    //    if (UseKernel)
    //    {
    //        byte[] kernelValues = KernelValues.Select(x => (byte)x).ToArray(); // cross (+)
    //        var kernel = new Mat(KernelRows, KernelCols, MatType.CV_8UC1, kernelValues);
    //        Cv2.Erode(src, dilate, kernel, Anchor, Iterations, BorderType);
    //    }
    //    else
    //    {
    //        Cv2.Dilate(src, dilate, null, Anchor, Iterations, BorderType);
    //    }

    //    Mat = dilate;
    //    UpdateMatToView();
    //    return base.Invoke(previors, diagram);
    //}
}
