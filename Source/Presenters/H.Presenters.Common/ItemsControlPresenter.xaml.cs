using H.Mvvm.ViewModels.Base;
using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Windows;

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

    private DataTemplate _itemTemplate;
    public DataTemplate ItemTemplate
    {
        get { return _itemTemplate; }
        set
        {
            _itemTemplate = value;
            RaisePropertyChanged();
        }
    }

    public static DataTemplate GetItemTemplate(DependencyObject obj)
    {
        return (DataTemplate)obj.GetValue(ItemTemplateProperty);
    }

    public static void SetItemTemplate(DependencyObject obj, DataTemplate value)
    {
        obj.SetValue(ItemTemplateProperty, value);
    }

    public static readonly DependencyProperty ItemTemplateProperty =
        DependencyProperty.RegisterAttached("ItemTemplate", typeof(DataTemplate), typeof(ItemsSourcePresenterBase), new PropertyMetadata(default(DataTemplate), OnItemTemplateChanged));

    static public void OnItemTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d as DependencyObject;

        DataTemplate n = (DataTemplate)e.NewValue;

        DataTemplate o = (DataTemplate)e.OldValue;
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

             if (this._targetObject is FrameworkElement element)
             {
                 var find = ItemsControlPresenter.GetItemTemplate(element);
                 if (find != null)
                     x.ItemTemplate = find;
             }
         });
    }
}
