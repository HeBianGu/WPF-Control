// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.App.AIDI.Base;
using H.Common.Attributes;
using H.Extensions.Common;
using H.Extensions.FontIcon;
using H.Mvvm.Commands;

namespace H.App.AIDI.Presenters;
[Icon(FontIcons.Add)]
[Display(Name = "添加模块/工具")]
public class ModulesSelectPresenter : DisplayBindableBase
{
    public ModulesSelectPresenter(AIDIProjectItem projcetItem)
    {
        var modules = this.GetType().GetInstances<IModulePresenter>(projcetItem);
        this.ModulePresenters = modules.ToObservable();
        this.SelectedModulePresenter = modules?.FirstOrDefault();
    }
    private ObservableCollection<IModulePresenter> _ModulePresenters = new ObservableCollection<IModulePresenter>();
    public ObservableCollection<IModulePresenter> ModulePresenters
    {
        get { return _ModulePresenters; }
        set
        {
            _ModulePresenters = value;
            RaisePropertyChanged();
        }
    }
    private IModulePresenter _SelectedModulePresenter;
    public IModulePresenter SelectedModulePresenter
    {
        get { return _SelectedModulePresenter; }
        set
        {
            _SelectedModulePresenter = value;
            RaisePropertyChanged();
        }
    }


    public RelayCommand AddCommand => new RelayCommand(x =>
    {
        if (x is IModulePresenter t)
        {
            this.SelectedModulePresenter = t;
        }
    });
}




