using H.Extensions.Common;
using H.Services.Common;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;

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

        protected virtual IEnumerable<ISplashLoad> GetSplashLoads()
        {
            foreach (var item in Ioc.Services.GetServices<IDbConnectService>())
            {
                yield return item;
            }

            foreach (var item in System.Ioc.Services.GetServices<ISplashLoad>())
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
            var presenter = Ioc.Services.GetService<ISplashScreenViewPresenter>();
            Func<IDialog, ISplashScreenViewPresenter, bool?> func = (c, s) =>
            {
                if (c?.IsCancel != true)
                {
                    if (s != null)
                        s.Message = "正在加载设置数据...";
                    //  Do ：加载设置参数
                    bool r = SettingDataManager.Instance.Load(x =>
                    {
                        if (s != null)
                            s.Message = $"正在加载设置<{x.Name}>数据...";
                    }, out string message);
                    if (r == false)
                        s.Message = message;
                    Thread.Sleep(sleep);
                }

                {
                    int index = 0;
                    var loads = Ioc.GetAssignableFromServices<ISplashLoad>().Distinct();
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
                         x.Title = ApplicationProvider.Version;
                         x.Width = 500;
                         x.Height = 300;
                         if (x is Window w)
                             w.SizeToContent = SizeToContent.Manual;
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
                //bool r = SettingDataManager.Instance.Load(null, out string message);
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
                var presenter = System.Ioc.Services.GetService<ILoginViewPresenter>();
                if (presenter == null)
                    return;
                bool? r = IocMessage.Window.Show(presenter, x =>
                {
                    x.MinWidth = 400;
                    x.DialogButton = DialogButton.None;
                    x.Title = ApplicationProvider.Version;
                    if (x is Window w)
                        w.SizeToContent = SizeToContent.WidthAndHeight;
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
                var presenter = Ioc.Services.GetService<ILoginedSplashViewPresenter>();
                Func<IDialog, ILoginedSplashViewPresenter, bool?> func = (c, s) =>
                {
                    if (c?.IsCancel != true)
                    {
                        if (s != null)
                            s.Message = "正在加载用户设置数据...";
                        //  Do ：加载设置参数
                        bool r = SettingDataManager.Instance.LoadLoginedLoad(x =>
                        {
                            if (s != null)
                                s.Message = $"正在加载设置<{x.Name}>数据...";
                            Thread.Sleep(20);
                        }, out string message);
                        if (r == false)
                            s.Message = message;
                        Thread.Sleep(sleep);
                    }

                    var loads = Ioc.GetAssignableFromServices<ISplashLoad>().Distinct().OfType<ILoginedSplashLoad>();
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
                                w.SizeToContent = SizeToContent.Height;
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
