using Quartz;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static Quartz.Logging.OperationName;

namespace H.Controls.ScheduleBox
{
    public class ScheduleBox : ListBox
    {
        static ScheduleBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ScheduleBox), new FrameworkPropertyMetadata(typeof(ScheduleBox)));
        }

        public ScheduleBox()
        {
            {
                CommandBinding binding = new CommandBinding(ScheduleCommands.Start);
                binding.Executed += (l, k) =>
                {
                    IocSchedule.Instance.Start();
                };
                binding.CanExecute += (l, k) =>
                {
                    k.CanExecute = true;
                };
                this.CommandBindings.Add(binding);
            }

            {
                CommandBinding binding = new CommandBinding(ScheduleCommands.Add);
                binding.Executed += (l, k) =>
                {
                    IocSchedule.Instance.Start();
                };
                binding.CanExecute += (l, k) =>
                {
                    k.CanExecute = true;
                };
                this.CommandBindings.Add(binding);
            }

            IocSchedule.Instance.LogMessaged += x =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    this.Messages.Add(x);
                });

            };

            this.RefreshData();
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


        private void RefreshData()
        {
            List<JobDetail> jobDetails = new List<JobDetail>();
            var js = IocSchedule.Instance.GetJobDetails();
            foreach (var item in js)
            {
                var jd = new JobDetail(item);
                var ts = IocSchedule.Instance.GetTriggersOfJob(item.Key);
                jd.Triggers = new ObservableCollection<ITrigger>(ts);
                jobDetails.Add(jd);
            }
            this.ItemsSource = new ObservableCollection<JobDetail>(jobDetails);
            this.SchedulerMetaData = IocSchedule.Instance.GetMetaData();
        }

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
