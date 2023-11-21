using System;

namespace H.Providers.Ioc
{
    public interface IRevertible
    {
        string Name { get; }
        void AddAction(Action redo, Action undo);
        void Undo();
        void Redo();
    }
}