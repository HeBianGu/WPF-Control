// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.App.AIDI.Presenters.Components;
using H.Common.Interfaces;
using H.Extensions.Common;
using System.Text.Json.Serialization;

namespace H.App.AIDI.Base;
public interface IPagePresenter : IRepositoryPresenter, IDable
{
    public string PID { get; set; }
}

public interface ITabPagePresenter : IPagePresenter
{
    ImageViewComponentPresenter ImageViewComponentPresenter { get; set; }
    ObservableCollection<IPageTabPresenter> PageTabPresenters { get; set; }
    IPageTabPresenter SelectedPageTabPresenter { get; set; }
}

public abstract class PagePresenterBase : RepositoryPresenterBase, IPagePresenter, ITabPagePresenter
{
    protected PagePresenterBase()
    {

    }
    public PagePresenterBase(AIDIProjectItem projcetItem)
    {
        this.Init();
        this.ProjectItem = projcetItem;
    }

    protected override void Init()
    {
        this.ImageIOComponentPresenter = new ImageIOComponentPresenter(this);
        this.PageTabPresenters = this.CreatePageTabPresenters().ToObservable();
        this.SelectedPageTabPresenter = this.PageTabPresenters?.FirstOrDefault();
        this.PageTabComponentPresenter = new PageTabComponentPresenter(this);
        base.Init();
    }

    public string PID { get; set; }

    public AIDIProjectItem ProjectItem { get; set; }

    private ImageListComponentPresenter _ImageListComponentPresenter = new ImageListComponentPresenter();
    [JsonIgnore]
    public ImageListComponentPresenter ImageListComponentPresenter
    {
        get { return _ImageListComponentPresenter; }
        set
        {
            _ImageListComponentPresenter = value;
            RaisePropertyChanged();
        }
    }

    private ImageIOComponentPresenter _ImageIOComponentPresenter;
    [JsonIgnore]
    public ImageIOComponentPresenter ImageIOComponentPresenter
    {
        get { return _ImageIOComponentPresenter; }
        set
        {
            _ImageIOComponentPresenter = value;
            RaisePropertyChanged();
        }
    }


    private ImageViewComponentPresenter _ImageViewComponentPresenter = new ImageViewComponentPresenter();
    [JsonIgnore]
    public ImageViewComponentPresenter ImageViewComponentPresenter
    {
        get { return _ImageViewComponentPresenter; }
        set
        {
            _ImageViewComponentPresenter = value;
            RaisePropertyChanged();
        }
    }

    private TagManagerComponentPresenter _TagManagerComponentPresenter = new TagManagerComponentPresenter();
    [JsonIgnore]
    public TagManagerComponentPresenter TagManagerComponentPresenter
    {
        get { return _TagManagerComponentPresenter; }
        set
        {
            _TagManagerComponentPresenter = value;
            RaisePropertyChanged();
        }
    }

    private PageTabComponentPresenter _PageTabComponentPresenter;
    [JsonIgnore]
    public PageTabComponentPresenter PageTabComponentPresenter
    {
        get { return _PageTabComponentPresenter; }
        set
        {
            _PageTabComponentPresenter = value;
            RaisePropertyChanged();
        }
    }

    private ObservableCollection<IPageTabPresenter> _PageTabPresenters = new ObservableCollection<IPageTabPresenter>();
    [JsonIgnore]
    public ObservableCollection<IPageTabPresenter> PageTabPresenters
    {
        get { return _PageTabPresenters; }
        set
        {
            _PageTabPresenters = value;
            RaisePropertyChanged();
        }
    }


    private IPageTabPresenter _SelectedPageTabPresenter;
    [JsonIgnore]
    public IPageTabPresenter SelectedPageTabPresenter
    {
        get { return _SelectedPageTabPresenter; }
        set
        {
            _SelectedPageTabPresenter = value;
            RaisePropertyChanged();
        }
    }

    private bool _UseImageIODock = false;
    public bool UseImageIODock
    {
        get { return _UseImageIODock; }
        set
        {
            _UseImageIODock = value;
            RaisePropertyChanged();
        }
    }

    protected virtual IEnumerable<IPageTabPresenter> CreatePageTabPresenters()
    {
        yield break;
    }
}
