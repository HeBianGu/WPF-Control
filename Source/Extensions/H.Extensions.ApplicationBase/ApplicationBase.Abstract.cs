using H.Extensions.Common;
using H.Providers.Ioc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace H.Extensions.ApplicationBase
{
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

        /// <summary>
        /// 加载启动页面
        /// </summary>
        protected virtual void OnSplashScreen(StartupEventArgs e)
        {
            int sleep = 1000;
            var presenter = Ioc.Services.GetService<ISplashScreenViewPresenter>();
            Func<IDialog, ISplashScreenViewPresenter, bool?> func = (c, s) =>
            {
                if (c?.IsCancel != true)
                {
                    if (s != null)
                        s.Message = "正在加载设置数据...";
                    //  Do ：加载设置参数
                    bool r = SettingDataManager.Instance.Load(out string message);
                    if (r == false)
                        s.Message = message;
                    Thread.Sleep(sleep);
                }

                if (c?.IsCancel != true)
                {
                    System.Collections.Generic.IEnumerable<ISplashLoad> loads = Ioc.Services.GetServices<ISplashLoad>();
                    foreach (ISplashLoad load in loads)
                    {
                        if (load == null)
                            continue;

                        if (s != null)
                            s.Message = $"正在加载{load.Name}...";
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
                bool? r = IocMessage.Window.ShowAction(presenter, x =>
                {
                    x.DialogButton = DialogButton.None;
                    x.Title = ApplicationProvider.Version;
                    x.Width = 500;
                    x.Height = 300;
                    if (x is Window w)
                        w.SizeToContent = SizeToContent.Manual;
                }, x => func.Invoke(x, presenter)).Result;
                if (r == false)
                {
                    IocLog.Instance?.Info("启动失败，程序退出");
                    this.Shutdown();
                    return;
                }
            }
            else
            {
                bool r = SettingDataManager.Instance.Load(out string message);
                if (r == false)
                    IocMessage.Window.Show(message);
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
            var presenter = Ioc.Services.GetService<ILoginViewPresenter>();
            if (presenter == null)
                return;
            bool? r = IocMessage.Window.Show(presenter, x =>
            {
                x.Width = 400;
                x.DialogButton = DialogButton.Cancel;
                x.Title = ApplicationProvider.Version;
            }).Result;
            if (r == false)
            {
                IocLog.Instance?.Info("登录失败程序退出");
                this.Shutdown();
                return;
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
            //    SettingDataManager.Instance.Add(settings.ToArray());
            //}
            ////  Do ：加载Option中的ISetting
            //{
            //    var settings = Ioc.Services.GetServices(typeof(IOptions<>)).OfType<ISetting>();
            //    SettingDataManager.Instance.Add(settings.ToArray());
            //}
            //ConcurrentDictionary<Type,Func<ServiceProviderEngine>>

            //Microsoft.Extensions.DependencyInjection
        }
    }

}
