using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Providers.Ioc
{
   
    public interface IExcelService
    {
        bool Export(IEnumerable<object> collection, string path, string sheetName, out string message);
        IEnumerable<T> Read<T>(string filePath, Func<object, T> convert, out string message);
    }

    public class Exceler : Ioc<IExcelService>
    {

    }
}
