



using System;
using System.Linq;
using System.Windows;
using H.Providers.Ioc;

#if NETFRAMEWORK
using System.Data.Entity;
#endif

#if NETCOREAPP
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
#endif
namespace H.DataBases.Share
{
    public interface IDbSetting
    {
        string GetConnect();
    }



}
