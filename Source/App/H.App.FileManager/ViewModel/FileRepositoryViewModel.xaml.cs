using H.Extensions.Common;
using H.Extensions.ViewModel;
using H.Providers.Ioc;
using H.Providers.Mvvm;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;
using System.Windows.Input;
using System.Xml.Serialization;

namespace H.App.FileManager
{
    public class FileRepositoryViewModel : RepositoryViewModel<fm_dd_file>
    {
        public FileRepositoryViewModel()
        {
            this.UseMessage = false;
            this.Collection.PageCount = 100;

            this.UpdateCommands = this.Commands.OfType<IRelayCommand>().Where(x => x.GroupName == "更新").ToObservable();
            this.MenuCommands = this.Commands.OfType<IRelayCommand>().Where(x => x.GroupName == "菜单").ToObservable();
            this.MoreCommands = this.Commands.OfType<IRelayCommand>().Where(x => string.IsNullOrEmpty(x.GroupName)).ToObservable();
        }
        public override void RefreshData(params string[] includes)
        {
            includes = includes ?? GetIncludes()?.ToArray();
            IEnumerable<SelectViewModel<fm_dd_file>> collection = includes == null ? Repository.GetList().Select(x => new SelectViewModel<fm_dd_file>(x))
            : Repository.GetList(includes).Select(x => new SelectViewModel<fm_dd_file>(x));
            Collection.Load(collection);
        }

        [Browsable(false)]
        [JsonIgnore]
        [XmlIgnore]
        public ObservableCollection<IRelayCommand> UpdateCommands { get; } = new ObservableCollection<IRelayCommand>();

        [Browsable(false)]
        [JsonIgnore]
        [XmlIgnore]
        public ObservableCollection<IRelayCommand> MoreCommands { get; private set; } = new ObservableCollection<IRelayCommand>();

        [Browsable(false)]
        [JsonIgnore]
        [XmlIgnore]
        public ObservableCollection<IRelayCommand> MenuCommands { get; private set; } = new ObservableCollection<IRelayCommand>();

        [Display(Name = "重新加载文件", GroupName = "更新")]
        public RelayCommand RefreshCommand => new RelayCommand(async (s, e) =>
        {
            if (IocProject.Instance.Current is FileProjectItem projectItem)
            {
                Random random = new Random();
                var r = await await IocMessage.Dialog.ShowString(async (c, x) =>
                   {
                       x.Value = "正在加载数据...";
                       var files = projectItem.BaseFolder.GetAllFiles();
                       Repository.Clear();

                       List<fm_dd_file> dbFiles = new List<fm_dd_file>();
                       foreach (var file in files)
                       {
                           fm_dd_file fm_Dd_File = null;
                           if (file.IsImage())
                           {
                               fm_dd_image image = new fm_dd_image();
                               image.PixelHeight = 200;
                               image.PixelWidth = 400;
                               fm_Dd_File = image;
                           }
                           else if (file.IsVedio())
                           {
                               fm_dd_video video = new fm_dd_video();
                               //video.Images.Add(new fm_dd_video_image() { Name = "缩率图", Url = "C:\\Users\\LENOVO\\Pictures\\OpenCV\\0.jpg" });
                               //video.Images.Add(new fm_dd_video_image() { Name = "缩率图", Url = "C:\\Users\\LENOVO\\Pictures\\OpenCV\\0.jpg" });
                               //video.Images.Add(new fm_dd_video_image() { Name = "缩率图", Url = "C:\\Users\\LENOVO\\Pictures\\OpenCV\\0.jpg" });
                               //video.Images.Add(new fm_dd_video_image() { Name = "缩率图", Url = "C:\\Users\\LENOVO\\Pictures\\OpenCV\\0.jpg" });
                               //video.Images.Add(new fm_dd_video_image() { Name = "缩率图", Url = "C:\\Users\\LENOVO\\Pictures\\OpenCV\\0.jpg" });
                               fm_Dd_File = video;
                           }
                           else if (file.IsAudio())
                           {
                               fm_Dd_File = new fm_dd_audio();
                           }
                           else
                           {
                               fm_Dd_File = new fm_dd_file();
                           }
                           fm_Dd_File.Name = Path.GetFileNameWithoutExtension(file);
                           fm_Dd_File.Url = Path.GetFullPath(file);
                           fm_Dd_File.Extend = Path.GetExtension(file);
                           fm_Dd_File.Size = new FileInfo(file).Length;
                           //fm_Dd_File.Score = random.Next(0, 10).ToString();
                           //fm_Dd_File.Favorite = random.Next(10) == 1;
                           //fm_Dd_File.Project = Ioc.GetService<IProjectService>()?.Current?.Title;

                           dbFiles.Add(fm_Dd_File);
                       }
                       await Add(dbFiles.ToArray());
                       x.Value = "加载完成，正在保存...";
                       return await Save();
                   });
                if (r > 0)
                    IocMessage.Snack.ShowInfo("保存完成");
                else
                    IocMessage.Snack.ShowError("保存失败");
            }
        }, (s, e) => IocProject.Instance.Current is FileProjectItem);


