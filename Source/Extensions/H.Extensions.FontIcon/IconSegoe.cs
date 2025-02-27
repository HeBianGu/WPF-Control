using H.Mvvm;
using System.Windows;

namespace H.Extensions.FontIcon
{
    public class IconSegoe : BindableBase
    {
        public int CodePoint { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string CodeKey { get; set; }
        public RelayCommand CopyCommand => new RelayCommand((s, e) =>
        {
            Clipboard.SetText(this.Key);
        });

        public RelayCommand CopyCodeKeyCommand => new RelayCommand((s, e) =>
        {
            Clipboard.SetText(this.CodeKey);
        });
    }
}
