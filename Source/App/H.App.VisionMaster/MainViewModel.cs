using H.Controls.Diagram;
using H.Controls.Diagram.Flowables;
using H.Controls.Diagram.Parts;
using H.Controls.Diagram.Presenter.DiagramDatas;
using H.Controls.Diagram.Presenter.DiagramDatas.Base;
using H.Controls.Diagram.Presenter.NodeDatas;
using H.Controls.Diagram.Presenters.OpenCV;
using H.Controls.Diagram.Presenters.OpenCV.NodeDataGroups;
using H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Other;
using H.Extensions.Common;
using H.Iocable;
using H.Mvvm;
using H.Mvvm.ViewModels.Base;
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
        //this.DiagramDatas.Add(this.CreateDiagramData());
        //this.SelectedDiagramData = this.DiagramDatas.FirstOrDefault();

        IocProject.Instance.CurrentChanged = (o, n) =>
        {
            //if (n == null)
            //    return;
            //this.DiagramDatas = (n as VisionProjectItem).DiagramDatas;
            //this.SelectedDiagramData = this.DiagramDatas.FirstOrDefault();
            if (n is IVisionProjectItem vision)
                this.CurrentProject = vision;
        };
    }

    private IVisionProjectItem _currentProject;
    public IVisionProjectItem CurrentProject
    {
        get { return _currentProject; }
        set
        {
            _currentProject = value;
            RaisePropertyChanged();
        }
    }


    public IVisionOpenCVDiagramData CreateDiagramData()
    {
        return new VisionOpenCVDiagramData() { Width = 1000, Height = 1500 };
    }

    //private IVisionOpenCVDiagramData _selectedDiagramData;
    //public IVisionOpenCVDiagramData SelectedDiagramData
    //{
    //    get { return _selectedDiagramData; }
    //    set
    //    {
    //        _selectedDiagramData = value;
    //        RaisePropertyChanged();
    //    }
    //}

    //private ObservableCollection<IVisionOpenCVDiagramData> _diagramDatas = new ObservableCollection<IVisionOpenCVDiagramData>();
    //public ObservableCollection<IVisionOpenCVDiagramData> DiagramDatas
    //{
    //    get { return _diagramDatas; }
    //    set
    //    {
    //        _diagramDatas = value;
    //        RaisePropertyChanged();
    //    }
    //}

    public RelayCommand NewProjectCommand => new RelayCommand(async x =>
    {
        var p = Ioc.GetService<IProjectViewPresenter>();
        await p.NewProject();
    });

    public RelayCommand SaveProjectCommand => new RelayCommand(async x =>
    {
        string message = null;
        if (this.CurrentProject == null)
            return;
        var r = await IocMessage.Dialog.ShowWait(x =>
        {
            return this.CurrentProject?.Save(out message);

        }, x => x.Title = $"正在保存工程<{this.CurrentProject.Title}>...");
        if (r == false && !string.IsNullOrEmpty(message))
            await IocMessage.ShowDialogMessage(message);
    });

    public RelayCommand ShowProjectsCommand => new RelayCommand(async x =>
    {
        var p = Ioc.GetService<IProjectViewPresenter>();
        await p.ShowProjectList();
    });


}
