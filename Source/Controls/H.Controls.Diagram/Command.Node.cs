// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase



using H.Providers.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace H.Controls.Diagram
{
    public class RemoveNodeCommand : MarkupCommandBase
    {
        public override void Execute(object parameter)
        {
            if (parameter is Node node)
                node.Delete();

            if (parameter is ContextMenu menu)
            {
                menu.PlacementTarget.GetParent<Node>()?.Delete();
            }
        }
    }
}
