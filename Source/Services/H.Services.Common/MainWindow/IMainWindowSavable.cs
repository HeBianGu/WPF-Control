using H.Common.Interfaces;

namespace H.Services.Common.MainWindow;

public interface IMainWindowSavableService : ISplashSave
{
    void Load(Window window);
}
