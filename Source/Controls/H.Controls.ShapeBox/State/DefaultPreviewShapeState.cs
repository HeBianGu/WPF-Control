// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Windows.Input;

namespace H.Controls.ShapeBox.State;

public class SelectabPrviewShapeState : PreviewShapeStateBase
{
    public bool UseSelect { get; set; } = true;
    public override void MouseDown(object sender, MouseButtonEventArgs e)
    {
        base.MouseDown(sender, e);

        if (this.View is ISelectShapeBox shapeBox && this.UseSelect)
        {
            Point point = e.GetPosition(this.GetElementView());
            var mouses = this.GetShapes().OfType<ISelectableShape>();
            var finds = mouses.Where(x => x.Hit(this.View, point));
            if (finds.Any())
                shapeBox.SelectShapes(finds.ToArray());
            else
                shapeBox.SelectShapes();
        }
    }
}

public class DefaultPreviewShapeState : SelectabPrviewShapeState
{

}
