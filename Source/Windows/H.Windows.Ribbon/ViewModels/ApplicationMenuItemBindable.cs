// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Windows.Ribbon
{
    public class ApplicationMenuItemBindable : MenuItemBindable
    {
        public ApplicationMenuItemBindable()
            : this(false)
        {
        }

        public ApplicationMenuItemBindable(bool isApplicationMenu)
            : base(isApplicationMenu)
        {
        }
    }
}
