namespace H.Extensions.Animations;

public class GroupTransitionable : ITransitionable
{
    public GroupTransitionable(params ITransitionable[] transitionables)
    {
        this._transitionables = transitionables;
    }
    private readonly ITransitionable[] _transitionables;

    public async Task Close(DependencyObject visual)
    {
        foreach (ITransitionable transitionable in this._transitionables)
        {
            await transitionable.Close(visual);
        }
    }

    public async Task Show(DependencyObject visual)
    {
        foreach (ITransitionable transitionable in this._transitionables)
        {
            await transitionable.Show(visual);
        }
    }
}
