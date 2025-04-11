using H.DataBases.Sqlite;
using H.Modules.Identity;
using H.Modules.Login;

namespace H.ApplicationBases.Identify;
public interface IDefaultIndentifyOptions
{
    void UseIdentifyOptions(Action<IIdentifyOptions> action);
    void UseLoginOptions(Action<ILoginOptions> action);
    void UseRegistorOptions(Action<IRegistorOptions> action);
    void UseSqliteSettable(Action<ISqliteSettable> action);
}