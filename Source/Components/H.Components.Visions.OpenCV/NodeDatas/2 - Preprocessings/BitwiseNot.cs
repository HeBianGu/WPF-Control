// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Controls.Diagram.Presenter.DiagramDatas.Base;

namespace H.Components.Visions.OpenCV.NodeDatas.Basic;
[Icon(FontIcons.Color)]
[Display(Name = "反转黑白", GroupName = "基础函数", Description = "二指图片的效果反转既黑色变白色，白色变黑色", Order = 20)]
public class BitwiseNot : OpenCVNodeDataBase, IPreprocessingGroupableNodeData
{
    protected override FlowableResult<IMatImage> Invoke(Mat fromImage)
    {
        if (fromImage == null)
            return this.Error(null, "数据源为空");
        Mat src = fromImage.Clone();
        Cv2.BitwiseNot(src, src);
        return this.OK(src);
    }
}

