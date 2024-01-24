using Quartz;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace H.Controls.ScheduleBox
{
    [Display(Name = "循环触发器", GroupName = "Default")]
    public class RepeatTrigger : TriggerBase
    {
        private int _repeatCount = 10;
        [DefaultValue(1)]
        [Display(Name = "重复次数")]
        public int RepeatCount
        {
            get { return _repeatCount; }
            set
            {
                _repeatCount = value;
                RaisePropertyChanged();
            }
        }

        private TimeSpan _interval = TimeSpan.FromSeconds(5);
        [TypeConverter(typeof(TimeSpanConverter))]
        [Display(Name = "触发间隔")]
        public TimeSpan Interval
        {
            get { return _interval; }
            set
            {
                _interval = value;
                RaisePropertyChanged();
            }
        }

        private bool _useRepeatForever;
        [Display(Name = "重复执行")]
        public bool UseRepeatForever
        {
            get { return _useRepeatForever; }
            set
            {
                _useRepeatForever = value;
                RaisePropertyChanged();
            }
        }

        public override void LoadDefault()
        {
            base.LoadDefault();
            this.Interval = TimeSpan.FromSeconds(1);
        }

        private DateTimeOffset _endAt = DateTimeOffset.MaxValue;
        [Display(Name = "结束时间")]
        [TypeConverter(typeof(DateTimeOffsetConverter))]
        public DateTimeOffset EndAt
        {
            get { return _endAt; }
            set
            {
                _endAt = value;
                RaisePropertyChanged();
            }
        }

        public override ITrigger Build(IScheduleJob scheduleJob)
        {
            scheduleJob.NextFireTime = System.DateTime.Now + this.Interval;
            return TriggerBuilder.Create()
           .WithIdentity(this.Key, scheduleJob.GroupName)
           .StartAt(this.StartAt)
           .EndAt(this.EndAt)
           .WithSimpleSchedule(x =>
           {
               x.WithInterval(this.Interval);
               if (this.UseRepeatForever)
                   x.RepeatForever();
               else
                   x.WithRepeatCount(this.RepeatCount);
           }).Build();
        }
    }
}
