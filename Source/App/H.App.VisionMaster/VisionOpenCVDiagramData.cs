using H.Controls.Diagram.Flowables;
using H.Controls.Diagram.Parts;
using H.Controls.Diagram.Parts.Base;
using H.Controls.Diagram.Presenter.DiagramDatas;
using H.Controls.Diagram.Presenter.NodeDatas;
using H.Controls.Diagram.Presenters.OpenCV;
using H.Controls.Diagram.Presenters.OpenCV.Base;
using H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Basic;
using H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Other;
using H.Extensions.Common;
using H.Mvvm;
using H.Mvvm.ViewModels.Base;
using H.Services.Common;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using OpenCvSharp.WpfExtensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace H.App.VisionMaster;
public class VisionOpenCVDiagramData : OpenCVDiagramData, IVisionOpenCVDiagramData
{
    //public VisionOpenCVDiagramData()
    //{
    //    this.ImageDatas = OpenCVImages.GetImageFiles().Select(x => new ImageData(x)).OfType<IImageData>().ToObservable();
    //    this.SelectedImageData = this.ImageDatas.FirstOrDefault();
    //}
    private ImageSource _resultImageSource;
    [JsonIgnore]
    public ImageSource ResultImageSource
    {
        get { return _resultImageSource; }
        set
        {
            _resultImageSource = value;
            RaisePropertyChanged();
        }
    }

    private ObservableCollection<IVisionMessage> _messages = new ObservableCollection<IVisionMessage>();
    [JsonIgnore]
    public ObservableCollection<IVisionMessage> Messages
    {
        get { return _messages; }
        set
        {
            _messages = value;
            RaisePropertyChanged();
        }
    }

    private IVisionMessage _currentMessage;
    [JsonIgnore]
    public IVisionMessage CurrentMessage
    {
        get { return _currentMessage; }
        set
        {
            _currentMessage = value;
            RaisePropertyChanged();
        }
    }

    public override async Task<bool?> Start()
    {
        this.Messages.Clear();
        this.SelectedImageTabIndex = 1;
        this.CurrentMessage = null;
        bool? result;
        var imageSourceNode = this.Nodes.FirstOrDefault(x => x.GetContent<IVisionImageSourceStartNodeData>() != null);
        if (imageSourceNode != null && imageSourceNode.GetContent() is IVisionImageSourceStartNodeData visionImageSource)
        {
            visionImageSource.FilePath = this.SelectedImageData.FilePath;
            result = await this.InvokeNode(imageSourceNode);
            var rimage = this.Messages.LastOrDefault()?.ResultImageSource;
            this.SelectedImageData.ResultImageSource = rimage;
        }
        else
        {
            result = await base.Start();
        }
        this.LogCurrentMessage();
        return result;
    }

    public void LogCurrentMessage()
    {
        var totalTimeSpan = this.Messages.Sum(x => x.TimeSpan.Ticks);
        var rimage = this.Messages.LastOrDefault()?.ResultImageSource;
        this.CurrentMessage = new VisionMessage()
        {
            TimeSpan = TimeSpan.FromTicks(totalTimeSpan),
            Message = this.Message,
            ResultImageSource = rimage,
        };
    }

    private ObservableCollection<IImageData> _imageDatas = new ObservableCollection<IImageData>();
    public ObservableCollection<IImageData> ImageDatas
    {
        get { return _imageDatas; }
        set
        {
            _imageDatas = value;
            RaisePropertyChanged();
        }
    }

    private IImageData _selectedImageData;
    [JsonIgnore]
    public IImageData SelectedImageData
    {
        get { return _selectedImageData; }
        set
        {
            _selectedImageData = value;
            RaisePropertyChanged();
        }
    }


