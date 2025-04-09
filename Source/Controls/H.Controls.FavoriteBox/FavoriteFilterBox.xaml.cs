using H.Extensions.Tree;
using H.Mvvm;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using H.Common.Interfaces.Where;
using H.Mvvm.ViewModels;
using H.Mvvm.ViewModels.Tree;
using H.Mvvm.Commands;
using H.Extensions.Common;

namespace H.Controls.FavoriteBox
{
    public class FavoriteFilterBox : TreeView
    {
        static FavoriteFilterBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FavoriteFilterBox), new FrameworkPropertyMetadata(typeof(FavoriteFilterBox)));
        }

        public FavoriteFilterBox()
        {
            this.Filter = new FavoriteFilter(this);
            this.RefreshData();
            IocFavoriteService.Instance.CollectionChanged += (l, k) =>
            {
                this.RefreshData();
            };

            CommandBinding bingding = new CommandBinding(Commands.Clear);
            bingding.Executed += (s, e) =>
            {
                this.SelectNone();
            };
            bingding.CanExecute += (l, k) =>
            {
                k.CanExecute = this.SelectedItem != null;
            };
            this.CommandBindings.Add(bingding);
        }

        public string GroupName
        {
            get { return (string)GetValue(GroupNameProperty); }
            set { SetValue(GroupNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GroupNameProperty =
            DependencyProperty.Register("GroupName", typeof(string), typeof(FavoriteFilterBox), new FrameworkPropertyMetadata(default(string), (d, e) =>
            {
                FavoriteFilterBox control = d as FavoriteFilterBox;

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
            this.DelayInvoke(() => this.ItemsSource = new FavoriteTree().GetTreeNodes());
        }
        protected override void OnSelectedItemChanged(RoutedPropertyChangedEventArgs<object> e)
        {
            base.OnSelectedItemChanged(e);

            this.Filter = new FavoriteFilter(this);
            this.OnFilterChanged();
        }

        public IFilterable Filter
        {
            get { return (IFilterable)GetValue(FilterProperty); }
            set { SetValue(FilterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FilterProperty =
            DependencyProperty.Register("Filter", typeof(IFilterable), typeof(FavoriteFilterBox), new FrameworkPropertyMetadata(default(IFilterable), (d, e) =>
            {
                FavoriteFilterBox control = d as FavoriteFilterBox;

                if (control == null) return;

                if (e.OldValue is IFilterable o)
                {

                }

                if (e.NewValue is IFilterable n)
                {

                }

            }));


        public static readonly RoutedEvent FilterChangedRoutedEvent =
            EventManager.RegisterRoutedEvent("FilterChanged", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(FavoriteFilterBox));

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
            DependencyProperty.Register("PropertyName", typeof(string), typeof(FavoriteFilterBox), new FrameworkPropertyMetadata(default(string), (d, e) =>
            {
                FavoriteFilterBox control = d as FavoriteFilterBox;

                if (control == null) return;

                if (e.OldValue is string o)
                {

                }

                if (e.NewValue is string n)
                {

                }

            }));

    }

    public class FavoriteFilter : IFilterable
    {
        FavoriteFilterBox _favoriteBox;
        public FavoriteFilter(FavoriteFilterBox favoriteBox)
        {
            _favoriteBox = favoriteBox;
        }
        public bool IsMatch(object obj)
        {
            if (obj == null)
                return false;
            if (_favoriteBox.PropertyName == null)
                return true;

            if (this._favoriteBox.SelectedItem == null)
                return true;
            object model = obj;
            if (obj is IModelBindable viewModel)
            {
                model = viewModel.GetModel();
            }
            var property = model.GetType().GetProperty(this._favoriteBox.PropertyName);
            if (property == null)
                return false;
            var value = property.GetValue(model);
            if (value == null)
                return false;
            if (this._favoriteBox.SelectedItem is TreeNodeBase<object> node && node.Model is IFavoriteItem favorite)
            {
                return value.Equals(favorite.Path);
            }
            return true;
        }
    }
}
