using H.Mvvm.ViewModels.Base;
using Quartz;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace H.Controls.ScheduleBox
{
    public abstract class TriggerBase : DisplayBindableBase, IScheduleTrigger
    {
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

        private DateTimeOffset _startAt = DateTimeOffset.Now;
        [Display(Name = "开始时间")]
        [TypeConverter(typeof(DateTimeOffsetConverter))]
        public DateTimeOffset StartAt
        {
            get { return _startAt; }
            set
            {
                _startAt = value;
                RaisePropertyChanged();
            }
        }

        public abstract ITrigger Build(IScheduleJob scheduleJob);
    }
}
