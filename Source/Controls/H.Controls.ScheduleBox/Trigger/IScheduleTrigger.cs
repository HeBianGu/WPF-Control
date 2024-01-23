using Quartz;

namespace H.Controls.ScheduleBox
{
    public interface IScheduleTrigger
    {

        ITrigger Build(IScheduleJob scheduleJob);
    }
}
