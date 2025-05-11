// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

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
