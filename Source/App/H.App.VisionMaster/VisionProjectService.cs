using H.Controls.Diagram.Presenter.DiagramDatas.Base;
using H.Iocable;
using H.Modules.Login;
using H.Modules.Project;
using H.Mvvm;
using H.Services.Common;
using H.Services.Serializable;
using Microsoft.Extensions.Options;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.Json.Serialization;

namespace H.App.VisionMaster;

public class VisionProjectService : ProjectServiceBase<VisionProjectItem>, ILoginedSplashLoad
{
    public VisionProjectService(IOptions<ProjectOptions> options) : base(options)
    {

    }

    public override VisionProjectItem Create()
    {
        var n = new VisionProjectItem()
        {
            Path = AppPaths.Instance.Project
        };
        n.InitData();
        return n;
    }
}

public interface IVisionProjectItem : IProjectItem
{
    ObservableCollection<IVisionOpenCVDiagramData> DiagramDatas { get; set; }

    IVisionOpenCVDiagramData SelectedDiagramData { get; set; }
}

public class VisionProjectItem : ProjectItemBase, IVisionProjectItem
{
    public IVisionOpenCVDiagramData CreateDiagramData()
    {
        return new VisionOpenCVDiagramData() { Width = 1000, Height = 1500 };
    }
    private ObservableCollection<IVisionOpenCVDiagramData> _diagramDatas = new ObservableCollection<IVisionOpenCVDiagramData>();
    [JsonIgnore]
    public ObservableCollection<IVisionOpenCVDiagramData> DiagramDatas
    {
        get { return _diagramDatas; }
        set
        {
            _diagramDatas = value;
            RaisePropertyChanged();
        }
    }

    private IVisionOpenCVDiagramData _selectedDiagramData;
    [JsonIgnore]
    public IVisionOpenCVDiagramData SelectedDiagramData
    {
        get { return _selectedDiagramData; }
        set
        {
            _selectedDiagramData = value;
            RaisePropertyChanged();
        }
    }

    public void InitData()
    {
        if (this.DiagramDatas == null || this.DiagramDatas.Count == 0)
            this.DiagramDatas = new ObservableCollection<IVisionOpenCVDiagramData>() { this.CreateDiagramData() };
        this.SelectedDiagramData = this.DiagramDatas?.FirstOrDefault();
    }
    [JsonIgnore]
    public RelayCommand AddDiagramCommand => new RelayCommand(async x =>
    {
        var data = this.CreateDiagramData();
        var r = await IocMessage.Form.ShowEdit(data, null, null, x => x.UseGroupNames = "基础信息,数据");
        if (r != true)
            return;
        this.DiagramDatas.Add(data);
        this.SelectedDiagramData = data;
    });
    [JsonIgnore]
    public RelayCommand DeleteDiagramCommand => new RelayCommand(e =>
    {
        if (this.SelectedDiagramData == null)
            return;
        this.DiagramDatas.Remove(this.SelectedDiagramData);
    }, e => this.SelectedDiagramData != null && this.DiagramDatas.Count > 1);
    [JsonIgnore]
    public RelayCommand SaveDiagramCommand => new RelayCommand(e =>
    {
        if (this.SelectedDiagramData == null)
            return;
    }, e => this.SelectedDiagramData != null);

    [JsonIgnore]
    public RelayCommand SaveAsDiagramTemplateCommand => new RelayCommand(e =>
    {
        if (this.SelectedDiagramData == null)
            return;
    }, e => this.SelectedDiagramData != null);
    [JsonIgnore]
    public RelayCommand DuplicationDiagramCommand => new RelayCommand(e =>
    {
        if (this.SelectedDiagramData == null)
            return;
    }, e => this.SelectedDiagramData != null);


    public override bool Save(out string message)
    {
        message = null;
        this.SaveToFile(this.DiagramDatas);
        return true;
    }

    protected override object GetSaveFileData()
    {
        return base.GetSaveFileData();
    }

    public override bool Load(out string message)
    {
        message = null;
        var path = this.GetFilePath();
        if(this.LoadFile(out ObservableCollection<IVisionOpenCVDiagramData> datas))
        {
            this.DiagramDatas = datas;
            this.InitData();
        }
        return true;
    }
}
