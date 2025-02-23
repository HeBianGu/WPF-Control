using System.Reflection;

namespace HeBianGu.Diagram.OpenCV;

public static class AssemblyExtension
{
    public static IEnumerable<T> GetInstances<T>(this Assembly assembly)
    {
        var types = typeof(T).Assembly.GetTypes();
        types = types.Where(t => t.IsClass && !t.IsAbstract).ToArray();
        types = types.Where(t => typeof(T).IsAssignableFrom(t)).ToArray();
        return types.Select(t => Activator.CreateInstance(t)).OfType<T>();
    }
}