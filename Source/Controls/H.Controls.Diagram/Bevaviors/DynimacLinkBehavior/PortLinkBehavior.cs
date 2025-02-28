// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using Microsoft.Xaml.Behaviors;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace H.Controls.Diagram.Bevaviors.DynimacLinkBehavior;

public class PortLinkBehavior : Behavior<Port>
{
    private Diagram diagram;

    protected override void OnAttached()
    {
        if (this.AssociatedObject.Content is IPortData port)
        {
            if (port.PortType != PortType.Input)
                this.AssociatedObject.MouseLeftButtonDown += AssociatedObject_MouseLeftButtonDown;
        }
        this.AssociatedObject.MouseLeftButtonUp += AssociatedObject_MouseLeftButtonUp;
        this.AssociatedObject.Loaded += AssociatedObject_Loaded;
        this.AssociatedObject.MouseEnter += AssociatedObject_MouseEnter;
        this.AssociatedObject.MouseLeave += AssociatedObject_MouseLeave;
    }

    protected override void OnDetaching()
    {
        this.AssociatedObject.MouseLeftButtonDown -= AssociatedObject_MouseLeftButtonDown;
        this.AssociatedObject.MouseLeftButtonUp -= AssociatedObject_MouseLeftButtonUp;
        this.AssociatedObject.Loaded -= AssociatedObject_Loaded;
        this.AssociatedObject.MouseEnter -= AssociatedObject_MouseEnter;
        this.AssociatedObject.MouseLeave -= AssociatedObject_MouseLeave;
        this.Clear();
        this.diagram = null;
    }

    private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
    {
        diagram = this.AssociatedObject.GetParent<Diagram>();
    }

    private void AssociatedObject_MouseLeave(object sender, MouseEventArgs e)
    {
        if (diagram._dynamicLink.Visibility != Visibility.Visible)
            return;
        if (e.MouseDevice.LeftButton == MouseButtonState.Released)
            return;

        Part.SetIsCanDrop(this.AssociatedObject, false);
        Part.SetIsDragEnter(this.AssociatedObject, false);
        Part.SetIsCanDrop(this.diagram._dynamicLink, false);
        Part.SetIsDragEnter(this.diagram._dynamicLink, false);
        this.diagram._dynamicLink.HasError = false;
        this.AssociatedObject.HasError = false;
        this.SetMessage("拖拽中...");
    }

    private void AssociatedObject_MouseEnter(object sender, MouseEventArgs e)
    {
        if (diagram._dynamicLink.Visibility != Visibility.Visible) return;
        if (e.MouseDevice.LeftButton == MouseButtonState.Released) return;
        Port from = diagram._dynamicLink.FromPort;
        bool self = this.AssociatedObject == from;
        if (from.Content is IDropable drop)
        {
            bool canDrop = drop.CanDrop(this.AssociatedObject, out string message);
            this.SetMessage(canDrop ? "可放下" : message ?? "验证失败");
            Part.SetIsCanDrop(this.AssociatedObject, canDrop);
            Part.SetIsCanDrop(this.diagram._dynamicLink, canDrop);
            if (canDrop)
            {
                this.diagram._dynamicLink.ToPort.Dock = this.AssociatedObject.Dock;
                this.diagram._dynamicLink.Update();
            }
            else
            {
                this.diagram._dynamicLink.HasError = true;
                this.AssociatedObject.HasError = true;
            }
        }
        else
        {
            this.SetMessage(!self ? "可放下" : "不能连接自己");
            Part.SetIsCanDrop(this.diagram._dynamicLink, !self);
            Part.SetIsCanDrop(this.AssociatedObject, !self);
            if (!self)
            {
                this.diagram._dynamicLink.ToPort.Dock = this.AssociatedObject.Dock;
                this.diagram._dynamicLink.Update();
            }
            else
            {
                this.diagram._dynamicLink.HasError = true;
                this.AssociatedObject.HasError = true;
            }
        }

        //System.Diagnostics.Debug.WriteLine("AssociatedObject_MouseEnter");
    }

