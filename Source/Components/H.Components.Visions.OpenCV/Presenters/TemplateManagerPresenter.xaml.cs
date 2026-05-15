// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.Mvvm.Commands;
using H.Extensions.Mvvm.ViewModels.Base;

namespace H.Components.Visions.OpenCV.Presenters;

public class TemplateManagerPresenterBase : DisplayBindableBase
{
    private readonly IResultImageSourceNodeData _nodeData;
    public TemplateManagerPresenterBase(IResultImageSourceNodeData nodeData)
    {
        _nodeData = nodeData;
        if (nodeData.ResultImageSource != null)
            this.ImageSource = _nodeData.ResultImageSource;
        var fromNodeDatas = nodeData.GetAllFromNodeDatas().OfType<IResultImageSourceNodeData>();
        this.ResultImageSourceNodeDatas = fromNodeDatas.ToObservable();
        this.ResultImageSourceNodeDatas.Insert(0, nodeData);
        this.SelectedImageSourceNodeData = nodeData;
        this.ViewStates = this.GetViewStates().ToObservable();
        this.ViewState = this.ViewStates?.FirstOrDefault();
    }
    [Icon(FontIcons.Setting)]
    [Display(Name = "选择当前图像")]
    public DisplayCommand SelectCurrentImageCommand => new DisplayCommand(x =>
    {
        if (this._nodeData.ResultImageSource == null)
            return;
        this.SelectedImageSourceNodeData = this._nodeData;
        this.FilePath = null;
    }, x => this._nodeData.ResultImageSource != null);

    [Icon(FontIcons.Setting)]
    [Display(Name = "选择其他图像")]
    public DisplayCommand SelectOtherCommand => new DisplayCommand(x =>
    {
        var r = IocMessage.IOFileDialog.ShowOpenImageFile();
        if (r == null)
            return;
        this.FilePath = r;
        this.ImageSource = r.ToImageSource();
    });

    private ImageSource _ImageSource;
    public ImageSource ImageSource
    {
        get { return _ImageSource; }
        set
        {
            _ImageSource = value;
            RaisePropertyChanged();
        }
    }

    private IResultImageSourceNodeData _SelectedImageSourceNodeData;
    public IResultImageSourceNodeData SelectedImageSourceNodeData
    {
        get { return _SelectedImageSourceNodeData; }
        set
        {
            _SelectedImageSourceNodeData = value;
            RaisePropertyChanged();
            this.ImageSource = value.ResultImageSource;
            this.UpdateResult();
        }
    }

    private ObservableCollection<IResultImageSourceNodeData> _ResultImageSourceNodeDatas = new ObservableCollection<IResultImageSourceNodeData>();
    public ObservableCollection<IResultImageSourceNodeData> ResultImageSourceNodeDatas
    {
        get { return _ResultImageSourceNodeDatas; }
        set
        {
            _ResultImageSourceNodeDatas = value;
            RaisePropertyChanged();
        }
    }

    private string _FilePath;
    public string FilePath
    {
        get { return _FilePath; }
        set
        {
            _FilePath = value;
            RaisePropertyChanged();
            this.UpdateResult();
        }
    }

    private ObservableCollection<IViewState> _ViewStates = new ObservableCollection<IViewState>();
    public ObservableCollection<IViewState> ViewStates
    {
        get { return _ViewStates; }
        set
        {
            _ViewStates = value;
            RaisePropertyChanged();
        }
    }

    private IViewState _ViewState;
    public IViewState ViewState
    {
        get { return _ViewState; }
        set
        {
            _ViewState = value;
            RaisePropertyChanged();
            this.UpdateResult();
        }
    }

    protected virtual IEnumerable<IViewState> GetViewStates()
    {
        yield break;
    }

    private ObservableCollection<IShape> _Shapes = new ObservableCollection<IShape>();
    public ObservableCollection<IShape> Shapes
    {
        get { return _Shapes; }
        set
        {
            _Shapes = value;
            RaisePropertyChanged();
        }
    }

    protected virtual void UpdateResult()
    {

    }
}

