// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Windows.Ribbon
{
    public class MenuItemBindable : SplitButtonBindable
    {
        public MenuItemBindable()
            : this(false)
        {
        }

        public MenuItemBindable(bool isApplicationMenu)
            : base(isApplicationMenu)
        {
        }
    }
}
