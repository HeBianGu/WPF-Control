// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Collections.Specialized;
using System.Windows.Threading;

namespace H.Controls.TreeLayoutBox
{
    public class TreeLayoutItem : TreeViewItem
    {
        static TreeLayoutItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TreeLayoutItem), new FrameworkPropertyMetadata(typeof(TreeLayoutItem)));
        }

        public TreeLayoutItem()
        {
            this.Loaded += (l, k) => this.InvalidateVisual();
            this.SizeChanged += (l, k) => this.InvalidateVisual();
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new TreeLayoutItem();
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is TreeLayoutItem;
        }

        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsChanged(e);
            this.ScheduleInvalidateConnectorVisualsAroundSelf();
        }

        protected override void OnExpanded(RoutedEventArgs e)
        {
            base.OnExpanded(e);
            this.ScheduleInvalidateConnectorVisualsAroundSelf();
        }

        protected override void OnCollapsed(RoutedEventArgs e)
        {
            base.OnCollapsed(e);
            this.ScheduleInvalidateConnectorVisualsAroundSelf();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            TreeLayoutBox treeLayoutBox = this.GetTreeLayoutBox();
            if (treeLayoutBox == null)
                return;

            Brush brush = treeLayoutBox.LineStroke;
            if (brush == null)
                return;

            double thickness = treeLayoutBox.LineStrokeThickness;
            double lineWidth = treeLayoutBox.LineWidth;

            if (thickness <= 0 || lineWidth <= 0 || this.ActualHeight <= 0)
                return;

            Pen pen = new Pen(brush, thickness);

            double x = lineWidth / 2.0;
            x = 0.0;
            double y = this.GetHeaderCenterY(treeLayoutBox);

            if (!this.IsRootItem())
            {
                if (!this.IsFirstItem())
                {
                    drawingContext.DrawLine(pen, new Point(x, 0), new Point(x, y));
                }

                drawingContext.DrawLine(pen, new Point(x, y), new Point(lineWidth, y));

                if (!this.IsLastItem())
                {
                    drawingContext.DrawLine(pen, new Point(x, y), new Point(x, this.ActualHeight));
                }
            }
        }

        internal void InvalidateConnectorVisualsRecursive()
        {
            this.InvalidateVisual();

            for (int i = 0; i < this.Items.Count; i++)
            {
                if (this.ItemContainerGenerator.ContainerFromIndex(i) is TreeLayoutItem child)
                {
                    child.InvalidateConnectorVisualsRecursive();
                }
            }
        }

        private void ScheduleInvalidateConnectorVisualsAroundSelf()
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new Action(() =>
            {
                this.InvalidateConnectorVisualsRecursive();

                ItemsControl parent = ItemsControl.ItemsControlFromItemContainer(this);
                if (parent == null)
                    return;

                for (int i = 0; i < parent.Items.Count; i++)
                {
                    if (parent.ItemContainerGenerator.ContainerFromIndex(i) is TreeLayoutItem sibling)
                    {
                        sibling.InvalidateVisual();
                    }
                }

                if (parent is TreeLayoutItem parentItem)
                {
                    parentItem.InvalidateVisual();
                }
            }));
        }

        private double GetHeaderCenterY(TreeLayoutBox treeLayoutBox)
        {
            double itemHeight = treeLayoutBox.ItemHeight;
            if (itemHeight <= 0)
                itemHeight = this.ActualHeight;

            return Math.Max(0, Math.Min(this.ActualHeight, itemHeight / 2.0));
        }

        private bool IsRootItem()
        {
            return ItemsControl.ItemsControlFromItemContainer(this) is TreeLayoutBox;
        }

        private bool IsFirstItem()
        {
            ItemsControl parent = ItemsControl.ItemsControlFromItemContainer(this);
            if (parent == null)
                return false;

            int index = parent.ItemContainerGenerator.IndexFromContainer(this);
            if (index < 0)
                return false;

            return index == 0;
        }

        private bool IsLastItem()
        {
            ItemsControl parent = ItemsControl.ItemsControlFromItemContainer(this);
            if (parent == null)
                return true;

            int index = parent.ItemContainerGenerator.IndexFromContainer(this);
            if (index < 0)
                return true;

            return index == parent.Items.Count - 1;
        }

        private TreeLayoutBox GetTreeLayoutBox()
        {
            DependencyObject current = this;

            while (current != null)
            {
                if (current is TreeLayoutBox treeLayoutBox)
                    return treeLayoutBox;

                current = VisualTreeHelper.GetParent(current);
            }

            return null;
        }
    }
}
