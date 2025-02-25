namespace H.Controls.Diagram.Extension.AutoTest;

//public class AutoTestResultPresenter : RepositoryPropertyChangedPresenter<ats_dd_result>
//{
//    private IDiagram _diagram;
//    /// <summary> 说明  </summary>
//    public IDiagram Diagram
//    {
//        get { return _diagram; }
//        set
//        {
//            _diagram = value;
//            RaisePropertyChanged();
//        }
//    }

//    public AutoTestResultPresenter(IDiagram diagram)
//    {
//        this.Diagram = diagram;
//        this.UseAdd = false;
//        this.UseEdit = false;

//    }
//    protected override async Task View(object obj)
//    {
//        if (obj is ats_dd_result model)
//        {
//            AutoTestResult result = new AutoTestResult(model);
//            result.Diagram = DiagramBase.CreateFromXml(model.Diagram, Diagram.GetType());
//            await IocMessage.Presenter.ShowClose(result, null, model.BatchCode, x =>
//            {
//                x.Margin = new Thickness(20);
//                x.Padding = new Thickness(20);
//            });
//        }
//    }
//}
