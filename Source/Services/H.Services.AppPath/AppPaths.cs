namespace H.Services.AppPath;

public static class AppPaths
{
    private static IAppPathServce _instance;
    public static IAppPathServce Instance
    {
        get
        {
            if (_instance == null)
                throw new Exception("AppPathServce is not registered.");
            return _instance;
        }
    }
    public static void Register(IAppPathServce instance)
    {
        _instance = instance;
    }
}