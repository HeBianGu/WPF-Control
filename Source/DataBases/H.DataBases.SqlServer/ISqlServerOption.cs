#if NETFRAMEWORK
using System.Data.Entity;
#endif

#if NETCOREAPP
#endif

namespace H.DataBases.SqlServer
{
    public interface ISqlServerOption
    {
        string InitialCatalog { get; set; }
        string Password { get; set; }
        string Server { get; set; }
        string UserName { get; set; }
        string ConfigPath { get; set; }
    }


}
