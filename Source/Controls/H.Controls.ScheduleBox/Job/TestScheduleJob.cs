using Quartz;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Threading;

namespace H.Controls.ScheduleBox
{
    [Display(Name = "测试工作", GroupName = "Default")]
    public class TestScheduleJob : ScheduleJobBase<TestScheduleJob>
    {
        private string _testValue;
        [Display(Name = "测试数据")]
        public string TestValue
        {
            get { return _testValue; }
            set
            {
                _testValue = value;
                RaisePropertyChanged();
            }
        }

        public override bool Invoke(IJobExecutionContext context, TestScheduleJob scheduleJob, out string message)
        {
            //Debug.WriteLine($"【任务执行】：{DateTime.Now}");
            //Debug.WriteLine($"【触发时间】：{context.ScheduledFireTimeUtc?.LocalDateTime}");
            //Debug.WriteLine($"【下次触发时间】：{context.NextFireTimeUtc?.LocalDateTime}");


            //IocSchedule.Instance.OnLogMessaged($"【任务执行】：{DateTime.Now}");
            //IocSchedule.Instance.OnLogMessaged($"【触发时间】：{context.ScheduledFireTimeUtc?.LocalDateTime}");
            //IocSchedule.Instance.OnLogMessaged($"【下次触发时间】：{context.NextFireTimeUtc?.LocalDateTime}");

            message = null;
            JobKey jobKey = context.JobDetail.Key;
            Debug.WriteLine("SimpleJob says: {0} executing at {1:r}", jobKey, DateTime.Now);
            //IocSchedule.Instance.OnLogMessaged($"SimpleJob says: {jobKey} executing at {DateTime.Now}");
            for (int i = 0; i < 10; i++)
            {
                scheduleJob.Message = $"SimpleJob says: {jobKey} executing at {DateTime.Now} 测试数据{scheduleJob.TestValue} {i}";
                Thread.Sleep(100);
            }

            return true;
        }
    }
}
