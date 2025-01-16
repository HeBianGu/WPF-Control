namespace H.Services.Common
{
    public interface IMainWindowSavableService : ISplashSave
    {
        void Load(Window window);
    }
}
