using H.Controls.Diagram.Presenter.Diagrams;
using H.Mvvm;

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
