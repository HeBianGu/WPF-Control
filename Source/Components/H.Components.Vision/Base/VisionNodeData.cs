// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.Diagram.Presenter.Extensions;

namespace H.Components.Vision.Base;
public abstract class VisionNodeData<T> : DemoNodeDataBase, IVisionNodeData<T> where T : IVisionImage
{
    ~VisionNodeData()
    {
        this.Dispose();
    }

    [JsonIgnore]
    [Expressionable]
    [Display(Name = "图像结果", GroupName = VisionPropertyGroupNames.ResultParameters, Description = "当前流程运行完返回的图像结果")]
    public virtual T ResultImage { get; set; }

    private int _InvokeTotal;
    [ReadOnly(true)]
    [Expressionable]
    [Display(Name = "运行次数", GroupName = VisionPropertyGroupNames.ResultParameters, Description = "结果参数，此结果可应用再条件分支等作为判断参数")]
    public int InvokeTotal
    {
        get { return _InvokeTotal; }
        set
        {
            _InvokeTotal = value;
            RaisePropertyChanged();
        }
    }

    public override IFlowableResult Invoke(IFlowableLinkData previors, IFlowableDiagramData diagram)
    {
        this.InvokeTotal++;
        base.Invoke(previors, diagram);
        IStartVisionNodeData<T> srcData = diagram.GetStartNodeDatas().OfType<IStartVisionNodeData<T>>().FirstOrDefault();
        IVisionNodeData<T> fromData = this.GetFromNodeData<IVisionNodeData<T>>(diagram, previors);
        return this.InvokeAction(() => this.Invoke(srcData, fromData ?? srcData, this.DiagramData as IFlowableDiagramData));
    }

    private bool _lic;
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
        ISrcVisionNodeData<T> srcData = this.DiagramData.GetStartNodeDatas().OfType<ISrcVisionNodeData<T>>().FirstOrDefault();
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
        this.ResultImage?.Dispose();
        this.ResultImage = default(T);
        base.Clear();
    }

    protected virtual FlowableResult<T> InvokeAction(Func<FlowableResult<T>> invoke)
    {
        FlowableResult<T> result = invoke.Invoke();
        //if (this.Image != null && this.Image.Equals(result.Value))
        //    this.Image?.Dispose();
        if (result == null)
            return result;
        this.ResultImage = result.Value;
        if (this.UseResultImageSource)
        {
            this.ResultImageSource = result.Value?.ToImageSource();
            Thread.Sleep(this.PreviewMillisecondsDelay);
        }
        if (this.ResultPresenter == null)
            this.ResultPresenter = this.CreateResultPresenter();
        return result;
    }

    protected abstract FlowableResult<T> Invoke(IStartVisionNodeData<T> srcImageNodeData, IVisionNodeData<T> from, IFlowableDiagramData diagram);

    //protected abstract ImageSource ToImageSource(T image);

    public override void Dispose()
    {
        base.Dispose();
        this.ResultImage?.Dispose();
        //foreach (IVisionResultImage<T> item in this.ResultImages)
        //{
        //    item.Image?.Dispose();
        //}
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
        this.ResultImageSource = frameMat.ToImageSource();
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
}

//public class VisionLicenseViewPresenter : LicenseViewPresenter
//{
//    public VisionLicenseViewPresenter()
//    {
//        this.Module = typeof(VisionLicenseService).Name;
//        this.UseModule = false;

//    }
//    protected override ILicenseService LicenseService => VisionLicenseService.Vision;
//}

//public class VisionLicenseService : LicenseService
//{
//    public VisionLicenseService()
//    {
//        this.UseTrial = false;
//        this.TrialEndTime = DateTime.Now.AddDays(1);
//    }

//    private static VisionLicenseService _vision;
//    public static VisionLicenseService Vision
//    {
//        get
//        {
//            if (_vision == null)
//            {
//                _vision = new VisionLicenseService();
//                //_vision.ClearFiles();
//                _vision.Load(out string message);
//            }
//            return _vision;
//        }
//    }
//    public override LicenseOption IsVail(out string error)
//    {
//        return this.IsVail(this.GetType().Name, out error);
//    }

//    protected override void OnTrial()
//    {
//        base.OnTrial();
//        this.Save(out string message);
//    }
//    public override bool Save(out string message)
//    {
//        foreach (var item in this.GetFilePaths())
//        {
//            this.GetSerializerService()?.Save(item, this);
//        }
//        return base.Save(out message);
//    }

//    public override bool Load(out string message)
//    {
//        foreach (var item in GetFilePaths().Reverse())
//        {
//            if (File.Exists(item))
//            {
//                base.Load(item);
//                return this.Save(out message);
//            }
//        }
//        message = "未找到许可配置文件";
//        return false;
//    }

//    private void ClearFiles()
//    {
//        foreach (var item in this.GetFilePaths())
//        {
//            if (File.Exists(item))
//            {
//                try
//                {
//                    File.Delete(item);
//                }
//                catch { }
//            }
//        }
//    }

//    protected override string GetLicFile(string module)
//    {
//        return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "License", module, "license.lic");
//    }


//    private IEnumerable<string> GetFilePaths()
//    {
//        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
//        yield return this.GetDefaultPath();
//        yield return System.IO.Path.Combine(Path.GetTempPath(), "Microsoft.Extensions.Xmlable.dll.config");
//        if (AppPaths.Instance != null)
//        {
//            yield return System.IO.Path.Combine(AppPaths.Instance.Setting, this.GetType().Name + ".json");
//            yield return System.IO.Path.Combine(AppPaths.Instance.Cache, "nuget-clear.bat");
//            yield return System.IO.Path.Combine(AppPaths.Instance.Config, "nuget-push.bat");
//            yield return System.IO.Path.Combine(AppPaths.Instance.UserLog, "nuget.bat");
//        }
//        string userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
//        yield return Path.Combine(userProfile, ".cache", "cache_1024.bat");
//        yield return Path.Combine(userProfile, ".dotnet", "8.0.3.2025.bat");
//    }

//    protected override ISerializerService GetSerializerService()
//    {
//        return new VisionTextJsonSerializerService();
//    }
//}

//public class ShowVisionLicenseCommand : ShowLicenseCommand
//{
//    public override async void Execute(object parameter)
//    {
//        var option = VisionLicenseService.Vision.IsVail(this.Module, out string error);
//        if (option == null)
//        {
//            var r = await IocMessage.Dialog.Show(error);
//            if (r != true)
//                return;
//        }

//        var p = this.CreateLicenseViewPresenter();
//        await IocMessage.Dialog.Show(p, x =>
//        {
//            x.Title = this.Name;
//            x.DialogButton = DialogButton.None;
//        });
//    }

//    protected override ILicenseViewPresenter CreateLicenseViewPresenter()
//    {
//        var r = new VisionLicenseViewPresenter();
//        if (!string.IsNullOrEmpty(this.Module))
//            r.Module = this.Module;
//        r.UseModule = false;
//        return r;
//    }
//}

//public class VisionTextJsonSerializerService : TextJsonSerializerService
//{
//    public override string SerializeObject<T>(T t)
//    {
//        return base.SerializeObject(t).EncryptAES();
//    }

//    public override object DeserializeObject(string txt, Type type)
//    {
//        return base.DeserializeObject(txt.DecryptAES(), type);
//    }
//}

