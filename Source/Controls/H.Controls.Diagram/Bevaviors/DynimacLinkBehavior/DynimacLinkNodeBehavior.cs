// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Diagram.Bevaviors.DynimacLinkBehavior;

public class DynimacLinkNodeBehavior : DynimacLinkBehaviorBase<Node>
{
    private Node _temp;

    protected override HitTestFilterBehavior MouseMoveHitTestFilter(DependencyObject obj)
    {
        if (obj.GetType().Name == this.GetType().Name)
            return HitTestFilterBehavior.ContinueSkipSelf;
        this.SetMessage("拖动中...");
        if (obj is Node item)
        {
            if (item.Content is IDropable drop)
            {
                bool canDrop = drop.CanDrop(item, out string message);
                this.SetMessage(canDrop ? "可放下" : message ?? "验证失败");
                Part.SetIsCanDrop(item, canDrop);
                Part.SetIsCanDrop(this.AssociatedObject._dynamicLink, canDrop);
            }
            else
            {
                //  Do ：自己不可以和自己连线
                bool self = item == this.AssociatedObject._dynamicLink.FromNode;
                this.SetMessage(!self ? "可放下" : "不能连接自己");
                Part.SetIsCanDrop(item, !self);
                Part.SetIsCanDrop(this.AssociatedObject._dynamicLink, !self);
            }
            Node.SetIsDragging(item, true);
            Part.SetIsDragEnter(item, true);
            Part.SetIsDragEnter(this.AssociatedObject._dynamicLink, true);
            _temp = item;

            return HitTestFilterBehavior.Stop;
        }
        else
        {
            if (_temp != null)
            {
                Node.SetIsDragging(_temp, false);
                Part.SetIsCanDrop(_temp, false);
                Part.SetIsDragEnter(_temp, false);
                _temp = null;
            }

            Part.SetIsDragEnter(this.AssociatedObject._dynamicLink, false);
            Part.SetIsCanDrop(this.AssociatedObject._dynamicLink, false);
        }

        return HitTestFilterBehavior.Continue;
    }

    protected override void ClearDynamic()
    {
        this.AssociatedObject._dynamicLink.Visibility = Visibility.Collapsed;
        this.SetMessage(null);
        if (_temp != null)
        {
            Node.SetIsDragging(_temp, false);
            Part.SetIsCanDrop(_temp, false);
            Part.SetIsDragEnter(_temp, false);
            _temp = null;
        }
    }

    protected override void Create(Node node)
    {
        this.AssociatedObject._dynamicLink.ToNode = node;
        if (this.AssociatedObject._dynamicLink.FromNode == this.AssociatedObject._dynamicLink.ToNode)
            return;

        Link link = Link.Create(this.AssociatedObject._dynamicLink.FromNode, node);
        this.AssociatedObject.Layout.DoLayoutLink(link);
        this.AssociatedObject.OnAddLinked(link);
        this.AssociatedObject.OnItemsChanged();
    }

    protected override void InitDynamic(Node node)
    {
        this.AssociatedObject._dynamicLink.Visibility = Visibility.Visible;
        this.AssociatedObject._dynamicLink.IsHitTestVisible = false;
        this.AssociatedObject._dynamicLink.FromNode = node;
        Point position = NodeLayer.GetPosition(node);
        LinkLayer.SetStart(this.AssociatedObject._dynamicLink, position);
        LinkLayer.SetEnd(this.AssociatedObject._dynamicLink, position);
    }
}
