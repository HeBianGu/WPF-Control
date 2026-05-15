// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")


using H.Components.Vision.Presenters;
using H.Controls.ShapeBox.State;
using H.Controls.ShapeBox.State.Base;

namespace H.Components.Vision.Base;

public abstract class StateNodeDataBase : StyleNodeDataBase
{
    private IViewState _defaultState;
    protected StateNodeDataBase()
    {
        this._defaultState = this.GetDefaultViewState();
        this.ViewStates = this.CreateViewStates().ToObservable();
        this.ViewState = this._defaultState;
    }


    protected void UpdateViewStates()
    {
        this.ViewStates = this.CreateViewStates().ToObservable();
    }

    private IViewState _viewState;
    [JsonIgnore]
    public IViewState ViewState
    {
        get { return _viewState; }
        set
        {
            _viewState = value;
            RaisePropertyChanged();
        }
    }

    private ObservableCollection<IViewState> _ViewStates = new ObservableCollection<IViewState>();
    [JsonIgnore]
    public ObservableCollection<IViewState> ViewStates
    {
        get { return _ViewStates; }
        set
        {
            _ViewStates = value;
            RaisePropertyChanged();
        }
    }

    protected virtual IViewState GetDefaultViewState()
    {
        return new NoneState();
    }

    protected virtual IEnumerable<IViewState> CreateViewStates()
    {
        if (this._defaultState == null)
            yield break;
        yield return this._defaultState;
        yield return new AddPickPixelShapeState();
        yield return new AddRulerLineShapeState();
    }
}

