namespace H.Ioc;

public abstract class IocThrowIfNone<Interface>
{
    public static Interface Instance => System.Ioc.GetService<Interface>();
}
