// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Attach;
using H.Common.Exceptions;
using H.Common.Interfaces;
using H.Extensions.ApplicationBase.Properties;
using H.Extensions.AppPath;
using H.Extensions.Common;
using H.Services.AppPath;
using H.Services.Common;
using H.Services.Common.DataBase;
using H.Services.Common.MainWindow;
using H.Services.Common.Schedule;
using H.Services.Common.SplashScreen;
using H.Services.Common.Theme;
using H.Services.Identity;
using H.Services.Logger;
using H.Services.Message;
using H.Services.Message.Dialog;
using H.Services.Setting;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace H.Extensions.ApplicationBase;

public abstract partial class ApplicationBase : Application, IConfigureableApplication, ILoginableApplication
{
    public ApplicationBase()
    {
        this.ShutdownMode = ShutdownMode.OnMainWindowClose;
        AppPaths.Register(this.CreateAppPathServce());
        this.InitExcetion();
        this.InitServiceCollection();
    }

    protected void InitServiceCollection()
    {
        ServiceCollection sc = new ServiceCollection();
        this.ConfigureServices(sc);
        Ioc.Build(sc);
        //  Do ：在显示页面前需要加载多语言
        Ioc.GetService<ILoadGlobalizationOptionsService>(false)?.Load(out string message);
    }

    protected virtual IAppPathServce CreateAppPathServce()
    {
        string company = ApplicationProvider.Company ?? "HeBianGu";
        if (company == ApplicationProvider.Title)
            company = "HeBianGu";
        if (company == ApplicationProvider.Product)
            company = "HeBianGu";
        return new AppPathServce(this.GetAppPathVersion(), company);
    }

    protected virtual string GetAppPathVersion()
    {
        //  文件路径按照Minor版本号进行区分，避免不同版本之间的配置文件冲突，
        var version = Assembly.GetEntryAssembly().GetName().Version;
        return $"{version.Major}.{version.Minor}";
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        this.Configure();
        this.OnSingleton(e);
        base.OnStartup(e);
        Window window = this.CreateMainWindow(e);
        window.Loaded += (s, e) =>
        {
            var loads = Ioc.GetAssignableFromServices<IMainWindowLoadedLoadable>().Distinct();
            foreach (var item in loads)
                item.Load(out string message);

        };
        this.OnSplashScreen(e);
        this.OnLogin();
        Ioc<IMainWindowSavableService>.Instance?.Load(window);
        this.MainWindow.Show();
        this.ILogger?.Info("系统启动");
        Ioc<IScheduledTaskService>.Instance?.Start();
    }

    #region - Exception -
    // 跨线程弹窗标记：0表示未显示，1表示正在显示；通过Interlocked原子更新。
    private int _showingException;

    /// <summary>
    /// 注册UI线程、应用程序域及未观察任务的全局异常处理，重复调用不会重复订阅。
    /// </summary>
    protected virtual void InitExcetion()
    {
        this.UnregisterExceptionHandlers();
        DispatcherUnhandledException += App_DispatcherUnhandledException;
        AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
    }

    /// <summary>
    /// 取消全局异常订阅，避免重复处理及静态事件持有应用实例。
    /// </summary>
    private void UnregisterExceptionHandlers()
    {
        DispatcherUnhandledException -= App_DispatcherUnhandledException;
        AppDomain.CurrentDomain.UnhandledException -= CurrentDomain_UnhandledException;
        TaskScheduler.UnobservedTaskException -= TaskScheduler_UnobservedTaskException;
    }

    /// <summary>
    /// 处理UI线程未捕获异常；严重资源或内存异常不标记为已处理，交由运行时处理。
    /// </summary>
    protected void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
    {
        e.Handled = CanHandleException(e.Exception);
        this.HandleException(e.Exception, !e.Handled);
    }

