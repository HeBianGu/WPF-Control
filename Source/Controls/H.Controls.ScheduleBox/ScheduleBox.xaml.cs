using H.Presenters.Common;
using H.Services.Common;
using H.Services.Message;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace H.Controls.ScheduleBox
{
    public partial class ScheduleBox : ItemsControl
    {
        static ScheduleBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ScheduleBox), new FrameworkPropertyMetadata(typeof(ScheduleBox)));
        }

        private ObservableCollection<IScheduleJob> _scheduleJobs = new ObservableCollection<IScheduleJob>();
        public ScheduleBox()
        {
            {
                CommandBinding binding = new CommandBinding(ScheduleCommands.Start);
                binding.Executed += (l, k) =>
                {
                    //IocSchedule.Instance.Start();
                    this.Start();
                };
                binding.CanExecute += (l, k) =>
                {
                    k.CanExecute = _scheduler == null || !_scheduler.IsStarted || _scheduler.IsShutdown;
                };
                this.CommandBindings.Add(binding);
            }

            {
                CommandBinding binding = new CommandBinding(ScheduleCommands.Stop);
                binding.Executed += (l, k) =>
                {
                    this.Stop();
                };
                binding.CanExecute += (l, k) =>
                {
                    k.CanExecute = _scheduler != null && _scheduler.IsStarted && !_scheduler.IsShutdown;
                };
                this.CommandBindings.Add(binding);
            }

            {
                CommandBinding binding = new CommandBinding(ScheduleCommands.Add);
                binding.Executed += async (l, k) =>
                {
                    var source = this.JobsSource;
                    var selectedItem = source?.FirstOrDefault();
                    ListBoxPresenter selectItemPresenter = new ListBoxPresenter(source, selectedItem);
                    selectItemPresenter.DisplayMemberPath = "Name";
                    var r = await IocMessage.Dialog.Show(selectItemPresenter);
                    if (r == false)
                        return;
                    if (selectItemPresenter.SelectedItem is IScheduleJob job)
                        _scheduleJobs.Add(job);
                };
                binding.CanExecute += (l, k) =>
                {
                    k.CanExecute = _scheduler == null || _scheduler.IsShutdown;
                };
                this.CommandBindings.Add(binding);
            }


            {
                CommandBinding binding = new CommandBinding(ScheduleCommands.Edit);
                binding.Executed += (l, k) =>
                {
                    if (k.Parameter is IScheduleJob job)
                        IocMessage.Dialog.Show(job);
                };
                binding.CanExecute += (l, k) =>
                {
                    k.CanExecute = _scheduler == null || _scheduler.IsShutdown;
                };
                this.CommandBindings.Add(binding);
            }

            {
                CommandBinding binding = new CommandBinding(ScheduleCommands.Delete);
                binding.Executed += (l, k) =>
                {
                    if (k.Parameter is IScheduleJob job)
                        this._scheduleJobs.Remove(job);
                };
                binding.CanExecute += (l, k) =>
                {
                    k.CanExecute = _scheduler == null || _scheduler.IsShutdown;
                };
                this.CommandBindings.Add(binding);
            }

            //IocSchedule.Instance.LogMessaged += x =>
            //{
            //    Application.Current.Dispatcher.Invoke(() =>
            //    {
            //        this.AddMessage(x);
            //    });

            //};
            this.ItemsSource = this._scheduleJobs;
        }

        public ObservableCollection<string> Messages
        {
            get { return (ObservableCollection<string>)GetValue(MessagesProperty); }
            set { SetValue(MessagesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MessagesProperty =
            DependencyProperty.Register("Messages", typeof(ObservableCollection<string>), typeof(ScheduleBox), new FrameworkPropertyMetadata(new ObservableCollection<string>(), (d, e) =>
            {
                ScheduleBox control = d as ScheduleBox;

                if (control == null) return;

                if (e.OldValue is ObservableCollection<string> o)
                {

                }

                if (e.NewValue is ObservableCollection<string> n)
                {

                }

            }));


        public ObservableCollection<IScheduleJob> JobsSource { get; } = new ObservableCollection<IScheduleJob>();


        IScheduler _scheduler;
        public async void Start()
        {
            ISchedulerFactory sf = new StdSchedulerFactory();
            _scheduler = await sf.GetScheduler();
            _scheduler.ListenerManager.AddSchedulerListener(this);
            //_scheduler.ListenerManager.AddJobListener(this);
            //_scheduler.ListenerManager.AddTriggerListener(this);
            foreach (var scheduleJob in this._scheduleJobs)
            {
                scheduleJob.Schedule(_scheduler);
            }
            await _scheduler.Start();
        }


        public void Stop()
        {
            _scheduler?.Shutdown();
        }


        //private void RefreshData()
        //{
        //    //List<JobDetail> jobDetails = new List<JobDetail>();
        //    //var js = IocSchedule.Instance.GetJobDetails();
        //    //foreach (var item in js)
        //    //{
        //    //    var jd = new JobDetail(item);
        //    //    var ts = IocSchedule.Instance.GetTriggersOfJob(item.Key);
        //    //    jd.Triggers = new ObservableCollection<ITrigger>(ts);
        //    //    jobDetails.Add(jd);
        //    //}
        //    //this.ItemsSource = new ObservableCollection<JobDetail>(jobDetails);
        //    //this.SchedulerMetaData = IocSchedule.Instance.GetMetaData();

        //    this.ItemsSource = this._scheduleJobs;
        //}

        public SchedulerMetaData SchedulerMetaData
        {
            get { return (SchedulerMetaData)GetValue(SchedulerMetaDataProperty); }
            set { SetValue(SchedulerMetaDataProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SchedulerMetaDataProperty =
            DependencyProperty.Register("SchedulerMetaData", typeof(SchedulerMetaData), typeof(ScheduleBox), new FrameworkPropertyMetadata(default(SchedulerMetaData), (d, e) =>
            {
                ScheduleBox control = d as ScheduleBox;

                if (control == null) return;

                if (e.OldValue is SchedulerMetaData o)
                {

                }

                if (e.NewValue is SchedulerMetaData n)
                {

                }

            }));
    }

    partial class ScheduleBox : IJobListener, ITriggerListener, ISchedulerListener
    {
        string IJobListener.Name { get; } = "ScheduleBoxJobListener";
        string ITriggerListener.Name { get; } = "ScheduleBoxTriggerListener";
        #region - ISchedulerListener -

        public Task JobAdded(IJobDetail jobDetail, CancellationToken cancellationToken = default)
        {

            this.AddMessage($"{jobDetail.Key.Name} {jobDetail.Key.Group}");
            return Task.CompletedTask;
        }


        public void AddMessage(string message, [CallerMemberName] string methodName = null)
        {
            this.Dispatcher.Invoke(() =>
            {
                this.Messages.Add($"[{DateTimeOffset.Now}] [{methodName}] - {message}");
            });
        }


        public Task JobDeleted(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            this.AddMessage($"{jobKey.Name} {jobKey.Group}");
            return Task.CompletedTask;
        }
        #endregion
        #region - IJobListener -

        public Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            this.AddMessage($"{context.JobDetail.Key.Name} {context.JobDetail.Key.Group}");
            return Task.CompletedTask;

        }

        public Task JobInterrupted(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            this.AddMessage($"{jobKey.Name} {jobKey.Group}");
            return Task.CompletedTask;
        }

        public Task JobPaused(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            this.AddMessage($"{jobKey.Name} {jobKey.Group}");
            return Task.CompletedTask;
        }

        public Task JobResumed(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            this.AddMessage($"{jobKey.Name} {jobKey.Group}");
            return Task.CompletedTask;
        }

        public Task JobScheduled(ITrigger trigger, CancellationToken cancellationToken = default)
        {
            this.AddMessage($"{trigger.Key.Name} {trigger.Key.Group}");
            this.AddMessage($"{trigger.JobKey.Name} {trigger.JobKey.Group}");
            return Task.CompletedTask;
        }

        public Task JobsPaused(string jobGroup, CancellationToken cancellationToken = default)
        {
            this.AddMessage($"{jobGroup}");
            return Task.CompletedTask;
        }

        public Task JobsResumed(string jobGroup, CancellationToken cancellationToken = default)
        {
            this.AddMessage($"{jobGroup}");
            return Task.CompletedTask;
        }

        public Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            this.AddMessage($"{context.JobDetail.Key.Name} {context.JobDetail.Key.Group}");
            return Task.CompletedTask;
        }

        public Task JobUnscheduled(TriggerKey triggerKey, CancellationToken cancellationToken = default)
        {
            this.AddMessage($"{triggerKey.Name} {triggerKey.Group}");
            return Task.CompletedTask;
        }

        public Task JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException, CancellationToken cancellationToken = default)
        {
            this.AddMessage($"{context.JobDetail.Key.Name} {context.JobDetail.Key.Group}");
            return Task.CompletedTask;
        }

        public Task SchedulerError(string msg, SchedulerException cause, CancellationToken cancellationToken = default)
        {
            this.AddMessage($"{msg} {cause?.Message}");
            return Task.CompletedTask;
        }

        public Task SchedulerInStandbyMode(CancellationToken cancellationToken = default)
        {
            this.AddMessage($"{MethodBase.GetCurrentMethod().Name}");
            return Task.CompletedTask;
        }

        public Task SchedulerShutdown(CancellationToken cancellationToken = default)
        {
            this.AddMessage($"{MethodBase.GetCurrentMethod().Name}");
            return Task.CompletedTask;
        }

        public Task SchedulerShuttingdown(CancellationToken cancellationToken = default)
        {
            this.AddMessage($"{MethodBase.GetCurrentMethod().Name}");
            return Task.CompletedTask;
        }

        public Task SchedulerStarted(CancellationToken cancellationToken = default)
        {
            this.AddMessage($"{MethodBase.GetCurrentMethod().Name}");
            return Task.CompletedTask;
        }

        public Task SchedulerStarting(CancellationToken cancellationToken = default)
        {
            this.AddMessage($"{MethodBase.GetCurrentMethod().Name}");
            return Task.CompletedTask;
        }

        public Task SchedulingDataCleared(CancellationToken cancellationToken = default)
        {
            this.AddMessage($"{MethodBase.GetCurrentMethod().Name}");
            return Task.CompletedTask;
        }
        #endregion

        #region - ITriggerListener -
        public Task TriggerComplete(ITrigger trigger, IJobExecutionContext context, SchedulerInstruction triggerInstructionCode, CancellationToken cancellationToken = default)
        {
            this.AddMessage($"{trigger.Key.Name} {trigger.Key.Group}");
            return Task.CompletedTask;
        }

        public Task TriggerFinalized(ITrigger trigger, CancellationToken cancellationToken = default)
        {
            this.AddMessage($"{trigger.Key.Name} {trigger.Key.Group}");
            return Task.CompletedTask;
        }

        public Task TriggerFired(ITrigger trigger, IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            this.AddMessage($"{trigger.Key.Name} {trigger.Key.Group}");
            return Task.CompletedTask;
        }

        public Task TriggerMisfired(ITrigger trigger, CancellationToken cancellationToken = default)
        {
            this.AddMessage($"{trigger.Key.Name} {trigger.Key.Group}");
            return Task.CompletedTask;
        }

        public Task TriggerPaused(TriggerKey triggerKey, CancellationToken cancellationToken = default)
        {
            this.AddMessage($"{triggerKey.Name} {triggerKey.Group}");
            return Task.CompletedTask;
        }

        public Task TriggerResumed(TriggerKey triggerKey, CancellationToken cancellationToken = default)
        {
            this.AddMessage($"{triggerKey.Name} {triggerKey.Group}");
            return Task.CompletedTask;
        }

        public Task TriggersPaused(string triggerGroup, CancellationToken cancellationToken = default)
        {
            this.AddMessage($"{triggerGroup}");
            return Task.CompletedTask;
        }

        public Task TriggersResumed(string triggerGroup, CancellationToken cancellationToken = default)
        {
            this.AddMessage($"{triggerGroup}");
            return Task.CompletedTask;
        }

        public Task<bool> VetoJobExecution(ITrigger trigger, IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            this.AddMessage($"{trigger.Key.Name} {trigger.Key.Group}");
            return Task.FromResult(true);
        }
        #endregion

    }
}
