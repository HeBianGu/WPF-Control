// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Windows;
using System.Windows.Input;

namespace H.Controls.Panel.Panel;

/// <summary> 停靠控件 </summary>
public class SideDockPanel : PanelBase
{
    public SideDockPanel()
    {
        //  Do ：定位到中心位置
        {
            CommandBinding binding = new CommandBinding(Commands.Selected);

            this.CommandBindings.Add(binding);

            binding.Executed += (l, k) =>
            {
                if (k.Parameter is UIElement element)
                {
                    int index = this.Children.IndexOf(element);

                    //if (index >= 0)
                    //{
                    //    if (index > (this.Children.Count - 1) / 2)
                    //    {
                    //        this.StartIndex = (index - (this.Children.Count - 1) / 2);
                    //    }
                    //    else
                    //    {
                    //        this.StartIndex = this.Children.Count - ((this.Children.Count - 1) / 2 - index);
                    //    }
                    //}

                    this.StartIndex = index;
                }
            };

            binding.CanExecute += (l, k) => { k.CanExecute = true; };

        }
    }

    public DockType Dock
    {
        get { return (DockType)GetValue(DockProperty); }
        set { SetValue(DockProperty, value); }
    }


    public static readonly DependencyProperty DockProperty =
        DependencyProperty.Register("Dock", typeof(DockType), typeof(SideDockPanel), new PropertyMetadata(default(DockType), (d, e) =>
        {
            SideDockPanel control = d as SideDockPanel;

            if (control == null) return;

            //Dock config = e.NewValue as Dock;
            control.InvalidateArrange();
        }));

    public bool IsFull
    {
        get { return (bool)GetValue(IsFullProperty); }
        set { SetValue(IsFullProperty, value); }
    }


    public static readonly DependencyProperty IsFullProperty =
        DependencyProperty.Register("IsFull", typeof(bool), typeof(SideDockPanel), new PropertyMetadata(true, (d, e) =>
         {
             SideDockPanel control = d as SideDockPanel;

             if (control == null) return;

             //bool config = e.NewValue as bool;

             control.InvalidateArrange();
         }));

    public double SmallSize
    {
        get { return (double)GetValue(SmallSizeProperty); }
        set { SetValue(SmallSizeProperty, value); }
    }


    public static readonly DependencyProperty SmallSizeProperty =
        DependencyProperty.Register("SmallSize", typeof(double), typeof(SideDockPanel), new PropertyMetadata(100.0, (d, e) =>
        {
            SideDockPanel control = d as SideDockPanel;

            if (control == null) return;

            //double config = e.NewValue as double;

            control.InvalidateArrange();

        }));


    private double GetSmallSizeHeight(Size finalSize)
    {
        if (this.Dock == DockType.TopAndBottom)
        {
            return this.SmallSize * 2 > finalSize.Height ? finalSize.Height / 4 : this.SmallSize;
        }
        else
        {
            return this.SmallSize > finalSize.Height ? finalSize.Height / 3 : this.SmallSize;
        }
    }

