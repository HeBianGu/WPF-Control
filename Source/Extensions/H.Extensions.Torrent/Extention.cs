using H.Extensions.Torrent;
using Microsoft.Extensions.DependencyInjection;

namespace System;

public static class Extention
{
    /// <summary>
    /// 注册
    /// </summary>
    /// <param name="service"></param>
    public static void AddTorrent(this IServiceCollection service)
    {
        service.AddSingleton<ITorrentService, TorrentService>();
    }
}
