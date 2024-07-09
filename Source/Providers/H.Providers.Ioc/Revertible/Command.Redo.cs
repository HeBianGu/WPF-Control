namespace H.Services.Common
{
    public class RedoCommand : IocMarkupCommandBase
    {
        public override void Execute(object parameter)
        {
            Ioc<IRevertibleService>.Instance?.Redo();
        }

        public override bool CanExecute(object parameter)
        {
            return Ioc<IRevertibleService>.Instance?.CanRedo == true;
        }
    }
}
