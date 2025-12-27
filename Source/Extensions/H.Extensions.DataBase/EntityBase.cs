// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace H.Extensions.DataBase;

public abstract class EntityBase<TPrimaryKey> : IEntityBase<TPrimaryKey>
{
    /// <summary>
    /// 主键
    /// </summary>
    [Browsable(false)]
    [ReadOnly(true)]
    [Column("id", Order = 0)]
    public virtual TPrimaryKey ID { get; set; }
}