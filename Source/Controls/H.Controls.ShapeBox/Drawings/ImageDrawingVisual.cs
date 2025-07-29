// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Windows.Media;

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
            control.Draw();
        }));

    public void Draw()
    {
        if (this.ImageSource == null)
            return;
        using var dc = this.RenderOpen();
        var rect = new Rect(0, 0, this.ImageSource.Width, this.ImageSource.Height);
        dc.DrawImage(this.ImageSource, rect);
    }

}
