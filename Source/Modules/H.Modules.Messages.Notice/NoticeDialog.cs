// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

global using H.Services.Message.Notice;

namespace H.Modules.Messages.Notice
{
    public static class NoticeDialog
    {
        private static readonly NoticeMessageService _noticeMessageService = new NoticeMessageService();
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
        public static async Task<T> ShowProgress<T>(Func<IPercentNoticeItem, T> action)
        {
           return await _noticeMessageService.ShowProgress(action);
        }
        public static async Task<T> ShowString<T>(Func<INoticeItem, T> action)
        {
          return await  _noticeMessageService.ShowString(action);
        }
        public static void Show(INoticeItem message)
        {
           _noticeMessageService.Show(message);
        }
    }
}
