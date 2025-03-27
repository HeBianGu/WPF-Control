// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Windows.Markup;

namespace H.Extensions.MarkupExtension;

[MarkupExtensionReturnType(typeof(TimeSpan))]
public class TimeSpanParseExtension : System.Windows.Markup.MarkupExtension
{
    public string Input { get; set; }
    public string Format { get; set; }
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return TimeSpan.ParseExact(this.Input, this.Format, System.Globalization.CultureInfo.CurrentCulture);
    }
}

[MarkupExtensionReturnType(typeof(TimeSpan))]
public class TimeSpanFromMethodExtension : System.Windows.Markup.MarkupExtension
{
    public string Param { get; set; }
    public string MethodName { get; set; }
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        System.Reflection.MethodInfo method = typeof(TimeSpan).GetMethod(this.MethodName);
        System.Reflection.ParameterInfo param = method.GetParameters().FirstOrDefault();
        object value = Convert.ChangeType(this.Param, param.ParameterType);
        return method.Invoke(null, new object[] { param });
    }
}