    public RelayCommand AddImageDataCommand => new RelayCommand(x =>
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory; //设置初始路径
        openFileDialog.Filter = FileExtension.ImageExtensionsFilter; //设置“另存为文件类型”或“文件类型”框中出现的选择内容
        openFileDialog.FilterIndex = 1; //设置默认显示文件类型为Csv文件(*.csv)|*.csv
        openFileDialog.Title = "打开文件"; //获取或设置文件对话框标题
        openFileDialog.RestoreDirectory = true; //设置对话框是否记忆上次打开的目录
        openFileDialog.Multiselect = true;//设置多选
        if (openFileDialog.ShowDialog() != true)
            return;

        foreach (var item in openFileDialog.FileNames)
        {
            this.ImageDatas.Add(new ImageData(item));
        }

    });


    public RelayCommand DeleteImageDataCommand => new RelayCommand(async x =>
    {
        if (this.SelectedImageData == null)
            return;
        await IocMessage.Dialog.ShowDeleteDialog(x =>
         {
             var index = this.ImageDatas.IndexOf(this.SelectedImageData);
             this.ImageDatas.Remove(this.SelectedImageData);
             var find = this.ImageDatas.ElementAtOrDefault(index);
             if (find == null)
                 this.SelectedImageData = this.ImageDatas.FirstOrDefault();
             else
                 this.SelectedImageData = find;
         });

    }, x => this.SelectedImageData != null);


    private bool _useAllImage;
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
    public bool UseAutoSwitch
    {
        get { return _useAutoSwitch; }
        set
        {
            _useAutoSwitch = value;
            RaisePropertyChanged();
        }
    }

    public RelayCommand ClearImageDatasCommand => new RelayCommand(async x =>
    {
        if (this.SelectedImageData == null)
            return;
        await IocMessage.Dialog.ShowDeleteAllDialog(x =>
         {
             this.ImageDatas.Clear();
         });
    }, x => this.ImageDatas.Count > 0);
    public RelayCommand AddImageDatasCommand => new RelayCommand(x =>
    {
        var folderDialog = new CommonOpenFileDialog();
        folderDialog.Title = "请选择一个文件夹";
        folderDialog.IsFolderPicker = true;
        if (folderDialog.ShowDialog() == CommonFileDialogResult.Ok)
        {
            string selectedFolderPath = folderDialog.FileName;
            var images = selectedFolderPath.GetAllImages().Select(x => new ImageData(x));
            this.ImageDatas.AddRange(images);
        }
    });

    [JsonIgnore]
    public RelayCommand ImageFileSelectionChangedCommand => new RelayCommand(l =>
    {
        if (this.SelectedImageData == null)
            return;

        //var s = TypeDescriptor.GetConverter(typeof(ImageSource)).ConvertFromString(this.SelectedImageFile.FilePath) as ImageSource;
        //this.ResultImageSource = this.SelectedImageFile.ToImageEx().GetImageSource();
        this.ResultImageSource = this.SelectedImageData.ResultImageSource;
    });
    protected override IEnumerable<INodeDataGroup> CreateNodeGroups()
    {
        yield return new VisionImageSourceNodeDataGroup();
        var gs = base.CreateNodeGroups();
        foreach (var item in gs)
        {
            yield return item;
        }
    }

    protected override void OnSelectedPartChanged()
    {
        base.OnSelectedPartChanged();

        if (this.SelectedPart is Node node)
        {
            var data = node.GetContent();
            if (data is IImageNodeData imageNodeData)
            {
                this.ResultImageSource = imageNodeData.ImageSource;
            }
            if (data is IFilePathable filePathable)
            {
                this.SelectedImageData = this.ImageDatas.FirstOrDefault(x => x.FilePath == filePathable.FilePath);
            }

            this.SelectedImageTabIndex = 1;
        }
    }

    private int _selectedImageTabIndex;
    [JsonIgnore]
    public int SelectedImageTabIndex
    {
        get { return _selectedImageTabIndex; }
        set
        {
            _selectedImageTabIndex = value;
            RaisePropertyChanged();
        }
    }


    //protected override void OnInvokingPart(Part part)
    //{
    //    base.OnInvokingPart(part);

    //    if (part.GetContent() is IOpenCVNodeData openCVNodeData)
    //    {
    //        this.ImageSource = openCVNodeData?.SrcMat?.ToWriteableBitmap();
    //        var message = new VisionMessage()
    //        {
    //            Index = this.Messages.Count + 1,
    //            Message = "正在执行",
    //            State = openCVNodeData.State
    //        };
    //        if (openCVNodeData is INameable nameable)
    //            message.Type = nameable.Name;
    //        this.Messages.Add(message);
    //    }
    //}

    protected override void OnInvokedPart(Part part)
    {
        base.OnInvokedPart(part);

        if (part.GetContent() is IOpenCVNodeData openCVNodeData)
        {
            this.ResultImageSource = openCVNodeData?.Mat?.ToWriteableBitmap();
            var message = new VisionMessage()
            {
                Index = this.Messages.Count + 1,
                Message = openCVNodeData.Message,
                State = openCVNodeData.State,
                TimeSpan = openCVNodeData.TimeSpan,
                ResultImageSource = openCVNodeData.Mat?.ToWriteableBitmap()
            };
            if (openCVNodeData is INameable nameable)
                message.Type = nameable.Name;
            this.Messages.Add(message);
            this.LogCurrentMessage();
        }
    }

    protected override void OnSerializing()
    {
        base.OnSerializing();
    }

    protected override void OnDeserialized()
    {
        base.OnDeserialized();
        this.SelectedImageData = this.ImageDatas.FirstOrDefault();
    }
}
