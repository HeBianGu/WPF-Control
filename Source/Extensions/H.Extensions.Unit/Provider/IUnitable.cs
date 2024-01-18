using System;

namespace H.Extensions.Unit
{

    public interface IUnitable
    {
        string ToString(object value);
        object ToValue(string str);

        int Digits { get; set; }
    }
}

