//using Microsoft.Extensions.Logging;
//using Quartz;
//using Quartz.Impl;
//using Quartz.Impl.Matchers;
//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Diagnostics;
//using System.Linq;
//using System.Reflection;
//using System.Threading;
//using System.Threading.Tasks;

//namespace H.Controls.ScheduleBox
//{

//    /// <summary>
//    /// 监听一些 动作
//    /// </summary>
//    public class CustomSchedulerListener : ISchedulerListener
//    {
//        public async Task JobAdded(IJobDetail jobDetail, CancellationToken cancellationToken = default)
//        {
//            await Task.Run(() => {

//                Debug.WriteLine($"{jobDetail.Key.Name} 添加进来了");
//            });
//        }

//        public async Task JobDeleted(JobKey jobKey, CancellationToken cancellationToken = default)
//        {
//            await Task.Run(() => {

//                Debug.WriteLine($"{jobKey.Name} 删除了");
//            });
//        }

//        public Task JobInterrupted(JobKey jobKey, CancellationToken cancellationToken = default)
//        {
//            System.Diagnostics.Debug.WriteLine(MethodBase.GetCurrentMethod().Name);
//            return Task.CompletedTask;
//        }

//        public Task JobPaused(JobKey jobKey, CancellationToken cancellationToken = default)
//        {
//            System.Diagnostics.Debug.WriteLine(MethodBase.GetCurrentMethod().Name);
//            return Task.CompletedTask;
//        }

//        public Task JobResumed(JobKey jobKey, CancellationToken cancellationToken = default)
//        {
//            System.Diagnostics.Debug.WriteLine(MethodBase.GetCurrentMethod().Name);
//            return Task.CompletedTask;
//        }

//        public Task JobScheduled(ITrigger trigger, CancellationToken cancellationToken = default)
//        {
//            System.Diagnostics.Debug.WriteLine(MethodBase.GetCurrentMethod().Name);
//            return Task.CompletedTask;
//        }

//        public Task JobsPaused(string jobGroup, CancellationToken cancellationToken = default)
//        {
//            System.Diagnostics.Debug.WriteLine(MethodBase.GetCurrentMethod().Name);
//            return Task.CompletedTask;
//        }

//        public Task JobsResumed(string jobGroup, CancellationToken cancellationToken = default)
//        {
//            System.Diagnostics.Debug.WriteLine(MethodBase.GetCurrentMethod().Name);
//            return Task.CompletedTask;
//        }

//        public Task JobUnscheduled(TriggerKey triggerKey, CancellationToken cancellationToken = default)
//        {
//            System.Diagnostics.Debug.WriteLine(MethodBase.GetCurrentMethod().Name);
//            return Task.CompletedTask;
//        }

//        public Task SchedulerError(string msg, SchedulerException cause, CancellationToken cancellationToken = default)
//        {
//            System.Diagnostics.Debug.WriteLine(MethodBase.GetCurrentMethod().Name);
//            return Task.CompletedTask;
//        }

//        public Task SchedulerInStandbyMode(CancellationToken cancellationToken = default)
//        {
//            System.Diagnostics.Debug.WriteLine(MethodBase.GetCurrentMethod().Name);
//            return Task.CompletedTask;
//        }

//        public Task SchedulerShutdown(CancellationToken cancellationToken = default)
//        {
//            System.Diagnostics.Debug.WriteLine(MethodBase.GetCurrentMethod().Name);
//            return Task.CompletedTask;
//        }

//        public Task SchedulerShuttingdown(CancellationToken cancellationToken = default)
//        {
//            System.Diagnostics.Debug.WriteLine(MethodBase.GetCurrentMethod().Name);
//           return Task.CompletedTask;
//        }

//        public async Task SchedulerStarted(CancellationToken cancellationToken = default)
//        {
//            System.Diagnostics.Debug.WriteLine(MethodBase.GetCurrentMethod().Name);
//            await Task.CompletedTask;
//        }

//        public Task SchedulerStarting(CancellationToken cancellationToken = default)
//        {
//            System.Diagnostics.Debug.WriteLine(MethodBase.GetCurrentMethod().Name);
//            return Task.CompletedTask;
//        }

//        public Task SchedulingDataCleared(CancellationToken cancellationToken = default)
//        {
//            System.Diagnostics.Debug.WriteLine(MethodBase.GetCurrentMethod().Name);
//            return Task.CompletedTask;
//        }

//        public Task TriggerFinalized(ITrigger trigger, CancellationToken cancellationToken = default)
//        {
//            System.Diagnostics.Debug.WriteLine(MethodBase.GetCurrentMethod().Name);
//            return Task.CompletedTask;
//        }

