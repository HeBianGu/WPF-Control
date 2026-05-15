// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.Form.PropertyItem.TextPropertyItems;
using H.Mvvm.Commands;
using H.Services.AppPath;
using H.Services.Message;
using H.Services.Message.Dialog;
using H.Services.Message.IODialog;
using System.IO;
using System.Runtime.Serialization;

namespace H.Components.Vision.Base;
[Icon(FontIcons.Design)]
[Display(Name = "绘制比例尺")]
public class DrawScalerLineShapeState : AddRulerLineShapeState
{
    private readonly IScalerNodeData _scalerNodeData;
    public DrawScalerLineShapeState(IScalerNodeData scalerNodeData)
    {
        _scalerNodeData = scalerNodeData;
    }

    private double _WorldDistance;
    [Unit("mm")]
    [PropertyItem(typeof(UnitTextPropertyItem))]
    [Display(Name = "实际长度")]
    [Range(0, double.MaxValue)]
    public double WorldDistance
    {
        get { return _WorldDistance; }
        set
        {
            _WorldDistance = value;
            RaisePropertyChanged();
            this.UpdateScaler();
        }
    }


    private double _PixelDistance;
    [Unit("px")]
    [PropertyItem(typeof(UnitTextPropertyItem))]
    [Display(Name = "像素长度")]
    [ReadOnly(true)]
    public double PixelDistance
    {
        get { return _PixelDistance; }
        set
        {
            _PixelDistance = value;
            RaisePropertyChanged();
            this.UpdateScaler();
        }
    }

    void UpdateScaler()
    {
        if (this.PixelDistance == 0)
        {
            this.Scaler = 1;
            return;
        }
        this.Scaler = this.WorldDistance / this.PixelDistance;
    }

    private double _Scaler;
    [Display(Name = "实际长度/像素长度")]
    [ReadOnly(true)]
    public double Scaler
    {
        get { return _Scaler; }
        set
        {
            _Scaler = value;
            RaisePropertyChanged();
        }
    }

    protected override async void Sumit()
    {
        base.Sumit();
        this.DrawStateShape(this.Shape);
        this.PixelDistance = this.Shape.Length;
        if (this.PixelDistance > 10)
        {
            this.WorldDistance = this.PixelDistance;
            var r = await IocMessage.Form.ShowEdit(this, null, null, x => x.UseCommand = false);
            if (r == true)
            {
                var scaler = this.WorldDistance / this.PixelDistance;
                this._scalerNodeData.Scaler = scaler;
            }
        }
    }
}

public interface IScalerNodeData
{
    int PixelWidth { get; set; }
    /// <summary>
    /// 实际长度/像素长度
    /// </summary>
    double Scaler { get; set; }

    string GetWorldDistance(double px);
}

public abstract class SrcFilesVisionNodeData<T> : StartVisionNodeData<T>, ISrcFilesVisionNodeData<T> where T : class, IVisionImage
{
    private bool _useAllImage = false;
    /// <summary>
    /// 获取或设置是否使用所有图像
    /// </summary>
    public bool UseAllImage
    {
        get { return _useAllImage; }
        set
        {
            _useAllImage = value;
            RaisePropertyChanged();
        }
    }

    private bool _useAutoSwitch;
    /// <summary>
    /// 获取或设置是否自动切换
    /// </summary>
    public bool UseAutoSwitch
    {
        get { return _useAutoSwitch; }
        set
        {
            _useAutoSwitch = value;
            RaisePropertyChanged();
        }
    }

    private string _srcFilePath;
    [Expressionable]
    [Browsable(true)]
    [Display(Name = "当前图片文件", GroupName = VisionPropertyGroupNames.RunParameters)]
    [PropertyItem(typeof(OpenFileDialogPropertyItem))]
    public string SrcFilePath
    {
        get { return _srcFilePath; }
        set
        {
            _srcFilePath = value;
            RaisePropertyChanged();
        }
    }

    private ObservableCollection<string> _srcFilePaths = new ObservableCollection<string>();
    public ObservableCollection<string> SrcFilePaths
    {
        get { return _srcFilePaths; }
        set
        {
            _srcFilePaths = value;
            this.SrcFilePath = value?.FirstOrDefault();
            RaisePropertyChanged();
        }
    }

