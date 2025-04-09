using H.Mvvm.Commands;
using H.Mvvm.ViewModels.Base;
using System.Windows;

namespace H.Extensions.FontIcon;

public class IconSegoe : BindableBase
{
    public int CodePoint { get; set; }
    public string Key { get; set; }
    public string Value { get; set; }
    public string CodeKey { get; set; }
    public RelayCommand CopyCommand => new RelayCommand(x =>
    {
        Clipboard.SetText(this.Key);
    });

    public RelayCommand CopyCodeKeyCommand => new RelayCommand(x =>
    {
        Clipboard.SetText(this.CodeKey);
    });
}
