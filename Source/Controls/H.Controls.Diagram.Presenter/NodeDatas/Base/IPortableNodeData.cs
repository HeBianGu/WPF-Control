namespace H.Controls.Diagram.Presenter.NodeDatas.Base;

public interface IPortableNodeData : ILinkDataCreator
{
    public List<IPortData> PortDatas { get; set; }
}
