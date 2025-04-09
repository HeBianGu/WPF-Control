// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control
using Microsoft.Xaml.Behaviors;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;

namespace H.Controls.Adorner.Draggable.Bevhavior;

public abstract class DraggableAdornerBehaviorBase : Behavior<UIElement>
{
    private Point startPoint;
    private IDraggableAdorner adorner;
    //  Do ：鼠标放下时加载要显示的AdornerElement,默认应该是AssociatedObject，但存在Popup时需要加载Popup.PlacementTaget
    private UIElement _adornerElement;
    private bool _isCapture;

    public double Opacity
    {
        get { return (double)GetValue(OpacityProperty); }
        set { SetValue(OpacityProperty, value); }
    }
    public static readonly DependencyProperty OpacityProperty =
        DependencyProperty.Register("Opacity", typeof(double), typeof(DraggableAdornerBehavior), new PropertyMetadata(0.5));

    public Type AncestorType
    {
        get { return (Type)GetValue(AncestorTypeProperty); }
        set { SetValue(AncestorTypeProperty, value); }
    }


    public static readonly DependencyProperty AncestorTypeProperty =
        DependencyProperty.Register("AncestorType", typeof(Type), typeof(DraggableAdornerBehavior), new PropertyMetadata(typeof(ScrollViewer)));

    /// <summary> 判断可否放置的分组 </summary>
    public string DragGroup
    {
        get { return (string)GetValue(DragGroupProperty); }
        set { SetValue(DragGroupProperty, value); }
    }


    public static readonly DependencyProperty DragGroupProperty =
        DependencyProperty.Register("DragGroup", typeof(string), typeof(DraggableAdornerBehavior), new PropertyMetadata("DragGroup"));


    public DragDropEffects DragDropEffects
    {
        get { return (DragDropEffects)GetValue(DragDropEffectsProperty); }
        set { SetValue(DragDropEffectsProperty, value); }
    }


    public static readonly DependencyProperty DragDropEffectsProperty =
        DependencyProperty.Register("DragDropEffects", typeof(DragDropEffects), typeof(DraggableAdornerBehavior), new PropertyMetadata(DragDropEffects.Copy));

    public RoutingStrategy RoutingStrategy { get; set; } = RoutingStrategy.Bubble;
    public DraggableAdornerMode DropAdornerMode { get; set; }

    protected override void OnAttached()
    {
        this.AssociatedObject.AllowDrop = true;
        if (this.RoutingStrategy == RoutingStrategy.Bubble)
        {
            this.AssociatedObject.PreviewMouseDown += AssociatedObject_MouseDown;
            this.AssociatedObject.PreviewMouseUp += AssociatedObject_MouseUp;
            this.AssociatedObject.PreviewMouseMove += AssociatedObject_MouseMove;
        }
        else
        {
            this.AssociatedObject.MouseDown += AssociatedObject_MouseDown;
            this.AssociatedObject.MouseUp += AssociatedObject_MouseUp;
            this.AssociatedObject.MouseMove += AssociatedObject_MouseMove;
        }

        this.AssociatedObject.MouseLeave += AssociatedObject_MouseLeave;
        this.AssociatedObject.GiveFeedback += AssociatedObject_GiveFeedback;
    }

    protected override void OnDetaching()
    {
        this.AssociatedObject.AllowDrop = false;

        this.AssociatedObject.PreviewMouseDown -= AssociatedObject_MouseDown;
        this.AssociatedObject.PreviewMouseUp -= AssociatedObject_MouseUp;
        this.AssociatedObject.PreviewMouseMove -= AssociatedObject_MouseMove;
        this.AssociatedObject.MouseDown -= AssociatedObject_MouseDown;
        this.AssociatedObject.MouseUp -= AssociatedObject_MouseUp;
        this.AssociatedObject.MouseMove -= AssociatedObject_MouseMove;
        this.AssociatedObject.MouseLeave -= AssociatedObject_MouseLeave;
        this.AssociatedObject.GiveFeedback -= AssociatedObject_GiveFeedback;
    }



