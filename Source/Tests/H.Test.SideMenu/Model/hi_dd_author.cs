﻿
using H.Services.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using H.Extensions.DataBase;

namespace H.Test.SideMenu
{
    public class hi_dd_author : DbModelBase
    {
        public hi_dd_author()
        {
            this.Name = "默认权限";
        }
        [Required]
        [Display(Name = "权限名称")]
        [RegularExpression(@"^[\u4e00-\u9fa5]{0,}$", ErrorMessage = "只能输入汉字！")]
        [Column("author_name", Order = 1)]
        public string Name { get; set; }

        [Display(Name = "权限编码")]
        [Column("author_code", Order = 2)]
        public string AuthorCode { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        
        [System.Xml.Serialization.XmlIgnore]
        [Display(Name = "角色列表")]
        //[PropertyItemType(Type = typeof(MultiSelectRepositoryPropertyItem))]
        public ICollection<hi_dd_role> Roles { get; set; }
    }
}
