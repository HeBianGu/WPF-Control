global using H.Services.Common;
global using System;
global using System.ComponentModel.DataAnnotations;
global using System.Reflection;
global using System.Windows.Input;
global using System.Windows.Markup;

namespace H.Windows.Dialog
{
    public abstract class DialogCommandBase : MarkupExtension, ICommand
    {
        public int Width { get; set; } = 500;
        public int Height { get; set; } = 300;

        public event EventHandler? CanExecuteChanged
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
        public virtual bool CanExecute(object? parameter)
        {
            return parameter is Type;
        }

        public abstract void Execute(object? parameter);

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
