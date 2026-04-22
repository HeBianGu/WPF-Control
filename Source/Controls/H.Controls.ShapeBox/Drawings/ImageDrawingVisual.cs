// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

public class ImageDrawingVisual : DrawingVisual
{
    public ImageSource ImageSource
    {
        get { return (ImageSource)GetValue(ImageSourceProperty); }
        set { SetValue(ImageSourceProperty, value); }
    }

    public static readonly DependencyProperty ImageSourceProperty =
        DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(ImageDrawingVisual), new FrameworkPropertyMetadata(default(ImageSource), (d, e) =>
        {
            ImageDrawingVisual control = d as ImageDrawingVisual;

            if (control == null) return;

            if (e.OldValue is ImageSource o)
            {

            }

            if (e.NewValue is ImageSource n)
            {

            }
        }));


    public double Width
    {
        get { return (double)GetValue(WidthProperty); }
        set { SetValue(WidthProperty, value); }
    }

    public static readonly DependencyProperty WidthProperty =
        DependencyProperty.Register("Width", typeof(double), typeof(ImageDrawingVisual), new FrameworkPropertyMetadata(default(double), (d, e) =>
        {
            ImageDrawingVisual control = d as ImageDrawingVisual;

            if (control == null) return;

            if (e.OldValue is double o)
            {

            }

            if (e.NewValue is double n)
            {

            }
        }));


    public double Height
    {
        get { return (double)GetValue(HeightProperty); }
        set { SetValue(HeightProperty, value); }
    }

    public static readonly DependencyProperty HeightProperty =
        DependencyProperty.Register("Height", typeof(double), typeof(ImageDrawingVisual), new FrameworkPropertyMetadata(default(double), (d, e) =>
        {
            ImageDrawingVisual control = d as ImageDrawingVisual;

            if (control == null) return;

            if (e.OldValue is double o)
            {

            }

            if (e.NewValue is double n)
            {

            }
        }));


    public void Draw()
    {
        using var dc = this.RenderOpen();
        if (this.ImageSource == null)
            return;
        Rect rect = new Rect(0, 0, this.Width, this.Height);
        if (this.Width == 0 || this.Height == 0)
            rect = new Rect(0, 0, this.ImageSource.Width, this.ImageSource.Height);
        dc.DrawImage(this.ImageSource, rect);
    }

}