        [Display(Name = "打开文件夹", GroupName = "更新")]
        public RelayCommand OpenForderCommand { get; set; } = new RelayCommand(l =>
        {
            if (l is string project)
            {

            }
        });

        [Display(Name = "复制")]

        public RelayCommand CopyCommand { get; set; } = new RelayCommand(l =>
        {
            if (l is string project)
            {

            }
        });

        [Display(Name = "另存为")]
        public RelayCommand SaveAsCommand { get; set; } = new RelayCommand(l =>
        {
            if (l is string project)
            {

            }
        });

        [Display(Name = "打开文件夹", GroupName = "菜单")]
        public RelayCommand LoadVideoImageCommand { get; set; } = new RelayCommand(l =>
        {

        });

        [Display(Name = "更新视频信息", GroupName = "更新")]
        public RelayCommand UpdateVieoInfoCommand { get; set; } = new RelayCommand(l =>
        {
            if (l is string project)
            {

            }
        });

        [Display(Name = "更新视频缩率图", GroupName = "更新")]
        public RelayCommand UpdateVediogImageCommand => new RelayCommand(l =>
        {
            foreach (var item in this.Collection.FilterSource.Select(x => x.Model).OfType<fm_dd_video>())
            {
                var dir = Path.GetDirectoryName(item.Url);
                var images = Directory.GetFiles(dir).Where(x => x.IsImage() && Path.GetFileNameWithoutExtension(x).StartsWith(item.Name));
                item.Images.Clear();
                foreach (var image in images)
                {
                    item.Images.Add(new fm_dd_video_image() { Url = image, Name = Path.GetFileNameWithoutExtension(item.Name) });
                }
                item.SelectedImageIndex = 0;
            }
        });
        //}, x => this.Collection.FilterSource.OfType<fm_dd_video>().Count() > 0);


        [Display(Name = "根据名称加载标签", GroupName = "更新")]
        public RelayCommand UpdateTagCommand => new RelayCommand(l =>
        {

        });

        [Display(Name = "移除不存在的文件", GroupName = "更新")]
        public RelayCommand RemoveAbsentFileCommand { get; set; } = new RelayCommand(l =>
        {
            if (l is string project)
            {

            }
        });

        [Display(Name = "彻底删除选中文件")]
        public RelayCommand DeleteSelectedFileCommand { get; set; } = new RelayCommand(l =>
        {
            if (l is string project)
            {

            }
        });

        [Display(Name = "彻底删除当前筛选的文件", GroupName = "更新")]
        public RelayCommand DeleteFilterFilesCommand { get; set; } = new RelayCommand(l =>
        {
            if (l is string project)
            {

            }
        });

        [Display(Name = "移除当前筛选的文件", GroupName = "更新")]
        public RelayCommand RemoveFilterFilesCommand { get; set; } = new RelayCommand(l =>
        {
            if (l is string project)
            {

            }
        });

        [Display(Name = "收藏", GroupName = "菜单")]
        public RelayCommand FavoriteCommand { get; set; } = new RelayCommand(l =>
        {
            if (l is string project)
            {

            }
        });


        [Display(Name = "评分9", GroupName = "菜单")]
        public RelayCommand Score9Command { get; set; } = new RelayCommand(l =>
        {
            if (l is string project)
            {

            }
        });


        [Display(Name = "评分8", GroupName = "菜单")]
        public RelayCommand Score8Command { get; set; } = new RelayCommand(l =>
        {
            if (l is string project)
            {

            }
        });


        [Display(Name = "评分7", GroupName = "菜单")]
        public RelayCommand Score7Command { get; set; } = new RelayCommand(l =>
        {
            if (l is string project)
            {

            }
        });

        [Display(Name = "移除评分低于1的文件", GroupName = "更新")]
        public RelayCommand RemoveScore1Command { get; set; } = new RelayCommand(l =>
        {
            if (l is string project)
            {

            }
        });

        [Display(Name = "删除评分低于1的文件", GroupName = "更新")]
        public RelayCommand DeleteScore1Command { get; set; } = new RelayCommand(l =>
        {
            if (l is string project)
            {

            }
        });
    }
}
