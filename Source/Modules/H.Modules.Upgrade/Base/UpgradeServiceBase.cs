// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Services.Common.Upgrade;
using H.Services.Logger;
using Microsoft.Extensions.Options;

namespace H.Modules.Upgrade.Base;

internal abstract class UpgradeServiceBase : IUpgradeService
{
    protected readonly IOptions<UpgradeOptions> _options;

    protected UpgradeServiceBase(IOptions<UpgradeOptions> options)
    {
        this._options = options;
    }

    #region - IUpgradeService -

    private async Task<(bool success, string message)> ShowUpgradeWindowAsync(UpgradeVersionData versionData, Action<UpgradePresenter> option = null)
    {
        bool? r = await Application.Current.Dispatcher.Invoke(() =>
        {
            var presenter = new UpgradePresenter(versionData);
            option?.Invoke(presenter);
            return IocMessage.Window.Show(presenter, x =>
            {
                x.DialogButton = DialogButton.None;
                x.Title = "检查更新";
                x.Width = 500;
                x.Height = 400;
            });
        });
        if (r == true)
        {
            string message = "退出程序，等待用户安装完成后重新启动";
            IocLog.Info(message);
            return (false, message);
        }
        if (versionData.Force)
        {
            string message = "强制更新，退出程序";
            IocLog.Info(message);
            return (false, message);
        }

        {
            string message = "用户取消更新";
            IocLog.Info(message);
            return (true, message);
        }
    }

    public async Task<(bool success, string message)> UpgradeAsync()
    {
        var rdata = await this.GetVersionAsync();
        if (rdata.data == null)
            return (true, rdata.message);
        return await this.ShowUpgradeWindowAsync(rdata.data);
    }

    #endregion

    #region - ISplashLoad -

    public string Name => "版本更新";
    public bool Load(out string message)
    {
        if (this._options.Value.CheckUpgradeOnStart == false)
        {
            message = "已跳过程序启动时检查更新";
            IocLog.Info(message);
            return true;
        }
        var r = this.UpgradeAsync();
        message = r.Result.message;
        return r.Result.success;
    }
    #endregion

    protected abstract Task<(IUpgradeInfo updateInfo, string message)> GetUpdateInfoAsync();

    private async Task<(UpgradeVersionData data, string message)> GetVersionAsync()
    {
        var currentVersion = Assembly.GetEntryAssembly().GetName().Version;
        this._options.Value.HasNewVersion = false;
        this._options.Value.UpgradeVersion = currentVersion.ToString();
        var updateInfo = await this.GetUpdateInfoAsync();
        if (updateInfo.updateInfo == null)
            return (null, updateInfo.message);

        this._options.Value.UpgradeVersion = updateInfo.updateInfo.Version;
        this._options.Value.HasNewVersion = true;
        bool isUpdate = new Version(updateInfo.updateInfo.Version) > Assembly.GetEntryAssembly().GetName().Version;
        if (!isUpdate)
        {
            string message = "当前已经是最新版本";
            return (null, message);
        }
        return (new UpgradeVersionData()
        {
            Version = updateInfo.updateInfo.Version.ToString(),
            Messages = updateInfo.updateInfo.Changelog?.ToList(),
            Uri = updateInfo.updateInfo.Url
        }, null);

    }

    public async Task<(bool success, string message)> ShowUpgradeAsync()
    {
        var versionData = await IocMessage.Dialog.ShowWait(x =>
        {
            //x.Title = "正在检查软件更新...";
            return this.GetVersionAsync().Result;
        });

        if (versionData.data == null)
            return (true, versionData.message);
        return await this.ShowUpgradeWindowAsync(versionData.data, x => x.UseCheckUpgradeOnStart = false);
    }
}
