// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")


namespace H.Components.Visions.OpenCV.NodeDatas.Basic;
[Icon(FontIcons.Color)]
[Display(Name = "旋转图片", GroupName = "基础函数", Description = "会改变整个图像的像素数量和分辨率", Order = 3)]
public class Rotate : OpenCVNodeDataBase, IPreprocessingGroupableNodeData
{
    private RotateFlags _rotateFlags;
    [Display(Name = "旋转方式", GroupName = VisionPropertyGroupNames.RunParameters)]
    public RotateFlags RotateFlags
    {
        get { return _rotateFlags; }
        set
        {
            _rotateFlags = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    protected override FlowableResult<IMatImage> Invoke(Mat fromImage)
    {
        Mat result = new Mat();
        Cv2.Rotate(fromImage, result, this.RotateFlags);
        return this.OK(result);
    }
}
