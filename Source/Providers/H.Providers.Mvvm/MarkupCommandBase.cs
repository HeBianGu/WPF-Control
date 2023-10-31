using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace H.Providers.Mvvm
{
    public abstract class MarkupCommandBase : MarkupExtension, ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }
        public virtual bool CanExecute(object parameter)
        {
            return true;
        }

        public abstract void Execute(object parameter);

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }

}
