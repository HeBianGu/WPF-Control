// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.App.AIDI.Base;
using H.Mvvm.Commands;
using H.Services.Message;

namespace H.App.AIDI.Presenters;
public class ModuleViewPagePresenter : ModuleViewPagePresenterBase
{
    public ModuleViewPagePresenter()
    {

    }
    public ModuleViewPagePresenter(AIDIProjectItem projcetItem, IModulePresenter modulePresenter) : base(projcetItem)
    {
        this.ModulePresenter = modulePresenter;
    }
    public IModulePresenter ModulePresenter { get; }
    public RelayCommand ApplyCommand => new RelayCommand(async x =>
    {
        await IocMessage.Dialog.ShowWait(x =>
         {
             var images = this.Repository.Collection.Where(l => this.Where(l.Model)).Select(x => x.Model).ToList();
             var clones = images.Select(x =>
             {
                 var r = x.CloneData();
                 r.PageID = this.ModulePresenter.ID;
                 return r;
             }).ToArray();

             this.ModulePresenter.ClearDatas();
             this.Repository.Add(clones);
             this.Repository.Save();
             this.ProjectItem.SelectedPagePresenter = this.ModulePresenter;
             this.ProjectItem.SelectedPagePresenter.RefreshData();
             return true;
         });
    });
}




