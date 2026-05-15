// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")


global using H.Components.Vision.NodeGroups.Morphologys;

namespace H.Components.Visions.OpenCV.NodeDatas.Morphology;


[Icon(FontIcons.HomeGroup)]
[Display(Name = "梯度", GroupName = "形态学", Description = " 原图 - 腐蚀  ，求图形的边缘", Order = 50)]
public class Gradient : MorphologyOpenCVNodeDataBase, IMorphologyGroupableNodeData
{
    protected override MorphTypes GetMorphType()
    {
        return MorphTypes.Gradient;
    }
}