    private void AssociatedObject_GiveFeedback(object sender, GiveFeedbackEventArgs e)
    {
        if (adorner != null)
        {
            //Visual lbl = sender as Visual;
            //PresentationSource source = PresentationSource.FromVisual(lbl);
            //if (source == null || source.RootVisual is Popup)
            //{
            //    Point pos = (Application.Current.MainWindow.Content as FrameworkElement).PointFromScreen(GetMousePosition());
            //    adorner.UpdatePosition(pos);
            //}
            //else
            //{
            //    Point pos = lbl.PointFromScreen(GetMousePosition());
            //    adorner.UpdatePosition(pos);
            //}

            Point pos = this._adornerElement.PointFromScreen(GetMousePosition());
            adorner.UpdatePosition(pos);
        }
    }

    private void AssociatedObject_MouseMove(object sender, MouseEventArgs e)
    {
        if (!_isCapture)
            return;
        if (e.LeftButton == MouseButtonState.Released)
            return;
        Point current = e.GetPosition(this.AssociatedObject);
        Vector diff = startPoint - current;
        if (Math.Abs(diff.X) < SystemParameters.MinimumHorizontalDragDistance &&
            Math.Abs(diff.Y) < SystemParameters.MinimumVerticalDragDistance)
            return;

        UIElement adornerElement = this._adornerElement;
        UIElement control = this.AncestorType == null ? adornerElement : adornerElement.GetParent(this.AncestorType) as UIElement;
        AdornerLayer layer = AdornerLayer.GetAdornerLayer(control ?? adornerElement);
        IEnumerable<System.Windows.Documents.Adorner> find = layer.GetAdorners(adornerElement)?.Where(l => l == adorner);
        if (find != null && find.Count() > 0)
            layer.Remove(adorner as System.Windows.Documents.Adorner);
        var offset = e.GetPosition(this.AssociatedObject);
        adorner = this.CreateDragAdorner(adornerElement, offset);
        this.BeforeDoDragDrop(adornerElement);
        layer.Add(adorner as System.Windows.Documents.Adorner);
        this.DoDragDrop();
        layer.Remove(adorner as System.Windows.Documents.Adorner);
    }

    protected virtual void BeforeDoDragDrop(UIElement element)
    {
        if (this.DragDropEffects == DragDropEffects.Move)
        {
            if (element.GetContent() is IDoDragDrop doDragDrop)
                doDragDrop.DoDragDrop(element, (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control ? DragDropEffects.Copy : DragDropEffects.Move);
        }
    }

    protected abstract IDraggableAdorner CreateDragAdorner(UIElement adornerElement, Point offset);

    /// <summary>
    /// 用于子类重写拖拽时放入的数据
    /// </summary>
    /// <returns></returns>
    protected virtual void DoDragDrop()
    {
        DataObject dragData = new DataObject(this.DragGroup, adorner);
        DragDrop.DoDragDrop(this.AssociatedObject, dragData, this.DragDropEffects);
    }


    private void AssociatedObject_MouseUp(object sender, MouseButtonEventArgs e)
    {
        //Mouse.OverrideCursor = Cursors.Arrow;
        _isCapture = false;
    }

    private void AssociatedObject_MouseLeave(object sender, MouseEventArgs e)
    {
        //Mouse.OverrideCursor = Cursors.Arrow;
        _isCapture = false;
    }

    private void AssociatedObject_MouseDown(object sender, MouseButtonEventArgs e)
    {
        startPoint = e.GetPosition(sender as UIElement);
        _isCapture = true;

        // 初始化应用Adorner的元素，如果是Popup则使用PlacementTarget
        FrameworkElement lbl = sender as FrameworkElement;
        var popup = lbl.GetParent<Popup>();
        this._adornerElement = popup != null ? popup.PlacementTarget : lbl;

        //PresentationSource source = PresentationSource.FromVisual(lbl);
        //if (source == null)
        //    this._adornerElement = Application.Current.MainWindow;
        //else if (source.RootVisual is Popup popup)
        //    this._adornerElement = popup.PlacementTarget;
        //else
        //    this._adornerElement = lbl;
    }

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool GetCursorPos(ref Win32Point pt);

    [StructLayout(LayoutKind.Sequential)]
    internal struct Win32Point
    {
        public int X;
        public int Y;
    };

    public static Point GetMousePosition()
    {
        Win32Point w32Mouse = new Win32Point();
        GetCursorPos(ref w32Mouse);
        return new Point(w32Mouse.X, w32Mouse.Y);
    }
}



