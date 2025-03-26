using H.Presenters.Common;
using H.Services.Common;
using H.Mvvm;
using Quartz;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;
using H.Mvvm.ViewModels.Base;
using H.Mvvm.Commands;
using H.Services.Message;

namespace H.Controls.ScheduleBox
{

    public abstract class ScheduleJobBase : DisplayBindableBase, IScheduleJob, IJob
    {
        public ScheduleJobBase()
        {
            this.TriggerSource.Add(new DataTimeTrigger());
            this.TriggerSource.Add(new RepeatTrigger());
            this.TriggerSource.Add(new DailyTimeIntervalTrigger());
            this.TriggerSource.Add(new CalendarIntervalTrigger());

        }
        private string _key = Guid.NewGuid().ToString();
        [Display(Name = "唯一标识")]
        public string Key
        {
            get { return _key; }
            set
            {
                _key = value;
                RaisePropertyChanged();
            }
        }

        private IJobDetail CreateDetail()
        {
            JobDataMap map = new JobDataMap(1);
            map.Add("this", this);
            return JobBuilder.Create(this.GetType())
                .WithIdentity(this.Key, this.GroupName)
                .UsingJobData(map)
                .Build();
        }

        private ObservableCollection<IScheduleTrigger> _triggers = new ObservableCollection<IScheduleTrigger>();
        public ObservableCollection<IScheduleTrigger> Triggers
        {
            get { return _triggers; }
            set
            {
                _triggers = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<IScheduleTrigger> TriggerSource { get; } = new ObservableCollection<IScheduleTrigger>();

        public void Schedule(IScheduler scheduler)
        {
            IJobDetail job = this.CreateDetail();
            //foreach (var t in this.Triggers)
            //{
            //    ITrigger trigger = t.Build(this);
            //    scheduler.ScheduleJob(job, trigger).Wait();
            //}

            IReadOnlyCollection<ITrigger> triggers = this.Triggers.Select(x => x.Build(this)).ToList().AsReadOnly();
            scheduler.ScheduleJob(job, triggers, true).Wait();

        }

        public RelayCommand AddTriggerCommand => new RelayCommand(async l =>
        {
            var source = this.TriggerSource;
            var selectedItem = source?.FirstOrDefault();
            ListBoxPresenter selectItemPresenter = new ListBoxPresenter(source, selectedItem);
            selectItemPresenter.DisplayMemberPath = "Name";
            var r = await IocMessage.Dialog.Show(selectItemPresenter);
            if (r == false)
                return;
            if (selectItemPresenter.SelectedItem is IScheduleTrigger trigger)
                this.Triggers.Add(trigger);
        });


        public RelayCommand DeleteTriggerCommand => new RelayCommand(l =>
        {
            if (l is IScheduleTrigger scheduleTrigger)
            {
                this.Triggers.Remove(scheduleTrigger);
            }
        });
        [System.Text.Json.Serialization.JsonIgnore]
        
        [System.Xml.Serialization.XmlIgnore]
        [Browsable(true)]
        [Display(Name = "任务分组")]
        public override string GroupName { get => base.GroupName; set => base.GroupName = value; }


        private DateTime? _nextFireTime;
        public DateTime? NextFireTime
        {
            get { return _nextFireTime; }
            set
            {
                _nextFireTime = value;
                RaisePropertyChanged();
            }
        }


        private bool _isRunning;
        public bool IsRunning
        {
            get { return _isRunning; }
            set
            {
                _isRunning = value;
                RaisePropertyChanged();
            }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand EditCommand => new RelayCommand(l =>
        {
            IocMessage.Form.ShowEdit(this);
        });

        public abstract Task Execute(IJobExecutionContext context);
    }
}
