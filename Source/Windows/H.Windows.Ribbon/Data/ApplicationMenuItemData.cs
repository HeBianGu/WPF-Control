﻿// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Windows.Ribbon
{
    public class ApplicationMenuItemData : MenuItemData
    {
        public ApplicationMenuItemData()
            : this(false)
        {
        }

        public ApplicationMenuItemData(bool isApplicationMenu)
            : base(isApplicationMenu)
        {
        }
    }
}
