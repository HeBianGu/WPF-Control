// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Morphology;

[Display(Name = "闭运算", GroupName = "形态学", Description = " 膨胀 + 腐蚀，先膨胀后腐蚀，用于填充小孔或连接断裂区域", Order = 20)]
public class Close : MorphologyOpenCVNodeDataBase
{
    protected override MorphTypes GetMorphType()
    {
        return MorphTypes.Close;
    }
}
