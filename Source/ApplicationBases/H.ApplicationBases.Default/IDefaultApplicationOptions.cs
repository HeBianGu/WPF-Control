using H.ApplicationBases.Module;

namespace H.ApplicationBases.Default
{
    public interface IDefaultApplicationOptions
    {
        void UseModuleDefaultOptions(Action<IModuleDefaultOptions> action);
    }
}
