// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Modules.Identity
{
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
}
