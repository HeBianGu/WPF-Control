// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace H.Presenters.Design
{
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

            if (GridLinePen == null)
                return;

            foreach (var item in RowDefinitions)
            {
                dc.DrawLine(GridLinePen, new Point(0, item.Offset), new Point(ActualWidth, item.Offset));
            }
            dc.DrawLine(GridLinePen, new Point(0, ActualHeight), new Point(ActualWidth, ActualHeight));

            foreach (var item in ColumnDefinitions)
            {
                dc.DrawLine(GridLinePen, new Point(item.Offset, 0), new Point(item.Offset, ActualHeight));
            }
            dc.DrawLine(GridLinePen, new Point(ActualWidth, 0), new Point(ActualWidth, ActualHeight));
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
            RowDefinitions.Clear();
            ColumnDefinitions.Clear();
            for (int i = 0; i < Rows; i++)
            {
                RowDefinitions.Add(new RowDefinition() { Height = RowGridLength, MinHeight = MinRowHeight });
            }
            for (int j = 0; j < Columns; j++)
            {
                ColumnDefinitions.Add(new ColumnDefinition() { Width = ColumnGridLength });
            }
        }
    }
}
