// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace H.Controls.TreeLayoutBox
{

    public partial class TreeLayoutBox : TreeView
    {
        static TreeLayoutBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TreeLayoutBox), new FrameworkPropertyMetadata(typeof(TreeLayoutBox)));
        }

        public Brush LineStroke
        {
            get { return (Brush)GetValue(LineStrokeProperty); }
            set { SetValue(LineStrokeProperty, value); }
        }

        public static readonly DependencyProperty LineStrokeProperty =
            DependencyProperty.Register("LineStroke", typeof(Brush), typeof(TreeLayoutBox), new FrameworkPropertyMetadata(default(Brush), (d, e) =>
            {
                TreeLayoutBox control = d as TreeLayoutBox;
                if (control == null)
                    return;

                control.ScheduleInvalidateAllConnectorVisuals();
            }));

        public double LineStrokeThickness
        {
            get { return (double)GetValue(LineStrokeThicknessProperty); }
            set { SetValue(LineStrokeThicknessProperty, value); }
        }

        public static readonly DependencyProperty LineStrokeThicknessProperty =
            DependencyProperty.Register("LineStrokeThickness", typeof(double), typeof(TreeLayoutBox), new FrameworkPropertyMetadata(default(double), (d, e) =>
            {
                TreeLayoutBox control = d as TreeLayoutBox;
                if (control == null)
                    return;

                control.ScheduleInvalidateAllConnectorVisuals();
            }));

        public double LineWidth
        {
            get { return (double)GetValue(LineWidthProperty); }
            set { SetValue(LineWidthProperty, value); }
        }

        public static readonly DependencyProperty LineWidthProperty =
            DependencyProperty.Register("LineWidth", typeof(double), typeof(TreeLayoutBox), new FrameworkPropertyMetadata(10d, (d, e) =>
            {
                TreeLayoutBox control = d as TreeLayoutBox;
                if (control == null)
                    return;

                control.ScheduleInvalidateAllConnectorVisuals();
            }));

        public double ItemHeight
        {
            get { return (double)GetValue(ItemHeightProperty); }
            set { SetValue(ItemHeightProperty, value); }
        }

        public static readonly DependencyProperty ItemHeightProperty =
            DependencyProperty.Register("ItemHeight", typeof(double), typeof(TreeLayoutBox), new FrameworkPropertyMetadata(default(double), (d, e) =>
            {
                TreeLayoutBox control = d as TreeLayoutBox;
                if (control == null)
                    return;

                control.ScheduleInvalidateAllConnectorVisuals();
            }));

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
            this.ScheduleInvalidateAllConnectorVisuals();
        }

        private void ScheduleInvalidateAllConnectorVisuals()
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new Action(() =>
            {
                for (int i = 0; i < this.Items.Count; i++)
                {
                    if (this.ItemContainerGenerator.ContainerFromIndex(i) is TreeLayoutItem item)
                    {
                        item.InvalidateConnectorVisualsRecursive();
                    }
                }
            }));
        }
    }
}
