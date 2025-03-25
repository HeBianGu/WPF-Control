global using H.Extensions.Torrent.Provider;
global using System.Collections.ObjectModel;

namespace H.Extensions.Torrent;

public class TorrentInfo
{
    private readonly ReadOnlyCollection<Piece> _pieces;
    public TorrentInfo(string name,
                    Sha1Hash infoHash,
                    IEnumerable<TorrentFile> files,
                    IEnumerable<Piece> pieces,
                    IEnumerable<IEnumerable<Uri>> trackers,
                    IReadOnlyList<byte> metadata)
    {
        this.Name = name;
        _pieces = new ReadOnlyCollection<Piece>(pieces.ToList());
        this.InfoHash = infoHash;
        this.Files = new List<TorrentFile>();
        this.Files.AddRange(files);
        this.TotalSize = this.Files.Any() ? this.Files.Sum(f => f.Size) : 0;
        this.Trackers = trackers.Select(x => (IReadOnlyList<Uri>)new ReadOnlyCollection<Uri>(x.ToList())).ToList().AsReadOnly();
        this.PieceSize = _pieces.First().Size;
        this.Metadata = metadata;
    }
    public string Name { get; }

    public Sha1Hash InfoHash { get; }

    public IReadOnlyList<byte> Metadata { get; }

    public List<TorrentFile> Files { get; }

    public long TotalSize { get; }

    public IReadOnlyList<IReadOnlyList<Uri>> Trackers { get; }

    public int PieceSize { get; }

    public IReadOnlyList<Piece> Pieces => _pieces;

    public long PieceOffset(Piece piece)
    {
        return (long)piece.Index * (long)this.PieceSize;
    }

    public int FileIndex(long offset)
    {
        long remainder;
        return FileIndex(offset, out remainder);
    }

    public int FileIndex(long offset, out long remainder)
    {
        if (offset < 0)
            throw new IndexOutOfRangeException();

        if (this.Files.Count == 0)
            throw new IndexOutOfRangeException();

        int fileIndex = 0;
        while (offset > this.Files[fileIndex].Size)
        {
            TorrentFile result = this.Files[fileIndex];
            offset -= result.Size;
            fileIndex++;

            if (fileIndex > this.Files.Count)
                throw new IndexOutOfRangeException();
        }

        remainder = offset;
        return fileIndex;
    }

    public long FileOffset(int fileIndex)
    {
        long offset = 0;
        for (int i = 0; i < fileIndex; i++)
            offset += this.Files[i].Size;
        return offset;
    }
}
