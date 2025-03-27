// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Presenters.Design.Presenter;

public class FixedGrid : GridBase
{
    public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(FixedGrid), "S.FixedGrid.Default");

    static FixedGrid()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(FixedGrid), new FrameworkPropertyMetadata(typeof(FixedGrid)));
    }
    public int Rows
    {
        get { return (int)GetValue(RowsProperty); }
        set { SetValue(RowsProperty, value); }
    }

    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty RowsProperty =
        DependencyProperty.Register("Rows", typeof(int), typeof(FixedGrid), new FrameworkPropertyMetadata(default(int), (d, e) =>
        {
            FixedGrid control = d as FixedGrid;

            if (control == null) return;

            if (e.OldValue is int o)
            {

            }

            if (e.NewValue is int n)
            {

            }
            control.Refresh();
        }));


    public int Columns
    {
        get { return (int)GetValue(ColumnsProperty); }
        set { SetValue(ColumnsProperty, value); }
    }

    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty ColumnsProperty =
        DependencyProperty.Register("Columns", typeof(int), typeof(FixedGrid), new FrameworkPropertyMetadata(default(int), (d, e) =>
        {
            FixedGrid control = d as FixedGrid;

            if (control == null) return;

            if (e.OldValue is int o)
            {

            }

            if (e.NewValue is int n)
            {

            }
            control.Refresh();
        }));


    public GridLength RowGridLength
    {
        get { return (GridLength)GetValue(RowGridLengthProperty); }
        set { SetValue(RowGridLengthProperty, value); }
    }

    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty RowGridLengthProperty =
        DependencyProperty.Register("RowGridLength", typeof(GridLength), typeof(FixedGrid), new FrameworkPropertyMetadata(new GridLength(1, GridUnitType.Star), (d, e) =>
        {
            FixedGrid control = d as FixedGrid;

            if (control == null) return;

            if (e.OldValue is GridLength o)
            {

            }

            if (e.NewValue is GridLength n)
            {

            }

            control.Refresh();

        }));


    public GridLength ColumnGridLength
    {
        get { return (GridLength)GetValue(ColumnGridLengthProperty); }
        set { SetValue(ColumnGridLengthProperty, value); }
    }

    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty ColumnGridLengthProperty =
        DependencyProperty.Register("ColumnGridLength", typeof(GridLength), typeof(FixedGrid), new FrameworkPropertyMetadata(new GridLength(1, GridUnitType.Star), (d, e) =>
        {
            FixedGrid control = d as FixedGrid;

            if (control == null) return;

            if (e.OldValue is GridLength o)
            {

            }

            if (e.NewValue is GridLength n)
            {

            }

            control.Refresh();

        }));

    protected override void Refresh()
    {
        this.RowDefinitions.Clear();
        this.ColumnDefinitions.Clear();
        for (int i = 0; i < this.Rows; i++)
        {
            this.RowDefinitions.Add(new RowDefinition() { Height = this.RowGridLength, MinHeight = this.MinRowHeight });
        }
        for (int j = 0; j < this.Columns; j++)
        {
            this.ColumnDefinitions.Add(new ColumnDefinition() { Width = this.ColumnGridLength });
        }
    }
}
