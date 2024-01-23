using Quartz;
using System.ComponentModel.DataAnnotations;

namespace H.Controls.ScheduleBox
{
    [Display(Name = "定时触发器", GroupName = "Default")]

    public class DataTimeTrigger : TriggerBase
    {
        public override ITrigger Build(IScheduleJob scheduleJob)
        {
            scheduleJob.NextFireTime = this.StartAt.DateTime;
            return TriggerBuilder.Create()
                 .WithIdentity(this.Key, scheduleJob.GroupName)
                 .StartAt(this.StartAt)
                 .Build();

        }
    }
}
