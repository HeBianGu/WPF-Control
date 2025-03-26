using H.Common.Attributes;
using H.Extensions.Attach;
namespace H.Modules.Guide;

[Icon("\xEC92")]
[Display(Name = "功能列表", Description = "显示版本功能列表")]
public class GuideTreePresenter : DisplayBindableBase
{
    private readonly GuideTree _guideTree;
    public GuideTreePresenter(GuideTree guideTree)
    {
        this._guideTree = guideTree;
        this.RefreshData();
    }

    public GuideTree GuideTree => this._guideTree;

    private ObservableCollection<GuideData> _guideDatas = new ObservableCollection<GuideData>();
    public ObservableCollection<GuideData> GuideDatas
    {
        get { return _guideDatas; }
        set
        {
            _guideDatas = value;
            RaisePropertyChanged("GuideDatas");
        }
    }


    public void RefreshData()
    {
        this.GuideDatas = this.CreateGuideDatas().ToObservable();
    }

    public IEnumerable<GuideData> CreateGuideDatas()
    {
        var nodes = this._guideTree.GetGuideTreeNodes();
        foreach (var node in nodes)
        {
            GuideData guideData = new GuideData();
            guideData.Tilte = Cattach.GetGuideTitle(node.Element);
            guideData.Icon = Cattach.GetGuideIcon(node.Element);
            guideData.Data = Cattach.GetGuideData(node.Element);
            guideData.DataTemplate = Cattach.GetGuideDataTemplate(node.Element);
            guideData.Version = Cattach.GetGuideAssemblyVersion(node.Element) ?? "1.0.0.0";
            guideData.Element = node.Element;
            yield return guideData;
        }
    }
}

public class GuideData : BindableBase
{
    public object Tilte { get; set; }

    public string Icon { get; set; }

    public object Data { get; set; }

    public DataTemplate DataTemplate { get; set; }

    public string Version { get; set; }

    public UIElement Element { get; set; }
}
