// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")
using H.Controls.ShapeBox.Shapes.Base;
using H.Controls.ShapeBox.State.Base;
using H.Extensions.Common;
using System.Windows.Media;

namespace H.Controls.ShapeBox
{
    public interface IStateShapeView : IView
    {
        void DrawStateShape(params IShape[] shapes);
    }

    public class StateShapeBox : SelectShapeBox, IStateShapeView
    {
        private DrawingVisual _StateShapeDrawingVisual = new DrawingVisual();
        public StateShapeBox()
        {
            this.MouseDown += this.StateShapeBox_MouseDown;
            this.MouseMove += this.StateShapeBox_MouseMove;
            this.MouseUp += this.StateShapeBox_MouseUp;
            this.MouseLeave += this.StateShapeBox_MouseLeave;
        }


        private void StateShapeBox_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.GetState()?.MouseLeave(sender, e);
        }

        private void StateShapeBox_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.GetState()?.MouseUp(sender, e);
        }

        private void StateShapeBox_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {

            this.GetState()?.MouseMove(sender, e);
        }

        private void StateShapeBox_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.GetState()?.MouseDown(sender, e);
        }

        protected override IEnumerable<Visual> CreateVisuals()
        {
            return base.CreateVisuals().Concat(this._StateShapeDrawingVisual.ToEnumerable());
        }

        protected override void OnScaleChanged()
        {
            base.OnScaleChanged();
            this.ViewState?.ScaleChanged();
        }

        protected virtual IState GetState()
        {
            return this.ViewState;
        }

        public IViewState ViewState
        {
            get { return (IViewState)GetValue(ViewStateProperty); }
            set { SetValue(ViewStateProperty, value); }
        }

        public static readonly DependencyProperty ViewStateProperty =
            DependencyProperty.Register("ViewState", typeof(IViewState), typeof(StateShapeBox), new FrameworkPropertyMetadata(default(IViewState), (d, e) =>
            {
                StateShapeBox control = d as StateShapeBox;

                if (control == null) return;

                if (e.OldValue is IViewState o)
                {
                    o.Exit();
                }

                if (e.NewValue is IViewState n)
                {
                    n.View = control;
                    n.Enter();
                }
            }));


        public Brush StateStroke
        {
            get { return (Brush)GetValue(StateStrokeProperty); }
            set { SetValue(StateStrokeProperty, value); }
        }

        public static readonly DependencyProperty StateStrokeProperty =
            DependencyProperty.Register("StateStroke", typeof(Brush), typeof(StateShapeBox), new FrameworkPropertyMetadata(Brushes.Chartreuse, (d, e) =>
            {
                StateShapeBox control = d as StateShapeBox;

                if (control == null) return;

                if (e.OldValue is Brush o)
                {

                }

                if (e.NewValue is Brush n)
                {

                }

            }));


        public double StateStrokeThickness
        {
            get { return (double)GetValue(StateStrokeThicknessProperty); }
            set { SetValue(StateStrokeThicknessProperty, value); }
        }

        public static readonly DependencyProperty StateStrokeThicknessProperty =
            DependencyProperty.Register("StateStrokeThickness", typeof(double), typeof(StateShapeBox), new FrameworkPropertyMetadata(1.0, (d, e) =>
            {
                StateShapeBox control = d as StateShapeBox;

                if (control == null) return;

                if (e.OldValue is double o)
                {

                }

                if (e.NewValue is double n)
                {

                }

            }));



        public Brush StateFill
        {
            get { return (Brush)GetValue(StateFillProperty); }
            set { SetValue(StateFillProperty, value); }
        }

        public static readonly DependencyProperty StateFillProperty =
            DependencyProperty.Register("StateFill", typeof(Brush), typeof(StateShapeBox), new FrameworkPropertyMetadata(default(Brush), (d, e) =>
            {
                StateShapeBox control = d as StateShapeBox;

                if (control == null) return;

                if (e.OldValue is Brush o)
                {

                }

                if (e.NewValue is Brush n)
                {

                }

            }));


        public void DrawStateShape(params IShape[] shapes)
        {
            using var drawingContext = this._StateShapeDrawingVisual.RenderOpen();
            if (shapes == null|| shapes.Length==0)
                return;
            double strokeThickness = this.ToViewThickness(this.StateStrokeThickness);
            foreach (var shape in shapes)
            {
                if (shape == null)
                    continue;
                shape.Draw(this, drawingContext, this.StateStroke, strokeThickness, this.StateFill);
            }
        }
    }
}
