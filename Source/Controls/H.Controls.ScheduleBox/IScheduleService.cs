using Quartz;
using System;
using System.Collections.Generic;

namespace H.Controls.ScheduleBox
{
    public interface IScheduleService
    {
        IReadOnlyCollection<IJobDetail> GetJobDetails();
        IReadOnlyCollection<ITrigger> GetTriggersOfJob(JobKey jobKey);

        SchedulerMetaData GetMetaData();

        void Add(IJobDetail job);

        void Start();

        event Action<string> LogMessaged;

        void OnLogMessaged(string message);
    }
}
