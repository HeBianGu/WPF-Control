using System.Collections.Generic;


namespace H.Extensions.Unit
{
    public class ByteSizeUnitable : LongUnitableBase
    {
        protected override Dictionary<long, List<string>> CreateMap()
        {
            long u = 1024;
            var map = new Dictionary<long, List<string>>();
            map.Add(1, new List<string> { "Byte" });
            map.Add(u, new List<string> { "KB" });
            map.Add(u * u, new List<string> { "MB" });
            map.Add(u * u * u, new List<string> { "G" });
            map.Add(u * u * u * u, new List<string> { "T" });
            return map;
        }
    }
}

