// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.ShapeBox.State.Adds;
[Icon(FontIcons.ChromeFullScreen)]
[Display(Name = "绘制标尺")]
public class AddRulerLineShapeState : AddFromToShapeState<RulerLineShape>
{
    public AddRulerLineShapeState(IShapes shapes) : base(shapes)
    {
        this.Shape.UseHandle = false;
        //this.Shape.UseText = true;
    }
    public AddRulerLineShapeState()
    {
        this.Shape.UseHandle = false;
        //this.Shape.UseText = true;
    }

    protected override void Sumit()
    {
        base.Sumit();
        this.RefreshStateShapes();
    }

    public override IShapeStyleSetting GetShapeStyleSetting()
    {
        return RulerLineShapeStyleSetting.Instance;
    }
}
