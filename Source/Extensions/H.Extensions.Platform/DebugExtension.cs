using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Diagnostics
{
    public static class DebugExtension
    {
        public static async Task Write(string message)
        {
            await message.Write();
        }

        public static async Task Error(string message)
        {
            await message.Error();
        }
    }
}
