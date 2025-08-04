// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Attributes;
using H.Controls.ShapeBox.Shapes;
using H.Controls.ShapeBox.State.Base;
using H.Extensions.FontIcon;
using System.ComponentModel.DataAnnotations;

namespace H.Controls.ShapeBox.State.Adds
{
    [Icon(FontIcons.CalculatorSubtract)]
    [Display(Name = "绘制直线")]
    public class AddLineShapeState : AddFromToShapeState<LineShape>
    {
        public AddLineShapeState()
        {
            this.Shape.UseText = true;
        }

        public override IShapeStyleSetting GetShapeStyleSetting()
        {
            return LineShapeStyleSetting.Instance;
        }
    }
}
