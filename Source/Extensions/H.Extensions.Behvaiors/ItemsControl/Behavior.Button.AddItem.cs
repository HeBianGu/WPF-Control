// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Extensions.Behvaiors
{
    public class ButtonAddItemBehavior : AddItemButtonBehaviorBase
    {
        protected override void OnClick()
        {
            object addItem = CreateNewItem();
            if (addItem == null)
                return;
            ItemsSource.Add(addItem);
        }
    }

}