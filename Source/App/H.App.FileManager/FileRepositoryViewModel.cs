using H.Extensions.ValueConverter;
using H.Extensions.ViewModel;
using H.Providers.Ioc;
using H.Providers.Mvvm;
using System;
using System.IO;
using System.Linq;

namespace H.App.FileManager
{
    public class FileRepositoryViewModel : RepositoryViewModel<fm_dd_file>
    {
        public RelayCommand RefreshCommand => new RelayCommand(async (s, e) =>
        {
            if (IocProject.Instance.Current is FileProjectItem projectItem)
            {
                Random random=new Random();
                var r = await await IocMessage.Dialog.ShowString(async (c, x) =>
                   {
                       x.Value = "正在加载数据...";
                       var files = projectItem.BaseFolder.GetAllFiles();
                       this.Repository.Clear();
                       await this.Add(files.Select(x => new fm_dd_file()
                       {
                           Name = Path.GetFileNameWithoutExtension(x),
                           Url = Path.GetFullPath(x),
                           Extend = Path.GetExtension(x),
                           Size = new FileInfo(x).Length,
                           Score = random.Next(0, 10).ToString(),
                           Favorite = random.Next(10)==1
                       }).ToArray()); ;
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
