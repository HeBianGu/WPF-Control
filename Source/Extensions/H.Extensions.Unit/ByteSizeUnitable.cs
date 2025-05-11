// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

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

