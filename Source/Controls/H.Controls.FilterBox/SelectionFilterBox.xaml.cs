using H.Providers.Ioc;
using H.Providers.Mvvm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace H.Controls.FilterBox
{
    public class SelectionFilterBox : ListBox
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
            this.OnFilterChagned();

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
                control.RefreshData();
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
                control.RefreshData();
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

                if (control == null) return;

                if (e.OldValue is INotifyCollectionChanged o)
                    o.CollectionChanged -= control.CollectionChanged;
                if (e.NewValue is INotifyCollectionChanged n)
                    n.CollectionChanged += control.CollectionChanged;
                control.RefreshData();
            }));

        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.RefreshData();
        }

        private void RefreshData()
        {
            PropertyInfo propertyInfo = this.GetPropertyInfo();
            if (propertyInfo == null)
                return;
            if (this.Datas == null)
            {
                this.Items.Clear();
                return;
            }
            this.ItemsSource = this.GetItemsSource(propertyInfo);
        }

        public PropertyInfo GetPropertyInfo()
        {
            if (this.Type == null)
                return null;
            if (string.IsNullOrWhiteSpace(this.PropertyName))
                return null;
            PropertyInfo propertyInfo = this.Type.GetProperty(this.PropertyName);
            return propertyInfo;
        }

        private IEnumerable GetItemsSource(PropertyInfo propertyInfo)
        {
            List<object> items = new List<object>();
            if (this.UseCheckAll)
                yield return "全选";
            foreach (object data in this.Datas)
            {
                if (data == null)
                    continue;
                object value = data is IModelViewModel model ? propertyInfo.GetValue(model.GetModel()) : propertyInfo.GetValue(data);

                if (items.Contains(value))
                    continue;
                items.Add(value);
                yield return value;
            }
        }

        public IFilter Filter
        {
            get { return (SelectionFilter)GetValue(FilterProperty); }
            set { SetValue(FilterProperty, value); }
        }

        
        public static readonly DependencyProperty FilterProperty =
            DependencyProperty.Register("Filter", typeof(IFilter), typeof(SelectionFilterBox), new FrameworkPropertyMetadata(default(IFilter), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) =>
            {
                SelectionFilterBox control = d as SelectionFilterBox;

                if (control == null) return;

                if (e.OldValue is IFilter o)
                {

                }

                if (e.NewValue is IFilter n)
                {

                }

            }));


        //声明和注册路由事件
        public static readonly RoutedEvent FilterChagnedRoutedEvent =
            EventManager.RegisterRoutedEvent("FilterChagned", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(SelectionFilterBox));
        //CLR事件包装
        public event RoutedEventHandler FilterChagned
        {
            add { this.AddHandler(FilterChagnedRoutedEvent, value); }
            remove { this.RemoveHandler(FilterChagnedRoutedEvent, value); }
        }

        //激发路由事件,借用Click事件的激发方法

        protected void OnFilterChagned()
        {
            RoutedEventArgs args = new RoutedEventArgs(FilterChagnedRoutedEvent, this);
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
                object value = obj is IModelViewModel model ? _propertyInfo.GetValue(model.GetModel())
                    : _propertyInfo.GetValue(obj);
                if (value == null && item == null)
                    return true;
                if (value?.Equals(item) == true)
                    return true;
            }
            return false;

        }
    }
}
