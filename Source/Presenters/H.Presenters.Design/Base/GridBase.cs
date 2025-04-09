// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Presenters.Design.Base;

public class GridBase : Grid
{
    public Pen GridLinePen
    {
        get { return (Pen)GetValue(GridLinePenProperty); }
        set { SetValue(GridLinePenProperty, value); }
    }

    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty GridLinePenProperty =
        DependencyProperty.Register("GridLinePen", typeof(Pen), typeof(GridBase), new FrameworkPropertyMetadata(new Pen(Brushes.Black, 1), (d, e) =>
        {
            GridBase control = d as GridBase;

            if (control == null) return;

            if (e.OldValue is Pen o)
            {

            }

            if (e.NewValue is Pen n)
            {

            }

            control.InvalidateVisual();
        }));


    public double MinRowHeight
    {
        get { return (double)GetValue(MinRowHeightProperty); }
        set { SetValue(MinRowHeightProperty, value); }
    }

    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty MinRowHeightProperty =
        DependencyProperty.Register("MinRowHeight", typeof(double), typeof(GridBase), new FrameworkPropertyMetadata(default(double), (d, e) =>
        {
            GridBase control = d as GridBase;

            if (control == null) return;

            if (e.OldValue is double o)
            {

            }

            if (e.NewValue is double n)
            {

            }
            control.Refresh();
        }));



    protected override void OnRender(DrawingContext dc)
    {
        base.OnRender(dc);

        if (this.GridLinePen == null)
            return;

        foreach (var item in this.RowDefinitions)
        {
            dc.DrawLine(this.GridLinePen, new Point(0, item.Offset), new Point(this.ActualWidth, item.Offset));
        }
        dc.DrawLine(this.GridLinePen, new Point(0, this.ActualHeight), new Point(this.ActualWidth, this.ActualHeight));

        foreach (var item in this.ColumnDefinitions)
        {
            dc.DrawLine(this.GridLinePen, new Point(item.Offset, 0), new Point(item.Offset, this.ActualHeight));
        }
        dc.DrawLine(this.GridLinePen, new Point(this.ActualWidth, 0), new Point(this.ActualWidth, this.ActualHeight));
    }

    protected override Size ArrangeOverride(Size arrangeSize)
    {
        InvalidateVisual();
        return base.ArrangeOverride(arrangeSize);
    }

    protected virtual void Refresh()
    {

    }


}
