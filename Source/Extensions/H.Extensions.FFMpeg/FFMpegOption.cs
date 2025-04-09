using FFMpegCore;
using FFMpegCore.Enums;
using H.Extensions.Setting;
using H.Services.Setting;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace H.Extensions.FFMpeg;

[Display(Name = "FFmpeg设置", GroupName = SettingGroupNames.GroupControl, Description = "使用调查文件帮助优化本软件")]
public class FFMpegOption : IocOptionInstance<FFMpegOption>, IFFMpegOption
{
    private string _workingDirectory = string.Empty;
    [Display(Name = "工作路径")]
    public string WorkingDirectory
    {
        get { return _workingDirectory; }
        set
        {
            _workingDirectory = value;
            RaisePropertyChanged();
        }
    }

    private bool _usingMultithreading = true;
    [DefaultValue(true)]
    [Display(Name = "启用多线程")]
    public bool UsingMultithreading
    {
        get { return _usingMultithreading; }
        set
        {
            _usingMultithreading = value;
            RaisePropertyChanged();
        }
    }


    private string _binaryFolder = string.Empty;
    [Display(Name = "exe路径")]
    public string BinaryFolder
    {
        get { return _binaryFolder; }
        set
        {
            _binaryFolder = value;
            RaisePropertyChanged();
        }
    }


    private string _temporaryFilesFolder = Path.GetTempPath();
    [DefaultValue(FFMpegLogLevel.Info)]
    [Display(Name = "缓存路径")]
    public string TemporaryFilesFolder
    {
        get { return _temporaryFilesFolder; }
        set
        {
            _temporaryFilesFolder = value;
            RaisePropertyChanged();
        }
    }

    private FFMpegLogLevel _FFMpegLogLevel = FFMpegLogLevel.Info;
    [DefaultValue(FFMpegLogLevel.Info)]
    [Display(Name = "日志级别")]
    public FFMpegLogLevel FFMpegLogLevel
    {
        get { return _FFMpegLogLevel; }
        set
        {
            _FFMpegLogLevel = value;
            RaisePropertyChanged();
        }
    }

    private bool _useCache = true;
    [DefaultValue(true)]
    [Display(Name = "启用缓存")]
    public bool UseCache
    {
        get { return _useCache; }
        set
        {
            _useCache = value;
            RaisePropertyChanged();
        }
    }

    public override void LoadDefault()
    {
        base.LoadDefault();
        this.TemporaryFilesFolder = Path.GetTempPath();
        this.BinaryFolder = string.Empty;
        this.WorkingDirectory = string.Empty;
        this.Applay();
    }

    public override bool Load(out string message)
    {
        var r = base.Load(out message);
        this.Applay();
        return r;
    }

    public override bool Save(out string message)
    {
        this.Applay();
        return base.Save(out message);
    }

    private void Applay()
    {
        GlobalFFOptions.Configure(options =>
        {
            options.LogLevel = this.FFMpegLogLevel;
            options.WorkingDirectory = this.WorkingDirectory ?? string.Empty;
            options.BinaryFolder = this.BinaryFolder ?? string.Empty;
            options.UseCache = this.UseCache;
            options.TemporaryFilesFolder = this.TemporaryFilesFolder;
        });
    }
}
