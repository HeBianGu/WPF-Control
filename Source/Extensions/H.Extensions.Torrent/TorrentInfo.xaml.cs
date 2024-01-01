using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace H.Extensions.Torrent
{
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
            Name = name;
            _pieces = new ReadOnlyCollection<Piece>(pieces.ToList());
            InfoHash = infoHash;
            Files = new List<TorrentFile>();
            Files.AddRange(files);
            TotalSize = Files.Any() ? Files.Sum(f => f.Size) : 0;
            Trackers = trackers.Select(x => (IReadOnlyList<Uri>)new ReadOnlyCollection<Uri>(x.ToList())).ToList().AsReadOnly();
            PieceSize = _pieces.First().Size;
            Metadata = metadata;
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
            return (long)piece.Index * (long)PieceSize;
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

            if (Files.Count == 0)
                throw new IndexOutOfRangeException();

            int fileIndex = 0;
            while (offset > Files[fileIndex].Size)
            {
                TorrentFile result = Files[fileIndex];
                offset -= result.Size;
                fileIndex++;

                if (fileIndex > Files.Count)
                    throw new IndexOutOfRangeException();
            }

            remainder = offset;
            return fileIndex;
        }

        public long FileOffset(int fileIndex)
        {
            long offset = 0;
            for (int i = 0; i < fileIndex; i++)
                offset += Files[i].Size;
            return offset;
        }
    }
}
