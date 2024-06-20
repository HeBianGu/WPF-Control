namespace H.Extensions.Unit
{

    public interface IUnitable
    {
        string ToString(object value);
        object Parse(string str);
    }
}

