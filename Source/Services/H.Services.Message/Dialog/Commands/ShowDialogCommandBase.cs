// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control
global using H.Common.Commands;

namespace H.Services.Message.Dialog.Commands;

public abstract class ShowDialogCommandBase : DisplayMarkupCommandBase
{
    public double Width { get; set; } = double.NaN;
    public double Height { get; set; } = double.NaN;
    public DialogButton DialogButton { get; set; }
    public ITransitionable Transitionable { get; set; }
    public HorizontalAlignment HorizontalContentAlignment { get; set; } = HorizontalAlignment.Stretch;
    public DataTemplate PresenterTemplate { get; set; }
    protected virtual void Invoke(IDialog w, object parameter = null)
    {
        w.Width = this.Width;
        w.Height = this.Height;
        w.Transitionable = this.Transitionable;
        w.Title = this.Name ?? w.Title;
        w.HorizontalContentAlignment = this.HorizontalContentAlignment;
        w.Icon = this.Icon ?? w.Icon;
        var target = this.GetTargetElement(parameter);
        if (target != null)
        {
            DataTemplate template = ShowDialogCommandBase.GetPresenterTemplate(target);
            if (template != null)
                w.PresenterTemplate = template;
        }
         
        if (this.PresenterTemplate != null)
            w.PresenterTemplate = this.PresenterTemplate;
    }

    public static DataTemplate GetPresenterTemplate(DependencyObject obj)
    {
        return (DataTemplate)obj.GetValue(PresenterTemplateProperty);
    }

    public static void SetPresenterTemplate(DependencyObject obj, DataTemplate value)
    {
        obj.SetValue(PresenterTemplateProperty, value);
    }

    public static readonly DependencyProperty PresenterTemplateProperty =
        DependencyProperty.RegisterAttached("PresenterTemplate", typeof(DataTemplate), typeof(ShowDialogCommandBase), new PropertyMetadata(default(DataTemplate)));

}
