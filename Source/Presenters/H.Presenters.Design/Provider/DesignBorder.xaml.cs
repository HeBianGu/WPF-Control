using System.Windows;
using System.Windows.Controls;

namespace H.Presenters.Design
{
    public class DesignBorder : Border
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(DesignBorder), "S.DesignBorder.Default");

        static DesignBorder()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DesignBorder), new FrameworkPropertyMetadata(typeof(DesignBorder)));
        }
    }
}
