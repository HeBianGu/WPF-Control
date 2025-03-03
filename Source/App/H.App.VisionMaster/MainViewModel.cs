using H.Controls.Diagram;
using H.Controls.Diagram.Flowables;
using H.Controls.Diagram.Parts;
using H.Controls.Diagram.Presenter.DiagramDatas;
using H.Controls.Diagram.Presenter.DiagramDatas.Base;
using H.Controls.Diagram.Presenter.NodeDatas;
using H.Controls.Diagram.Presenters.OpenCV;
using H.Controls.Diagram.Presenters.OpenCV.NodeDataGroups;
using H.Extensions.Common;
using H.Mvvm;
using H.Services.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace H.App.VisionMaster;

public class MainViewModel : DisplayBindableBase
{
    public MainViewModel()
    {
        //this.NodeDataGroups = this.CreateNodeDataGroups().ToObservable();
        this.DiagramDatas.Add(this.CreateDiagramData());
        this.SelectedDiagramData = this.DiagramDatas.FirstOrDefault();
    }

    public IVisionOpenCVDiagramData CreateDiagramData()
    {
        return new VisionOpenCVDiagramData() { Width = 1000, Height = 1500 };
    }

    //public IEnumerable<INodeDataGroup> CreateNodeDataGroups()
    //{
    //    return typeof(BasicDataGroup).Assembly.GetInstances<INodeDataGroup>().OrderBy(x => x.Order);
    //}

    //private ObservableCollection<INodeDataGroup> _nodeDataGroups = new ObservableCollection<INodeDataGroup>();
    //public ObservableCollection<INodeDataGroup> NodeDataGroups
    //{
    //    get { return _nodeDataGroups; }
    //    set
    //    {
    //        _nodeDataGroups = value;
    //        RaisePropertyChanged();
    //    }
    //}

    private IVisionOpenCVDiagramData _selectedDiagramData;
    public IVisionOpenCVDiagramData SelectedDiagramData
    {
        get { return _selectedDiagramData; }
        set
        {
            _selectedDiagramData = value;
            RaisePropertyChanged();
        }
    }


    private ObservableCollection<IVisionOpenCVDiagramData> _diagramDatas = new ObservableCollection<IVisionOpenCVDiagramData>();
    public ObservableCollection<IVisionOpenCVDiagramData> DiagramDatas
    {
        get { return _diagramDatas; }
        set
        {
            _diagramDatas = value;
            RaisePropertyChanged();
        }
    }

    public RelayCommand AddDiagramCommand => new RelayCommand(async (s, e) =>
    {
        var data = this.CreateDiagramData();
        var r = await IocMessage.Form.ShowEdit(data, null, null, x => x.UseGroupNames = "基础信息,数据");
        if (r != true)
            return;
        this.DiagramDatas.Add(data);
        this.SelectedDiagramData = data;
    });

    public RelayCommand DeleteDiagramCommand => new RelayCommand((s, e) =>
    {
        if (this.SelectedDiagramData == null)
            return;
        this.DiagramDatas.Remove(this.SelectedDiagramData);
    }, (s, e) => this.SelectedDiagramData != null && this.DiagramDatas.Count > 1);

    public RelayCommand SaveDiagramCommand => new RelayCommand((s, e) =>
    {
        if (this.SelectedDiagramData == null)
            return;
    }, (s, e) => this.SelectedDiagramData != null);

    public RelayCommand SaveAsDiagramTemplateCommand => new RelayCommand((s, e) =>
    {
        if (this.SelectedDiagramData == null)
            return;
    }, (s, e) => this.SelectedDiagramData != null);

    public RelayCommand DuplicationDiagramCommand => new RelayCommand((s, e) =>
    {
        if (this.SelectedDiagramData == null)
            return;
    }, (s, e) => this.SelectedDiagramData != null);
}
