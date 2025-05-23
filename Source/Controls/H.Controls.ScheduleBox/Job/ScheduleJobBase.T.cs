// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Services.Logger;
using Quartz;

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
