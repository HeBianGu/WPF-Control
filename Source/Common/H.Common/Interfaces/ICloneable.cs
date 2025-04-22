namespace H.Common.Interfaces;

public interface ICloneable<T> where T : ICloneable<T>
{
    T Clone();
}