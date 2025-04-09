using H.Common.Attributes;
namespace H.Modules.Guide;
[Icon("\xEC92")]
[Display(Name = "功能列表", Description = "显示版本功能列表")]
public class ShowGuideTreeCommand : ShowGuideTreeCommandBase
{
    protected override bool IsMatch(UIElement element)
    {
        return true;
    }
}
