using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public class Ioc
    {
        private static IServiceProvider _services = null;
        public static IServiceProvider Services => _services;
        public static void Build(IServiceProvider services)
        {
            _services = services;
        }
    }
}
