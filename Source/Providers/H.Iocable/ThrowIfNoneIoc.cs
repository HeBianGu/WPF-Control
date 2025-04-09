namespace H.Iocable;

public abstract class ThrowIfNoneIoc<Interface>
{
    public static Interface Instance => Ioc.GetService<Interface>(true);
}
