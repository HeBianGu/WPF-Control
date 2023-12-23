using H.Extensions.ValueConverter;
using H.Extensions.ViewModel;
using H.Providers.Ioc;
using H.Providers.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Windows;
using System.Windows.Threading;
using static System.Formats.Asn1.AsnWriter;
using System.Xml.Linq;

namespace H.App.FileManager
{
    public class FileRepositoryViewModel : RepositoryViewModel<fm_dd_file>
    {
        public override void RefreshData(params string[] includes)
        {

            includes = includes ?? this.GetIncludes()?.ToArray();
            IEnumerable<SelectViewModel<fm_dd_file>> collection = includes == null ? this.Repository.GetList().Select(x => new SelectViewModel<fm_dd_file>(x))
            : this.Repository.GetList(includes).Select(x => new SelectViewModel<fm_dd_file>(x));
            this.Collection.Load(collection);
        }

        protected override bool Where(fm_dd_file entity)
        {
            return entity.Project == Ioc.GetService<IProjectService>()?.Current?.Title;
        }

        public RelayCommand RefreshCommand => new RelayCommand(async (s, e) =>
        {
            if (IocProject.Instance.Current is FileProjectItem projectItem)
            {
                Random random = new Random();
                var r = await await IocMessage.Dialog.ShowString(async (c, x) =>
                   {
                       x.Value = "正在加载数据...";
                       var files = projectItem.BaseFolder.GetAllFiles();
                       this.Repository.Clear();

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
                               fm_Dd_File = new fm_dd_video();
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
                           fm_Dd_File.Score = random.Next(0, 10).ToString();
                           fm_Dd_File.Favorite = random.Next(10) == 1;
                           fm_Dd_File.Project = Ioc.GetService<IProjectService>()?.Current?.Title;
                           await this.Add(fm_Dd_File);
                       }
                       x.Value = "加载完成，正在保存...";
                       return await this.Save();
                   });
                if (r > 0)
                    IocMessage.Snack.ShowInfo("保存完成");
                else
                    IocMessage.Snack.ShowError("保存失败");
            }
        }, (s, e) => IocProject.Instance.Current is FileProjectItem);
    }
}
