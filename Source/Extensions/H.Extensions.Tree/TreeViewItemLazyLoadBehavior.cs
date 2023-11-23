using H.Providers.Mvvm;
using Microsoft.Xaml.Behaviors;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace H.Extensions.Tree
{
    public class TreeViewItemLazyLoadBehavior : Behavior<TreeViewItem>
    {
        public ITree Tree { get; set; }

        protected override void OnAttached()
        {
            this.AssociatedObject.Loaded += AssociatedObject_Loaded;
        }

        protected override void OnDetaching()
        {
            this.AssociatedObject.Loaded -= AssociatedObject_Loaded;
        }

        private void AssociatedObject_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            this.RefreshData();
        }

        private void RefreshData()
        {
            if (this.Tree == null)
                return;
            foreach (var item in this.AssociatedObject.ItemsSource)
            {
                if (item is TreeNodeBase<object> node)
                {
                    if (node.IsLoaded)
                        continue;
                    if (node.Nodes.Count > 0)
                        continue;

                    var chidren = TreeDataProvider.Instance.GetTreeNodes(node.Model, this.Tree, false);
                    node.Nodes = new ObservableCollection<TreeNodeBase<object>>(chidren);
                    System.Diagnostics.Debug.WriteLine(node.Nodes.Count);
                    node.IsLoaded= true;
                }
            }
        }
    }
}
