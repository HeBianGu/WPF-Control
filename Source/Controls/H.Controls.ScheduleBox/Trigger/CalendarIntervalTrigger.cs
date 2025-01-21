using H.Controls.Form;
using H.Controls.Form.PropertyItem.Attribute.SourcePropertyItem;
using H.Controls.Form.PropertyItem.ComboBoxPropertyItems;
using Quartz;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace H.Controls.ScheduleBox
{

    [Display(Name = "日期触发器", GroupName = "Default")]
    public class CalendarIntervalTrigger : TriggerBase
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

        private int _interval = 1;
        [Display(Name = "触发间隔")]
        public int Interval
        {
            get { return _interval; }
            set
            {
                _interval = value;
                RaisePropertyChanged();
            }
        }

        private IntervalUnit _intervalUnit = IntervalUnit.Minute;
        [Display(Name = "间隔单位")]
        public IntervalUnit IntervalUnit
        {
            get { return _intervalUnit; }
            set
            {
                _intervalUnit = value;
                RaisePropertyChanged();
            }
        }


        private TimeZoneInfo _timeZoneInfo = TimeZoneInfo.Utc;
        [Display(Name = "时区")]
        [MethodNameSourcePropertyItem(typeof(FormComboBoxPropertyItem), nameof(GetTimeZoneInfos))]
        public TimeZoneInfo TimeZoneInfo
        {
            get { return _timeZoneInfo; }
            set
            {
                _timeZoneInfo = value;
                RaisePropertyChanged();
            }
        }

        public IEnumerable<TimeZoneInfo> GetTimeZoneInfos()
        {
            return TimeZoneInfo.GetSystemTimeZones();
        }

        private DateTimeOffset _endAt = DateTimeOffset.MaxValue;
        [TypeConverter(typeof(DateTimeOffsetConverter))]
        [Display(Name = "结束时间")]
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
            return TriggerBuilder.Create()
           .WithIdentity(this.Key, scheduleJob.GroupName)
           .StartAt(this.StartAt)
           .EndAt(this.EndAt)
           .WithCalendarIntervalSchedule(x => x
           .WithInterval(this.Interval, this.IntervalUnit)
           .InTimeZone(this.TimeZoneInfo)).Build();
        }
    }
}
