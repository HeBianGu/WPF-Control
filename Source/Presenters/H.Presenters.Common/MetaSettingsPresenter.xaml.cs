// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.FontIcon;
using H.Extensions.TextJsonable;
using H.Iocable;
using H.Mvvm.Commands;
using H.Services.Common.Serialize.Meta;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace H.Presenters.Common;

public abstract class MetaSettingsPresenterBase : MetaSettingPresenterBase, IMetaSetting
{

}

public class MetaSettingsPresenter<T> : MetaSettingsPresenterBase, IMetaSetting
{
    private ObservableCollection<T> _collection = new ObservableCollection<T>();
    public ObservableCollection<T> Collection
    {
        get { return _collection; }
        set
        {
            _collection = value;
            RaisePropertyChanged();
        }
    }

    private T _SelectedItem;
    [JsonIgnore]
    public T SelectedItem
    {
        get { return _SelectedItem; }
        set
        {
            _SelectedItem = value;
            RaisePropertyChanged();
        }
    }

    public override void Load()
    {
        var datas = this.GetMetaSettingService().Deserilize<List<T>>(this.ID);
        if (datas == null)
            return;
        this.Collection = datas.ToObservable();
    }

    [Icon(FontIcons.Add)]
    [Display(Name = "新增")]
    public DisplayCommand AddCommand => new DisplayCommand(async x =>
    {
        var newItem = this.CreateNewItem();
        var r = await IocMessage.Form.ShowEdit(newItem);
        if (r != true)
            return;
        this.Collection.Add(newItem);
        this.SelectedItem = newItem;
    });

    protected virtual T CreateNewItem()
    {
        return Activator.CreateInstance<T>();
    }

    public override bool Save(out string message)
    {
        this.GetMetaSettingService().Serilize(this.Collection.ToList(), this.ID);
        message = null;
        return true;
    }

    public async Task<bool?> AddSave(T t)
    {
        var r = await IocMessage.Form.ShowEdit(t);
        if (r != true)
            return r;
        this.Load();
        this.Collection.Add(t);
        this.Save(out _);
        return true;
    }

    public virtual async Task<T> ShowSelectItemAsync()
    {
        this.Load();
        var r = await IocMessage.Dialog.Show(this);
        if (r != true)
            return default;
        return this.SelectedItem;
    }
}
