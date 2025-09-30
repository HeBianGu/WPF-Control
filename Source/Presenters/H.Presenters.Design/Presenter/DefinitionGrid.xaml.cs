// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Presenters.Design.Presenter;

public class DefinitionGrid : GridBase
{
    public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(DefinitionGrid), "S.DefinitionGrid.Default");
    static DefinitionGrid()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(DefinitionGrid), new FrameworkPropertyMetadata(typeof(DefinitionGrid)));
    }
    [TypeConverter(typeof(ObservableCollectionTypeConverter<GridLength, GridLengthConverter>))]
    public ObservableCollection<GridLength> Rows
    {
        get { return (ObservableCollection<GridLength>)GetValue(RowsProperty); }
        set { SetValue(RowsProperty, value); }
    }

    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty RowsProperty =
        DependencyProperty.Register("Rows", typeof(ObservableCollection<GridLength>), typeof(DefinitionGrid), new FrameworkPropertyMetadata(new ObservableCollection<GridLength>(), (d, e) =>
        {
            DefinitionGrid control = d as DefinitionGrid;

            if (control == null) return;

            if (e.OldValue is ObservableCollection<GridLength> o)
            {

            }

            if (e.NewValue is ObservableCollection<GridLength> n)
            {

            }
            control.Refresh();
        }));

    [TypeConverter(typeof(ObservableCollectionTypeConverter<GridLength, GridLengthConverter>))]
    public ObservableCollection<GridLength> Columns
    {
        get { return (ObservableCollection<GridLength>)GetValue(ColumnsProperty); }
        set { SetValue(ColumnsProperty, value); }
    }

    public static readonly DependencyProperty ColumnsProperty =
        DependencyProperty.Register("Columns", typeof(ObservableCollection<GridLength>), typeof(DefinitionGrid), new FrameworkPropertyMetadata(new ObservableCollection<GridLength>(), (d, e) =>
        {
            DefinitionGrid control = d as DefinitionGrid;

            if (control == null) return;

            if (e.OldValue is ObservableCollection<GridLength> o)
            {

            }

            if (e.NewValue is ObservableCollection<GridLength> n)
            {

            }
            control.Refresh();
        }));

    protected override void Refresh()
    {
        this.RowDefinitions.Clear();
        this.ColumnDefinitions.Clear();

        if (this.Rows != null)
        {
            foreach (var item in this.Rows)
            {
                this.RowDefinitions.Add(new RowDefinition() { Height = item, MinHeight = this.MinRowHeight });
            }
        }
        if (this.Columns != null)
        {
            foreach (var item in this.Columns)
            {
                this.ColumnDefinitions.Add(new ColumnDefinition() { Width = item });
            }
        }
    }
}
