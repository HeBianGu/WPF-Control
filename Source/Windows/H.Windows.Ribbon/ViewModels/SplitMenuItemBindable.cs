// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Windows.Ribbon
{
    public class SplitMenuItemBindable : MenuItemBindable
    {
        public SplitMenuItemBindable()
            : this(false)
        {
        }

        public SplitMenuItemBindable(bool isApplicationMenu)
            : base(isApplicationMenu)
        {
        }
    }
}
