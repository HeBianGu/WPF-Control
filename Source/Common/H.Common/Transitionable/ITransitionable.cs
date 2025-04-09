namespace H.Common.Transitionable;

public interface ITransitionable
{
    Task Show(DependencyObject visual);

    Task Close(DependencyObject visual);
}
