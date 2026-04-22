// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.App.AIDI.Base;
using H.App.AIDI.DB;
using H.App.AIDI.ViewModel;
using H.Controls.TagBox;
using H.DataBases.Share;
using H.Extensions.ApplicationBase;
using H.Extensions.DataBase.Repository;
using H.Modules.Home.Presenters;
using Microsoft.Extensions.DependencyInjection;

namespace H.App.AIDI;
public partial class App : ApplicationBase
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddApplicationServices();
        services.AddProject<AIDIProjectService>(x => x.UseOpenCurrentOnLoad = false);
        services.AddHome<ProjectThumbnialHomeViewPresenter>();
        services.AddProjectSplashSave();

        services.AddTag<ProjectTagService>(x =>
        {
            x.Tags.Add(new Tag() { Name = "训练数据", Description = "训练数据数据", Background = Brushes.Green });
            x.Tags.Add(new Tag() { Name = "测试数据", Description = "测试数据数据", Background = Brushes.Gray });
            x.Tags.Add(new Tag() { Name = "验证数据", Description = "验证数据数据", Background = Brushes.LightSkyBlue });
        });

        services.AddDbContextBySetting<AIDIDataContext>();
        services.AddSingleton<IStringRepository<fm_dd_image>, DbContextRepository<AIDIDataContext, fm_dd_image>>();
        services.AddSingleton<IRepositoryBindable<fm_dd_image>, AIDIRepositoryBindable>();
    }

    protected override void Configure(IApplicationBuilder app)
    {
        app.UseApplicationOptions();
    }

    protected override Window CreateMainWindow(StartupEventArgs e)
    {
        return new MainWindow();
    }
}
