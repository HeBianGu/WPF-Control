// Copyright ? 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control
using Microsoft.Xaml.Behaviors;
using Microsoft.Xaml.Behaviors.Input;
using System.Windows;
using System.Windows.Input;
namespace H.Extensions.Behvaiors.Triggers;

/// <summary>
/// 可以注册 单击 双击 ..N击的触发器
/// </summary>
public class MouseTrigger : EventTriggerBase<UIElement>
{
    public static readonly DependencyProperty FiredOnProperty = DependencyProperty.Register("FiredOn", typeof(KeyTriggerFiredOn), typeof(MouseTrigger));
    /// <summary>
    /// Determines whether or not to listen to the KeyDown or KeyUp event.
    /// </summary>
    public KeyTriggerFiredOn FiredOn
    {
        get { return (KeyTriggerFiredOn)this.GetValue(FiredOnProperty); }
        set { this.SetValue(FiredOnProperty, value); }
    }

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
        DependencyProperty.Register("MouseButton", typeof(MouseButton), typeof(MouseTrigger), new FrameworkPropertyMetadata(default(MouseButton), (d, e) =>
        {
            MouseTrigger control = d as MouseTrigger;

            if (control == null) return;

            if (e.OldValue is MouseButton o)
            {

            }

            if (e.NewValue is MouseButton n)
            {

            }

        }));


    public MouseTriggerMode Mode
    {
        get { return (MouseTriggerMode)GetValue(ModeProperty); }
        set { SetValue(ModeProperty, value); }
    }

    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty ModeProperty =
        DependencyProperty.Register("Mode", typeof(MouseTriggerMode), typeof(MouseTrigger), new FrameworkPropertyMetadata(default(MouseTriggerMode), (d, e) =>
        {
            MouseTrigger control = d as MouseTrigger;

            if (control == null) return;

            if (e.OldValue is MouseTriggerMode o)
            {

            }

            if (e.NewValue is MouseTriggerMode n)
            {

            }

        }));

    public int ClickCount
    {
        get { return (int)GetValue(ClickCountProperty); }
        set { SetValue(ClickCountProperty, value); }
    }

    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty ClickCountProperty =
        DependencyProperty.Register("ClickCount", typeof(int), typeof(MouseTrigger), new FrameworkPropertyMetadata(default(int), (d, e) =>
        {
            MouseTrigger control = d as MouseTrigger;

            if (control == null) return;

            if (e.OldValue is int o)
            {

            }

            if (e.NewValue is int n)
            {

            }

        }));


    public bool Handled
    {
        get { return (bool)GetValue(HandledProperty); }
        set { SetValue(HandledProperty, value); }
    }

    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty HandledProperty =
        DependencyProperty.Register("Handled", typeof(bool), typeof(MouseTrigger), new FrameworkPropertyMetadata(default(bool), (d, e) =>
        {
            MouseTrigger control = d as MouseTrigger;

            if (control == null) return;

            if (e.OldValue is bool o)
            {

            }

            if (e.NewValue is bool n)
            {

            }

        }));


    public bool UseHandle
    {
        get { return (bool)GetValue(UseHandleProperty); }
        set { SetValue(UseHandleProperty, value); }
    }

    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty UseHandleProperty =
        DependencyProperty.Register("UseHandle", typeof(bool), typeof(MouseTrigger), new FrameworkPropertyMetadata(true, (d, e) =>
        {
            MouseTrigger control = d as MouseTrigger;

            if (control == null) return;

            if (e.OldValue is bool o)
            {

            }

            if (e.NewValue is bool n)
            {

            }

        }));


    protected override void OnEvent(EventArgs eventArgs)
    {
        if (this.FiredOn == KeyTriggerFiredOn.KeyDown)
            this.Source.AddHandler(UIElement.MouseDownEvent, new MouseButtonEventHandler(OnMousePress), true);
        else
        {
            this.Source.AddHandler(UIElement.MouseUpEvent, new MouseButtonEventHandler(OnMousePress), true);
        }
    }

    List<MouseButtonEventArgs> _args = new List<MouseButtonEventArgs>();
    private void OnMousePress(object sender, MouseButtonEventArgs e)
    {
        System.Diagnostics.Debug.WriteLine(e.ClickCount);
        System.Diagnostics.Debug.WriteLine(e.ChangedButton);
        if (e.ClickCount != this.ClickCount)
            return;
        if (e.ChangedButton != this.MouseButton)
            return;
        if (e.Handled && this.UseHandle)
            return;

        if (this.AssociatedObject is FrameworkElement element)
        {
            Point point = e.GetPosition(element);
            if (this.Mode == MouseTriggerMode.Left && point.X > element.ActualWidth / 2)
                return;
            if (this.Mode == MouseTriggerMode.Right && point.X < element.ActualWidth / 2)
                return;
            if (this.Mode == MouseTriggerMode.Top && point.Y > element.ActualHeight / 2)
                return;
            if (this.Mode == MouseTriggerMode.Bottom && point.Y < element.ActualHeight / 2)
                return;
        }
        this.InvokeActions(e);
        e.Handled = this.Handled;
    }

    protected override void OnDetaching()
    {
        if (this.Source != null)
        {
            if (this.FiredOn == KeyTriggerFiredOn.KeyDown)
                this.Source.RemoveHandler(UIElement.MouseDownEvent, new MouseButtonEventHandler(OnMousePress));
            else
            {
                this.Source.RemoveHandler(UIElement.MouseUpEvent, new MouseButtonEventHandler(OnMousePress));
            }
        }

        base.OnDetaching();
    }
}


public enum MouseTriggerMode
{
    None = 0,
    Left,
    Right,
    Top,
    Bottom
}
