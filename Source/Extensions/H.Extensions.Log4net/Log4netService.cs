using H.Extensions.Setting;
using H.Services.Setting;
using log4net;
using log4net.Appender;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace H.Extensions.Log4net;

[Display(Name = "日志记录", GroupName = SettingGroupNames.GroupSystem, Description = "点击显示新手向导")]
public class Log4netService : ILogService
{
    private readonly IOptions<Log4netOptions> _options;
    public Log4netService(IOptions<Log4netOptions> options)
    {
        this._options = options;
        string process = Process.GetCurrentProcess().ProcessName;
        this.InitLogger(process);
    }

    private void InitLogger(string name)
    {
        string logconfig = @"log4net.config";
        ReplaceFileTag(logconfig);
        Stopwatch st = new Stopwatch();
        //  开始计时
        st.Start();
        log4net.GlobalContext.Properties["dynamicName"] = name;
        Logger = LogManager.GetLogger(name);
        //  终止计时
        st.Stop();
        if (st.ElapsedMilliseconds > 2000)
        {
            Logger.Info("log4net.config file ERROR!!!");
            System.IO.FileStream fs = new System.IO.FileStream(logconfig, System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite);
            StreamReader sr = new StreamReader(fs, System.Text.Encoding.UTF8);
            string str = sr.ReadToEnd();
            str = str.Replace(@"ref=""SQLAppender""", @"ref=""SQLAppenderError""");
            sr.Close();
            fs.Close();
            System.IO.FileStream fs1 = new System.IO.FileStream(logconfig, System.IO.FileMode.Open, System.IO.FileAccess.Write);
            StreamWriter swWriter = new StreamWriter(fs1, System.Text.Encoding.UTF8);
            swWriter.Flush();
            swWriter.Write(str);
            swWriter.Close();
            fs1.Close();
        }
        if (!Directory.Exists(this._options.Value.LogPath))
            Directory.CreateDirectory(this._options.Value.LogPath);
        InitLogPath(this._options.Value.LogPath);
    }

    private ILog Logger = null;
    private void InitLogPath(string repository)
    {
        RollingFileAppender appender = new RollingFileAppender();
        appender.File = repository + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + "\\";
        appender.AppendToFile = true;
        appender.MaxSizeRollBackups = -1;
        //appender.MaximumFileSize = "1MB";  
        appender.RollingStyle = log4net.Appender.RollingFileAppender.RollingMode.Date;
        appender.DatePattern = "yyyy-MM-dd_HH\".log\"";
        appender.StaticLogFileName = false;
        appender.LockingModel = new log4net.Appender.FileAppender.MinimalLock();
        appender.Layout = new log4net.Layout.PatternLayout("%date [%thread] %-5level - %message%newline");
        appender.ActivateOptions();
        log4net.Config.BasicConfigurator.Configure(appender);
    }

    private void ReplaceFileTag(string logconfig)
    {
        try
        {
            FileStream fs = new FileStream(logconfig, FileMode.Open, FileAccess.ReadWrite);
            StreamReader sr = new StreamReader(fs, Encoding.UTF8);
            string str = sr.ReadToEnd();
            sr.Close();
            fs.Close();

            if (str.IndexOf("#LOG_PATH#") > -1)
            {
                str = str.Replace(@"#LOG_PATH#", this._options.Value.tempPath);
                FileStream fs1 = new FileStream(logconfig, FileMode.Open, FileAccess.Write);
                StreamWriter swWriter = new StreamWriter(fs1, Encoding.UTF8);
                swWriter.Flush();
                swWriter.Write(str);
                swWriter.Close();
                fs1.Close();
            }
        }
        catch { }
    }

    public virtual void Debug(params string[] messages)
    {
        foreach (string item in messages)
        {
            this.Logger.Debug(item);
        }
    }

    public virtual void Error(params string[] messages)
    {
        foreach (string item in messages)
        {
            this.Logger.Error(item);
        }
    }

    public virtual void Fatal(params string[] messages)
    {
        foreach (string item in messages)
        {
            this.Logger.Fatal(item);
        }
    }

    public virtual void Fatal(params Exception[] messages)
    {
        foreach (Exception item in messages)
        {
            this.Logger.Fatal(item.Message, item);
        }
    }

    public virtual void Trace(params string[] messages)
    {
        foreach (string item in messages)
        {
            this.Logger.Debug(item);
        }
    }

    public virtual void Warn(params string[] messages)
    {
        foreach (string item in messages)
        {
            this.Logger.Warn(item);
        }
    }

    public virtual void Error(params Exception[] messages)
    {
        foreach (Exception item in messages)
        {
            this.Logger.Error(item);
        }
    }

    public virtual void Info(params string[] messages)
    {
        foreach (string item in messages)
        {
            this.Logger.Info(item);
        }
    }
}
