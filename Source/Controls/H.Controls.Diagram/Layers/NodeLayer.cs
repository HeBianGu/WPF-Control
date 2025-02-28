// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using System;
using System.Windows;
using System.Windows.Media;

namespace H.Controls.Diagram.Layers;

public class NodeLayer : Layer
{
    /// <summary>
    /// 位置
    /// </summary>
    public static readonly DependencyProperty PositionProperty = DependencyProperty.RegisterAttached(
        "Position", typeof(Point), typeof(NodeLayer), new FrameworkPropertyMetadata(default(Point), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnPositionChanged));

    public static Point GetPosition(DependencyObject d)
    {
        return (Point)d.GetValue(PositionProperty);
    }

    public static void SetPosition(DependencyObject obj, Point value)
    {
        obj.SetValue(PositionProperty, value);
    }

    private static void OnPositionChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
    {
        if (sender is Node node)
        {
            Diagram diagram = node.GetDiagram();
            if (diagram == null) return;
            //  Do ：拖拽时更新Location
            node.Location = (Point)args.NewValue;
            diagram.Layout?.UpdateNode(node);

            NodeLayer p = VisualTreeHelper.GetParent(sender as UIElement) as NodeLayer;
            p.ArrangeNode(node);
        }

        //if (p == null) 
        //    return;
        //p?.InvalidateArrange();


    }

    protected override Size MeasureOverride(Size constraint)
    {
        Size childConstraint = new Size(double.PositiveInfinity, double.PositiveInfinity);

        foreach (Node child in this.Children)
        {
            if (child == null) continue; child.Measure(childConstraint);

            ////  Do ：测量端口
            //foreach (var port in child.Ports)
            //{
            //    port.Measure(childConstraint);
            //}
        }

        return new Size();
    }


    protected void ArrangeNode(Node child)
    {
        Point point = GetPosition(child);

        //  Do ：居中
        point.X = point.X - child.DesiredSize.Width / 2;
        point.Y = point.Y - child.DesiredSize.Height / 2;

        child.Arrange(new Rect(point, child.DesiredSize));

        //if (child.CheckDefaultTransformGroup())
        //{
        //    if (UseAnimation)
        //    {
        //        child.BeginAnimationXY(point.X, point.Y, this.Duration.TotalMilliseconds);
        //    }
        //    else
        //    {
        //        child.BeginAnimationXY(point.X, point.Y, 0);
        //    }
        //}
    }


    protected override Size ArrangeOverride(Size arrangeSize)
    {

#if DEBUG
        DateTime dateTime = DateTime.Now;
#endif  

        foreach (Node child in this.Children)
        {
            if (child == null) continue;
            this.ArrangeNode(child);

            //Point point = NodeLayer.GetPosition(child);

            ////  Do ：居中
            //point.X = point.X - child.DesiredSize.Width / 2;
            //point.Y = point.Y - child.DesiredSize.Height / 2;

            //child.Arrange(new Rect(new Point(0, 0), child.DesiredSize));

            //if (child.CheckDefaultTransformGroup())
            //{
            //    if (UseAnimation)
            //    {
            //        child.BeginAnimationXY(point.X, point.Y, this.Duration.TotalMilliseconds);
            //    }
            //    else
            //    {
            //        child.BeginAnimationXY(point.X, point.Y, 0);
            //    }
            //}
        }

#if DEBUG
        TimeSpan span = DateTime.Now - dateTime;
        System.Diagnostics.Debug.WriteLine("NodeLayer.ArrangeOverride：" + span.ToString());
#endif 
        return base.ArrangeOverride(arrangeSize);

    }

}
