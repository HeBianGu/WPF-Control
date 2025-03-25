namespace H.Extensions.Torrent.Provider;

public class Piece
{
    public Piece(int index)
    {
        this.Index = index;
    }

    public Piece(int index, int size, Sha1Hash hash)
    {
        this.Index = index;
        this.Size = size;
        this.Hash = hash;
    }

    public int Index { get; }

    public int Size { get; }

    public Sha1Hash Hash { get; }
}
