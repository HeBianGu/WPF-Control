﻿using H.Controls.FavoriteBox;
using H.Controls.TagBox;
using H.DataBases.Share;
using H.Extensions.ApplicationBase;
using H.Modules.Identity;
using H.Modules.Operation;
using H.Services.Common;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using System.Windows.Media;
using H.Extensions.DataBase;
using H.Styles.Default;

namespace H.App.FileManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : ApplicationBase
    {
        protected override Window CreateMainWindow(StartupEventArgs e)
        {
            return new MainWindow();
        }

        protected override void ConfigureServices(IServiceCollection services)
        {
            services.AddWindowMessage();
            services.AddAdornerDialogMessage();
            services.AddSnackMessage();
            services.AddFormMessageService();
            services.AddProject<FileProjectService>();
            services.AddSplashScreen();
            services.AddSingleton<IAppSaveService, AppSaveService>();
            services.AddTag<ProjectTagService>(x =>
            {
                x.Tags.Add(new Tag() { Name = "严重", Description = "这是一个严重标签", Background = Brushes.Purple });
                x.Tags.Add(new Tag() { Name = "错误", Description = "这是一个严重标签", Background = Brushes.Red });
                x.Tags.Add(new Tag() { Name = "警告", Description = "这是一个严重标签", Background = Brushes.Orange });
                x.Tags.Add(new Tag() { Name = "运行", Description = "这是一个严重标签", Background = Brushes.Blue });
                x.Tags.Add(new Tag() { Name = "成功", Description = "这是一个严重标签", Background = Brushes.Green });

                x.Tags.Add(new Tag() { Name = "刘德华", GroupName = "Object", Description = "这是一个严重标签", Background = Brushes.Purple });
                x.Tags.Add(new Tag() { Name = "范伟", GroupName = "Object", Description = "这是一个严重标签", Background = Brushes.Red });
                x.Tags.Add(new Tag() { Name = "张娜拉", GroupName = "Object", Description = "这是一个严重标签", Background = Brushes.Orange });
                x.Tags.Add(new Tag() { Name = "欧阳娜娜", GroupName = "Object", Description = "这是一个严重标签", Background = Brushes.Blue });
                x.Tags.Add(new Tag() { Name = "刘恺威", GroupName = "Object", Description = "这是一个严重标签", Background = Brushes.Green });

                x.Tags.Add(new Tag() { Name = "中国", GroupName = "Area", Description = "这是一个严重标签", Background = Brushes.Purple });
                x.Tags.Add(new Tag() { Name = "日本", GroupName = "Area", Description = "这是一个严重标签", Background = Brushes.Red });
                x.Tags.Add(new Tag() { Name = "韩国", GroupName = "Area", Description = "这是一个严重标签", Background = Brushes.Orange });
                x.Tags.Add(new Tag() { Name = "欧美", GroupName = "Area", Description = "这是一个严重标签", Background = Brushes.Blue });
                x.Tags.Add(new Tag() { Name = "泰国", GroupName = "Area", Description = "这是一个严重标签", Background = Brushes.Green });

                x.Tags.Add(new Tag() { Name = "超高清", GroupName = "Articulation", Description = "这是一个严重标签", Background = Brushes.Purple });
                x.Tags.Add(new Tag() { Name = "高清", GroupName = "Articulation", Description = "这是一个严重标签", Background = Brushes.Red });
                x.Tags.Add(new Tag() { Name = "标清", GroupName = "Articulation", Description = "这是一个严重标签", Background = Brushes.Orange });
                x.Tags.Add(new Tag() { Name = "模糊", GroupName = "Articulation", Description = "这是一个严重标签", Background = Brushes.Blue });
                x.Tags.Add(new Tag() { Name = "差劲", GroupName = "Articulation", Description = "这是一个严重标签", Background = Brushes.Green });
            });

            services.AddFavorite<ProjectFavoriteService>(x =>
            {
                x.FavoriteItems.Add(new FavoriteItem() { Path = "默认" });
                x.FavoriteItems.Add(new FavoriteItem() { Path = "资源" });
                x.FavoriteItems.Add(new FavoriteItem() { Path = "学习" });
                x.FavoriteItems.Add(new FavoriteItem() { Path = "娱乐" });
                x.FavoriteItems.Add(new FavoriteItem() { Path = "工作" });
            });
            services.AddSetting();
            services.AddAppService();
            services.AddTorrent();
            services.AddFFMpeg(x =>
            {
                x.WorkingDirectory = "D:\\ffmpeg";
            });
            services.AddSwitchThemeViewPresenter();

            services.AddSingleton<IScheduledTaskService, ProjectSaveScheduledTaskService>();


            //  Do ：身份认证
            services.AddDbContextBySetting<IdentifyDataContext>();
            services.AddSingleton<IStringRepository<hi_dd_user>, DbContextRepository<IdentifyDataContext, hi_dd_user>>();
            services.AddUserViewPresenter();

            services.AddSingleton<IStringRepository<hi_dd_role>, DbContextRepository<IdentifyDataContext, hi_dd_role>>();
            services.AddRoleViewPresenter();

            services.AddSingleton<IStringRepository<hi_dd_author>, DbContextRepository<IdentifyDataContext, hi_dd_author>>();
            services.AddAuthorityViewPresenter();

            //  Do ：操作日志
            services.AddDbContextBySetting<OperationDataContext>();
            services.AddSingleton<IStringRepository<hi_dd_operation>, DbContextRepository<OperationDataContext, hi_dd_operation>>();
            services.AddOperationViewPresenter();

            //  Do ：登录和注册页面
            services.AddRegisterLoginViewPresenter();
            services.AddLoginService();
            services.AddRegisterService();
        }

        protected override void Configure(IApplicationBuilder app)
        {
            base.Configure(app);

            app.UseSettingDataManager(x =>
            {
                x.Add(AppSetting.Instance);
            });

            //app.UseVlc(x =>
            //{
            //    //x.LibvlcPath = "G:\\BaiduNetdiskDownload\\libvlc\\win-x64";
            //});

            app.UseFFMpeg();

            app.UseLogin();

            app.UseWindowSetting();
            app.UseTheme();
            app.UseSwithTheme();
            //app.UseSetting();
            app.UseSettingSecurity();

        }
    }
}
