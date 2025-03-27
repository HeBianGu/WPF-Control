namespace H.Common;

public class LazyInstance<T> where T : new()
{
    public static T Instance = new Lazy<T>(() => new T()).Value;
}
