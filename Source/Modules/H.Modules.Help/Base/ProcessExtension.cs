// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Diagnostics;

namespace H.Modules.Help.Base;

public static class ProcessExtension
{

    public static void ShowProcess(this string uri)
    {
        Process.Start(new ProcessStartInfo(uri) { UseShellExecute = true });

    }
}
