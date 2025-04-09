
using H.Mvvm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace H.Controls.FilterBox
{
    public interface ISelectionFilterBox
    {
        IEnumerable Datas { get; set; }
        string DisplayName { get; set; }
        IFilterable Filter { get; set; }
        string PropertyName { get; set; }
        Type Type { get; set; }
        bool UseCheckAll { get; set; }
        event RoutedEventHandler FilterChanged;
        ListBoxItem GetCheckAllItem();
        PropertyInfo GetPropertyInfo();
        void RefreshData();
    }

    public class SelectionFilterBox : ListBox, ISelectionFilterBox
    {
        static SelectionFilterBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SelectionFilterBox), new FrameworkPropertyMetadata(typeof(SelectionFilterBox)));
        }

        public SelectionFilterBox()
        {
            this.Filter = new SelectionFilter(this);
        }

        public string DisplayName
        {
            get { return (string)GetValue(DisplayNameProperty); }
            set { SetValue(DisplayNameProperty, value); }
        }


        public static readonly DependencyProperty DisplayNameProperty =
            DependencyProperty.Register("DisplayName", typeof(string), typeof(SelectionFilterBox), new FrameworkPropertyMetadata(default(string), (d, e) =>
            {
                SelectionFilterBox control = d as SelectionFilterBox;

                if (control == null) return;

                if (e.OldValue is string o)
                {

                }

                if (e.NewValue is string n)
                {

                }

            }));
        private bool _flag = false;
        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);
            if (_flag == true)
                return;
            ListBoxItem checkItem = this.GetCheckAllItem();
            if (checkItem == null)
                return;
            this.Filter = new SelectionFilter(this);
            this.OnFilterChanged();

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

        public Type Type
        {
            get { return (Type)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }


        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.Register("Type", typeof(Type), typeof(SelectionFilterBox), new FrameworkPropertyMetadata(default(Type), (d, e) =>
            {
                SelectionFilterBox control = d as SelectionFilterBox;

                if (control == null) return;

                if (e.OldValue is Type o)
                {

                }

                if (e.NewValue is Type n)
                {

                }
                control.DelayRefreshData();
            }));


        public bool UseCheckAll
        {
            get { return (bool)GetValue(UseCheckAllProperty); }
            set { SetValue(UseCheckAllProperty, value); }
        }


        public static readonly DependencyProperty UseCheckAllProperty =
            DependencyProperty.Register("UseCheckAll", typeof(bool), typeof(SelectionFilterBox), new FrameworkPropertyMetadata(true, (d, e) =>
            {
                SelectionFilterBox control = d as SelectionFilterBox;

                if (control == null) return;

                if (e.OldValue is bool o)
                {

                }

                if (e.NewValue is bool n)
                {

                }

            }));


        public string PropertyName
        {
            get { return (string)GetValue(PropertyNameProperty); }
            set { SetValue(PropertyNameProperty, value); }
        }


        public static readonly DependencyProperty PropertyNameProperty =
            DependencyProperty.Register("PropertyName", typeof(string), typeof(SelectionFilterBox), new FrameworkPropertyMetadata(default(string), (d, e) =>
            {
                SelectionFilterBox control = d as SelectionFilterBox;

                if (control == null) return;

                if (e.OldValue is string o)
                {

                }

                if (e.NewValue is string n)
                {

                }
                control.DelayRefreshData();
            }));

        public IEnumerable Datas
        {
            get { return (IEnumerable)GetValue(DatasProperty); }
            set { SetValue(DatasProperty, value); }
        }


        public static readonly DependencyProperty DatasProperty =
            DependencyProperty.Register("Datas", typeof(IEnumerable), typeof(SelectionFilterBox), new FrameworkPropertyMetadata(default(IEnumerable), (d, e) =>
            {
                SelectionFilterBox control = d as SelectionFilterBox;
                if (control == null)
                    return;
                if (e.OldValue is INotifyCollectionChanged o)
                    o.CollectionChanged -= control.CollectionChanged;
                if (e.NewValue is INotifyCollectionChanged n)
                    n.CollectionChanged += control.CollectionChanged;
                control.DelayRefreshData();
            }));

        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.DelayRefreshData();
        }

        private void DelayRefreshData()
        {
            this.DelayInvoke(() =>
            {
                if (this.IsLoaded == false)
                    return;
                this.RefreshData();
            });
        }


        public void RefreshData()
        {
            PropertyInfo propertyInfo = this.GetPropertyInfo();
            if (propertyInfo == null)
                return;
            if (this.Datas == null)
            {
                if (this.ItemsSource is IList list)
                    list.Clear();
                else
                    this.Items.Clear();
                return;
            }
            try
            {
                var datas = this.Datas.OfType<object>().ToList();
                this.ItemsSource = this.GetItemsSource(propertyInfo, datas);
            }
            catch (Exception ex)
            {
                IocLog.Instance?.Error(ex);
            }
        }

        public PropertyInfo GetPropertyInfo()
        {
            var propertyName = this.Dispatcher.Invoke(() => this.PropertyName);
            var type = this.Dispatcher.Invoke(() => this.Type);
            if (type == null)
                return null;
            if (string.IsNullOrWhiteSpace(propertyName))
                return null;
            PropertyInfo propertyInfo = type.GetProperty(propertyName);
            return propertyInfo;
        }

        private IEnumerable GetItemsSource(PropertyInfo propertyInfo, List<object> datas)
        {
            List<object> items = new List<object>();
            //yield return "全选";
            foreach (object data in datas)
            {
                if (data == null)
                    continue;
                object model = data;
                if (data is IModelBindable mv)
                    model = mv.GetModel();
                if (!this.Type.IsAssignableFrom(model.GetType()))
                    continue;
                object value = propertyInfo.GetValue(model);
                if (items.Contains(value))
                    continue;
                items.Add(value);
            }
            var order = items.Order().ToList();
            if (this.UseCheckAll)
                order.Insert(0, "全选");
            return order;
        }

        public IFilterable Filter
        {
            get { return (SelectionFilter)GetValue(FilterProperty); }
            set { SetValue(FilterProperty, value); }
        }


        public static readonly DependencyProperty FilterProperty =
            DependencyProperty.Register("Filter", typeof(IFilterable), typeof(SelectionFilterBox), new FrameworkPropertyMetadata(default(IFilterable), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) =>
            {
                SelectionFilterBox control = d as SelectionFilterBox;

                if (control == null) return;

                if (e.OldValue is IFilterable o)
                {

                }

                if (e.NewValue is IFilterable n)
                {

                }

            }));


        //声明和注册路由事件
        public static readonly RoutedEvent FilterChangedRoutedEvent =
            EventManager.RegisterRoutedEvent("FilterChanged", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(SelectionFilterBox));
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


        public ListBoxItem GetCheckAllItem()
        {
            return this.Dispatcher.Invoke(() =>
               {
                   if (this.Items.Count > 0 && this.UseCheckAll)
                       return this.ItemContainerGenerator.ContainerFromIndex(0) as ListBoxItem;
                   return null;
               });
        }
    }

    public class SelectionFilter : IDisplayFilter
    {
        private readonly SelectionFilterBox _selectionFilterBox;
        private PropertyInfo _propertyInfo;
        public SelectionFilter(SelectionFilterBox SelectionFilterBox)
        {
            _selectionFilterBox = SelectionFilterBox;
            _propertyInfo = this._selectionFilterBox.GetPropertyInfo();
        }

        public string DisplayName => _selectionFilterBox.DisplayName;

        public bool IsMatch(object obj)
        {
            if (obj == null)
                return false;
            if (_propertyInfo == null)
                return true;

            IList selectedItems = _selectionFilterBox.Dispatcher.Invoke(() => _selectionFilterBox.SelectedItems);
            if (selectedItems.Count == 0)
                return true;
            ListBoxItem checkItem = this._selectionFilterBox.GetCheckAllItem();
            if (checkItem != null)
            {
                if (checkItem.Dispatcher.Invoke(() => checkItem.IsSelected))
                    return true;
            }
            foreach (object item in selectedItems)
            {
                //object value = obj is IModelViewModel model ? _propertyInfo.GetValue(model.GetModel())
                //    : _propertyInfo.GetValue(obj);
                object model = obj;
                if (obj is IModelBindable mv)
                    model = mv.GetModel();
                if (!this._selectionFilterBox.Type.IsAssignableFrom(model.GetType()))
                    continue;
                object value = _propertyInfo.GetValue(model);
                if (value == null && item == null)
                    return true;
                if (value?.Equals(item) == true)
                    return true;
            }
            return false;

        }
    }
}
