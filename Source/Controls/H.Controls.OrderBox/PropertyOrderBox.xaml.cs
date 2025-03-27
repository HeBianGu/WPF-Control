global using H.Services.Message;
global using H.Services.Message.Dialog;
global using System.Windows;
global using System.Windows.Controls;
global using System.Windows.Controls.Primitives;

namespace H.Controls.OrderBox
{
    [TemplatePart(Name = "PART_Button")]
    [TemplatePart(Name = "PART_Selector")]
    public class PropertyOrderBox : Control
    {
        private PropertyOrdersPrensenter _propertyOrders;
        public PropertyOrdersPrensenter PropertyOrders => _propertyOrders;
        private Button _button = null;
        private Selector _selector = null;
        static PropertyOrderBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PropertyOrderBox), new FrameworkPropertyMetadata(typeof(PropertyOrderBox)));
        }

        public PropertyOrderBox()
        {
            this.ID = this.GetType().Name;
            this.Order = new PropertyOrderBoxOrder(this);

            this.DefinePropertyOrderPrensenters.CollectionChanged +=(l, k) =>
            {
                this.RefreshType(this.Type);
            };

        }

        public string ID { get; set; }
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


        public static readonly DependencyProperty DisplayNameProperty =
            DependencyProperty.Register("DisplayName", typeof(string), typeof(PropertyOrderBox), new FrameworkPropertyMetadata(default(string), (d, e) =>
            {
                PropertyOrderBox control = d as PropertyOrderBox;

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
            DependencyProperty.Register("Type", typeof(Type), typeof(PropertyOrderBox), new FrameworkPropertyMetadata(default(Type), (d, e) =>
            {
                PropertyOrderBox control = d as PropertyOrderBox;

                if (control == null) return;

                if (e.OldValue is Type o)
                {

                }

                if (e.NewValue is Type n)
                {
                    control.RefreshType(n);
                }
            }));

        public IOrderWhereable Order
        {
            get { return (IOrderWhereable)GetValue(OrderProperty); }
            private set { SetValue(OrderProperty, value); }
        }

        public static readonly DependencyProperty OrderProperty =
            DependencyProperty.Register("Order", typeof(IOrderWhereable), typeof(PropertyOrderBox), new FrameworkPropertyMetadata(default(IOrderWhereable), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) =>
            {
                PropertyOrderBox control = d as PropertyOrderBox;

                if (control == null) return;

                if (e.OldValue is IOrderWhereable o)
                {

                }

                if (e.NewValue is IOrderWhereable n)
                {

                }

            }));


        public static readonly RoutedEvent OrderChangedRoutedEvent =
            EventManager.RegisterRoutedEvent("OrderChanged", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(PropertyOrderBox));

        public event RoutedEventHandler OrderChanged
        {
            add { this.AddHandler(OrderChangedRoutedEvent, value); }
            remove { this.RemoveHandler(OrderChangedRoutedEvent, value); }
        }


        protected void OnOrderChanged()
        {
            RoutedEventArgs args = new RoutedEventArgs(OrderChangedRoutedEvent, this);
            this.RaiseEvent(args);
        }


        public string PropertyNames
        {
            get { return (string)GetValue(PropertyNamesProperty); }
            set { SetValue(PropertyNamesProperty, value); }
        }


        public static readonly DependencyProperty PropertyNamesProperty =
            DependencyProperty.Register("PropertyNames", typeof(string), typeof(PropertyOrderBox), new FrameworkPropertyMetadata(default(string), (d, e) =>
            {
                PropertyOrderBox control = d as PropertyOrderBox;

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
            EventManager.RegisterRoutedEvent("SelectedChanged", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(PropertyOrderBox));
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

        private void RefreshType(Type type)
        {
            if (type == null)
                return;
            Func<PropertyInfo, bool> predicate = x =>
            {
                return this.PropertyNames?.Split(',').Contains(x.Name) != false;
            };
            this._propertyOrders = new PropertyOrdersPrensenter(type, predicate)
            {
                ID = this.ID
            };
            this._propertyOrders.Load();

                    if (this._propertyOrders.PropertyOrders.Count > 0)
                return;
            //加载预定义的条件
            foreach (var item in this.DefinePropertyOrderPrensenters)
            {
                item.Properties = this._propertyOrders.Properties;
                foreach (PropertyOrder Order in item.Conditions)
                {
                    PropertyInfo propertyInfo = item.Properties.FirstOrDefault(x => x.Name == Order.PropertyName);
                    Order.PropertyInfo = propertyInfo;
                }
                _propertyOrders.PropertyOrders.Add(item);
            }
            this.OnOrderChanged();
        }

        public ObservableCollection<PropertyOrderPrensenter> DefinePropertyOrderPrensenters { get; } = new ObservableCollection<PropertyOrderPrensenter>();
public async void ShowConfig()
        {
            bool? r = await IocMessage.Dialog.Show(_propertyOrders, x =>
            {
                x.MinWidth = 900;
                x.DialogButton = DialogButton.Sumit;
                x.Title = "数据排序器";
            });
            if (r == true)
            {
                this.Save();
                IocMessage.ShowSnackInfo("保存成功");
            }
        }

        private void Save()
        {
            _propertyOrders.Save(out string message);
            this.Order = new PropertyOrderBoxOrder(this);
            this.OnOrderChanged();
        }
    }
}
