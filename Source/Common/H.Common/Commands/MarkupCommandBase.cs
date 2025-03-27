using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Markup;

namespace H.Common.Commands;

[MarkupExtensionReturnType(typeof(ICommand))]
public abstract class MarkupCommandBase : MarkupExtension, ICommand, INotifyPropertyChanged
{
    #region - ICommand -
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

    protected UIElement GetTargetElement(object parameter)
    {
        if (parameter is UIElement element)
            return element;
        return null;
        try
        {
            return this.Target.TargetObject is UIElement element1 ? element1 : null;
        }
        catch (Exception)
        {
            return null;
        }
    }
    #endregion

    #region - MarkupExtension -

    //IProvideValueTarget：获取目标对象和属性。
    //IXamlTypeResolver：解析 XAML 类型名称。
    //IUriContext：获取基 URI。
    //IRootObjectProvider：获取根对象。
    //IXamlSchemaContextProvider：获取架构上下文。
    //IAmbientProvider：获取环境属性。
    //IXamlNameResolver：解析命名对象。
    //IXamlNamespaceResolver：解析命名空间。
    //IXamlLineInfo：获取行号和列号信息。
    private IServiceProvider _serviceProvider;
    protected IProvideValueTarget Target => _serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        return this;
    }

    #endregion

    #region - INotifyPropertyChanged -
    public event PropertyChangedEventHandler PropertyChanged;

    public virtual void RaisePropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

    }
    #endregion

}
