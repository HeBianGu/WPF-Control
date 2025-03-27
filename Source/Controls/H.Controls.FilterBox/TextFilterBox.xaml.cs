
using H.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using H.Common.Interfaces;

namespace H.Controls.FilterBox
{
    public interface ITextFilterBox
    {
        string DisplayName { get; set; }
        IFilterable Filter { get; }
        string PropertyNames { get; set; }
        StringComparison StringComparison { get; set; }
        bool UseSearchable { get; set; }
        event RoutedEventHandler FilterChanged;
    }

    public class TextFilterBox : TextBox, ITextFilterBox
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



        public IFilterable Filter
        {
            get { return (IFilterable)GetValue(FilterProperty); }
            private set { SetValue(FilterProperty, value); }
        }


        public static readonly DependencyProperty FilterProperty =
            DependencyProperty.Register("Filter", typeof(IFilterable), typeof(TextFilterBox), new FrameworkPropertyMetadata(default(IFilterable), (d, e) =>
            {
                TextFilterBox control = d as TextFilterBox;

                if (control == null) return;

                if (e.OldValue is IFilterable o)
                {

                }

                if (e.NewValue is IFilterable n)
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
            obj = obj is IModelBindable model ? model.GetModel() : obj;
            string[] propertyNames = this._textFilterBox.PropertyNames?.Split(',');
            IEnumerable<PropertyInfo> ps = obj.GetType().GetProperties().Where(x => propertyNames?.Contains(x.Name) != false);
            foreach (PropertyInfo p in ps)
            {
                if (p.Name == "Item")
                    continue;
                object value = p.GetValue(obj);
                if (value != null)
                {
                    bool r = value.ToString().Contains(txt, this._textFilterBox.StringComparison);
                    if (r)
                        return true;
                }
            }
            return false;

        }
    }

}
