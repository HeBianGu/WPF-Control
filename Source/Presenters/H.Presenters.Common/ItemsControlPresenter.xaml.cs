using H.Mvvm.ViewModels.Base;
using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace H.Presenters.Common;

public abstract class ItemsSourcePresenterBase : DisplayBindableBase, IItemsSourcePresenter
{
    private IEnumerable _itemsSource;
    public IEnumerable ItemsSource
    {
        get { return _itemsSource; }
        set
        {
            _itemsSource = value;
            RaisePropertyChanged();
        }
    }
}
[Icon("\xE890")]
[Display(Name = "查看数据")]
public class ItemsControlPresenter : ItemsSourcePresenterBase, IItemsSourcePresenter
{


}

public static partial class DialogServiceExtension
{
    public static async Task<bool?> ShowItemsControl(this IDialogMessageService service, Action<IItemsSourcePresenter> option, Action<IItemsSourcePresenter> sumitAction = null, Action<IDialog> builder = null, Func<bool> canSumit = null)
    {
        return await service.ShowDialog<ItemsControlPresenter>(option, sumitAction, x =>
        {
            x.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Stretch;
            x.MinWidth = 200;
            builder?.Invoke(x);
        }, canSumit);
    }
}

public class ShowItemsControlCommand : ShowSourceCommandBase
{
    public override async Task ExecuteAsync(object parameter)
    {
        await IocMessage.Dialog.ShowItemsControl(x =>
         {
             if (parameter is IEnumerable objects)
                 x.ItemsSource = objects;
             else
                 x.ItemsSource = this.ItemsSource;
         });
    }
}
