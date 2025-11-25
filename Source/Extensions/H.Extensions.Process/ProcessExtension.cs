// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Diagnostics;

namespace H.Extensions.Process
{
    public static class ProcessExtension
    {

        public static void StartProcess(this string uri)
        {
            System.Diagnostics.Process.Start(new ProcessStartInfo(uri) { UseShellExecute = true });
        }
    }

}
