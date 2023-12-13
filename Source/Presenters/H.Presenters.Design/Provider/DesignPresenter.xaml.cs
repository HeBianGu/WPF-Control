using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace H.Presenters.Design
{

    public class TextBlockKeys
    {
        public static ComponentResourceKey Binding => new ComponentResourceKey(typeof(TextBlockKeys), "S.TextBlock.Binding");
    }

    public class BorderKeys
    {
        public static ComponentResourceKey Binding => new ComponentResourceKey(typeof(BorderKeys), "S.Border.Binding");
    }
}
