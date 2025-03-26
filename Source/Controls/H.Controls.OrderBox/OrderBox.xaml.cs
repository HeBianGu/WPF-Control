global using H.Common.Interfaces.Where;
using H.Services.Common;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace H.Controls.OrderBox
{
    public class OrderBox : ListBox
    {
        static OrderBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(OrderBox), new FrameworkPropertyMetadata(typeof(OrderBox)));
        }

        public OrderBox()
        {
            this.Orders.CollectionChanged += (l, k) =>
            {
                this.RefreshData();
            };
        }
        public void RefreshData()
        {
            this.ItemsSource = this.GetItemsSource();
        }

        private IEnumerable GetItemsSource()
        {
            if (this.UseCheckAll)
                yield return "全选";
            foreach (IOrderWhereable data in this.Orders)
            {
                yield return data;
            }
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

        public ObservableCollection<IOrderWhereable> Orders { get; } = new ObservableCollection<IOrderWhereable>();

        public bool UseCheckAll
        {
            get { return (bool)GetValue(UseCheckAllProperty); }
            set { SetValue(UseCheckAllProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseCheckAllProperty =
            DependencyProperty.Register("UseCheckAll", typeof(bool), typeof(OrderBox), new FrameworkPropertyMetadata(true, (d, e) =>
            {
                OrderBox control = d as OrderBox;

                if (control == null) return;

                if (e.OldValue is bool o)
                {

                }

                if (e.NewValue is bool n)
                {

                }
                control.RefreshData();
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
            this.Order = new OrderBoxOrder(this);
            this.OnOrderChanged();
        }


        public IOrderWhereable Order
        {
            get { return (IOrderWhereable)GetValue(OrderProperty); }
            private set { SetValue(OrderProperty, value); }
        }

        public static readonly DependencyProperty OrderProperty =
            DependencyProperty.Register("Order", typeof(IOrderWhereable), typeof(OrderBox), new FrameworkPropertyMetadata(default(IOrderWhereable), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) =>
            {
                OrderBox control = d as OrderBox;

                if (control == null) return;

                if (e.OldValue is IOrderWhereable o)
                {

                }

                if (e.NewValue is IOrderWhereable n)
                {

                }

            }));


        public static readonly RoutedEvent OrderChangedRoutedEvent =
            EventManager.RegisterRoutedEvent("OrderChanged", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(OrderBox));

        public event RoutedEventHandler OrderChanged
        {
            add { this.AddHandler(OrderChangedRoutedEvent, value); }
            remove { this.RemoveHandler(OrderChangedRoutedEvent, value); }
        }


        public void OnOrderChanged()
        {
            RoutedEventArgs args = new RoutedEventArgs(OrderChangedRoutedEvent, this);
            this.RaiseEvent(args);
        }
    }

    public class OrderBoxOrder : IOrderWhereable
    {
        private OrderBox _orderBox;
        public OrderBoxOrder(OrderBox orderBox)
        {
            _orderBox = orderBox;
        }

        public IEnumerable Where(IEnumerable from)
        {

            if (this._orderBox.SelectionMode == SelectionMode.Single)
            {
                if (this._orderBox.SelectedItem is IOrderWhereable order)
                {
                    return order.Where(from);
                }
                else
                {
                    return from;
                }
            }
            else
            {
                var orders = this._orderBox.SelectedItems.OfType<IOrderWhereable>();
                if (orders == null || orders.Count() == 0)
                    return from;
                IEnumerable result = from;
                foreach (var item in orders)
                {
                    result = item.Where(result);
                }
                return result;
            }

        }
    }

}
