
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;


namespace H.Controls.Diagram.Extension
{
    public class NodeGroup : ObservableCollection<NodeData>
    {
        public NodeGroup()
        {

        }

        public NodeGroup(IEnumerable<NodeData> collection) : base(collection)
        {

        }
        public string Name { get; set; }

        public int Columns { get; set; }
    }
}
