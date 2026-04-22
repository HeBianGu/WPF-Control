// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.App.AIDI.DB;
using H.Extensions.DataBase.Repository;
using H.Iocable;
using H.Mvvm.Commands;
using H.Services.Message;
using System.Text.Json.Serialization;

namespace H.App.AIDI.Base;
public interface IRepositoryPresenter
{
    IRepositoryBindable<fm_dd_image> Repository { get; }
    void RefreshData();

    void ClearDatas();

    bool Where(fm_dd_image entity);

    bool ResultWhere(fm_dd_image entity);
}

public abstract class RepositoryPresenterBase : DisplayBindableBase, IRepositoryPresenter
{
    [JsonIgnore]
    public IRepositoryBindable<fm_dd_image> Repository => Ioc.GetService<IRepositoryBindable<fm_dd_image>>();

    public void RefreshData()
    {
        this.Repository.RefreshData();
    }


    public void ClearDatas()
    {
        var cimages = this.Repository.Repository.GetList(x => x.PageID == this.ID);
        foreach (var item in cimages)
            this.Repository.Repository.Delete(item);
        this.Repository.Repository.Save();
    }

    public RelayCommand RefreshDataCommand => new RelayCommand(async x =>
    {
        await IocMessage.Dialog.ShowWait(x =>
        {
            this.RefreshData();
            return true;
        });
    });
    public virtual bool Where(fm_dd_image entity)
    {
        return entity.PageID == this.ID;
    }

    public virtual bool ResultWhere(fm_dd_image entity)
    {
        return false;
    }
}
