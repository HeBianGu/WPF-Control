// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Components.VisionDiagrams.OpenCV.Base;

[Icon(FontIcons.Photo)]
[Display(Name = "本地数据源", GroupName = "图像采集", Description = "从本地文件夹按顺序读取图像数据", Order = 5)]
public class OpenCVFolderSrcFilesNodeData : OpenCVSrcFilesNodeDataBase
{
    public string FolderPath { get; set; }
    protected override void LoadSrcFilePaths()
    {
        this.SrcFilePaths.Clear();
        if (!Directory.Exists(this.FolderPath))
            return;
        IEnumerable<string> imagePaths = this.FolderPath.GetAllImages();
        foreach (string imagePath in imagePaths)
        {
            string relatvePath = Path.GetRelativePath(AppDomain.CurrentDomain.BaseDirectory, imagePath);
            this.SrcFilePaths.Add(new SrcFilePathData() { SrcFilePath = relatvePath });
        }
        this.SrcFilePath = this.SrcFilePaths?.FirstOrDefault();
    }
}
