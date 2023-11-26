using H.Providers.Ioc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace H.Controls.FilterBox
{
    [TemplatePart(Name = "PART_Button")]
    [TemplatePart(Name = "PART_Selector")]
    public class PropertyFilterBox : Control, IDisplayFilterBox
    {
        private PropertyConfidtionsPrensenter _propertyConfidtions;
        public PropertyConfidtionsPrensenter PropertyConfidtions => _propertyConfidtions;
        private Button _button = null;
        private Selector _selector = null;
        static PropertyFilterBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PropertyFilterBox), new FrameworkPropertyMetadata(typeof(PropertyFilterBox)));
        }

        public PropertyFilterBox()
        {
            this.Filter = new PropertyFilterBoxFilter(this);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _button = this.Template.FindName("PART_Button", this) as Button;
            _button.Click += (l, k) =>
            {
                this.ShowConfig();
            };

            _selector = this.Template.FindName("PART_Selector", this) as Selector;
            _selector.SelectionChanged += (l, k) =>
            {
                if (l is Popup popup && popup.IsOpen == false)
                    return;
                this.OnSelectedChanged();
                this.Save();
            };
        }


        public string DisplayName
        {
            get { return (string)GetValue(DisplayNameProperty); }
            set { SetValue(DisplayNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DisplayNameProperty =
            DependencyProperty.Register("DisplayName", typeof(string), typeof(PropertyFilterBox), new FrameworkPropertyMetadata(default(string), (d, e) =>
            {
                PropertyFilterBox control = d as PropertyFilterBox;

                if (control == null) return;

                if (e.OldValue is string o)
                {

                }

                if (e.NewValue is string n)
                {

                }

            }));

        public Type Type
        {
            get { return (Type)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.Register("Type", typeof(Type), typeof(PropertyFilterBox), new FrameworkPropertyMetadata(default(Type), (d, e) =>
            {
                PropertyFilterBox control = d as PropertyFilterBox;

                if (control == null) return;

                if (e.OldValue is Type o)
                {

                }

                if (e.NewValue is Type n)
                {
                    control.RefreshType(n);
                }
            }));

        public IFilter Filter
        {
            get { return (IFilter)GetValue(FilterProperty); }
            private set { SetValue(FilterProperty, value); }
        }

        public static readonly DependencyProperty FilterProperty =
            DependencyProperty.Register("Filter", typeof(IFilter), typeof(PropertyFilterBox), new FrameworkPropertyMetadata(default(IFilter), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) =>
            {
                PropertyFilterBox control = d as PropertyFilterBox;

                if (control == null) return;

                if (e.OldValue is IFilter o)
                {

                }

                if (e.NewValue is IFilter n)
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
            DependencyProperty.Register("PropertyNames", typeof(string), typeof(PropertyFilterBox), new FrameworkPropertyMetadata(default(string), (d, e) =>
            {
                PropertyFilterBox control = d as PropertyFilterBox;

                if (control == null) return;

                if (e.OldValue is string o)
                {

                }

                if (e.NewValue is string n)
                {

                }
                control.RefreshType(control.Type);
            }));



        public static readonly RoutedEvent SelectedChangedRoutedEvent =
            EventManager.RegisterRoutedEvent("SelectedChanged", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(PropertyFilterBox));
        public event RoutedEventHandler SelectedChanged
        {
            add { this.AddHandler(SelectedChangedRoutedEvent, value); }
            remove { this.RemoveHandler(SelectedChangedRoutedEvent, value); }
        }
        protected void OnSelectedChanged()
        {
            RoutedEventArgs args = new RoutedEventArgs(SelectedChangedRoutedEvent, this);
            this.RaiseEvent(args);
        }

        void RefreshType(Type type)
        {
            if (type == null)
                return;
            Func<PropertyInfo, bool> predicate = x =>
            {
                return this.PropertyNames?.Split(',').Contains(x.Name) != false;
            };
            this._propertyConfidtions = new PropertyConfidtionsPrensenter(type, predicate)
            {
                ID = type.Name
            };
            this._propertyConfidtions.Load();
        }


        public async void ShowConfig()
        {
            var r = await IocMessage.Dialog.Show(_propertyConfidtions, x =>
            {
                if (x is Control control)
                {
                    control.MinWidth = 900;
                }
            }, DialogButton.Sumit, "数据过滤器");
            if (r == true)
            {
                this.Save();
                IocMessage.Snack?.ShowTime("保存成功");
            }
        }

        void Save()
        {
            _propertyConfidtions.Save();
            this.Filter = new PropertyFilterBoxFilter(this);
        }
    }


    public class PropertyFilterBoxFilter : IDisplayFilter
    {
        PropertyFilterBox _propertyFilter;
        public PropertyFilterBoxFilter(PropertyFilterBox propertyFilter)
        {
            _propertyFilter = propertyFilter;
        }

        public string DisplayName => _propertyFilter.DisplayName;

        public bool IsMatch(object obj)
        {
            if (_propertyFilter.PropertyConfidtions == null)
                return true;
            return _propertyFilter.PropertyConfidtions.IsMatch(obj);
        }
    }
}
