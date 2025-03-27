namespace H.Services.AppPath;

public interface IAppPathServce
{
    string AppName { get; }
    string AppPath { get; }
    string Cache { get; }
    string Company { get; set; }
    string Component { get; }
    string Config { get; }
    string ConfigExtention { get; set; }
    string Data { get; }
    string Default { get; }
    string Document { get; set; }
    string License { get; }
    string Log { get; }
    string Module { get; }
    string Project { get; }

    string DefaultProjects { get; }
    string RegistryPath { get; }
    string Setting { get; }
    string Template { get; }
    string UserCache { get; }
    string UserData { get; }
    string UserLicense { get; }
    string UserLog { get; }
    string UserPath { get; }
    string UserProject { get; }
    string UserSetting { get; }
    string UserTemplate { get; }
    string Version { get; }
}

public static class AppPathExtension
{
    public static bool ClearCache(this IAppPathServce servce, out string message)
    {
        try
        {
            Directory.Delete(servce.Cache, true);
            message = null;
            return true;
        }
        catch (Exception ex)
        {
            message = ex.Message;
            return false;
        }
    }

    /// <summary>
    /// 清空设置
    /// </summary>
    /// <returns>清空是否成功</returns>
    public static bool ClearSetting(this IAppPathServce servce, out string message)
    {
        try
        {
            if (Directory.Exists(servce.Setting))
                Directory.Delete(servce.Setting, true);
            if (Directory.Exists(servce.UserSetting))
                Directory.Delete(servce.UserSetting, true);
            message = null;
            return true;
        }
        catch (Exception ex)
        {
            message = ex.Message;
            return false;
        }
    }
}
