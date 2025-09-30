// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Adorner.Draggable.Bevhavior;

public class DraggableGridAdorner : System.Windows.Documents.Adorner
{
    public DraggableGridAdorner(UIElement adornedElement) : base(adornedElement)
    {
        vbrush = new VisualBrush(this.AdornedElement);
        vbrush.Opacity = .5;
    }

    protected override void OnRender(DrawingContext dc)
    {

        List<double> x = new List<double>();
        List<double> y = new List<double>();

        for (int i = 0; i < 10; i++)
        {
            x.Add(i * i * i);
            y.Add(i * i * i);
        }

        GuidelineSet guideline = new GuidelineSet(x.ToArray(), y.ToArray());

        //dc.DrawRectangle(vbrush, null, new Rect(p, this.RenderSize));

        //dc.PushGuidelineSet(guideline);
    }

    private Brush vbrush;

    //private Point location;

    /// <summary> 相对于拖动控件的拖动位置 </summary>
    public Point Offset { get; set; }

    public DraggableAdornerMode DropAdornerMode { get; set; }
}

