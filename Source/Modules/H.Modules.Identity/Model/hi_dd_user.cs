
using H.Controls.Form;
using H.Extensions.Behvaiors;
using H.Providers.Ioc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace H.Modules.Identity
{
    [Display(Name = "用户管理")]
    public class hi_dd_user : DbModelBase
    {

        public hi_dd_user()
        {
            Name = "普通用户";
            Account = "user";
            Password = "123456";
        }
        private string _name;
        [Display(Name = "用户名称")]
        [Required]
        [RegularExpression(@"^[\u4e00-\u9fa5]{0,}$", ErrorMessage = "只能输入汉字！")]
        [Column("user_name", Order = 1)]
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged();
            }
        }
        private string _account;
        [Display(Name = "登陆账号")]
        [Required]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9_]{4,15}$", ErrorMessage = "字母开头，允许5-16字节，允许字母数字下划线！")]
        [Column("account", Order = 2)]
        public string Account
        {
            get { return _account; }
            set
            {
                _account = value;
                RaisePropertyChanged();
            }
        }


        private string _password;
        [Display(Name = "登陆密码")]
        [Required]
        [Column("password", Order = 3)]
        [RegularExpression(@"^[0-9]{5,17}$", ErrorMessage = "以字母开头，长度在6~18之间，只能包含字母、数字和下划线！！")]
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                RaisePropertyChanged();
            }
        }

        private string _displayName;
        [Display(Name = "昵称")]
        [Column("display", Order = 4)]
        /// <summary> 说明  </summary>
        public string DisplayName
        {
            get { return _displayName; }
            set
            {
                _displayName = value;
                RaisePropertyChanged();
            }
        }

        [Browsable(false)]
        [Column("role_id", Order = 5)]
        //[Binding("Role.ID")]
        public string RoleID { get; set; }

        private hi_dd_role _role;
        [XmlIgnore]
        [Required]
        [Display(Name = "角色")]
        [Column("role", Order = 6)]
        [DataGridColumn("*", PropertyPath = "{0}.Name")]
        [BindingGetSelectSourceMethod(nameof(GetRoles))]
        [PropertyItemType(Type = typeof(OnlyComboBoxSelectSourcePropertyItem))]
        public virtual hi_dd_role Role
        {
            get { return _role; }
            set
            {
                _role = value;
                RaisePropertyChanged();
            }
        }

        public List<hi_dd_role> GetRoles()
        {
            return Ioc.GetService<IStringRepository<hi_dd_role>>().GetList();
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
