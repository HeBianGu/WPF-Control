using H.Mvvm;
using H.Mvvm.ViewModels.Base;
namespace H.Modules.Guide;
public class GuideTreePresenter : DisplayBindableBase
{
    private readonly GuideTree _guideTree;
    public GuideTreePresenter(GuideTree guideTree)
    {
        this._guideTree = guideTree;
    }

    public GuideTree GuideTree => this._guideTree;
}
