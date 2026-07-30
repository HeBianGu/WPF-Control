// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.ShapeBox.Base;
using H.Extensions.Common;

namespace H.Controls.ShapeBox.Shapes;

public interface ITextShape : IShape
{
    Rect BoundingBox { get; }
    double FontSize { get; set; }
    Point Position { get; set; }
    string Text { get; set; }
    Brush TextBackground { get; set; }
    Brush TextForeground { get; set; }
    double TextOpacity { get; set; }
    bool UseScale { get; set; }
}

public class TextShape : PreviewShapeBase, IBoundingBoxShape, ITextShape
{
    public TextShape()
    {

    }

    public TextShape(Point position)
    {
        this.Position = position;
    }
    private string _Text;
    [Display(Name = "文本内容", GroupName = ShapePropertyGroupNames.DataGroup)]
    public string Text
    {
        get { return _Text; }
        set
        {
            _Text = value;
            RaisePropertyChanged();
        }
    }

    private Brush _TextForeground;
    [Display(Name = "文本颜色", GroupName = ShapePropertyGroupNames.StyleGroup)]
    public Brush TextForeground
    {
        get { return _TextForeground; }
        set
        {
            _TextForeground = value;
            RaisePropertyChanged();
        }
    }

    private Brush _TextBackground;
    [Display(Name = "文本背景", GroupName = ShapePropertyGroupNames.StyleGroup)]
    public Brush TextBackground
    {
        get { return _TextBackground; }
        set
        {
            _TextBackground = value;
            RaisePropertyChanged();
        }
    }


    private Point _Position;
    [Display(Name = "位置", GroupName = ShapePropertyGroupNames.StyleGroup)]
    public Point Position
    {
        get { return _Position; }
        set
        {
            _Position = value;
            RaisePropertyChanged();
        }
    }

    private double _FontSize = 10;
    [Display(Name = "字号", GroupName = ShapePropertyGroupNames.StyleGroup)]
    public double FontSize
    {
        get { return _FontSize; }
        set
        {
            _FontSize = value;
            RaisePropertyChanged();
        }
    }

    private double _TextOpacity = 1.0;
    [Display(Name = "透明度", GroupName = ShapePropertyGroupNames.StyleGroup)]
    public double TextOpacity
    {
        get { return _TextOpacity; }
        set
        {
            _TextOpacity = value;
            RaisePropertyChanged();
        }
    }

    private bool _UseScale;
    [Display(Name = "跟随缩放", GroupName = ShapePropertyGroupNames.StyleGroup)]
    public bool UseScale
    {
        get { return _UseScale; }
        set
        {
            _UseScale = value;
            RaisePropertyChanged();
        }
    }

    public Rect BoundingBox
    {
        get
        {
            if (string.IsNullOrEmpty(this.Text))
                return new Rect(this.Position, new Size(0, 0));
            FormattedText formattedText = this.Text.ToForematedText(this.TextForeground, this.FontSize);
            return new Rect(this.Position, new Size(formattedText.Width, formattedText.Height));
        }
    }

    public override void MatrixDrawing(IView view, DrawingContext drawingContext, Pen pen, Brush fill = null)
    {
        double fontsize = this.UseScale ? this.FontSize / view.Scale : this.FontSize;
        this.DrawBackgroundText(view, drawingContext, this.Position, this.TextForeground ?? pen.Brush, this.GetTextBackground(fill), fontsize, 0);
        base.MatrixDrawing(view, drawingContext, pen, fill);
    }

    public void DrawBackgroundText(IView view, DrawingContext drawingContext, Point point, Brush foregound, Brush brush, double fontsize = 10, double offset = 5)
    {
        if (string.IsNullOrEmpty(this.Text))
            return;

        var l = this.UseScale ? offset / view.Scale : offset;
        point.Offset(l / 2, -l);
        drawingContext.DrawTextAtBottomRight(this.Text, point, foregound, fontsize, null, (f, r) =>
        {
            var fill = this.GetTextBackground(brush);
            var padding = 2 / view.Scale;
            var rect = r.GetPadding(l / 2);
            drawingContext.DrawRoundedRectangle(fill, null, rect, l / 2, l / 2);
        });
    }

    protected virtual Brush GetTextBackground(Brush brush)
    {
        return this.TextBackground ?? brush;
    }
}
