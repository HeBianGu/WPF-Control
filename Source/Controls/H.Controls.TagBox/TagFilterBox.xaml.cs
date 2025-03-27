global using H.Services.Common;
global using H.Mvvm;
global using System;
global using System.Collections;
global using System.Linq;
global using System.Windows;
global using System.Windows.Controls;
global using System.Windows.Threading;
global using H.Common.Interfaces.Where;
global using H.Mvvm.ViewModels;

namespace H.Controls.TagBox
{
    public class TagFilterBox : ListBox
    {
        static TagFilterBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TagFilterBox), new FrameworkPropertyMetadata(typeof(TagFilterBox)));
        }

        public TagFilterBox()
        {
            this.Filter = new TagFilter(this);
            this.RefreshData();
            IocTagService.Instance.CollectionChanged += (l, k) =>
            {
                this.RefreshData();
            };
        }

        public string GroupName
        {
            get { return (string)GetValue(GroupNameProperty); }
            set { SetValue(GroupNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GroupNameProperty =
            DependencyProperty.Register("GroupName", typeof(string), typeof(TagFilterBox), new FrameworkPropertyMetadata(default(string), (d, e) =>
            {
                TagFilterBox control = d as TagFilterBox;

                if (control == null) return;

                if (e.OldValue is string o)
                {

                }

                if (e.NewValue is string n)
                {

                }
                control.RefreshData();
            }));


        public void RefreshData()
        {
            this.DelayInvoke(() => this.ItemsSource = this.GetItemsSource());
        }

        private IEnumerable GetItemsSource()
        {
            if (this.UseCheckAll)
                yield return "全选";
            foreach (object value in IocTagService.Instance.Collection.Where(x => x.GroupName == this.GroupName).OrderByDescending(x => x.Order))
            {
                yield return value;
            }
        }

        private bool _flag = false;
        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);
            if (_flag == true)
                return;
            ListBoxItem checkItem = this.GetCheckAllItem();
            if (checkItem == null)
                return;

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
            this.Filter = new TagFilter(this);
            this.OnFilterChanged();
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

        public bool UseCheckAll
        {
            get { return (bool)GetValue(UseCheckAllProperty); }
            set { SetValue(UseCheckAllProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseCheckAllProperty =
            DependencyProperty.Register("UseCheckAll", typeof(bool), typeof(TagFilterBox), new FrameworkPropertyMetadata(true, (d, e) =>
            {
                TagFilterBox control = d as TagFilterBox;

                if (control == null) return;

                if (e.OldValue is bool o)
                {

                }

                if (e.NewValue is bool n)
                {

                }
                control.RefreshData();
            }));

        public IFilterable Filter
        {
            get { return (IFilterable)GetValue(FilterProperty); }
            set { SetValue(FilterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FilterProperty =
            DependencyProperty.Register("Filter", typeof(IFilterable), typeof(TagFilterBox), new FrameworkPropertyMetadata(default(IFilterable), (d, e) =>
            {
                TagFilterBox control = d as TagFilterBox;

                if (control == null) return;

                if (e.OldValue is IFilterable o)
                {

                }

                if (e.NewValue is IFilterable n)
                {

                }

            }));


        public static readonly RoutedEvent FilterChangedRoutedEvent =
            EventManager.RegisterRoutedEvent("FilterChanged", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(TagFilterBox));

        public event RoutedEventHandler FilterChanged
        {
            add { this.AddHandler(FilterChangedRoutedEvent, value); }
            remove { this.RemoveHandler(FilterChangedRoutedEvent, value); }
        }
        protected void OnFilterChanged()
        {
            RoutedEventArgs args = new RoutedEventArgs(FilterChangedRoutedEvent, this);
            this.RaiseEvent(args);
        }

        public string PropertyName
        {
            get { return (string)GetValue(PropertyNameProperty); }
            set { SetValue(PropertyNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PropertyNameProperty =
            DependencyProperty.Register("PropertyName", typeof(string), typeof(TagFilterBox), new FrameworkPropertyMetadata(default(string), (d, e) =>
            {
                TagFilterBox control = d as TagFilterBox;

                if (control == null) return;

                if (e.OldValue is string o)
                {

                }

                if (e.NewValue is string n)
                {

                }

            }));

    }

    public class TagFilter : IFilterable
    {
        TagFilterBox _tagBox;
        public TagFilter(TagFilterBox tagBox)
        {
            _tagBox = tagBox;
        }
        public bool IsMatch(object obj)
        {
            if (obj == null)
                return false;
            if (_tagBox.PropertyName == null)
                return true;

            var selectedTags = this._tagBox.SelectedItems.OfType<ITag>();
            if (selectedTags.Count() == 0)
                return true;
            object model = obj;
            if (obj is IModelBindable viewModel)
            {
                model = viewModel.GetModel();
            }
            var property = model.GetType().GetProperty(this._tagBox.PropertyName);
            if (property == null)
                return false;
            var value = property.GetValue(model);
            if (value == null)
                return false;

            var vs = value.ToString().Split(TagOptions.Instance.SplitChars.ToCharArray());
            foreach (var v in vs)
            {
                if (selectedTags.Any(x => x.Name == v))
                    return true;
            }
            return false;
        }
    }
}
