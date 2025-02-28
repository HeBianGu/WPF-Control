// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control
using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace H.Controls.Diagram.Bevaviors.DynimacLinkBehavior;

[Obsolete("节点使用左键连线PortLinkBehavior")]
public class DynimacLinkPortBehavior : DynimacLinkBehaviorBase<Port>
{
    private Port _tempPort;

    protected override HitTestFilterBehavior MouseMoveHitTestFilter(DependencyObject obj)
    {
#if DEBUG
        DateTime dateTime = DateTime.Now;
#endif  
        //System.Diagnostics.Debug.WriteLine("MouseMoveHitTestFilter");
        //return HitTestFilterBehavior.Stop;

        //if (obj.GetType().Name == this.GetType().Name)
        //{
        //    return HitTestFilterBehavior.ContinueSkipSelf;
        //} 

        this.SetMessage("拖动中...");

        if (obj is Port item)
        {
            Port from = this.AssociatedObject._dynamicLink.FromPort;
            _tempPort = item;
            if (from.Content is IDropable drop)
            {
                bool canDrop = drop.CanDrop(item, out string message);
                this.SetMessage(canDrop ? "可放下" : message ?? "验证失败");
                Part.SetIsCanDrop(item, canDrop);
                Part.SetIsCanDrop(this.AssociatedObject._dynamicLink, canDrop);
                this.AssociatedObject._dynamicLink.HasError = canDrop;
            }
            else
            {
                //Port.SetIsCanDrop(item, true);
                //Link.SetIsCanDrop(this.AssociatedObject._dynamicLink, true); 
                //  Do ：自己不可以和自己连线
                bool self = item == this.AssociatedObject._dynamicLink.FromPort;
                this.SetMessage(!self ? "可放下" : "不能连接自己");
                Part.SetIsCanDrop(this.AssociatedObject._dynamicLink, !self);
                this.AssociatedObject._dynamicLink.HasError = !self;
            }
            this.AssociatedObject._dynamicLink.ToPort.Dock = item.Dock;

            Part.SetIsDragEnter(item, true);
            Part.SetIsDragEnter(this.AssociatedObject._dynamicLink, true);
            return HitTestFilterBehavior.Stop;
        }
        else
        {
            if (_tempPort != null)
            {
                Part.SetIsCanDrop(_tempPort, false);
                Part.SetIsDragEnter(_tempPort, false);
                _tempPort = null;
            }
            Part.SetIsCanDrop(this.AssociatedObject._dynamicLink, false);
            Part.SetIsDragEnter(this.AssociatedObject._dynamicLink, false);
        }

#if DEBUG
        TimeSpan span = DateTime.Now - dateTime;
        System.Diagnostics.Debug.WriteLine("DynimacLinkPortBehavior.MouseMoveHitTestFilter：" + span.ToString());
#endif 
        return HitTestFilterBehavior.Continue;
    }

    protected override void ClearDynamic()
    {
#if DEBUG
        DateTime dateTime = DateTime.Now;
#endif            
        this.AssociatedObject._dynamicLink.Visibility = Visibility.Collapsed;
        //this.AssociatedObject.LinkDrawer.IsUseArrow = _isUseArrow;

        if (_tempPort != null)
        {
            Part.SetIsDragEnter(_tempPort, false);
            _tempPort = null;
        }


        if (this.AssociatedObject._dynamicLink.FromPort == null) return;

        //  Do ：设置所有系统点可用
        System.Collections.Generic.List<Node> where = this.AssociatedObject.Nodes.Where(l => l != this.AssociatedObject._dynamicLink.FromPort.ParentNode).OfType<Node>().ToList();

        where.ForEach(l => l.GetPorts().ForEach(k => k.Visibility = Visibility.Hidden));

#if DEBUG
        TimeSpan span = DateTime.Now - dateTime;
        System.Diagnostics.Debug.WriteLine("ClearDynamic：" + span.ToString());
#endif 
    }

    protected override void Create(Port item)
    {
#if DEBUG
        DateTime dateTime = DateTime.Now;
#endif
        this.AssociatedObject._dynamicLink.ToPort = item;

        if (this.AssociatedObject._dynamicLink.FromPort == null) return;
        if (this.AssociatedObject._dynamicLink.FromPort == this.AssociatedObject._dynamicLink.ToPort) return;

        Node fromNode = this.AssociatedObject._dynamicLink.FromPort.ParentNode;
        Port fromPort = this.AssociatedObject._dynamicLink.FromPort;
        Link link = Link.Create(fromNode, item.ParentNode, fromPort, item);
        this.AssociatedObject.LinkLayer.Children.Add(link);

        //  ToDo：整体刷新可能会有性能问题 后续注意
        //this.AssociatedObject.RefreshData();
        this.AssociatedObject.Layout.DoLayoutLink(link);

        //this.AssociatedObject.RefreshLayout();
        this.AssociatedObject.OnAddLinked(link);

#if DEBUG
        TimeSpan span = DateTime.Now - dateTime;
        System.Diagnostics.Debug.WriteLine("CreateDynamicLink：" + span.ToString());
#endif 
    }


    //bool _isUseArrow;
    protected override void InitDynamic(Port port)
    {
#if DEBUG
        DateTime dateTime = DateTime.Now;
#endif
        //_isUseArrow = this.AssociatedObject.LinkDrawer.IsUseArrow;
        //this.AssociatedObject.LinkDrawer.IsUseArrow = false;
        this.AssociatedObject._dynamicLink.Visibility = Visibility.Visible;
        this.AssociatedObject._dynamicLink.IsHitTestVisible = false;
        this.AssociatedObject._dynamicLink.FromPort = port;
        this.AssociatedObject._dynamicLink.SetDefaultMessage("拖拽中...");
        this.AssociatedObject._dynamicLink.ToPort = new Port();
        Point position = NodeLayer.GetPosition(port);
        Thickness thickness = port.Margin;
        position = new Point(position.X + (thickness.Left - thickness.Right) / 2, position.Y + (thickness.Top - thickness.Bottom) / 2);

        LinkLayer.SetStart(this.AssociatedObject._dynamicLink, position);
        LinkLayer.SetEnd(this.AssociatedObject._dynamicLink, position);

        //  Do ：设置所有系统点可用
        this.AssociatedObject.Nodes.Where(l => l != port.ParentNode).ToList().ForEach(l => l.GetPorts(k => k.PortType == PortType.Input || k.PortType == PortType.Both).ForEach(k => k.Visibility = Visibility.Visible));
#if DEBUG
        TimeSpan span = DateTime.Now - dateTime;
        System.Diagnostics.Debug.WriteLine("InitDynamic：" + span.ToString());
#endif 
    }
}
