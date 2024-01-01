namespace H.Extensions.Torrent
{
    public class Piece
    {
        public Piece(int index)
        {
            Index = index;
        }

        public Piece(int index, int size, Sha1Hash hash)
        {
            Index = index;
            Size = size;
            Hash = hash;
        }

        public int Index { get; }

        public int Size { get; }

        public Sha1Hash Hash { get; }
    }
}
