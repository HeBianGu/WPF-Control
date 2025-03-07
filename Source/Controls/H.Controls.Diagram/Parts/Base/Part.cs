// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace H.Controls.Diagram.Parts.Base;

public abstract class Part : ContentPresenter
{
    public static RoutedUICommand DeleteCommand = new RoutedUICommand();

    //public string Id { get; set; } = Guid.NewGuid().ToString(); 
    public Rect Bound { get; private set; }

    internal void Measure()
    {
        this.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
    }

    internal void MeasureBound(Point center)
    {
        this.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

        this.Bound = new Rect(center.X - this.DesiredSize.Width / 2.0, center.Y - this.DesiredSize.Height / 2, this.DesiredSize.Width, this.DesiredSize.Height);

    }

    //internal void ArrangeBound()
    //{
    //    this.Arrange(this.Bound);
    //}
    private Diagram _diagram;
    public Diagram GetDiagram()
    {
        return _diagram == null ? (_diagram = this.GetParent<Diagram>()) : _diagram;
    }

    public Part()
    {
        CommandBinding binding = new CommandBinding(DeleteCommand, (l, k) =>
         {
             this.Delete();
         });

        this.CommandBindings.Add(binding);

        this.MouseLeftButtonDown += Part_MouseLeftButtonDown;
    }

    private void Part_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
#if DEBUG
        DateTime dateTime = DateTime.Now;
#endif  
        Diagram diagram = this.GetParent<Diagram>();
        diagram.SelectedPart = this;
        this.IsSelected = !this.IsSelected;
        this.RefreshSelected();
#if DEBUG
        TimeSpan span = DateTime.Now - dateTime;
        System.Diagnostics.Debug.WriteLine("Part_MouseLeftButtonDown：" + span.ToString());
#endif 

        if (this is Link)
            e.Handled = true;
    }

    //private void Control_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    //{
    //    this.IsSelected = !this.IsSelected;
    //}

    public bool IsSelected
    {
        get { return (bool)GetValue(IsSelectedProperty); }
        set { SetValue(IsSelectedProperty, value); }
    }


    public static readonly DependencyProperty IsSelectedProperty =
        DependencyProperty.Register("IsSelected", typeof(bool), typeof(Part), new PropertyMetadata(default(bool), (d, e) =>
         {
             Part control = d as Part;
             if (control == null)
                 return;
             control.OnSelectionChanged();
         }));



    public static readonly RoutedEvent SelectionChangedRoutedEvent =
        EventManager.RegisterRoutedEvent("SelectionChanged", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(Part));

    public event RoutedEventHandler SelectionChanged
    {
        add { this.AddHandler(SelectionChangedRoutedEvent, value); }
        remove { this.RemoveHandler(SelectionChangedRoutedEvent, value); }
    }



    protected void OnSelectionChanged()
    {
        RoutedEventArgs args = new RoutedEventArgs(SelectionChangedRoutedEvent, this);
        this.RaiseEvent(args);
    }

    private void RefreshSelected()
    {
        //if (!this.IsSelected)
        //    return;

        Diagram diagram = this.GetParent<Diagram>();
        foreach (Part child in diagram.GetChildren<Part>())
        {
            if (child != this && !Keyboard.IsKeyDown(Key.LeftCtrl))
                child.IsSelected = false;
        }
        diagram.SelectedPart = this;
    }

    /// <summary>
    /// 删除图层上的元素
    /// </summary>
    public virtual void Delete()
    {
        Diagram diagram = this.GetParent<Diagram>();
        //  Do ：删除显示数据
        Layer layer = this.GetParent<Layer>();
        if (layer == null)
            return;
        layer.Children.Remove(this);
        diagram?.OnItemsChanged();
    }

    /// <summary>
    /// 清理逻辑关系
    /// </summary>
    public virtual void Clear()
    {

    }




    /// <summary>
    /// 是否拖入
    /// </summary>
    public static readonly DependencyProperty IsDragEnterProperty = DependencyProperty.RegisterAttached(
        "IsDragEnter", typeof(bool), typeof(Part), new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnIsDragEnterChanged));

    public static bool GetIsDragEnter(DependencyObject d)
    {
        return (bool)d.GetValue(IsDragEnterProperty);
    }

    public static void SetIsDragEnter(DependencyObject obj, bool value)
    {
        obj.SetValue(IsDragEnterProperty, value);
    }

    private static void OnIsDragEnterChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
    {

    }


    /// <summary>
    /// 设置是否放入有效区域并且验证可以放下的效果
    /// </summary>
    public static readonly DependencyProperty IsCanDropProperty = DependencyProperty.RegisterAttached(
        "IsCanDrop", typeof(bool), typeof(Part), new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnIsCanDropChanged));

    public static bool GetIsCanDrop(DependencyObject d)
    {
        return (bool)d.GetValue(IsCanDropProperty);
    }

    public static void SetIsCanDrop(DependencyObject obj, bool value)
    {
        obj.SetValue(IsCanDropProperty, value);
    }

    private static void OnIsCanDropChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
    {

    }

    public bool HasError
    {
        get { return (bool)GetValue(HasErrorProperty); }
        set { SetValue(HasErrorProperty, value); }
    }


    public static readonly DependencyProperty HasErrorProperty =
        DependencyProperty.Register("HasError", typeof(bool), typeof(Part), new FrameworkPropertyMetadata(default(bool), (d, e) =>
        {
            Part control = d as Part;

            if (control == null) return;

            if (e.OldValue is bool o)
            {

            }

            if (e.NewValue is bool n)
            {

            }

        }));


    public T GetContent<T>()
    {
        return this.CheckAccess() ? (T)this.Content : this.Dispatcher.Invoke(() => (T)this.Content);
    }


    public virtual Part GetPrevious()
    {
        return this;
    }

    public virtual Part GetNext()
    {
        return this;
    }

    public virtual IData Data => this.GetContent<IData>();
}
