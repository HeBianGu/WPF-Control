namespace H.Services.Common;

public interface IChartDataProvider
{
    IEnumerable<Tuple<string, double>> GetData();
}
