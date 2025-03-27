// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Services.Message.Form;
public interface IFormMessageService
{
    Task<bool?> ShowEdit<T>(T value, Action<IDialog> action = null, Predicate<T> match = null, Action<IFormOption> option = null, Window owner = null);
    Task<bool?> ShowView<T>(T value, Action<IDialog> action = null, Action<IFormOption> option = null, Window owner = null);
    Task<bool?> ShowTabEdit<T>(T value, Action<IDialog> action = null, Predicate<T> match = null, Action<IFormOption> option = null, Window owner = null);
}
