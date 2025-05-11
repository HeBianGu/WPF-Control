// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using Quartz;
using System.Diagnostics;

namespace H.Controls.ScheduleBox
{
    public class HelloJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                //Debug.WriteLine($"【任务执行】：{DateTime.Now}");
                //Debug.WriteLine($"【触发时间】：{context.ScheduledFireTimeUtc?.LocalDateTime}");
                //Debug.WriteLine($"【下次触发时间】：{context.NextFireTimeUtc?.LocalDateTime}");

                JobKey jobKey = context.JobDetail.Key;
                Debug.WriteLine("SimpleJob says: {0} executing at {1:r}", jobKey, DateTime.Now);
                //IocSchedule.Instance.OnLogMessaged($"SimpleJob says: {jobKey} executing at {DateTime.Now}");
                //IocSchedule.Instance.OnLogMessaged($"【任务执行】：{DateTime.Now}");
                //IocSchedule.Instance.OnLogMessaged($"【触发时间】：{context.ScheduledFireTimeUtc?.LocalDateTime}");
                //IocSchedule.Instance.OnLogMessaged($"【下次触发时间】：{context.NextFireTimeUtc?.LocalDateTime}");
            }
            catch (Exception)
            {

            }
            await Task.FromResult(true);
        }
    }
}
