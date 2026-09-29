// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Modules.Upgrade.Base;
using H.Services.Serializable.Web;
using Microsoft.Extensions.Options;

namespace H.Modules.Upgrade;

internal class XmlUpgradeService : UpgradeServiceBase
{
    private readonly IWebXmlSerializerService _webXmlService;
    public XmlUpgradeService(IOptions<UpgradeOptions> options, IWebXmlSerializerService webXmlService) : base(options)
    {
        _webXmlService = webXmlService;
    }

    protected override async Task<(IUpgradeInfo updateInfo, string message)> GetUpdateInfoAsync()
    {
        var args = await _webXmlService.LoadAsync<XmlUpgradeInfo>(_options.Value.UpgradeInfoUri, x => x.Timeout = TimeSpan.FromMilliseconds(5));
        return (args.value, args.message);
    }
}
