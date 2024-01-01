namespace H.Extensions.Torrent
{
    public class TorrentFile
    {
        public TorrentFile(string name, long size)
        {
            Name = name;
            Size = size;
        }

        public string Name { get; }
        public long Size { get; }
    }
}
