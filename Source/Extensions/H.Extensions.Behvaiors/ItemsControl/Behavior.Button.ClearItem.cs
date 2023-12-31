﻿// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Extensions.Behvaiors
{
    public class ButtonClearItemBehavior : ButtonBehaviorBase
    {
        protected override void OnClick()
        {
            if (ItemsSource == null)
                return;
            ItemsSource.Clear();
        }
    }

}