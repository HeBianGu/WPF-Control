namespace H.Services.Revertible;

public abstract class IocRevertibleBase<T> : Ioc<T> where T : IRevertibleService
{
    public static void Commit(Action redo, Action undo, string name = null, object data = null, bool autoDo = true)
    {
        if (autoDo)
            redo?.Invoke();
        if (Instance == null)
            return;
        using (Instance.Begin(name, data))
        {
            Instance.Current.AddAction(redo, undo);
        }
    }
}
