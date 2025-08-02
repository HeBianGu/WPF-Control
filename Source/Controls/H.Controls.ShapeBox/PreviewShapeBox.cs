// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")
using H.Controls.ShapeBox.Shapes.Base;
using H.Extensions.Common;
using System.Windows.Input;
using System.Windows.Media;

namespace H.Controls.ShapeBox
{
    public interface IPreviewShapeView
    {
        void DrawPreviewShape(params IPreviewShape[] previewShapes);
    }

    public class PreviewShapeBox : StateShapeBox, IPreviewShapeView
    {
        private DrawingVisual _PreviewShapeDrawingVisual = new DrawingVisual();
        protected override IEnumerable<Visual> CreateVisuals()
        {
            return base.CreateVisuals().Concat(this._PreviewShapeDrawingVisual.ToEnumerable());
        }

        public Brush PreviewStroke
        {
            get { return (Brush)GetValue(PreviewStrokeProperty); }
            set { SetValue(PreviewStrokeProperty, value); }
        }

        public static readonly DependencyProperty PreviewStrokeProperty =
            DependencyProperty.Register("PreviewStroke", typeof(Brush), typeof(PreviewShapeBox), new FrameworkPropertyMetadata(SystemColors.HighlightBrush, (d, e) =>
            {
                PreviewShapeBox control = d as PreviewShapeBox;

                if (control == null) return;

                if (e.OldValue is Brush o)
                {

                }

                if (e.NewValue is Brush n)
                {

                }

            }));


        public Brush PreviewFill
        {
            get { return (Brush)GetValue(PreviewFillProperty); }
            set { SetValue(PreviewFillProperty, value); }
        }

        public static readonly DependencyProperty PreviewFillProperty =
            DependencyProperty.Register("PreviewFill", typeof(Brush), typeof(PreviewShapeBox), new FrameworkPropertyMetadata(Brushes.Chartreuse, (d, e) =>
            {
                PreviewShapeBox control = d as PreviewShapeBox;

                if (control == null) return;

                if (e.OldValue is Brush o)
                {

                }

                if (e.NewValue is Brush n)
                {

                }

            }));



        public double PreviewStrokeThickness
        {
            get { return (double)GetValue(PreviewStrokeThicknessProperty); }
            set { SetValue(PreviewStrokeThicknessProperty, value); }
        }

        public static readonly DependencyProperty PreviewStrokeThicknessProperty =
            DependencyProperty.Register("PreviewStrokeThickness", typeof(double), typeof(PreviewShapeBox), new FrameworkPropertyMetadata(1.0, (d, e) =>
            {
                PreviewShapeBox control = d as PreviewShapeBox;

                if (control == null) return;

                if (e.OldValue is double o)
                {

                }

                if (e.NewValue is double n)
                {

                }

            }));


        public double Offset
        {
            get { return (double)GetValue(OffsetProperty); }
            set { SetValue(OffsetProperty, value); }
        }

        public static readonly DependencyProperty OffsetProperty =
            DependencyProperty.Register("Offset", typeof(double), typeof(PreviewShapeBox), new FrameworkPropertyMetadata(10.0, (d, e) =>
            {
                PreviewShapeBox control = d as PreviewShapeBox;

                if (control == null) return;

                if (e.OldValue is double o)
                {

                }

                if (e.NewValue is double n)
                {

                }

            }));


        public void DrawPreviewShape(params IPreviewShape[] previewShapes)
        {
            using var drawingContext = this._PreviewShapeDrawingVisual.RenderOpen();
            if (previewShapes == null || previewShapes.Length == 0)
                return;
            Point point = Mouse.GetPosition(this);
            double offset = this.Offset / this.Scale;
            drawingContext.PushTransform(new TranslateTransform(point.X + offset, point.Y + offset));
            double strokeThickness = this.ToViewThickness(this.PreviewStrokeThickness);
            foreach (var previewShape in previewShapes)
            {
                previewShape.DrawPreview(this, drawingContext, this.PreviewStroke, this.PreviewStrokeThickness / this.Scale, this.PreviewFill);
            }
            drawingContext.Pop();
        }
    }
}
