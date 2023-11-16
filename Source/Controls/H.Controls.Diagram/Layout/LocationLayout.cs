// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase


using H.Extensions.TypeConverter;
using System.Collections.Generic;
using System.ComponentModel;

namespace H.Controls.Diagram
{
    [DisplayName("LocationLayout")]
    [TypeConverter(typeof(DisplayNameConverter))]
    public class LocationLayout : Layout
    {
        /// <summary> 布局点和线 </summary>
        public override void DoLayout(params Node[] nodes)
        {
            foreach (Node node in nodes)
            {
                //  Do ：布局Node
                NodeLayer.SetPosition(node, node.Location);
            }

            //this.UpdateNode(nodes);
        }

        public override void RemoveNode(params Node[] nodes)
        {
            foreach (var node in nodes)
            {
                node.Delete();
            }
        }

        public override void AddNode(params Node[] nodes)
        {
            this.DoLayout(nodes);
        }
    }
}
