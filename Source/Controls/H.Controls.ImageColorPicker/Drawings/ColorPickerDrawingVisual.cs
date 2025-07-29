// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")
using System.Diagnostics;
using System.Windows.Media;

namespace H.Controls.ImageColorPicker.Drawings
{
    public class ColorPickerDrawingVisual : DrawingVisual
    {
        public Point? Position
        {
            get { return (Point?)GetValue(PositionProperty); }
            set { SetValue(PositionProperty, value); }
        }

        public static readonly DependencyProperty PositionProperty =
            DependencyProperty.Register("Position", typeof(Point?), typeof(ColorPickerDrawingVisual), new FrameworkPropertyMetadata(null, (d, e) =>
            {
                ColorPickerDrawingVisual control = d as ColorPickerDrawingVisual;
                if (control == null)
                    return;

            }));


        public Point? PreviewPosition
        {
            get { return (Point?)GetValue(PreviewPositionProperty); }
            set { SetValue(PreviewPositionProperty, value); }
        }

        public static readonly DependencyProperty PreviewPositionProperty =
            DependencyProperty.Register("PreviewPosition", typeof(Point?), typeof(ColorPickerDrawingVisual), new FrameworkPropertyMetadata(default(Point?), (d, e) =>
            {
                ColorPickerDrawingVisual control = d as ColorPickerDrawingVisual;

                if (control == null)
                    return;
            }));


        public Color? PreviewColor
        {
            get { return (Color?)GetValue(PreviewColorProperty); }
            set { SetValue(PreviewColorProperty, value); }
        }

        public static readonly DependencyProperty PreviewColorProperty =
            DependencyProperty.Register("PreviewColor", typeof(Color?), typeof(ColorPickerDrawingVisual), new FrameworkPropertyMetadata(default(Color?), (d, e) =>
            {
                ColorPickerDrawingVisual control = d as ColorPickerDrawingVisual;

                if (control == null) return;

            }));

        public double Length { get; set; } = 6;

        public void Draw()
        {
            using var dc = this.RenderOpen();

            if (this.Position.HasValue)
                dc.DrawHandle(this.Position.Value, Brushes.Chartreuse, this.Length / 5, this.Length);
            if (this.PreviewPosition.HasValue)
            {
                dc.DrawHandle(this.PreviewPosition.Value, Brushes.Chartreuse, this.Length / 5, this.Length);
                if (this.PreviewColor != null)
                    dc.DrawColorText(this.PreviewPosition.Value, this.PreviewColor.Value, this, this.Length * 2);
            }
        }
    }
}
