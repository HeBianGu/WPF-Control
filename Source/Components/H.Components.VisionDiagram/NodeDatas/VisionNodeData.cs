// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Components.VisionDiagram.DiagramData;
using H.Controls.Diagram.Presenter.Extensions;
using H.Iocable;

namespace H.Components.VisionDiagram.NodeDatas;
public abstract class VisionNodeData<T> : VisionNodeDataBase, IVisionNodeData<T> where T : class, IVisionImage
{
    ~VisionNodeData()
    {
        this.Dispose();
    }

    private T _ResultImage;
    [JsonIgnore]
    [Expressionable]
    [Tab(VisionTabNames.ResultParameters)]
    [Display(Name = "图像结果", GroupName = VisionTabNames.ResultParameters, Description = "当前流程运行完返回的图像结果")]
    public virtual T ResultImage
    {
        get { return _ResultImage; }
        set
        {
            _ResultImage = value;
            RaisePropertyChanged();
            this.InvalidateResultImageSource();
        }
    }


    private int _InvokeTotal;
    [ReadOnly(true)]
    [Expressionable]
    [Tab(VisionTabNames.ResultParameters)]
    [Display(Name = "运行次数", GroupName = VisionTabNames.ResultParameters, Description = "结果参数，此结果可应用再条件分支等作为判断参数")]
    public int InvokeTotal
    {
        get { return _InvokeTotal; }
        set
        {
            _InvokeTotal = value;
            RaisePropertyChanged();
        }
    }

    IVisionImage IVisionNodeData.VisionImage => this.ResultImage;

    protected override ImageSource CreateImageSource()
    {
        return this.ResultImage?.ToImageSource();
    }

    public override IFlowableResult Invoke(IFlowableLinkData previors, IFlowableDiagramData diagram)
    {
        this.InvokeTotal++;
        base.Invoke(previors, diagram);
        IStartVisionNodeData srcData = diagram.GetStartNodeDatas().OfType<IStartVisionNodeData>().FirstOrDefault();
        IVisionNodeData fromData = this.GetFromNodeData<IVisionNodeData>(diagram, previors);
        return this.InvokeAction(() => this.Invoke(srcData, fromData ?? srcData, this.DiagramData as IFlowableDiagramData));
    }

    protected T GetVisionImage(IVisionNodeData from)
    {
        var converter = Ioc.GetService<IVisionImageConverterService>();
        var resultImage = converter.Convert<T>(from?.VisionImage);
        return resultImage;
    }

    public override async Task<IFlowableResult> TryInvokeAsync(IFlowableLinkData previors, IFlowableDiagramData diagram)
    {
#if DEBUG
        //if (!this._lic)
        //{
        //    var options = VisionLicenseService.Vision.IsVail(out string message);
        //    if (options == null)
        //    {
        //        var r = await IocMessage.Dialog.Show(new VisionLicenseViewPresenter());
        //        if (r != true)
        //            return await Task.FromResult(this.Error("许可无效:请联系客服QQ908293466"));
        //    }
        //    this._lic = true;
        //}
#endif
        return await base.TryInvokeAsync(previors, diagram);
    }

    protected void Invoke()
    {
        if (this.DiagramData == null)
            return;
        if (this.DiagramData is IFlowableDiagramData flowable && flowable.State == DiagramFlowableState.Running)
            return;
        IStartVisionNodeData srcData = this.DiagramData.GetStartNodeDatas().OfType<IStartVisionNodeData>().FirstOrDefault();
        INodeData from = this.FromNodeDatas.FirstOrDefault();
        if (this.FromNodeDatas.Count() == 0)
            from = this;
        if (this.FromNodeDatas.Count() > 1)
            return;
        if (from is IVisionNodeData<T> visionNodeData)
        {
            if (visionNodeData.ResultImage == null)
                return;
            if (!visionNodeData.ResultImage.IsValid())
                return;
            this.Clear();
            this.InvokeAction(() => this.Invoke(srcData, visionNodeData, this.DiagramData as IFlowableDiagramData));
            if (this.DiagramData is IResultImageSourceDiagramData imageSourceDiagramData)
                imageSourceDiagramData.ResultImageSource = this.ResultImageSource;
        }
    }

