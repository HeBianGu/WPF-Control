using H.DataBases.Sqlite;
using H.Extensions.ApplicationBase;
using H.Modules.Identity;
using H.Modules.Login;

namespace H.ApplicationBases.Identify
{
    public class DefaultIndentifyOptions : CacheActionOptionsBase, IDefaultIndentifyOptions
    {
        public void UseLoginOptions(Action<ILoginOptions> action)
        {
            this.ConfigOptions(action);
        }

        public void UseIdentifyOptions(Action<IIdentifyOptions> action)
        {
            this.ConfigOptions(action);
        }

        public void UseRegistorOptions(Action<IRegistorOptions> action)
        {
            this.ConfigOptions(action);
        }

        public void UseSqliteSettable(Action<ISqliteSettable> action)
        {
            this.ConfigOptions(action);
        }
    }
}
