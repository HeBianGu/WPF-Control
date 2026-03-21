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

[Icon("\xE890")]
[Display(Name = "查看数据")]
public class DataGridTypePresenter : ItemsSourcePresenterBase, IItemsSourcePresenter
{
    private Type _Type;
    public Type Type
    {
        get { return _Type; }
        set
        {
            _Type = value;
            RaisePropertyChanged();
        }
    }
}

public static partial class DialogServiceExtension
{
    public static async Task<bool?> ShowDataGrid(this IDialogMessageService service, Action<DataGridPresenter> option, Action<DataGridPresenter> sumitAction = null, Action<IDialog> builder = null, Func<DataGridPresenter, Task<bool>> canSumit = null)
    {
        return await service.ShowDialog(option, sumitAction, x =>
        {
            x.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Stretch;
            x.MinWidth = 200;
            x.Padding = new Thickness(2);
            builder?.Invoke(x);
        }, canSumit);
    }

    public static async Task<bool?> ShowTypeDataGrid<ItemT>(this IDialogMessageService service, Action<DataGridTypePresenter> option, Action<DataGridTypePresenter> sumitAction = null, Action<IDialog> builder = null, Func<DataGridTypePresenter, Task<bool>> canSumit = null)
    {
        return await service.ShowTypeDataGrid(x =>
        {
            option?.Invoke(x);
            x.Type = typeof(ItemT);
        }, sumitAction, builder, canSumit);
    }

    public static async Task<bool?> ShowTypeDataGrid(this IDialogMessageService service, Action<DataGridTypePresenter> option, Action<DataGridTypePresenter> sumitAction = null, Action<IDialog> builder = null, Func<DataGridTypePresenter, Task<bool>> canSumit = null)
    {
        return await service.ShowDataGrid(option, sumitAction, builder, canSumit);
    }

    public static async Task<bool?> ShowDataGrid<T>(this IDialogMessageService service, Action<T> option, Action<T> sumitAction = null, Action<IDialog> builder = null, Func<T, Task<bool>> canSumit = null) where T : new()
    {
        return await service.ShowDialog(option, sumitAction, x =>
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


public class ShowTypeDataGridCommand : ShowSourceCommandBase
{
    public Type Type { get; set; }
    public override async Task ExecuteAsync(object parameter)
    {
        await IocMessage.Dialog.ShowTypeDataGrid(x =>
        {
            if (parameter is IEnumerable objects)
                x.ItemsSource = objects;
            else
                x.ItemsSource = this.ItemsSource;
            x.Type = this.Type;
        });
    }
}