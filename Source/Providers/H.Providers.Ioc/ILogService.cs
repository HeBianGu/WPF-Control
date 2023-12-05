using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public interface ILogService
    {
        void Debug(params string[] messages);
        void Error(params Exception[] messages);
        void Error(params string[] messages);
        void Fatal(params string[] messages);
        void Fatal(params Exception[] messages);
        void Info(params string[] messages);
        void Trace(params string[] messages);
        void Warn(params string[] messages);
    }

    public class IocLog : Ioc<ILogService>
    {

    }
}
