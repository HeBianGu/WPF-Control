namespace System
{
    public abstract class IocThrowIfNone<Interface>
    {
        public static Interface Instance => Ioc.GetService<Interface>();
    }
}
