using System.Windows;

namespace H.Windows.Dialog
{
    public class DialogKeys
    {
        public static ComponentResourceKey None => new ComponentResourceKey(typeof(DialogKeys), "S.DialogWindow.None");
        public static ComponentResourceKey Sumit => new ComponentResourceKey(typeof(DialogKeys), "S.DialogWindow.Sumit");
        public static ComponentResourceKey Cancel => new ComponentResourceKey(typeof(DialogKeys), "S.DialogWindow.Cancel");
        public static ComponentResourceKey SumitAndCancel => new ComponentResourceKey(typeof(DialogKeys), "S.DialogWindow.SumitAndCancel");
    }
}
