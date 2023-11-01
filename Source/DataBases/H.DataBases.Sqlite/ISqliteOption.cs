


#if NETFRAMEWORK
using System.Data.Entity;
#endif

#if NETCOREAPP
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.Sqlite;
#endif
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Serialization;
using System;

namespace H.DataBases.Sqlite
{
    public interface ISqliteOption
    {
        string InitialCatalog { get; set; }
        string FilePath { get; set; }
        string ConfigPath { get; set; }
    }
}
