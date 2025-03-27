namespace H.Extensions.Torrent.Provider;

public sealed class Sha1Hash
{
    public const int Length = 20;
    public static readonly Sha1Hash Empty;

    static Sha1Hash()
    {
        Empty = new Sha1Hash(new byte[Length]);
    }
    public Sha1Hash(byte[] value)
    {
        if (value == null || value.Length != Length)
            throw new ArgumentException(string.Format("Value must be {0} bytes.", Length));

        this.Value = value;
    }

    public byte[] Value { get; }

    public static implicit operator byte[](Sha1Hash hash)
    {
        return hash.Value;
    }

    public static bool operator ==(Sha1Hash x, Sha1Hash y)
    {
        if (ReferenceEquals(x, y))
            return true;
        else if ((object)x == null || (object)y == null)
            return false;
        else
            return x.Value.SequenceEqual(y.Value);
    }

    public static bool operator !=(Sha1Hash x, Sha1Hash y)
    {
        return !(x == y);
    }

    public override bool Equals(object obj)
    {
        if (obj is Sha1Hash)
        {
            Sha1Hash other = (Sha1Hash)obj;
            return this.Value.SequenceEqual(other.Value);
        }
        else
        {
            return false;
        }
    }

    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            foreach (byte el in this.Value)
                hash = hash * 31 + el.GetHashCode();
            return hash;
        }
    }

    public override string ToString()
    {
        string base64 = Convert.ToBase64String(this.Value);
        return base64.Substring(0, 8);
    }
}
