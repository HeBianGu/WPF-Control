using System;
namespace H.Extensions.Unit
{
    public interface IUnitable<T> where T : IComparable<T>
    {
        string ToString(T value);
        T Parse(string str);
    }

}

