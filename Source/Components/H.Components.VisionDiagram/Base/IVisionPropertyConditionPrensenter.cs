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

using H.Controls.FilterBox;

namespace H.Components.VisionDiagram.Base;
public interface IVisionPropertyConditionPrensenter : IPropertyConditionPrensenter
{
    int SelectedInputIndex { get; set; }
    INodeData SelectedInputNodeData { get; set; }
    int SelectedOutputIndex { get; set; }
    INodeData SelectedOutputNodeData { get; set; }

    bool IsMatchInputNode();
    void UpdateProperties(INodeData value);
}

