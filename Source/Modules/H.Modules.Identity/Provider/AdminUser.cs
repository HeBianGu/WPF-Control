// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Modules.Identity
{
    public class AdminUser : User
    {
        public static AdminUser Instance = new AdminUser();
        public AdminUser() : base(new hi_dd_user() { ID = "02A15DAA-423E-46F3-884D-AF114ACDF5BA", Name = "系统管理员", Account = "admin", Password = "123456" })
        {

        }
        public override bool IsValid(string authorId)
        {
            return true;
        }
    }
}
