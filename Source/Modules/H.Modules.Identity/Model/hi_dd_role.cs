using H.Extensions.DataBase;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace H.Modules.Identity
{
    [Display(Name = "角色管理")]
    public class hi_dd_role : DbModelBase
    {
        public hi_dd_role()
        {
            this.Name = "普通角色";
        }

        private string _name;
        [Display(Name = "角色名称")]
        [Column("role_name", Order = 1)]
        [Required]
        [RegularExpression(@"^[\u4e00-\u9fa5]{0,}$", ErrorMessage = "只能输入汉字！")]
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged();
            }
        }


        private string _code;
        [Display(Name = "角色编码")]
        [Column("role_code", Order = 2)]
        public string Code
        {
            get { return _code; }
            set
            {
                _code = value;
                RaisePropertyChanged();
            }
        }

        [System.Text.Json.Serialization.JsonIgnore]
        
        [System.Xml.Serialization.XmlIgnore]
        [Display(Name = "权限列表")]
        //[PropertyItemType(Type = typeof(MultiSelectRepositoryPropertyItem))] 
        public virtual ICollection<hi_dd_author> Authors { get; set; } = new ObservableCollection<hi_dd_author>();

        [System.Text.Json.Serialization.JsonIgnore]
        
        [System.Xml.Serialization.XmlIgnore]
        [Display(Name = "用户列表")]
        //[PropertyItemType(Type = typeof(MultiSelectRepositoryPropertyItem))]
        public virtual ICollection<hi_dd_user> Users { get; set; } = new ObservableCollection<hi_dd_user>();

        public override string ToString()
        {
            return this.Name;
        }
    }
}
