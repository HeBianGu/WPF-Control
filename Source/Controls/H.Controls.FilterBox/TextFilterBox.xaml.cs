using H.Providers.Ioc;
using H.Providers.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace H.Controls.FilterBox
{
    public class TextFilterBox : TextBox, IDisplayFilterBox
    {
        static TextFilterBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TextFilterBox), new FrameworkPropertyMetadata(typeof(TextFilterBox)));
        }

        public TextFilterBox()
        {
            this.Filter = new TextFilter(this);

        }
        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);
            this.Filter = new TextFilter(this);
            this.OnFilterChanged();
        }


        public string DisplayName
        {
            get { return (string)GetValue(DisplayNameProperty); }
            set { SetValue(DisplayNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DisplayNameProperty =
            DependencyProperty.Register("DisplayName", typeof(string), typeof(TextFilterBox), new FrameworkPropertyMetadata(default(string), (d, e) =>
            {
                TextFilterBox control = d as TextFilterBox;

                if (control == null) return;

                if (e.OldValue is string o)
                {

                }

                if (e.NewValue is string n)
                {

                }
            }));


        public bool UseSearchable
        {
            get { return (bool)GetValue(UseSearchableProperty); }
            set { SetValue(UseSearchableProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseSearchableProperty =
            DependencyProperty.Register("UseSearchable", typeof(bool), typeof(TextFilterBox), new FrameworkPropertyMetadata(default(bool), (d, e) =>
            {
                TextFilterBox control = d as TextFilterBox;

                if (control == null) return;

                if (e.OldValue is bool o)
                {

                }

                if (e.NewValue is bool n)
                {

                }

            }));

        public string PropertyNames
        {
            get { return (string)GetValue(PropertyNamesProperty); }
            set { SetValue(PropertyNamesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PropertyNamesProperty =
            DependencyProperty.Register("PropertyNames", typeof(string), typeof(TextFilterBox), new FrameworkPropertyMetadata(default(string), (d, e) =>
            {
                TextFilterBox control = d as TextFilterBox;

                if (control == null) return;

                if (e.OldValue is string o)
                {

                }

                if (e.NewValue is string n)
                {

                }
            }));


        public StringComparison StringComparison
        {
            get { return (StringComparison)GetValue(StringComparisonProperty); }
            set { SetValue(StringComparisonProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StringComparisonProperty =
            DependencyProperty.Register("StringComparison", typeof(StringComparison), typeof(TextFilterBox), new FrameworkPropertyMetadata(StringComparison.OrdinalIgnoreCase, (d, e) =>
            {
                TextFilterBox control = d as TextFilterBox;

                if (control == null) return;

                if (e.OldValue is StringComparison o)
                {

                }

                if (e.NewValue is StringComparison n)
                {

                }

            }));



        public IFilter Filter
        {
            get { return (IFilter)GetValue(FilterProperty); }
            private set { SetValue(FilterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FilterProperty =
            DependencyProperty.Register("Filter", typeof(IFilter), typeof(TextFilterBox), new FrameworkPropertyMetadata(default(IFilter), (d, e) =>
            {
                TextFilterBox control = d as TextFilterBox;

                if (control == null) return;

                if (e.OldValue is IFilter o)
                {

                }

                if (e.NewValue is IFilter n)
                {

                }

            }));


        public static readonly RoutedEvent FilterChangedRoutedEvent =
            EventManager.RegisterRoutedEvent("FilterChanged", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(TextFilterBox));

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
    }

    public class TextFilter : IDisplayFilter
    {
        private readonly TextFilterBox _textFilterBox;
        public TextFilter(TextFilterBox textFilterBox)
        {
            _textFilterBox = textFilterBox;
        }

        public string DisplayName => this._textFilterBox.DisplayName;

        public bool IsMatch(object obj)
        {
            string txt = this._textFilterBox.Text;
            if (string.IsNullOrEmpty(txt))
                return true;
            if (obj is ISearchable searchable && this._textFilterBox.UseSearchable)
                return searchable.Filter(txt);
            if (obj == null)
                return false;
            obj = obj is IModelViewModel model ? model.GetModel() : obj;
            string[] propertyNames = this._textFilterBox.PropertyNames?.Split(',');
            var ps = obj.GetType().GetProperties().Where(x => propertyNames?.Contains(x.Name) != false);
            foreach (var p in ps)
            {
                if (p.Name == "Item")
                    continue;
                var value = p.GetValue(obj);
                if (value != null)
                {
                    var r = value.ToString().Contains(txt, this._textFilterBox.StringComparison);
                    if (r)
                        return true;
                }
            }
            return false;

        }
    }
}
