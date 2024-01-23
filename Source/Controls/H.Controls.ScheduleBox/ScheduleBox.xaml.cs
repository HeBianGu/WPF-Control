using H.Presenters.Common;
using H.Providers.Ioc;
using Quartz;
using Quartz.Impl;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static Quartz.Logging.OperationName;

namespace H.Controls.ScheduleBox
{
    public class ScheduleBox : ItemsControl
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
                    SelectItemPresenter selectItemPresenter = new SelectItemPresenter(source, selectedItem);
                    selectItemPresenter.DisplayMemberPath = "Name";
                    var r = await IocMessage.Dialog.Show(selectItemPresenter);
                    if (r == false)
                        return;
                    if (selectItemPresenter.SelectedItem is IScheduleJob job)
                        _scheduleJobs.Add(job);
                };
                binding.CanExecute += (l, k) =>
                {
                    k.CanExecute = true;
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
                    k.CanExecute = true;
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
            //        this.Messages.Add(x);
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
}
