// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.App.AIDI.Base;
using H.App.AIDI.DB;
using H.App.AIDI.Model;
using H.Common.Attributes;
using H.Extensions.FontIcon;

namespace H.App.AIDI.Presenters;
[Icon(FontIcons.Split20)]
[Display(Name = "数据划分", GroupName = "工具", Description = "比例划分:随机按照设定比例划分当前已标注的图像到训练\r\n集和测试集\r\n数量划分:随机选择设定数量的图片为训练集")]
public class DataPartitioningTrainingAssistantItemPresenter : TrainingAssistantItemPresenterBase
{
    private DataPartitioningData _DataPartitioningData = new DataPartitioningData();
    public DataPartitioningData DataPartitioningData
    {
        get { return _DataPartitioningData; }
        set
        {
            _DataPartitioningData = value;
            RaisePropertyChanged();
        }
    }

    public override (bool success, string message) Invoke(IModulePresenter modulePresenter)
    {
        double ratio = this.DataPartitioningData.Ratio;

        if (ratio < 0 || ratio > 100)
            return (false, "比例必须在 0 到 100 之间");

        List<fm_dd_image> images = modulePresenter.Repository.Collection.Source
            .Where(x => x.Model.PageID == modulePresenter.ID)
            .Select(x => x.Model)
            .ToList();

        if (images.Count == 0)
            return (false, "没有可划分的已标注图像");

        int trainCount = (int)Math.Round(images.Count * ratio / 100.0, MidpointRounding.AwayFromZero);
        List<fm_dd_image> shuffled = images.OrderBy(_ => Random.Shared.Next()).ToList();

        foreach (fm_dd_image item in shuffled)
        {
            item.DatasetType = DatasetType.Test;
        }

        foreach (fm_dd_image item in shuffled.Take(trainCount))
        {
            item.DatasetType = DatasetType.Train;
        }

        modulePresenter.Repository.Save();
        modulePresenter.RefreshData();
        return (true, $"划分完成，训练集 {trainCount} 张，测试集 {images.Count - trainCount} 张");
    }
}




