// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.ShapeBox.Shapes.Base;
using System.Xml.Serialization;

namespace H.Components.VisionDiagram.NodeDatas;
public interface IResultImageSourceNodeData : IDiagramableNodeData
{
    ImageSource ResultImageSource { get; }
    ObservableCollection<IShape> ResultShapes { get; set; }
}

public abstract class ResultImageSourceNodeDataBase : WaitFromVisionNodeData, IResultImageSourceNodeData
{
    //private bool _useResultImageSource = true;
    //[JsonIgnore]
    //[Browsable(false)]
    //public bool UseResultImageSource
    //{
    //    get { return _useResultImageSource; }
    //    set
    //    {
    //        _useResultImageSource = value;
    //        RaisePropertyChanged();
    //    }
    //}

    private ImageSource _resultImageSource;
    [JsonIgnore]
    [Browsable(false)]
    [XmlIgnore]
    public ImageSource ResultImageSource
    {
        get
        {
            if (this._resultImageSource != null)
                return this._resultImageSource;
            this._resultImageSource = this.CreateImageSource();
            return _resultImageSource;
        }
    }

    protected void InvalidateResultImageSource()
    {
        this._resultImageSource = null;
    }

    protected abstract ImageSource CreateImageSource();

    private ObservableCollection<IShape> _resultShapes = new ObservableCollection<IShape>();
    public ObservableCollection<IShape> ResultShapes
    {
        get { return _resultShapes; }
        set
        {
            _resultShapes = value;
            RaisePropertyChanged();
        }
    }

    public override void Clear()
    {
        this.ResultShapes.Clear();
        this.InvalidateResultImageSource();
        base.Clear();
    }
}
