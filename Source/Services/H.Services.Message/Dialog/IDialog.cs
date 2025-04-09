using H.Common.Interfaces;

namespace H.Services.Message.Dialog;

public interface IDialog : ILayoutable, ICancelable, ITransitionHostable
{
    Func<Task<bool>> CanSumit { get; set; }
    void Sumit();
    void Close();
    string Icon { get; set; }
    string Title { get; set; }
    bool? DialogResult { get; set; }
    DialogButton DialogButton { get; set; }
    Window Owner { get; set; }
    DataTemplate PresenterTemplate { get; set; }
}
