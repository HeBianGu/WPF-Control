using H.Mvvm.Commands;
using H.Presenters.Common;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace H.Presenters.Common;
public interface ITreeViewPresenter : IItemsSourcePresenter
{
    object SelectedItem { get; }
}
[Icon("\xE890")]
[Display(Name = "查看数据")]
public class TreeViewPresenter : ItemsSourcePresenterBase, ITreeViewPresenter
{
    public TreeViewPresenter()
    {

    }
    public TreeViewPresenter(IEnumerable source, object selectedItem)
    {
        this.ItemsSource = source;
        this.SelectedItem = selectedItem;
    }

    private object _selectedItem;
    public object SelectedItem
    {
        get { return _selectedItem; }
        set
        {
            _selectedItem = value;
            RaisePropertyChanged();
        }
    }

    public RelayCommand SelectedItemChangedCommand => new RelayCommand(x =>
    {
        if (x is RoutedPropertyChangedEventArgs<object> p)
        {
            this.SelectedItem = p.NewValue;
        }
    });
}

public static partial class DialogServiceExtension
{
    public static async Task<bool?> ShowTreeView(this IDialogMessageService service, Action<ITreeViewPresenter> option, Action<ITreeViewPresenter> sumitAction = null, Action<IDialog> builder = null, Func<ITreeViewPresenter, Task<bool>> canSumit = null)
    {
        return await service.ShowDialog<TreeViewPresenter>(option, sumitAction, x =>
        {
            x.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Stretch;
            x.MinWidth = 200;
            x.Padding = new Thickness(2);
            builder?.Invoke(x);
        }, canSumit);
    }
}

public class ShowTreeViewCommand : ShowSourceCommandBase
{
    public override async Task ExecuteAsync(object parameter)
    {
        await IocMessage.Dialog.ShowTreeView(x =>
         {
             if (parameter is IEnumerable objects)
                 x.ItemsSource = objects;
             else
                 x.ItemsSource = this.ItemsSource;
             if (this._targetObject is FrameworkElement element)
             {
                 var find = TreeViewPresenter.GetItemTemplate(element);
                 if (find != null)
                     x.ItemContentTemplate = find;
             }
         });
    }
}
