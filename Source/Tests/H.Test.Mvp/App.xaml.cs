﻿using H.Extensions.ApplicationBase;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;

namespace H.Test.Mvp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : ApplicationBase
    {
        protected override void ConfigureServices(IServiceCollection services)
        {
            services.AddAbout(x => x.ProductName = "设置标题");
            services.AddSetting();
            services.AddFeedBack();
            services.AddSwitchThemeViewPresenter();
        }

        protected override Window CreateMainWindow(StartupEventArgs e)
        {
            return new MainWindow();
        }

        protected override void Configure(IApplicationBuilder app)
        {
            base.Configure(app);

            app.UseSettingDefault();
        }
    }
}
