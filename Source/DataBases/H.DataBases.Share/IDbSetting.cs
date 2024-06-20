#if NETFRAMEWORK
using System.Data.Entity;
#endif

#if NETCOREAPP
#endif
namespace H.DataBases.Share
{
    public interface IDbSettable
    {
        string GetConnect();
    }
}
