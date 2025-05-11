// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using FFMpegCore.Enums;

namespace H.Extensions.FFMpeg;
public interface IFFMpegOption
{
    string BinaryFolder { get; set; }
    FFMpegLogLevel FFMpegLogLevel { get; set; }
    string TemporaryFilesFolder { get; set; }
    bool UseCache { get; set; }
    bool UsingMultithreading { get; set; }
    string WorkingDirectory { get; set; }

    bool Load(out string message);
    void LoadDefault();
    bool Save(out string message);
}