// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.ShapeBox.Drawings;

namespace H.Controls.ShapeBox.Shapes.Base;

public interface ITitleShape : IStyleShape
{
    string Title { get; set; }
}

public abstract class TitleShapeBase : PreviewShapeBase, ITitleShape
{
    public string Title { get; set; }

    [Display(Name = "启用标题", GroupName = "样式")]
    public bool UseTitle { get; set; } = true;
    public Brush TitleForeground { get; set; } = Brushes.Black;
    public Brush TitleBackground { get; set; }

    public virtual void DrawTitle(IView view, DrawingContext drawingContext, Point point, Brush brush, double fontsize = 10.0, double offset = 5)
    {
        if (!this.UseTitle)
            return;
        if (string.IsNullOrEmpty(this.Title))
            return;
        drawingContext.DrawTextAt(this.Title, point, this.TitleForeground, fontsize, this.GetTitleBackground(brush), offset);
    }

    public virtual void DrawTitle(IView view, DrawingContext drawingContext, Pen pen, Brush fill = null)
    {

    }

    protected virtual Brush GetTitleBackground(Brush brush)
    {
        return this.TitleBackground ?? brush;
    }
}
