// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control
namespace H.Template.Controls;
[TemplatePart(Name = PART_Content)]
public partial class MyControl : ContentControl
{
    const string PART_Content = "PART_Content";
    static MyControl()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(MyControl), new FrameworkPropertyMetadata(typeof(MyControl)));
    }

    protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
    }

    private Control _control = null;
    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        this._control = this.GetTemplateChild(PART_Content) as Control;
    }
}

public static class MyControlKeys
{
    public static ComponentResourceKey Default => new ComponentResourceKey(typeof(MyControlKeys), "S.MyControl.Default");
}

