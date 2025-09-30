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
using H.DataBases.Share;
using H.Services.Setting;
using System.ComponentModel.DataAnnotations;

namespace H.DataBases.Sqlite
{
    public class SqliteSettable : SqliteSettableBase<SqliteSettable>, ISqliteSettable, IDbSettable
    {

    }
}
