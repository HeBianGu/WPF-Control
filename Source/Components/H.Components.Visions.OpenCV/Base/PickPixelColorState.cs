// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.ShapeBox.State.Adds;

namespace H.Components.Visions.OpenCV.Base;
[Icon(FontIcons.Color)]
[Display(Name = "拾取颜色")]
public class PickPixelColorState : AddPickPixelShapeState
{
    private readonly IColorNodeData _nodeData;
    public PickPixelColorState(IColorNodeData nodeData) : base()
    {
        this._nodeData = nodeData;
    }

    protected override void Sumit()
    {
        base.Sumit();
        this._nodeData.Color = this.Shape.Color;
        this.DrawStateShape(this.Shape);
    }
}

