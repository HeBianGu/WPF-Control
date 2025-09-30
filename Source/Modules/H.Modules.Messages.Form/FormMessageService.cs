// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Common.Interfaces;
global using H.Controls.Form;
global using H.Extensions.Common;
global using H.Services.Message;
global using H.Services.Message.Dialog;
global using H.Services.Message.Form;
global using System.ComponentModel;
global using System.ComponentModel.DataAnnotations;
global using System.Windows;

namespace H.Modules.Messages.Form
{
    public class FormMessageService : IFormMessageService
    {
        public async Task<bool?> ShowEdit<T>(T value, Action<IDialog> action = null, Predicate<T> match = null, Action<IFormOption> option = null, Window owner = null)
        {
            return await this.ShowEdit<T, StaticFormPresenter>(value, action, match, option, owner);
        }

        public async Task<bool?> ShowTabEdit<T>(T value, Action<IDialog> action = null, Predicate<T> match = null, Action<IFormOption> option = null, Window owner = null)
        {

            Action<IFormOption> toption = x =>
            {
                option?.Invoke(x);
                if (x is TabFormPresenter tab)
                {
                    tab.UpdateTabNames();
                }
            };
            return await this.ShowEdit<T, TabFormPresenter>(value, action, match, toption, owner);
        }

        public async Task<bool?> ShowView<T>(T value, Action<IDialog> action = null, Action<IFormOption> option = null, Window owner = null)
        {
            Action<IFormOption> optionView = x =>
            {
                option?.Invoke(x);
                x.UsePropertyView = true;
            };
            return await this.Show<T, StaticFormPresenter>(value, null, action, optionView, owner);
        }

        private async Task<bool?> ShowEdit<T, P>(T value, Action<IDialog> action = null, Predicate<T> match = null, Action<P> option = null, Window owner = null) where P : class, IFormOption, new()
        {
            Func<Task<bool>> canSumit = async () =>
            {
                //如果传入验证方式则不用ModelState
                if (match != null)
                    return match.Invoke(value);

                if (value.ModelState(out List<string> errors) == false)
                {
                    await IocMessage.Dialog.Show(errors.FirstOrDefault());
                    return false;
                }
                return true;
            };

            return await this.Show(value, canSumit, action, option, owner);
        }

        private async Task<bool?> Show<T, P>(T value, Func<Task<bool>> canSumit = null, Action<IDialog> action = null, Action<P> option = null, Window owner = null) where P : class, IFormOption, new()
        {
            var presenter = Activator.CreateInstance<P>();
            presenter.SelectObject = value;
            Action<IDialog> dialogAction = x =>
            {
                if (value is ITitleable titleable && !string.IsNullOrEmpty(titleable.Title))
                    x.Title = titleable.Title;
                else if (value is INameable nameable && !string.IsNullOrEmpty(nameable.Name))
                    x.Title = nameable.Name;
                else if (value is ITextable textable && !string.IsNullOrEmpty(textable.Text))
                    x.Title = textable.Text;
                if (value is IIconable iconable && !string.IsNullOrEmpty(iconable.Icon))
                    x.Icon = iconable.Icon;
                x.DialogButton = DialogButton.Sumit;
                x.HorizontalContentAlignment = HorizontalAlignment.Stretch;
                x.MinWidth = 400;
                action?.Invoke(x);
            };

            //presenter.UseCommand = false;
            option?.Invoke(presenter);
            return await IocMessage.Dialog.Show(presenter, dialogAction, canSumit);
        }

    }
}
