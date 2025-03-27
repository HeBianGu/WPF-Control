// Copyright ? 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control
using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Input;
namespace H.Extensions.Behvaiors.Triggers;

/// <summary>
/// 按住会没间隔一段时间执行
/// </summary>
public class MousePressTrigger : EventTriggerBase<UIElement>
{
    System.Timers.Timer _timer = new System.Timers.Timer();


    public MousePressTrigger()
    {
        _timer.Interval = this.Interval;
        _timer.Elapsed += (l, k) =>
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                ElapsedInvokeActions();
            });
        };
    }

    protected virtual void ElapsedInvokeActions()
    {
        this.InvokeActions(null);
    }

    protected void Stop()
    {
        _timer.Stop();
    }

    public bool UseInvokeOnDown
    {
        get { return (bool)GetValue(UseInvokeOnDownProperty); }
        set { SetValue(UseInvokeOnDownProperty, value); }
    }

    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty UseInvokeOnDownProperty =
        DependencyProperty.Register("UseInvokeOnDown", typeof(bool), typeof(MousePressTrigger), new FrameworkPropertyMetadata(true, (d, e) =>
        {
            MousePressTrigger control = d as MousePressTrigger;

            if (control == null) return;

            if (e.OldValue is bool o)
            {

            }

            if (e.NewValue is bool n)
            {

            }

        }));


    public double Interval
    {
        get { return (double)GetValue(IntervalProperty); }
        set { SetValue(IntervalProperty, value); }
    }

    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty IntervalProperty =
        DependencyProperty.Register("Interval", typeof(double), typeof(MousePressTrigger), new FrameworkPropertyMetadata(1000.0, (d, e) =>
        {
            MousePressTrigger control = d as MousePressTrigger;

            if (control == null) return;

            if (e.OldValue is double o)
            {

            }

            if (e.NewValue is double n)
            {

            }

        }));


    protected override string GetEventName()
    {
        return "Loaded";
    }

    public MouseButton MouseButton
    {
        get { return (MouseButton)GetValue(MouseButtonProperty); }
        set { SetValue(MouseButtonProperty, value); }
    }

    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty MouseButtonProperty =
        DependencyProperty.Register("MouseButton", typeof(MouseButton), typeof(MousePressTrigger), new FrameworkPropertyMetadata(default(MouseButton), (d, e) =>
        {
            MousePressTrigger control = d as MousePressTrigger;

            if (control == null) return;

            if (e.OldValue is MouseButton o)
            {

            }

            if (e.NewValue is MouseButton n)
            {

            }

        }));

    protected override void OnEvent(EventArgs eventArgs)
    {
        this.Source.AddHandler(UIElement.MouseDownEvent, new MouseButtonEventHandler(OnMouseDown), true);
        this.Source.AddHandler(UIElement.MouseUpEvent, new MouseButtonEventHandler(OnMouseUp), true);
    }

    private void OnMouseUp(object sender, MouseButtonEventArgs e)
    {
        _timer.Stop();
    }

    private void OnMouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton != this.MouseButton)
            return;
        if (e.ButtonState == MouseButtonState.Released)
            return;
        _timer.Start();

        if (this.UseInvokeOnDown)
            this.InvokeActions(e);
    }

    protected override void OnDetaching()
    {
        if (this.Source != null)
        {
            this.Source.RemoveHandler(UIElement.MouseDownEvent, new MouseButtonEventHandler(OnMouseDown));
            this.Source.RemoveHandler(UIElement.MouseUpEvent, new MouseButtonEventHandler(OnMouseUp));
        }

        base.OnDetaching();
    }
}
