global using H.Mvvm.Commands;
global using H.Presenters.Common;
global using System.Collections;
global using System.ComponentModel.DataAnnotations;
global using System.Windows;
global using H.Common.Attributes;

namespace H.Presenters.Common;
public interface IListBoxPresenter : IItemsSourcePresenter
{
    string DisplayMemberPath { get; set; }
    object SelectedItem { get; set; }

    bool UseDelete { get; set; }
}
[Icon("\xE890")]
[Display(Name = "选择数据")]
public class ListBoxPresenter : ItemsSourcePresenterBase, IListBoxPresenter
{
    public ListBoxPresenter()
    {

    }
    public ListBoxPresenter(IEnumerable source, object selectedItem)
    {
        this.ItemsSource = source;
        this.SelectedItem = selectedItem;
    }

    private string _displayMemberPath;
    public string DisplayMemberPath
    {
        get { return _displayMemberPath; }
        set
        {
            _displayMemberPath = value;
            RaisePropertyChanged();
        }
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

    private bool _useDelete;
    public bool UseDelete
    {
        get { return _useDelete; }
        set
        {
            _useDelete = value;
            RaisePropertyChanged();
        }
    }


    public RelayCommand DeleteSelectedCommand => new RelayCommand(x =>
    {
        if (this.ItemsSource is IList list)
            list.Remove(this.SelectedItem);
    }, x => this.SelectedItem != null);
}


public static partial class DialogServiceExtension
{
    [Obsolete("ShowListBox<T>")]
    public static async Task<bool?> ShowListBox(this IDialogMessageService service, Action<IListBoxPresenter> option, Action<IListBoxPresenter> sumitAction = null, Action<IDialog> builder = null, Func<IListBoxPresenter, Task<bool>> canSumit = null)
    {
        return await service.ShowDialog<ListBoxPresenter>(option, sumitAction, x =>
        {
            x.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Stretch;
            x.MinWidth = 200;
            x.Padding = new Thickness(2);
            builder?.Invoke(x);
        }, canSumit);
    }

    public static async Task<T> ShowListBox<T>(this IDialogMessageService service, Action<IListBoxPresenter> option, Action<IListBoxPresenter> sumitAction = null, Action<IDialog> builder = null, Func<IListBoxPresenter, Task<bool>> canSumit = null)
    {
        T result = default(T);
        var r = await service.ShowDialog<ListBoxPresenter>(option, x=>
        {
            sumitAction?.Invoke(x);
            result = (T)x.SelectedItem;
        }, x =>
        {
            x.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Stretch;
            x.MinWidth = 200;
            x.Padding = new Thickness(2);
            builder?.Invoke(x);
        }, canSumit);
        return result;
    }
}

public class ShowListBoxCommand : ShowSourceCommandBase
{
    public string DisplayMemberPath { get; set; }
    public override async Task ExecuteAsync(object parameter)
    {
        await IocMessage.Dialog.ShowListBox(x =>
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
                 else
                     x.DisplayMemberPath = this.DisplayMemberPath;
             }
         });
    }
}