    public override void Clear()
    {
        this.ResultImageDisopse();
        base.Clear();
    }

    protected virtual void ResultImageDisopse()
    {
        this.ResultImage?.Dispose();
        this.ResultImage = default;
    }

    protected virtual FlowableResult<T> InvokeAction(Func<FlowableResult<T>> invoke)
    {
        FlowableResult<T> result = invoke.Invoke();
        if (result == null)
            return result;
        this.ResultImage = result.Value;
        //if (this.UseResultImageSource)
        //{
        //    this.InvalidateResultImageSource();
        //    Thread.Sleep(this.PreviewMillisecondsDelay);
        //}
        if (this.ResultPresenter == null)
            this.ResultPresenter = this.CreateResultPresenter();
        return result;
    }

    protected abstract FlowableResult<T> Invoke(IStartVisionNodeData srcImageNodeData, IVisionNodeData from, IFlowableDiagramData diagram);

    public override void Dispose()
    {
        this.Clear();
        base.Dispose();
    }

    protected virtual FlowableResult<T> OK(T mat, string message = "运行成功")
    {
        this.Message = message;
        return new FlowableResult<T>(mat, message) { State = FlowableResultState.OK };
    }

    protected virtual FlowableResult<T> OK(T mat, IResultPresenter resultPresenter, string message = "运行成功")
    {
        this.Message = message;
        this.ResultPresenter = resultPresenter;
        return new FlowableResult<T>(mat, message) { State = FlowableResultState.OK };
    }

    protected virtual FlowableResult<T> Error(T mat, string message = "运行错误")
    {
        this.Message = message;
        return new FlowableResult<T>(mat, message) { State = FlowableResultState.Error };
    }

    protected virtual FlowableResult<T> Continue(T mat, string message = "不满足条件返回")
    {
        this.Message = message;
        return new FlowableResult<T>(mat, message) { State = FlowableResultState.Continue };
    }

    protected virtual async Task<bool?> InvokeFrameMatAsync(T frameMat, bool invokeThis = true)
    {
        IFlowableDiagramData invokeable = this.DiagramData as IFlowableDiagramData;
        Action<IPartData> invoking = x =>
        {
            //OpenCVNodeDataBase data = x.GetContent<OpenCVNodeDataBase>();
            //data.UseInfoLogger = false;
            //data.UseReview = false;
            //data.UseAnimation = false;
            //diagram.Dispatcher.Invoke(() =>
            //{
            //    invokeable?.OnInvokingPart(x);
            //});
        };

        Action<IPartData> invoked = x =>
        {
            if (this.State == FlowableState.Canceling)
                return;
            invokeable?.OnInvokedPart(x);
            //Thread.Sleep(1000);
        };
        if (invokeThis)
            invoking.Invoke(this);
        //this.Clear();
        if (this.ResultImage is IDisposable disposable)
            disposable.Dispose();
        this.ResultImage = (T)frameMat.Clone();
        //this.SrcMat = this.Mat;
        //this.ResultImageSource = frameMat.ToImageSource();
        if (invokeThis)
            invoked?.Invoke(this);
        var allToNodes = this.GetAllToNodeDatas(this.DiagramData).OfType<IFlowableNodeData>();
        allToNodes.GotoState(invokeable, x => FlowableState.Wait);
        IEnumerable<IFlowableNodeData> tos = this.GetToNodeDatas(this.DiagramData).OfType<IFlowableNodeData>();
        foreach (var to in tos)
        {
            IFlowableLinkData linkData = this.GetToLinkDatas(this.DiagramData).OfType<IFlowableLinkData>().Where(x => x.ToNodeID == to.ID)?.FirstOrDefault();
            bool? r = await to.Start(invokeable, linkData);
            if (r == false)
                return false;
        }
        await Task.Delay(this.InvokeMillisecondsDelay);
        return true;
    }

    public virtual IEnumerable<IExpression> GetExpressions(Predicate<object> predicate = null)
    {
        var propertyExpressions = this.GetPropertyInfoExpressions(this.Text, predicate);
        foreach (var propertyExpression in propertyExpressions)
        {
            yield return propertyExpression;
            foreach (var item in propertyExpression.GetVisionAllDefineChildrenExpressions())
                yield return item;
        }
    }
}
