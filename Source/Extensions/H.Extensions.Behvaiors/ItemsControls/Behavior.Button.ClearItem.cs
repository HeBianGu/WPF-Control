// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Extensions.Behvaiors.ItemsControls;

public class ButtonClearItemBehavior : ButtonBehaviorBase
{
    protected override void OnClick()
    {
        if (this.ItemsSource == null)
            return;
        this.ItemsSource.Clear();
    }
}