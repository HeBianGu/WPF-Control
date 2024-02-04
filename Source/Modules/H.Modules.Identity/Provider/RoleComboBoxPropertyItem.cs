// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using H.Controls.Form;
using System.Reflection;

namespace H.Modules.Identity
{
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
}
