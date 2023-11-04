using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace H.Styles.Default
{
    public class ButtonKeys
    {
        public static ComponentResourceKey Default => new ComponentResourceKey(typeof(ButtonKeys), "S.Button.Default");
        public static ComponentResourceKey Geometry => new ComponentResourceKey(typeof(ButtonKeys), "S.Button.Geometry");
        public static ComponentResourceKey ToolBar => new ComponentResourceKey(typeof(ButtonKeys), "S.Button.ToolBar");
    }
}
