using H.Common.Interfaces;
using H.Extensions.AppPath;
using H.Extensions.Attach;
using H.Services.AppPath;
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
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Windows;

namespace H.Extensions.ApplicationBase;

public abstract partial class ApplicationBase : Application, IConfigureableApplication
{
    public ApplicationBase()
    {
        AppPaths.Register(this.CreateAppPathServce());
        this.InitExcetion();
        this.InitServiceCollection();
    }

    protected void InitServiceCollection()
    {
        ServiceCollection sc = new ServiceCollection();
        this.ConfigureServices(sc);
        Ioc.Build(sc);
        this.OnSetting();
    }

    protected virtual IAppPathServce CreateAppPathServce()
    {
        return new AppPathServce();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        this.Configure();
        this.OnSingleton(e);
        base.OnStartup(e);
        Window window = this.CreateMainWindow(e);
        this.OnSplashScreen(e);
        this.OnLogin(e);
        Ioc<IMainWindowSavableService>.Instance?.Load(window);
        this.MainWindow.Show();
        this.ILogger?.Info("系统启动");
        Ioc<IScheduledTaskService>.Instance?.Start();
    }

    #region - Exception -
    protected virtual void InitExcetion()
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
    protected virtual async void ShowMessage(string message, string title = "提示")
    {
        await IocMessage.ShowDialogMessage(message,title);
        //MessageBox.Show(message);

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
    public void Configure()
    {
        ApplicationBuilder bulder = new ApplicationBuilder();
        this.Configure(bulder);
    }

    protected virtual IEnumerable<ISplashLoad> GetSplashLoads()
    {
        foreach (IDbConnectService item in Ioc.Services.GetServices<IDbConnectService>())
        {
            yield return item;
        }

        foreach (ISplashLoad item in Ioc.Services.GetServices<ISplashLoad>())
        {
            yield return item;
        }
    }

    /// <summary>
    /// 加载启动页面
    /// </summary>
    protected virtual void OnSplashScreen(StartupEventArgs e)
    {
        int sleep = 1000;
        ISplashScreenViewPresenter presenter = Ioc.Services.GetService<ISplashScreenViewPresenter>();
        //  Do ： 在显示页面前需要加载主题，否则主题会出现变化
        var tls = Ioc.GetService<ILoadThemeOptionsService>(false);
        tls?.Load(out string message);
        Func<IDialog, ISplashScreenViewPresenter, bool?> func = (c, s) =>
        {
            if (c?.IsCancel != true)
            {
                if (s != null)
                    s.Message = "正在加载设置数据...";
                //  Do ：加载设置参数
                string message = null;
                var r = IocSetting.Instance?.Load(x =>
                {
                    if (s != null)
                        s.Message = $"正在加载设置<{x.Name}>数据...";
                }, out message);
                if (r == false)
                    s.Message = message;
                Thread.Sleep(sleep);
            }

            {
                int index = 0;
                IEnumerable<ISplashLoad> loads = Ioc.GetAssignableFromServices<ISplashLoad>().Distinct();
                foreach (ISplashLoad load in loads)
                {
                    if (c?.IsCancel == true)
                        return null;

                    if (load == null)
                        continue;
                    index++;
                    if (s != null)
                        s.Message = $"[{index}/{loads.Count()}]正在加载{load.Name}...";
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
            }

            if (s != null)
                s.Message = "加载完成";
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
                    x.Width = 500;
                    x.Height = 300;
                    if (x is Window w)
                    {
                        w.SizeToContent = SizeToContent.Manual;
                        Cattach.SetCaptionBackground(w, null);
                    }
                }, func).Result;
            });
            if (r == false)
            {
                IocLog.Instance?.Info("启动失败，程序退出");
                this.Shutdown();
                return;
            }
        }
        else
        {
            //bool r = IocSetting.Instance.Load(null, out string message);
            //if (r == false)
            //    IocMessage.Window.Show(message);
            bool? fr = func.Invoke(null, null);
            if (fr == false)
                throw new ArgumentException("初始化数据异常，请看日志");
        }
    }

    /// <summary>
    /// 加载登录页面
    /// </summary>
    protected virtual void OnLogin(StartupEventArgs e)
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
                if (x is Window w)
                {
                    w.SizeToContent = SizeToContent.WidthAndHeight;
                    w.ShowInTaskbar = true;
                    Cattach.SetCaptionBackground(w, null);
                }
            }).Result;
            if (r == false)
            {
                IocLog.Instance?.Info("登录失败程序退出");
                this.Shutdown();
                return;
            }
        }

        {

            int sleep = 1000;
            ILoginedSplashViewPresenter presenter = Ioc.Services.GetService<ILoginedSplashViewPresenter>();
            //  Do ： 在显示页面前需要加载主题，否则主题会出现变化
            var tls = Ioc.GetService<ILoadThemeOptionsService>(false);
            tls?.Load(out string message);
            Func<IDialog, ILoginedSplashViewPresenter, bool?> func = (c, s) =>
            {
                if (c?.IsCancel != true)
                {
                    if (s != null)
                        s.Message = "正在加载用户设置数据...";
                    //  Do ：加载设置参数
                    string message = null;
                    var r = IocSetting.Instance?.LoadLoginedLoad(x =>
                    {
                        if (s != null)
                            s.Message = $"正在加载设置<{x.Name}>数据...";
                        Thread.Sleep(20);
                    }, out message);
                    if (r == false)
                        s.Message = message;
                    Thread.Sleep(sleep);
                }

                IEnumerable<ILoginedSplashLoad> loads = Ioc.GetAssignableFromServices<ISplashLoad>().Distinct().OfType<ILoginedSplashLoad>();
                int index = 0;
                foreach (ILoginedSplashLoad load in loads)
                {
                    if (c?.IsCancel == true)
                        return null;

                    if (load == null)
                        continue;
                    index++;
                    if (s != null)
                        s.Message = $"[{index}/{loads.Count()}]正在加载用户数据<{load.Name}>...";
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
                    s.Message = "用户数据加载完成";
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
                        x.Title = Ioc<ILoginService>.Instance?.User?.Account;
                        //x.Width = 500;
                        x.MinHeight = 0.0;
                        x.Height = double.NaN;
                        if (x is Window w)
                        {
                            w.SizeToContent = SizeToContent.Height;
                            Cattach.SetCaptionBackground(w, null);
                        }
                    }, func).Result;
                });
                if (r == false)
                {
                    IocLog.Instance?.Info("加载用户数据异常");
                    this.Shutdown();
                    return;
                }
                if (r == null)
                {
                    IocLog.Instance?.Info("用户取消登录加载用户数据操作");
                    this.Shutdown();
                    return;
                }
            }
            else
            {
                bool? fr = func.Invoke(null, null);
                if (fr == false)
                {
                    IocLog.Instance?.Info("加载用户数据异常");
                    this.Shutdown();
                    return;
                }
            }
        }
    }

    protected abstract Window CreateMainWindow(StartupEventArgs e);

    /// <summary>
    /// 加载注入的配置信息
    /// </summary>
    protected virtual void OnSetting()
    {

        //var sss=   Ioc.Services.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Static|bind);

        ////  Do ：加载注入的ISetting
        //{
        //    var settings = Ioc.Services.GetServices<ISetting>();
        //    IocSetting.Instance.Add(settings.ToArray());
        //}
        ////  Do ：加载Option中的ISetting
        //{
        //    var settings = Ioc.Services.GetServices(typeof(IOptions<>)).OfType<ISetting>();
        //    IocSetting.Instance.Add(settings.ToArray());
        //}
        //ConcurrentDictionary<Type,Func<ServiceProviderEngine>>

        //Microsoft.Extensions.DependencyInjection
    }
}
