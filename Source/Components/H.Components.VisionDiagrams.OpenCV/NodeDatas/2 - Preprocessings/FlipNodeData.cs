// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Components.VisionDiagrams.OpenCV.NodeDatas.Basic;
[Icon(FontIcons.Color)]
[Display(Name = "反转图片", GroupName = "基础函数", Description = "会改变整个图像的像素数量和分辨率", Order = 3)]
public class FlipNodeData : OpenCVNodeDataBase, IPreprocessingGroupableNodeData
{
    private FlipMode _flipMode;
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "翻转方式", GroupName = VisionTabNames.RunParameters, Description = "选择图像沿水平轴、垂直轴或双轴翻转")]
    public FlipMode FlipMode
    {
        get { return _flipMode; }
        set
        {
            _flipMode = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    protected override FlowableResult<IMatImage> Invoke(Mat fromImage)
    {
        Mat result = new Mat();
        Cv2.Flip(fromImage, result, this.FlipMode);
        return this.OK(result);
    }
}
