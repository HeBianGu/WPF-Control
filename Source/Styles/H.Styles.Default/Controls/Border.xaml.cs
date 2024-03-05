// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace H.Styles.Default
{
    public class BorderKeys
    {
        public static ComponentResourceKey Default => new ComponentResourceKey(typeof(BorderKeys), "S.Border.Default");
        public static ComponentResourceKey Effect => new ComponentResourceKey(typeof(BorderKeys), "S.Border.Effect");
        public static ComponentResourceKey BorderThickness => new ComponentResourceKey(typeof(BorderKeys), "S.Border.BorderThickness");
        public static ComponentResourceKey BorderBrush => new ComponentResourceKey(typeof(BorderKeys), "S.Border.BorderBrush");
        public static ComponentResourceKey Background => new ComponentResourceKey(typeof(BorderKeys), "S.Border.Background");
    }
}
