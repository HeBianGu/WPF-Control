// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

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

