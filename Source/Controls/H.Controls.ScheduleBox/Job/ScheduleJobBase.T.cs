using Quartz;
using System;
using System.Threading.Tasks;

namespace H.Controls.ScheduleBox
{
    public abstract class ScheduleJobBase<T> : ScheduleJobBase where T : class, IScheduleJob
    {
        public override async Task Execute(IJobExecutionContext context)
        {
            var scheduleJob = context.JobDetail.JobDataMap.Get("this") as T;

            scheduleJob.IsRunning = true;
            await Task.Run(() =>
           {
               try
               {
                   var r = this.Invoke(context, scheduleJob, out string message);
                   scheduleJob.NextFireTime = context.NextFireTimeUtc?.LocalDateTime;
                   scheduleJob.Message = message;
               }
               catch (Exception ex)
               {
                   scheduleJob.Message = ex.Message;
                   IocLog.Instance?.Error(ex);
               }
               finally
               {
                   scheduleJob.IsRunning = false;
               }
           });

        }

        public abstract bool Invoke(IJobExecutionContext context, T scheduleJob, out string message);
    }
}
