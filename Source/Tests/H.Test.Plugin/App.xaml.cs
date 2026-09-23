// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.ApplicationBase;
using H.Modules.Plugin;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;

namespace H.Test.Plugin;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : H.Extensions.ApplicationBase.ApplicationBase
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddPlugin();
    }

    protected override System.Windows.Window CreateMainWindow(StartupEventArgs e)
    {
        return new MainWindow();
    }

}
