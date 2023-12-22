#if NETFRAMEWORK
using System.Data.Entity;
#endif

#if NETCOREAPP
#endif
using Microsoft.Extensions.DependencyInjection;
using System;

namespace H.DataBases.Share
{
    public interface IDbSetting
    {
        string GetConnect();
    }
}
