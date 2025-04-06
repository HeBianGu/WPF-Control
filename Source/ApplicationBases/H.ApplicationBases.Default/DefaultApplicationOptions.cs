using H.ApplicationBases.Module;

namespace H.ApplicationBases.Default
{
    public class DefaultApplicationOptions : IDefaultApplicationOptions
    {
        public Action<IModuleDefaultOptions> ModuleDefaultOptions { get; private set; }

        public void UseModuleDefaultOptions(Action<IModuleDefaultOptions> action)
        {
            this.ModuleDefaultOptions = action;
        }
    }
}
