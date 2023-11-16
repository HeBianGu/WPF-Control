namespace H.Controls.Diagram.Extension
{
    public interface IDiagramTemplate
    {
        public string Name { get; set; }

        public string GroupName { get; set; }

        public string TypeName { get; set; }

        public IDiagram Diagram { get; set; }
    }
}
