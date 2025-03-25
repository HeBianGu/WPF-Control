global using H.Common.Transitionable;
global using H.Mvvm.ViewModels.Base;
global using System.Windows;

namespace H.Extensions.Animations;

public abstract class TransitionableBase<V> : BindableBase, ITransitionable
{
    //public TimeSpan ShowDuration { get; set; } = TimeSpan.FromMilliseconds(2000);
    //public TimeSpan CloseDuration { get; set; } = TimeSpan.FromMilliseconds(1000);

    public TimeSpan ShowDuration { get; set; } = TimeSpan.FromMilliseconds(200);
    public TimeSpan CloseDuration { get; set; } = TimeSpan.FromMilliseconds(100);
    public V From { get; set; }
    public V To { get; set; }
    public abstract Task Close(DependencyObject visual);
    public abstract Task Show(DependencyObject visual);
}

public abstract class TransitionableBase<T, V> : TransitionableBase<V> where T : DependencyObject
{
    public override async Task Show(DependencyObject visual)
    {
        if (visual is T t)
        {
            await Show(t);
        }
    }

    public override async Task Close(DependencyObject visual)
    {
        if (visual is T t)
        {
            await Close(t);
        }
    }

    public abstract Task Show(T visual);

    public abstract Task Close(T visual);
}

public abstract class ElementTransitionableBase<T> : TransitionableBase<UIElement, T>
{

}

public abstract class ElementTransitionable : ElementTransitionableBase<double>
{

}
