// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.DataBases.Share;
using H.Extensions.ApplicationBase;
using H.Extensions.DataBase;
using H.Extensions.FontIcon;
using H.Modules.Identity;
using H.Modules.Login;
using H.Modules.Operation;
using H.Modules.Setting;
using H.Services.Setting;
using H.Styles;
using H.Styles.Controls;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace H.Test.Globalization;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : ApplicationBase
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddApplicationServices();
        services.AddIdentifyDefaultServices();
        services.AddGlobalization();

    }

    protected override Window CreateMainWindow(StartupEventArgs e)
    {
        return new MainWindow();
    }

    protected override void Configure(IApplicationBuilder app)
    {
        base.Configure(app);
        app.UseGlobalizations(x =>
        {

        });

        app.UseApplicationOptions();
    }
}
