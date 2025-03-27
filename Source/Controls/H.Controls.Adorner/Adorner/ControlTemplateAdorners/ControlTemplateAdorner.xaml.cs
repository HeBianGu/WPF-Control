// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


global using H.Extensions.Common;
using H.Controls.Adorner.Adorner.Base;
using System.Windows;
using System.Windows.Controls;

namespace H.Controls.Adorner.Adorner.ControlTemplateAdorners;

public class ControlTemplateAdorner : VisualCollectionAdornerBase
{
    static ControlTemplateAdorner()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ControlTemplateAdorner), new FrameworkPropertyMetadata(typeof(ControlTemplateAdorner)));
    }

    //private VisualCollection visualCollection;
    protected ContentControl _contentControl { get; set; } = new ContentControl();

    public ControlTemplateAdorner(UIElement adornedElement) : base(adornedElement)
    {
        //visualCollection = new VisualCollection(this);
        this._contentControl.HorizontalAlignment = HorizontalAlignment.Stretch;
        this._contentControl.VerticalAlignment = VerticalAlignment.Stretch;
        this._contentControl.HorizontalContentAlignment = HorizontalAlignment.Stretch;
        this._contentControl.VerticalContentAlignment = VerticalAlignment.Stretch;
        _visualCollection.Add(this._contentControl);
        this._contentControl.Template = this.CreateTemplate();
        this._contentControl.Content = adornedElement.GetContent();
    }

    protected virtual ControlTemplate CreateTemplate()
    {
        return GetTemplate(this.AdornedElement);
    }

    public static ControlTemplate GetTemplate(DependencyObject obj)
    {
        return (ControlTemplate)obj.GetValue(TemplateProperty);
    }

    public static void SetTemplate(DependencyObject obj, ControlTemplate value)
    {
        obj.SetValue(TemplateProperty, value);
    }

    public static readonly DependencyProperty TemplateProperty =
        DependencyProperty.RegisterAttached("Template", typeof(ControlTemplate), typeof(ControlTemplateAdorner), new PropertyMetadata(default(ControlTemplate), OnTemplateChanged));

    public static void OnTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d;
        ControlTemplate n = (ControlTemplate)e.NewValue;
        ControlTemplate o = (ControlTemplate)e.OldValue;
    }

    protected override Size MeasureOverride(Size constraint)
    {
        this._contentControl.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
        base.MeasureOverride(constraint);
        return new Size(Math.Max(this._contentControl.DesiredSize.Width, this.AdornedElement.DesiredSize.Width), Math.Max(this._contentControl.DesiredSize.Height, this.AdornedElement.DesiredSize.Height));
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
        Size r = base.ArrangeOverride(finalSize);
        this.RefreshLayout();
        return r;
    }

    protected virtual void RefreshLayout()
    {
        //Point point = new Point();
        //point.X = (this.AdornedElement.DesiredSize.Width - this._contentControl.DesiredSize.Width) / 2;
        //point.Y = (this.AdornedElement.DesiredSize.Height - this._contentControl.DesiredSize.Height) / 2;
        //this._contentControl.Arrange(new Rect(point, this._contentControl.DesiredSize));
        this._contentControl.Arrange(new Rect(new Point(0, 0), this.AdornedElement.RenderSize));
    }

    //protected override Visual GetVisualChild(int index)
    //{
    //    return visualCollection[index];
    //}
    //protected override int VisualChildrenCount
    //{
    //    get
    //    {
    //        return visualCollection.Count;
    //    }
    //}
}
