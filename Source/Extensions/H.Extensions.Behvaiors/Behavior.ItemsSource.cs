// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using Microsoft.Xaml.Behaviors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace H.Extensions.Behvaiors
{
    public abstract class ButtonBehaviorBase : Behavior<Button>
    {
        protected override void OnAttached()
        {
            AssociatedObject.Click += AssociatedObject_Click;
        }

        private void AssociatedObject_Click(object sender, RoutedEventArgs e)
        {
            this.OnClick();
        }

        protected override void OnDetaching()
        {
            AssociatedObject.Click -= AssociatedObject_Click;
        }

        protected abstract void OnClick();

        protected ItemsControl ItemsControl => this.AssociatedObject.GetParent<ItemsControl>();
        protected IList ItemsSource => ItemsControl.ItemsSource as IList;
        protected object Item => this.AssociatedObject.DataContext;
    }

    public abstract class AddItemButtonBehaviorBase : ButtonBehaviorBase
    {
        public object DefaultValue
        {
            get { return GetValue(DefaultValueProperty); }
            set { SetValue(DefaultValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DefaultValueProperty =
            DependencyProperty.Register("DefaultValue", typeof(object), typeof(AddItemButtonBehaviorBase), new FrameworkPropertyMetadata(default(object), (d, e) =>
            {
                AddItemButtonBehaviorBase control = d as AddItemButtonBehaviorBase;

                if (control == null) return;

                if (e.OldValue is object o)
                {

                }

                if (e.NewValue is object n)
                {

                }

            }));

        protected object CreateNewItem()
        {
            if (this.DefaultValue == null && this.ItemsSource == null)
                return null;
            if (this.ItemsSource == null)
                this.ItemsControl.ItemsSource = this.DefaultValue.GetType().CreateObservableCollection<IList>();
            if (this.DefaultValue is ICloneable cloneable)
                return cloneable.Clone();
            if (this.ItemsSource.TryCreateItem(out object instance))
                return instance;
            return null;
        }
    }

    public class ButtonAddItemBehavior : AddItemButtonBehaviorBase
    {
        protected override void OnClick()
        {
            object addItem = this.CreateNewItem();
            if (addItem == null) 
                return;
            this.ItemsSource.Add(addItem);
        }
    }

    public class ButtonInsertnBehavior : AddItemButtonBehaviorBase
    {
        public int Index
        {
            get { return (int)GetValue(IndexProperty); }
            set { SetValue(IndexProperty, value); }
        }

        public static readonly DependencyProperty IndexProperty =
            DependencyProperty.Register("Index", typeof(int), typeof(ButtonInsertnBehavior), new FrameworkPropertyMetadata(0, (d, e) =>
             {
                 ButtonInsertnBehavior control = d as ButtonInsertnBehavior;

                 if (control == null) return;

                 if (e.OldValue is int o)
                 {

                 }

                 if (e.NewValue is int n)
                 {

                 }

             }));

        protected override void OnClick()
        {
            object addItem = this.CreateNewItem();
            if (addItem == null) 
                return;
            this.ItemsSource.Insert(this.Index, addItem);
        }
    }

    public class ButtonRemoveItemBehavior : ButtonBehaviorBase
    {
        protected override void OnClick()
        {
            if (this.ItemsSource == null)
                return;
            if (this.Item == null)
                return;
            this.ItemsSource.Remove(this.Item);
        }
    }


    public class ButtonClearItemBehavior : ButtonBehaviorBase
    {
        protected override void OnClick()
        {
            if (this.ItemsSource == null)
                return;
            this.ItemsSource.Clear();
        }
    }

    public class ButtonRemoveCheckedItemBehavior : ButtonBehaviorBase
    {
        protected ListBox ListBox => this.ItemsControl as ListBox;
        protected override void OnClick()
        {
            if (this.ListBox == null) 
                return;
            List<object> objs = new List<object>();
            foreach (object item in this.ListBox.Items)
            {
                DependencyObject find = this.ListBox.ItemContainerGenerator.ContainerFromItem(item);
                if (find is ListBoxItem listBoxItem)
                {
                    if (!listBoxItem.IsSelected) 
                        continue;
                    objs.Add(item);
                }
            }
            if (this.ItemsSource is IList list)
            {
                foreach (object item in objs)
                {
                    list.Remove(item);
                }
            }
        }
    }

}