global using H.Controls.Diagram.Presenter.DiagramTemplates;

namespace H.Controls.Diagram.Presenter.NodeDatas.Card;

[Display(Name = "基本流程图", GroupName = "流程图", Order = 0)]
public class FlowableDiagramTemplateNodeData : TitleCardNodeData
{
    public FlowableDiagramTemplateNodeData()
    {
        this.Title = "模板";
    }
    public FlowableDiagramTemplateNodeData(DiagramTemplate template) : this()
    {
        this.Template = template;
        this.Text = template.Name;
        //this.Name = template.Name;
        //this.FilePath = template.FilePath;
    }

    private DiagramTemplate _template;
    [XmlIgnore]
    public DiagramTemplate Template
    {
        get { return _template; }
        set
        {
            _template = value;
            RaisePropertyChanged();
        }
    }

    private string _filePath;
    [XmlIgnore]
    public string FilePath
    {
        get { return _filePath; }
        set
        {
            _filePath = value;
            RaisePropertyChanged();
        }
    }

    [XmlIgnore]
    [Display(Name = "查看模板", GroupName = "操作")]
    public DisplayCommand ShowTemplateCommand => new DisplayCommand(e =>
    {
        //    await IocMessage.Dialog.Show(this.Template, null, this.Name, x =>
        //    {
        //        x.HorizontalAlignment = HorizontalAlignment.Stretch;
        //        x.VerticalAlignment = VerticalAlignment.Stretch;
        //        x.Margin = new Thickness(20);
        //        x.Padding = new Thickness(20);
        //    });
    });

    public override async Task<IFlowableResult> InvokeAsync(IFlowableLinkData previors, IFlowableDiagramData diagram)
    {
        if (this.Template.Diagram is IFlowableDiagramData flowable)
        {
            bool? r = await flowable.Start();
            this.Message = flowable.Message;
            return r == true ? this.OK(flowable.Message) : this.Error(flowable.Message);
        }

        return this.Error("没有找到对应流程信息");
    }
}
