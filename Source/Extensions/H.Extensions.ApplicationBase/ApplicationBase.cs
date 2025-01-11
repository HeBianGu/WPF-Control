using H.Iocable;
using H.Services.Common;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace H.Extensions.ApplicationBase
{
    public abstract partial class ApplicationBase : Application
    {
        public ApplicationBase()
        {
            this.OnExcetion();
            this.OnRefreshIoc();
        }

        protected void OnRefreshIoc()
        {
            ServiceCollection sc = new ServiceCollection();
            this.ConfigureServices(sc);
            //ServiceProvider sp = sc.BuildServiceProvider();
            Ioc.Build(sc);
            this.OnSetting();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            ApplicationBuilder bulder = new ApplicationBuilder();
            this.Configure(bulder);
            this.OnSingleton(e);
            base.OnStartup(e);
            this.CreateMainWindow(e);
            this.OnSplashScreen(e);
            this.OnLogin(e);
            this.MainWindow.Show();
            this.ILogger?.Info("系统启动");
            Ioc<IScheduledTaskService>.Instance?.Start();
        }

        #region - Exception -
        protected virtual void OnExcetion()
        {
            //#if DEBUG
            //            return;
            //#endif
            DispatcherUnhandledException += App_DispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
        }

        protected void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            //#if DEBUG
            //            Trace.Assert(false);
            //#endif
            Current.Dispatcher.Invoke(() =>
            {
                this.ShowMessage(e.Exception?.ToString(), "系统异常");
            });

            e.Handled = true;
            this.ILogger?.Error("系统异常");
            this.ILogger?.Error(e.Exception);
        }

        protected void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            //#if DEBUG
            //            Trace.Assert(false);
            //#endif
            Exception error = (Exception)e.ExceptionObject;
            Current.Dispatcher.Invoke(() => MessageBox.Show("当前应用程序遇到一些问题，该操作已经终止，请进行重试，如果问题继续存在，请联系管理员", "意外的操作"));
            this.ILogger?.Fatal("当前应用程序遇到一些问题，该操作已经终止，请进行重试，如果问题继续存在，请联系管理员", "意外的操作");
            this.ILogger?.Fatal(error);
        }


        /// <summary> 异步线程抛出没有补货的异常 </summary>
        protected void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            //#if DEBUG
            //            Trace.Assert(false);
            //#endif
            StringBuilder sb = new StringBuilder();
            foreach (Exception item in e.Exception.InnerExceptions)
            {
                sb.AppendLine($@"异常类型：{item.GetType()}
异常内容：{item.Message}
来自：{item.Source}
{item.StackTrace}");
            }

            e.SetObserved();

            this.ILogger?.Error("Task Exception");
            this.ILogger?.Error(sb.ToString());
            Current.Dispatcher.Invoke(() =>
            {
                this.ShowMessage(sb.ToString(), "系统任务异常");
                //MessageProxy.Windower.ShowSumit(sb.ToString(), "系统任务异常", false, -1)

            });
        }

        #endregion

        #region - Exit -

        protected override void OnExit(ExitEventArgs e)
        {
            try
            {
                Ioc<IScheduledTaskService>.Instance?.Stop();
                this.ILogger?.Info("系统退出");
                Ioc<IOperationService>.Instance?.Log<ApplicationBase>("系统推出");
            }
            catch (Exception ex)
            {
                this.ILogger?.Error(ex);
            }
            finally
            {
                base.OnExit(e);
            }
        }

        #endregion

        #region - Instance -

        private Mutex mutex;
        /// <summary>
        /// 检查应用程序只需打开一个
        /// </summary>
        public virtual void OnSingleton(StartupEventArgs e)
        {
            Process thisProc = Process.GetCurrentProcess();
            bool createdNew;
            mutex = new Mutex(true, thisProc.ProcessName, out createdNew);
            if (!createdNew)
            {
                this.ShowMessage("当前程序已经运行！");
                this.Shutdown();
            }
        }

        #endregion


        public ILogService ILogger => Ioc.Services.GetService<ILogService>();
        protected virtual void ShowMessage(string message, string title = "提示")
        {
            MessageBox.Show(message);


            //if (MessageProxy.Windower == null)
            //{
            //    MessageBox.Show("当前程序已经运行！");
            //}
            //else
            //{
            //    MessageProxy.Windower.ShowSumit("当前程序已经运行！");
            //}

        }
    }
}
