// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Services.Message.IODialog;
using H.ValueConverter.Files;
using System.IO;

namespace H.Presenters.Common;


[Icon("\xE70F")]
[Display(Name = "文件信息")]
public class FileInfoPresenter : DisplayBindableBase
{
    public FileInfoPresenter(FileInfo fileInfo)
    {
        this.FileInfo = fileInfo;
        this.Update(fileInfo);
    }
    private FileInfo _FileInfo;

    public FileInfo FileInfo
    {
        get { return _FileInfo; }
        set
        {
            _FileInfo = value;
            RaisePropertyChanged();
        }
    }
    protected void Update(FileInfo fileInfo)
    {
        this.FileName = fileInfo.Name;
        this.Url = fileInfo.FullName;
        this.Extend = fileInfo.Extension;
        this.Size = fileInfo.Length;
        this.LastPlayTime = fileInfo.LastAccessTime;
    }

    [ReadOnly(true)]
    [Required]
    [Display(Name = "文件名称")]
    public string FileName { get; set; }

    [ReadOnly(true)]
    [Required]
    [Display(Name = "文件路径")]
    public string Url { get; set; }

    [ReadOnly(true)]
    [Display(Name = "扩展名")]
    public string Extend { get; set; }

    private long _size;
    [ReadOnly(true)]
    [Display(Name = "文件大小")]
    public long Size
    {
        get { return _size; }
        set
        {
            _size = value;
            RaisePropertyChanged();
        }
    }

    private DateTime _lastPlayTime;
    [Display(Name = "最近打开")]
    public DateTime LastPlayTime
    {
        get { return _lastPlayTime; }
        set
        {
            _lastPlayTime = value;
            RaisePropertyChanged();
        }
    }

}
