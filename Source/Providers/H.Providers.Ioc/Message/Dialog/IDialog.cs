namespace H.Providers.Ioc
{
    public interface IDialog : ILayoutable, ICancelable
    {
        Func<bool> CanSumit { get; set; }
        void Sumit();
        void Close();
        string Title { get; set; }
        bool? DialogResult { get; set; }
        DialogButton DialogButton { get; set; }
        Window Owner { get; set; }
    }
}
