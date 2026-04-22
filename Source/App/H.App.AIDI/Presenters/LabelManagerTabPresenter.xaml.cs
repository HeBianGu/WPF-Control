// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.App.AIDI.Base;
using H.App.AIDI.Model;
using H.App.AIDI.Presenters.Components;
using H.App.AIDI.ViewModel;
using H.Common.Attributes;
using H.Extensions.FontIcon;
using H.Extensions.Mvvm.Commands;
using H.Mvvm.Commands;
using H.Services.Project;
using System.Text.Json.Serialization;

namespace H.App.AIDI.Presenters;
[Display(Name = "①标注管理", Description = "管理标签的创建、编辑和删除", GroupName = "流程")]
public class LabelManagerTabPresenter : TabPresenterBase
{
    public LabelManagerTabPresenter(IModulePresenter modulePresenter) : base(modulePresenter)
    {
        for (int i = 0; i < 10; i++)
        {
            LabelData labelData = new LabelData();
            labelData.Type = "NG";
            labelData.Image = "默认图片.png";
            labelData.TypeProperty = "标注";
            labelData.List = "训练集";
            labelData.Location = "(129,140)";
            labelData.Length = 113;
            labelData.Width = 21;
            this.LabelDatas.Add(labelData);
        }

        this.SelectedLabelData = this.LabelDatas.FirstOrDefault();
    }
    private TagManagerComponentPresenter _TagManagerComponentPresenter = new TagManagerComponentPresenter();
    [JsonIgnore]
    public TagManagerComponentPresenter TagManagerComponentPresenter
    {
        get { return _TagManagerComponentPresenter; }
        set
        {
            _TagManagerComponentPresenter = value;
            RaisePropertyChanged();
        }
    }

    private string _LabelName;
    public string LabelName
    {
        get { return _LabelName; }
        set
        {
            _LabelName = value;
            RaisePropertyChanged();
        }
    }

    private ObservableCollection<LabelData> _LabelDatas = new ObservableCollection<LabelData>();
    public ObservableCollection<LabelData> LabelDatas
    {
        get { return _LabelDatas; }
        set
        {
            _LabelDatas = value;
            RaisePropertyChanged();
        }
    }


    private LabelData _SelectedLabelData;
    public LabelData SelectedLabelData
    {
        get { return _SelectedLabelData; }
        set
        {
            _SelectedLabelData = value;
            RaisePropertyChanged();
        }
    }


    public RelayCommand AddLabelCommand => new RelayCommand(x =>
    {
        IocProject.Instance.Current.AddLabelItem(this.LabelName);
    }, x => !string.IsNullOrEmpty(LabelName));


    private LabelItem _SelectedLabelItem;
    public LabelItem SelectedLabelItem
    {
        get { return _SelectedLabelItem; }
        set
        {
            _SelectedLabelItem = value;
            RaisePropertyChanged();
        }
    }

    [Icon(FontIcons.Delete)]
    [Display(Name = "删除")]
    public DisplayCommand DeleteLabelCommand => new DisplayCommand(x =>
    {
        if (IocProject.Instance.Current is AIDIProjectItem projectItem)
        {
            projectItem.Labels.Remove(this.SelectedLabelItem);
            projectItem.Save(out string message);
        }
    }, x => this.SelectedLabelItem != null);
    [Icon(FontIcons.Clear)]
    [Display(Name = "清空")]
    public DisplayCommand ClearLabelsCommand => new DisplayCommand(x =>
    {
        if (IocProject.Instance.Current is AIDIProjectItem projectItem)
        {
            projectItem.Labels.Clear();
            projectItem.Save(out string message);
        }
    });
}
