// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.App.AIDI.Base;
using H.Extensions.Mvvm.ViewModels.Tree;
using H.Extensions.Tree;
using H.Modules.Project.Base;
using H.Mvvm.Commands;
using H.Services.Message;
using H.Services.Message.Dialog;

namespace H.App.AIDI.Presenters;
public class ModuleTreeDataTemplateSelector : DataTemplateSelector
{
    public DataTemplate ModulePresenterDataTemplate { get; set; }
    public DataTemplate InputPagePresenterDataTemplate { get; set; }
    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    {
        if (item is IModulePresenter)
            return this.ModulePresenterDataTemplate;
        if (item is InputPagePresenter)
            return this.InputPagePresenterDataTemplate;
        return base.SelectTemplate(item, container);
    }
}

public class AIDIProjectItemPresenter : ProjectItemPresenter<AIDIProjectItem>
{
    public AIDIProjectItemPresenter(AIDIProjectItem projectItem) : base(projectItem)
    {
        this.UpdateModuleTreeNodes();
    }
    public RelayCommand AddModuleCommand => new RelayCommand(async x =>
    {
        IPagePresenter pagePresenter = this.ProjectItem.InputPagePresenter;
        if (x is IPagePresenter xp)
            pagePresenter = xp;
        ModulesSelectPresenter presenter = new ModulesSelectPresenter(this.ProjectItem);
        var r = await IocMessage.Dialog.Show(presenter, x => x.DialogButton = DialogButton.None);
        if (r != true)
            return;
        var n = presenter.SelectedModulePresenter;
        n.PID = pagePresenter.ID;
        this.ProjectItem.ModulePresenters.Add(n);
        this.ProjectItem.SelectedPagePresenter = n.ModuleViewPagePresenter;
        presenter.SelectedModulePresenter.ModuleViewPagePresenter.RefreshData();
        this.UpdateModuleTreeNodes();
    });

    public RelayCommand DeleteModuleCommand => new RelayCommand(async x =>
    {
        if (x is IModulePresenter module)
        {
            await IocMessage.Dialog.ShowDeleteDialog(x =>
            {
                this.ProjectItem.ModulePresenters.Remove(module);
                if (this.ProjectItem.SelectedPagePresenter == null)
                {
                    this.ProjectItem.SelectedPagePresenter = this.ProjectItem.ModulePresenters?.FirstOrDefault();
                    this.ProjectItem.SelectedPagePresenter.RefreshData();
                }
            });
            this.UpdateModuleTreeNodes();
        }
    });

    public RelayCommand GotoPageCommand => new RelayCommand(async x =>
    {
        if (x is IPagePresenter presenter)
        {
            this.ProjectItem.SelectedPagePresenter = presenter;
            await IocMessage.Dialog.ShowWait(x =>
            {
                this.ProjectItem.SelectedPagePresenter.RefreshData();
                return true;
            });
        }
    });

    private IEnumerable<ITreeNode> _ModuleTreeNodes;
    public IEnumerable<ITreeNode> ModuleTreeNodes
    {
        get { return _ModuleTreeNodes; }
        set
        {
            _ModuleTreeNodes = value;
            RaisePropertyChanged();
        }
    }


    public void UpdateModuleTreeNodes()
    {
        this.ModuleTreeNodes = this.ProjectItem.GetTreeNodes();
    }
}