    /// <summary>
    /// 处理应用程序域未捕获异常；进程即将终止时仅记录日志，不等待UI提示。
    /// </summary>
    protected void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        var error = e.ExceptionObject as Exception ?? new Exception("发生未知的未处理异常");
        this.HandleException(error, e.IsTerminating);
    }

    /// <summary> 处理未观察到的异步任务异常。 </summary>
    protected void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
    {
        // 先标记为已观察，再记录和提示；业务代码仍应主动await并处理任务异常。
        e.SetObserved();
        this.HandleException(e.Exception);
    }

    /// <summary>
    /// 解包反射调用异常和仅含一个内部异常的聚合异常，保留多异常聚合供后续逐项处理。
    /// </summary>
    private static Exception UnwrapException(Exception exception)
    {
        while (true)
        {
            if (exception is TargetInvocationException { InnerException: not null } invocation)
                exception = invocation.InnerException;
            else if (exception is AggregateException aggregate && aggregate.InnerExceptions.Count == 1)
                exception = aggregate.InnerExceptions[0];
            else
                return exception;
        }
    }

    /// <summary>
    /// 判断异常是否允许由UI全局处理器标记为已处理；聚合异常中的每一项都必须满足条件。
    /// </summary>
    /// <remarks>此判断仅排除已知严重异常，不保证发生其他异常后应用状态仍然完整。</remarks>
    private static bool CanHandleException(Exception exception)
    {
        exception = UnwrapException(exception);
        if (exception is AggregateException aggregate)
            return aggregate.InnerExceptions.All(CanHandleException);
        return exception is not OutOfMemoryException and not StackOverflowException and not AccessViolationException;
    }

    /// <summary>
    /// 判断异常的Message是否可直接向用户展示，可重写以支持项目自定义业务异常。
    /// </summary>
    /// <remarks>业务异常消息应为友好说明，不应包含堆栈、凭据等敏感信息。</remarks>
    protected virtual bool IsBusinessException(Exception exception) => exception is BusinessException or ValidationException;

    /// <summary>
    /// 生成用户可见提示：业务异常显示Message，系统异常按类型返回友好说明。
    /// </summary>
    /// <param name="exception">已解包的单个异常。</param>
    /// <returns>不含异常堆栈的提示内容。</returns>
    protected virtual string GetExceptionMessage(Exception exception)
    {
        if (this.IsBusinessException(exception))
            return string.IsNullOrWhiteSpace(exception.Message) ? "操作未能完成，请检查输入后重试。" : exception.Message;
        return exception switch
        {
            UnauthorizedAccessException => "没有权限执行此操作，请检查访问权限。",
            FileNotFoundException or DirectoryNotFoundException => "所需文件或目录不存在，请检查配置。",
            IOException => "文件读写失败，请检查文件是否被占用及存储空间。",
            TimeoutException => "操作超时，请稍后重试。",
            _ => exception.Message
            //_ => "系统发生异常，详情请查看日志。"
        };
    }

    /// <summary>
    /// 统一记录原始异常，并在允许提示时异步展示；不阻塞异常发生线程等待Dispatcher。
    /// </summary>
    /// <param name="exception">保留内部异常及堆栈的原始异常。</param>
    /// <param name="terminating">是否按终止级别处理；为true时记录Fatal日志且不弹窗。</param>
    private void HandleException(Exception exception, bool terminating = false)
    {
        this.LogException(exception, terminating);
        if (!terminating)
            _ = this.ShowExceptionAsync(exception);
    }

    /// <summary>
    /// 记录完整异常；日志服务不可用或记录失败时回退到Trace，避免日志异常再次进入全局处理。
    /// </summary>
    private void LogException(Exception exception, bool terminating = false)
    {
        try
        {
            var logger = this.ILogger;
            if (logger == null)
                Trace.TraceError(exception.ToString());
            else if (terminating)
                logger.Fatal(exception);
            else
                logger.Error(exception);
        }
        catch (Exception loggingException)
        {
            Trace.WriteLine(exception);
            Trace.WriteLine(loggingException);
        }
    }

    /// <summary>
    /// 解包、分类并在UI线程异步提示异常，过滤操作取消并防止弹窗重入。
    /// </summary>
    /// <remarks>已有弹窗时不再展示新弹窗，但原始异常已由HandleException记录，不会丢失日志。</remarks>
    private async Task ShowExceptionAsync(Exception exception)
    {
        if (Interlocked.CompareExchange(ref _showingException, 1, 0) != 0)
            return;
        try
        {
            exception = UnwrapException(exception);
            var errors = exception is AggregateException aggregate
                ? aggregate.Flatten().InnerExceptions.Select(UnwrapException).ToArray()
                : new[] { exception };
            // 操作取消不作为用户错误提示；应用退出时不再创建新的弹窗。
            errors = errors.Where(x => x is not OperationCanceledException).ToArray();
            if (errors.Length == 0 || this.Dispatcher.HasShutdownStarted || this.Dispatcher.HasShutdownFinished)
                return;
            string message = string.Join(Environment.NewLine, errors.Select(this.GetExceptionMessage).Distinct());
            // 聚合异常全部为业务异常时才使用操作提示标题，混合异常仍标记为系统异常。
            string title = errors.All(this.IsBusinessException) ? "操作提示" : "系统异常";
            // Unwrap同时等待Dispatcher调度和弹窗任务，以便捕获异步展示过程中的异常。
            await this.Dispatcher.InvokeAsync(() => this.ShowExceptionMessageAsync(message, title)).Task.Unwrap();
        }
        catch (OperationCanceledException)
        {
            // 应用退出时，Dispatcher可能取消尚未执行的提示。
        }
        catch (Exception notificationException)
        {
            // 展示失败仅记录日志，不再次弹窗，避免递归异常提示。
            this.LogException(notificationException);
        }
        finally
        {
            Interlocked.Exchange(ref _showingException, 0);
        }
    }

    /// <summary>
    /// 在UI线程展示异常提示，可重写为自定义弹窗、通知或其他展示方式。
    /// </summary>
    /// <remarks>重写时应返回可等待的Task，避免使用async void使异常脱离上层保护。</remarks>
    /// <param name="message">已分类的用户提示内容。</param>
    /// <param name="title">操作提示或系统异常标题。</param>
    protected virtual async Task ShowExceptionMessageAsync(string message, string title)
    {
        await IocMessage.ShowDialogMessage(message, title);
    }

    #endregion

    #region - Exit -

    protected override void OnExit(ExitEventArgs e)
    {
        try
        {
            var disposes = Ioc.GetAssignableFromServices<IAppExitService>().Distinct();
            foreach (var item in disposes)
                item.Dispose();
            this.ILogger?.Info("系统退出");
            Ioc<IOperationService>.Instance?.Log<ApplicationBase>("系统推出");
        }
        catch (Exception ex)
        {
            this.ILogger?.Error(ex);
        }
        finally
        {
            this.UnregisterExceptionHandlers();
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
            this.ShowMessage(H.Extensions.ApplicationBase.Properties.Resources.Message_Singleton);
            this.Shutdown();
        }
    }

    #endregion

    public ILogService ILogger => Ioc.Services.GetService<ILogService>();
    protected virtual async void ShowMessage(string message, string title = null)
    {
        await IocMessage.ShowDialogMessage(message, title);
    }

    void ILoginableApplication.Login()
    {
        this.OnLogin();
    }

    void IConfigureableApplication.Configure()
    {
        this.Configure();
    }
}

