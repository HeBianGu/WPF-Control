using H.Controls.Diagram.Presenter.DiagramDatas.Base;
using H.Mvvm;
using H.Mvvm.ViewModels;

namespace H.Controls.Diagram.Extension.AutoTest;

public class AutoTestResult : SelectBindable<ats_dd_result>
{
    public AutoTestResult() : base(new ats_dd_result())
    {

    }
    public AutoTestResult(ats_dd_result model) : base(model)
    {

    }

    private IDiagramData _diagram;
    public IDiagramData Diagram
    {
        get { return _diagram; }
        set
        {
            _diagram = value;
            RaisePropertyChanged();
        }
    }

}
