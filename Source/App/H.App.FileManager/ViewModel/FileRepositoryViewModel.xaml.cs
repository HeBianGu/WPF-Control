using H.Controls.TagBox;
using H.Extensions.Common;
using H.Extensions.ViewModel;
using H.Providers.Ioc;
using H.Providers.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;
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
            this.MoreCommands = this.Commands.OfType<IRelayCommand>().Where(x => x.GroupName != "菜单" && x.GroupName != "更新").ToObservable();
        }

        public static FileRepositoryViewModel Instance => DbIoc.GetService<IRepositoryViewModel<fm_dd_file>>() as FileRepositoryViewModel;

        public override void RefreshData(params string[] includes)
        {
            includes = includes ?? GetIncludes()?.ToArray();
            IEnumerable<SelectViewModel<fm_dd_file>> collection = includes == null ? Repository.GetList().Select(x => new SelectViewModel<fm_dd_file>(x))
            : Repository.GetList(includes).Select(x => new SelectViewModel<fm_dd_file>(x));
            Collection.Load(collection);
        }

        private ObservableCollection<fm_dd_file> _hisotry = new ObservableCollection<fm_dd_file>();
        public ObservableCollection<fm_dd_file> History
        {
            get { return _hisotry; }
            set
            {
                _hisotry = value;
                RaisePropertyChanged("History");
            }
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
                int r = await await IocMessage.Dialog.ShowString(async (c, x) =>
                   {
                       x.Value = "正在加载数据...";
                       List<string> files = projectItem.BaseFolder.GetAllFiles();
                       Repository.Clear();

                       List<fm_dd_file> dbFiles = new List<fm_dd_file>();
                       foreach (string file in files)
                       {
                           fm_dd_file fileEnity = Ioc.GetService<IFileToEntityService>().ToEntity(file);
                           dbFiles.Add(fileEnity);
                       }
                       await Add(dbFiles.ToArray());
                       x.Value = "加载完成，正在保存...";
                       return await Save();
                   });
                if (r >= 0)
                    IocMessage.Snack.ShowInfo("保存完成");
                else
                    IocMessage.Snack.ShowError("保存失败");
            }
        }, (s, e) => IocProject.Instance.Current is FileProjectItem);

        [Display(Name = "打开文件夹", GroupName = "菜单")]
        public RelayCommand OpenDirectoryCommand => new RelayCommand(l =>
        {
            string folder = Path.GetDirectoryName(this.Collection.SelectedItem.Model.Url);
            Process.Start(new ProcessStartInfo(folder) { UseShellExecute = true });
        }, x => this.Collection.SelectedItem != null);

        [Display(Name = "打开文件", GroupName = "菜单")]
        public RelayCommand OpenCommand => new RelayCommand(l =>
        {
            if (File.Exists(this.Collection.SelectedItem.Model.Url))
                Process.Start(new ProcessStartInfo(this.Collection.SelectedItem.Model.Url) { UseShellExecute = true });
        }, x => this.Collection.SelectedItem != null);


        [Display(Name = "复制", GroupName = "菜单")]
        public RelayCommand CopyCommand { get; set; } = new RelayCommand(l =>
        {
            if (l is string project)
            {

            }
        });

        [Display(Name = "另存为", GroupName = "菜单")]
        public RelayCommand SaveAsCommand { get; set; } = new RelayCommand(l =>
        {
            if (l is string project)
            {

            }
        });

        [Display(Name = "更新视频信息", GroupName = "更新")]
        public RelayCommand UpdateVieoInfoCommand { get; set; } = new RelayCommand(l =>
        {
            if (l is string project)
            {

            }
        });

        [Display(Name = "更新视频缩率图", GroupName = "更新")]
        public RelayCommand UpdateVedioImageCommand => new RelayCommand(async l =>
        {
            bool? r = await IocMessage.Dialog.Show("确定更新？");
            if (r != true)
                return;
            foreach (fm_dd_video item in this.Collection.FilterSource.Select(x => x.Model).OfType<fm_dd_video>())
            {
                string dir = Path.GetDirectoryName(item.Url);
                IEnumerable<string> images = Directory.GetFiles(dir).Where(x => x.IsImage() && Path.GetFileNameWithoutExtension(x).StartsWith(item.Name));
                item.Images.Clear();
                foreach (string image in images)
                {
                    item.Images.Add(new fm_dd_video_image() { Url = image, Name = Path.GetFileNameWithoutExtension(item.Name) });
                }
                item.SelectedImageIndex = 0;
            }
        }, x => this.Collection.FilterSource.Select(x => x.Model).OfType<fm_dd_video>().Count() > 0);

        [Display(Name = "根据名称加载标签", GroupName = "更新")]
        public RelayCommand UpdateTagCommand => new RelayCommand(l =>
        {
            ITagService tagService = Ioc.GetService<ITagService>();
            foreach (fm_dd_file item in this.Collection.FilterSource.Select(x => x.Model))
            {
                if (item is fm_dd_file)
                {
                    IEnumerable<ITag> finds = tagService.Collection.Where(x => x.GroupName == null).Where(x => item.Name.Contains(x.Name));
                    foreach (ITag find in finds)
                    {
                        item.Tags = tagService.ConvertToCheck(item.Tags, find);
                    }
                }
                if (item is fm_dd_image image)
                {
                    {
                        IEnumerable<ITag> finds = tagService.Collection.Where(x => x.GroupName == "Object").Where(x => item.Name.Contains(x.Name));
                        foreach (ITag find in finds)
                        {
                            image.Object = tagService.ConvertToCheck(image.Object, find);
                        }
                    }

                    {
                        IEnumerable<ITag> finds = tagService.Collection.Where(x => x.GroupName == "Area").Where(x => item.Name.Contains(x.Name));
                        foreach (ITag find in finds)
                        {
                            image.Area = tagService.ConvertToCheck(image.Area, find);
                        }
                    }

                    {
                        IEnumerable<ITag> finds = tagService.Collection.Where(x => x.GroupName == "Articulation").Where(x => item.Name.Contains(x.Name));
                        foreach (ITag find in finds)
                        {
                            image.Articulation = tagService.ConvertToCheck(image.Articulation, find);
                        }
                    }
                }

            }
        });

        [Display(Name = "移除不存在的文件", GroupName = "更新")]
        public RelayCommand RemoveAbsentFileCommand => new RelayCommand(async l =>
        {
            bool? r = await IocMessage.Dialog.Show("确定移除？");
            if (r != true)
                return;
            List<fm_dd_file> finds = this.Collection.FilterSource.Select(x => x.Model).Where(x => !File.Exists(x.Url)).ToList();
            foreach (fm_dd_file item in finds)
            {
                await this.Delete(item);
            }
            IocMessage.Snack.ShowInfo($"操作完成[{finds.Count()}]条");
        }, x => this.Collection.FilterSource.Count > 0);

        [Display(Name = "彻底删除", GroupName = "菜单")]
        public RelayCommand DeleteSelectedFileCommand => new RelayCommand(async l =>
        {
            bool? r = await IocMessage.Dialog.Show("确定删除？");
            if (r != true)
                return;
            if (File.Exists(this.Collection.SelectedItem.Model.Url))
                File.Delete(this.Collection.SelectedItem.Model.Url);
            await this.Delete(this.Collection.SelectedItem);
            IocMessage.Snack.ShowInfo($"操作完成");

        }, x => this.Collection.SelectedItem != null);


        [Display(Name = "移除", GroupName = "菜单")]
        public RelayCommand RemoveSelectedFileCommand => new RelayCommand(async l =>
        {
            bool? r = await IocMessage.Dialog.Show("确定删除？");
            if (r != true)
                return;
            await this.Delete(this.Collection.SelectedItem);
            IocMessage.Snack.ShowInfo($"操作完成");

        }, x => this.Collection.SelectedItem != null);

        [Display(Name = "彻底删除当前筛选的文件", GroupName = "更新")]
        public RelayCommand DeleteFilterFilesCommand => new RelayCommand(async l =>
        {
            bool? r = await IocMessage.Dialog.Show("确定删除？");
            if (r != true)
                return;
            List<SelectViewModel<fm_dd_file>> finds = this.Collection.FilterSource.ToList();
            foreach (SelectViewModel<fm_dd_file> item in finds)
            {
                if (File.Exists(item.Model.Url))
                    File.Delete(item.Model.Url);
                await this.Delete(item);
            }
            IocMessage.Snack.ShowInfo($"操作完成[{finds.Count()}]条");
        }, x => this.Collection.FilterSource.Count > 0);

        [Display(Name = "移除当前筛选的文件", GroupName = "更新")]
        public RelayCommand RemoveFilterFilesCommand => new RelayCommand(async l =>
        {
            bool? r = await IocMessage.Dialog.Show("确定移除？");
            if (r != true)
                return;
            List<SelectViewModel<fm_dd_file>> finds = this.Collection.FilterSource.ToList();
            foreach (SelectViewModel<fm_dd_file> item in finds)
            {
                await this.Delete(item);
            }
            IocMessage.Snack.ShowInfo($"操作完成[{finds.Count()}]条");
        }, x => this.Collection.FilterSource.Count > 0);

        [Display(Name = "移除评分低于1的文件", GroupName = "更新")]
        public RelayCommand RemoveScore1Command => new RelayCommand(async l =>
        {
            bool? r = await IocMessage.Dialog.Show("确定移除？");
            if (r != true)
                return;
            List<SelectViewModel<fm_dd_file>> finds = this.Collection.FilterSource.Where(x => x.Model.Score < 1).ToList();
            foreach (SelectViewModel<fm_dd_file> item in finds)
            {
                await this.Delete(item);
            }
            IocMessage.Snack.ShowInfo($"操作完成[{finds.Count()}]条");
        }, x => this.Collection.FilterSource.Where(x => x.Model.Score < 1).Count() > 0);

        [Display(Name = "彻底删除评分低于1的文件", GroupName = "更新")]
        public RelayCommand DeleteScore1Command => new RelayCommand(async l =>
        {
            bool? r = await IocMessage.Dialog.Show("确定删除？");
            if (r != true)
                return;
            List<SelectViewModel<fm_dd_file>> finds = this.Collection.FilterSource.Where(x => x.Model.Score < 1).ToList();
            foreach (SelectViewModel<fm_dd_file> item in finds)
            {
                if (File.Exists(item.Model.Url))
                    File.Delete(item.Model.Url);
                await this.Delete(item);
            }
            IocMessage.Snack.ShowInfo($"操作完成[{finds.Count()}]条");
        }, x => this.Collection.FilterSource.Where(x => x.Model.Score < 1).Count() > 0);

        [Browsable(false)]
        public RelayCommand SelectionChangedCommand => new RelayCommand((s, e) =>
        {
            if (e is fm_dd_file file)
            {
                file.Watched = true;
                file.LastPlayTime = DateTime.Now;
                file.PlayCount = file.PlayCount + 1;
                this.History.Add(file);
            }
        });

        [Browsable(false)]
        public RelayCommand MouseDoubleClickCommand => new RelayCommand(async (s, e) =>
        {
            if (e is fm_dd_file file)
            {
                this.History.Add(file);
                IFileView view = Ioc.GetService<IFileToViewService>().ToView(file);
                await IocMessage.Dialog.Show(view);
            }
        });
    }
}
