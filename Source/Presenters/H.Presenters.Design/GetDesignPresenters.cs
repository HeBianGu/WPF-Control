using H.Common.Interfaces;
using System.Windows.Markup;
namespace H.Presenters.Design;
public class GetDesignPresenters : MarkupExtension
{
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return this.GetType().GetInstances<IDesignPresenter>().ToList();
    }
}