//        public Task TriggerPaused(TriggerKey triggerKey, CancellationToken cancellationToken = default)
//        {
//            throw new NotImplementedException();
//        }

//        public Task TriggerResumed(TriggerKey triggerKey, CancellationToken cancellationToken = default)
//        {
//            System.Diagnostics.Debug.WriteLine(MethodBase.GetCurrentMethod().Name);
//            return Task.CompletedTask;
//        }

//        public Task TriggersPaused(string triggerGroup, CancellationToken cancellationToken = default)
//        {
//            System.Diagnostics.Debug.WriteLine(MethodBase.GetCurrentMethod().Name);
//            return Task.CompletedTask;
//        }

//        public Task TriggersResumed(string triggerGroup, CancellationToken cancellationToken = default)
//        {
//            System.Diagnostics.Debug.WriteLine(MethodBase.GetCurrentMethod().Name);
//            return Task.CompletedTask;
//        }
//    }


//    public class CustomJobListener : IJobListener
//    {
//        public string Name => "CustomJobListener";

//        /// <summary>
//        /// 停止执行
//        /// </summary>
//        /// <param name="context"></param>
//        /// <param name="cancellationToken"></param>
//        /// <returns></returns>
//        public async Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default)
//        {

//            await Task.Run(() => {

//                Debug.WriteLine("停止执行");
//            });
//        }
//        /// <summary>
//        /// 待执行
//        /// </summary>
//        /// <param name="context"></param>
//        /// <param name="cancellationToken"></param>
//        /// <returns></returns>
//        public async Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default)
//        {
//            await Task.Run(() => {

//                Debug.WriteLine("待执行");
//            });
//        }
//        /// <summary>
//        /// 已执行
//        /// </summary>
//        /// <param name="context"></param>
//        /// <param name="jobException"></param>
//        /// <param name="cancellationToken"></param>
//        /// <returns></returns>
//        public async Task JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException, CancellationToken cancellationToken = default)
//        {
//            await Task.Run(() => {

//                Debug.WriteLine("已执行");
//            });
//        }
//    }


//    public class CustomTriggerListener : ITriggerListener
//    {
//        public string Name => "CustomTriggerListener";

//        /// <summary>
//        /// 任务完成时触发
//        /// </summary>
//        /// <param name="trigger"></param>
//        /// <param name="context"></param>
//        /// <param name="triggerInstructionCode"></param>
//        /// <param name="cancellationToken"></param>
//        /// <returns></returns>
//        public async Task TriggerComplete(ITrigger trigger, IJobExecutionContext context, SchedulerInstruction triggerInstructionCode, CancellationToken cancellationToken = default)
//        {
//            await Task.Run(() => {

//                Debug.WriteLine("TriggerComplete");
//            });
//        }
//        /// <summary>
//        ///Trigger被激发 它关联的job即将被运行
//        /// </summary>
//        /// <param name="trigger"></param>
//        /// <param name="context"></param>
//        /// <param name="cancellationToken"></param>
//        /// <returns></returns>
//        public async Task TriggerFired(ITrigger trigger, IJobExecutionContext context, CancellationToken cancellationToken = default)
//        {
//            await Task.Run(() => {

//                Debug.WriteLine("TriggerFired");
//            });
//        }
//        /// <summary>
//        /// 当Trigger错过被激发时执行,比如当前时间有很多触发器都需要执行，但是线程池中的有效线程都在工作，
//        /// 那么有的触发器就有可能超时，错过这一轮的触发。
//        /// </summary>
//        /// <param name="trigger"></param>
//        /// <param name="cancellationToken"></param>
//        /// <returns></returns>
//        public async Task TriggerMisfired(ITrigger trigger, CancellationToken cancellationToken = default)
//        {
//            await Task.Run(() => {
//                Debug.WriteLine("TriggerMisfired");
//            });
//        }
//        /// <summary>
//        /// 如果返回true 则取消任务， 返回false 则继续执行
//        /// </summary>
//        /// <param name="trigger"></param>
//        /// <param name="context"></param>
//        /// <param name="cancellationToken"></param>
//        /// <returns></returns>
//        public async Task<bool> VetoJobExecution(ITrigger trigger, IJobExecutionContext context, CancellationToken cancellationToken = default)
//        {
//            return true;
//        }
//    }

//    public class ScheduleService : IScheduleService
//    {
//        //private static IScheduler _scheduler = StdSchedulerFactory.GetDefaultScheduler().Result;
//        //public static IScheduler GetScheduler()
//        //{
//        //    return _Scheduler;
//        //}

