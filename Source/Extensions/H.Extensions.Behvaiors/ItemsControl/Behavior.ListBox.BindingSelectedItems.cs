using Microsoft.Xaml.Behaviors;
using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace H.Extensions.Behvaiors
{
    public class ListBoxBindingSelectedItemsBehavior : Behavior<ListBox>
    {

        public IList SelectedItems
        {
            get { return (IList)GetValue(SelectedItemsProperty); }
            set { SetValue(SelectedItemsProperty, value); }
        }

        
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

        private void RefreshData()
        {
            if (AssociatedObject.SelectionMode == SelectionMode.Single)
                return;
            AssociatedObject.SelectionChanged -= AssociatedObject_SelectionChanged;
            AssociatedObject.SelectedItems.Clear();
            foreach (object item in SelectedItems)
            {
                AssociatedObject.SelectedItems.Add(item);
            }
            AssociatedObject.SelectionChanged += AssociatedObject_SelectionChanged;
        }


        protected override void OnAttached()
        {
            RefreshData();
        }

        private void AssociatedObject_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IList dataSource = SelectedItems;
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
            AssociatedObject.SelectionChanged -= AssociatedObject_SelectionChanged;
        }
    }
}
