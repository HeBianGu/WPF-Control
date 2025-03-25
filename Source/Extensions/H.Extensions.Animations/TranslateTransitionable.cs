using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace H.Extensions.Animations;

public class TranslateTransitionable : ElementTransitionableBase<Point>
{
    public TranslateTransitionable()
    {
        this.From = new Point(200, 0);
        this.To = new Point(200, 0);
    }
    public override async Task Close(UIElement visual)
    {
        var aminationX = new DoubleAnimation(0, this.To.X, this.CloseDuration);
        var aminationY = new DoubleAnimation(0, this.To.Y, this.CloseDuration);
        using var renderTransformDisposable = new RenderTransformDisposable(visual);
        var transform = new TranslateTransform();
        visual.RenderTransform = transform;
        transform.BeginAnimation(TranslateTransform.XProperty, aminationX);
        transform.BeginAnimation(TranslateTransform.YProperty, aminationY);
        await Task.Delay(this.CloseDuration);
    }

    public override async Task Show(UIElement visual)
    {
        var amination = new DoubleAnimation(0, this.ShowDuration);
        using var renderTransformDisposable = new RenderTransformDisposable(visual);
        var transform = new TranslateTransform(this.From.X, this.From.Y);
        visual.RenderTransform = transform;
        transform.BeginAnimation(TranslateTransform.XProperty, amination);
        transform.BeginAnimation(TranslateTransform.YProperty, amination);
        await Task.Delay(this.ShowDuration);
    }
}
