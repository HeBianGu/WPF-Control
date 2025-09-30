// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Windows.Markup;

namespace H.Presenters.Common;
public interface IItemsSourcePresenter
{
    IEnumerable ItemsSource { get; set; }

    DataTemplate ItemContentTemplate { get; set; }
}

[Icon("\xE890")]
[Display(Name = "查看数据")]
public class DataGridPresenter : ItemsSourcePresenterBase, IItemsSourcePresenter
{

}

public static partial class DialogServiceExtension
{
    public static async Task<bool?> ShowDataGrid(this IDialogMessageService service, Action<IItemsSourcePresenter> option, Action<IItemsSourcePresenter> sumitAction = null, Action<IDialog> builder = null, Func<IItemsSourcePresenter, Task<bool>> canSumit = null)
    {
        return await service.ShowDialog<DataGridPresenter>(option, sumitAction, x =>
        {
            x.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Stretch;
            x.MinWidth = 200;
            x.Padding = new Thickness(2);
            builder?.Invoke(x);
        }, canSumit);
    }
}

public abstract class ShowSourceCommandBase : DisplayMarkupCommandBase
{
    public IEnumerable ItemsSource { get; set; }

    protected object _targetObject;
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        var provideValueTarget = serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;
        if (provideValueTarget != null)
            this._targetObject = provideValueTarget.TargetObject;
        return base.ProvideValue(serviceProvider);
    }
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