public partial class ApplicationBase
{
    /// <summary>
    /// 依赖注入注册服务
    /// </summary>
    /// <param name="services"></param>
    protected virtual void ConfigureServices(IServiceCollection services)
    {

    }

    protected virtual void Configure(IApplicationBuilder app)
    {

    }
    protected void Configure()
    {
        ApplicationBuilder bulder = new ApplicationBuilder();
        this.Configure(bulder);
    }

    /// <summary>
    /// 加载启动页面
    /// </summary>
    protected virtual void OnSplashScreen(StartupEventArgs e)
    {
        ISplashScreenViewPresenter presenter = Ioc.Services.GetService<ISplashScreenViewPresenter>();
        int sleep = presenter?.SleepMicroseconds ?? 100;
        //  Do ： 在显示页面前需要加载主题，否则主题会出现变化
        var tls = Ioc.GetService<ILoadThemeOptionsService>(false);
        tls?.Load(out string message);
        Func<IDialog, ISplashScreenViewPresenter, bool?> func = (c, s) =>
        {

            int index = 0;
            IEnumerable<ISplashLoadable> loads = Ioc.GetAssignableFromServices<ISplashLoadable>().Distinct();
            IEnumerable<IDefaultTemplateable> templates = Ioc.GetAssignableFromServices<IDefaultTemplateable>().Distinct();
            int total = loads.Count() + templates.Count() + 1;
            {
                index++;
                if (s != null)
                    s.Message = $"[{index}/{total}]{H.Extensions.ApplicationBase.Properties.Resources.Message_LoadingSetting}...";
                //  Do ：加载设置参数
                string message = null;
                var r = IocSetting.Instance?.Load(x =>
                {
                    if (s != null)
                        s.Message = $"[{index}/{total}]{H.Extensions.ApplicationBase.Properties.Resources.Message_LoadingSetting}<{x.Name}>...";
                    if (x is IDefaultTemplateable templateable)
                    {
                        templateable.LoadDefaultTemplate();
                        //if (t.success)
                        //{
                        //    s.Message = $"[{index}/{total}]加载<{x.Name}>默认模板数据加载完成";
                        //    Thread.Sleep(100);
                        //}
                    }
                }, out message);
                if (r == false)
                    s.Message = message;
                Thread.Sleep(sleep);
            }

            {
                index++;
                foreach (IDefaultTemplateable item in templates)
                {
                    if (c?.IsCancel == true)
                        return null;
                    if (item == null)
                        continue;
                    if (s != null)
                        s.Message = $"[{index}/{total}]{H.Extensions.ApplicationBase.Properties.Resources.Message_LoadingTempate}<{item.Name}>...";
                    item.LoadDefaultTemplate();
                }
            }

            {

                foreach (ISplashLoadable load in loads)
                {
                    if (c?.IsCancel == true)
                        return null;

                    if (load == null)
                        continue;
                    index++;
                    if (s != null)
                        s.Message = $"[{index}/{total}]{H.Extensions.ApplicationBase.Properties.Resources.Message_Loading}{load.Name}...";
                    bool r = load.Load(out string message);
                    if (s != null && !string.IsNullOrEmpty(message) && !r)
                        s.Message = message;
                    if (r == false)
                    {
                        Thread.Sleep(sleep);
                        return false;
                    }
                    Thread.Sleep(sleep);
                }
            }

            if (s != null)
                s.Message = H.Extensions.ApplicationBase.Properties.Resources.Message_LoadSuccess;
            Thread.Sleep(sleep);
            return true;
        };
        if (presenter != null)
        {
            bool? r = this.Dispatcher.Invoke(() =>
            {
                return IocMessage.Window.ShowAction(presenter, x =>
                {
                    x.DialogButton = DialogButton.None;
                    x.Title = Assembly.GetEntryAssembly().GetName().Version.ToString();
                }, func).Result;
            });
            if (r == false)
            {
                IocLog.Info("启动失败，程序退出");
                this.Shutdown();
                return;
            }
        }
        else
        {
            bool? fr = func.Invoke(null, null);
            if (fr == false)
                throw new ArgumentException(H.Extensions.ApplicationBase.Properties.Resources.Message_LoadException);
        }
    }

