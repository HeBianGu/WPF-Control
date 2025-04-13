// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using H.Controls.Form.Attributes;
using H.Controls.Form.PropertyItem.TextPropertyItems;
using H.Services.Identity.User;

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
