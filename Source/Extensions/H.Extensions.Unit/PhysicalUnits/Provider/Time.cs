using System;

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
