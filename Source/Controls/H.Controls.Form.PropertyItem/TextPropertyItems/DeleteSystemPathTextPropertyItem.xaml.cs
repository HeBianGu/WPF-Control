// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control
using H.Mvvm;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Input;

namespace H.Controls.Form.PropertyItem.TextPropertyItems
{
    public class DeleteSystemPathTextPropertyItem : OpenDeleteSystemPathTextPropertyItem
    {
        public DeleteSystemPathTextPropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {

        }

        protected override IEnumerable<IDisplayCommand> CreateCommands()
        {
            yield return this.ClearCommand;
        }
    }
}
