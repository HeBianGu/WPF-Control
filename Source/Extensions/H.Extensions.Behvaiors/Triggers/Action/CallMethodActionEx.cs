// Copyright ? 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using Microsoft.Xaml.Behaviors.Core;

namespace H.Extensions.Behvaiors.Triggers.Action;

public class CallMethodActionEx : CallMethodAction
{
    protected override void Invoke(object parameter)
    {
        if (AssociatedObject == null)
            return;

        base.Invoke(parameter);
        //MethodDescriptor methodDescriptor = FindBestMethod(parameter);
        //if (methodDescriptor != null)
        //{
        //    ParameterInfo[] parameters = methodDescriptor.Parameters;
        //    if (parameters.Length == 0)
        //    {
        //        methodDescriptor.MethodInfo.Invoke(Target, null);
        //    }
        //    else if (parameters.Length == 2 && base.AssociatedObject != null && parameter != null && parameters[0].ParameterType.IsAssignableFrom(base.AssociatedObject.GetType()) && parameters[1].ParameterType.IsAssignableFrom(parameter.GetType()))
        //    {
        //        methodDescriptor.MethodInfo.Invoke(Target, new object[2] { base.AssociatedObject, parameter });
        //    }
        //}
        //else if (TargetObject != null)
        //{
        //    throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionStringTable.CallMethodActionValidMethodNotFoundExceptionMessage, MethodName, TargetObject.GetType().Name));
        //}
    }
}
