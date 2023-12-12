using System.Windows;
using System.Windows.Controls;

namespace H.Controls.PrintBox
{
    [TemplatePart(Name = "PART_Host")]
    public class PrintPage : ContentControl
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(PrintPage), "S.PrintPage.Default");

        static PrintPage()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PrintPage), new FrameworkPropertyMetadata(typeof(PrintPage)));
        }

        Border _border = null;
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _border = this.Template.FindName("PART_Host", this) as Border;
        }

        public UIElement GetElement()
        {
            return this._border.Child;
        }
    }
}
