

using H.Mvvm;

namespace H.Controls.Diagram.Extension
{
    public class AutoTestResult : SelectBindable<ats_dd_result>
    {
        public AutoTestResult() : base(new ats_dd_result())
        {

        }
        public AutoTestResult(ats_dd_result model) : base(model)
        {

        }

        private IDiagram _diagram;
        /// <summary> 说明  </summary>
        public IDiagram Diagram
        {
            get { return _diagram; }
            set
            {
                _diagram = value;
                RaisePropertyChanged();
            }
        }

    }
}
