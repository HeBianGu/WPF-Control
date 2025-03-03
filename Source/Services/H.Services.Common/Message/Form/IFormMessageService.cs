// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Services.Common
{
    //public interface ITabFormOption : IFormOption
    //{
    //    ObservableCollection<string> TabNames { get; set; }
    //}

    public interface IFormMessageService
    {
        Task<bool?> ShowEdit<T>(T value, Action<IDialog> action = null, Predicate<T> match = null, Action<IFormOption> option = null, Window owner = null);
        Task<bool?> ShowView<T>(T value, Action<IDialog> action = null, Action<IFormOption> option = null, Window owner = null);
        Task<bool?> ShowTabEdit<T>(T value, Action<IDialog> action = null, Predicate<T> match = null, Action<IFormOption> option = null, Window owner = null);
    }

    public static class FormMessageExtension
    {
        public static Task<bool?> ShowEdit<T>(this IFormMessageService formMessage, T value, Predicate<T> match, Action<IDialog> action = null, Action<IFormOption> option = null, Window owner = null)
        {
            return formMessage.ShowEdit(value, action, match, option, owner);
        }

    }
}
