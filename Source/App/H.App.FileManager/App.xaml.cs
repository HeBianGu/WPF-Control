using H.Controls.TagBox;
using H.DataBases.Share;
using H.Extensions.ApplicationBase;
using H.Extensions.ViewModel;
using H.Modules.Login;
using H.Modules.Project;
using H.Providers.Ioc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

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
            services.AddDbContextBySetting<DataContext>();
            services.AddSingleton<IStringRepository<fm_dd_file>, DbContextRepository<DataContext, fm_dd_file>>();
            services.AddSingleton<IRepositoryViewModel<fm_dd_file>, FileRepositoryViewModel>();
            services.AddTag(x =>
            {
                x.Tags.Add(new Tag() { Name = "严重", Description = "这是一个严重标签", Background = Brushes.Purple });
                x.Tags.Add(new Tag() { Name = "错误", Description = "这是一个严重标签", Background = Brushes.Red });
                x.Tags.Add(new Tag() { Name = "警告", Description = "这是一个严重标签", Background = Brushes.Orange });
                x.Tags.Add(new Tag() { Name = "运行", Description = "这是一个严重标签", Background = Brushes.Blue });
                x.Tags.Add(new Tag() { Name = "成功", Description = "这是一个严重标签", Background = Brushes.Green });
            });
        }
    }

    public class FileProjectService : ProjectServiceBase, IProjectService
    {
        public FileProjectService(IOptions<ProjectOptions> options) : base(options)
        {

        }

        public IProjectItem Create()
        {
            return new FileProjectItem()
            {
                Path = SystemPathSetting.Instance.Project
            };
        }
    }

    public class FileProjectItem : ProjectItemBase
    {
        [Required]
        [Display(Name = "文件路径", Order = 2)]
        public string BaseFolder { get; set; }
    }
}
