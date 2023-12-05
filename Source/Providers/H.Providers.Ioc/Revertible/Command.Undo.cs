using System;

namespace H.Providers.Ioc
{
    public class UndoCommand : IocMarkupCommandBase
    {
        public override void Execute(object parameter)
        {
            Ioc<IRevertibleService>.Instance?.Undo();
        }
        public override bool CanExecute(object parameter)
        {
            return Ioc<IRevertibleService>.Instance?.CanUndo == true;
        }
    }
}
