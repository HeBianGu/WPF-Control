using H.Controls.Form;
using H.Controls.Form.Attributes;
using H.Controls.Form.PropertyItem.Attribute.SourcePropertyItem;
using H.Controls.Form.PropertyItem.ComboBoxPropertyItems;
using Quartz;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace H.Controls.ScheduleBox
{
    [Display(Name = "每日触发器", GroupName = "Default")]
    public class DailyTimeIntervalTrigger : TriggerBase
    {
        private int _hour;
        [Display(Name = "小时")]
        [Range(0, 23)]
        public int Hour
        {
            get { return _hour; }
            set
            {
                _hour = value;
                RaisePropertyChanged();
            }
        }

        private int _minute;
        [Display(Name = "分钟")]
        [Range(0, 59)]
        public int Minute
        {
            get { return _minute; }
            set
            {
                _minute = value;
                RaisePropertyChanged();
            }
        }

        private int _second;
        [Display(Name = "秒")]
        [Range(0, 59)]
        public int Second
        {
            get { return _second; }
            set
            {
                _second = value;
                RaisePropertyChanged();
            }
        }

        private DayOfWeeks _dayOfWeeks = new DayOfWeeks();
        [Property(UsePresenter = true)]
        [Display(Name = "重复")]
        public DayOfWeeks DayOfWeeks
        {
            get { return _dayOfWeeks; }
            set
            {
                _dayOfWeeks = value;
                RaisePropertyChanged();
            }
        }

        private TimeZoneInfo _timeZoneInfo = TimeZoneInfo.Utc;
        [Display(Name = "时区")]
        [PropertyNameSourcePropertyItem(typeof(FormComboBoxPropertyItem), nameof(GetTimeZoneInfos))]
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
            return (IDailyTimeIntervalTrigger)TriggerBuilder.Create()
           .WithIdentity(this.Key, scheduleJob.GroupName)
           .StartAt(this.StartAt)
           .EndAt(this.EndAt)
           .WithDailyTimeIntervalSchedule(x => x
           .OnDaysOfTheWeek(this.DayOfWeeks.ToArray())
           .StartingDailyAt(TimeOfDay.HourMinuteAndSecondOfDay(this.Hour, this.Minute, this.Second))
           .InTimeZone(this.TimeZoneInfo))
           .Build();
        }
    }
}
