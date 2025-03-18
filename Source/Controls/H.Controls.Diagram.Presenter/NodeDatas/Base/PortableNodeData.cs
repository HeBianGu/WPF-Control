global using System.Runtime.Serialization;
namespace H.Controls.Diagram.Presenter.NodeDatas.Base;

public abstract class PortableNodeData : NodeData, IPortableNodeData
{
    public List<IPortData> PortDatas { get; set; } = new List<IPortData>();
    protected List<IPortData> _defaultPortDatas;
    protected PortableNodeData()
    {
        this.InitPortDatas();
    }

    protected virtual void InitPortDatas()
    {
        List<IPortData> ds = this.CreatePortDatas().ToList();
        foreach (IPortData item in ds)
        {
            item.BuildTextData();
        }
        this._defaultPortDatas = ds.ToList();
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
        data.InitPortDatas();
        return data;
    }

    #region - Serializing -
    [OnSerializing]
    internal void OnSerializingMethod(StreamingContext context)
    {

    }

    [OnDeserialized]
    internal void OnDeserializedMethod(StreamingContext context)
    {
        this.PortDatas = this.PortDatas.Except(this._defaultPortDatas).ToList();
    }
    #endregion

}
