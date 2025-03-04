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


    public class ShowIocDialogCommand : DialogCommandBase
    {
        public Type Type { get; set; }

        public override void Execute(object parameter)
        {
            var value = Ioc.Services.GetService(this.Type);
            if (value == null)
                throw new ArgumentNullException(this.Type.FullName);
            var title = value is ITitleable titleable ? titleable.Title : value.GetType().GetCustomAttribute<DisplayAttribute>()?.Name;
            DialogWindow.ShowPresenter(value, x =>
            {
                x.Width = this.Width;
                x.Height = this.Height;
                x.Title = title;
            });
        }

        public override bool CanExecute(object? parameter)
        {
            return this.Type != null;
        }
    }
}
