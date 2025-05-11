// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Diagnostics;
using System.IO;

namespace H.Extensions.Process
{
    public class ProcessService
    {
        public void ShowSelectFile(string file)
        {
            try
            {
                if (string.IsNullOrEmpty(file) || !File.Exists(file))
                    return;
                string args = string.Format("/e, /select, \"{0}\"", file);
                ProcessStartInfo info = new ProcessStartInfo();
                info.FileName = "explorer.exe";
                info.Arguments = args;
                System.Diagnostics.Process.Start(info);
            }
            catch (System.Exception ex)
            {
                // 记录异常或显示错误信息
                Debug.WriteLine($"Failed to open file in explorer: {ex.Message}");
            }
        }
    }
}
