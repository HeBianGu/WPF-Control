﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using Microsoft.Extensions.DependencyInjection;
using H.Extensions.ApplicationBase;
using H.Styles.Default;

namespace H.Test.SideMenu
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : ApplicationBase
    {
        protected override void ConfigureServices(IServiceCollection services)
        {
            services.AddAdornerDialogMessage();
            services.AddSetting();
        }

        protected override void Configure(IApplicationBuilder app)
        {
            app.UseSettingDataManager(x =>
            {
                x.Add(AppSetting.Instance);
            });

            app.UseWindowSetting();
        }

        protected override Window CreateMainWindow(StartupEventArgs e)
        {
            return new MainWindow();
        }
    }
}
