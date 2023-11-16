// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Media;

namespace H.Controls.Diagram
{
    public class DynimacLinkNodeBehavior : DynimacLinkBehaviorBase<Node>
    {
        private Node _temp;

        protected override HitTestFilterBehavior MouseMoveHitTestFilter(DependencyObject obj)
        {
            if (obj.GetType().Name == this.GetType().Name)
            {
                return HitTestFilterBehavior.ContinueSkipSelf;
            }
            this.SetMessage("拖动中...");
            if (obj is Node item)
            {
                if (item.Content is IDropable drop)
                {
                    bool canDrop = drop.CanDrop(item, out string message);
                    this.SetMessage(canDrop ? "可放下" : message ?? "验证失败");
                    Port.SetIsCanDrop(item, canDrop);
                    Link.SetIsCanDrop(this.AssociatedObject._dynamicLink, canDrop);
                }
                else
                {
                    //  Do ：自己不可以和自己连线
                    bool self = item == this.AssociatedObject._dynamicLink.FromNode;
                    this.SetMessage(!self ? "可放下" : "不能连接自己");
                    Node.SetIsCanDrop(item, !self);
                    Link.SetIsCanDrop(this.AssociatedObject._dynamicLink, !self);
                }
                Node.SetIsDragging(item, true);
                Node.SetIsDragEnter(item, true);
                Link.SetIsDragEnter(this.AssociatedObject._dynamicLink, true);
                _temp = item;

                return HitTestFilterBehavior.Stop;
            }
            else
            {
                if (_temp != null)
                {
                    Node.SetIsDragging(_temp, false);
                    Node.SetIsCanDrop(_temp, false);
                    Node.SetIsDragEnter(_temp, false);
                    _temp = null;
                }

                Link.SetIsDragEnter(this.AssociatedObject._dynamicLink, false);
                Link.SetIsCanDrop(this.AssociatedObject._dynamicLink, false);
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
                Node.SetIsCanDrop(_temp, false);
                Node.SetIsDragEnter(_temp, false);
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
}
