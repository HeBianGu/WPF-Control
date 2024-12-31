namespace H.Services.Common
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

    public static class DialogExtension
    {
        public static string Success(this IDialog dialog)
        {
            return "Success";
        }
        public static string Error(this IDialog dialog)
        {
            return "Error";
        }

        public static string Sumit(this IDialog dialog)
        {
            return "Sumit";
        }

        public static string Cancel(this IDialog dialog)
        {
            return "Cancel";
        }

        public static string Close(this IDialog dialog)
        {
            return "Close";
        }
    }
}
