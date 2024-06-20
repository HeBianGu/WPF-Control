// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

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
            this.Color = color;
            this.Name = name;
        }

        public override bool Equals(object obj)
        {
            ColorItem ci = obj as ColorItem;
            if (ci == null)
                return false;
            return ci.Color.Equals(this.Color) && ci.Name.Equals(this.Name);
        }

        public override int GetHashCode()
        {
            return this.Color.GetHashCode() ^ this.Name.GetHashCode();
        }
    }
}
