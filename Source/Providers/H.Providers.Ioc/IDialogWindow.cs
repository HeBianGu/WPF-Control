using System;

namespace H.Providers.Ioc
{
    public interface IDialogWindow
    {
        Func<bool> CanSumit { get; set; }
        bool IsCancel { get; }
        void Sumit();
    }
}
