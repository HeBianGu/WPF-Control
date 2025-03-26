global using H.Extensions.Common;
using System.Windows;
using System.Windows.Controls;

namespace H.Controls.PDF
{
    public class BindSelectedItemBehavior : BehaviorForStyle<TreeView, BindSelectedItemBehavior>
    {
        #region SelectedItem Property

        public object SelectedItem
        {
            get => GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register(nameof(SelectedItem), typeof(object),
                typeof(BindSelectedItemBehavior), new UIPropertyMetadata(null, OnSelectedItemChanged));

        private static void OnSelectedItemChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is TreeViewItem item)
            {
                item.SetValue(TreeViewItem.IsSelectedProperty, true);
            }
            else
            {
                if (sender is BindSelectedItemBehavior behavior)
                {
                    var treeitem = behavior.AssociatedObject.GetChild<TreeViewItem>(x => x.DataContext == e.NewValue);
                    if (treeitem != null)
                    {
                        treeitem.IsSelected = true;
                    };
                }
            }
        }

        #endregion

        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.SelectedItemChanged += OnTreeViewSelectedItemChanged;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            if (this.AssociatedObject != null)
            {
                this.AssociatedObject.SelectedItemChanged -= OnTreeViewSelectedItemChanged;
            }
        }

        private void OnTreeViewSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            this.SelectedItem = e.NewValue;
        }
    }
}