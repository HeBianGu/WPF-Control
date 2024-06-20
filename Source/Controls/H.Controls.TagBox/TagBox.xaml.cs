using H.Extensions.ValueConverter;
using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace H.Controls.TagBox
{
    public class TagBox : ListBox
    {
        static TagBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TagBox), new FrameworkPropertyMetadata(typeof(TagBox)));
        }

        public TagBox()
        {
            IocTagService.Instance.CollectionChanged += (l, k) =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    this.RefreshData();
                });

            };
        }


        public string SearchText
        {
            get { return (string)GetValue(SearchTextProperty); }
            set { SetValue(SearchTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SearchTextProperty =
            DependencyProperty.Register("SearchText", typeof(string), typeof(TagBox), new FrameworkPropertyMetadata(default(string), (d, e) =>
            {
                TagBox control = d as TagBox;

                if (control == null) return;

                if (e.OldValue is string o)
                {

                }

                if (e.NewValue is string n)
                {

                }
            }));


        private void RefreshData()
        {
            var service = Ioc.GetService<ITagService>();
            this.ItemsSource = service?.Collection.Where(x => x.GroupName == this.GroupName).OrderByDescending(x => x.Order);
        }

        public string GroupName
        {
            get { return (string)GetValue(GroupNameProperty); }
            set { SetValue(GroupNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GroupNameProperty =
            DependencyProperty.Register("GroupName", typeof(string), typeof(TagBox), new FrameworkPropertyMetadata(default(string), (d, e) =>
            {
                TagBox control = d as TagBox;

                if (control == null) return;

                if (e.OldValue is string o)
                {

                }

                if (e.NewValue is string n)
                {

                }
                //control.RefreshData();
            }));


        private bool _flag = false;
        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);
            if (this._flag)
                return;

            if (e.AddedItems != null)
            {
                foreach (var item in e.AddedItems.OfType<ITag>())
                {
                    item.Order++;
                }
            }

            var tags = this.SelectedItems.OfType<ITag>();
            this._flag = true;
            this.Tags = string.Join(",", tags.Select(x => x.Name));
            this.OnTagChanged();
            this._flag = false;
        }

        public string Tags
        {
            get { return (string)GetValue(TagsProperty); }
            set { SetValue(TagsProperty, value); }
        }

        public static readonly DependencyProperty TagsProperty =
            DependencyProperty.Register("Tags", typeof(string), typeof(TagBox), new FrameworkPropertyMetadata(string.Empty, // default value
                                       FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | // Flags
                                           FrameworkPropertyMetadataOptions.Journal, (d, e) =>
            {
                TagBox control = d as TagBox;
                if (control == null)
                    return;
                if (control._flag)
                    return;
                if (control.SelectionMode == SelectionMode.Single)
                    return;
                control._flag = true;
                control.SelectedItems.Clear();
                if (e.OldValue is string o)
                {

                }

                if (e.NewValue is string n)
                {
                    control.RefreshSelection();
                }
                control._flag = false;
            }));


        private void RefreshSelection()
        {
            if (this.Tags == null)
            {
                this.SelectedItems.Clear();
                return;
            }
            var values = this.Tags.Split(TagOptions.Instance.SplitChars.ToCharArray());
            foreach (var value in values)
            {
                var find = this.ItemsSource.OfType<ITag>().FirstOrDefault(x => x.Name == value);
                if (find == null)
                    continue;
                this.SelectedItems.Add(find);
            }
        }


        //声明和注册路由事件
        public static readonly RoutedEvent TagChangedRoutedEvent =
            EventManager.RegisterRoutedEvent("TagChanged", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(TagBox));
        //CLR事件包装
        public event RoutedEventHandler TagChanged
        {
            add { this.AddHandler(TagChangedRoutedEvent, value); }
            remove { this.RemoveHandler(TagChangedRoutedEvent, value); }
        }

        //激发路由事件,借用Click事件的激发方法

        protected void OnTagChanged()
        {
            RoutedEventArgs args = new RoutedEventArgs(TagChangedRoutedEvent, this);
            this.RaiseEvent(args);
        }
    }

    public class TagBoxSearchConverter : MarkupMultiValueConverterBase
    {
        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length != 2)
                return DependencyProperty.UnsetValue;
            if (values[0] is ITag tag && values[1] is string txt)
            {
                if (string.IsNullOrEmpty(txt) || tag.Name.Contains(txt))
                    return Visibility.Visible;
                return Visibility.Collapsed;
            }
            return DependencyProperty.UnsetValue;
        }
    }
}
