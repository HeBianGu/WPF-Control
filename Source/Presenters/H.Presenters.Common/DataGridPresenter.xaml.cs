using H.Mvvm.ViewModels.Base;
using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace H.Presenters.Common;
public interface IItemsSourcePresenter
{
    IEnumerable ItemsSource { get; set; }
}

[Icon("\xE890")]
[Display(Name = "查看数据")]
public class DataGridPresenter : ItemsSourcePresenterBase, IItemsSourcePresenter
{

}

public static partial class DialogServiceExtension
{
    public static async Task<bool?> ShowDataGrid(this IDialogMessageService service, Action<IItemsSourcePresenter> option, Action<IItemsSourcePresenter> sumitAction = null, Action<IDialog> builder = null, Func<bool> canSumit = null)
    {
        return await service.ShowDialog<DataGridPresenter>(option, sumitAction, x =>
        {
            x.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Stretch;
            x.MinWidth = 200;
            builder?.Invoke(x);
        }, canSumit);
    }
}

public abstract class ShowSourceCommandBase : DisplayMarkupCommandBase
{
    public IEnumerable ItemsSource { get; set; }
}

public class ShowDataGridCommand : ShowSourceCommandBase
{

    public override async Task ExecuteAsync(object parameter)
    {
        await IocMessage.Dialog.ShowDataGrid(x =>
         {
             if (parameter is IEnumerable objects)
                 x.ItemsSource = objects;
             else
                 x.ItemsSource = this.ItemsSource;
         });
    }
}
