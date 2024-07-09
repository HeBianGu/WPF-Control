//using H.Mvvm;
//using Quartz;
//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Linq;
//using System.Windows;
//using System.Windows.Controls;
//using static Quartz.Logging.OperationName;

//namespace H.Controls.ScheduleBox
//{
//    public class JobDetail : SelectViewModel<IJobDetail>
//    {
//        public JobDetail(IJobDetail jobDetail) : base(jobDetail)
//        {
//            this.Job = Activator.CreateInstance(jobDetail.JobType) as IJob;
//        }

//        private ObservableCollection<ITrigger> _triggers = new ObservableCollection<ITrigger>();
//        /// <summary> 说明  </summary>
//        public ObservableCollection<ITrigger> Triggers
//        {
//            get { return _triggers; }
//            set
//            {
//                _triggers = value;
//                RaisePropertyChanged();
//            }
//        }


//        private IJob _job;
//        /// <summary> 说明  </summary>
//        public IJob Job
//        {
//            get { return _job; }
//            set
//            {
//                _job = value;
//                RaisePropertyChanged();
//            }
//        }
//    }

//}
