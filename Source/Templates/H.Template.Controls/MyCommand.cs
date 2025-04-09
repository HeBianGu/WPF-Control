using H.Common.Attributes;
using H.Common.Commands;
namespace H.Template.Controls;
[Icon("\xE713")]
[Display(Name = "我的操作", Description = "默认操作")]
public class MyCommand : DisplayMarkupCommandBase
{
    public override Task ExecuteAsync(object parameter)
    {
        return base.ExecuteAsync(parameter);
    }
}