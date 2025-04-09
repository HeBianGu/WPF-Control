using H.Controls.Adorner.Adorner.Base;
using System.Windows;
using System.Windows.Controls;

namespace H.Controls.Adorner.Adorner;

public class PresenterAdorner : VisualCollectionAdornerBase
{
    private ContentPresenter _contentPresenter = new ContentPresenter();
    public PresenterAdorner(UIElement adornedElement, object presenter) : base(adornedElement)
    {
        _contentPresenter.Content = presenter;
        _visualCollection.Add(_contentPresenter);
    }

    public ContentPresenter ContentPresenter => this._contentPresenter;
    public object Presenter => this._contentPresenter.Content;
    protected override Size MeasureOverride(Size constraint)
    {
        this._contentPresenter.Measure(this.AdornedElement.RenderSize);
        return new Size(Math.Max(this._contentPresenter.DesiredSize.Width, this.AdornedElement.DesiredSize.Width), Math.Max(this._contentPresenter.DesiredSize.Height, this.AdornedElement.DesiredSize.Height));
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
        this._contentPresenter.Arrange(new Rect(this.AdornedElement.RenderSize));
        return base.ArrangeOverride(finalSize);
    }
}