//        public ScheduleService()
//        {

//        }

//        public event Action<string> LogMessaged;

//        public void OnLogMessaged(string message)
//        {
//            LogMessaged?.Invoke(message);
//        }

//        //public ScheduleService()
//        //{

//        //    //// obtain your logger factory, for example from IServiceProvider
//        //    //ILoggerFactory loggerFactory = ...;

//        //    //Quartz.Logging.LogContext.SetCurrentLogProvider(loggerFactory);


//        //    //this.Initializing();
//        //    //this.CreateJobs();
//        //    //this.CreateListener();
//        //}


//        public void CreateListener()
//        {
//            _scheduler.ListenerManager.AddSchedulerListener(new CustomSchedulerListener());
//            _scheduler.ListenerManager.AddJobListener(new CustomJobListener());
//            _scheduler.ListenerManager.AddJobListener(new CustomJobListener());

//        }


//        public IReadOnlyCollection<IJobDetail> GetJobDetails()
//        {
//            var keys = this.GetJobKeys().Result;
//            return keys.Select(x => _scheduler.GetJobDetail(x).Result).ToList().AsReadOnly();
//        }


//        public async Task<IReadOnlyCollection<JobKey>> GetJobKeys()
//        {
//            return await _scheduler.GetJobKeys(GroupMatcher<JobKey>.AnyGroup());
//        }

//        public async Task<IReadOnlyCollection<TriggerKey>> GetTriggerKeys()
//        {
//            return await _scheduler.GetTriggerKeys(GroupMatcher<TriggerKey>.AnyGroup());
//        }

//        public IReadOnlyCollection<ITrigger> GetTriggersOfJob(JobKey jobKey)
//        {
//            return _scheduler.GetTriggersOfJob(jobKey).Result;
//        }

//        public SchedulerMetaData GetMetaData()
//        {
//            return _scheduler.GetMetaData().Result;
//        }

//        public async Task<IJobDetail> GetJobDetail(JobKey jobKey)
//        {
//            return await _scheduler.GetJobDetail(jobKey);
//        }

//        private IScheduler _scheduler;
//        public async void Initializing()
//        {
//            Debug.WriteLine("------- Initializing ----------------------");
//            // First we must get a reference to a scheduler
//            ISchedulerFactory sf = new StdSchedulerFactory();
//            _scheduler = await sf.GetScheduler();
//            Debug.WriteLine("------- Initialization Complete -----------");
//        }

//        public async void ShutDown()
//        {
//            // shut down the scheduler
//            Debug.WriteLine("------- Shutting Down ---------------------");
//            await _scheduler?.Shutdown(true);
//            Debug.WriteLine("------- Shutdown Complete -----------------");
//        }


//        public async void Start()
//        {
//            await _scheduler.Start();
//        }


//        public void Add(IJobDetail job)
//        {
//            //添加作业
//            _scheduler.AddJob(job, true);
//        }


//        public void PauseJobs()
//        {
//            //暂停作业
//            _scheduler.PauseJobs(GroupMatcher<JobKey>.GroupEquals("groupa"));
//        }


//        public void ResumeJobs()
//        {
//            //恢复作业
//            _scheduler.ResumeJobs(GroupMatcher<JobKey>.GroupEquals("groupa"));
//        }


//        public virtual void CreateJobs()
//        {
//            if (_scheduler == null)
//                return;
//            // computer a time that is on the next round minute
//            DateTimeOffset runTime = DateBuilder.EvenSecondDateBefore(DateTimeOffset.UtcNow);
//            Debug.WriteLine("------- Scheduling Job  -------------------");
//            // define the job and tie it to our HelloJob class
//            IJobDetail job = JobBuilder.Create<HelloJob>()
//                .WithIdentity("job1", "group1")
//                .Build();

//            // Trigger the job to run on the next round minute
//            ISimpleTrigger trigger = (ISimpleTrigger)TriggerBuilder.Create()
//                .WithIdentity("trigger1", "group1")
//                //.StartNow()
//                //.StartAt(runTime)
//                .WithSimpleSchedule(x => x.WithIntervalInSeconds(5).WithRepeatCount(10))
//                .Build();

//            // Tell quartz to schedule the job using our trigger
//            _scheduler.ScheduleJob(job, trigger).Wait();
//            Debug.WriteLine($"{job.Key} will run at: {runTime:r}");

//            // Start up the scheduler (nothing can actually run until the
//            // scheduler has been started)
//            //await _scheduler.Start();
//        }


//        public List<IScheduleJob> Jobs { get; set; } = new List<IScheduleJob>();
//    }
//}
