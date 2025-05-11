// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.Form.PropertyItem.ComboBoxPropertyItems;
using System.Reflection;

namespace H.Modules.Identity
{
    public class RoleComboBoxPropertyItem : FormComboBoxPropertyItem
    {
        public RoleComboBoxPropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {

        }

        //protected override IEnumerable<object> CreateSource()
        //{
        //    return RoleViewPresenterProxy.Instance?.GetRoles();
        //}
    }
}
