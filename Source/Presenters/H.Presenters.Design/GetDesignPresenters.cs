using H.Common.Interfaces;
using H.Presenters.Design.Presenter;
using System.Windows.Markup;
namespace H.Presenters.Design;
public class GetDesignPresenters : MarkupExtension
{
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return this.GetType().GetInstances<IDesignPresenter>().ToList();
    }
}

public class DesignPresenterSelectedBehavior : SelectedHitTestAdornerBehavior
{
    public IDesignPresenter SelectedDesignPresenter
    {
        get { return (IDesignPresenter)GetValue(SelectedDesignPresenterProperty); }
        set { SetValue(SelectedDesignPresenterProperty, value); }
    }

    public static readonly DependencyProperty SelectedDesignPresenterProperty =
        DependencyProperty.Register("SelectedDesignPresenter", typeof(IDesignPresenter), typeof(DesignPresenterSelectedBehavior), new FrameworkPropertyMetadata(default(IDesignPresenter),FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) =>
        {
            DesignPresenterSelectedBehavior control = d as DesignPresenterSelectedBehavior;

            if (control == null) return;

            if (e.OldValue is IDesignPresenter o)
            {

            }

            if (e.NewValue is IDesignPresenter n)
            {
                if (control.SelectedElement.GetDataContext() == n)
                    return;
                control.SelectedElement = control.GetElement(n);
            }
            else
            {
                control.SelectedElement = null;
            }
        }));

    private UIElement GetElement(IDesignPresenter designPresenter)
    {
       return this.AssociatedObject.GetChild<DesignBorder>(x => x.GetDataContext() == designPresenter);
    }

    protected override void OnSelectdElementChanged()
    {
        base.OnSelectdElementChanged();
        this.SelectedDesignPresenter=this.SelectedElement?.GetDataContext() as IDesignPresenter;
    }

}
