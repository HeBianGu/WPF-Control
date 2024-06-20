﻿// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Windows;
using System.Windows.Controls;

namespace H.Controls.TreeListView
{
    public partial class TreeListView : TreeView
    {
        static TreeListView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TreeListView), new FrameworkPropertyMetadata(typeof(TreeListView)));
        }


        protected override DependencyObject GetContainerForItemOverride()
        {
            return new TreeListViewItem();
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is TreeListViewItem;
        }


        public object SelectItem
        {
            get { return GetValue(SelectItemProperty); }
            set { SetValue(SelectItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectItemProperty =
            DependencyProperty.Register("SelectItem", typeof(object), typeof(TreeListView), new PropertyMetadata(default(object), (d, e) =>
             {
                 TreeListView control = d as TreeListView;

                 if (control == null) return;

                 object config = e.NewValue;

             }));


        protected override void OnSelectedItemChanged(RoutedPropertyChangedEventArgs<object> e)
        {
            base.OnSelectedItemChanged(e);

            this.SelectItem = e.NewValue;
        }


        public GridViewColumnCollection Columns
        {
            get { return (GridViewColumnCollection)GetValue(ColumnsProperty); }
            set { SetValue(ColumnsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColumnsProperty =
            DependencyProperty.Register("Columns", typeof(GridViewColumnCollection), typeof(TreeListView), new PropertyMetadata(new GridViewColumnCollection(), (d, e) =>
             {
                 TreeListView control = d as TreeListView;

                 if (control == null) return;

                 GridViewColumnCollection config = e.NewValue as GridViewColumnCollection;

             }));

    }

    public partial class TreeListView
    {
        public static new ComponentResourceKey DefaultStyleKey => new ComponentResourceKey(typeof(TreeListView), "S.TreeListView.Default");
    }
}
