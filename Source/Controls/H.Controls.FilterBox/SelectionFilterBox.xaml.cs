using H.Providers.Ioc;
using H.Providers.Mvvm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace H.Controls.FilterBox
{
    public class SelectionFilterBox : ListBox, IFilterBox
    {
        static SelectionFilterBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SelectionFilterBox), new FrameworkPropertyMetadata(typeof(SelectionFilterBox)));
        }

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);
            this.Filter = new SelectionFilter(this);
        }

        public Type Type
        {
            get { return (Type)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
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


        public string PropertyName
        {
            get { return (string)GetValue(PropertyNameProperty); }
            set { SetValue(PropertyNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
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

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DatasProperty =
            DependencyProperty.Register("Datas", typeof(IEnumerable), typeof(SelectionFilterBox), new FrameworkPropertyMetadata(default(IEnumerable), (d, e) =>
            {
                SelectionFilterBox control = d as SelectionFilterBox;

                if (control == null) return;

                if (e.OldValue is IEnumerable o)
                {

                }

                if (e.NewValue is IEnumerable n)
                {

                }
                control.RefreshData();
            }));

        private void RefreshData()
        {
            var propertyInfo = this.GetPropertyInfo();
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
            var propertyInfo = this.Type.GetProperty(this.PropertyName);
            return propertyInfo;
        }

        private IEnumerable GetItemsSource(PropertyInfo propertyInfo)
        {
            List<object> items = new List<object>();
            foreach (var data in this.Datas)
            {
                if (data == null)
                    continue;
                var value= propertyInfo.GetValue(data);
                if(items.Contains(value))
                    continue;
                items.Add(value);
                yield return value;
            }
        }

        public IFilter Filter
        {
            get { return (IFilter)GetValue(FilterProperty); }
            private set { SetValue(FilterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FilterProperty =
            DependencyProperty.Register("Filter", typeof(IFilter), typeof(SelectionFilterBox), new FrameworkPropertyMetadata(default(IFilter), (d, e) =>
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
    }

    public class SelectionFilter : IFilter
    {
        SelectionFilterBox _selectionFilterBox;
        PropertyInfo _propertyInfo;
        public SelectionFilter(SelectionFilterBox SelectionFilterBox)
        {
            _selectionFilterBox = SelectionFilterBox;
            _propertyInfo = this._selectionFilterBox.GetPropertyInfo();
        }

        public bool IsMatch(object obj)
        {
            if (obj == null)
                return false;
            if (_selectionFilterBox.SelectedItems.Count == 0)
                return true;
            foreach (var item in _selectionFilterBox.SelectedItems)
            {
                if (_propertyInfo.GetValue(obj).Equals(item))
                    return true;
            }
            return false;

        }
    }
}
