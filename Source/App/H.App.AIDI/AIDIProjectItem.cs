// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.App.AIDI.Base;
using H.App.AIDI.DB;
using H.App.AIDI.Presenters;
using H.App.AIDI.ViewModel;
using H.Controls.TagBox;
using H.Extensions.Common;
using H.Extensions.DataBase.Repository;
using H.Extensions.Tree;
using H.Iocable;
using H.Modules.Project.Base;
using H.Services.Project;
using System.Collections;
using System.Text.Json.Serialization;

namespace H.App.AIDI;
public class AIDIProjectItem : ProjectItemBase, ITree
{
    [JsonIgnore]
    public override IProjectItemPresenter Presenter => new AIDIProjectItemPresenter(this);

    [JsonIgnore]
    public override IProjectItemThumbnailPresenter ThumbnailPresenter => new AIDIProjectItemThumbnailPresenter(this);

    [Browsable(false)]
    public ObservableCollection<Tag> Tags { get; set; } = new ObservableCollection<Tag>();


    private InputPagePresenter _InputPagePresenter;
    public InputPagePresenter InputPagePresenter
    {
        get { return _InputPagePresenter; }
        set
        {
            _InputPagePresenter = value;
            RaisePropertyChanged();
        }
    }

    private IPagePresenter _SelectedPagePresenter;
    public IPagePresenter SelectedPagePresenter
    {
        get { return _SelectedPagePresenter; }
        set
        {
            _SelectedPagePresenter = value;
            RaisePropertyChanged();
        }
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

    public override bool Load(out string message)
    {
        Ioc.GetService<IRepositoryBindable<fm_dd_image>>().RefreshData();
        ImageEx.ClearCache();
        var r = base.Load(out message);
        IocTagService.Instance.Load(out message);
        return r;
    }

    public override bool Save(out string message)
    {
        var filePath = this.InputPagePresenter.Repository.Collection.SelectedItem?.Model.Url;
        if (File.Exists(filePath))
            this.ThumbnialBase64Image = filePath.ToImageEx().ToBase64String();
        return base.Save(out message);
    }

    public bool Where(fm_dd_image entity)
    {
        if (entity.ProjectID != this.ID)
            return false;

        //if (this.SelectedPagePresenter is IModulePresenter modulePresenter)
        //{
        //    modulePresenter.Where(entity);
        //    var parent = this.GetParentModulePresenter(modulePresenter) ?? this.InputPagePresenter;
        //    // 父节点的结果数据传递下去
        //    if (parent.ResultWhere(entity) == true)
        //        return true;
        //}
        return this.SelectedPagePresenter.Where(entity);
    }

    public IEnumerable GetChildren(object parent)
    {
        if (parent == null)
            yield return this.InputPagePresenter;

        if (parent == this.InputPagePresenter)
        {
            foreach (var item in this.ModulePresenters.Where(x => x.PID == null || x.PID == this.InputPagePresenter.ID))
                yield return item;
        }

        if (parent is IModulePresenter module)
        {
            foreach (var item in this.ModulePresenters.Where(x => x.PID == module.ID))
                yield return item;
        }
    }

    //public IPagePresenter GetParentModulePresenter(IModulePresenter modulePresenter)
    //{
    //    return this.ModulePresenters.GetProvious(modulePresenter, false);
    //}

    #region Label

    private ObservableCollection<LabelItem> _Labels = new ObservableCollection<LabelItem>();
    [Browsable(false)]
    public ObservableCollection<LabelItem> Labels
    {
        get { return _Labels; }
        set
        {
            _Labels = value;
            RaisePropertyChanged();
        }
    }

    //[JsonIgnore]
    //[Icon(FontIcons.AddTo)]
    //[Display(Name = "添加标注", GroupName = "菜单栏,工具栏")]
    //public DisplayCommand AddTagCommand => new DisplayCommand(async x =>
    //{
    //    LabelItem labelItem = new LabelItem();
    //    var r = await IocMessage.Form.ShowEdit(labelItem, x => x.Title = "新增标注");
    //    if (r != true)
    //        return;
    //    this.Labels.Add(labelItem);
    //    //this.Save(out string message);
    //    //this.RefreshLabelFilters();
    //    //var finds = Application.Current.MainWindow.GetChildren<ContentPresenter>(x => x.Content is ImagePresenter);
    //    //foreach (var item in finds.Select(x => x.Content).OfType<ImagePresenter>())
    //    //    item.RefreshViewStates();
    //    this.Save(out string message);
    //});

    //[JsonIgnore]
    //[Icon(FontIcons.Tag)]
    //[Display(Name = "管理标注", GroupName = "菜单栏,工具栏")]
    //public DisplayCommand TagManagerCommand => new DisplayCommand(async x =>
    //{
    //    LabelManagerPresenter presenter = new LabelManagerPresenter();
    //    var r = await IocMessage.Dialog.Show(presenter);
    //    if (r != true)
    //        return;
    //    this.Labels = presenter.Collection;
    //    //this.Save(out string message);
    //    //this.RefreshLabelFilters();
    //    //var finds = Application.Current.MainWindow.GetChildren<ContentPresenter>(x => x.Content is ImagePresenter);
    //    //foreach (var item in finds.Select(x => x.Content).OfType<ImagePresenter>())
    //    //    item.RefreshViewStates();
    //    this.Save(out string message);
    //});
    #endregion
}


