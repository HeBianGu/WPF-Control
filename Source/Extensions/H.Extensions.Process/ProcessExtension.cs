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

        public static bool StartArgsProcess(this string workingDirectory, string exe, string args, Action<string> titleLog, Action<System.Diagnostics.Process, string> infoLog)
        {
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = exe,
                    Arguments = args,
                    WorkingDirectory = workingDirectory,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                    StandardOutputEncoding = System.Text.Encoding.UTF8,
                };

                titleLog?.Invoke($"正在启动进程...");
                using var process = new System.Diagnostics.Process { StartInfo = psi, EnableRaisingEvents = true };
                process.OutputDataReceived += (s, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data))
                        infoLog?.Invoke(process, e.Data);
                };
                process.ErrorDataReceived += (s, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data))
                        infoLog?.Invoke(process, "[ERR] " + e.Data);
                };

                if (!process.Start())
                {
                    titleLog?.Invoke("进程启动失败");
                    return false;
                }

                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                process.WaitForExit();
                if (process.ExitCode == 0)
                {
                    titleLog?.Invoke("进程运行完成");
                    return true;
                }
                else
                {
                    titleLog?.Invoke($"进程运行失败，错误代码={process.ExitCode}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                titleLog?.Invoke("进程运行异常" + ex.Message);
                return false;
            }
        }
    }
}
