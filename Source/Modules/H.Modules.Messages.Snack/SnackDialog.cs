// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Services.Common;
using System;
using System.Threading.Tasks;

namespace H.Modules.Messages.Snack
{
    public static class SnackDialog
    {
        private static readonly SnackMessageService _noticeMessageService = new SnackMessageService();
        public static void ShowInfo(string message)
        {
            _noticeMessageService.ShowWarn(message);
        }
        public static void ShowError(string message)
        {
            _noticeMessageService.ShowError(message);
        }
        public static void ShowWarn(string message)
        {
            _noticeMessageService.ShowWarn(message);
        }
        public static void ShowSuccess(string message)
        {
            _noticeMessageService.ShowSuccess(message);
        }
        public static void ShowFatal(string message)
        {
            _noticeMessageService.ShowFatal(message);
        }

        public static async Task<bool?> ShowDialog(string message)
        {
            return await _noticeMessageService.ShowDialog(message);
        }
        public static async Task<T> ShowProgress<T>(Func<IPercentSnackItem, T> action)
        {
            return await _noticeMessageService.ShowProgress(action);
        }
        public static async Task<T> ShowString<T>(Func<ISnackItem, T> action)
        {
            return await _noticeMessageService.ShowString(action);
        }
        public static void Show(ISnackItem message)
        {
            _noticeMessageService.Show(message);
        }
    }
}
