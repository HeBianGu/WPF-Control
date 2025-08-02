// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")
using H.Controls.ShapeBox.Shapes.Base;
using H.Extensions.Common;
using System.Windows.Media;

namespace H.Controls.ShapeBox
{
    public class SelectShapeBox : ShapeBox
    {
        private DrawingVisual _SelectableShapeDrawingVisual = new DrawingVisual();

        protected override IEnumerable<Visual> CreateVisuals()
        {
            return base.CreateVisuals().Concat(this._SelectableShapeDrawingVisual.ToEnumerable());
        }

        public Brush SelectStroke
        {
            get { return (Brush)GetValue(SelectStrokeProperty); }
            set { SetValue(SelectStrokeProperty, value); }
        }

        public static readonly DependencyProperty SelectStrokeProperty =
            DependencyProperty.Register("SelectStroke", typeof(Brush), typeof(SelectShapeBox), new FrameworkPropertyMetadata(Brushes.Red, (d, e) =>
            {
                SelectShapeBox control = d as SelectShapeBox;

                if (control == null) return;

                if (e.OldValue is Brush o)
                {

                }

                if (e.NewValue is Brush n)
                {

                }

            }));


        public double SelectStrokeThickness
        {
            get { return (double)GetValue(SelectStrokeThicknessProperty); }
            set { SetValue(SelectStrokeThicknessProperty, value); }
        }

        public static readonly DependencyProperty SelectStrokeThicknessProperty =
            DependencyProperty.Register("SelectStrokeThickness", typeof(double), typeof(SelectShapeBox), new FrameworkPropertyMetadata(1.0, (d, e) =>
            {
                SelectShapeBox control = d as SelectShapeBox;

                if (control == null) return;

                if (e.OldValue is double o)
                {

                }

                if (e.NewValue is double n)
                {

                }

            }));



        public Brush SelectFill
        {
            get { return (Brush)GetValue(SelectFillProperty); }
            set { SetValue(SelectFillProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectFillProperty =
            DependencyProperty.Register("SelectFill", typeof(Brush), typeof(SelectShapeBox), new FrameworkPropertyMetadata(default(Brush), (d, e) =>
            {
                SelectShapeBox control = d as SelectShapeBox;

                if (control == null) return;

                if (e.OldValue is Brush o)
                {

                }

                if (e.NewValue is Brush n)
                {

                }

            }));


        protected override void OnScaleChanged()
        {
            base.OnScaleChanged();
            if (this.SelectStrokeThickness > 0)
                this.DrawSelectableShapes();
        }

        protected override void OnShapesChanged()
        {
            base.OnShapesChanged();
            this.SelectShapes();
        }

        public override void UpdateAll()
        {
            base.UpdateAll();
            this.DrawSelectableShapes();
        }

        private List<ISelectableShape> _selectShapes = new List<ISelectableShape>();
        public void SelectShapes(params ISelectableShape[] selectableShapes)
        {
            this._selectShapes.Clear();
            if (selectableShapes != null)
                this._selectShapes.AddRange(selectableShapes);
            this.DrawSelectableShapes();
        }

        private void DrawSelectableShapes()
        {
            using var drawingContext = this._SelectableShapeDrawingVisual.RenderOpen();
            if (this._selectShapes == null || this._selectShapes.Count() == 0)
                return;
            var strokeThickness = this.ToViewThickness(this.SelectStrokeThickness);
            foreach (var item in this._selectShapes)
            {
                item.DrawSelect(this, drawingContext, this.SelectStroke, strokeThickness, this.SelectFill);
            }
        }
    }
}
