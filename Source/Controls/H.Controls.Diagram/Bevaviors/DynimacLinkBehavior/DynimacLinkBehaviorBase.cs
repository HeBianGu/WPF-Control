// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using Microsoft.Xaml.Behaviors;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace H.Controls.Diagram.Bevaviors.DynimacLinkBehavior;

public abstract class DynimacLinkBehaviorBase<T> : Behavior<Diagram> where T : FrameworkElement
{
    [Obsolete]
    protected override void OnAttached()
    {
        this.AssociatedObject.PreviewMouseRightButtonDown += Diagram_PreviewMouseDown;
        this.AssociatedObject.PreviewMouseMove += Diagram_PreviewMouseMove;
        this.AssociatedObject.PreviewMouseRightButtonUp += Diagram_PreviewMouseUp;

        //   AssociatedObject.AddHandler(UIElement.MouseRightButtonUpEvent,
        //new MouseButtonEventHandler(Diagram_PreviewMouseUp), true);
        this.AssociatedObject.MouseLeave += AssociatedObject_MouseLeave;
    }

    private void AssociatedObject_MouseLeave(object sender, MouseEventArgs e)
    {

        //System.Diagnostics.Debug.WriteLine("说明");

    }

    [Obsolete]
    protected override void OnDetaching()
    {
        this.AssociatedObject.PreviewMouseRightButtonDown -= Diagram_PreviewMouseDown;
        this.AssociatedObject.PreviewMouseMove -= Diagram_PreviewMouseMove;
        this.AssociatedObject.PreviewMouseRightButtonUp -= Diagram_PreviewMouseUp;

        //        AssociatedObject.RemoveHandler(UIElement.MouseRightButtonUpEvent,
        //new MouseButtonEventHandler(Diagram_PreviewMouseUp));
    }


    private void Diagram_PreviewMouseDown(object sender, MouseButtonEventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("Diagram_PreviewMouseDown");
        Point point = Mouse.GetPosition(this.AssociatedObject);
        PointHitTestParameters parameters = new PointHitTestParameters(point);
        VisualTreeHelper.HitTest(this.AssociatedObject, HitTestFilter, HitTestCallBack, parameters);
    }

    private HitTestResultBehavior HitTestCallBack(HitTestResult result)
    {
        return result.VisualHit is T ? HitTestResultBehavior.Stop : HitTestResultBehavior.Continue;
    }

    private HitTestFilterBehavior HitTestFilter(DependencyObject obj)
    {
        Type type = obj.GetType();

        if (type.Name == this.GetType().Name) return HitTestFilterBehavior.ContinueSkipSelf;

        if (obj is T item)
            this.InitDynamic(item);

        return HitTestFilterBehavior.Continue;
    }

    //bool _isRefreshing;

    protected Point point;
    protected Point previous;
    private PointHitTestParameters parameters;

    [Obsolete]
    private void Diagram_PreviewMouseMove(object sender, MouseEventArgs e)
    {
        if (this.AssociatedObject._dynamicLink.Visibility != Visibility.Visible) return;

        if (e.MouseDevice.RightButton == MouseButtonState.Released) return;
#if DEBUG
        DateTime dateTime = DateTime.Now;
#endif

        point = Mouse.GetPosition(this.AssociatedObject);
        previous = LinkLayer.GetEnd(this.AssociatedObject._dynamicLink);
        parameters = new PointHitTestParameters(point);
        LinkLayer.SetEnd(this.AssociatedObject._dynamicLink, point);

        //if (_isRefreshing)
        //    return;
        //_isRefreshing = true;

        this.Dispatcher.DelayInvoke(DispatcherPriority.SystemIdle, new Action(() =>
        {
            //_isRefreshing = false;
            Vector v = point - previous;
            this.AssociatedObject._dynamicLink.ToPort.Dock = Math.Abs(v.X) > Math.Abs(v.Y) ? v.X > 0 ? Dock.Left : Dock.Right : v.Y > 0 ? Dock.Top : Dock.Bottom;

            VisualTreeHelper.HitTest(this.AssociatedObject.NodeLayer, MouseMoveHitTestFilter, HitTestCallBack, parameters);
            this.AssociatedObject._dynamicLink.Update();

#if DEBUG
            TimeSpan span = DateTime.Now - dateTime;
            System.Diagnostics.Debug.WriteLine("Diagram_PreviewMouseMove：" + span.ToString());
#endif
        }));

    }
    protected abstract HitTestFilterBehavior MouseMoveHitTestFilter(DependencyObject obj);

    private void Diagram_PreviewMouseUp(object sender, MouseButtonEventArgs e)
    {
        //System.Diagnostics.Debug.WriteLine("Diagram_PreviewMouseUp");

#if DEBUG
        DateTime dateTime = DateTime.Now;
#endif 
        Point point = Mouse.GetPosition(this.AssociatedObject);
        PointHitTestParameters parameters = new PointHitTestParameters(point);

        VisualTreeHelper.HitTest(this.AssociatedObject.NodeLayer, MouseUpHitTestFilter, HitTestCallBack, parameters);

        this.Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new Action(() =>
          {
              this.ClearDynamic();
              LinkLayer.SetStart(this.AssociatedObject._dynamicLink, new Point(0, 0));
              LinkLayer.SetEnd(this.AssociatedObject._dynamicLink, new Point(0, 0));
          }));

#if DEBUG
        TimeSpan span = DateTime.Now - dateTime;
        System.Diagnostics.Debug.WriteLine("Diagram_PreviewMouseUp：" + span.ToString());
#endif 
    }


    protected abstract void ClearDynamic();

    private HitTestFilterBehavior MouseUpHitTestFilter(DependencyObject obj)
    {
        Type type = obj.GetType();

        if (type.Name == this.GetType().Name) return HitTestFilterBehavior.ContinueSkipSelf;

        if (obj is T item)
        {
            this.Create(item);

            return HitTestFilterBehavior.Stop;
        }

        return HitTestFilterBehavior.Continue;
    }

    protected abstract void Create(T t);

    protected abstract void InitDynamic(T t);

    protected void SetMessage(string message)
    {
        this.AssociatedObject.InvokeMessage(message);
        this.AssociatedObject._dynamicLink.SetDefaultMessage(message);
    }
}
