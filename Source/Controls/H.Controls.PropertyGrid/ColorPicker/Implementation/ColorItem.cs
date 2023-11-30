// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Windows.Media;

namespace H.Controls.PropertyGrid
{
    public class ColorItem
    {
        public Color? Color
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }

        public ColorItem(Color? color, string name)
        {
            Color = color;
            Name = name;
        }

        public override bool Equals(object obj)
        {
            var ci = obj as ColorItem;
            if (ci == null)
                return false;
            return (ci.Color.Equals(Color) && ci.Name.Equals(Name));
        }

        public override int GetHashCode()
        {
            return this.Color.GetHashCode() ^ this.Name.GetHashCode();
        }
    }
}
