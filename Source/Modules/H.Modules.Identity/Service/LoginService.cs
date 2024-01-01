// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using H.Controls.Form;
using H.Providers.Ioc;
using H.Providers.Mvvm;


using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

namespace H.Modules.Identity
{
    internal class LoginService : ViewModelBase, ILoginService
    {
        private IUser _user;
        public IUser User
        {
            get { return _user; }
            private set
            {
                _user = value;
                RaisePropertyChanged();
            }
        }

        public bool Login(string name, string password, out string message)
        {
            message = "登录成功";
            if (string.IsNullOrEmpty(name))
            {
                message = "用户名不能为空";
                return false;
            }

            if (string.IsNullOrEmpty(password))
            {
                message = "密码不能为空";
                return false;
            }

            if (AdminUser.Instance.Account == name && AdminUser.Instance.Password == password)
            {
                this.User = AdminUser.Instance;
            }
            else
            {
                IStringRepository<hi_dd_user> reporitory = Ioc.GetService<IStringRepository<hi_dd_user>>();
                hi_dd_user user = reporitory.GetList(x => x.Account.ToLower() == name.ToLower()).FirstOrDefault();
                if (user == null)
                {
                    message = "用户名不正确";
                    return false;
                }
                if (user.Password.ToLower() != password.ToLower())
                {
                    message = "密码不正确";
                    return false;
                }
                this.User = new User(user);
            }
            Ioc<IOperationService>.Instance?.Log<hi_dd_user>("登录", "登录成功");
            return true;
        }

        public bool Logout(out string message)
        {
            message = null;
            Ioc<IOperationService>.Instance?.Log<hi_dd_user>("登录", "退出登录");
            User = null;
            return true;
        }
    }

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

    public class User : SelectViewModel<hi_dd_user>, IUser
    {
        public User() : base(new hi_dd_user())
        {

        }
        public User(hi_dd_user model) : base(model)
        {
            Role = new Role(model.Role);
        }

        [Browsable(false)]
        public string ID => Model.ID;

        [Required]
        [Display(Name = "用户名称")]
        public string Name
        {
            get { return Model.Name; }
            set
            {
                Model.Name = value;
                RaisePropertyChanged();
            }
        }

        //private string _name;
        [Required]
        [Display(Name = "登录名称")]
        public string Account
        {
            get { return Model.Account; }
            set
            {
                Model.Account = value;
                RaisePropertyChanged();
            }
        }


        [Required]
        [Display(Name = "登录密码")]
        public string Password
        {
            get { return Model.Password; }
            set
            {
                Model.Password = value;
                RaisePropertyChanged();
            }
        }

        //[Browsable(false)]
        //public string RoleID { get; set; }

        private Role _role;
        [XmlIgnore]
        [Required]
        [Display(Name = "用户角色")]
        [PropertyItemType(Type = typeof(RoleComboBoxPropertyItem))]
        public Role Role
        {
            get { return _role; }
            set
            {
                _role = value;
                //this.RoleID = value?.ID;
                Model.Role = value?.Model;
                Model.RoleID = value?.Model?.ID;
                RaisePropertyChanged();
            }
        }

        public virtual bool IsValid(string authorId)
        {
            if (Role == null) return false;
            return Role.IsValid(authorId);
        }
    }

    public class RoleComboBoxPropertyItem : ComboBoxSelectSourcePropertyItem
    {
        public RoleComboBoxPropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {

        }

        //protected override IEnumerable<object> CreateSource()
        //{
        //    return RoleViewPresenterProxy.Instance?.GetRoles();
        //}
    }

    //public class Author : NotifyPropertyChangedBase, IAuthor
    //{
    //    [Browsable(false)]
    //    public string ID { get; set; } = Guid.NewGuid().ToString();
    //    private string _name;
    //    [Required]
    //    [Display(Name = "权限名称")]
    //    public string Name
    //    {
    //        get { return _name; }
    //        set
    //        {
    //            _name = value;
    //            RaisePropertyChanged();
    //        }
    //    }
    //}

    public class AdminRole : Role
    {
        public static AdminRole Instance = new AdminRole();
        public AdminRole() : base(new hi_dd_role() { ID = "3991C5C2-0213-483C-80A2-99AA310A31FC", Name = "管理员" })
        {

        }

        public override bool IsValid(string authorId)
        {
            return true;
        }
    }

    public class Role : SelectViewModel<hi_dd_role>, IRole
    {
        public Role() : base(new hi_dd_role())
        {

        }
        public Role(hi_dd_role model) : base(model)
        {

        }


        [Browsable(false)]
        public string ID => Model.ID;
        //private string _name;
        [Required]
        [Display(Name = "角色名称")]
        public string Name
        {
            get { return Model.Name; }
            set
            {
                Model.Name = value;
                RaisePropertyChanged();
            }
        }

        //private ObservableCollection<IAuthor> _authors = new ObservableCollection<IAuthor>();
        //[Browsable(false)]
        //public ObservableCollection<IAuthor> Authors
        //{
        //    get { return _authors; }
        //    set
        //    {
        //        _authors = value;
        //        RaisePropertyChanged();
        //    }
        //}

        public virtual bool IsValid(string authorId)
        {
            return true;
            //return Model.Authors.Any(x => x.AuthorCode == authorId);
        }
    }

    public interface ILoginOption
    {
        string Company { get; set; }
        string Copyright { get; set; }
        string LastPassword { get; set; }
        string LastUserName { get; set; }
        string Logo { get; set; }
        string Password { get; set; }
        string PasswordRegular { get; set; }
        string Product { get; set; }
        int ProductFontSize { get; set; }
        bool Remember { get; set; }
        string Title { get; set; }
        string UserName { get; set; }
        string Version { get; set; }
    }
}