    private double GetSmallSizeWidth(Size finalSize)
    {
        if (this.Dock == DockType.LeftAndRight)
        {
            return this.SmallSize * 2 > finalSize.Width ? finalSize.Width / 4 : this.SmallSize;
        }
        else
        {
            return this.SmallSize > finalSize.Width ? finalSize.Width / 3 : this.SmallSize;
        }
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
        List<UIElement> children = this.GetChildren();
        if (children.Count == 0)
            return finalSize;
        Action<UIElement, Point> action = (elment, end) =>
        {
            elment.Arrange(new Rect(end, elment.DesiredSize));
        };

        var samllHeight = this.GetSmallSizeHeight(finalSize);
        var smallWidth = this.GetSmallSizeWidth(finalSize);
        if (this.Dock == DockType.Bottom)
        {
            FrameworkElement center = children[0] as FrameworkElement;
            double cx = finalSize.Width / 2;
            double cy = (finalSize.Height - samllHeight) / 2;
            center.Width = finalSize.Width;
            center.Height = finalSize.Height - samllHeight;
            center.Measure(finalSize);
            Point cp = new Point(cx - center.DesiredSize.Width / 2, cy - center.DesiredSize.Height / 2);
            action(center, cp);

            int count = children.Count - 1;
            double len = finalSize.Width / count;
            int index = 0;
            foreach (FrameworkElement item in children.Cast<FrameworkElement>())
            {
                if (item == center) 
                    continue;
                double ix = index * len + len / 2;
                double iy = finalSize.Height - samllHeight / 2;
                item.Width = len;
                item.Height = samllHeight;
                item.Measure(finalSize);
                Point ip = new Point(ix - item.DesiredSize.Width / 2, iy - item.DesiredSize.Height / 2);
                action(item, ip);
                index++;
            }

        }

        else if (this.Dock == DockType.Top)
        {
            FrameworkElement center = children[0] as FrameworkElement;
            double cx = finalSize.Width / 2;
            double cy = (finalSize.Height - samllHeight) / 2 + samllHeight;
            center.Width = finalSize.Width;
            center.Height = finalSize.Height - samllHeight;
            center.Measure(finalSize);
            Point cp = new Point(cx - center.DesiredSize.Width / 2, cy - center.DesiredSize.Height / 2);
            action(center, cp);

            int count = children.Count - 1;
            double len = finalSize.Width / count;
            int index = 0;

            foreach (FrameworkElement item in children.Cast<FrameworkElement>())
            {
                if (item == center) 
                    continue;
                double ix = index * len + len / 2;
                double iy = samllHeight / 2;
                item.Width = len;
                item.Height = samllHeight;
                item.Measure(finalSize);
                Point ip = new Point(ix - item.DesiredSize.Width / 2, iy - item.DesiredSize.Height / 2);
                action(item, ip);
                index++;
            }
        }

        else if (this.Dock == DockType.TopAndBottom)
        {
            FrameworkElement center = children[0] as FrameworkElement;
            double cx = finalSize.Width / 2;
            double cy = finalSize.Height / 2;
            center.Width = finalSize.Width;
            center.Height = finalSize.Height - samllHeight * 2;
            center.Measure(finalSize);
            Point cp = new Point(cx - center.DesiredSize.Width / 2, cy - center.DesiredSize.Height / 2);
            action(center, cp);

            int count = (children.Count - 1) / 2 + (children.Count - 1) % 2;
            Func<int, double> GetLen = index =>
               {
                   return this.IsFull && (children.Count - 1) % 2 == 1 && index >= count ? finalSize.Width / (count - 1) : finalSize.Width / count;
               };

            {
                int index = 0;
                foreach (FrameworkElement item in children.Cast<FrameworkElement>())
                {
                    if (item == center) 
                        continue;
                    double len = GetLen(index);
                    double ix = index < count ? index * len + len / 2 : (index - count) * len + len / 2;
                    double iy = index < count ? finalSize.Height - samllHeight / 2 : samllHeight / 2;
                    item.Width = len;
                    item.Height = samllHeight;
                    item.Measure(finalSize);
                    Point ip = new Point(ix - item.DesiredSize.Width / 2, iy - item.DesiredSize.Height / 2);
                    action(item, ip);
                    index++;
                }
            }
        }

        else if (this.Dock == DockType.Left)
        {
            FrameworkElement center = children[0] as FrameworkElement;
            double cx = (finalSize.Width - smallWidth) / 2 + smallWidth;
            double cy = finalSize.Height / 2;
            center.Width = finalSize.Width - smallWidth;
            center.Height = finalSize.Height;
            center.Measure(finalSize);
            Point cp = new Point(cx - center.DesiredSize.Width / 2, cy - center.DesiredSize.Height / 2);
            action(center, cp);

            int count = children.Count - 1;
            double len = finalSize.Height / count;
            int index = 0;
            foreach (FrameworkElement item in children.Cast<FrameworkElement>())
            {
                if (item == center)
                    continue;
                double ix = smallWidth / 2;
                double iy = index * len + len / 2;
                item.Height = len;
                item.Width = smallWidth;
                item.Measure(finalSize);
                Point ip = new Point(ix - item.DesiredSize.Width / 2, iy - item.DesiredSize.Height / 2);
                action(item, ip);
                index++;
            }
        }

        else if (this.Dock == DockType.Right)
        {
            FrameworkElement center = children[0] as FrameworkElement;
            double cx = (finalSize.Width - smallWidth) / 2;
            double cy = finalSize.Height / 2;
            center.Width = finalSize.Width - smallWidth;
            center.Height = finalSize.Height;
            center.Measure(finalSize);
            Point cp = new Point(cx - center.DesiredSize.Width / 2, cy - center.DesiredSize.Height / 2);
            action(center, cp);

            int count = children.Count - 1;
            double len = finalSize.Height / count;
            int index = 0;
            foreach (FrameworkElement item in children.Cast<FrameworkElement>())
            {
                if (item == center) 
                    continue;
                double ix = finalSize.Width - smallWidth / 2;
                double iy = index * len + len / 2;
                item.Height = len;
                item.Width = smallWidth;
                item.Measure(finalSize);
                Point ip = new Point(ix - item.DesiredSize.Width / 2, iy - item.DesiredSize.Height / 2);
                action(item, ip);
                index++;
            }

        }

        else if (this.Dock == DockType.LeftAndRight)
        {
            FrameworkElement center = children[0] as FrameworkElement;
            double cx = finalSize.Width / 2;
            double cy = finalSize.Height / 2;
            center.Width = finalSize.Width - smallWidth * 2;
            center.Height = finalSize.Height;
            center.Measure(finalSize);
            Point cp = new Point(cx - center.DesiredSize.Width / 2, cy - center.DesiredSize.Height / 2);
            action(center, cp);

            int count = (children.Count - 1) / 2 + (children.Count - 1) % 2;
            Func<int, double> GetLen = index =>
            {
                return this.IsFull && (children.Count - 1) % 2 == 1 && index >= count ? finalSize.Height / (count - 1) : finalSize.Height / count;
            };

            {
                int index = 0;
                foreach (FrameworkElement item in children.Cast<FrameworkElement>())
                {
                    if (item == center) 
                        continue;
                    double len = GetLen(index);
                    double ix = index < count ? finalSize.Width - smallWidth / 2 : smallWidth / 2;
                    double iy = index < count ? index * len + len / 2 : (index - count) * len + len / 2;
                    item.Width = smallWidth;
                    item.Height = len;
                    item.Measure(finalSize);
                    Point ip = new Point(ix - item.DesiredSize.Width / 2, iy - item.DesiredSize.Height / 2);
                    action(item, ip);
                    index++;
                }
            }
        }

        return base.ArrangeOverride(finalSize);
    }

    protected override Size MeasureOverride(Size availableSize)
    {
        return base.MeasureOverride(availableSize);
    }
}

public enum DockType
{
    Left,
    Right,
    Bottom,
    Top,
    LeftAndRight,
    TopAndBottom
}