    private void AssociatedObject_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        if (diagram._dynamicLink.Visibility != Visibility.Visible) return;
        if (diagram._dynamicLink.FromPort == this.AssociatedObject) return;
        e.Handled = true;
        Port port = sender as Port;
        this.Clear();
        this.Dispatcher.BeginInvoke(DispatcherPriority.Input, new Action(() =>
        {
            this.Create(port);
        }));
    }

    private Point point;
    private Point previous;
    private void Diagram_MouseMove(object sender, MouseEventArgs e)
    {
        if (diagram._dynamicLink.Visibility != Visibility.Visible)
            return;
        if (e.MouseDevice.LeftButton == MouseButtonState.Released)
            return;

#if DEBUG
        DateTime dateTime = DateTime.Now;
#endif
        point = Mouse.GetPosition(this.diagram);
        previous = LinkLayer.GetEnd(diagram._dynamicLink);
        LinkLayer.SetEnd(diagram._dynamicLink, point);

        //this.Dispatcher.DelayInvoke(DispatcherPriority.SystemIdle, new Action(() =>
        //{
        if (Part.GetIsCanDrop(this.diagram._dynamicLink))
        {
            diagram._dynamicLink.Update();
            return;
        }
        Vector v = point - previous;
        if (v.Length > SystemParameters.MinimumHorizontalDragDistance)
        {
            this.DelayInvoke(() =>
            {
                diagram._dynamicLink.ToPort.Dock = Math.Abs(v.X) > Math.Abs(v.Y) ? v.X > 0 ? Dock.Left : Dock.Right : v.Y > 0 ? Dock.Top : Dock.Bottom;
            });
        }


        diagram._dynamicLink.Update();
#if DEBUG
        TimeSpan span = DateTime.Now - dateTime;
        System.Diagnostics.Debug.WriteLine("Diagram_PreviewMouseMove：" + span.ToString());
#endif
        //}));

    }

    private void AssociatedObject_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        e.Handled = true;
        this.Init();
    }

    private void Diagram_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        this.Clear();
    }

    private void Clear()
    {
        Mouse.OverrideCursor = null;
        this.ClearDynamic();
        diagram.MouseMove -= Diagram_MouseMove;
        diagram.MouseLeftButtonUp -= Diagram_MouseLeftButtonUp;

        Part.SetIsCanDrop(this.AssociatedObject, false);
        Part.SetIsDragEnter(this.AssociatedObject, false);
        this.AssociatedObject.HasError = false;
        this.SetMessage(null);
    }

    private void Init()
    {
        Mouse.OverrideCursor = Cursors.Pen;
        diagram.MouseMove += Diagram_MouseMove;
        diagram.MouseLeftButtonUp += Diagram_MouseLeftButtonUp;
        this.InitDynamic(this.AssociatedObject);
    }

    protected void InitDynamic(Port port)
    {
#if DEBUG
        DateTime dateTime = DateTime.Now;
#endif
        diagram._dynamicLink.Visibility = Visibility.Visible;
        diagram._dynamicLink.IsHitTestVisible = false;
        diagram._dynamicLink.FromPort = port;
        if (port.Content is ILinkInitializer initializer)
            initializer.InitLink(diagram._dynamicLink);
        this.SetMessage("拖拽中...");
        diagram._dynamicLink.ToPort = new Port();
        Point position = NodeLayer.GetPosition(port);
        Thickness thickness = port.Margin;
        position = new Point(position.X + (thickness.Left - thickness.Right) / 2, position.Y + (thickness.Top - thickness.Bottom) / 2);

        LinkLayer.SetStart(diagram._dynamicLink, position);
        LinkLayer.SetEnd(diagram._dynamicLink, position);
        diagram._dynamicLink.Update();

        //diagram._dynamicLink.Update();

        //this.Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, new Action(() =>
        //{
        //    //  Do ：设置所有系统点可用
        //    diagram.Nodes.Where(l => l != port.ParentNode).ToList().ForEach(l => l.GetPorts(k => k.PortType == PortType.Input || k.PortType == PortType.Both).ForEach(k => k.Visibility = Visibility.Visible));
        //})); 

#if DEBUG
        TimeSpan span = DateTime.Now - dateTime;
        System.Diagnostics.Debug.WriteLine("InitDynamic：" + span.ToString());
#endif 
    }

    protected void ClearDynamic()
    {
#if DEBUG
        DateTime dateTime = DateTime.Now;
#endif
        this.diagram._dynamicLink.HasError = false;
        diagram._dynamicLink.Visibility = Visibility.Collapsed;
        if (diagram._dynamicLink.FromPort == null)
            return;

        ////  Do ：设置所有系统点可用
        //System.Collections.Generic.List<Node> where = diagram.Nodes.Where(l => l != diagram._dynamicLink.FromPort.ParentNode).OfType<Node>().ToList();
        //where.ForEach(l => l.GetPorts().ForEach(k => k.Visibility = Visibility.Collapsed));
#if DEBUG
        TimeSpan span = DateTime.Now - dateTime;
        System.Diagnostics.Debug.WriteLine("ClearDynamic：" + span.ToString());
#endif 
    }

    [Obsolete]
    protected void Create(Port item)
    {
#if DEBUG
        DateTime dateTime = DateTime.Now;
#endif
        diagram._dynamicLink.ToPort = item;
        if (diagram._dynamicLink.FromPort == null) return;
        if (diagram._dynamicLink.FromPort == diagram._dynamicLink.ToPort) return;
        Node fromNode = diagram._dynamicLink.FromPort.ParentNode;
        Port fromPort = diagram._dynamicLink.FromPort;
        Link link = Link.Create(fromNode, item.ParentNode, fromPort, item);
        //diagram.LinkLayer.Children.Add(link);
        //diagram.Layout.DoLayoutLink(link);
        //diagram.OnAddLinked(link);
        //diagram.OnItemsChanged();
        diagram.AddLink(link);

        this.Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, new Action(() =>
                   {
                       Part.SetIsCanDrop(this.diagram._dynamicLink, false);
                       Part.SetIsDragEnter(this.diagram._dynamicLink, false); ;
                   }));

#if DEBUG
        TimeSpan span = DateTime.Now - dateTime;
        System.Diagnostics.Debug.WriteLine("CreateDynamicLink：" + span.ToString());
#endif 
    }

    protected void SetMessage(string message)
    {
        this.diagram.InvokeMessage(message);
        this.diagram._dynamicLink.SetDefaultMessage(message);
    }
}
