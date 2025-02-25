namespace H.Controls.Diagram.Presenter.NodeDatas.Base;

public abstract class PortableNodeData : NodeData, IPortableNodeData
{
    //[XmlIgnore]
    [Browsable(false)]
    public List<IPortData> PortDatas { get; set; } = new List<IPortData>();

    protected override void InitPort()
    {
        List<IPortData> ds = this.CreatePortDatas().ToList();
        foreach (IPortData item in ds)
        {
            item.BuildTextData();
        }
        this.PortDatas = ds.ToList();
    }

    protected virtual IEnumerable<IPortData> CreatePortDatas()
    {
        {
            IPortData port = CreatePortData();
            port.Dock = Dock.Left;
            port.PortType = PortType.Both;
            yield return port;
        }

        {
            IPortData port = CreatePortData();
            port.Dock = Dock.Right;
            port.PortType = PortType.Both;
            yield return port;
        }

        {
            IPortData port = CreatePortData();
            port.Dock = Dock.Top;
            port.PortType = PortType.Both;
            yield return port;
        }

        {
            IPortData port = CreatePortData();
            port.Dock = Dock.Bottom;
            port.PortType = PortType.Both;
            yield return port;
        }
    }


    public override object Clone()
    {
        PortableNodeData data = base.Clone() as PortableNodeData;
        data.PortDatas.Clear();
        data.InitPort();
        return data;
    }
}
