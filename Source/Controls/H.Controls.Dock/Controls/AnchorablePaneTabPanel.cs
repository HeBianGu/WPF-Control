﻿
using H.Controls.Dock.Layout;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace H.Controls.Dock.Controls
{
    /// <summary>
    /// provides a <see cref="Panel"/> that contains the TabItem Headers of the <see cref="LayoutAnchorablePaneControl"/>.
    /// </summary>
    public class AnchorablePaneTabPanel : Panel
    {
        #region Constructors

        public AnchorablePaneTabPanel()
        {
            this.FlowDirection = System.Windows.FlowDirection.LeftToRight;
        }

        #endregion Constructors

        #region Overrides

        protected override Size MeasureOverride(Size availableSize)
        {
            double totWidth = 0;
            double maxHeight = 0;
            System.Collections.Generic.IEnumerable<UIElement> visibleChildren = this.Children.Cast<UIElement>().Where(ch => ch.Visibility != System.Windows.Visibility.Collapsed);
            foreach (FrameworkElement child in visibleChildren)
            {
                child.Measure(new Size(double.PositiveInfinity, availableSize.Height));
                totWidth += child.DesiredSize.Width;
                maxHeight = Math.Max(maxHeight, child.DesiredSize.Height);
            }

            if (totWidth > availableSize.Width)
            {
                double childFinalDesideredWidth = availableSize.Width / visibleChildren.Count();
                foreach (FrameworkElement child in visibleChildren)
                {
                    child.Measure(new Size(childFinalDesideredWidth, availableSize.Height));
                }
            }

            return new Size(Math.Min(availableSize.Width, totWidth), maxHeight);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            System.Collections.Generic.IEnumerable<UIElement> visibleChildren = this.Children.Cast<UIElement>().Where(ch => ch.Visibility != System.Windows.Visibility.Collapsed);

            double finalWidth = finalSize.Width;
            double desideredWidth = visibleChildren.Sum(ch => ch.DesiredSize.Width);
            double offsetX = 0.0;

            if (finalWidth > desideredWidth)
            {
                foreach (FrameworkElement child in visibleChildren)
                {
                    double childFinalWidth = child.DesiredSize.Width;
                    child.Arrange(new Rect(offsetX, 0, childFinalWidth, finalSize.Height));

                    offsetX += childFinalWidth;
                }
            }
            else
            {
                double childFinalWidth = finalWidth / visibleChildren.Count();
                foreach (FrameworkElement child in visibleChildren)
                {
                    child.Arrange(new Rect(offsetX, 0, childFinalWidth, finalSize.Height));

                    offsetX += childFinalWidth;
                }
            }

            return finalSize;
        }

        protected override void OnMouseLeave(System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed &&
                LayoutAnchorableTabItem.IsDraggingItem())
            {
                LayoutAnchorable contentModel = LayoutAnchorableTabItem.GetDraggingItem().Model as LayoutAnchorable;
                DockingManager manager = contentModel.Root.Manager;
                LayoutAnchorableTabItem.ResetDraggingItem();

                manager.StartDraggingFloatingWindowForContent(contentModel);
            }

            base.OnMouseLeave(e);
        }

        #endregion Overrides
    }
}