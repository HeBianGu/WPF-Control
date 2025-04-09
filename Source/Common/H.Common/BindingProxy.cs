// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Common;

public class BindingProxy : Freezable
{
    protected override Freezable CreateInstanceCore()
    {
        return new BindingProxy();
    }

    public object Data
    {
        get { return GetValue(DataProperty); }
        set { SetValue(DataProperty, value); }
    }

    public static readonly DependencyProperty DataProperty =
        DependencyProperty.Register("Data", typeof(object), typeof(BindingProxy), new UIPropertyMetadata(null));
}
