using H.Providers.Mvvm;
using Quartz;
using System.Collections.ObjectModel;

namespace H.Controls.ScheduleBox
{
    public class JobDetail : SelectViewModel<IJobDetail>
    {
        public JobDetail(IJobDetail jobDetail) : base(jobDetail)
        {

        }

        private ObservableCollection<ITrigger> _triggers = new ObservableCollection<ITrigger>();
        /// <summary> 说明  </summary>
        public ObservableCollection<ITrigger> Triggers
        {
            get { return _triggers; }
            set
            {
                _triggers = value;
                RaisePropertyChanged();
            }
        }
    }
}
