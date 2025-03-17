// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control
global using H.Controls.Diagram.Layers.Base;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace H.Controls.Diagram.Layers;

public class LinkLayer : Layer
{

    /// <summary>
    /// 说明
    /// </summary>
    public static readonly DependencyProperty StartProperty = DependencyProperty.RegisterAttached(
        "Start", typeof(Point), typeof(LinkLayer), new FrameworkPropertyMetadata(default(Point), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnStartChanged));

    public static Point GetStart(DependencyObject d)
    {
        return (Point)d.GetValue(StartProperty);
    }

    public static void SetStart(DependencyObject obj, Point value)
    {
        obj.SetValue(StartProperty, value);
    }

    private static void OnStartChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
    {
        if (sender is Link link)
        {
            //link.Update();
        }
    }


    /// <summary>
    /// 说明
    /// </summary>
    public static readonly DependencyProperty EndProperty = DependencyProperty.RegisterAttached(
        "End", typeof(Point), typeof(LinkLayer), new FrameworkPropertyMetadata(default(Point), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnEndChanged));

    public static Point GetEnd(DependencyObject d)
    {
        return (Point)d.GetValue(EndProperty);
    }

    public static void SetEnd(DependencyObject obj, Point value)
    {
        obj.SetValue(EndProperty, value);
    }

    private static void OnEndChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
    {
        if (sender is Link link)
        {
            //link.Update();
        }
    }


    protected override Size MeasureOverride(Size constraint)
    {
        Size childConstraint = new Size(double.PositiveInfinity, double.PositiveInfinity);

        foreach (UIElement child in this.Children)
        {
            if (child == null) continue;
            child.Measure(childConstraint);
        }

        return new Size();
    }

    protected override Size ArrangeOverride(Size arrangeSize)
    {
#if DEBUG
        DateTime dateTime = DateTime.Now;
#endif
        int ssss = this.Children.Count;
        foreach (Link child in this.Children)
        {
            if (child == null) continue;                 //Point start = LinkLayer.GetStart(child);
            //Point end = LinkLayer.GetEnd(child);
            //child.Draw(start, end);
            child.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            child.Arrange(new Rect(new Point(0, 0), child.DesiredSize));
            //if (this.UseAnimation)
            //{
            //    //child.BeginAnimationOpacity(0, 1, this.Duration.TotalMilliseconds * 3);
            //}
            //else
            //{
            child.Opacity = 1;
            //}
        }
#if DEBUG
        TimeSpan span = DateTime.Now - dateTime;
        System.Diagnostics.Debug.WriteLine("LinkLayer.ArrangeOverride：" + span.ToString());
#endif 
        return base.ArrangeOverride(arrangeSize);
    }

    private Storyboard _storyboard;
    /// <summary>
    ///     更新路径
    /// </summary>
    protected void RunPath(Path path, double MilliSecond = 1000)
    {
        //if (!this.UseAnimation) return;

        double _pathLength = path.Data.GetTotalLength(new Size(path.ActualWidth, path.ActualHeight), path.StrokeThickness) * 2;

        if (Math.Abs(_pathLength) < 1E-06) return;

        path.StrokeDashOffset = _pathLength;

        path.StrokeDashArray = new DoubleCollection(new List<double>
        {
            _pathLength,
            _pathLength
        });

        //定义动画
        if (_storyboard != null)
            _storyboard.Stop();
        _storyboard = new Storyboard();

        DoubleAnimationUsingKeyFrames frames = new DoubleAnimationUsingKeyFrames();

        LinearDoubleKeyFrame frame0 = new LinearDoubleKeyFrame
        {
            Value = _pathLength,
            KeyTime = KeyTime.FromTimeSpan(TimeSpan.Zero)
        };

        LinearDoubleKeyFrame frame1 = new LinearDoubleKeyFrame
        {
            Value = 0,
            KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(MilliSecond))
        };
        frames.KeyFrames.Add(frame0);
        frames.KeyFrames.Add(frame1);

        Storyboard.SetTarget(frames, path);
        Storyboard.SetTargetProperty(frames, new PropertyPath(Shape.StrokeDashOffsetProperty));
        _storyboard.Children.Add(frames);

        _storyboard.Begin();
    }


}
