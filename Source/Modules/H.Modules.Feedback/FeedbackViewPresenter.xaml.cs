// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Extensions.Common;
global using H.Mvvm.Commands;
global using H.Mvvm.ViewModels.Base;
global using H.Services.Common.Feedback;
global using H.Services.Message;
global using H.Services.Message.IODialog;
global using H.Services.Setting;
global using System.Collections.ObjectModel;
global using System.ComponentModel;
global using System.ComponentModel.DataAnnotations;
namespace H.Modules.Feedback;

[Display(Name = "意见反馈", GroupName = SettingGroupNames.GroupSystem, Description = "点击显示意见反馈")]
public class FeedbackViewPresenter : BindableBase, IFeedbackViewPresenter
{
    private string _title;
    [Display(Name = "您的称呼")]
    public string Title
    {
        get { return _title; }
        set
        {
            _title = value;
            RaisePropertyChanged();
        }
    }

    private string _contact;
    [Browsable(false)]
    [Display(Name = "联系方式")]
    public string Contact
    {
        get { return _contact; }
        set
        {
            _contact = value;
            RaisePropertyChanged();
        }
    }

    private string _text;
    [Browsable(false)]
    [Display(Name = "问题描述")]
    public string Text
    {
        get { return _text; }
        set
        {
            _text = value;
            RaisePropertyChanged();
        }
    }

    private ObservableCollection<string> _files = new ObservableCollection<string>();
    public ObservableCollection<string> Files
    {
        get { return _files; }
        set
        {
            _files = value;
            RaisePropertyChanged();
        }
    }

    public RelayCommand AddFilesCommand => new RelayCommand(async l =>
    {
        var r = IocMessage.IOFileDialog.ShowOpenImageFiles();
        if (r == null)
            return;
        if (this.Files.Count + r.Length > 5)
        {
            await IocMessage.Dialog.Show("已超过附件上限5个");
            return;
        }
        this.Files.AddRange(r);
    });
}
