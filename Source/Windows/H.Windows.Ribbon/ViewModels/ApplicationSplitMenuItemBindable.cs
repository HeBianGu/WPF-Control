// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Windows.Ribbon
{
    public class ApplicationSplitMenuItemBindable : SplitMenuItemBindable
    {
        public ApplicationSplitMenuItemBindable()
            : this(false)
        {
        }

        public ApplicationSplitMenuItemBindable(bool isApplicationMenu)
            : base(isApplicationMenu)
        {
        }
    }
}
