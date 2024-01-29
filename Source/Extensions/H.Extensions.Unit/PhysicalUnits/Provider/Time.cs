using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace H.Extensions.Unit
{
    public partial struct Time
    {
        public Time(TimeSpan ts)
        {
            this.Value = ts.TotalSeconds;
        }

        static public implicit operator Time(TimeSpan ts)
        {
            return new Time(ts);
        }

    }
}
