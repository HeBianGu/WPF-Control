global using H.Services.Message;
global using H.Services.Message.Dialog;
global using System.Windows.Controls.Primitives;

namespace H.Controls.FilterBox
{
    [TemplatePart(Name = "PART_Button")]
    [TemplatePart(Name = "PART_Selector")]
    public class PropertyFilterBox : Control
    {
        private PropertyConditionsPrensenter _propertyConfidtions;
        public PropertyConditionsPrensenter PropertyConfidtions => _propertyConfidtions;
        private Button _button = null;
        private Selector _selector = null;
        static PropertyFilterBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PropertyFilterBox), new FrameworkPropertyMetadata(typeof(PropertyFilterBox)));
        }

        public PropertyFilterBox()
        {
            this.Filter = new PropertyFilterBoxFilter(this);
            this.ID = GetType().Name;

            this.DefinePropertyConditionPrensenters.CollectionChanged += (l, k) =>
            {
                this.RefreshType(this.Type);
            };
        }


        public DataTemplate SelectedDataTemplate
        {
            get { return (DataTemplate)GetValue(SelectedDataTemplateProperty); }
            set { SetValue(SelectedDataTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedDataTemplateProperty =
            DependencyProperty.Register("SelectedDataTemplate", typeof(DataTemplate), typeof(PropertyFilterBox), new FrameworkPropertyMetadata(default(DataTemplate), (d, e) =>
            {
                PropertyFilterBox control = d as PropertyFilterBox;

                if (control == null) return;

                if (e.OldValue is DataTemplate o)
                {

                }

                if (e.NewValue is DataTemplate n)
                {

                }

            }));


        public string ID { get; set; }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _button = this.Template.FindName("PART_Button", this) as Button;
            _button.Click += (l, k) =>
            {
                ShowConfig();
            };

            _selector = this.Template.FindName("PART_Selector", this) as Selector;
            _selector.SelectionChanged += (l, k) =>
            {
                if (l is Popup popup && popup.IsOpen == false)
                    return;
                OnSelectedChanged();
                Save();
            };
        }


        public string DisplayName
        {
            get { return (string)GetValue(DisplayNameProperty); }
            set { SetValue(DisplayNameProperty, value); }
        }


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

        public IFilterable Filter
        {
            get { return (IFilterable)GetValue(FilterProperty); }
            private set { SetValue(FilterProperty, value); }
        }

        public static readonly DependencyProperty FilterProperty =
            DependencyProperty.Register("Filter", typeof(IFilterable), typeof(PropertyFilterBox), new FrameworkPropertyMetadata(default(IFilterable), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) =>
            {
                PropertyFilterBox control = d as PropertyFilterBox;

                if (control == null) return;

                if (e.OldValue is IFilterable o)
                {

                }

                if (e.NewValue is IFilterable n)
                {

                }

            }));


        public string PropertyNames
        {
            get { return (string)GetValue(PropertyNamesProperty); }
            set { SetValue(PropertyNamesProperty, value); }
        }


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
            add { AddHandler(SelectedChangedRoutedEvent, value); }
            remove { RemoveHandler(SelectedChangedRoutedEvent, value); }
        }
        protected void OnSelectedChanged()
        {
            RoutedEventArgs args = new RoutedEventArgs(SelectedChangedRoutedEvent, this);
            RaiseEvent(args);
        }

        private void RefreshType(Type type)
        {
            if (type == null)
                return;
            Func<PropertyInfo, bool> predicate = x =>
            {
                return this.PropertyNames?.Split(',').Contains(x.Name) != false;
            };
            _propertyConfidtions = new PropertyConditionsPrensenter(type, predicate)
            {
                ID = this.ID
            };
            _propertyConfidtions.Load();
            if (this._propertyConfidtions.PropertyConfidtions.Count > 0)
                return;
            //加载预定义的条件
            foreach (PropertyConditionPrensenter item in this.DefinePropertyConditionPrensenters)
            {
                item.Properties = _propertyConfidtions.Properties;
                foreach (IPropertyConfidtion confidtion in item.Conditions)
                {
                    PropertyInfo propertyInfo = item.Properties.FirstOrDefault(x => x.Name == confidtion.Filter.PropertyName);
                    confidtion.Filter.PropertyInfo = propertyInfo;
                }
                _propertyConfidtions.PropertyConfidtions.Add(item);
            }
            this.OnFilterChanged();
        }

        public ObservableCollection<PropertyConditionPrensenter> DefinePropertyConditionPrensenters { get; } = new ObservableCollection<PropertyConditionPrensenter>();

        public async void ShowConfig()
        {
            bool? r = await IocMessage.Dialog.Show(_propertyConfidtions, x =>
            {
                x.DialogButton = DialogButton.Sumit;
                x.Title = "数据过滤器";
                x.MinWidth = 900;
            });
            if (r == true)
            {
                Save();
                IocMessage.ShowSnackInfo("保存成功");
            }
        }

        private void Save()
        {
            _propertyConfidtions.Save(out string message);
            this.Filter = new PropertyFilterBoxFilter(this);
            this.OnFilterChanged();
        }

        public static readonly RoutedEvent FilterChangedRoutedEvent =
    EventManager.RegisterRoutedEvent("FilterChanged", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(PropertyFilterBox));

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


    public class PropertyFilterBoxFilter : IDisplayFilter
    {
        private PropertyFilterBox _propertyFilter;
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
