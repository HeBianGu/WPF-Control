using System.Collections.Generic;

namespace System
{
    public interface IChartDataProvider
    {
        IEnumerable<Tuple<string, double>> GetData();
    }
}
