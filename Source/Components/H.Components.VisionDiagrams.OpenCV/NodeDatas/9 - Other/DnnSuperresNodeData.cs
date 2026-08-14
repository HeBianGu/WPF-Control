// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using OpenCvSharp.DnnSuperres;

namespace H.Components.VisionDiagrams.OpenCV.NodeDatas.Other;
[Icon(FontIcons.DialShape1)]
[Display(Name = "超分辨率处理", GroupName = "基础函数", Description = "超分辨率处理是一项强大的技术，能够显著提升图像和视频的质量", Order = 60)]
public class DnnSuperresNodeData : OpenCVNodeDataBase, IOtherGroupableNodeData
{
    private string _algo = "fsrcnn";
    [DefaultValue("fsrcnn")]
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "算法类型", GroupName = VisionTabNames.RunParameters, Description = "选择与超分辨率模型对应的 EDSR、ESPCN、FSRCNN 或 LapSRN 算法")]
    public string Algo
    {
        get { return _algo; }
        set
        {
            _algo = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private int _scale = 4;
    [DefaultValue(4)]
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "缩放系数", GroupName = VisionTabNames.RunParameters, Description = "设置超分辨率模型训练时对应的图像放大倍数")]
    public int Scale
    {
        get { return _scale; }
        set
        {
            _scale = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private string _modelFileName = "Data/Model/FSRCNN_x4.pb";
    [DefaultValue("Data/Model/FSRCNN_x4.pb")]
    [ReadOnly(true)]
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "模型文件", GroupName = VisionTabNames.RunParameters, Description = "设置 OpenCV DNN 超分辨率模型文件路径")]
    public string ModelFileName
    {
        get { return _modelFileName; }
        set
        {
            _modelFileName = value;
            RaisePropertyChanged();
        }
    }

    //public override IFlowableResult Invoke(Part previors, Node diagram)
    //{
    //    var src = this.GetFromMat(diagram);
    //    var dnn = new DnnSuperResImpl("fsrcnn", 4);
    //    string path = GetDataPath(this.ModelFileName);
    //    dnn.ReadModel(path);
    //    //using var src = new Mat(ImagePath.Mandrill, ImreadModes.Color);
    //    var dst = new Mat();
    //    dnn.Upsample(src, dst);
    //    this.UpdateMatToView(dst);
    //    return base.Invoke(previors, diagram);
    //}

    protected override FlowableResult<IMatImage> Invoke(Mat fromImage)
    {
        Mat src = fromImage;
        using DnnSuperResImpl dnn = new DnnSuperResImpl(this.Algo, this.Scale);
        string path = this.ModelFileName.ToDataPath();
        dnn.ReadModel(path);
        //using var src = new Mat(ImagePath.Mandrill, ImreadModes.Color);
        Mat dst = new Mat();
        dnn.Upsample(src, dst);
        return this.OK(dst);
    }
}

