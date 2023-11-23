// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase


using Microsoft.Xaml.Behaviors;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace H.Controls.Diagram
{
    public class NodeMouseDragBehavior : Behavior<Node>
    {
       private Point? _start;
        private Diagram _layer;
        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.Unloaded += AssociatedObject_Unloaded;
            this.AssociatedObject.Loaded += AssociatedObject_Loaded;
            this.AssociatedObject.MouseDown += AssociatedObject_MouseDown;
        }

        private void AssociatedObject_Unloaded(object sender, RoutedEventArgs e)
        {
            this.AssociatedObject.MouseDown -= AssociatedObject_MouseDown;
        }

        protected override void OnDetaching()
        {
            this.AssociatedObject.Loaded -= AssociatedObject_Loaded;
            this.AssociatedObject.MouseDown -= AssociatedObject_MouseDown;
        }

        private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            _layer = this.AssociatedObject.GetDiagram() as Diagram;
        }

        private void _layer_MouseLeave(object sender, MouseEventArgs e)
        {
            this.EndDragging();
        }

        private void _layer_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.EndDragging();
        }

        private void _layer_MouseMove(object sender, MouseEventArgs e)
        {
            e.Handled=true;
            if (this._start == null)
                return;
            if (e.LeftButton == MouseButtonState.Released)
                return;

            Node.SetIsDragging(this.AssociatedObject, true);

            var p = e.GetPosition(_layer);
            double x = p.X - _start.Value.X;
            double y = p.Y - _start.Value.Y;
            //  Do ：更新Node
            Point old = NodeLayer.GetPosition(this.AssociatedObject);
            Point point = new Point(old.X + x, old.Y + y);
            System.Diagnostics.Debug.WriteLine("Change " + x);
            System.Diagnostics.Debug.WriteLine("Change " + y);
            NodeLayer.SetPosition(this.AssociatedObject, point);
            this._start = p;
        }

        private void AssociatedObject_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this._start = e.GetPosition(_layer);
            e.Handled = true;
            this.StartDragging();
            this.AssociatedObject.GetDiagram().Focus();
        }

        private void StartDragging()
        {
            _layer.MouseMove += _layer_MouseMove;
            _layer.MouseUp += _layer_MouseUp;
            _layer.MouseLeave += _layer_MouseLeave;
        }

        private void EndDragging()
        {
            _layer.MouseMove -= _layer_MouseMove;
            _layer.MouseUp -= _layer_MouseUp;
            _layer.MouseLeave -= _layer_MouseLeave;

            this._start = null;
            Node.SetIsDragging(this.AssociatedObject, false);
        }
    }
}
