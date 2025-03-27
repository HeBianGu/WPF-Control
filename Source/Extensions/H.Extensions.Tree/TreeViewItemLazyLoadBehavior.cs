using H.Mvvm.ViewModels.Tree;
using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace H.Extensions.Tree;

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
        foreach (object item in this.AssociatedObject.ItemsSource)
        {
            if (item is TreeNodeBase<object> node)
            {
                if (node.IsLoaded)
                    continue;
                if (node.Nodes.Count > 0)
                    continue;

                node.IsBuzy = true;
                System.Collections.Generic.IEnumerable<TreeNodeBase<object>> chidren = this.Tree.GetTreeNodes(node.Model, false).ToList();
                //node.Nodes = new ObservableCollection<TreeNodeBase<object>>(chidren);
                node.Nodes.Clear();
                foreach (var child in chidren)
                {
                    Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Input, new Action(() =>
                    {
                        node.Nodes.Add(child);
                    }));

                }
                System.Diagnostics.Debug.WriteLine(node.Nodes.Count);
                node.IsLoaded = true;
            }
        }
    }
}
