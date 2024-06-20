namespace H.Extensions.Torrent
{
    public class TorrentFile
    {
        public TorrentFile(string name, long size)
        {
            this.Name = name;
            this.Size = size;
        }

        public string Name { get; }
        public long Size { get; }
    }
}
