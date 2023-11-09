﻿// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

namespace H.Windows.Ribbon
{
    public class MenuItemData : SplitButtonData
    {
        public MenuItemData()
            : this(false)
        {
        }

        public MenuItemData(bool isApplicationMenu)
            : base(isApplicationMenu)
        {
        }
    }
}
