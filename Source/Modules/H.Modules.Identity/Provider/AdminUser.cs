// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

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
