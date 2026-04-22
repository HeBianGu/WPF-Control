// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.App.AIDI.Base;
using H.App.AIDI.DB;
using H.Extensions.Common;
using H.Mvvm.Commands;
using H.Services.Message;
using H.Services.Message.IODialog;
using H.Services.Project;
using System.Text.Json.Serialization;

namespace H.App.AIDI.Presenters.Components;
public class ImageIOComponentPresenter : RepositoryPresenterBase
{
    private readonly IPagePresenter _pagePresenter;
    public ImageIOComponentPresenter(IPagePresenter pagePresenter)
    {
        this._pagePresenter = pagePresenter;
    }

    public IPagePresenter PagePresenter => this._pagePresenter;
    [JsonIgnore]
    public RelayCommand ImportImagesCommand => new RelayCommand(async x =>
    {
        var urls = IocMessage.IOFileDialog.ShowOpenImageFiles();
        if (urls == null)
            return;
        await this.LoadImagesAsync(urls);
        this.Repository.RefreshData();
    });

    async Task LoadImagesAsync(IEnumerable<string> urls)
    {
        int r = await await IocMessage.Dialog.ShowString(async (c, x) =>
        {
            x.Value = "正在加载数据...";
            List<string> files = urls.ToList();
            //Repository.Clear();
            List<fm_dd_image> dbFiles = new List<fm_dd_image>();
            int index = 0;
            foreach (string file in files)
            {
                index++;
                if (c.IsCancel)
                    return -1;
                if (!file.IsImage())
                    continue;
                fm_dd_image image = file.ToImageEntity();
                image.ProjectID = IocProject.Instance.Current.ID;
                image.PageID = this._pagePresenter.ID;
                if (this.Repository.Collection.FirstOrDefault(k => k.Model.Url.GetFullPath() == file) == null)
                    dbFiles.Add(image);
                x.Value = $"[{index}/{files.Count}] {file}";
            }
            await this.Repository.Add(dbFiles.ToArray());
            x.Value = "加载完成，正在保存...";
            return await this.Repository.Save();
        }, x => x.Title = "正在加载图像数据...");

        if (r >= 0)
            IocMessage.ShowSnackInfo("保存完成");
        else
            IocMessage.Snack.ShowError("保存失败");
    }

    [JsonIgnore]
    public RelayCommand ImportFolderCommand => new RelayCommand(async x =>
    {
        var r = IocMessage.IOFolderDialog.ShowOpenFolder();
        if (r == null)
            return;
        var urls = r.GetAllImages();
        await this.LoadImagesAsync(urls);
        this.Repository.RefreshData();
    });
}
