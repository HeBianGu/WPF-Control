// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.Form.Attributes;
using H.Controls.Form.PropertyItem.TextPropertyItems;
using H.Services.Identity.User;
using System.ComponentModel.DataAnnotations;

namespace H.Modules.Login.Base
{
    public class TestUser : IUser
    {
        public TestUser(string account, string password, string name)
        {
            this.Account = account;
            this.Password = password;
            this.Name = name;
        }

        public string ID => "{C44465C0-AFBC-405D-ADA8-94A7825E7699}";
        [Display(Name = "账号")]
        public string Account { get; set; }
        [Display(Name = "密码")]
        [PropertyItem(typeof(PasswordTextPropertyItem))]
        public string Password { get; set; }
        [Display(Name = "显示名称")]
        public string Name { get; set; }
        public bool IsValid(string authorId)
        {
            return true;
        }
    }
}
