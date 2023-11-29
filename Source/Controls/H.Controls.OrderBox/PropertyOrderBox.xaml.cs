using H.Providers.Ioc;
using System;
using System.Collections;
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

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
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

        public IOrder Order
        {
            get { return (IOrder)GetValue(OrderProperty); }
            private set { SetValue(OrderProperty, value); }
        }

        public static readonly DependencyProperty OrderProperty =
            DependencyProperty.Register("Order", typeof(IOrder), typeof(PropertyOrderBox), new FrameworkPropertyMetadata(default(IOrder), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) =>
            {
                PropertyOrderBox control = d as PropertyOrderBox;

                if (control == null) return;

                if (e.OldValue is IOrder o)
                {

                }

                if (e.NewValue is IOrder n)
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

        void RefreshType(Type type)
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
        }


        public async void ShowConfig()
        {
            var r = await IocMessage.Dialog.Show(_propertyOrders, x =>
            {
                if (x is Control control)
                {
                    control.MinWidth = 900;
                }
            }, DialogButton.Sumit, "数据排序器");
            if (r == true)
            {
                this.Save();
                IocMessage.Snack?.ShowInfo("保存成功");
            }
        }

        void Save()
        {
            _propertyOrders.Save();
            this.Order = new PropertyOrderBoxOrder(this);
        }
    }
}
