global using H.Services.Common.SplashScreen;
namespace H.App.FileManager
{
    public class AppSaveService : IAppSaveService
    {
        public string Name => "应用程序保存";

        public bool Save(out string message)
        {
            return IocProject.Instance.Current.Save(out message);
        }
    }
}
