// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using H.Common.Interfaces;
using H.Services.Common.Upgrade;
using H.Services.Message;
using H.Services.Message.Dialog;
using H.Services.Serializable.Web;
using Microsoft.Extensions.Options;
using System.Reflection;
using System.Windows;

namespace H.Modules.Upgrade;

internal class UpdateService : IUpgradeService, ISplashLoad
{
    private readonly IOptions<UpgradeOptions> _options;
    private readonly IWebXmlSerializerService _webXmlService;
    public UpdateService(IOptions<UpgradeOptions> options, IWebXmlSerializerService webXmlService)
    {
        this._options = options;
        _webXmlService = webXmlService;
    }
    private VersionData GetVersion(out string message)
    {
        message = null;
        UpdateXmlInfo args = _webXmlService.Load<UpdateXmlInfo>(_options.Value.Uri, out message);
        if (args == null)
            return null;

        bool isUpdate = new Version(args.Version) > Assembly.GetEntryAssembly().GetName().Version;
        if (!isUpdate)
        {
            message = "当前已经是最新版本";
            return null;
        }
        return new VersionData()
        {
            Version = args.Version.ToString(),
            Messages = args.Changelog?.Split(';').ToList(),
            Uri = args.Url
        };

    }

    #region - IUpgradeService -
    public bool CanUpgrade(out string message)
    {
        VersionData versionData = this.GetVersion(out message);
        return versionData != null;
    }

    public bool Upgrade(out string message)
    {
        message = null;
        VersionData data = this.GetVersion(out message);
        if (data == null)
            return false;
        bool? r = Application.Current.Dispatcher.Invoke(() =>
        {
            return IocMessage.Dialog.Show(new UpgradePresenter(data), x =>
            {
                x.DialogButton = DialogButton.None;
                x.Width = 500;
                x.Height = 400;
            }).Result;
        });
        ;
        if (r == false)
        {
            message = "用户取消更新";
            return !data.Force;
        }
        return true;
    }

    public string UpgradeVersion => this.GetVersion(out string message)?.Version;

    #endregion

    #region - ISplashLoad -

    public string Name => "版本更新";
    public bool Load(out string message)
    {
        if (this._options.Value.CheckUpdateOnStart == false)
        {
            message = "已跳过程序启动时检查更新";
            return true;
        }
        return this.Upgrade(out message);
    }
    #endregion
}
