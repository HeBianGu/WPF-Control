using System.Windows.Media;
using System.Windows.Media.Animation;

namespace H.Extensions.Animations;

public class ScaleTransitionable : ElementTransitionable
{
    public ScaleTransitionable()
    {
        this.From = 0.5;
        this.To = 0.8;
    }
    public override async Task Close(UIElement visual)
    {
        DoubleAnimation amination = new DoubleAnimation(this.To, this.CloseDuration);
        await Task.Delay(this.CloseDuration);
        using RenderTransformDisposable renderTransformDisposable = new RenderTransformDisposable(visual);
        ScaleTransform scaleTransform = new ScaleTransform(1, 1);
        visual.RenderTransform = scaleTransform;
        visual.RenderTransformOrigin = new Point(0.5, 0.5);
        scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, amination);
        scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, amination);
        await Task.Delay(this.CloseDuration);
    }

    public override async Task Show(UIElement visual)
    {
        DoubleAnimation amination = new DoubleAnimation(0, 1, this.ShowDuration);
        using RenderTransformDisposable renderTransformDisposable = new RenderTransformDisposable(visual);
        ScaleTransform scaleTransform = new ScaleTransform(this.From, this.From);
        visual.RenderTransform = scaleTransform;
        visual.RenderTransformOrigin = new Point(0.5, 0.5);
        scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, amination);
        scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, amination);
        await Task.Delay(this.ShowDuration);
    }
}
