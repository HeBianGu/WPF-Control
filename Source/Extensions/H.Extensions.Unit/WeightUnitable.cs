using System.Collections.Generic;

namespace H.Extensions.Unit
{
    public class WeightUnitable : IntUnitableBase
    {
        protected override Dictionary<int, List<string>> CreateMap()
        {
            int u = 1000;
            var map = new Dictionary<int, List<string>>();
            map.Add(1, new List<string> { "G" });
            map.Add(u, new List<string> { "KG" });
            map.Add(u * u, new List<string> { "T" });
            return map;
        }
    }
}

