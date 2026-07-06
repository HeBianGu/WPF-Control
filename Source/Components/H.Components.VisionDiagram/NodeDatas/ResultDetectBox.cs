// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Components.VisionDiagram.NodeDatas;

public class ResultDetectBox
{
    public DefectBox Box { get; set; }
    public string ClassName { get; set; }
    public double Confidence { get; set; }
    public string Base64Image { get; set; }
}

public struct DefectBox
{
    public int ClassId { get; set; }
    public Rect Box { get; set; }
    public float Score { get; set; }
}

