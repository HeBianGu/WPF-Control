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

    public override IFlowableResult Invoke(IFlowableLinkData previors, IFlowableDiagramData diagram)
    {
        this.ResultPresenter = null;
        var r = base.Invoke(previors, diagram);
        if (this.ResultPresenter == null)
            this.ResultPresenter = this.CreateResultPresenter();
        return r;
    }

    public override void Clear()
    {
        this.ResultPresenter = null;
        base.Clear();
    }
}
