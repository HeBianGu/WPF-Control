// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace H.Modules.Identity
{
    [Display(Name = "权限管理")]
    public class hi_dd_author : DbModelBase
    {
        public hi_dd_author()
        {
            this.Name = "默认权限";
        }

        private string _name;
        [Required]
        [Display(Name = "权限名称")]
        [RegularExpression(@"^[\u4e00-\u9fa5]{0,}$", ErrorMessage = "只能输入汉字！")]
        [Column("author_name", Order = 1)]
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged();
            }
        }

        private string _authorCode;
        [Display(Name = "权限编码")]
        [Column("author_code", Order = 2)]
        public string AuthorCode
        {
            get { return _authorCode; }
            set
            {
                _authorCode = value;
                RaisePropertyChanged();
            }
        }

        [System.Text.Json.Serialization.JsonIgnore]

        [System.Xml.Serialization.XmlIgnore]
        [Display(Name = "角色列表")]
        //[PropertyItemType(Type = typeof(MultiSelectRepositoryPropertyItem))]
        public virtual ICollection<hi_dd_role> Roles { get; set; } = new ObservableCollection<hi_dd_role>();

        public override string ToString()
        {
            return this.Name;
        }

    }
}
