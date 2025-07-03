// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Controls.Diagram.Datas;
using H.Controls.Diagram.Presenter.Extensions;
using System.Text.RegularExpressions;
namespace H.Controls.Diagram.Presenter.DiagramDatas.Base;
public abstract class FlowableDiagramDataBase : ZoomableDiagramDataBase, IFlowableDiagramData
{
    private DiagramFlowableState _state = DiagramFlowableState.None;
    [JsonIgnore]
    public DiagramFlowableState State
    {
        get { return _state; }
        set
        {
            _state = value;
            RaisePropertyChanged("State");
        }
    }

    private DiagramFlowableMode _flowableMode = DiagramFlowableMode.Node;
    [Display(Name = "运行模式", Order = 0, GroupName = "数据")]
    public DiagramFlowableMode FlowableMode
    {
        get { return _flowableMode; }
        set
        {
            _flowableMode = value;
            RaisePropertyChanged();
        }
    }

    private DiagramFlowableZoomMode _flowableZoomMode = DiagramFlowableZoomMode.None;
    [Display(Name = "自动缩放", GroupName = "数据", Description = "执行时节点自动缩放")]
    public DiagramFlowableZoomMode FlowableZoomMode
    {
        get { return _flowableZoomMode; }
        set
        {
            _flowableZoomMode = value;
            RaisePropertyChanged();
        }
    }

    private bool _useFlowableSelectToRunning = true;
    public bool UseFlowableSelectToRunning
    {
        get { return _useFlowableSelectToRunning; }
        set
        {
            _useFlowableSelectToRunning = value;
            RaisePropertyChanged();
        }
    }

    public virtual void OnInvokingPart(IPartData part)
    {
        var diagram = this.GetTargetElement<Diagram>();
        if (diagram == null)
            return;
        if (this.FlowableZoomMode == DiagramFlowableZoomMode.Rect)
        {
            diagram.Dispatcher.Invoke(() =>
            {
                var n = diagram.Nodes.FirstOrDefault(x => x.GetContent() == part);
                if (n == null)
                    return;
                diagram.ZoomTo(n);
            });

        }

        else if (this.FlowableZoomMode == DiagramFlowableZoomMode.Center)
        {
            //Point point = diagram.SelectedPart.Bound.GetCenter();
            //diagram.ZoomToCenter(part.Bound.BottomRight);
        }
        if (this.UseFlowableSelectToRunning)
        {
            diagram.Dispatcher.Invoke(() =>
            {
                var n = diagram.Nodes.FirstOrDefault(x => x.GetContent() == part);
                diagram.SelectedPart = n;
            });
        }

        if (part is ITextable textable)
            this.Message = "正在运行 - " + textable.Text;
    }

    public virtual void OnInvokedPart(IPartData part)
    {
        //if (this.UseFlowableSelectToRunning)
        //    part.IsSelected = false;
        ////if (this.FlowableZoomMode == DiagramFlowableZoomMode.Rect)
        ////    this.ZoomTo(part.Bound);
        ////else if (this.FlowableZoomMode == DiagramFlowableZoomMode.Center)
        ////{
        ////    Point point = part.Bound.GetCenter();
        ////    //zoombox.ZoomToCenter(part.Bound.BottomRight);
        ////}
        ////if (this.UseFlowableSelectToRunning)
        ////    part.IsSelected = true;
    }
    [Icon(FontIcons.Replay)]
    [Display(Name = "开始", GroupName = "操作", Order = 0)]
    public DisplayCommand StartCommand => new DisplayCommand(async e =>
    {
        await this.Start();
        if (this.FlowableZoomMode != DiagramFlowableZoomMode.None)
            this.ZoomToFit();
    }, e => this.CanStart());

    [Icon(FontIcons.Location)]
    [Display(Name = "停止", GroupName = "操作", Order = 0)]
    public DisplayCommand StopCommand => new DisplayCommand(e =>
    {
        this.Stop();
    }, e => this.State.CanStop());

    [Icon(FontIcons.Refresh)]
    [Display(Name = "重置", GroupName = "操作", Order = 0)]
    public DisplayCommand ResetCommand => new DisplayCommand(e =>
    {
        this.Reset();
    }, e => this.State.CanReset());

    public virtual async Task<bool?> Start()
    {
        var start = await this.GetStartNodeData();
        if (start == null)
            return false;
        var r = await this.InvokeState(() => start.Start(this));
        return r;
    }

    protected virtual async Task<IFlowableNodeData> GetStartNodeData()
    {
        var start = await this.TryGetStartNodeData<IFlowableNodeData>();
        if (start == null)
        {
            this.Message = "未运行";
            IocMessage.ShowNotifyInfo(this.Message);
            return null;
        }
        return start;
    }

    protected virtual bool CanStart()
    {
        return this.State.CanStart() && this.FlowableNodeDatas.Count() > 0;
    }

    public virtual void Stop()
    {
        if (!this.State.CanStop())
            return;
        this.State = DiagramFlowableState.Canceling;
        this.GotoState(x =>
        {
            if (x.State == FlowableState.Running || x.State == FlowableState.Wait || x.State == FlowableState.Ready || x.State == FlowableState.Break)
                return FlowableState.Canceling;
            return null;
        });
    }

    public virtual void Reset()
    {
        this.GotoState(x => FlowableState.Ready);
    }

    public virtual void Wait(Func<IFlowableNodeData, bool> match = null)
    {
        this.GotoState(x => FlowableState.Wait, match);
    }

    [JsonIgnore]
    public IEnumerable<IFlowableNodeData> FlowableNodeDatas => this.DataSource.GetNodeDatas().OfType<IFlowableNodeData>();

    protected override IDiagramDataSource CreateDataSource()
    {
        return new FlowableDiagramDataSource(this.Datas.NodeDatas, this.Datas.LinkDatas);
    }
}
