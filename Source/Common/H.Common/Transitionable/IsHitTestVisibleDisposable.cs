namespace H.Common.Transitionable;

public class IsHitTestVisibleDisposable : IDisposable
{
    private readonly bool _isHitTestVisible;
    private readonly UIElement _element;
    public IsHitTestVisibleDisposable(UIElement element)
    {
        _element = element;
        _isHitTestVisible = _element.IsHitTestVisible;
        _element.IsHitTestVisible = false;
    }
    public void Dispose()
    {
        _element.IsHitTestVisible = _isHitTestVisible;
    }
}
