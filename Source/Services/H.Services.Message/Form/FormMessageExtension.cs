// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Services.Message;

public static class FormMessageExtension
{
    public static Task<bool?> ShowEdit<T>(this IFormMessageService formMessage, T value, Predicate<T> match, Action<IDialog> action = null, Action<IFormOption> option = null, Window owner = null)
    {
        return formMessage.ShowEdit(value, action, match, option, owner);
    }

}
