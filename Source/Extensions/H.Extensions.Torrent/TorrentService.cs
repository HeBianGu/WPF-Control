using BencodeNET.Parsing;
using System.IO;

namespace H.Extensions.Torrent;

public class TorrentService : ITorrentService
{
    public TorrentInfo CreateInfo(string torrentFile)
    {
        using (var stream = File.OpenRead(torrentFile))
        {
            var parser = new BencodeParser();
            var torrent = parser.Parse<BencodeNET.Torrents.Torrent>(stream);

            var files = new List<TorrentFile>();
            if (torrent.File != null)
            {
                files.Add(new TorrentFile(torrent.File.FileName, torrent.File.FileSize));
            }
            else
            {
                files.AddRange(torrent.Files.Select(x => new TorrentFile(x.FullPath, x.FileSize)));
            }

            var pieces = new List<Piece>();
            byte[] pieceHashes = torrent.Pieces;
            int pieceIndex = 0;
            for (long offset = 0; offset + torrent.PieceSize <= torrent.TotalSize; offset += torrent.PieceSize)
            {
                int length = (int)Math.Min(torrent.PieceSize, torrent.TotalSize - offset);
                byte[] hash = new byte[Sha1Hash.Length];
                Array.Copy(pieceHashes, pieceIndex * Sha1Hash.Length, hash, 0, Sha1Hash.Length);
                Piece piece = new Piece(pieceIndex, length, new Sha1Hash(hash));
                pieces.Add(piece);
                pieceIndex++;
            }

            return new TorrentInfo(torrent.DisplayName,
                                new Sha1Hash(torrent.GetInfoHashBytes()),
                                files,
                                pieces,
                                torrent.Trackers.Select(x => x.Select(y =>
                                {
                                    if (Uri.TryCreate(y, UriKind.RelativeOrAbsolute, out Uri uri))
                                        return uri;
                                    return null;
                                }).OfType<Uri>()),
                                new byte[0]);
        }

    }
}
