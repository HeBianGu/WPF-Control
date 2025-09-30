// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

#if NETFRAMEWORK
using System.Data.Entity;
#endif

#if NETCOREAPP
#endif

namespace H.DataBases.SqlServer
{
    public interface ISqlServerOption
    {
        string InitialCatalog { get; set; }
        string Password { get; set; }
        string Server { get; set; }
        string UserName { get; set; }
        string ConfigPath { get; set; }
    }

}
