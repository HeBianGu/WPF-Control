// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control
using System.Reflection;

namespace H.Controls.Form.PropertyItem.TextPropertyItems
{
    public class HyperlinkPropertyItem : TextPropertyViewItem
    {
        public HyperlinkPropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {

        }


        public RelayCommand ProcessCommand => new RelayCommand(x =>
        {
            Process.Start(new ProcessStartInfo(this.Value) { UseShellExecute = true });
        });
    }
}
