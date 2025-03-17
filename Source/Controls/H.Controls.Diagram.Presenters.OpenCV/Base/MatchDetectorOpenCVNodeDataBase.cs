using H.Services.Common;
using System.Threading.Tasks;
namespace H.Controls.Diagram.Presenters.OpenCV.Base;
public abstract class MatchDetectorOpenCVNodeDataBase : DetectorOpenCVNodeDataBase
{
    private string _templateFilePath;
    [Required]
    [Display(Name = "模板图片", GroupName = "数据")]
    [PropertyItem(typeof(OpenFileDialogPropertyItem))]
    public string TemplateFilePath
    {
        get { return _templateFilePath; }
        set
        {
            _templateFilePath = value;
            RaisePropertyChanged();
        }
    }

    protected override async Task<IFlowableResult> BeforeInvokeAsync(IFlowableLinkData previors, IFlowableDiagramData current)
    {
        if (File.Exists(this.TemplateFilePath) == false)
        {
            var r = await IocMessage.Form?.ShowEdit(this, null, null, x =>
            {
                x.UsePropertyNames = nameof(TemplateFilePath);
            });
            if (r != true)
                return this.Error("未设置模板图片地址");
        }
        return await base.BeforeInvokeAsync(previors, current);
    }
}

