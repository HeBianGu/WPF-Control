// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

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
