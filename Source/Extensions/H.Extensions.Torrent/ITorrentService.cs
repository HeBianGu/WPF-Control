﻿using System;

namespace H.Extensions.Torrent
{
    public interface ITorrentService
    {
        TorrentInfo CreateInfo(string torrentFile);
    }

    public class IocTorrentService : Ioc<ITorrentService>
    {

    }
}