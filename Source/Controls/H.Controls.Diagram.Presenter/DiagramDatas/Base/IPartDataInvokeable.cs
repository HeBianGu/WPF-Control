
namespace H.Controls.Diagram.Presenter.DiagramDatas.Base;

public interface IPartDataInvokeable
{
    public void OnInvokingPart(IPartData part);
    public void OnInvokedPart(IPartData part);
}
