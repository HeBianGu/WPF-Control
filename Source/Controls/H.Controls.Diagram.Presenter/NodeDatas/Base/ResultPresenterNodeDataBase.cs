// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Diagram.Presenter.NodeDatas.Base;

public interface IResultPresenter
{

}

public interface IResultPresenterNodeData
{
    IResultPresenter ResultPresenter { get; }
}

public abstract class ResultPresenterNodeDataBase : FlowableNodeData, IResultPresenterNodeData
{
    private IResultPresenter _resultPresenter;
    [JsonIgnore]
    [Browsable(false)]
    [XmlIgnore]
    public IResultPresenter ResultPresenter
    {
        get { return _resultPresenter; }
        protected set
        {
            _resultPresenter = value;
            RaisePropertyChanged();
        }
    }

    public virtual IResultPresenter CreateResultPresenter()
    {
        return null;
    }

    protected virtual IFlowableResult OK(IResultPresenter resultPresenter = null, string message = "运行成功")
    {
        return this.ToResult(resultPresenter, FlowableResultState.OK, message);
    }

    protected virtual IFlowableResult Error(IResultPresenter resultPresenter = null, string message = "运行错误")
    {
        return this.ToResult(resultPresenter, FlowableResultState.Error, message);
    }

    protected virtual IFlowableResult ToResult(IResultPresenter resultPresenter, FlowableResultState state, string message = "运行成功")
    {
        this.ResultPresenter = resultPresenter;
        this.Message = message;
        return new FlowableResult(message) { State = state };
    }
}
