// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Extensions.Unit
{
    public class MillimeterUnitable : LongUnitableBase
    {
        protected override Dictionary<long, List<string>> CreateMap()
        {
            long u = 1000;
            var map = new Dictionary<long, List<string>>();
            map.Add(1, new List<string> { "MM" });
            map.Add(u, new List<string> { "CM" });
            map.Add(u * u, new List<string> { "DM" });
            map.Add(u * u * u, new List<string> { "M" });
            map.Add(u * u * u * u, new List<string> { "KM" });
            return map;
        }
    }
}

