using System.Collections;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Xaml.Behaviors;

namespace H.Extensions.Behvaiors
{
    public class ListBoxBindingSelectedItemsBehavior : Behavior<ListBox>
    {

        public IList SelectedItems
        {
            get { return (IList)GetValue(SelectedItemsProperty); }
            set { SetValue(SelectedItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemsProperty =
            DependencyProperty.Register("SelectedItems", typeof(IList), typeof(ListBoxBindingSelectedItemsBehavior), new FrameworkPropertyMetadata(default(IList), (d, e) =>
            {
                ListBoxBindingSelectedItemsBehavior control = d as ListBoxBindingSelectedItemsBehavior;

                if (control == null)
                    return;
                if (control.AssociatedObject == null)
                    return;
                control.RefreshData();
            }));

        void RefreshData()
        {
            if (this.AssociatedObject.SelectionMode == SelectionMode.Single)
                return;
            this.AssociatedObject.SelectionChanged -= this.AssociatedObject_SelectionChanged;
            this.AssociatedObject.SelectedItems.Clear();
            foreach (object item in this.SelectedItems)
            {
                this.AssociatedObject.SelectedItems.Add(item);
            }
            this.AssociatedObject.SelectionChanged += this.AssociatedObject_SelectionChanged;
        }


        protected override void OnAttached()
        {
            this.RefreshData();
        }

        private void AssociatedObject_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IList dataSource = this.SelectedItems;
            foreach (object item in e.AddedItems)
            {
                dataSource.Add(item);
            }
            foreach (object item in e.RemovedItems)
            {
                dataSource.Remove(item);
            }
        }

        protected override void OnDetaching()
        {
            this.AssociatedObject.SelectionChanged -= AssociatedObject_SelectionChanged;
        }
    }
}
