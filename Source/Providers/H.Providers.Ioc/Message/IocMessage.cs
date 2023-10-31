// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using H.Providers.Ioc;

namespace H.Providers.Ioc
{
    public static class IocMessage
    {
        public static IDialogMessage Dialog => System.Ioc.GetService<IDialogMessage>();
        public static ISnackMessage Snack => System.Ioc.GetService<ISnackMessage>();
        public static ITaskBarMessage TaskBar => System.Ioc.GetService<ITaskBarMessage>();
        public static ISystemNotifyMessage SystemNotify => System.Ioc.GetService<ISystemNotifyMessage>();
        public static INotifyMessage Notify => System.Ioc.GetService<INotifyMessage>();
        public static IFormMessage Form => System.Ioc.GetService<IFormMessage>();
    }
}
