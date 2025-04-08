using H.Controls.FavoriteBox;
using H.Controls.TagBox;
using H.DataBases.Share;
using H.Extensions.ApplicationBase;
using H.Modules.Identity;
using H.Modules.Operation;
using H.Services.Common;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Media;
using H.Extensions.DataBase;
using H.Styles;
using H.Services.Common.Schedule;
using H.ApplicationBases.Default;

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
            base.ConfigureServices(services);
            services.AddApplicationServices();
            services.AddProject<FileProjectService>();
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
            services.AddAppService();
            services.AddTorrent();
            services.AddFFMpeg(x =>
            {
                x.WorkingDirectory = "D:\\ffmpeg";
            });
            services.AddSingleton<IScheduledTaskService, ProjectSaveScheduledTaskService>();
            services.AddDefaultIdentify();
        }

        protected override void Configure(IApplicationBuilder app)
        {
            base.Configure(app);
            app.UseApplicationOptions();
            app.UseDefaultIdentifyOptions();
            app.UseSettingDataOptions(x =>
            {
                x.Add(AppSetting.Instance);
            });
            app.UseFFMpeg();
            app.UseWindowSetting();
        }
    }
}
