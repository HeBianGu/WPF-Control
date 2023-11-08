using H.Extensions.Common;
using H.Providers.Ioc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace H.Extensions.ApplicationBase
{
    partial class ApplicationBase
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
            var exist = Ioc.Exist<ISplashScreenViewPresenter>();
            if (exist)
            {
                Func<ICancelable, ISplashScreenViewPresenter, bool?> func = (c, s) =>
                {
                    if (c.IsCancel == false)
                    {
                        ILoad load = Ioc.GetService<IDbConnectService>(false);
                        if (load != null)
                        {
                            s.Message = $"正在加载{load.Name}...";
                            var r = load.Load(out string message);
                            if (r == false)
                            {
                                s.Message = message;
                                Thread.Sleep(sleep);
                                return false;
                            }
                            Thread.Sleep(sleep);
                        }
                    }

                    if (c.IsCancel == false)
                    {
                        s.Message = "正在加载设置数据...";
                        //  Do ：加载设置参数
                        var r = SettingDataManager.Instance.Load(out var message);
                        if (r == false)
                        {
                            s.Message = message;
                        }

                        Thread.Sleep(sleep);
                    }

                    s.Message = "加载完成";
                    Thread.Sleep(sleep);
                    return true;
                };
                var r = IocMessage.Dialog.ShowIoc(func, x =>
                {
                    if (x is Control c)
                    {
                        c.Width = 500;
                        c.Height= 300;
                    }
                    if (x is Window w)
                    {
                        w.SizeToContent = SizeToContent.Manual;
                    }
                }, DialogButton.None, ApplicationProvider.Version).Result;
                if (r == false)
                {
                    Logger.Instance?.Info("启动失败，程序退出");
                    this.Shutdown();
                    return;
                }
            }
            else
            {
                var r = SettingDataManager.Instance.Load(out var message);
                if (r == false)
                    IocMessage.Dialog.ShowMessage(message);
            }
        }

        /// <summary>
        /// 加载登录页面
        /// </summary>
        protected virtual void OnLogin(StartupEventArgs e)
        {
            var exist = Ioc.Exist<ILoginViewPresenter>();
            if (exist == false)
                return;

            var r = IocMessage.Dialog.ShowIoc<ILoginViewPresenter>(null, x =>
            {
                if (x is Control c)
                    c.Width = 400;
            }, DialogButton.None, ApplicationProvider.Version).Result;
            if (r == false)
            {
                Logger.Instance?.Info("登录失败程序退出");
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
