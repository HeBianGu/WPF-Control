using H.Mvvm.Attributes;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Windows.Input;

namespace H.Mvvm.ViewModels.Base
{
    public abstract class CommandsBindableBase : Bindable
    {
        public CommandsBindableBase()
        {
            this.Commands = new ObservableCollection<ICommand>(this.CreateCommands().OrderBy(x => x.Order).OfType<ICommand>());
        }
        [Browsable(false)]
        [System.Text.Json.Serialization.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        public ObservableCollection<ICommand> Commands { get; } = new ObservableCollection<ICommand>();

        protected virtual IEnumerable<IDisplayCommand> CreateCommands()
        {
            var type = this.GetType();
            var cmdps = type.GetProperties().Where(x => typeof(IDisplayCommand).IsAssignableFrom(x.PropertyType));
            foreach (PropertyInfo cmdp in cmdps)
            {
                if (cmdp.CanRead == false)
                    continue;
                if (cmdp.GetCustomAttribute<BrowsableAttribute>()?.Browsable == false)
                    continue;
                IDisplayCommand command = cmdp.GetValue(this) as IDisplayCommand;
                if (command is IDisplayCommand displayCommand)
                {
                    var attr = cmdp.GetCustomAttribute<DisplayAttribute>();
                    if (attr != null)
                    {
                        displayCommand.Name = displayCommand.Name ?? attr.Name;
                        displayCommand.Description = displayCommand.Description ?? attr.Description;
                        if (displayCommand.Order <= 0)
                            displayCommand.Order = attr.GetOrder() ?? 0;
                        displayCommand.GroupName = displayCommand.GroupName ?? attr.GroupName;
                    }
                    var icon = cmdp.GetCustomAttribute<IconAttribute>();
                    if (icon != null)
                        displayCommand.Icon = displayCommand.Icon ?? icon.Icon;
                }
                yield return command;
            }
        }
    }

}
