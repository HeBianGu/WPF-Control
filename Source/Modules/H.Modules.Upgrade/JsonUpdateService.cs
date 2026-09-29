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


internal class JsonUpdateService : UpgradeServiceBase
{
    private readonly IWebJsonSerializerService _webJsonService;
    public JsonUpdateService(IOptions<UpgradeOptions> options, IWebJsonSerializerService webJsonService) : base(options)
    {
        _webJsonService = webJsonService;
    }

    protected override async Task<(IUpgradeInfo updateInfo, string message)> GetUpdateInfoAsync()
    {
        var args = await _webJsonService.LoadAsync<JsonUpgradeInfo>(_options.Value.UpgradeInfoUri, x => x.Timeout = TimeSpan.FromSeconds(5));
        return (args.value, args.message);
    }
}
