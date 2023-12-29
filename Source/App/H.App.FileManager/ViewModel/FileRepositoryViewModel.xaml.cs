﻿using H.Controls.TagBox;
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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;
using System.Windows.Input;
using System.Windows.Media;
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
            var r = await IocMessage.Dialog.Show("确定更新？");
            if (r != true)
                return;
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
        }, x => this.Collection.FilterSource.Select(x => x.Model).OfType<fm_dd_video>().Count() > 0);

        [Display(Name = "根据名称加载标签", GroupName = "更新")]
        public RelayCommand UpdateTagCommand => new RelayCommand(l =>
        {
            var tagService = Ioc.GetService<ITagService>();
            foreach (var item in this.Collection.FilterSource.Select(x => x.Model))
            {
                if (item is fm_dd_file)
                {
                    var finds = tagService.Collection.Where(x => x.GroupName == null).Where(x => item.Name.Contains(x.Name));
                    foreach (var find in finds)
                    {
                        item.Tags = tagService.ConvertToCheck(item.Tags, find);
                    }
                }
                if (item is fm_dd_image image)
                {
                    {
                        var finds = tagService.Collection.Where(x => x.GroupName == "Object").Where(x => item.Name.Contains(x.Name));
                        foreach (var find in finds)
                        {
                            image.Object = tagService.ConvertToCheck(image.Object, find);
                        }
                    }

                    {
                        var finds = tagService.Collection.Where(x => x.GroupName == "Area").Where(x => item.Name.Contains(x.Name));
                        foreach (var find in finds)
                        {
                            image.Area = tagService.ConvertToCheck(image.Area, find);
                        }
                    }

                    {
                        var finds = tagService.Collection.Where(x => x.GroupName == "Articulation").Where(x => item.Name.Contains(x.Name));
                        foreach (var find in finds)
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
            var r = await IocMessage.Dialog.Show("确定移除？");
            if (r != true)
                return;
            var finds = this.Collection.FilterSource.Select(x => x.Model).Where(x => !File.Exists(x.Url)).ToList();
            foreach (var item in finds)
            {
                await this.Delete(item);
            }
            IocMessage.Snack.ShowInfo($"操作完成[{finds.Count()}]条");
        }, x => this.Collection.FilterSource.Count > 0);

        [Display(Name = "彻底删除选中文件")]
        public RelayCommand DeleteSelectedFileCommand => new RelayCommand(async l =>
        {
            var r = await IocMessage.Dialog.Show("确定删除？");
            if (r != true)
                return;
            if (File.Exists(this.Collection.SelectedItem.Model.Url))
                File.Delete(this.Collection.SelectedItem.Model.Url);
            await this.Delete(this.Collection.SelectedItem);
            IocMessage.Snack.ShowInfo($"操作完成");

        }, x => this.Collection.SelectedItem != null);

        [Display(Name = "彻底删除当前筛选的文件", GroupName = "更新")]
        public RelayCommand DeleteFilterFilesCommand => new RelayCommand(async l =>
        {
            var r = await IocMessage.Dialog.Show("确定删除？");
            if (r != true)
                return;
            var finds = this.Collection.FilterSource.ToList();
            foreach (var item in finds)
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
            var r = await IocMessage.Dialog.Show("确定移除？");
            if (r != true)
                return;
            var finds = this.Collection.FilterSource.ToList();
            foreach (var item in finds)
            {
                await this.Delete(item);
            }
            IocMessage.Snack.ShowInfo($"操作完成[{finds.Count()}]条");
        }, x => this.Collection.FilterSource.Count > 0);

        [Display(Name = "移除评分低于1的文件", GroupName = "更新")]
        public RelayCommand RemoveScore1Command => new RelayCommand(async l =>
        {
            var r = await IocMessage.Dialog.Show("确定移除？");
            if (r != true)
                return;
            var finds = this.Collection.FilterSource.Where(x => x.Model.Score < 1).ToList();
            foreach (var item in finds)
            {
                await this.Delete(item);
            }
            IocMessage.Snack.ShowInfo($"操作完成[{finds.Count()}]条");
        }, x => this.Collection.FilterSource.Where(x => x.Model.Score < 1).Count() > 0);

        [Display(Name = "彻底删除评分低于1的文件", GroupName = "更新")]
        public RelayCommand DeleteScore1Command => new RelayCommand(async l =>
        {
            var r = await IocMessage.Dialog.Show("确定删除？");
            if (r != true)
                return;
            var finds = this.Collection.FilterSource.Where(x => x.Model.Score < 1).ToList();
            foreach (var item in finds)
            {
                if (File.Exists(item.Model.Url))
                    File.Delete(item.Model.Url);
                await this.Delete(item);
            }
            IocMessage.Snack.ShowInfo($"操作完成[{finds.Count()}]条");
        }, x => this.Collection.FilterSource.Where(x => x.Model.Score < 1).Count() > 0);
    }
}