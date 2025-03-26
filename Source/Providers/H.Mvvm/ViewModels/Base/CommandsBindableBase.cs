global using System.Collections.ObjectModel;
global using System.Windows.Input;

namespace H.Mvvm.ViewModels.Base;

/// <summary>
/// 提供了创建命令的基类，用于绑定到视图模型。
/// </summary>
public abstract class CommandsBindableBase : Bindable
{
    /// <summary>
    /// 初始化 <see cref="CommandsBindableBase"/> 类的新实例。
    /// </summary>
    public CommandsBindableBase()
    {
        this.Commands = new ObservableCollection<ICommand>(this.CreateCommands().OrderBy(x => x.Order).OfType<ICommand>());
    }

    /// <summary>
    /// 获取命令的集合。
    /// </summary>
    [Browsable(false)]
    [System.Text.Json.Serialization.JsonIgnore]
    [System.Xml.Serialization.XmlIgnore]
    public ObservableCollection<ICommand> Commands { get; } = new ObservableCollection<ICommand>();

    /// <summary>
    /// 创建命令的方法，派生类可以重写此方法以创建自定义命令。
    /// </summary>
    /// <returns>命令的集合。</returns>
    protected virtual IEnumerable<IDisplayCommand> CreateCommands()
    {
        Type type = this.GetType();
        IEnumerable<PropertyInfo> cmdps = type.GetProperties().Where(x => typeof(IDisplayCommand).IsAssignableFrom(x.PropertyType));
        foreach (PropertyInfo cmdp in cmdps)
        {
            if (cmdp.CanRead == false)
                continue;
            if (cmdp.GetCustomAttribute<BrowsableAttribute>()?.Browsable == false)
                continue;
            IDisplayCommand command = cmdp.GetValue(this) as IDisplayCommand;
            if (command is IDisplayCommand displayCommand)
            {
                DisplayAttribute attr = cmdp.GetCustomAttribute<DisplayAttribute>();
                if (attr != null)
                {
                    displayCommand.Name = displayCommand.Name ?? attr.Name;
                    displayCommand.Description = displayCommand.Description ?? attr.Description;
                    if (displayCommand.Order <= 0)
                        displayCommand.Order = attr.GetOrder() ?? 0;
                    displayCommand.GroupName = displayCommand.GroupName ?? attr.GroupName;
                }
                IconAttribute icon = cmdp.GetCustomAttribute<IconAttribute>();
                if (icon != null)
                    displayCommand.Icon = displayCommand.Icon ?? icon.Icon;
            }
            yield return command;
        }
    }
}
