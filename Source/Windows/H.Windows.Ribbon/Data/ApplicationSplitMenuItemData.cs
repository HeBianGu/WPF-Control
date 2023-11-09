﻿// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

namespace H.Windows.Ribbon
{
    public class ApplicationSplitMenuItemData : SplitMenuItemData
    {
        public ApplicationSplitMenuItemData()
            : this(false)
        {
        }

        public ApplicationSplitMenuItemData(bool isApplicationMenu)
            : base(isApplicationMenu)
        {
        }
    }
}
