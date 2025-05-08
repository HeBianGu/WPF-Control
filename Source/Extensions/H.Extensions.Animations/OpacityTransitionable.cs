using System.Windows.Media.Animation;

namespace H.Extensions.Animations;

public class OpacityTransitionable : ElementTransitionable
{
    public OpacityTransitionable()
    {
        this.From = 0;
        this.To = 0;
    }

    public override async Task Close(UIElement visual)
    {
        DoubleAnimation amination = new DoubleAnimation(this.To, this.CloseDuration)
        {
            AccelerationRatio = this.AccelerationRatio,
            DecelerationRatio = this.DecelerationRatio,
        };
        visual.BeginAnimation(UIElement.OpacityProperty, amination);
        await Task.Delay(this.CloseDuration);
    }

    public override async Task Show(UIElement visual)
    {
        DoubleAnimation amination = new DoubleAnimation(this.From, 1, this.ShowDuration)
        {
            AccelerationRatio = 0.3,
            DecelerationRatio = 0.5,
        };
        visual.BeginAnimation(UIElement.OpacityProperty, amination);
        await Task.Delay(this.CloseDuration);
    }
}
