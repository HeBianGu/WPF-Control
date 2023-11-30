using System;

namespace H.Providers.Ioc
{
    public interface IDialog : ILayoutable
    {
        Func<bool> CanSumit { get; set; }
        bool IsCancel { get; }
        void Sumit();
        void Close();

        string Title { get; set; }
        bool? DialogResult { get; set; }
    }
}
