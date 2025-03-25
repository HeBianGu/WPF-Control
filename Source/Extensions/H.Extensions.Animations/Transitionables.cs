namespace H.Extensions.Animations;

public static class Transitionables
{
    public static ITransitionable Opacity = new OpacityTransitionable();
    public static ITransitionable Scale = new ScaleTransitionable();
    public static ITransitionable Rotate = new RotateTransitionable();
    public static ITransitionable XTranslate = new TranslateTransitionable();
    public static ITransitionable YTranslate = new TranslateTransitionable()
    {
        From = new System.Windows.Point(0, -200),
        To = new System.Windows.Point(0, 200)
    };

    public static ITransitionable OpacityScale = new GroupTransitionable(Scale, Opacity)
    {

    };
}