    /// <summary>
    /// 加载登录页面
    /// </summary>
    protected virtual void OnLogin()
    {
        {
            ILoginViewPresenter presenter = Ioc.Services.GetService<ILoginViewPresenter>();
            if (presenter == null)
                return;
            bool? r = IocMessage.Window.Show(presenter, x =>
            {
                x.MinWidth = 400;
                x.DialogButton = DialogButton.None;
                x.Title = Assembly.GetEntryAssembly().GetName().Version.ToString();
            }).Result;
            if (r == false)
            {
                IocLog.Info("登录失败程序退出");
                this.Shutdown();
                return;
            }
        }

        {

            int sleep = 100;


            ILoginedSplashViewPresenter presenter = Ioc.Services.GetService<ILoginedSplashViewPresenter>();
            //  Do ： 在显示页面前需要加载主题，否则主题会出现变化
            var tls = Ioc.GetService<ILoadThemeOptionsService>(false);
            tls?.Load(out string message);
            Func<IDialog, ILoginedSplashViewPresenter, bool?> func = (c, s) =>
            {
                if (c?.IsCancel != true)
                {
                    if (s != null)
                        s.Message = $"{H.Extensions.ApplicationBase.Properties.Resources.Message_LoadException}...";
                    //  Do ：加载设置参数
                    string message = null;
                    var r = IocSetting.Instance?.LoadLoginedLoad(x =>
                    {
                        if (s != null)
                            s.Message = $"{H.Extensions.ApplicationBase.Properties.Resources.Message_LoadingSetting}<{x.Name}>...";
                        Thread.Sleep(20);
                    }, out message);
                    if (r == false)
                        s.Message = message;
                    Thread.Sleep(sleep);
                }



                IEnumerable<ILoginedSplashLoadable> loads = Ioc.GetAssignableFromServices<ISplashLoadable>().Distinct().OfType<ILoginedSplashLoadable>();

                {
                    IEnumerable<IDefaultTemplateable> templates = loads.OfType<IDefaultTemplateable>();
                    foreach (IDefaultTemplateable item in templates)
                    {
                        if (c?.IsCancel == true)
                            return null;
                        if (item == null)
                            continue;
                        s.Message = $"{H.Extensions.ApplicationBase.Properties.Resources.Message_LoadingTempate}<{item.Name}>...";
                        item.LoadDefaultTemplate();
                    }
                }

                int index = 0;
                foreach (ILoginedSplashLoadable load in loads)
                {
                    if (c?.IsCancel == true)
                        return null;

                    if (load == null)
                        continue;
                    index++;
                    if (s != null)
                        s.Message = $"[{index}/{loads.Count()}]{H.Extensions.ApplicationBase.Properties.Resources.Message_LoadingUser}<{load.Name}>...";
                    bool r = load.Load(out string message);
                    if (s != null && !string.IsNullOrEmpty(message))
                        s.Message = message;
                    if (r == false)
                    {
                        Thread.Sleep(sleep);
                        return false;
                    }
                    Thread.Sleep(sleep);
                }
                if (s != null)
                    s.Message = H.Extensions.ApplicationBase.Properties.Resources.Message_LoadSuccess
                ;
                Thread.Sleep(sleep);
                return true;
            };
            if (presenter != null)
            {
                bool? r = this.Dispatcher.Invoke(() =>
                {
                    return IocMessage.Window.ShowAction(presenter, x =>
                    {
                        x.DialogButton = DialogButton.None;
                        x.Title = Ioc<ILoginService>.Instance?.User?.Account ?? string.Empty;
                        x.MinHeight = 0.0;
                        x.Height = double.NaN;
                    }, func).Result;
                });
                if (r == false)
                {
                    IocLog.Info("加载用户数据异常");
                    this.Shutdown();
                    return;
                }
                if (r == null)
                {
                    IocLog.Info("用户取消登录加载用户数据操作");
                    this.Shutdown();
                    return;
                }
            }
            else
            {
                bool? fr = func.Invoke(null, null);
                if (fr == false)
                {
                    IocLog.Info("加载用户数据异常");
                    this.Shutdown();
                    return;
                }
            }
        }
    }

    protected abstract Window CreateMainWindow(StartupEventArgs e);


}
