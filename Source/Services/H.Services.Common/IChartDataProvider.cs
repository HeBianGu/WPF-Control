﻿namespace System
{
    public interface IChartDataProvider
    {
        IEnumerable<Tuple<string, double>> GetData();
    }
}
