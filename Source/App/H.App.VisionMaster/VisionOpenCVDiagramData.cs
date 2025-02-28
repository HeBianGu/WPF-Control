using H.Controls.Diagram.Datas;
using H.Controls.Diagram.Flowables;
using H.Controls.Diagram.Parts;
using H.Controls.Diagram.Parts.Base;
using H.Controls.Diagram.Presenter.DiagramDatas;
using H.Controls.Diagram.Presenter.DiagramDatas.Base;
using H.Controls.Diagram.Presenter.NodeDatas;
using H.Controls.Diagram.Presenters.OpenCV;
using H.Controls.Diagram.Presenters.OpenCV.Base;
using H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Basic;
using H.Extensions.Common;
using H.Extensions.FontIcon;
using H.Mvvm;
using OpenCvSharp.WpfExtensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace H.App.VisionMaster;

public interface IVisionImageSourceStartNodeData
{
    string FilePath { get; set; }
}

[Icon(FontIcons.ImageExport)]
[Display(Name = "图像源")]
public class VisionImageSourceStartNodeData : StartNodeDataBase, IVisionImageSourceStartNodeData
{

}

[Icon(FontIcons.ImageExport)]
public class VisionImageSourceNodeDataGroup : NodeDataGroupBase
{
    public VisionImageSourceNodeDataGroup()
    {
        this.Name = "图像源";
        this.Order = 0;
    }

    protected override IEnumerable<INodeData> CreateNodeDatas()
    {
        yield return new VisionImageSourceStartNodeData();
    }
}

public class VisionOpenCVDiagramData : OpenCVDiagramData, IVisionOpenCVDiagramData
{

    public VisionOpenCVDiagramData()
    {
        this.ImageFiles = OpenCVImages.GetImageFiles().ToObservable();
        this.SelectedImageFile = this.ImageFiles.FirstOrDefault();

    }
    private ImageSource _nodeImageSource;
    public ImageSource NodeImageSource
    {
        get { return _nodeImageSource; }
        set
        {
            _nodeImageSource = value;
            RaisePropertyChanged();
        }
    }

    private ObservableCollection<IVisionMessage> _messages = new ObservableCollection<IVisionMessage>();
    public ObservableCollection<IVisionMessage> Messages
    {
        get { return _messages; }
        set
        {
            _messages = value;
            RaisePropertyChanged();
        }
    }

    public override async Task<bool?> Start()
    {
        this.Messages.Clear();
        this.SelectedImageTabIndex = 1;

        var imageSourceNode = this.Nodes.FirstOrDefault(x => x.GetContent<IVisionImageSourceStartNodeData>() != null);
        if (imageSourceNode != null && imageSourceNode.GetContent() is IVisionImageSourceStartNodeData visionImageSource)
        {
            visionImageSource.FilePath = this.SelectedImageFile;
            return await this.InvokeNode(imageSourceNode);
        }
        else
        {
            return await base.Start();
        }
    }


    private ObservableCollection<string> _imageFiles = new ObservableCollection<string>();
    public ObservableCollection<string> ImageFiles
    {
        get { return _imageFiles; }
        set
        {
            _imageFiles = value;
            RaisePropertyChanged();
        }
    }

    private string _selectedImageFile;
    public string SelectedImageFile
    {
        get { return _selectedImageFile; }
        set
        {
            _selectedImageFile = value;
            RaisePropertyChanged();
        }
    }

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

        if(this.SelectedPart is Node node&&node.GetContent() is IImageNodeData imageNodeData)
        {
            this.NodeImageSource = imageNodeData.ImageSource;
        }
    }

    private int _selectedImageTabIndex;
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
            this.NodeImageSource = openCVNodeData?.Mat?.ToWriteableBitmap();
            var message = new VisionMessage()
            {
                Index = this.Messages.Count + 1,
                Message = openCVNodeData.Message,
                State = openCVNodeData.State,
                TimeSpan = openCVNodeData.TimeSpan
            };
            if (openCVNodeData is INameable nameable)
                message.Type = nameable.Name;
            this.Messages.Add(message);
        }
    }
}
