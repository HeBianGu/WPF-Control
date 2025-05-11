// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.DataBase;
using H.Services.Operation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace H.Modules.Operation
{
    [Display(Name = "操作日志")]
    public class hi_dd_operation : DbModelBase
    {
        [Browsable(false)]
        public string UserID { get; set; }

        [Display(Name = "操作账号")]
        public string UserName { get; set; }

        [Display(Name = "操作")]
        public string Method { get; set; }

        [Display(Name = "信息")]
        public string Title { get; set; }

        [Display(Name = "详情")]
        public string Message { get; set; }

        [Display(Name = "类型")]
        public string Type { get; set; }

        [Display(Name = "操作类型")]
        public OperationType OperationType { get; set; }

        [Display(Name = "结果")]
        public bool Result { get; set; }

        [Display(Name = "操作日期")]
        public DateTime Time { get; set; } = DateTime.Now;
    }
}
