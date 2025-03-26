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
        DoubleAnimation aminationX = new DoubleAnimation(0, this.To.X, this.CloseDuration);
        DoubleAnimation aminationY = new DoubleAnimation(0, this.To.Y, this.CloseDuration);
        using RenderTransformDisposable renderTransformDisposable = new RenderTransformDisposable(visual);
        TranslateTransform transform = new TranslateTransform();
        visual.RenderTransform = transform;
        transform.BeginAnimation(TranslateTransform.XProperty, aminationX);
        transform.BeginAnimation(TranslateTransform.YProperty, aminationY);
        await Task.Delay(this.CloseDuration);
    }

    public override async Task Show(UIElement visual)
    {
        DoubleAnimation amination = new DoubleAnimation(0, this.ShowDuration);
        using RenderTransformDisposable renderTransformDisposable = new RenderTransformDisposable(visual);
        TranslateTransform transform = new TranslateTransform(this.From.X, this.From.Y);
        visual.RenderTransform = transform;
        transform.BeginAnimation(TranslateTransform.XProperty, amination);
        transform.BeginAnimation(TranslateTransform.YProperty, amination);
        await Task.Delay(this.ShowDuration);
    }
}