    /// <summary>
    /// 清除图像数据命令
    /// </summary>
    public RelayCommand ClearImageDatasCommand => new RelayCommand(async x =>
    {
        await IocMessage.Dialog.ShowDeleteAllDialog(x =>
        {
            this.SrcFilePaths.Clear();
        });
    }, x => this.SrcFilePaths != null && this.SrcFilePaths.Count > 0);

    /// <summary>
    /// 添加图像数据集合命令
    /// </summary>
    public RelayCommand AddImageDatasCommand => new RelayCommand(x =>
    {
        this.AddFiles();
    }, x => this != null);

    protected virtual void AddFiles()
    {
        IocMessage.IOFolderDialog.ShowOpenFolderAction(x =>
        {
            string selectedFolderPath = x;
            IEnumerable<string> images = selectedFolderPath.GetAllImages();
            this.SrcFilePaths.AddRange(images);
            if (this.SrcFilePaths.Count == 0)
                this.SrcFilePath = this.SrcFilePaths.FirstOrDefault();
        });
    }

    /// <summary>
    /// 添加图像数据命令
    /// </summary>
    public RelayCommand AddImageDataCommand => new RelayCommand(x =>
    {
        this.AddFile();
    });

    protected virtual void AddFile()
    {
        IocMessage.IOFileDialog.ShowOpenImageFiles(x =>
        {
            foreach (string item in x)
            {
                this.SrcFilePaths.Add(item);
            }
            if (this.SrcFilePaths.Count == 0)
                this.SrcFilePath = this.SrcFilePaths.FirstOrDefault();
        });
    }

    /// <summary>
    /// 删除图像数据命令
    /// </summary>
    public RelayCommand DeleteImageDataCommand => new RelayCommand(async x =>
    {
        await IocMessage.Dialog.ShowDeleteDialog(x =>
        {
            int index = this.SrcFilePaths.IndexOf(this.SrcFilePath);
            this.SrcFilePaths.Remove(this.SrcFilePath);
            string find = this.SrcFilePaths.ElementAtOrDefault(index);
            this.SrcFilePath = find == null ? this.SrcFilePaths.FirstOrDefault() : find;
        });

    });

    public override void LoadDefault()
    {
        base.LoadDefault();
        this.LoadSrcFilePaths();
    }

    protected virtual void LoadSrcFilePaths()
    {
        IEnumerable<string> imagePaths = AppDomianPaths.Assets.GetAllImages();
        this.SrcFilePaths.Clear();
        foreach (string imagePath in imagePaths)
        {
            string relatvePath = Path.GetRelativePath(AppDomain.CurrentDomain.BaseDirectory, imagePath);
            this.SrcFilePaths.Add(relatvePath);
        }
        this.SrcFilePath = this.SrcFilePaths?.FirstOrDefault();
    }

    protected override async Task<IFlowableResult> BeforeInvokeAsync(IFlowableLinkData previors, IFlowableDiagramData diagram)
    {
        if (File.Exists(this.SrcFilePath) == false)
        {
            bool? r = await IocMessage.Form?.ShowEdit(this, x => x.Title = $"{this.Name}:请先选择文件", null, x =>
            {
                x.UsePropertyNames = nameof(SrcFilePath);
            });
            if (r != true)
                return this.Error("未设置源文件地址");
        }
        return await base.BeforeInvokeAsync(previors, diagram);
    }

    protected override void OnDeserializing(StreamingContext context)
    {
        base.OnDeserializing(context);
        this.SrcFilePaths.Clear();
        this.SrcFilePath = null;
    }

    public virtual bool IsValid(out string message)
    {
        if (this?.SrcFilePaths == null || this.SrcFilePaths.Count == 0)
        {
            message = "请选择数据源中的图片";
            //await IocMessage.ShowDialogMessage("请选择数据源中的图片");
            return false;
        }
        if (this.SrcFilePath == null)
        {
            this.SrcFilePath = this.SrcFilePaths?.FirstOrDefault();
        }
        this.SrcFilePath = this.SrcFilePath;
        message = null;
        return this.SrcFilePath != null;
    }
}

