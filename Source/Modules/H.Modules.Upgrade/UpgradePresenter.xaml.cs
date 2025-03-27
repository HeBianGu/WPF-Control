// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control




using H.Common.Attributes;
using H.Mvvm.Commands;
using H.Mvvm.ViewModels.Base;
using H.Services.Message;
using H.Services.Message.Dialog;
using H.Services.Setting;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace H.Modules.Upgrade;

[Icon("\xECC5")]
[Display(Name = "软件更新", GroupName = SettingGroupNames.GroupSystem, Description = "应用此功能检查软件更新")]
internal class UpgradePresenter : BindableBase
{
    private readonly VersionData _args;
    public UpgradePresenter(VersionData args)
    {
        _args = args;
        this.Title = $"检测到新版本：V{args.Version}";
        this.Messages = args.Messages;
    }

    public string Title { get; set; }
    public List<string> Messages { get; set; }

    private string _message;
    public string Message
    {
        get { return _message; }
        set
        {
            _message = value;
            RaisePropertyChanged();
        }
    }

    private double _value;
    public double Value
    {
        get { return _value; }
        set
        {
            _value = value;
            RaisePropertyChanged();
        }
    }


    private bool _isDownLoading;
    /// <summary> 说明  </summary>
    public bool IsDownLoading
    {
        get { return _isDownLoading; }
        set
        {
            _isDownLoading = value;
            RaisePropertyChanged();
        }
    }


    public RelayCommand DownLoadCommand => new RelayCommand(async x =>
    {
        if (UpgradeOptions.Instance.UseIEDownload)
        {
            Process.Start(new ProcessStartInfo(_args.Uri) { UseShellExecute = true });
            return;
        }
        this.IsDownLoading = true;
        try
        {
            string save = UpgradeOptions.Instance.SavePath;
            if (!Directory.Exists(save))
                Directory.CreateDirectory(save);
            string fileName = System.IO.Path.GetFileName(_args.Uri);
            string savePath = System.IO.Path.Combine(save, fileName);
            if (File.Exists(savePath))
            {
                bool? cr = await IocMessage.Dialog.Show("文件已存在，是否删除重新下载", x =>
                {
                    x.DialogButton = DialogButton.SumitAndCancel;
                    x.Title = "提示";
                });
                if (cr != true)
                {
                    this.Message = "文件已存在，直接安装取消下载";
                    cr = this.ShowSetup(savePath, out string message);
                    if (cr == false)
                    {
                        await IocMessage.ShowDialogMessage(message);
                        return;
                    }
                    return;
                }
                File.Delete(savePath);
            }
            string messageDownload = null;
            string url = await Task.Run(() =>
            {
                return this.ShowDownlowd(_args.Uri, savePath, out messageDownload);
            });
            if (string.IsNullOrEmpty(url))
            {
                await IocMessage.ShowDialogMessage(messageDownload);
                return;
            }

            bool r = this.ShowSetup(url, out string message1);
            return;
        }
        catch (Exception ex)
        {
            IocMessage.ShowDialogMessage(ex.Message);
        }
        finally
        {
            this.IsDownLoading = false;
        }
    });

    private string ShowDownlowd(string url, string savefilePath, out string message)
    {
        Action<string, string> action = (current, total) =>
        {
            this.Message = string.Format(UpgradeOptions.Instance.LoadFormat, FormatBytes(long.Parse(current)).PadLeft(10, ' '), FormatBytes(long.Parse(total)));
            this.Value = double.Parse(current) / double.Parse(total) * 100;

            if (current == total)
            {
                this.Value = 100.0;
                this.Message = $"下载完成！";
            }

        };
        DownloadService.DownloadFile(url, savefilePath, action, 50);

        if (!File.Exists(savefilePath))
        {
            message = "文件不存在，下载失败";
            return null;
        }
        message = "下载成功";
        return savefilePath;
    }


    public bool ShowSetup(string savePath, out string message)
    {
        message = null;
        bool? r = IocMessage.Dialog.Show("是否立即安装", x =>
        {
            x.DialogButton = DialogButton.SumitAndCancel;
            x.Title = "提示";
        }).Result;
        if (r == false)
        {
            message = "用户取消安装";
            return false;
        }

        string extend = Path.GetExtension(savePath).ToLower();
        if (extend.Equals(".msi") || extend.Equals(".exe"))
        {
            Process.Start(new ProcessStartInfo(savePath) { UseShellExecute = true });
            return true;
        }

        if (Path.GetExtension(savePath).ToLower().EndsWith("zip") || Path.GetExtension(savePath).ToLower().EndsWith("exe"))
        {
            Process.Start(new ProcessStartInfo(savePath) { UseShellExecute = true });
            return true;
        }
        message = "安装包格式不合法，请使用：.msi、.exe或.zip";
        return false;
    }

    private string FormatBytes(long bytes)
    {
        string[] Suffix = { "Byte", "KB", "MB", "GB", "TB" };
        int i = 0;
        double dblSByte = bytes;

        if (bytes > 1024)
            for (i = 0; (bytes / 1024) > 0; i++, bytes /= 1024)
                dblSByte = bytes / 1024.0;

        return String.Format("{0:0.##}{1}", dblSByte, Suffix[i]);
    }
}

internal class DownloadService
{
    /// <summary> 下载文件 </summary>        
    /// <param name="URL">下载文件地址</param>  
    /// <param name="Filename">下载后的存放地址</param>        
    /// <param name="Prog">用于显示的进度条</param>        
    /// 
    public static void DownloadFile(string URL, string filename, Action<string, string> percentAction = null, int refreshTime = 1000)
    {
        float percent = 0;
        int total = 0;
        int current = 0;
        HttpWebRequest Myrq = HttpWebRequest.Create(URL) as HttpWebRequest;
        Myrq.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.0.3705;)";
        //Myrq.Headers.Add("Token", Token);
        HttpWebResponse myrp = (HttpWebResponse)Myrq.GetResponse();


        long totalBytes = myrp.ContentLength;
        total = (int)totalBytes;
        Stream st = myrp.GetResponseStream();
        Stream so = new FileStream(filename, FileMode.Create);

        long totalDownloadedByte = 0;
        byte[] by = new byte[1024];

        int osize = st.Read(by, 0, by.Length);
        // Todo ：定时刷新进度 
        if (percentAction != null)
        {
            Action action = () =>
            {
                while (st.CanRead)
                {
                    Thread.Sleep(refreshTime);
                    // Todo ：返回进度 
                    percentAction(current.ToString(), total.ToString());
                    System.Diagnostics.Debug.WriteLine(current / total);
                    if (current == total)
                        break;
                }
            };

            Task task = new Task(action);
            task.Start();
        }

        while (osize > 0)
        {
            totalDownloadedByte = osize + totalDownloadedByte;
            so.Write(by, 0, osize);
            current = (int)totalDownloadedByte;
            osize = st.Read(by, 0, by.Length);
            percent = totalDownloadedByte / (float)totalBytes * 100;
        }
        so.Close();
        st.Close();
    }
}
