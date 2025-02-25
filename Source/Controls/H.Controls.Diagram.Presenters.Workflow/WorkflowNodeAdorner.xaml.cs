// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Controls.Adorner.Adorner.ControlTemplateAdorners;
using System.Windows;
using System.Windows.Controls;

namespace H.Controls.Diagram.Presenters.Workflow;

public class WorkflowNodeAdorner : ControlTemplateAdorner
{
    static WorkflowNodeAdorner()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(WorkflowNodeAdorner), new FrameworkPropertyMetadata(typeof(WorkflowNodeAdorner)));
    }

    public WorkflowNodeAdorner(UIElement adornedElement) : base(adornedElement)
    {

    }

    protected override ControlTemplate CreateTemplate()
    {
        return WorkflowNodeAdorner.GetTemplate(this.AdornedElement);
    }


    public static new ControlTemplate GetTemplate(DependencyObject obj)
    {
        return (ControlTemplate)obj.GetValue(TemplateProperty);
    }

    public static new void SetTemplate(DependencyObject obj, ControlTemplate value)
    {
        obj.SetValue(TemplateProperty, value);
    }


    public static new readonly DependencyProperty TemplateProperty =
        DependencyProperty.RegisterAttached("Template", typeof(ControlTemplate), typeof(WorkflowNodeAdorner), new PropertyMetadata(default(ControlTemplate), OnTemplateChanged));

    public static new void OnTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d;

        ControlTemplate n = (ControlTemplate)e.NewValue;

        ControlTemplate o = (ControlTemplate)e.OldValue;
    }



    //protected override ControlTemplate CreateTemplate()
    //{
    //    return WorkflowNodeAdorner.GetTemplate(this.AdornedElement) as ControlTemplate;
    //}

    //protected override void OnRender(DrawingContext drawingContext)
    //{
    //    //this._contentControl.Content = 2222;
    //    this._contentControl.Template = this.Template;
    //    this._contentControl.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
    //    Point point = new Point();
    //    point.X = (this.RenderSize.Width - this._contentControl.DesiredSize.Width) / 2;
    //    point.Y = (this.RenderSize.Height - this._contentControl.DesiredSize.Height) / 2;
    //    this._contentControl.Arrange(new Rect(point,this._contentControl.DesiredSize));
    //} 

    protected override Size MeasureOverride(Size constraint)
    {
        base.MeasureOverride(constraint);
        return this.AdornedElement.DesiredSize;
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
        base.ArrangeOverride(finalSize);
        this._contentControl.Arrange(new Rect(new Point(0, 0), this.AdornedElement.DesiredSize));
        return finalSize;
    }
}
