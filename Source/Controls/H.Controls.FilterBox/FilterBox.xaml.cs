global using H.Common.Interfaces.Where;
global using System;
global using System.Collections;
global using System.Collections.ObjectModel;
global using System.Linq;
global using System.Windows;
global using System.Windows.Controls;

namespace H.Controls.FilterBox
{
    public interface IFilterBox
    {
        IFilterable Filter { get; set; }
        ObservableCollection<IFilterable> Filters { get; }
        bool UseCheckAll { get; set; }
        event RoutedEventHandler FilterChanged;
        ListBoxItem GetCheckAllItem();
        void RefreshData();
    }

    public class FilterBox : ListBox, IFilterBox
    {
        static FilterBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FilterBox), new FrameworkPropertyMetadata(typeof(FilterBox)));
        }

        public FilterBox()
        {
            this.Filters.CollectionChanged += (l, k) =>
            {
                this.RefreshData();
            };
        }

        public ListBoxItem GetCheckAllItem()
        {
            return this.Dispatcher.Invoke(() =>
            {
                if (this.Items.Count > 0 && this.UseCheckAll)
                    return this.ItemContainerGenerator.ContainerFromIndex(0) as ListBoxItem;
                return null;
            });
        }

        public IFilterable Filter
        {
            get { return (IFilterable)GetValue(FilterProperty); }
            set { SetValue(FilterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FilterProperty =
            DependencyProperty.Register("Filter", typeof(IFilterable), typeof(FilterBox), new FrameworkPropertyMetadata(default(IFilterable), (d, e) =>
            {
                FilterBox control = d as FilterBox;

                if (control == null) return;

                if (e.OldValue is IFilterable o)
                {

                }

                if (e.NewValue is IFilterable n)
                {

                }

            }));

        public ObservableCollection<IFilterable> Filters { get; } = new ObservableCollection<IFilterable>();

        //声明和注册路由事件
        public static readonly RoutedEvent FilterChangedRoutedEvent =
            EventManager.RegisterRoutedEvent("FilterChanged", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(FilterBox));
        //CLR事件包装
        public event RoutedEventHandler FilterChanged
        {
            add { this.AddHandler(FilterChangedRoutedEvent, value); }
            remove { this.RemoveHandler(FilterChangedRoutedEvent, value); }
        }

        //激发路由事件,借用Click事件的激发方法
        protected void OnFilterChanged()
        {
            RoutedEventArgs args = new RoutedEventArgs(FilterChangedRoutedEvent, this);
            this.RaiseEvent(args);
        }

        public bool UseCheckAll
        {
            get { return (bool)GetValue(UseCheckAllProperty); }
            set { SetValue(UseCheckAllProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseCheckAllProperty =
            DependencyProperty.Register("UseCheckAll", typeof(bool), typeof(FilterBox), new FrameworkPropertyMetadata(true, (d, e) =>
            {
                FilterBox control = d as FilterBox;

                if (control == null) return;

                if (e.OldValue is bool o)
                {

                }

                if (e.NewValue is bool n)
                {

                }
                control.RefreshData();
            }));

        public void RefreshData()
        {
            this.ItemsSource = this.GetItemsSource();
        }

        private IEnumerable GetItemsSource()
        {
            if (this.UseCheckAll)
                yield return "全选";
            foreach (IFilterable data in this.Filters)
            {
                yield return data;
            }
        }

        private bool _flag = false;
        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);
            if (_flag == true)
                return;
            ListBoxItem checkItem = this.GetCheckAllItem();
            if (checkItem != null)
            {

                if (e.AddedItems.Count == 1 && this.Items[0] == e.AddedItems[0] && checkItem.IsMouseOver)
                {
                    _flag = true;
                    foreach (object item in this.Items)
                    {
                        this.SelectedItems.Add(item);
                    }
                    _flag = false;
                }

                else if (e.RemovedItems.Count == 1 && this.Items[0] == e.RemovedItems[0] && checkItem.IsMouseOver)
                {
                    _flag = true;
                    foreach (object item in this.Items)
                    {
                        this.SelectedItems.Remove(item);
                    }
                    _flag = false;
                }
                else
                {
                    if (this.SelectedItems.Count + e.AddedItems.Count >= this.Items.Count)
                        checkItem.IsSelected = true;
                    else
                        checkItem.IsSelected = false;
                }
            }

            this.Filter = new Filter(this);
            this.OnFilterChanged();
        }

    }

    public class Filter : IFilterable
    {
        private readonly FilterBox _filterBox;
        public Filter(FilterBox SelectionFilterBox)
        {
            _filterBox = SelectionFilterBox;
        }

        public bool IsMatch(object obj)
        {
            IList list = this._filterBox.Dispatcher.Invoke(() => this._filterBox.SelectedItems);
            var filters = list.OfType<IFilterable>();
            if (filters == null || filters.Count() == 0)
                return true;
            return filters.Any(f => f.IsMatch(obj));
        }
    }
}
