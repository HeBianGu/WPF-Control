using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Extensions.Unit
{
    public partial struct  TangentialVelocity
    {
        public static implicit operator TangentialVelocity(Velocity v)
        {
            return new TangentialVelocity(v.Value);
        }

    }
}
