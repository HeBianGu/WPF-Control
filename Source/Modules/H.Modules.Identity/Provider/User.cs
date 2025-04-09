// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

global using System.ComponentModel;
global using System.ComponentModel.DataAnnotations;
global using H.Controls.Form.Attributes;
global using H.Mvvm.ViewModels;

namespace H.Modules.Identity
{
    public class User : SelectBindable<hi_dd_user>, IUser
    {
        public User() : base(new hi_dd_user())
        {

        }
        public User(hi_dd_user model) : base(model)
        {
            this.Role = new Role(model.Role);
        }

        [Browsable(false)]
        public string ID => this.Model.ID;

        [Required]
        [Display(Name = "用户名称")]
        public string Name
        {
            get { return this.Model.Name; }
            set
            {
                this.Model.Name = value;
                RaisePropertyChanged();
            }
        }

        //private string _name;
        [Required]
        [Display(Name = "登录名称")]
        public string Account
        {
            get { return this.Model.Account; }
            set
            {
                this.Model.Account = value;
                RaisePropertyChanged();
            }
        }


        [Required]
        [Display(Name = "登录密码")]
        public string Password
        {
            get { return this.Model.Password; }
            set
            {
                this.Model.Password = value;
                RaisePropertyChanged();
            }
        }

        //[Browsable(false)]
        //public string RoleID { get; set; }

        private Role _role;
        [System.Text.Json.Serialization.JsonIgnore]
        
        [System.Xml.Serialization.XmlIgnore]
        [Required]
        [Display(Name = "用户角色")]
        [PropertyItem(typeof(RoleComboBoxPropertyItem))]
        public Role Role
        {
            get { return _role; }
            set
            {
                _role = value;
                //this.RoleID = value?.ID;
                this.Model.Role = value?.Model;
                this.Model.RoleID = value?.Model?.ID;
                RaisePropertyChanged();
            }
        }

        public virtual bool IsValid(string authorId)
        {
            if (this.Role == null) return false;
            return this.Role.IsValid(authorId);
        }
    }
}
