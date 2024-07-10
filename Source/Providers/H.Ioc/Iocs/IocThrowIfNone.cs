namespace H.Ioc.Iocs;

public abstract class IocThrowIfNone<Interface>
{
    public static Interface Instance => Ioc.GetService<Interface>();
}
